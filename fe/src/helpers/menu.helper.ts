import { MenuProps } from 'antd';
import { IMenu } from '@models/common/menu.model';

export function mapToAntdItems(data: IMenu[], validate: (permissionKey: string) => boolean): MenuProps['items'] {
  return data
    .filter((item) => {
      if (!item.permissionKeys || item.permissionKeys.length === 0) return true;

      // Hiển thị nếu có ít nhất 1 quyền
      return item.permissionKeys.some((p) => validate(p));

      // Nếu muốn yêu cầu tất cả quyền, dùng:
      // return item.permissionKeys.every((p) => validate(p));
    })
    .map((item) => ({
      key: item.routerLink,
      label: item.label,
      icon: item.icon,
      children: item.items ? mapToAntdItems(item.items, validate) : undefined
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

      if (pathname === key || pathname.startsWith(key + '/')) {
        selectedKeys = [key];
        if (parentKey) {
          if (!openKeys.includes(parentKey)) {
            openKeys.push(parentKey);
          }
        }
      }

      if (item.items && item.items.length) {
        traverse(item.items, key);
      }
    }
  };

  traverse(menuItems);

  return { selectedKeys, openKeys };
}
