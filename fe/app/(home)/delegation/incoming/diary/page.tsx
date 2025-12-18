'use client';

import { useEffect, useState } from 'react';
import { Card } from 'antd';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getLogStatus } from '@redux/feature/delegation/delegationThunk';
import { ILogStatus, IQueryLogStatus } from '@models/delegation/delegation.model';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import AppTable from '@components/common/Table';
import { ReduxStatus } from '@redux/const';
import { IAction, IColumn } from '@models/common/table.model';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { getStatusName } from '@utils/status.helper';
import { formatDateTimeView } from '@utils/index';
const Page = () => {
  const dispatch = useAppDispatch();
  const { status, total: totalItem, listLogStatus } = useAppSelector((state) => state.delegationState);
  console.log('listLogStatus', listLogStatus);
  const columns: IColumn<ILogStatus>[] = [
    {
      key: 'stt',
      title: 'STT',
      align: 'center',
      render: (_: any, __: any, index: number) => index + 1
    },
    {
      key: 'createdBy',
      dataIndex: 'createdBy',
      title: 'Người thực hiện',
      align: 'center'
    },
    {
      key: 'oldStatus',
      dataIndex: 'oldStatus',
      title: 'Trạng thái cũ',
      render: (value: number) => getStatusName(value)
    },
    {
      key: 'newStatus',
      dataIndex: 'newStatus',
      title: 'Trạng thái mới',
      render: (value: number) => getStatusName(value)
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: 'Mô tả'
    },
    {
      key: 'reason',
      dataIndex: 'reason',
      title: 'Lý do'
    },
    {
      key: 'createdDate',
      dataIndex: 'createdDate',
      title: 'Thời gian',
      align: 'center',
      render: (value: string) => formatDateTimeView(value)
    }
  ];

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryLogStatus>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getLogStatus(newQuery));
    },
    triggerFirstLoad: true
  });
  useEffect(() => {
    dispatch(getLogStatus(query));
  }, [dispatch,query]);

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);
  return (
    <Card title="Nhật ký đoàn vào" className="h-full">
      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={listLogStatus}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuDelegation);
