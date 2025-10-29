export type IPermission = {
  permissonKey: string;
  permissionName: string;
  parentKey?: string;
};

export type IPermissionTree = {
  id: number;
  key?: string;
  label?: string;
  children?: IPermissionTree[];
};

export type IPermissionGroupByEntity = {
  entity?: string;
  permissions: IPermission[];
};

export type IExpertise = {
  id: string;
  name: string;
  description?: string;
};
