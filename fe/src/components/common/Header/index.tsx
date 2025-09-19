'use client';

import { Layout, Typography } from 'antd';
import '@src/styles/globals.scss';
import { useNavigate } from '@hooks/navigate';

const { Header: HeaderAntd } = Layout;
const { Title } = Typography;

const HeaderComponent = () => {
  const { navigateTo } = useNavigate();
  return (
    <HeaderAntd
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
    </HeaderAntd>
  );
};

export default HeaderComponent;
