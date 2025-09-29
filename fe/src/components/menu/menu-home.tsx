'use client';

import { useState } from 'react';
import { useTheme } from 'next-themes';
import { Layout } from 'antd';

import { listMenuCore } from '@/constants/menu/menu.core';
import AppMenu from '@components/common/Menu';
import '@styles/menu.style.scss';

const { Sider } = Layout;

const MenuComponent = () => {
  const [collapsed, setCollapsed] = useState<boolean>(false);

  const { resolvedTheme } = useTheme();
  const siderTheme: 'light' | 'dark' = resolvedTheme === 'dark' ? 'dark' : 'light';

  return (
    <Sider
      theme={siderTheme}
      width="20%"
      collapsible
      collapsed={collapsed}
      onCollapse={(val) => setCollapsed(val)}
      // className="menu-core"
    >
      <AppMenu data={listMenuCore} />
    </Sider>
  );
};

export default MenuComponent;
