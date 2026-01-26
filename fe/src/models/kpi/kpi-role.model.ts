import { IQueryPaging } from '@models/common/model.common';

export type IViewKpiRole = {
  id: number;
  idNhanSu?: number;
  maNhanSu?: string;
  tenNhanSu?: string;
  tenPhongBan?: string;
  idDonVi?: number;
  tenDonViKiemNhiem?: string;
  tiLe?: number;
  role?: string;
};

export type IQueryKpiRole = IQueryPaging & {
  keyword?: string;
  chucVu?: string;
};

export type ICreateKpiRole = {
  idNhanSu: number;
  idDonVi?: number;
  tiLe?: number;
  role?: string;
};

export type IUpdateKpiRole = ICreateKpiRole & {
  id: number;
};

export type INhanSu = {
  idnhanSu?: number;
  maNhanSu?: string;
  hoTenDayDu?: string;
  tenPhongBan?: string;
};

export interface IRoleOption {
  label: string;
  value: string;
}
