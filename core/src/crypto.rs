//! ACG/ECU file crypto - Rust port of `Form3_AcgEcuToBin.cs`.
//!
//! Algorithm (matches the .NET `RijndaelManaged` defaults):
//!
//! - 32-byte key derived from MD5 of the password (placed at two overlapping
//!   offsets - see [`derive_key_keihin`] / [`derive_key_shinden`])
//! - AES-256 in ECB mode with PKCS#7 padding
//! - Plaintext is sliced into chunks before encryption; each ciphertext chunk
//!   is then Base64-encoded and concatenated with no separator. The chunk
//!   size in the original program is **43712 Base64 characters per chunk**
//!   (so each ciphertext chunk = 43712 * 3/4 = 32784 raw bytes).
//!
//! The hardcoded passwords live publicly inside the obfuscated `.exe`
//! (extracted via reflection during deobfuscation) and are exposed in
//! [`passwords`].

use aes::cipher::{
    block_padding::Pkcs7, BlockDecryptMut, BlockEncryptMut, KeyInit,
};
use aes::Aes256;
use base64::{engine::general_purpose::STANDARD, Engine as _};
use md5::{Digest, Md5};

/// Number of Base64 characters per ciphertext chunk in the wire format.
pub const CHUNK_BASE64_CHARS: usize = 43_712;

/// Plaintext bytes per encryption chunk (must be a multiple of 16 and small
/// enough that `chunk + PKCS7` stays under [`CHUNK_BASE64_CHARS`] of Base64).
/// `32_768` gives 32_784 ciphertext bytes after padding -> exactly 43_712
/// Base64 chars, matching the original.
pub const CHUNK_PLAINTEXT_BYTES: usize = 32_768;

/// Hardcoded ECU-file passwords lifted from the `.exe`.
///
/// Found in `Form3_AcgEcuToBin.cs` - both passwords are stored as plaintext
/// `private string` fields and only protect FILE crypto, not the on-the-wire
/// ECU communication.
pub mod passwords {
    /// ASCII path - applied via [`super::derive_key_keihin`].
    pub const KEIHIN: &str =
        "@ecu_homdaa!&2023*mnhdaK#^&hcbaHBQD@0lanmBV#!";
    /// BigEndian-Unicode path - applied via [`super::derive_key_shinden`].
    pub const SHINDEN: &str = "@Shinden@9919";
}

/// Honda ECU family - determines which key-derivation path to use.
#[derive(Debug, Clone, Copy, PartialEq, Eq)]
pub enum EcuVariant {
    /// Keihin - ASCII MD5 path.
    Keihin,
    /// Shinden - BigEndian-Unicode (UTF-16BE) MD5 path.
    Shinden,
}

impl EcuVariant {
    /// Default password for this variant.
    pub fn default_password(self) -> &'static str {
        match self {
            EcuVariant::Keihin => passwords::KEIHIN,
            EcuVariant::Shinden => passwords::SHINDEN,
        }
    }

    /// Derive the 32-byte AES-256 key for this variant + password.
    pub fn derive_key(self, password: &str) -> [u8; 32] {
        match self {
            EcuVariant::Keihin => derive_key_keihin(password),
            EcuVariant::Shinden => derive_key_shinden(password),
        }
    }
}

/// Build the AES-256 key the way `Form3.Decrypt(string A_0, string A_1)`
/// builds it for the Keihin (`@ecu_homdaa...`) password:
///
/// 1. Compute `MD5(ASCII(password))` -> 16 bytes
/// 2. Lay it down twice in a 32-byte buffer:
///    - first copy at offset 0  (bytes 0..16)
///    - second copy at offset 15 (bytes 15..31; overlaps byte 15)
/// 3. Byte 31 stays zero.
pub fn derive_key_keihin(password: &str) -> [u8; 32] {
    let hash = Md5::digest(password.as_bytes());
    let mut key = [0u8; 32];
    key[0..16].copy_from_slice(&hash);
    key[15..31].copy_from_slice(&hash);
    key
}

/// Build the AES-256 key for Shinden (matches `Form3.M_6` in the source):
///
/// 1. Encode password as UTF-16BE
/// 2. Compute MD5 -> 16 bytes
/// 3. Lay it down twice with strange overlap:
///    - first copy at offset 6  (bytes 6..22)
///    - second copy at offset 2 (bytes 2..18; overlaps part of first copy)
pub fn derive_key_shinden(password: &str) -> [u8; 32] {
    let beu: Vec<u8> = password
        .encode_utf16()
        .flat_map(|c| [(c >> 8) as u8, (c & 0xFF) as u8])
        .collect();
    let hash = Md5::digest(&beu);
    let mut key = [0u8; 32];
    // exactly the same Array.Copy order as the .NET source
    key[6..22].copy_from_slice(&hash);
    key[2..18].copy_from_slice(&hash);
    key
}

type Aes256EcbEnc = ecb::Encryptor<Aes256>;
type Aes256EcbDec = ecb::Decryptor<Aes256>;

/// AES-256-ECB-PKCS7 encrypt one buffer of plaintext (any length).
pub fn aes_encrypt_pkcs7(key: &[u8; 32], plaintext: &[u8]) -> Vec<u8> {
    let enc = Aes256EcbEnc::new(key.into());
    enc.encrypt_padded_vec_mut::<Pkcs7>(plaintext)
}

/// AES-256-ECB-PKCS7 decrypt one buffer of ciphertext.
pub fn aes_decrypt_pkcs7(key: &[u8; 32], ciphertext: &[u8]) -> Result<Vec<u8>, CryptoError> {
    let dec = Aes256EcbDec::new(key.into());
    dec.decrypt_padded_vec_mut::<Pkcs7>(ciphertext)
        .map_err(|e| CryptoError::AesUnpad(format!("{e:?}")))
}

