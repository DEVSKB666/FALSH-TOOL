//! `loy-bridge` - TCP/JSON daemon that forwards K-Line operations from
//! a remote client (the LOY-TUNER GUI) to the FTDI hardware sitting on
//! this machine.
//!
//! Run with `--help` for the full CLI. In its simplest form:
//!
//! ```text
//! loy-bridge --bind 0.0.0.0:7878
//! ```
//!
//! When compiled with `--features mock` (instead of the default
//! `hardware` feature) the binary returns canned data and never
//! touches the FTDI driver - useful for testing the wire protocol on
//! a CI box.

mod eeprom;
#[cfg(all(feature = "hardware", not(feature = "mock")))]
mod libusb_ftdi;
mod livedata;
mod proto;
mod server;
mod transport;

use clap::Parser;
use tracing_subscriber::{fmt, EnvFilter};

#[derive(Parser, Debug)]
#[command(
    name = "loy-bridge",
    version,
    about = "TCP bridge daemon for LOY-TUNER (FTDI K-Line forwarder)",
    long_about = None,
)]
struct Cli {
    /// Address:port to listen on. Defaults to all interfaces, port 7878.
    #[arg(long, default_value = "0.0.0.0:7878")]
    bind: String,

    /// Increase log verbosity (DEBUG instead of INFO).
    #[arg(short, long)]
    verbose: bool,
}

fn main() -> anyhow::Result<()> {
    let cli = Cli::parse();

    let default_level = if cli.verbose { "loy_bridge=debug" } else { "loy_bridge=info" };
    fmt()
        .with_env_filter(
            EnvFilter::try_from_default_env()
                .unwrap_or_else(|_| EnvFilter::new(default_level)),
        )
        .with_target(false)
        .compact()
        .init();

    if cfg!(feature = "mock") {
        tracing::warn!("running in MOCK mode - no real FTDI hardware will be used");
    }

    tracing::info!(version = env!("CARGO_PKG_VERSION"), "loy-bridge starting");
    server::run(&cli.bind)
}
