"use client";

/**
 * Thin wrapper around Tauri's `invoke` so the rest of the codebase doesn't
 * have to import `@tauri-apps/api/core` directly. When the page is rendered
 * outside Tauri (e.g. `npm run dev` in a browser tab) every command falls
 * back to a deterministic mock so the UI is still usable.
 */

type Invoke = <T>(cmd: string, args?: Record<string, unknown>) => Promise<T>;

/** True when running inside the Tauri webview, false in plain browsers. */
export const isTauri =
  typeof window !== "undefined" &&
  // Tauri 2 exposes a marker on window
  (("__TAURI_INTERNALS__" in window) || ("__TAURI__" in window));

let realInvoke: Invoke | null = null;
async function getInvoke(): Promise<Invoke> {
  if (realInvoke) return realInvoke;
  if (!isTauri) {
    realInvoke = (mockInvoke as unknown) as Invoke;
    return realInvoke;
  }
  // Lazy-import so the static export doesn't bundle Tauri code paths into
  // the browser fallback.
  const mod = await import("@tauri-apps/api/core");
  realInvoke = mod.invoke as unknown as Invoke;
  return realInvoke;
}

// ---------------------- Typed command surface ------------------------

export interface AppInfo {
  version: string;
  rust_version: string;
  tauri_version: string;
}

/** Backends a port can be opened through. `"bridge"` is a synthetic
 *  tag used when the device lives on a remote `loy-bridge` daemon -
 *  the local Tauri commands won't accept it; instead the call must be
 *  routed via the `*_via_bridge` family. */
export type PortBackend = "d2xx" | "libusb" | "bridge";

export interface FtdiDevice {
  index: number;
  serial: string;
  description: string;
  /** Which Rust transport will open this device. */
  backend: PortBackend;
  /** **Bridge ports only** - the daemon-side transport (`d2xx` /
   *  `libusb`) that owns the physical FTDI on the remote machine.
   *  Echo it back to `readEepromViaBridge()` so the daemon opens the
   *  cable through the matching driver. `undefined` for local
   *  `d2xx` / `libusb` entries. */
  daemon_backend?: "d2xx" | "libusb";
}

export interface EcuEntry {
  id: string;
  family: "Keihin" | "Shinden";
  part_code: string;
  ecm_id: string;
  start_offset: number;
  cksum_offset: number;
}

export interface EepromReadResult {
  family: "Keihin" | "Shinden";
  bytes: number[];          // EEPROM bytes (typically 256 or 512)
  ecm_id: string | null;    // detected ECM-id if available
  duration_ms: number;
  log: string[];
}

export interface KlineTestResult {
  backend: "d2xx" | "libusb";
  index: number;
  duration_ms: number;
  log: string[];
}

export interface OperationResult {
  family: string;
  label: string;
  ok: boolean;
  bytes: number[] | null;
  duration_ms: number;
  log: string[];
}

export interface XdfBreakResult {
  variant: "XDF" | "ADX";
  open_password: string;
  modify_password: string;
  plaintext: string;
}

/** Payload of the `kline-progress` Tauri event - emitted by long-running ops. */
export interface KlineProgress {
  label: string;
  current: number;
  total: number;
  percent: number;
  ts: number;
}

/** Payload of the `kline-log` Tauri event - emitted on every TX/RX. */
export interface KlineLogLine {
  /** "tx" cable->ECU, "rx" ECU->cable, "info" status, "err" error. */
  dir: "tx" | "rx" | "info" | "err";
  /** Hex string with single-space separators e.g. "91 91 0D DF". */
  hex: string;
  /** Number of bytes in this frame. */
  len: number;
  /** Optional human-readable note (used for "info"/"err"). */
  msg: string | null;
  /** UNIX-ms timestamp at the time of emission. */
  ts: number;
}

