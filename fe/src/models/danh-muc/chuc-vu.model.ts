import { IQueryPaging } from '@models/common/model.common';

export type IQueryChucVu = IQueryPaging & {};

export type IViewChucVu = {
  id: number;
  maChucVu: string;
  tenChucVu: string;
  hsChucVu: number;
  hsTrachNhiem: number;
};

export type ICreateChucVu = {
  maChucVu: string;
  tenChucVu: string;
  hsChucVu: number;
  hsTrachNhiem: number;
};

export type IUpdateChucVu = ICreateChucVu & {
  id: number;
};
