import { LoaiCongThuc } from '@/app/(home)/kpi/const/loaiCongThuc.enum';
import { IQueryPaging } from '@models/common/model.common';

export type IQueryKpiDonVi = IQueryPaging & {
  idDonVi?: number,
  loaiKpi?: number,
  namHoc?: string,
  trangThai?: number,
};

export type IViewKpiDonVi = {
  id: number,
  kpi: string,
  mucTieu: string,
  trongSo: number,
  donVi: string,
  idDonVi?: number,
  loaiKpi: number,
  namHoc: string,
  trangThai: number,
  ketQuaThucTe: number,
  capTrenDanhGia: number,
  diemKpi?: number;
  diemKpiCapTren?: number;
  isActive?: number;
  loaiKetQua?: LoaiCongThuc;
  ghiChu?: string;
  congThuc?:string;
};

export type ICreateKpiDonVi = {
  kpi: string,
  loaiKpi: number,
  // linhVuc: string,
  mucTieu: string,
  trongSo: string,
  idDonVi: number,
  namHoc: string,
  idKpiTruong?: number,
};

export type IUpdateTrangThaiKpiDonVi = {
  ids: number[];
  trangThai: number;
  note?: string;
};

export type IUpdateKpiDonVi = ICreateKpiDonVi & {
  id: number,
};

export type IViewGetTrangThai = {
  trangThai?: number,
}
export type IViewListRole = {
  role?: string,
  tenRole?: string,
}

export type IUpdateKpiDonViThucTe = {
  id: number,
  ketQuaThucTe?: number,
}
export interface IUpdateKpiDonViThucTeList {
  items: IUpdateKpiDonViThucTe[],
}

export type IUpdateCapTrenDonViDanhGia = {
  id: number,
  ketQuaCapTren?: number,
  diemKpiCapTren?: number
};
export interface IUpdateCapTrenDonViDanhGiaList {
  items: IUpdateCapTrenDonViDanhGia[],
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
}