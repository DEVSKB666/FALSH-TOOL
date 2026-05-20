/**
 * Honda BIN checksum helpers.
 *
 * Port of `MZA_TUNER_FLASH_2026/ns1/GForm12.cs::method_28` (and its
 * underlying byte-sum primitive `method_17`). The convention is the
 * same as every Honda KWP frame on the wire:
 *
 *   `(sum of every byte in the buffer) mod 256 == 0`
 *
 * One byte in the file is therefore reserved as the checksum slot
 * (`offset`). The fixer recomputes that one byte so the whole file
 * sums to zero again.
 *
 * The C# original supports two layouts:
 *
 * - **Tail checksum** (`offset == 0` in C#): the *last* byte holds
 *   the checksum, all preceding bytes are payload.
 * - **In-place checksum** (`offset > 0`): the byte at `offset` is the
 *   checksum, payload is `[0..offset)` plus `(offset..end]`. This
 *   matches the Shinden Honda layout where the checksum lives at a
 *   fixed offset (e.g. `0x7FF` for 32K maps).
 *
 * Both layouts produce a buffer that sums to 0 mod 256. The
 * `verifyChecksum` helper reports whether a freshly loaded file is
 * already balanced - if so, no fix is needed.
 */

/** Sum of `len` bytes starting at `start`, returned mod 256. */
function byteSum(buf: Uint8Array, len: number, start = 0): number {
  let s = 0;
  for (let i = start; i < start + len; i++) s = (s + buf[i]) & 0xff;
  return s & 0xff;
}

/** Whole-buffer sum mod 256. Convenience wrapper. */
export function sumMod256(buf: Uint8Array): number {
  return byteSum(buf, buf.length, 0);
}

/**
 * Returns `true` if `buf` already sums to 0 mod 256, regardless of
 * where the checksum byte happens to live. Cheaper than computing
 * the fix value when callers only want to display a status badge.
 */
export function verifyChecksum(buf: Uint8Array): boolean {
  return sumMod256(buf) === 0;
}

/**
 * Recompute the single checksum byte at `offset` so the whole buffer
 * sums to zero. `offset === 0` means "tail checksum" (legacy C#
 * default). Returns a *new* Uint8Array - the input is not mutated,
 * which is convenient for React state and undo histories.
 *
 * Mirrors `method_28` exactly:
 *
 *     // C# (paraphrased)
 *     b = (offset != 0)
 *           ? sum(buf, offset, 0) + sum(buf, len - (offset+1), offset+1)
 *           : sum(buf, len - 1, 1);
 *     buf[offset] = -b;
 */
export function fixChecksum(buf: Uint8Array, offset = 0): Uint8Array {
  const out = new Uint8Array(buf);
  const len = out.length;
  if (len === 0) return out;

  let b: number;
  if (offset === 0) {
    // Last-byte layout: sum bytes [1..len-1], store -sum at [0].
    // (This is what the C# original does when called with int_8 = 0;
    //  the first byte of `byte_12` becomes the checksum slot.)
    b = byteSum(out, len - 1, 1);
  } else {
    if (offset < 0 || offset >= len) {
      throw new RangeError(
        `checksum offset ${offset} is out of range for ${len}-byte buffer`,
      );
    }
    b = (byteSum(out, offset, 0) + byteSum(out, len - (offset + 1), offset + 1)) & 0xff;
  }
  out[offset] = (256 - b) & 0xff;
  return out;
}

/**
 * Heuristic: scan a freshly loaded BIN and guess the most likely
 * checksum offset. Returns `null` if the buffer is empty.
 *
 * Strategy: try the canonical Honda offsets (0, last byte, 0x7FF for
 * 32K-class maps, 0x3FFF for 16K). Whichever already balances the
 * buffer wins - if none match, fall back to the C# default (0).
 */
export function guessChecksumOffset(buf: Uint8Array): number | null {
  if (buf.length === 0) return null;
  const candidates = [0, buf.length - 1, 0x7ff, 0x3fff].filter(
    (o) => o >= 0 && o < buf.length,
  );
  for (const o of candidates) {
    // Test: zero-out candidate, sum the rest, and check whether
    // -sum (mod 256) equals the byte that was originally there.
    const rest = (sumMod256(buf) - buf[o]) & 0xff;
    if (((256 - rest) & 0xff) === buf[o]) return o;
  }
  return 0;
}
