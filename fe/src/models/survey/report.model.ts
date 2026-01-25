import { IQueryPaging } from '@models/common/model.common';

export type IQueryReport = IQueryPaging & {
  maKhaoSat?: string;
};

export interface IReportItem {
  reportId: number;
  surveyId: number;
  tenKhaoSat: string;
  totalParticipants: number;
  averageScore: number;
  generatedAt: string | Date;
}

export interface IQuestionStat {
  questionId: number;
  content: string;
  type: number;
  recentTextResponses: string[];
}

export interface IAnswerStat {
  label: string;
  count: number;
  percent: number;
}

export interface IRespondent {
  submissionId: number;
  userCode: string;
  fullName: string;
  submitTime?: string | Date;
  totalScore: number;
}

export interface IReportDetail {
  reportId: number;
  tenKhaoSat: string;
  totalParticipants: number;
  averageScore: number;
  lastGenerated: string | Date;
  statistics: {
    questions: IQuestionStat[];
  };
  respondents: IRespondent[];
}

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
