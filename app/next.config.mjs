/** @type {import('next').NextConfig} */
const nextConfig = {
  // Tauri ships the app as a static bundle - no Node server is shipped to
  // end-users, so we tell Next to do a fully-static export.
  output: "export",
  images: { unoptimized: true },
  // Tauri serves the app from a custom protocol (`tauri://localhost`); a
  // trailing slash keeps relative-asset URLs predictable.
  trailingSlash: true,
  reactStrictMode: true,
};

export default nextConfig;
