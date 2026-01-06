import { IQueryPaging } from '@models/common/model.common';

export type IQueryPhongBan = IQueryPaging & {
  idLoaiPhongBan?: number;
};

export type IViewPhongBan = {
  id: number;
  maPhongBan: string;
  tenPhongBan: string;
  loaiPhongBan: string;
  diaChi: string;
  hotline: string;
  fax: string;
  ngayThanhLap: Date;
  stt: number | null;
  chucVuNguoiDaiDien: string;
  nguoiDaiDien: string;
  isActive?: boolean;
};

export type IViewPhongBanWithNhanSu = {
  id: number;
  maPhongBan: string;
  tenPhongBan: string;
  loaiPhongBan: string;
  diaChi: string;
  hotline: string;
  fax: string;
  ngayThanhLap: Date;
  stt: number | null;
};

export type ICreatePhongBan = {
  maPhongBan: string;
  tenPhongBan: string;
  idLoaiPhongBan: number;
  diaChi: string;
  hotline: string;
  fax: string;
  ngayThanhLap: Date;
  stt: number | null;
  chucVuNguoiDaiDien: string | null;
  nguoiDaiDien: string;
};

export type IUpdatePhongBan = ICreatePhongBan & {
  id: number;
};
