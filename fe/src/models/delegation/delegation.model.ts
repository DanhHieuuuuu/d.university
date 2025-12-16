import { IQueryPaging } from '@models/common/model.common';
export interface IViewGuestGroup {
  id: number;                 
  code: string;
  name: string;
  content: string;
  idPhongBan: number;
  phongBan:string;
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
  delegationName:string;
  delegationCode:string;
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
  delegationIncomingId: number;    
  content: string;                  
  supporters?: ISupporter[] | null; 
}
export interface ISupporter {
  id: number;
  name: string;
  position: string;
  phoneNumber: string;
  departmentSupportId: number;
}
export type IQueryGuestGroup = IQueryPaging & {
  name?: string;
  idPhongBan?: number;
  status?: number;
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
  oldStatus:number,
  newStatus:number,
  description:string,
  reason:string,
  createdBy:string,
  createdDate:string
}