import { IQueryPaging } from '@models/common/model.common';

export type IQueryToBoMon = IQueryPaging & {
  
};

export type IViewToBoMon = {
  id: number;
  tenBoMon: string;
};

export type ICreateToBoMon = {
  maBoMon: string;
  tenBoMon: string;
  ngayThanhLap: Date;
  idPhongBan: number;
};
