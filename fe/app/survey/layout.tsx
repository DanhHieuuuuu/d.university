'use client';

import React, { useEffect } from 'react';
import { Layout } from 'antd';
import { useRouter, usePathname } from 'next/navigation';
import { useAppSelector } from '@redux/hooks';
import SurveyMenuComponent from '@components/menu/menu-survey';
import SurveyHeader from '@components/dhieu-custom/header';
import NotificationRealtime from '../(home)/notification/real_time/NotificationRealtime';

const { Content } = Layout;

export default function SurveyLayout({ children }: { children: React.ReactNode }) {
  const router = useRouter();
  const pathname = usePathname();

  // Check authentication for both staff and students
  const { isAuthenticated: isStaffAuth } = useAppSelector((state) => state.authState);
  const { isAuthenticated: isStudentAuth } = useAppSelector((state) => state.studentState);
  const isAuthenticated = isStaffAuth || isStudentAuth;

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
      {pathname !== '/survey/user/login' && <NotificationRealtime />}
      {pathname !== '/survey/user/login' && <SurveyMenuComponent />}
      <Layout style={{ background: '#F5F5F5' }}>
        {pathname !== '/survey/user/login' && <SurveyHeader />}
        <Content
          style={{
            height: pathname !== '/survey/user/login' ? 'calc(100vh - 64px)' : '100vh',
            padding: 16,
            background: '#F5F5F5',
            overflowY: 'auto'
          }}
        >
          {children}
        </Content>
      </Layout>
    </Layout>
  );
}
