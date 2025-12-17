'use client';

import React from 'react';
import { Layout } from 'antd';
import RoleMenuComponent from '@components/menu/menu-manager';
import AppHeader from '@components/common/Header';

const { Content } = Layout;

const RoleLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <Layout style={{ height: '100vh' }}>
      <AppHeader />
      <Layout hasSider className="h-full">
        <RoleMenuComponent />
        <Content
          style={{
            background: '#F5F5F5',
            color: 'var(--foreground)',
            padding: 16,
            height: 'calc(100vh - 64px)',
            overflowY: 'auto'
          }}
        >
          {children}
        </Content>
      </Layout>
    </Layout>
  );
};

export default RoleLayout;
