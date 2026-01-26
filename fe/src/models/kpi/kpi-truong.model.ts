import { LoaiCongThuc } from '@/constants/kpi/loaiCongThuc.enum';
import { IQueryPaging } from '@models/common/model.common';

export type IQueryKpiTruong = IQueryPaging & {
  loaiKpi?: number;
  namHoc?: string;
  trangThai?: number;
};

export interface IViewGetListKpiTruong {
  id?: number;
  kpi?: string;
}

export interface IKpiTruongLoaiSummary {
  loaiKpi: number;
  tuDanhGia: number;
  capTren: number;
}

export interface IKpiTruongSummary {
  tongTuDanhGia: number;
  tongCapTren: number;
  byLoaiKpi: IKpiTruongLoaiSummary[];
}

export type IViewKpiTruong = {
  id: number;
  linhVuc: string;
  chienLuoc: string;
  kpi: string;
  mucTieu: string;
  trongSo: string;
  loaiKpi: number;
  namHoc: string;
  trangThai: number;
  ketQuaThucTe?: number;
  capTrenDanhGia: number;
  diemKpi?: number;
  diemKpiCapTren?: number;
  isActive?: number;
  loaiKetQua?: LoaiCongThuc;
  ghiChu?: string;
  congThuc?: string;
  idCongThuc?: number;
};

export type ICreateKpiTruong = {
  kpi: string;
  loaiKpi: number;
  linhVuc: string;
  chienLuoc: string;
  mucTieu: string;
  trongSo: string;
  idDonVi: number;
  namHoc: string;
  idCongThuc: number;
  congThucTinh?: string;
  loaiKetQua?: LoaiCongThuc;
};

export type IUpdateTrangThaiKpiTruong = {
  ids: number[];
  trangThai: number;
  note?: string;
};

export type IUpdateKpiTruong = ICreateKpiTruong & {
  id: number;
};

export type IViewGetTrangThai = {
  trangThai?: number;
};

export type IUpdateKpiTruongThucTe = {
  id: number;
  ketQuaThucTe?: number;
};
export interface IUpdateKpiTruongThucTeList {
  items: IUpdateKpiTruongThucTe[];
}

export type IUpdateCapTrenTruongDanhGia = {
  id: number;
  ketQuaCapTren?: number;
  diemKpiCapTren?: number;
};
export interface IUpdateCapTrenTruongDanhGiaList {
  items: IUpdateCapTrenTruongDanhGia[];
}
