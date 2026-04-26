# loy-bridge

TCP/JSON bridge daemon for **LOY-TUNER**. Runs on the notebook that has
the FTDI cable plugged in and forwards K-Line operations from a remote
client (typically the main LOY-TUNER GUI on another PC on the same
WiFi).

```
┌─────────────┐  TCP/JSON  ┌──────────────────────┐  K-Line  ┌──────┐
│ LOY-TUNER   │ ─────────▶ │  loy-bridge.exe      │ ───────▶ │ ECU  │
│ (other PC)  │   :7878    │  (notebook + FTDI)   │ 10400 bd │ Honda│
└─────────────┘            └──────────────────────┘          └──────┘
```

## Build

```cmd
:: Real hardware (default)
cargo build -p loy-bridge --release

:: Mock mode - no FTDI driver required, returns canned bytes
cargo build -p loy-bridge --release --no-default-features --features mock
```

The binary lands at `target\release\loy-bridge.exe`.

## Run

```cmd
loy-bridge --bind 0.0.0.0:7878
loy-bridge --bind 192.168.1.50:7878 --verbose   # bind to a specific NIC
```

CLI flags:

| Flag         | Default            | Notes                              |
| ------------ | ------------------ | ---------------------------------- |
| `--bind`     | `0.0.0.0:7878`     | Listen address (host:port)         |
| `--verbose`  | `false`            | DEBUG-level logs                   |

`RUST_LOG=loy_bridge=trace` overrides the level if you need finer
control.

## Wire protocol

Newline-delimited JSON, request → response, one line each. Roughly
inspired by JSON-RPC 2.0 but trimmed down.

### Request envelope

```json
{ "id": 1, "method": "list_ports", "params": null }
```

### Response envelope

```json
{ "id": 1, "result": [...] }                            // success
{ "id": 2, "error": "no FTDI device at index 0" }       // failure
```

### Methods

#### `ping`

Sanity check. No params.

```json
→ {"id": 1, "method": "ping"}
← {"id": 1, "result": {"pong": true}}
```

#### `list_ports`

List FTDI devices visible to the daemon. No params.

```json
→ {"id": 2, "method": "list_ports"}
← {"id": 2, "result": [
     {"index": 0, "serial": "FTABCDEF", "description": "FT232R USB UART"}
   ]}
```

#### `read_eeprom`

Read 256 EEPROM cells (= 512 bytes) from a Honda Keihin or Shinden
ECU. Takes ~25-30 seconds against a real ECU.

```json
→ {"id": 3,
   "method": "read_eeprom",
   "params": {"variant": "keihin", "device_index": 0}}
← {"id": 3, "result": {
     "family": "Keihin",
     "bytes": [0x41, 0x42, ...],
     "duration_ms": 26412,
     "log": ["[d2xx] opened device #0", "[keihin] init sequence", ...]
   }}
```

`variant` is `"keihin"` or `"shinden"` (lowercase).
`device_index` defaults to `0` if omitted.

## Quick test

In one terminal:

```cmd
loy-bridge --features mock --bind 127.0.0.1:7878 --verbose
```

In another:

```cmd
:: Windows: ncat from nmap, or your favourite TCP client
echo {"id":1,"method":"ping"} | ncat 127.0.0.1 7878
echo {"id":2,"method":"list_ports"} | ncat 127.0.0.1 7878
```

You should see one JSON object per line of output. Two requests
on the same connection are fine - the server keeps it open.

## Security

The daemon does **no** authentication. Bind only to a trusted LAN
interface (or `127.0.0.1` for local-only access) and put the host
behind a firewall.

## Reference

K-Line frame definitions and timing live in:
- `app/src-tauri/src/eeprom.rs` (the original Tauri-coupled port)
- `bridge/src/eeprom.rs`        (this crate's stripped-down copy)

Both ultimately come from the deobfuscated `Form_EepromTool.cs` in the
upstream `MZA_TUNER_FLASH_2026` source.
