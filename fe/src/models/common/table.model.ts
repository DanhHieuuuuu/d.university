import { ETableColumnType } from '@/constants/e-table.consts';
import { TableColumnType, TagProps } from 'antd';
import { BaseButtonProps } from 'antd/es/button/button';

export type ITagInfo = {
  label: string;
  color?: TagProps['color'];
  className?: string;
};

export type IColumn<T> = TableColumnType<T> & {
  showOnConfig?: boolean;
  type?: ETableColumnType;
  getTagInfo?: (value: any, record?: T) => ITagInfo | null;
};

export type IAction = {
  label: string;
  tooltip?: string;
  command: Function;
  icon: React.ReactNode;
  color?: BaseButtonProps['color'];
  hidden?: (record: any) => boolean;
  permission?: string;
};
