import { IQueryPaging } from '@models/common/model.common';
import { ICreateNsQuanHe } from './quanHeGiaDinh.model';

export type IQueryNhanSu = IQueryPaging & {
  cccd?: string;
  idPhongBan?: number;
};

export type IViewNhanSu = {
  idNhanSu: number;
  maNhanSu?: string;
  hoDem?: string;
  ten?: string;
  hoTen?: string;
  ngaySinh?: Date | string;
  noiSinh?: string;
  gioiTinh?: number;
  quocTich?: number;
  danToc?: number;
  tonGiao?: number;
  nguyenQuan?: string;
  noiOHienTai?: string;
  soCccd?: string;
  ngayCapCccd?: Date | string;
  noiCapCccd?: string;
  soDienThoai?: string;
  email?: string;
  khanCapSoDienThoai?: string;
  khanCapNguoiLienHe?: string;
  tenChucVu: string;
  tenPhongBan: string;
  thongTinGiaDinh?: ICreateNsQuanHe[];
};

export type ICreateNhanSu = {
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
  ngayCapCccd?: Date;
  noiCapCccd?: string;
  soDienThoai?: string;
  email?: string;
  khanCapSoDienThoai?: string;
  khanCapNguoiLienHe?: string;
  thongTinGiaDinh?: ICreateNsQuanHe[];
  chieuCao?: number;
  canNang?: number;
  nhomMau?: string;
};

export type IUpdateNhanSu = {
  idNhanSu: number;
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
  ngayCapCccd?: Date;
  noiCapCccd?: string;
  soDienThoai?: string;
  email?: string;
  khanCapSoDienThoai?: string;
  khanCapNguoiLienHe?: string;
  thongTinGiaDinh?: ICreateNsQuanHe[];
  chieuCao?: number;
  canNang?: number;
  nhomMau?: string;
};
