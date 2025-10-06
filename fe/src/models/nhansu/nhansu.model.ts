import { IQueryPaging } from '@models/common/model.common';

export type IQueryNhanSu = IQueryPaging & {
  cccd?: string;
};

export type IViewNhanSu = {
  idNhanSu: number;
  maNhanSu?: string;
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
  ngayCapCccd?: Date;
  noiCapCccd?: string;
  soDienThoai?: string;
  email?: string;
  khanCapSoDienThoai?: string;
  khanCapNguoiLienHe?: string;
  tenChucVu: string;
  tenPhongBan: string;
};

export type ICreateNhanSu = {
  maNhanSu?: string;
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
  maSoThue?: string;
  tenNganHang1?: string;
  atm1?: string;
  tenNganHang2?: string;
  atm2?: string;
  thongTinGiaDinh?: ICreateNsQuanHe[];
};

export type ICreateNsQuanHe = {
  quanHe?: number;
  hoTen?: string;
  ngaySinh?: Date;
  queQuan?: string;
  quocTich?: number;
  soDienThoai?: string;
  ngheNghiep?: string;
  donViCongTac?: string;
};

export type ICreateHopDongNs = {
  soHopDong?: string;
  idLoaiHopDong?: number;
  ngayKyKet?: Date;
  kyLan1?: Date;
  kyLan2?: Date;
  kyLan3?: Date;
  ngayBatDauThuViec?: Date;
  ngayKetThucThuViec?: Date;
  hopDongCoThoiHanTuNgay?: Date;
  hopDongCoThoiHanDenNgay?: Date;
  luongCoBan?: number;
  idToBoMon?: number;
  idPhongBan?: number;
  idChucVu?: number;
  ghiChu?: string;
  idNhanSu?: number;
  thongTinNhanSu?: ICreateNhanSu;
};
