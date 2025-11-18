'use client';

import React from 'react';
import { Layout } from 'antd';
import StudentMenuComponent from '@components/menu/menu-student';
import AppHeader from '@components/common/Header';

const { Content } = Layout;

const StudentLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <AppHeader />
      <Layout hasSider className="h-full">
        <StudentMenuComponent />
        <Content
          style={{
            background: '#F5F5F5',
            color: 'var(--foreground)',
            padding: 16
          }}
        >
          {children}
        </Content>
      </Layout>
    </Layout>
  );
};

export default StudentLayout;
