'use client';

import React, { useMemo } from 'react';
import { Table, TableProps } from 'antd';
import { IColumn } from '@models/common/table.model';
import '@styles/table.style.scss';

interface SimpleTableProps<T> extends TableProps<T> {
  columns: IColumn<T>[];
  rowSelection?: TableProps<T>['rowSelection'];
  height?: number;
}

const SimpleTable = <T extends object>(props: SimpleTableProps<T>) => {
  const { columns, rowSelection, ...rest } = props;

  const indexColumn: IColumn<T> = useMemo(
    () => ({
      key: '__index',
      title: 'STT',
      width: 60,
      align: 'center',
      fixed: 'left',
      render: (_: any, __: T, index: number) => index + 1
    }),
    []
  );

  const finalColumns = useMemo(() => {
    return [indexColumn, ...columns];
  }, [indexColumn, columns]);

  return (
    <Table<T>
      size="small"
      tableLayout="fixed"
      columns={finalColumns}
      scroll={{ x: 'max-content', y: props.height ?? 370 }}
      rowSelection={rowSelection}
      {...rest}
    />
  );
};

export default SimpleTable;
