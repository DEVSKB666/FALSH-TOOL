//! Pull printable ASCII / UTF-8 strings out of a binary blob.
//!
//! Mirrors the venerable `strings(1)` utility but with two extras:
//!   * keyword-grep filter (case-insensitive substrings)
//!   * UTF-8 awareness for our Thai text (multi-byte BMP)
//!
//! Run as:
//!   cargo run --manifest-path tools/extract_strings/Cargo.toml -- \
//!       <input.bin> <output.txt> [keyword1 keyword2 ...]
//!
//! Without keywords every >=8-char run is dumped. Otherwise we only
//! keep strings that contain *any* of the (lowercased) keywords.

use std::env;
use std::fs;
use std::io::Write;
use std::path::PathBuf;

const MIN_RUN: usize = 8;

fn is_printable_ascii(b: u8) -> bool {
    (0x20..=0x7E).contains(&b) || b == b'\t'
}

fn extract_ascii(bytes: &[u8]) -> Vec<String> {
    let mut out = Vec::new();
    let mut start: Option<usize> = None;
    for (i, &b) in bytes.iter().enumerate() {
        if is_printable_ascii(b) {
            if start.is_none() {
                start = Some(i);
            }
        } else {
            if let Some(s) = start.take() {
                if i - s >= MIN_RUN {
                    if let Ok(text) = std::str::from_utf8(&bytes[s..i]) {
                        out.push(text.to_string());
                    }
                }
            }
        }
    }
    if let Some(s) = start {
        if bytes.len() - s >= MIN_RUN {
            if let Ok(text) = std::str::from_utf8(&bytes[s..]) {
                out.push(text.to_string());
            }
        }
    }
    out
}

/// Walk the bytes looking for runs of valid UTF-8 that contain at
/// least one non-ASCII codepoint - this catches Thai / CJK strings
/// that the ASCII pass would split apart.
fn extract_utf8_thai(bytes: &[u8]) -> Vec<String> {
    let mut out = Vec::new();
    let mut i = 0;
    while i < bytes.len() {
        // try to decode a UTF-8 sequence starting at i
        let n_max = (bytes.len() - i).min(4);
        let mut consumed = 0;
        for n in (1..=n_max).rev() {
            if std::str::from_utf8(&bytes[i..i + n]).is_ok() {
                consumed = n;
                break;
            }
        }
        if consumed == 0 {
            i += 1;
            continue;
        }
        let mut end = i + consumed;
        let mut has_thai = (0xE00..=0xE7F)
            .contains(&bytes[i..end].iter().fold(0u32, |a, &b| (a << 8) | (b as u32)));
        // greedily extend the run as long as the next codepoint also
        // decodes cleanly and is printable
        while end < bytes.len() {
            let nm = (bytes.len() - end).min(4);
            let mut step = 0;
            for n in (1..=nm).rev() {
                if let Ok(s) = std::str::from_utf8(&bytes[end..end + n]) {
                    let ch = s.chars().next().unwrap();
                    if (ch as u32) >= 0x20 && ch != '\u{007F}' {
                        step = n;
                        if (0x0E00..=0x0E7F).contains(&(ch as u32)) {
                            has_thai = true;
                        }
                        break;
                    }
                }
            }
            if step == 0 { break; }
            end += step;
        }
        if has_thai && end - i >= 6 {
            if let Ok(text) = std::str::from_utf8(&bytes[i..end]) {
                out.push(text.to_string());
            }
        }
        i = end.max(i + 1);
    }
    out
}

fn main() -> std::io::Result<()> {
    let args: Vec<String> = env::args().collect();
    if args.len() < 3 {
        eprintln!("usage: {} <input> <output> [keyword ...]", args[0]);
        std::process::exit(2);
    }
    let input = &args[1];
    let output = &args[2];
    let keywords: Vec<String> = args[3..].iter().map(|s| s.to_lowercase()).collect();

    // The MZA-TUNER monitor lives under a Thai-named subfolder which the
    // Windows console codepage mangles when forwarded as argv. To make the
    // tool callable from cmd.exe regardless of CP, we accept either a
    // direct path *or* a glob expression like "C:\\MZATUNER\\data\\*\\data\\app.so".
    // The MZA-TUNER monitor lives under a Thai-named subfolder which the
    // Windows console codepage mangles when forwarded as argv. So when
    // the user passes `:auto` we walk the well-known install root and
    // pick the first child directory containing `data\app.so`.
    let resolved: PathBuf = if input == ":auto" {
        let root = PathBuf::from(r"C:\MZATUNER\data");
        eprintln!("[*] :auto - scanning {}", root.display());
        let mut hit = None;
        for entry in fs::read_dir(&root)? {
            let entry = match entry {
                Ok(e) => e,
                Err(e) => { eprintln!("[?] skip entry: {e}"); continue; }
            };
            let p = entry.path();
            eprintln!("[?] candidate: {}", p.display());
            if p.is_dir() {
                let candidate = p.join("data").join("app.so");
                if candidate.exists() {
                    hit = Some(candidate);
                    break;
                }
            }
        }
        hit.ok_or_else(|| std::io::Error::new(std::io::ErrorKind::NotFound,
                                              format!("no app.so found under {root:?}")))?
    } else {
        PathBuf::from(input)
    };

    let bytes = fs::read(&resolved)?;
    eprintln!("[*] read {} bytes from {}", bytes.len(), resolved.display());

    let mut strings = extract_ascii(&bytes);
    strings.extend(extract_utf8_thai(&bytes));
    eprintln!("[*] {} candidate strings", strings.len());

    if !keywords.is_empty() {
        strings.retain(|s| {
            let lower = s.to_lowercase();
            keywords.iter().any(|k| lower.contains(k))
        });
        eprintln!("[*] {} strings match keywords {:?}", strings.len(), keywords);
    }

    // dedup but preserve order
    let mut seen = std::collections::HashSet::new();
    strings.retain(|s| seen.insert(s.clone()));

    let mut out = fs::File::create(output)?;
    for s in &strings {
        writeln!(out, "{s}")?;
    }
    eprintln!("[*] wrote {} lines to {}", strings.len(), output);
    Ok(())
}
