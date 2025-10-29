import type { IQueryPaging } from '@models/common/model.common';

export type IRole = {
  id?: number;
  name?: string;
  description?: string;
  status?: number;
  totalUser?: number;
};

export type IQueryRole = IQueryPaging & {};

export type ICreateRole = {
  name: string;
  description?: string;
};

export type IUpdateRole = ICreateRole & {
  id: number | null;
};

export type IRoleDetail = {
  id?: number;
  name?: string;
  description?: string;
  permissions?: string[]
  permissionIds?: number[];
};

export type IUpdateRolePermission = {
  roleId: number;
  permissionIds: number[];
};
