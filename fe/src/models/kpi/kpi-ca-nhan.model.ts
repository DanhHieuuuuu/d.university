import { LoaiCongThuc } from '@/app/(home)/kpi/const/loaiCongThuc.enum';
import { IQueryPaging } from '@models/common/model.common';

export type IQueryKpiCaNhan = IQueryPaging & {
  idNhanSu?: number,
  loaiKpi?: number,
  namHoc?: string,
  phongBan?: string,
  trangThai?: string,
  idPhongBan?: number,
  chucVu?: string,
  role?: string,
};

export type IViewKpiCaNhan = {
  id: number,
  kpi: string,
  mucTieu: string,
  trongSo: string,
  loaiKpi: number,
  namHoc?: string,
  nhanSu: string,
  idNhanSu?: number,
  trangThai: number,
  phongBan?: string,
  ketQuaThucTe?: number,
  diemKpi?: number; 
  capTrenDanhGia?: number,
  diemKpiCapTren?: number,
  loaiKetQua?: LoaiCongThuc;
  isActive?: number,
  role?: string,
  ghiChu?: string,
  congThuc?:string;
  linhVuc?: string,
  chienLuoc?: string,
  isCaNhanKeKhai?: boolean,
};

export type ICreateKpiCaNhan = {
  kpi: string,
  loaiKPI: number,
  linhVuc: string,
  mucTieu: string,
  trongSo: string,
  idNhanSu: number,
  namHoc: string,
};

export type IUpdateKpiCaNhan = ICreateKpiCaNhan & {
  id: number,
};

export type IUpdateTrangThaiKpiCaNhan = {
  ids: number[];
  trangThai: number;
  note?: string;
};
export type IViewGetTrangThai = {
  trangThai?: number,
};
export type IViewListRole = {
  role?: string,
  tenRole?: string,
};

export type IUpdateKpiCaNhanThucTe = {
  id: number,
  ketQuaThucTe?: number,
  diemKpi?: number
};
export interface IUpdateKpiCaNhanThucTeList {
  items: IUpdateKpiCaNhanThucTe[],
};

export type IUpdateCapTrenDanhGia = {
  id: number,
  ketQuaCapTren?: number,
  diemKpiCapTren?: number
};
export interface IUpdateCapTrenDanhGiaList {
  items: IUpdateCapTrenDanhGia[],
};


export interface IViewNhanSu {
  idNhanSu?: number,
  tenNhanSu?: string,
  hoTenDayDu?: string,
  idnhanSu?: number,
  tenPhongBan?: string,
  hienTaiPhongBan?: number,
  maNhanSu?: string,
  tenHienThi?: string,
};