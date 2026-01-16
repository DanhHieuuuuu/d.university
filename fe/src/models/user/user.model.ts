import { IQueryPaging } from '@models/common/model.common';

export type IQueryUser = IQueryPaging & {
  IdPhongBan?: number;
};

export type IUserView = {
  id?: number;
  maNhanSu?: string;
  hoDem?: string;
  ten?: string;
  ngaySinh?: Date;
  noiSinh?: string;
  soDienThoai?: string;
  email?: string;
  email2?: string;
  soCccd?: string;
  tenChucVu?: string;
  tenPhongBan?: string;
  idPhongBan?: number;
  trangThai?: string;
  roleIds?: number[];
};

// export interface INhanSuData {
//   idNhanSu: number | null;
//   maNhanSu: string;
//   hoDem: string | null;
//   ten: string | null;
//   hoTen: string | null;
//   ngaySinh: string | null;
//   noiSinh: string | null;
//   gioiTinh: string | null;
//   soCccd: string | null;
//   soDienThoai: string | null;
//   email: string | null;
//   tenChucVu: string | null;
//   tenPhongBan: string | null;
//   roleIds?: number[];
// }

export interface IUserCreate {
  maNhanSu: string;
  email2?: string | null;
  password?: string | null;
}
