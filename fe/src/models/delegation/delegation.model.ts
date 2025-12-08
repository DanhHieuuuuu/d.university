import { IQueryPaging } from '@models/common/model.common';
export interface IViewGuestGroup {
  id: number;                 
  code: string;
  name: string;
  content: string;
  idPhongBan: number;
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
};
