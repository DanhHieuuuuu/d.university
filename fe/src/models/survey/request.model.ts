import { IQueryPaging } from '@models/common/model.common';

export type IQueryRequest = IQueryPaging & {
  maYeuCau?: string;
  trangThai?: number;
};

export interface IViewRequest {
  id: number;
  maYeuCau: string;
  tenKhaoSatYeuCau: string;
  moTa: string;
  thoiGianBatDau: string | Date;
  thoiGianKetThuc: string | Date;
  idPhongBan: number;
  trangThai: number;
  lyDoTuChoi: string;

  targets?: ITarget[];
  questions?: IQuestion[];
  criterias?: ICriteria[];
}

export interface ICreateRequest {
  maYeuCau: string;
  tenKhaoSatYeuCau: string;
  moTa: string;
  thoiGianBatDau: string | Date;
  thoiGianKetThuc: string | Date;
  idPhongBan: number;
  targets: ITarget[];
  questions: IQuestion[];
  criterias: ICriteria[];
}

export type IUpdateRequest = ICreateRequest & {
  id: number;
};

export interface ITarget {
  loaiDoiTuong: number;
  idPhongBan?: number;
  idKhoa?: number;
  idKhoaHoc?: number;
  moTa: string;
}

export interface IAnswer {
  noiDung: string;
  value: number;
  thuTu: number;
  isCorrect: boolean;
}

export interface IQuestion {
  maCauHoi: string;
  noiDung: string;
  loaiCauHoi: number;
  batBuoc: boolean;
  thuTu: number;
  answers: IAnswer[];
}

export interface ICriteria {
  tenTieuChi: string;
  weight: number;
  keyword: string;
  moTa: string;
}

export interface IRejectRequest {
  id: number;
  reason: string;
}
