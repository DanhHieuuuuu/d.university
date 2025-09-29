import { TableColumnProps, TableColumnType } from 'antd';

export type IColumn<T> = TableColumnType<T> & {
  showOnConfig?: boolean;
};
