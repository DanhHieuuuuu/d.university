import { MenuProps } from 'antd';

type MenuItem = Required<MenuProps>['items'][number];

/**
 * Tìm các key được chọn (selectedKeys) và key menu cha cần mở (openKeys)
 * dựa vào pathname hiện tại.
 */
export function getMenuKeysFromPath(
  menuItems: MenuItem[],
  pathname: string
): { selectedKeys: string[]; openKeys: string[] } {
  let selectedKeys: string[] = [];
  let openKeys: string[] = [];

  const traverse = (items: MenuItem[], parentKey?: string) => {
    for (const item of items) {
      if (!item) continue;
      const key = item.key as string;

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
