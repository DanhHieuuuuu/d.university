import { IQueryPaging } from '@models/common/model.common';

export type IQueryKpiCaNhan = IQueryPaging & {
  idNhanSu?: number,
  loaiKpi?: number,
  namHoc?: string,
  phongBan?: string ,
  trangThai?:string,
  idPhongBan?:number,
  chucVu?:string,
  role?:string,
};

export type IViewKpiCaNhan = {
    id: number,
    kpi: string,
    mucTieu: string,
    trongSo: string,
    loaiKPI: number,
    namHoc?: string,
    nhanSu: string,
    idNhanSu?: number,
    trangThai: number,
    phongBan?:string,
    ketQuaThucTe?: number,
    role?: string,
    linhVuc?: string,
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
};
export interface IUpdateKpiCaNhanThucTeList {
  items: IUpdateKpiCaNhanThucTe[],
};

export interface IViewNhanSu {
  idNhanSu?: number,
  tenNhanSu?: string,
  hoTenDayDu?: string,
  idnhanSu?: number,
  tenPhongBan?: string,
  hienTaiPhongBan?:number,
  maNhanSu?:string,
  tenHienThi?:string,
};