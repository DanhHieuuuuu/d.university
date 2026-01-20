'use client';

import React, { useEffect, useState } from 'react';
import { usePathname } from 'next/navigation';
import { Menu, MenuProps } from 'antd';

import { getMenuKeysFromPath, mapToAntdItems } from '@helpers/menu.helper';
import { IMenu } from '@models/common/menu.model';
import { useNavigate } from '@hooks/navigate';
import { useAppSelector } from '@redux/hooks';

type MenuPropsCustom = {
  data: IMenu[];
};

const AppMenu: React.FC<MenuPropsCustom> = ({ data }) => {
  const pathname = usePathname();
  const { navigateTo } = useNavigate();

  const userPermissions = useAppSelector((state) => state.authState.permissions) || [];

  const { selectedKeys, openKeys: initOpenKeys } = getMenuKeysFromPath(data, pathname);
  const [openKeys, setOpenKeys] = useState<string[]>(initOpenKeys);

  useEffect(() => {
    const { openKeys } = getMenuKeysFromPath(data, pathname);
    setOpenKeys(openKeys);
  }, [pathname, data]);

  const onClick: MenuProps['onClick'] = (e) => {
    navigateTo(e.key);
  };

  const validatePermission = (permissionKey: string) => {
    return userPermissions.includes(permissionKey);
  };
  const items = mapToAntdItems(data, validatePermission);

  return (
    <Menu
      mode="inline"
      items={items}
      openKeys={openKeys}
      selectedKeys={selectedKeys}
      onOpenChange={(keys) => setOpenKeys(keys as string[])}
      onClick={onClick}
    />
  );
};

export default AppMenu;
