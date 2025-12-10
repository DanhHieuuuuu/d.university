import { ETableColumnType } from '@/constants/e-table.consts';
import { TableColumnType } from 'antd';
import { BaseButtonProps } from 'antd/es/button/button';

export type IColumn<T> = TableColumnType<T> & {
  showOnConfig?: boolean;
  type?: ETableColumnType;
  getTagInfo?: Function;
};

export type IAction = {
  label: string;
  tooltip?: string;
  command: Function;
  icon: React.ReactNode;
  color?: BaseButtonProps['color'];
  
};