export const tauri = {
  /** Get app + runtime version info. */
  async appInfo() {
    return (await getInvoke())<AppInfo>("app_info");
  },

  /** List FTDI devices currently attached (local USB). */
  async listFtdi() {
    return (await getInvoke())<FtdiDevice[]>("list_ftdi");
  },

  /** List FTDI devices visible to a remote `loy-bridge` daemon. */
  async listFtdiViaBridge(url: string) {
    return (await getInvoke())<FtdiDevice[]>("list_ftdi_via_bridge", { url });
  },

  /** Liveness check for a remote bridge. Returns true on a successful
   *  ping within 5 seconds. */
  async bridgePing(url: string) {
    return (await getInvoke())<boolean>("bridge_ping", { url });
  },

  /** Read every entry in `data.ini`. */
  async listEcus(path?: string) {
    return (await getInvoke())<EcuEntry[]>("list_ecus", { path });
  },

  /** Decrypt a `.ECU`/`.ACG` file to raw bytes. */
  async decryptEcuFile(input: string, output: string, variant: "Keihin" | "Shinden", password?: string) {
    return (await getInvoke())<number>("decrypt_ecu_file", {
      input,
      output,
      variant,
      password,
    });
  },

  /** Encrypt a raw .BIN to a `.ECU` file. */
  async encryptEcuFile(input: string, output: string, variant: "Keihin" | "Shinden", password?: string) {
    return (await getInvoke())<number>("encrypt_ecu_file", {
      input,
      output,
      variant,
      password,
    });
  },

  /** Run a full Keihin/Shinden EEPROM read against the ECU (local USB). */
  async readEeprom(
    variant: "Keihin" | "Shinden",
    deviceIndex = 0,
    backend: "d2xx" | "libusb" = "d2xx",
  ) {
    return (await getInvoke())<EepromReadResult>("read_eeprom", {
      variant,
      deviceIndex,
      backend,
    });
  },

  /** EEPROM read forwarded through a remote `loy-bridge` daemon. Same
   *  return shape as the local one. `daemonBackend` selects which
   *  transport the daemon uses to open the cable - pass through the
   *  `FtdiDevice.daemon_backend` of the bridge port the user picked. */
  async readEepromViaBridge(
    url: string,
    variant: "Keihin" | "Shinden",
    deviceIndex = 0,
    daemonBackend: "d2xx" | "libusb" = "d2xx",
  ) {
    return (await getInvoke())<EepromReadResult>("read_eeprom_via_bridge", {
      url,
      variant,
      deviceIndex,
      daemonBackend,
    });
  },

  /** Open a port, run the K-Line handshake and send a Honda discovery
   *  probe so the cable's TX LED blinks. Streams `kline-log` events. */
  async klineTest(
    deviceIndex = 0,
    backend: "d2xx" | "libusb" = "d2xx",
    probe = true,
  ) {
    return (await getInvoke())<KlineTestResult>("kline_test", {
      deviceIndex,
      backend,
      probe,
    });
  },

  /** Reset the Honda flash counter (Keihin or Shinden). */
  async resetFlashCount(
    variant: "Keihin" | "Shinden",
    deviceIndex = 0,
    backend: "d2xx" | "libusb" = "d2xx",
  ) {
    return (await getInvoke())<OperationResult>("reset_flash_count", {
      variant,
      deviceIndex,
      backend,
    });
  },

  /** **DESTRUCTIVE** - format the Shinden EEPROM with a fixed byte. */
  async formatEeprom(
    fill: 0x00 | 0xFF,
    deviceIndex = 0,
    backend: "d2xx" | "libusb" = "d2xx",
  ) {
    return (await getInvoke())<OperationResult>("format_eeprom", {
      fill,
      deviceIndex,
      backend,
    });
  },

  /** Dump the Shinden ROM. `size` = "48K" (upper half) or "64K" (upper quarter). */
  async dumpRom(
    size: "48K" | "64K",
    deviceIndex = 0,
    backend: "d2xx" | "libusb" = "d2xx",
  ) {
    return (await getInvoke())<OperationResult>("dump_rom", {
      size,
      deviceIndex,
      backend,
    });
  },

  /** XDF / ADX password breaker (port of Form4 "RED_MATRIX"). Pass the
   *  raw file bytes, original filename (used to detect XDF vs ADX) and
   *  optionally a custom password. Returns the cleaned plaintext + the
   *  recovered passwords. */
  async breakXdfAdx(fileBytes: Uint8Array, filename: string, password?: string) {
    return (await getInvoke())<XdfBreakResult>("break_xdf_adx", {
      fileBytes: Array.from(fileBytes),
      filename,
      password,
    });
  },

  /** Append a new ECU entry to `data.ini`. Returns the auto-allocated id. */
  async addEcuEntry(
    family: "Keihin" | "Shinden",
    partCode: string,
    ecmId: string,
    startOffset: number,
    cksumOffset: number,
    path?: string,
  ) {
    return (await getInvoke())<string>("add_ecu_entry", {
      family,
      partCode,
      ecmId,
      startOffset,
      cksumOffset,
      path,
    });
  },

  /** Update an existing ECU entry by id. Returns true if it existed. */
  async updateEcuEntry(
    family: "Keihin" | "Shinden",
    id: string,
    partCode: string,
    ecmId: string,
    startOffset: number,
    cksumOffset: number,
    path?: string,
  ) {
    return (await getInvoke())<boolean>("update_ecu_entry", {
      family,
      id,
      partCode,
      ecmId,
      startOffset,
      cksumOffset,
      path,
    });
  },

  /** Delete an ECU entry by id. Returns true if it existed. */
  async deleteEcuEntry(family: "Keihin" | "Shinden", id: string, path?: string) {
    return (await getInvoke())<boolean>("delete_ecu_entry", {
      family,
      id,
      path,
    });
  },

  /** Read one live-data sample from the ECU (TyN Shop K-Line protocol).
   *  Returns the raw 29-byte TABLE_16 + 13-byte TABLE_20 responses,
   *  which the frontend parses with the ADX equations. */
  async readLiveSample(deviceIndex = 0, backend: "d2xx" | "libusb" = "d2xx") {
    return (await getInvoke())<{
      table16: number[];
      table20: number[];
      duration_ms: number;
    }>("read_live_sample", {
      deviceIndex,
      backend,
    });
  },
};

