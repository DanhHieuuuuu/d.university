import type { IQueryPaging } from '@models/model.common';
import type { IPermission } from '@models/permission';

export type IRole = {
  id?: string;
  name?: string;
  description?: string;
  createdAt?: string;
  updatedAt?: string;
  permissions?: IPermission[];
};

export type IQueryPagingRoles = IQueryPaging & {};

export type ICreateRole = {
  name: string;
  description?: string;
  rolePermissions?: IPermission[];
};

export type IUpdateRole = ICreateRole & {
  id?: string | null;
};
