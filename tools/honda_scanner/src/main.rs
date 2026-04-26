//! Scan a Flutter AOT `app.so` (or any binary blob) for byte sequences
//! that look like Honda K-Line frames. The goal is to recover the
//! actual byte sequences `MultiGaugesHondaMonitor` uses for its DTC
//! protocol, since the Dart source isn't directly recoverable from the
//! AOT snapshot.
//!
//! For every "anchor" sequence we know (Keihin / Shinden init headers,
//! cell-read templates) we walk the binary and dump a small window of
//! bytes around each hit. The hope is that DTC frames live alongside
//! the EEPROM / live-data frames in `.rodata` and become visible by
//! visual inspection of the dump.
//!
//! Run as:
//!   cargo run --manifest-path tools/honda_scanner/Cargo.toml --release -- \
//!       :auto out\honda_scan.txt

use std::env;
use std::fs;
use std::io::Write;
use std::path::PathBuf;

#[derive(Debug)]
struct Anchor {
    label:   &'static str,
    pattern: &'static [u8],
}

const ANCHORS: &[Anchor] = &[
    Anchor { label: "FAST_INIT_WAKE",   pattern: &[0xFE, 0x04, 0x72, 0x8C] },
    Anchor { label: "ESTABLISH_COMM",   pattern: &[0x72, 0x05, 0x00, 0xF0, 0x99] },
    Anchor { label: "KEIHIN_DIAG_HEAD", pattern: &[0x91, 0x91, 0x0D, 0xDF] },
    Anchor { label: "KEIHIN_CELL_READ", pattern: &[0x91, 0x91, 0x07, 0x40] },
    Anchor { label: "SHINDEN_HELLO_HD", pattern: &[0x27, 0x0B, 0xE0] },
    Anchor { label: "SHINDEN_SEED",     pattern: &[0x7E, 0x06, 0x01, 0x01, 0x00, 0x7A] },
    Anchor { label: "SHINDEN_CELL",     pattern: &[0x82, 0x82, 0x10, 0x06] },
    // KWP2000 standard - in case the monitor falls back to ISO 14230
    // service IDs for DTC reads/clears.
    Anchor { label: "KWP_READ_DTC_18",  pattern: &[0x18, 0x02, 0xFF, 0xFF] },
    Anchor { label: "KWP_CLEAR_DTC_14", pattern: &[0x14, 0xFF, 0x00] },
    Anchor { label: "KWP_TESTER_3E",    pattern: &[0x3E, 0x01] },
    Anchor { label: "KWP_START_DIAG_10",pattern: &[0x10, 0x82] },
];

const WINDOW_BEFORE: usize = 16;
const WINDOW_AFTER:  usize = 32;

fn main() -> std::io::Result<()> {
    let args: Vec<String> = env::args().collect();
    if args.len() < 3 {
        eprintln!("usage: {} <input or :auto> <output.txt>", args[0]);
        std::process::exit(2);
    }
    let input = &args[1];
    let output = &args[2];

    // Same `:auto` trick as `extract_strings` - cmd.exe codepage can't
    // handle the Thai-named MZA-TUNER subfolder so we resolve it
    // through `fs::read_dir` (which uses wide Win32 APIs internally).
    let resolved: PathBuf = if input == ":auto" {
        let root = PathBuf::from(r"C:\MZATUNER\data");
        let mut hit = None;
        for entry in fs::read_dir(&root)? {
            let entry = entry?;
            let p = entry.path();
            if p.is_dir() {
                let candidate = p.join("data").join("app.so");
                if candidate.exists() { hit = Some(candidate); break; }
            }
        }
        hit.ok_or_else(|| std::io::Error::new(std::io::ErrorKind::NotFound,
                                              format!("no app.so found under {root:?}")))?
    } else {
        PathBuf::from(input)
    };

    let bytes = fs::read(&resolved)?;
    eprintln!("[*] read {} bytes from {}", bytes.len(), resolved.display());

    let mut out = fs::File::create(output)?;
    let mut total_hits = 0usize;

    for anchor in ANCHORS {
        let mut hits = Vec::new();
        let mut i = 0;
        while i + anchor.pattern.len() <= bytes.len() {
            if &bytes[i..i + anchor.pattern.len()] == anchor.pattern {
                hits.push(i);
                i += 1;
            } else {
                i += 1;
            }
        }
        writeln!(out, "## {} ({:?}) - {} hits", anchor.label, anchor.pattern, hits.len())?;
        for &offset in &hits {
            let lo = offset.saturating_sub(WINDOW_BEFORE);
            let hi = (offset + anchor.pattern.len() + WINDOW_AFTER).min(bytes.len());
            let window = &bytes[lo..hi];
            writeln!(out, "  @0x{:08X}  {}", offset, hex_dump(window, offset - lo))?;
        }
        writeln!(out)?;
        total_hits += hits.len();
        eprintln!("[*] {:24} {:5} hits", anchor.label, hits.len());
    }

    eprintln!("[*] {} total hits, dumped to {}", total_hits, output);
    Ok(())
}

fn hex_dump(bytes: &[u8], anchor_off: usize) -> String {
    let mut s = String::new();
    for (i, b) in bytes.iter().enumerate() {
        if i == anchor_off { s.push('|'); }
        s.push_str(&format!("{:02X}", b));
        if i == anchor_off { s.push('|'); }
        s.push(' ');
    }
    s
}
