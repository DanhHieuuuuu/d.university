'use client';

import React, { useEffect, useState } from 'react';
import { usePathname } from 'next/navigation';
import { Menu, MenuProps } from 'antd';
import { useNavigate } from '@hooks/navigate';
import { getMenuKeysFromPath, mapToAntdItems } from '@helpers/menu.helper';
import { IMenu } from '@models/common/menu.model';

type MenuPropsCustom = {
  data: IMenu[];
};

const AppMenu: React.FC<MenuPropsCustom> = ({ data }) => {
  const pathname = usePathname();
  const { navigateTo } = useNavigate();

  const { selectedKeys, openKeys: initOpenKeys } = getMenuKeysFromPath(data, pathname);
  const [openKeys, setOpenKeys] = useState<string[]>(initOpenKeys);

  useEffect(() => {
    const { openKeys } = getMenuKeysFromPath(data, pathname);
    setOpenKeys(openKeys);
  }, [pathname, data]);

  const onClick: MenuProps['onClick'] = (e) => {
    navigateTo(e.key);
  };

  const items = mapToAntdItems(data);

  return (
    <Menu
      mode="inline"
      items={items}
      openKeys={openKeys}
      selectedKeys={selectedKeys}
      onOpenChange={(keys) => setOpenKeys(keys as string[])} // cần có
      onClick={onClick}
    />
  );
};

export default AppMenu;
