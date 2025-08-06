export type IPermission = {
  id?: string;
  action?: string;
  entity?: string;
  name?: string;
  createdAt?: string;
  updatedAt?: string;
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
