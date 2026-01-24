import { IQueryPaging } from '@models/common/model.common';
export interface IViewGuestGroup {
  id: number;
  code: string;
  name: string;
  content: string;
  idPhongBan: number;
  phongBan: string;
  staffReceptionName: string;
  location?: string | null;
  idStaffReception: number;
  totalPerson: number;
  phoneNumber?: string | null;
  status: number;
  requestDate: string;
  receptionDate: string;
  totalMoney: number;

  delegationDetails?: IDetailDelegationIncoming[] | null;
  receptionTimes?: IReceptionTime[] | null;
  departmentSupports?: IDepartmentSupport[] | null;
}
export interface IDetailDelegationIncoming {
  id: number;
  code: string;
  firstName: string;
  lastName: string;
  yearOfBirth: number;
  phoneNumber: string;
  email: string;
  isLeader: boolean;
  delegationIncomingId: number;
}
export interface IReceptionTime {
  id: number;
  startDate: string;
  endDate: string;
  date: string;
  content: string;
  totalPerson: number;
  address: string;
  delegationIncomingId: number;
  delegationName: string;
  delegationCode: string;
  prepares?: IPrepare[] | null;
}
export interface IPrepare {
  id: number;
  name: string;
  description: string;
  money: number;
  receptionTimeId: number;
}
export interface IDepartmentSupport {
  id: number;
  departmentSupportId: number;
  departmentSupportName: string;
  delegationIncomingName: string;
  delegationIncomingId: number;
  content: string;
  supporters: ISupporter[];
}
export interface ISupporter {
  id: number;
  supporterId: number;
  supporterCode: string;
  departmentSupportId: number;
  departmentSupport: any | null;
}

export type IQueryGuestGroup = IQueryPaging & {
  name?: string;
  idPhongBan?: number;
  status?: number;
};
export type IQuerySupporter = IQueryPaging & {
  supporterCode?: string;
  departmentSupportId?: number;
};
export type IQueryDepartmentSupport = IQueryPaging & {
  departmentSupportId?: number;
};
export type IQueryLogStatus = IQueryPaging & {
  CreateDate?: string;
  CreatedByName?: string;
};
export type IQueryLogReceptionTime = IQueryPaging & {
  CreateDate?: string;
  CreatedByName?: string;
};
export type ICreateDoanVao = {
  code: string;
  name: string;
  content: string;
  idPhongBan: number;
  location?: string;
  idStaffReception: number;
  totalPerson: number;
  phoneNumber?: string;
  status: number;
  requestDate: string;
  receptionDate: string;
  totalMoney: number;
};
export type IUpdateDoanVao = ICreateDoanVao & {
  id: number;
};
export interface ILogStatus {
  oldStatus: number;
  newStatus: number;
  description: string;
  reason: string;
  createdBy: string;
  createdByName: string;
  createdDate: string;
}
export interface ILogReceptionTime {
  id: number;
  receptionTimeId: number;
  description: string;
  type: string;
  reason: string;
  createdBy: string;
  createdDate: string;
  createdByName: string;
}
export type ICreateReceptionTime = {
  startDate: number;
  endDate: number;
  date: number;
  content: string;
  totalPerson: number;
  address: string;
  delegationIncomingId: number;
};
export type ICreateReceptionTimeList = {
  items: ICreateReceptionTime[];
};
export interface IUpdateReceptionTime {
  id: number;
  delegationIncomingId: number;
  date: string;
  startDate: string;
  endDate: string;
  content?: string;
  totalPerson?: number;
  address?: string;
}
export interface IUpdateReceptionTimes {
  items: IUpdateReceptionTime[];
}

export type ICreateSupporter = {
  departmentSupportId: number;
  supporters: {
    supporterId: number;
    supporterCode: string;
  }[];
};

export type ICreateDepartment = {
  delegationIncomingId: number;
  content?: string;
  departmentSupportIds: number[];
};

export interface IUpdateStatus {
  idDelegation: number;
  oldStatus: number;
  action: string;
}
export interface IUpdateDepartmentSupport {
  departmentSupportId: number;
  delegationIncomingId: number;
  content: string;
  supporters: {
    supporterId: number;
    supporterCode: string;
  }[];
}
export interface ICreatePrepareItem {
  name: string;
  description: string;
  money: number;
  receptionTimeId: number;
}
export interface ICreatePrepare {
  items: ICreatePrepareItem[];
}
export interface IUpdatePrepare {
  items: IPrepare[];
}
export interface BaoCaoDoanVao {
  listId: number[];
  isExportAll: boolean;
}

export interface IStatistical {
  totalAll: number,
  byStatus: IStatisticalStatus[]
}
export interface IStatisticalStatus{
  status:number,
  total: number
}
export interface IDateOption {
  label: string;
  value: string;
}