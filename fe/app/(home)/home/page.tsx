'use client';

import React from 'react';
import { Breadcrumb, theme } from 'antd';

import { useAppSelector } from '@redux/hooks';

const HomePage: React.FC = () => {
  const {
    token: { colorBgContainer, borderRadiusLG }
  } = theme.useToken();
  const { user } = useAppSelector((state) => state.authState);

  return (
    <div>
      <Breadcrumb style={{ margin: '16px 0' }} items={[{ title: 'User' }, { title: 'Bill' }]} />
      <div
        style={{
          padding: 24,
          minHeight: 360,
          background: colorBgContainer,
          borderRadius: borderRadiusLG
        }}
      >
        Xin chào {user?.ten || 'Bill is a cat.'}
      </div>
    </div>
  );
};

export default HomePage;
