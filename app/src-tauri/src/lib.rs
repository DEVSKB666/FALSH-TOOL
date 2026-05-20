//! Tauri 2 application entry-point. The actual command handlers live in
//! [`commands`]; the `run()` fn just wires plugins + invoke handler.

mod commands;
mod ftdi;
mod transport;
mod eeprom;
mod libusb_ftdi;
mod kline_log;
mod livedata;
mod livedata_session;
mod bridge_client;

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()
        .plugin(tauri_plugin_dialog::init())
        .plugin(tauri_plugin_fs::init())
        .plugin(tauri_plugin_store::Builder::default().build())
        .manage(livedata_session::LiveSessionState::default())
        .invoke_handler(tauri::generate_handler![
            commands::app_info,
            commands::list_ftdi,
            commands::list_ecus,
            commands::decrypt_ecu_file,
            commands::encrypt_ecu_file,
            commands::read_eeprom,
            commands::kline_test,
            commands::reset_flash_count,
            commands::format_eeprom,
            commands::dump_rom,
            commands::break_xdf_adx,
            commands::add_ecu_entry,
            commands::update_ecu_entry,
            commands::delete_ecu_entry,
            commands::read_live_sample,
            commands::read_ecm_id,
            livedata_session::livedata_start,
            livedata_session::livedata_poll,
            livedata_session::livedata_stop,
            commands::bridge_ping,
            commands::list_ftdi_via_bridge,
            commands::read_eeprom_via_bridge,
            commands::read_live_sample_via_bridge,
            commands::dump_rom_via_bridge,
            commands::read_ecm_id_via_bridge,
            commands::clear_dtc,
            commands::clear_dtc_via_bridge,
        ])
        .setup(|_app| {
            #[cfg(debug_assertions)]
            {
                eprintln!("[mza-tuner] dev build");
            }
            Ok(())
        })
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
