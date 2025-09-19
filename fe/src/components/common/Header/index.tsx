'use client';

import { Layout } from 'antd';
import '@src/styles/globals.scss';

const { Header: HeaderAntd } = Layout;

const HeaderComponent = () => {
  return (
    <HeaderAntd
      style={{
        background: 'var(--background-header)',
        color: 'var(--foreground)'
      }}
    >
      Header
    </HeaderAntd>
  );
};

export default HeaderComponent;
