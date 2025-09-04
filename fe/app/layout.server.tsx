import { Metadata } from 'next';
import localFont from 'next/font/local';

const geistSans = localFont({
  src: '../public/fonts/GeistVF.woff',
  variable: '--font-geist-sans',
  weight: '100 900'
});

const geistMono = localFont({
  src: '../public/fonts/GeistMonoVF.woff',
  variable: '--font-geist-mono',
  weight: '100 900'
});

export const metadata: Metadata = {
  title: 'Digital University',
  description: 'Ứng dụng quản trị đại học số'
};
