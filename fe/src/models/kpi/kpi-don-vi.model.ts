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
};

export type ICreateKpiDonVi = {
  kpi: string,
  loaiKpi: number,
  // linhVuc: string,
  mucTieu: string,
  trongSo: string,
  idDonVi: number,
  namHoc: string,
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