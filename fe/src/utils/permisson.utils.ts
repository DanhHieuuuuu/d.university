import { IPermissionTree } from '@models/permission';

/**
 * Chỉ lấy ra các permission được người dùng chọn
 * Chỉ bao gồm các permission dạng LEAF (không có children con)
 */
export const extractLeafPermissions = (ids: number[], permissionTree: IPermissionTree[]): number[] => {
  const leafSet = new Set<number>();

  const dfs = (node: IPermissionTree) => {
    if (!node.children?.length && ids.includes(node.id)) {
      leafSet.add(node.id);
    }

    node.children?.forEach(dfs);
  };

  permissionTree.forEach(dfs);
  return Array.from(leafSet);
};

/**
 * Xây dựng map quan hệ CHA -> CON cho permission tree
 */

export const buildParentMap = (
  tree: IPermissionTree[],
  parentId?: number,
  map: Map<number, number> = new Map()
): Map<number, number> => {
  tree.forEach((node) => {
    if (parentId !== undefined) {
      map.set(node.id, parentId);
    }

    if (node.children?.length) {
      buildParentMap(node.children, node.id, map);
    }
  });

  return map;
};

/**
 * Từ một permission con, truy ngược toàn bộ permission cha phía trên
 * Thêm các permission cha vào kết quả gửi payload
 */

export const collectAncestors = (id: number, parentMap: Map<number, number>, result: Set<number>) => {
  let current = id;

  while (parentMap.has(current)) {
    const parent = parentMap.get(current)!;
    if (!result.has(parent)) {
      result.add(parent);
    }
    current = parent;
  }
};
