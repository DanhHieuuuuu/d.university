import { IQueryPaging } from '@models/common/model.common';

export type IQueryStudent = IQueryPaging & {
  mssv?: string;
};

export type IViewStudent = {
  idStudent: number;
  mssv?: string;
  hoDem?: string;
  ten?: string;
  hoTen?: string;
  ngaySinh?: Date;
  noiSinh?: string;
  gioiTinh?: number;
  quocTich?: number;
  danToc?: number;
  soCccd?: string;
  soDienThoai?: string;
  email?: string;
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
  soCccd?: string;
  soDienThoai?: string;
  email?: string;
};


