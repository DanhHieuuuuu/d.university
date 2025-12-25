import { IQueryPaging } from '@models/common/model.common';

export type IQueryChuongTrinhKhung = IQueryPaging & {
  NganhId?: number;
  ChuyenNganhId?: number;
  KhoaHocId?: number;
};

export type IViewChuongTrinhKhung = {
  id: number;
  maChuongTrinhKhung: string;
  tenChuongTrinhKhung: string;
  tongSoTinChi: number;
  moTa: string;
  trangThai: boolean;
  khoaHocId: number;
  nganhId: number;
  chuyenNganhId: number;
  tenNganh?: string;
  tenChuyenNganh?: string;
};

export type ICreateChuongTrinhKhung = {
  maChuongTrinhKhung: string;
  tenChuongTrinhKhung: string;
  tongSoTinChi: number;
  moTa: string;
  trangThai: boolean;
  khoaHocId: number;
  nganhId: number;
  chuyenNganhId: number;
};

export type IUpdateChuongTrinhKhung = ICreateChuongTrinhKhung & {
  id: number;
};
