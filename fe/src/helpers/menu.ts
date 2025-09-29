import { MenuProps } from 'antd';
import { IMenu } from '@components/common/Menu';

export function mapToAntdItems(data: IMenu[]): MenuProps['items'] {
  return data
    .filter((item) => !item.hidden)
    .map((item) => ({
      key: item.routerLink,
      label: item.label,
      icon: item.icon,
      children: item.items ? mapToAntdItems(item.items) : undefined
    }));
}

export function getMenuKeysFromPath(
  menuItems: IMenu[],
  pathname: string
): { selectedKeys: string[]; openKeys: string[] } {
  let selectedKeys: string[] = [];
  let openKeys: string[] = [];

  const traverse = (items: IMenu[], parentKey?: string) => {
    for (const item of items) {
      if (!item) continue;
      const key = item.routerLink as string;

      // Nếu pathname khớp prefix thì chọn
      if (pathname === key || pathname.startsWith(key + '/')) {
        selectedKeys = [key];
        if (parentKey) openKeys = [parentKey];
      }

      if ((item as any).children) {
        traverse((item as any).children, key);
      }
    }
  };

  traverse(menuItems);

  return { selectedKeys, openKeys };
}
