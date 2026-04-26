//! XDF / ADX password breaker.
//!
//! Direct port of `Form4_XdfAdxBreaker.cs` (the "RED_MATRIX" UI in the
//! original MAZA program). The file format is a 264-byte fixed header
//! followed by an RC4-encrypted ASCII XML body. The breaker:
//!
//! 1. validates the magic bytes `05 22 97`,
//! 2. derives an RC4 key from a password using the Microsoft
//!    `CryptDeriveKey` algorithm (SHA-1 + ipad/opad MAC, 128-bit key),
//! 3. RC4-decrypts the body,
//! 4. strips `<openpassword>` and `<modifypassword>` lines,
//! 5. resets `<flags>` (`0x10000` for `.adx`, `0x1` for `.xdf`),
//! 6. returns the cleaned plaintext (caller writes it to disk).
//!
//! The original code only encrypts on save when needed - we never
//! re-encrypt because the goal is to *remove* the password, matching
//! the C# behaviour (`File.WriteAllText` of plaintext).

use sha1::{Digest, Sha1};
use thiserror::Error;

/// Magic bytes that identify a password-protected XDF/ADX file.
pub const MAGIC: [u8; 3] = [0x05, 0x22, 0x97];

/// Header length skipped before the encrypted payload.
pub const HEADER_LEN: usize = 264;

/// Default password used by the original program when `bytes[131] == 0`.
pub const DEFAULT_PASSWORD: &str = "$9Ad0$!#*)1QqPow^";

/// Variant decided by file extension - drives the `<flags>` reset.
#[derive(Debug, Clone, Copy, PartialEq, Eq)]
pub enum XdfVariant {
    /// `.xdf` (TunerPro definition file) - resets to `<flags>0x1</flags>`.
    Xdf,
    /// `.adx` (TunerPro acquisition definition) - resets to `<flags>0x10000</flags>`.
    Adx,
}

impl XdfVariant {
    /// Pick the variant from a filename based on the extension.
    pub fn from_filename(name: &str) -> Self {
        if name.to_ascii_lowercase().ends_with(".adx") {
            Self::Adx
        } else {
            Self::Xdf
        }
    }
    /// The `<flags>` line that replaces the original (variant-dependent).
    pub fn flags_line(self) -> &'static str {
        match self {
            Self::Adx => "    <flags>0x10000</flags>\r\n",
            Self::Xdf => "    <flags>0x1</flags>\r\n",
        }
    }
}

/// Errors raised by [`break_xdf_adx`].
#[derive(Debug, Error)]
pub enum XdfError {
    /// The input file is shorter than the 264-byte fixed header.
    #[error("file too short: {len} bytes (need at least {min})")]
    TooShort {
        /// Length of the input file in bytes.
        len: usize,
        /// Minimum required length (always [`HEADER_LEN`]).
        min: usize,
    },

    /// First three bytes of the file are not `05 22 97`.
    #[error("file does not have the XDF/ADX magic bytes 05 22 97")]
    BadMagic,

    /// Decryption succeeded but the output does not contain `ADXHEADER`
    /// or `XDFHEADER` - the password was wrong.
    #[error("decryption produced non-XML output - wrong password?")]
    WrongPassword,
}

/// Outcome of a successful break.
#[derive(Debug, Clone)]
pub struct XdfBreakResult {
    /// File variant detected from the filename extension.
    pub variant:         XdfVariant,
    /// Recovered plaintext of the original `<openpassword>` element.
    pub open_password:   String,
    /// Recovered plaintext of the original `<modifypassword>` element.
    pub modify_password: String,
    /// Cleaned ASCII XML body (passwords stripped, flags reset). This
    /// is the exact text the original "เซฟไฟล์" button writes to disk.
    pub plaintext:       String,
}

/// Break the password on an XDF/ADX file. The returned `plaintext` is
/// what the original "เซฟไฟล์" button writes to disk (no re-encryption).
pub fn break_xdf_adx(
    file_bytes: &[u8],
    filename: &str,
    password: Option<&str>,
) -> Result<XdfBreakResult, XdfError> {
    if file_bytes.len() < HEADER_LEN {
        return Err(XdfError::TooShort { len: file_bytes.len(), min: HEADER_LEN });
    }
    if file_bytes[..3] != MAGIC {
        return Err(XdfError::BadMagic);
    }

    let pwd = password.unwrap_or(DEFAULT_PASSWORD);
    let variant = XdfVariant::from_filename(filename);

    // Decrypt the body (everything after the 264-byte header).
    let key = derive_rc4_key_ms(pwd.as_bytes(), 16); // 128-bit RC4
    let mut body = file_bytes[HEADER_LEN..].to_vec();
    rc4_in_place(&key, &mut body);

    // Validate by looking for the canonical XDF/ADX header marker.
    let plain = String::from_utf8_lossy(&body).into_owned();
    let header_present =
        plain.contains("ADXHEADER") || plain.contains("XDFHEADER") || file_bytes[131] > 0;
    if !header_present {
        return Err(XdfError::WrongPassword);
    }

    // Walk every line; strip / replace as the original does.
    let mut out_open  = String::new();
    let mut out_mod   = String::new();
    let mut cleaned   = String::new();
    for line in plain.replace('\r', "").split('\n') {
        if line.trim().is_empty() {
            continue;
        }
        if line.contains("openpassword") {
            out_open = line
                .replace("    <openpassword>", "")
                .replace("</openpassword>", "");
        } else if line.contains("modifypassword") {
            out_mod = line
                .replace("    <modifypassword>", "")
                .replace("</modifypassword>", "");
        } else if line.contains("<flags>0x") {
            cleaned.push_str(variant.flags_line());
        } else {
            cleaned.push_str(line);
            cleaned.push_str("\r\n");
        }
    }

    Ok(XdfBreakResult {
        variant,
        open_password:   out_open,
        modify_password: out_mod,
        plaintext:       cleaned,
    })
}

