'use client';

import { useState } from 'react';
import { useTheme } from 'next-themes';
import { Layout, Menu, MenuProps } from 'antd';
import { usePathname } from 'next/navigation';
import { useNavigate } from '@hooks/navigate';

import { getMenuKeysFromPath } from '@src/helpers/menu';
import { listMenuCore } from '@/constants/menu/menu.core';
import '@styles/menu.style.scss';

const { Sider } = Layout;

const MenuComponent = () => {
  const [collapsed, setCollapsed] = useState<boolean>(false);
  const pathname = usePathname();
  const { navigateTo } = useNavigate();

  const { resolvedTheme } = useTheme();
  const siderTheme: 'light' | 'dark' = resolvedTheme === 'dark' ? 'dark' : 'light';

  const { selectedKeys, openKeys } = getMenuKeysFromPath(listMenuCore, pathname);

  const onClick: MenuProps['onClick'] = (e) => {
    navigateTo(e.key);
  };

  return (
    <Sider
      theme={siderTheme}
      width="20%"
      collapsible
      collapsed={collapsed}
      onCollapse={(val) => setCollapsed(val)}
      // className="menu-core"
    >
      <Menu
        mode="inline"
        items={listMenuCore}
        openKeys={openKeys}
        selectedKeys={selectedKeys}
        onClick={onClick}
      />
    </Sider>
  );
};

export default MenuComponent;
