import { IQueryPaging } from '@models/common/model.common';

export type IQueryChuyenNganh = IQueryPaging & {
  NganhId?: number;
};

export type IViewChuyenNganh = {
  id: number;
  maChuyenNganh: string;
  tenChuyenNganh: string;
  tenTiengAnh: string;
  moTa: string;
  trangThai: boolean;
  nganhId: number;
  tenNganh?: string;
};

export type ICreateChuyenNganh = {
  maChuyenNganh: string;
  tenChuyenNganh: string;
  tenTiengAnh: string;
  moTa: string;
  trangThai: boolean;
  nganhId: number;
};

export type IUpdateChuyenNganh = ICreateChuyenNganh & {
  id: number;
};
