'use client';

import { Layout } from 'antd';
import { useTheme } from 'next-themes';

import { listMenuManager } from '@/constants/menu/menu.role';
import AppMenu from '@components/common/Menu';
import '@styles/menu.style.scss';

const { Sider } = Layout;

const RoleMenuComponent = () => {
  const { resolvedTheme } = useTheme();
  const siderTheme: 'light' | 'dark' = resolvedTheme === 'dark' ? 'dark' : 'light';

  return (
    <Sider width="20%" theme={siderTheme} className="role-menu">
      <AppMenu data={listMenuManager} />
    </Sider>
  );
};

export default RoleMenuComponent;
