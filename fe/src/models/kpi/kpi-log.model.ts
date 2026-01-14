import { IQueryPaging } from "@models/common/model.common";

export interface KpiLogStatusDto {
  id: number;
  oldStatus?: number;
  newStatus?: number;
  description?: string;
  reason?: string;
  capKpi?: number;
  createdBy: string;
  createdByName: string;
  createdDate: string;
}

export type IQueryKpiLogStatus = IQueryPaging & {
  kpiId?: number;
  capKpi?: number;
}

export interface KpiLogStatusResponse {
  items: KpiLogStatusDto[];
  totalItem: number;
  summary?: any;
}