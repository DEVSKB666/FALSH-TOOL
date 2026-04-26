//! `mzactl` - command-line tool for the Rust port.
//!
//! Subcommands:
//!
//! * `decrypt-ecu` - turn an `.ECU` / `.ACG` file into a raw `.BIN`
//! * `encrypt-ecu` - the inverse
//! * `list-ecus`   - dump the Honda part-code database
//! * `find-ecm`    - look up an ECM-id in the database

use anyhow::{Context, Result};
use clap::{Parser, Subcommand, ValueEnum};
use std::fs;
use std::path::PathBuf;

use mza_tuner::crypto::{decrypt_ecu_file, encrypt_ecu_file, EcuVariant};
use mza_tuner::ecu_db::{EcuDatabase, Family};

#[derive(Parser, Debug)]
#[command(name = "mzactl", version = mza_tuner::VERSION,
          about = "MZA-TUNER Rust port - file utilities (no hardware required)")]
struct Cli {
    #[command(subcommand)]
    cmd: Cmd,
}

#[derive(Subcommand, Debug)]
enum Cmd {
    /// Decrypt a .ECU / .ACG file -> raw .BIN
    DecryptEcu {
        /// Path to the encrypted .ECU/.ACG file
        #[arg(short, long)]
        input: PathBuf,
        /// Where to write the decrypted bytes
        #[arg(short, long)]
        output: PathBuf,
        /// ECU family
        #[arg(short, long, value_enum, default_value_t = Variant::Keihin)]
        variant: Variant,
        /// Override the password (default = the variant's hardcoded password)
        #[arg(short, long)]
        password: Option<String>,
    },
    /// Encrypt a raw .BIN -> .ECU / .ACG
    EncryptEcu {
        #[arg(short, long)]
        input: PathBuf,
        #[arg(short, long)]
        output: PathBuf,
        #[arg(short, long, value_enum, default_value_t = Variant::Keihin)]
        variant: Variant,
        #[arg(short, long)]
        password: Option<String>,
    },
    /// List Honda part codes from data.ini
    ListEcus {
        /// Path to data.ini (defaults to the original install location)
        #[arg(short, long, default_value = "C:\\MZATUNER\\data.ini")]
        db: PathBuf,
        /// Filter by part-code substring (case-insensitive)
        #[arg(short, long)]
        filter: Option<String>,
        /// Show only this family
        #[arg(short = 'F', long, value_enum)]
        family: Option<Variant>,
    },
    /// Look up an ECM-id (5-byte hex like `0104080F01`)
    FindEcm {
        /// The ECM id to search for
        ecm_id: String,
        #[arg(short, long, default_value = "C:\\MZATUNER\\data.ini")]
        db: PathBuf,
    },
}

#[derive(ValueEnum, Clone, Debug)]
enum Variant {
    Keihin,
    Shinden,
}

impl From<Variant> for EcuVariant {
    fn from(v: Variant) -> Self {
        match v {
            Variant::Keihin => EcuVariant::Keihin,
            Variant::Shinden => EcuVariant::Shinden,
        }
    }
}

impl From<Variant> for Family {
    fn from(v: Variant) -> Self {
        match v {
            Variant::Keihin => Family::Keihin,
            Variant::Shinden => Family::Shinden,
        }
    }
}

fn main() -> Result<()> {
    let args = Cli::parse();
    match args.cmd {
        Cmd::DecryptEcu { input, output, variant, password } => {
            let txt = fs::read_to_string(&input)
                .with_context(|| format!("reading {input:?}"))?;
            let plain = decrypt_ecu_file(&txt, variant.into(), password.as_deref())
                .with_context(|| format!("decrypting {input:?}"))?;
            fs::write(&output, &plain)
                .with_context(|| format!("writing {output:?}"))?;
            println!("[+] decrypted {} -> {} ({} bytes)",
                     input.display(), output.display(), plain.len());
        }
        Cmd::EncryptEcu { input, output, variant, password } => {
            let bin = fs::read(&input)
                .with_context(|| format!("reading {input:?}"))?;
            let txt = encrypt_ecu_file(&bin, variant.into(), password.as_deref());
            fs::write(&output, &txt)
                .with_context(|| format!("writing {output:?}"))?;
            println!("[+] encrypted {} -> {} ({} bytes -> {} chars)",
                     input.display(), output.display(), bin.len(), txt.len());
        }
        Cmd::ListEcus { db, filter, family } => {
            let database = EcuDatabase::load(&db)
                .with_context(|| format!("loading {db:?}"))?;
            let needle = filter.as_deref().map(str::to_lowercase);
            let mut shown = 0usize;
            println!("{:<8} {:<6} {:<32} {:<12} {:>8} {:>8}",
                     "ID", "FAM", "PART_CODE", "ECM_ID", "START", "CKSUM");
            for (fam, e) in database.all() {
                if let Some(ref f) = family {
                    if Family::from(f.clone()) != fam { continue; }
                }
                if let Some(ref n) = needle {
                    if !e.part_code.to_lowercase().contains(n)
                       && !e.ecm_id.to_lowercase().contains(n) { continue; }
                }
                let fam_short = match fam { Family::Keihin => "Kh", Family::Shinden => "Sh" };
                println!("{:<8} {:<6} {:<32} {:<12} {:>8X} {:>8X}",
                         e.id, fam_short, truncate(&e.part_code, 32),
                         e.ecm_id, e.start_offset, e.cksum_offset);
                shown += 1;
            }
            println!("\n[total: {shown}]");
        }
        Cmd::FindEcm { ecm_id, db } => {
            let database = EcuDatabase::load(&db)?;
            match database.find_by_ecm_id(&ecm_id) {
                Some((fam, e)) => {
                    let fam_short = match fam { Family::Keihin => "Keihin", Family::Shinden => "Shinden" };
                    println!("Found:");
                    println!("  family       : {fam_short}");
                    println!("  id           : {}", e.id);
                    println!("  part_code    : {}", e.part_code);
                    println!("  ecm_id       : {}", e.ecm_id);
                    println!("  start_offset : 0x{:X}", e.start_offset);
                    println!("  cksum_offset : 0x{:X}", e.cksum_offset);
                }
                None => println!("ECM-id {ecm_id:?} not found in {db:?}"),
            }
        }
    }
    Ok(())
}

fn truncate(s: &str, max: usize) -> String {
    if s.chars().count() <= max { s.to_string() }
    else { s.chars().take(max - 1).collect::<String>() + "…" }
}
