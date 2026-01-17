import { IQueryPaging } from '@models/common/model.common';
import { ICriteria, IQuestion, ITarget, IViewRequest } from './request.model';

export type IQuerySurvey = IQueryPaging & {
  maKhaoSat?: string;
  idPhongBan?: number;
  status?: number;
};

export interface IViewSurvey {
  id: number;
  maKhaoSat: string;
  tenKhaoSat: string;
  moTa: string;
  thoiGianBatDau: string | Date;
  thoiGianKetThuc: string | Date;
  status: number;
  statusName: string;
  maYeuCauGoc?: string;
}

export type ISurveyDetail = IViewSurvey & {
  idPhongBan: number;
  surveyRequest?: ISurveyRequestSource;
};

export interface ISurveyRequestSource {
  id: number;
  maYeuCau: string;
  tenKhaoSatYeuCau: string;
  questions?: IQuestion[];
  targets?: ITarget[];
  criterias?: ICriteria[];
}

export interface ICreateSurvey {
  maKhaoSat: string;
  tenKhaoSat: string;
  moTa: string;
  thoiGianBatDau: string | Date;
  thoiGianKetThuc: string | Date;
  idPhongBan: number;
  maYeuCauGoc?: string;
}

export type IUpdateSurvey = ICreateSurvey & {
  id: number;
};

export type IQueryMySurvey = IQueryPaging & {
  status?: number;
};

export interface IMySurveyItem {
  id: number;
  maKhaoSat: string;
  tenKhaoSat: string;
  thoiGianBatDau: string | Date;
  thoiGianKetThuc: string | Date;
  status: number;
  statusName: string;
  maYeuCauGoc?: string;
  thoiGianNop?: string | Date;
}

export interface IAnswerExam {
  id: number;
  noiDung: string;
  thuTu: number;
}

export interface ISurveyExam {
  id: number;
  noiDung: string;
  loaiCauHoi: number;
  answers: IAnswerExam[];
}

export interface ISavedAnswer {
  questionId: number;
  selectedAnswerId?: number; // For single choice (type 1)
  selectedAnswerIds?: number[]; // For multiple choice (type 2)
  textResponse?: string; // For essay (type 3)
}

export interface IStartSurveyResponse {
  submissionId: number;
  surveyId: number;
  tenKhaoSat: string;
  thoiGianBatDau: string | Date;
  questions: ISurveyExam[];
  savedAnswers: ISavedAnswer[];
}

export interface ISubmitSurvey {
  submissionId: number;
  answers: ISavedAnswer[];
}

// AI Report Models
export interface IAIReportDetail {
  id: number;
  idBaoCao: number;
  idTieuChi: number;
  diemCamXuc: number;
  nhanCamXuc: string;
  tomTatNoiDung: string;
  xuHuong: string;
  goiYCaiThien: string;
  tenTieuChi: string;
  weight: number;
}

export interface IAnalyzeWithAI {
  reportId: number;
}

export interface ISurveyResult {
  submissionId: number;
  totalScore: number;
  totalCorrect: number;
  totalQuestions: number;
  submitTime: string | Date;
}

// Survey Log Models
export type IQuerySurveyLog = IQueryPaging & {
  loaiHanhDong?: string;
  tuNgay?: string | Date;
  denNgay?: string | Date;
};

export interface IViewSurveyLog {
  id: number;
  idNguoiThaoTac?: number;
  tenNguoiThaoTac?: string;
  loaiHanhDong: string;
  moTa: string;
  tenBang: string;
  idDoiTuong: string;
  duLieuCu?: string;
  duLieuMoi?: string;
  createdAt: string | Date;
}
