/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true, // Recommended for the `pages` directory, default in `app`.
  sassOptions: {},
  images: {
    remotePatterns: [
      {
        protocol: 'https',
        hostname: '**',
        port: '',
        pathname: '**',
      },
    ],
  },
  env: {},

  // 🔥 Disable ESLint when build (Vercel sẽ không fail vì ESLint nữa)
  eslint: {
    ignoreDuringBuilds: true,
  },
};

export default nextConfig;
