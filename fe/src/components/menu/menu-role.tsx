'use client';

import { useTheme } from 'next-themes';
import { usePathname, useRouter } from 'next/navigation';
import { Layout, Menu, MenuProps } from 'antd';
import { GlobalOutlined, HomeOutlined, TeamOutlined } from '@ant-design/icons';
import { getMenuKeysFromPath } from '@src/helpers/menu';
import '@src/styles/menu.style.scss';
import { useNavigateTo } from '@src/hooks/navigateTo';

const { Sider } = Layout;

type MenuItem = Required<MenuProps>['items'][number];

const listMenuRole: MenuItem[] = [
  {
    key: '/role',
    label: 'Tổng quan',
    icon: <HomeOutlined />
  },
  {
    key: '/role/web-manage',
    label: 'Phân quyền website',
    icon: <GlobalOutlined />
  },
  {
    key: '/role/user-manage',
    label: 'Quản lý tài khoản',
    icon: <TeamOutlined />
  }
];

const RoleMenuComponent = () => {
  const pathname = usePathname();
  const { navigateTo } = useNavigateTo();

  const { resolvedTheme } = useTheme();
  const siderTheme: 'light' | 'dark' = resolvedTheme === 'dark' ? 'dark' : 'light';

  const { selectedKeys, openKeys } = getMenuKeysFromPath(listMenuRole, pathname);

  const onClick: MenuProps['onClick'] = (e) => {
    navigateTo(e.key);
  };

  return (
    <Sider width="20%" theme={siderTheme} className="role-menu">
      <Menu
        mode="inline"
        items={listMenuRole}
        openKeys={openKeys}
        defaultSelectedKeys={selectedKeys}
        onClick={onClick}
      />
    </Sider>
  );
};

export default RoleMenuComponent;
