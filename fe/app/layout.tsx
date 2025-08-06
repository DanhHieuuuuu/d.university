import { ConfigProvider } from 'antd';
import localFont from 'next/font/local';
import { ToastContainer } from 'react-toastify';
import { AntdRegistry } from '@ant-design/nextjs-registry';

import StoreProvider from './StoreProvider';
import '@src/styles/globals.scss';

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

export default function RootLayout({
  children
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`${geistSans.variable} ${geistMono.variable} antialiased`}>
        <AntdRegistry>
          <StoreProvider>
            <ConfigProvider
              theme={{
                token: {
                  colorPrimary: '#0095e8'
                },
                components: {
                  Menu: {
                    itemColor: '#5e6278',
                    itemActiveBg: '#f4f6fa',
                    itemSelectedBg: '#f4f6fa',
                    colorBgTextActive: 'rgb(244, 246, 250)',
                    itemHoverBg: 'white',
                    itemHoverColor: '#000'
                  },
                  Input: {
                    colorTextDisabled: 'rgba(0,0,0,0.88)'
                  },
                  InputNumber: {
                    colorTextDisabled: 'rgba(0,0,0,0.88)'
                  },
                  Select: {
                    colorTextDisabled: 'rgba(0,0,0,0.88)'
                  },
                  Checkbox: {
                    colorTextDisabled: 'rgba(0,0,0,0.88)'
                  }
                }
              }}
            >
              {children}
            </ConfigProvider>
            <ToastContainer autoClose={2000} position="top-right" />
          </StoreProvider>
        </AntdRegistry>
      </body>
    </html>
  );
}
