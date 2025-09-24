'use client'

import React from 'react';
import { Layout } from 'antd';

import MenuComponent from '@components/menu/menu-home';
import HeaderComponent from '@components/common/Header';

const { Content, Footer } = Layout;

const Index = ({ children }: { children: React.ReactNode }) => {
  return (
    <Layout hasSider style={{ minHeight: '100vh' }}>
      <MenuComponent />
      <Layout>
        <HeaderComponent />
        <Content style={{ margin: 16, height: '100%' }}>{children}</Content>
        <Footer style={{ textAlign: 'center' }}>Ant Design ©{new Date().getFullYear()} Created by Ant UED</Footer>
      </Layout>
    </Layout>
  );
};

export default Index;
