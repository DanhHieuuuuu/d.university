import { IQueryPaging } from '@models/common/model.common';

export type IQueryKhoaHoc = IQueryPaging & {};

export type IViewKhoaHoc = {
  id: number;
  maKhoaHoc: string;
  tenKhoaHoc: string;
  nam: string;
  cachViet: string;
  nguoiTao: number;
  ngayTao: string;
};
