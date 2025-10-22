import { IQueryPaging } from '@models/common/model.common';

export type IQueryToBoMon = IQueryPaging & {};

export type IViewToBoMon = {
  id: number;
  tenBoMon: string;
  ngayThanhLap: Date;
  phongBan: string;
};

export type ICreateToBoMon = {
  maBoMon: string;
  tenBoMon: string;
  ngayThanhLap: Date;
  idPhongBan: number;
};

export type IUpdateToBoMon = ICreateToBoMon & {
  id: number;
};



