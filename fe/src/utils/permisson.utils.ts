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
 * Chọn permisisonId con -> lấy ra toàn bộ permisisonId cha phía trên
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
 * Chọn permissionID cha -> lấy ra toàn bộ permissionId con bên dưới
 */
export const buildChildrenMap = (
  tree: IPermissionTree[],
  map: Map<number, number[]> = new Map()
): Map<number, number[]> => {
  tree.forEach((node) => {
    if (node.children?.length) {
      map.set(
        node.id,
        node.children.map((c) => c.id)
      );

      buildChildrenMap(node.children, map);
    }
  });

  return map;
};

/**
 * Thu thập toàn bộ permisisonID cha bên trên
 * @param id PermisisonID con
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

/**
 * Thu thập toàn bộ permisisonID con bên dưới
 * @param id PermisisonID cha
 */
export const collectDescendants = (id: number, childrenMap: Map<number, number[]>, result: Set<number>) => {
  const children = childrenMap.get(id);
  if (!children?.length) return;

  for (const child of children) {
    if (!result.has(child)) {
      result.add(child);
      collectDescendants(child, childrenMap, result);
    }
  }
};

/**
 * Chuẩn hóa trước khi submit
 * Tích cha -> add toàn bộ id con
 * Tích con -> add cha
 */
export const normalizePermissionIds = (selectedIds: number[], permissionTree: IPermissionTree[]): number[] => {
  const parentMap = buildParentMap(permissionTree);
  const childrenMap = buildChildrenMap(permissionTree);

  const result = new Set<number>();

  selectedIds.forEach((id) => {
    result.add(id);
    collectAncestors(id, parentMap, result);
    collectDescendants(id, childrenMap, result);
  });

  return Array.from(result);
};
