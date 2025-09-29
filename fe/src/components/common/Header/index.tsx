'use client';

import { Layout, Typography } from 'antd';
import { useNavigate } from '@hooks/navigate';
import '@src/styles/globals.scss';

const { Header } = Layout;
const { Title } = Typography;

const AppHeader = () => {
  const { navigateTo } = useNavigate();
  return (
    <Header
      style={{
        background: 'var(--background-header)',
        color: 'var(--foreground)'
      }}
    >
      <div className="flex h-full items-center justify-between">
        <Title level={2} style={{ cursor: 'pointer' }} onClick={() => navigateTo('/home')}>
          University
        </Title>
      </div>
    </Header>
  );
};

export default AppHeader;
