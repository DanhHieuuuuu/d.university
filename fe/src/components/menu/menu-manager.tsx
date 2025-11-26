'use client';

import { Layout } from 'antd';

import { listMenuManager } from '@/constants/menu/menu.role';
import AppMenu from '@components/common/Menu';
import '@styles/menu.style.scss';

const { Sider } = Layout;

const RoleMenuComponent = () => {
  return (
    <Sider width="20%" className="role-menu" breakpoint="lg" collapsedWidth="80">
      <AppMenu data={listMenuManager} />
    </Sider>
  );
};

export default RoleMenuComponent;
