'use client';

import { Layout, Menu, MenuProps } from 'antd';
import { useTheme } from 'next-themes';
import { usePathname } from 'next/navigation';
import { useNavigate } from '@hooks/navigate';

import { getMenuKeysFromPath } from '@src/helpers/menu';
import { listMenuRole } from '@/constants/menu/menu.role';
import '@styles/menu.style.scss';

const { Sider } = Layout;

const RoleMenuComponent = () => {
  const pathname = usePathname();
  const { navigateTo } = useNavigate();

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
        selectedKeys={selectedKeys}
        onClick={onClick}
      />
    </Sider>
  );
};

export default RoleMenuComponent;