// ---------------------- Browser-mode mock ----------------------------

async function mockInvoke<T>(cmd: string, args?: Record<string, unknown>): Promise<T> {
  // Dummy responses - just enough for the UI to render in dev mode.
  switch (cmd) {
    case "app_info":
      return {
        version: "0.1.0-mock",
        rust_version: "rustc (mock)",
        tauri_version: "2.x (mock)",
      } as unknown as T;
    case "list_ftdi": {
      // Browser dev mode: fake one FTDI device so the connection flow can
      // be exercised. Real FTDI detection only works inside Tauri runtime
      // (which calls libftd2xx via the Rust backend).
      const enable = (typeof window !== "undefined") &&
        (localStorage.getItem("mza-tuner.mockFtdi") !== "off");
      if (!enable) return [] as unknown as T;
      return [
        {
          index: 0,
          serial: "MOCK-FT-001",
          description: "Mock FTDI USB-Serial (Browser dev)",
          backend: "d2xx",
        },
      ] as unknown as T;
    }
    case "list_ecus":
      return [] as unknown as T;
    case "read_eeprom": {
      const family = (args?.variant as string) ?? "Keihin";
      const bytes = Array.from({ length: 512 }, (_, i) => i & 0xff);
      return {
        family,
        bytes,
        ecm_id: family === "Keihin" ? "0104080F01" : "0103D00D01",
        duration_ms: 1200,
        log: [
          "[mock] running browser mock - launch via `npm run tauri:dev` for real ECU I/O",
          `[mock] simulated ${family} EEPROM read complete`,
        ],
      } as unknown as T;
    }
    case "decrypt_ecu_file":
    case "encrypt_ecu_file":
      return 0 as unknown as T;
    case "kline_test":
      return {
        backend: (args?.backend as string) ?? "d2xx",
        index: (args?.deviceIndex as number) ?? 0,
        duration_ms: 220,
        log: [
          "[mock] cable test - browser dev mode, no real hardware",
          "[mock] handshake skipped",
        ],
      } as unknown as T;
    case "reset_flash_count":
      return {
        family: (args?.variant as string) ?? "Keihin",
        label: `Reset Flash Count (${args?.variant ?? "Keihin"})`,
        ok: true,
        bytes: null,
        duration_ms: 1500,
        log: ["[mock] reset flash count - browser dev mode"],
      } as unknown as T;
    case "format_eeprom": {
      const fill = (args?.fill as number) ?? 0xFF;
      return {
        family: "Shinden",
        label: `Format EEPROM 0x${fill.toString(16).toUpperCase().padStart(2, "0")}`,
        ok: true,
        bytes: null,
        duration_ms: 1200,
        log: ["[mock] format eeprom - browser dev mode"],
      } as unknown as T;
    }
    case "dump_rom": {
      const size = (args?.size as string) ?? "48K";
      const totalBytes = size === "64K" ? 32768 : 49152;
      return {
        family: "Shinden",
        label: `ROM Dump ${size}`,
        ok: true,
        bytes: Array.from({ length: totalBytes }, (_, i) => (i ^ 0x55) & 0xff),
        duration_ms: 8000,
        log: [`[mock] ROM dump ${size} - returning fake ${totalBytes} bytes`],
      } as unknown as T;
    }
    case "break_xdf_adx":
      return {
        variant: ((args?.filename as string) ?? "x.xdf").toLowerCase().endsWith(".adx") ? "ADX" : "XDF",
        open_password:   "MockOpenPwd123",
        modify_password: "MockModifyPwd456",
        plaintext: '<XDFFORMAT>\r\n  <XDFHEADER>\r\n    <flags>0x1</flags>\r\n    [mock] cleaned in browser preview\r\n  </XDFHEADER>\r\n</XDFFORMAT>\r\n',
      } as unknown as T;
    case "add_ecu_entry":
      return ("ID9999") as unknown as T;
    case "update_ecu_entry":
    case "delete_ecu_entry":
      return true as unknown as T;
    case "read_live_sample":
      return {
        table16: [],
        table20: [],
        duration_ms: 0,
      } as unknown as T;
    case "bridge_ping":
      return false as unknown as T;
    case "list_ftdi_via_bridge":
      return [] as unknown as T;
    case "read_eeprom_via_bridge":
      return {
        family: (args?.variant as string) ?? "Keihin",
        bytes: Array.from({ length: 512 }, (_, i) => i & 0xff),
        ecm_id: null,
        duration_ms: 1,
        log: ["[mock] read_eeprom_via_bridge - browser preview"],
      } as unknown as T;
    default:
      throw new Error(`mockInvoke: unknown command ${cmd}`);
  }
}
