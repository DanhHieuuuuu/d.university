import { IQueryPaging } from "@models/common/model.common";

export type IQueryHopDong = IQueryPaging & {
  loaiHopDong?: number;
}

export type ICreateHopDong = {
  soHopDong?: string;
  idNhanSu?: number;
  idLoaiHopDong?: number;
  ngayKyKet?: Date;
  ngayBatDauThuViec?: Date;
  ngayKetThucThuViec?: Date;
  hopDongCoThoiHanTuNgay?: Date;
  hopDongCoThoiHanDenNgay?: Date;
  currency: string | null;
  payFrequency: string | null;
  maNhanSu?: string;
  maSoThue?: string;
  tenNganHang1?: string;
  atm1?: string;
  tenNganHang2?: string;
  atm2?: string;
  luongCoBan?: number;
  ghiChu?: string;
  idToBoMon?: number;
  idPhongBan?: number;
  idChucVu?: number;
};

export type IViewHopDong = {
  id?: number;
  soHopDong?: string | null;
  idNhanSu?: number | null;
  hoTen?: string | null;
  idLoaiHopDong?: number | null;
  ngayKyKet?: Date;
  ngayBatDauThuViec?: Date | null;
  ngayKetThucThuViec?: Date | null;
  hopDongCoThoiHanTuNgay?: Date;
  hopDongCoThoiHanDenNgay?: Date | null;
  luongCoBan?: number | null;
  ghiChu?: string | null;
};
