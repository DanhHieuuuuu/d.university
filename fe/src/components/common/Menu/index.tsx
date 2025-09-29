'use client';

import { useEffect, useState } from 'react';
import { usePathname } from 'next/navigation';
import { Menu as MenuAntd, MenuProps } from 'antd';
import { useNavigate } from '@hooks/navigate';
import { getMenuKeysFromPath, mapToAntdItems } from '@helpers/menu';

export type IMenu = {
  label: string;
  routerLink: string;
  icon?: React.ReactNode;
  hidden?: boolean;
  items?: IMenu[];
};

type MenuPropsCustom = {
  data: IMenu[];
};

const AppMenu = ({ data }: MenuPropsCustom) => {
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
    <MenuAntd
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
