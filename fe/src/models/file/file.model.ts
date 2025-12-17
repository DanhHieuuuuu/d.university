import type { IQueryPaging } from '@models/common/model.common';

export type IFile = {
  id?: number;
  name?: string;
  description?: string;
  link?: string;
  applicationField?: string;
};

export type ICreateFile = {
  name: string;
  description?: string;
  file?: string;
  applicationField?: string;
};

export type IQueryFile = IQueryPaging & {
  Name?: string;
};

export type IUpdateFile = ICreateFile & {
  id: number | null;
};