// --- Microsoft `CryptDeriveKey` ---------------------------------------

/// Replicate Microsoft's RC4 key derivation from a SHA-1 password hash.
///
/// Algorithm (from `CryptDeriveKey` MSDN docs, summarised):
///   1. h = SHA1(password)
///   2. ipad = 64-byte buffer of 0x36, XOR first 20 bytes with h
///   3. opad = 64-byte buffer of 0x5C, XOR first 20 bytes with h
///   4. derived = SHA1(ipad) || SHA1(opad)            (40 bytes)
///   5. key = first `key_len` bytes of `derived`
///
/// Provider "Microsoft Enhanced Cryptographic Provider v1.0" defaults
/// to 128-bit RC4 (`key_len = 16`).
fn derive_rc4_key_ms(password: &[u8], key_len: usize) -> Vec<u8> {
    let mut hasher = Sha1::new();
    hasher.update(password);
    let h = hasher.finalize();

    let mut ipad = [0x36u8; 64];
    let mut opad = [0x5Cu8; 64];
    for (i, b) in h.iter().enumerate() {
        ipad[i] ^= b;
        opad[i] ^= b;
    }

    let k1: [u8; 20] = Sha1::digest(&ipad).into();
    let k2: [u8; 20] = Sha1::digest(&opad).into();

    let mut derived = Vec::with_capacity(40);
    derived.extend_from_slice(&k1);
    derived.extend_from_slice(&k2);
    derived.truncate(key_len);
    derived
}

/// Stream-cipher RC4 (encrypt == decrypt). Mutates `data` in place.
fn rc4_in_place(key: &[u8], data: &mut [u8]) {
    let mut s: [u8; 256] = std::array::from_fn(|i| i as u8);
    let mut j: u8 = 0;
    for i in 0..256 {
        j = j.wrapping_add(s[i]).wrapping_add(key[i % key.len()]);
        s.swap(i, j as usize);
    }

    let mut i: u8 = 0;
    let mut j: u8 = 0;
    for byte in data.iter_mut() {
        i = i.wrapping_add(1);
        j = j.wrapping_add(s[i as usize]);
        s.swap(i as usize, j as usize);
        let k = s[(s[i as usize].wrapping_add(s[j as usize])) as usize];
        *byte ^= k;
    }
}

// --- Tests ------------------------------------------------------------

#[cfg(test)]
mod tests {
    use super::*;

    /// Sanity check the RC4 implementation against RFC 6229 vector
    /// `Key="Key", Plaintext="Plaintext"`.
    #[test]
    fn rc4_known_answer() {
        let mut buf = b"Plaintext".to_vec();
        rc4_in_place(b"Key", &mut buf);
        assert_eq!(buf, [0xBB, 0xF3, 0x16, 0xE8, 0xD9, 0x40, 0xAF, 0x0A, 0xD3]);
    }

    /// The MS-derived RC4 key must be deterministic for a given password.
    #[test]
    fn derive_rc4_key_is_deterministic() {
        let a = derive_rc4_key_ms(b"$9Ad0$!#*)1QqPow^", 16);
        let b = derive_rc4_key_ms(b"$9Ad0$!#*)1QqPow^", 16);
        assert_eq!(a, b);
        assert_eq!(a.len(), 16);
    }

    #[test]
    fn variant_from_filename() {
        assert_eq!(XdfVariant::from_filename("foo.adx"), XdfVariant::Adx);
        assert_eq!(XdfVariant::from_filename("foo.XDF"), XdfVariant::Xdf);
        assert_eq!(XdfVariant::from_filename("FOO.ADX"), XdfVariant::Adx);
    }

    #[test]
    fn rejects_bad_magic() {
        let bytes = vec![0x00; HEADER_LEN + 10];
        let err = break_xdf_adx(&bytes, "x.xdf", None).unwrap_err();
        assert!(matches!(err, XdfError::BadMagic));
    }

    #[test]
    fn rejects_too_short() {
        let bytes = vec![0x05, 0x22, 0x97]; // only the magic
        let err = break_xdf_adx(&bytes, "x.xdf", None).unwrap_err();
        assert!(matches!(err, XdfError::TooShort { .. }));
    }
}
