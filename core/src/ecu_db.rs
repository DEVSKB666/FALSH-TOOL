//! Honda ECU database parser - reads `C:\MZATUNER\data.ini`.
//!
//! The original `data.ini` ships with **246 Keihin** + **23 Shinden** entries.
//! Each ECU is described by four parallel `[section]` tables keyed by the
//! same `IDxxxx` identifier:
//!
//! ```ini
//! [khPartCode]
//! ID0001=38770-K2F-N01
//!
//! [khEcmId]
//! ID0001=0104080F01
//!
//! [khStartOffset]
//! ID0001=0
//!
//! [khCksumOffset]
//! ID0001=3FFF8
//! ```
//!
//! Sections that begin with `kh` are Keihin, `sh` are Shinden.

use anyhow::{Context, Result};
use configparser::ini::Ini;
use std::path::Path;

/// One ECU entry as joined across the four parallel sections.
#[derive(Debug, Clone)]
pub struct EcuEntry {
    /// Stable id like `ID0001`.
    pub id: String,
    /// OEM Honda part code (e.g. `38770-K2F-N01` or
    /// `30400-K1ZF-T01 PCX160 NOABS`).
    pub part_code: String,
    /// 5-byte hex ECM identifier (e.g. `0104080F01`).
    pub ecm_id: String,
    /// Hex start offset for flash operations (e.g. `0`, `4000`, `8000`,
    /// `E0000`).
    pub start_offset: u32,
    /// Hex offset of the ROM checksum (e.g. `3FFF8`, `7FFF8`, `FFFF8`).
    pub cksum_offset: u32,
}

/// Full database loaded from `data.ini`.
#[derive(Debug, Default)]
pub struct EcuDatabase {
    /// Keihin entries (sections beginning with `kh`).
    pub keihin: Vec<EcuEntry>,
    /// Shinden entries (sections beginning with `sh`).
    pub shinden: Vec<EcuEntry>,
}

impl EcuDatabase {
    /// Load and parse `data.ini`.
    pub fn load(path: impl AsRef<Path>) -> Result<Self> {
        let path = path.as_ref();
        let mut ini = Ini::new_cs(); // case-sensitive: "kh"/"sh" prefixes matter
        ini.load(path)
            .map_err(|e| anyhow::anyhow!("loading {path:?}: {e}"))?;
        let mut db = EcuDatabase::default();
        db.keihin  = collect(&ini, "khPartCode", "khEcmId", "khStartOffset", "khCksumOffset")?;
        db.shinden = collect(&ini, "shPartCode", "shEcmId", "shStartOffset", "shCksumOffset")?;
        Ok(db)
    }

    /// All entries (Keihin first, then Shinden) - useful for searching.
    pub fn all(&self) -> impl Iterator<Item = (Family, &EcuEntry)> {
        self.keihin
            .iter()
            .map(|e| (Family::Keihin, e))
            .chain(self.shinden.iter().map(|e| (Family::Shinden, e)))
    }

    /// Find an entry by exact ECM-id match (case-insensitive).
    pub fn find_by_ecm_id(&self, ecm_id: &str) -> Option<(Family, &EcuEntry)> {
        let needle = ecm_id.to_ascii_uppercase();
        self.all().find(|(_, e)| e.ecm_id.eq_ignore_ascii_case(&needle))
    }

    fn list_mut(&mut self, family: Family) -> &mut Vec<EcuEntry> {
        match family {
            Family::Keihin  => &mut self.keihin,
            Family::Shinden => &mut self.shinden,
        }
    }

    /// Append a new entry to the requested family. The numeric `IDxxxx`
    /// id is auto-allocated as `max_existing_id + 1`, formatted as
    /// `ID%04d`. Returns the new id.
    pub fn add(
        &mut self,
        family: Family,
        part_code: &str,
        ecm_id: &str,
        start_offset: u32,
        cksum_offset: u32,
    ) -> String {
        let next_num = self
            .list_mut(family)
            .iter()
            .map(|e| numeric_suffix(&e.id))
            .max()
            .unwrap_or(0)
            + 1;
        let id = format!("ID{:04}", next_num);
        self.list_mut(family).push(EcuEntry {
            id: id.clone(),
            part_code: part_code.to_string(),
            ecm_id: ecm_id.to_string(),
            start_offset,
            cksum_offset,
        });
        id
    }

