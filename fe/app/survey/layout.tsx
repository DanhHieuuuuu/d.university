'use client';

import React, { useEffect } from 'react';
import { Layout } from 'antd';
import { useRouter, usePathname } from 'next/navigation';
import { useAppSelector } from '@redux/hooks';
import SurveyMenuComponent from '@components/menu/menu-survey';

const { Content } = Layout;

export default function SurveyLayout({ children }: { children: React.ReactNode }) {
  const router = useRouter();
  const pathname = usePathname();
  const { isAuthenticated } = useAppSelector((state) => state.studentState);

  useEffect(() => {
    // Skip auth check for login page
    if (pathname === '/survey/user/login') {
      return;
    }

    // Redirect to login if not authenticated (for mysurvey and other protected pages)
    if (!isAuthenticated) {
      router.push('/survey/user/login');
    }
  }, [isAuthenticated, pathname, router]);

  return (
    <Layout hasSider={pathname !== '/survey/user/login'} style={{ height: '100vh' }}>
      {pathname !== '/survey/user/login' && <SurveyMenuComponent />}
      <Layout style={{ background: '#F5F5F5' }}>
        <Content style={{ height: '100vh', padding: 16, background: '#F5F5F5', overflowY: 'auto' }}>
          {children}
        </Content>
      </Layout>
    </Layout>
  );
}