/// Errors that can come out of [`encrypt_ecu_file`] / [`decrypt_ecu_file`].
#[derive(Debug, thiserror::Error)]
pub enum CryptoError {
    /// Base64 decoding of a chunk failed.
    #[error("base64 decode: {0}")]
    Base64(#[from] base64::DecodeError),
    /// AES decrypt / PKCS7 unpad failed (wrong password? corrupt file?).
    #[error("aes unpad: {0}")]
    AesUnpad(String),
    /// I/O error on the input file.
    #[error("io: {0}")]
    Io(#[from] std::io::Error),
}

/// Decrypt an entire `.ECU` / `.ACG` file (Base64 text) to raw bytes.
///
/// The wire format is:
/// - input is split into successive chunks of [`CHUNK_BASE64_CHARS`] chars
/// - each chunk is Base64-decoded and AES-256-ECB-PKCS7-decrypted
/// - the resulting plaintext chunks are concatenated
///
/// `password = None` falls back to the variant's default.
pub fn decrypt_ecu_file(
    content: &str,
    variant: EcuVariant,
    password: Option<&str>,
) -> Result<Vec<u8>, CryptoError> {
    let pwd = password.unwrap_or_else(|| variant.default_password());
    let key = variant.derive_key(pwd);

    let bytes = content.as_bytes();
    let mut out = Vec::with_capacity(bytes.len() * 3 / 4);
    let mut i = 0usize;
    while i < bytes.len() {
        let end = (i + CHUNK_BASE64_CHARS).min(bytes.len());
        // skip any whitespace (newlines/cr) at end
        let chunk = trim_b64(&bytes[i..end]);
        if chunk.is_empty() {
            break;
        }
        let ct = STANDARD.decode(chunk)?;
        let pt = aes_decrypt_pkcs7(&key, &ct)?;
        out.extend_from_slice(&pt);
        i = end;
    }
    Ok(out)
}

/// Encrypt a raw byte buffer to the `.ECU` / `.ACG` Base64 wire format.
pub fn encrypt_ecu_file(
    plaintext: &[u8],
    variant: EcuVariant,
    password: Option<&str>,
) -> String {
    let pwd = password.unwrap_or_else(|| variant.default_password());
    let key = variant.derive_key(pwd);

    let mut out = String::new();
    let mut i = 0usize;
    while i < plaintext.len() {
        let end = (i + CHUNK_PLAINTEXT_BYTES).min(plaintext.len());
        let ct = aes_encrypt_pkcs7(&key, &plaintext[i..end]);
        STANDARD.encode_string(&ct, &mut out);
        i = end;
    }
    // Edge case: zero-length input still needs one PKCS7 block of pad
    if plaintext.is_empty() {
        let ct = aes_encrypt_pkcs7(&key, b"");
        STANDARD.encode_string(&ct, &mut out);
    }
    out
}

fn trim_b64(s: &[u8]) -> &[u8] {
    let mut end = s.len();
    while end > 0 && matches!(s[end - 1], b'\n' | b'\r' | b'\t' | b' ') {
        end -= 1;
    }
    let mut start = 0;
    while start < end && matches!(s[start], b'\n' | b'\r' | b'\t' | b' ') {
        start += 1;
    }
    &s[start..end]
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn key_derivation_keihin_is_deterministic() {
        let k1 = derive_key_keihin(passwords::KEIHIN);
        let k2 = derive_key_keihin(passwords::KEIHIN);
        assert_eq!(k1, k2);
        // first 16 bytes are MD5 hash; last byte must be 0
        assert_eq!(k1[31], 0);
    }

    #[test]
    fn key_derivation_shinden_overlap() {
        // After the two overlapping copies, bytes 0..2 and 22..32 should
        // remain zero, while the middle section is hash data.
        let k = derive_key_shinden(passwords::SHINDEN);
        assert_eq!(&k[0..2], &[0, 0]);
        assert_eq!(&k[22..32], &[0; 10]);
    }

    #[test]
    fn round_trip_keihin_small() {
        let pt = b"Hello Honda ECU! 12345";
        let ct = encrypt_ecu_file(pt, EcuVariant::Keihin, None);
        let pt2 = decrypt_ecu_file(&ct, EcuVariant::Keihin, None).unwrap();
        assert_eq!(pt.as_slice(), pt2.as_slice());
    }

    #[test]
    fn round_trip_shinden_small() {
        let pt = b"\x00\x01\x02\x03\x04\xFF\xFE\xFD some bytes";
        let ct = encrypt_ecu_file(pt, EcuVariant::Shinden, None);
        let pt2 = decrypt_ecu_file(&ct, EcuVariant::Shinden, None).unwrap();
        assert_eq!(pt.as_slice(), pt2.as_slice());
    }

    #[test]
    fn round_trip_multi_chunk() {
        // > 1 chunk: 100k of pseudo-random bytes
        let mut pt = Vec::with_capacity(100_000);
        let mut x: u32 = 0x1234_5678;
        for _ in 0..100_000 {
            x = x.wrapping_mul(1_103_515_245).wrapping_add(12_345);
            pt.push((x >> 16) as u8);
        }
        let ct = encrypt_ecu_file(&pt, EcuVariant::Keihin, None);
        let pt2 = decrypt_ecu_file(&ct, EcuVariant::Keihin, None).unwrap();
        assert_eq!(pt, pt2);
    }

    #[test]
    fn wrong_password_fails() {
        let pt = b"secret data";
        let ct = encrypt_ecu_file(pt, EcuVariant::Keihin, Some("right-pwd"));
        let res = decrypt_ecu_file(&ct, EcuVariant::Keihin, Some("wrong-pwd"));
        assert!(res.is_err(), "decryption with wrong password should fail");
    }
}