    /// Update an entry in place. Returns `true` if the entry existed.
    pub fn update(
        &mut self,
        family: Family,
        id: &str,
        part_code: &str,
        ecm_id: &str,
        start_offset: u32,
        cksum_offset: u32,
    ) -> bool {
        if let Some(e) = self.list_mut(family).iter_mut().find(|e| e.id == id) {
            e.part_code    = part_code.to_string();
            e.ecm_id       = ecm_id.to_string();
            e.start_offset = start_offset;
            e.cksum_offset = cksum_offset;
            true
        } else {
            false
        }
    }

    /// Remove an entry by `id`. Returns `true` if the entry existed.
    pub fn delete(&mut self, family: Family, id: &str) -> bool {
        let v = self.list_mut(family);
        let before = v.len();
        v.retain(|e| e.id != id);
        v.len() != before
    }

    /// Persist the database to a `data.ini` at `path`. Layout matches
    /// the original MZA-TUNER format exactly: a header comment with
    /// timestamp followed by 8 sections (4 per family) ordered as
    /// PartCode, EcmId, StartOffset, CksumOffset.
    pub fn save(&self, path: impl AsRef<Path>) -> Result<()> {
        use std::io::Write;
        let path = path.as_ref();
        let mut f = std::fs::File::create(path)
            .with_context(|| format!("creating {path:?}"))?;

        // Best-effort timestamp (matches the leading line in the
        // shipped data.ini).
        let now = chrono_like_now();
        writeln!(f, "; MZATUNER Master Data - {now}")?;

        write_family(&mut f, "kh", &self.keihin)?;
        if !self.shinden.is_empty() {
            write_family(&mut f, "sh", &self.shinden)?;
        }
        Ok(())
    }
}

fn write_family(
    f: &mut std::fs::File,
    prefix: &str,
    entries: &[EcuEntry],
) -> std::io::Result<()> {
    use std::io::Write;
    for (sec, get_value) in [
        ("PartCode",    Box::new(|e: &EcuEntry| e.part_code.clone())     as Box<dyn Fn(&EcuEntry) -> String>),
        ("EcmId",       Box::new(|e: &EcuEntry| e.ecm_id.clone())),
        ("StartOffset", Box::new(|e: &EcuEntry| format!("{:X}", e.start_offset))),
        ("CksumOffset", Box::new(|e: &EcuEntry| format!("{:X}", e.cksum_offset))),
    ] {
        writeln!(f, "[{prefix}{sec}]")?;
        for e in entries {
            writeln!(f, "{}={}", e.id, get_value(e))?;
        }
        writeln!(f)?;
    }
    Ok(())
}

/// Tiny date-only formatter that avoids pulling in `chrono` for this
/// single timestamp. Returns something like `1/4/2026 1:55:46`.
fn chrono_like_now() -> String {
    use std::time::{SystemTime, UNIX_EPOCH};
    let secs = SystemTime::now()
        .duration_since(UNIX_EPOCH)
        .map(|d| d.as_secs())
        .unwrap_or(0);
    // Convert epoch seconds to date components without leap-second
    // weirdness. Good enough for a comment line.
    let days = secs / 86_400;
    let time_of_day = secs % 86_400;
    let h = time_of_day / 3600;
    let m = (time_of_day % 3600) / 60;
    let s = time_of_day % 60;
    let (y, mo, d) = days_to_ymd(days as i64);
    // Buddhist year (พ.ศ. - matches the Thai original).
    let buddhist = y + 543;
    format!("{d}/{mo}/{buddhist} {h}:{m:02}:{s:02}")
}

