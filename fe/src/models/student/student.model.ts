import { IQueryPaging } from '@models/common/model.common';

export type IQueryStudent = IQueryPaging & {
  mssv?: string;
};

export type IViewStudent = {
  idStudent: number;
  mssv: string;
  hoDem?: string;
  ten?: string;
  hoTen?: string;
  ngaySinh?: Date;
  noiSinh?: string;
  gioiTinh?: number;
  quocTich?: number;
  danToc?: number;
  tonGiao?: number;
  nguyenQuan?: string;
  noiOHienTai?: string;
  soCccd?: string;
  soDienThoai?: string;
  email?: string;

  khoaHoc?: number;
  khoa?: number;
  nganh?: number;
  chuyenNganh?: number;
  trangThaiHoc?: number;

};

export type ICreateStudent = {
  mssv?: string;
  hoDem?: string;
  ten?: string;
  ngaySinh?: Date;
  noiSinh?: string;
  gioiTinh?: number;
  quocTich?: number;
  danToc?: number;
  tonGiao?: number;
  nguyenQuan?: string;
  noiOHienTai?: string;
  soCccd?: string;
  soDienThoai?: string;
  email?: string;
  khoaHoc?: number;
  khoa?: number;
  nganh?: number;
  chuyenNganh?: number;
};

export type IUpdateStudent = ICreateStudent & {
  idStudent: number;
  trangThaiHoc?: number;
};
