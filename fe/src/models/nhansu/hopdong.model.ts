import { IQueryPaging } from '@models/common/model.common';
import { ICreateNhanSu } from './nhansu.model';

export type IQueryHopDong = IQueryPaging & {
  loaiHopDong?: number;
};

export type ICreateHopDong = {
  soHopDong?: string | null;
  idNhanSu?: number;
  idLoaiHopDong?: number;
  ngayKyKet?: Date | null;
  ngayBatDauThuViec?: Date | null;
  ngayKetThucThuViec?: Date | null;
  hopDongCoThoiHanTuNgay?: Date | null;
  hopDongCoThoiHanDenNgay?: Date | null;
  currency?: string | null;
  payFrequency?: string | null;
  maNhanSu?: string;
  maSoThue?: string;
  tenNganHang1?: string;
  atm1?: string;
  tenNganHang2?: string;
  atm2?: string;
  luongCoBan?: number | null;
  ghiChu?: string | null;
  idToBoMon?: number;
  idPhongBan?: number;
  idChucVu?: number;
  hasProbation: boolean;
  thongTinNhanSu?: ICreateNhanSu | null;
};

export type IViewHopDong = {
  id?: number;
  soHopDong?: string | null;
  idNhanSu?: number;
  hoTen?: string | null;
  idLoaiHopDong?: number;
  ngayKyKet?: Date | null;
  ngayBatDauThuViec?: Date | null;
  ngayKetThucThuViec?: Date | null;
  hopDongCoThoiHanTuNgay?: Date | null;
  hopDongCoThoiHanDenNgay?: Date | null;
  luongCoBan?: number | null;
  ghiChu?: string | null;
};
