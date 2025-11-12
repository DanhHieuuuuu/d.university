import { IQueryPaging } from '@models/common/model.common';

export type IFilterNotification = IQueryPaging & {
  isRead?: boolean;
  short?: boolean;
};

export type IViewNotification = {
  id: number;
  title: string;
  isRead: boolean;
  createdAt: Date | string;
};
