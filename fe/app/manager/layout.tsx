'use client';

import React from 'react';
import { Layout } from 'antd';
import RoleMenuComponent from '@components/menu/menu-manager';
import AppHeader from '@components/common/Header';

const { Content } = Layout;

const RoleLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <AppHeader />
      <Layout hasSider className="h-full">
        <RoleMenuComponent />
        <Content
          style={{
            background: 'var(--background)',
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

export default RoleLayout;
