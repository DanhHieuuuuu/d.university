import { IQueryPaging } from '@models/common/model.common';

export type IQueryQuyetDinh = IQueryPaging & {
  loaiQuyetDinh?: number;
  status?: number;
};

export type ICreateQuyetDinh = {
  idNhanSu?: number | null;
  maNhanSu?: string | null;
  loaiQuyetDinh?: number | null;
  noiDungTomTat?: string | null;
  ngayHieuLuc?: Date | null;
};

export type IUpdateQuyetDinh = {
  idQuyetDinh?: number | null;
  status?: number | null;
};

export type IViewQuyetDinh = {
  id?: number | null;
  idNhanSu?: number | null;
  maNhanSu?: string | null;
  hoTen?: string | null;
  loaiQuyetDinh?: number | null;
  noiDungTomTat?: string | null;
  ngayHieuLuc?: Date | null;
  status?: number | null;
};

export type IDetailQuyetDinh = {
  id?: number | null;
  idNhanSu?: number | null;
  maNhanSu?: string | null;
  hoTen?: string | null;
  loaiQuyetDinh?: number | null;
  noiDungTomTat?: string | null;
  ngayHieuLuc?: Date | null;
  status?: number | null;
  history?: IViewQuyetDinhLog[];
};

export type IViewQuyetDinhLog = {
  id: number | null;
  idQuyetDinh: number | null;
  oldStatus: number | null;
  newStatus: number | null;
  description: string | null;
  createdDate: Date | null;
};