fn days_to_ymd(mut days: i64) -> (i64, u32, u32) {
    days += 719_468;
    let era = if days >= 0 { days } else { days - 146_096 } / 146_097;
    let doe = (days - era * 146_097) as u64;
    let yoe = (doe - doe / 1460 + doe / 36_524 - doe / 146_096) / 365;
    let y = yoe as i64 + era * 400;
    let doy = doe - (365 * yoe + yoe / 4 - yoe / 100);
    let mp = (5 * doy + 2) / 153;
    let d = (doy - (153 * mp + 2) / 5 + 1) as u32;
    let m = (if mp < 10 { mp + 3 } else { mp - 9 }) as u32;
    let y = if m <= 2 { y + 1 } else { y };
    (y, m, d)
}

/// Honda ECU family (parallel to `crate::crypto::EcuVariant`).
#[derive(Debug, Clone, Copy, PartialEq, Eq)]
pub enum Family {
    /// Keihin
    Keihin,
    /// Shinden
    Shinden,
}

fn collect(
    ini: &Ini,
    sec_part: &str,
    sec_ecm: &str,
    sec_start: &str,
    sec_cksum: &str,
) -> Result<Vec<EcuEntry>> {
    // Sections may simply not exist (e.g. if the file is keihin-only).
    let part_map = ini.get_map_ref().get(sec_part);
    let ecm_map = ini.get_map_ref().get(sec_ecm);
    let start_map = ini.get_map_ref().get(sec_start);
    let cksum_map = ini.get_map_ref().get(sec_cksum);

    let part_map = match part_map {
        Some(m) => m,
        None => return Ok(Vec::new()),
    };

    let mut keys: Vec<&String> = part_map.keys().collect();
    // Sort by numeric suffix so ID0001 < ID0010 < ID0100
    keys.sort_by_key(|k| numeric_suffix(k));

    let mut out = Vec::with_capacity(keys.len());
    for key in keys {
        let id = key.clone();
        let part = part_map
            .get(key)
            .and_then(|v| v.clone())
            .unwrap_or_default();
        let ecm = ecm_map
            .and_then(|m| m.get(key))
            .and_then(|v| v.clone())
            .unwrap_or_default();
        let start = parse_hex_u32(
            start_map
                .and_then(|m| m.get(key))
                .and_then(|v| v.clone())
                .as_deref(),
        )
        .with_context(|| format!("parsing start offset for {id}"))?;
        let cksum = parse_hex_u32(
            cksum_map
                .and_then(|m| m.get(key))
                .and_then(|v| v.clone())
                .as_deref(),
        )
        .with_context(|| format!("parsing cksum offset for {id}"))?;
        out.push(EcuEntry {
            id,
            part_code: part,
            ecm_id: ecm,
            start_offset: start,
            cksum_offset: cksum,
        });
    }
    Ok(out)
}

fn parse_hex_u32(s: Option<&str>) -> Result<u32> {
    let s = s.unwrap_or("0").trim();
    if s.is_empty() {
        return Ok(0);
    }
    u32::from_str_radix(s.trim_start_matches("0x"), 16)
        .with_context(|| format!("invalid hex {s:?}"))
}

fn numeric_suffix(key: &str) -> u32 {
    key.trim_start_matches(|c: char| !c.is_ascii_digit())
        .parse()
        .unwrap_or(0)
}

#[cfg(test)]
mod tests {
    use super::*;
    use std::io::Write;

