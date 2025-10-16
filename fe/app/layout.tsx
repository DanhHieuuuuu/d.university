'use client';

import { ToastContainer } from 'react-toastify';
import { ThemeProvider, useTheme } from 'next-themes';
import { AntdRegistry } from '@ant-design/nextjs-registry';
import { ConfigProvider, theme as antdTheme } from 'antd';
import StoreProvider from './StoreProvider';

import GlobalLoading from '@components/common/Loading';
import { colors } from '@styles/colors';
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
                    colorPrimary: colors.primary
                  },
                  components: {
                    Menu: {
                      itemColor: colors.black,
                      itemHoverColor: colors.white,
                      itemHoverBg: colors.primaryLight,
                      itemActiveBg: colors.primaryNavy,
                      itemSelectedColor: colors.white,
                      itemSelectedBg: colors.primaryNavy
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
                    },
                    Breadcrumb: {
                      linkColor: '#0045DC',
                      linkHoverColor: '#0045DC'
                    },
                    Table: {
                      cellPaddingInlineSM: 16,
                      headerBg: '#ebecf0'
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
