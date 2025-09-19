'use client';

import { ToastContainer } from 'react-toastify';
import { ThemeProvider, useTheme } from 'next-themes';
import { AntdRegistry } from '@ant-design/nextjs-registry';
import { ConfigProvider, theme as antdTheme } from 'antd';
import StoreProvider from './StoreProvider';

import GlobalLoading from '@components/common/Loading';
import '@src/styles/globals.scss';

export default function RootLayout({
  children
}: Readonly<{
  children: React.ReactNode;
}>) {
  const { theme } = useTheme();

  return (
    <html lang="en" suppressHydrationWarning>
      <head>
        <meta charSet="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <title>Digital University</title>
      </head>
      <body>
        <ThemeProvider attribute="class" defaultTheme="light" enableSystem>
          <AntdRegistry>
            <StoreProvider>
              <ConfigProvider
                theme={{
                  algorithm: theme === 'dark' ? antdTheme.darkAlgorithm : antdTheme.defaultAlgorithm,
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
                <GlobalLoading />
              </ConfigProvider>
              <ToastContainer autoClose={2000} position="top-right" />
            </StoreProvider>
          </AntdRegistry>
        </ThemeProvider>
      </body>
    </html>
  );
}