    #[test]
    fn add_update_delete_roundtrip() -> Result<()> {
        let mut db = EcuDatabase::default();
        let id = db.add(Family::Keihin, "38770-TEST-001", "ABCDEF1234", 0x1000, 0x3FFF8);
        assert_eq!(id, "ID0001");
        assert_eq!(db.keihin.len(), 1);

        let id2 = db.add(Family::Keihin, "38770-TEST-002", "FEDCBA0987", 0x2000, 0x7FFF8);
        assert_eq!(id2, "ID0002");

        // update existing
        assert!(db.update(Family::Keihin, "ID0001", "38770-CHANGED", "1234567890", 0, 0xFFFF8));
        assert_eq!(db.keihin[0].part_code, "38770-CHANGED");
        assert_eq!(db.keihin[0].cksum_offset, 0xFFFF8);

        // update non-existent
        assert!(!db.update(Family::Keihin, "ID9999", "x", "y", 0, 0));

        // delete + verify gone
        assert!(db.delete(Family::Keihin, "ID0001"));
        assert_eq!(db.keihin.len(), 1);
        assert_eq!(db.keihin[0].id, "ID0002");

        // delete non-existent
        assert!(!db.delete(Family::Keihin, "ID9999"));

        // next add should jump to ID0003 (highest existing was 2).
        let id3 = db.add(Family::Keihin, "38770-NEW-003", "1111111111", 0, 0);
        assert_eq!(id3, "ID0003");
        Ok(())
    }

    #[test]
    fn save_then_reload() -> Result<()> {
        let temp = tempdir_like()?;
        let path = temp.join("data.ini");

        let mut db = EcuDatabase::default();
        db.add(Family::Keihin,  "38770-K2F-N01",        "0104080F01", 0x0,    0x3FFF8);
        db.add(Family::Keihin,  "38770-MKK-D02",        "0102A40101", 0x4000, 0xFFFF8);
        db.add(Family::Shinden, "30400-K1ZF-T01 PCX160", "0200000001", 0x8000, 0x1FFF8);
        db.save(&path)?;

        let reloaded = EcuDatabase::load(&path)?;
        assert_eq!(reloaded.keihin.len(),  2);
        assert_eq!(reloaded.shinden.len(), 1);
        assert_eq!(reloaded.keihin[0].part_code,    "38770-K2F-N01");
        assert_eq!(reloaded.keihin[1].cksum_offset, 0xFFFF8);
        assert_eq!(reloaded.shinden[0].part_code,   "30400-K1ZF-T01 PCX160");
        Ok(())
    }

    #[test]
    fn parse_minimal_inline() -> Result<()> {
        let temp = tempdir_like()?;
        let path = temp.join("data.ini");
        let mut f = std::fs::File::create(&path)?;
        writeln!(f, "[khPartCode]")?;
        writeln!(f, "ID0001=38770-K2F-N01")?;
        writeln!(f, "ID0002=38770-MKK-D02")?;
        writeln!(f, "")?;
        writeln!(f, "[khEcmId]")?;
        writeln!(f, "ID0001=0104080F01")?;
        writeln!(f, "ID0002=0102A40101")?;
        writeln!(f, "")?;
        writeln!(f, "[khStartOffset]")?;
        writeln!(f, "ID0001=0")?;
        writeln!(f, "ID0002=4000")?;
        writeln!(f, "")?;
        writeln!(f, "[khCksumOffset]")?;
        writeln!(f, "ID0001=3FFF8")?;
        writeln!(f, "ID0002=FFFF8")?;
        drop(f);

        let db = EcuDatabase::load(&path)?;
        assert_eq!(db.keihin.len(), 2);
        assert_eq!(db.keihin[0].part_code, "38770-K2F-N01");
        assert_eq!(db.keihin[0].ecm_id, "0104080F01");
        assert_eq!(db.keihin[0].start_offset, 0);
        assert_eq!(db.keihin[0].cksum_offset, 0x3_FFF8);
        assert_eq!(db.keihin[1].cksum_offset, 0xF_FFF8);
        Ok(())
    }

    /// Tiny self-contained equivalent of the `tempfile` crate so we don't
    /// pull in another dependency just for the unit tests.
    fn tempdir_like() -> Result<std::path::PathBuf> {
        let mut p = std::env::temp_dir();
        p.push(format!(
            "mza-tuner-rs-test-{}",
            std::time::SystemTime::now()
                .duration_since(std::time::UNIX_EPOCH)?
                .as_nanos()
        ));
        std::fs::create_dir_all(&p)?;
        Ok(p)
    }
}
