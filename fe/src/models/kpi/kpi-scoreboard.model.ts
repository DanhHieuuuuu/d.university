export interface IQueryKpiScoreBoard {
  NamHoc?: string;
  ViewUnitId?: number | null;
}
export interface IPersonalScore {
  idNhanSu: number;
  hoTen: string;
  chucVuChinh: string;
  diemTongKet: number;
  xepLoai: string;
  isFinalized: boolean;
}
export interface IUnitScore {
  idDonVi: number;
  tenDonVi: string;
  diemKpiDonVi: number;
  xepLoaiDonVi: string;
  isFinalized: boolean;
}
export interface ISchoolScore {
  diemKpiTruong: number;
  xepLoaiTruong: string;
  isFinalized: boolean;
}
export interface IKpiScoreBoardResponse {
  viewMode: 'SCHOOL' | 'UNIT' | 'PERSONAL';
  myScore: IPersonalScore;
  schoolScore?: ISchoolScore;
  allUnits?: IUnitScore[];
  
  currentUnitScore?: IUnitScore;
  staffScores?: IPersonalScore[];
}