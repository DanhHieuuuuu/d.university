
import { LoaiCongThuc } from '@/constants/kpi/loaiCongThuc.enum';
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
  idKpiTruong?: number,
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
  idCongThuc?: number;
  
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
  idCongThuc: number,
  congThucTinh?: string,
  loaiKetQua?: LoaiCongThuc;
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

export interface IKpiDonViLoaiSummary {
  loaiKpi: number;
  tuDanhGia: number;
  capTren: number;
}

export interface IKpiDonViSummary {
  tongTuDanhGia: number;
  tongCapTren: number;
  byLoaiKpi: IKpiDonViLoaiSummary[];
}


export interface IGiaoKpiDonVi {
  idKpiDonVi: number;
  nhanSus: {
    idNhanSu: number;
    trongSo?: string;
  }[];
}

export interface NhanSuDaGiaoDto {
  id?: number;
  idNhanSu: number;
  hoTen?: string;
  trongSo?: string;
  idKpiDonVi?: number;
}