export type IPermission = {
  permissonKey: string;
  permissionName: string;
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
