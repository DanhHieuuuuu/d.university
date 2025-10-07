'use client';

import { useState } from 'react';
import { useTheme } from 'next-themes';
import { Button, Layout, Space } from 'antd';
import { MenuFoldOutlined, MenuOutlined, MenuUnfoldOutlined } from '@ant-design/icons';

import { listMenuCore } from '@/constants/menu/menu.core';
import AppMenu from '@components/common/Menu';
import '@styles/menu.style.scss';

const { Sider } = Layout;

const MenuComponent = () => {
  const [collapsed, setCollapsed] = useState<boolean>(false);

  const { resolvedTheme } = useTheme();
  const siderTheme: 'light' | 'dark' = resolvedTheme === 'dark' ? 'dark' : 'light';

  return (
    <Sider theme={siderTheme} width="20%" collapsible collapsed={collapsed} trigger={null} className="menu-core">
      <div style={{ display: 'flex', padding: 8, justifyContent: collapsed ? 'center' : 'space-between' }}>
        <div className={`menu-title ${collapsed ? 'collapsed' : ''}`}>
          <MenuOutlined className="menu-icon" />
          <span className="menu-text">Menu</span>
        </div>
        <Button
          type="primary"
          color="blue"
          icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
          onClick={() => setCollapsed(!collapsed)}
          size="large"
        />
      </div>
      <AppMenu data={listMenuCore} />
    </Sider>
  );
};

export default MenuComponent;
