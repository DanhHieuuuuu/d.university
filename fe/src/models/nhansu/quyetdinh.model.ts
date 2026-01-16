import { IQueryPaging } from "@models/common/model.common";

export type IQueryQuyetDinh = IQueryPaging & {
  idLoaiHopDong?: number;
}

export type ICreateQuyetDinh = {
  idNhanSu?: number | null;
  maNhanSu?: string | null;
  loaiQuyetDinh?: number | null;
  noiDungTomTat?: string | null;
  ngayHieuLuc?: Date | null;
}

export type IUpdateQuyetDinh = {
  idQuyetDinh?: number | null;
  status?: number | null;
}

export type IViewQuyetDinh = {
  id?: number | null;
  idNhanSu?: number | null;
  maNhanSu?: string | null;
  hoTen?: string | null;
  loaiQuyetDinh?: number | null;
  noiDungTomTat?: string | null;
  ngayHieuLuc?: Date | null;
};