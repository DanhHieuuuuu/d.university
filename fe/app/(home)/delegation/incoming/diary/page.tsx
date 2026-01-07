'use client';

import { useEffect } from 'react';
import { Card, Tabs } from 'antd';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getLogStatus, getLogReceptionTime } from '@redux/feature/delegation/delegationThunk';
import { ILogStatus, IQueryLogStatus, ILogReceptionTime } from '@models/delegation/delegation.model';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import AppTable from '@components/common/Table';
import { ReduxStatus } from '@redux/const';
import { IColumn } from '@models/common/table.model';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { formatDateTimeView } from '@utils/index';
import { DelegationStatusConst } from '../../consts/delegation-status.consts';
import { ETableColumnType } from '@/constants/e-table.consts';

const { TabPane } = Tabs;

const Page = () => {
  const dispatch = useAppDispatch();

  // Lấy data từ redux
  const { status, total: totalLogStatus, listLogStatus } = useAppSelector((state) => state.delegationState);
  const { total: totalReception, listLogReceptionTime } = useAppSelector((state) => state.delegationState); // giả sử state chung

  // Column cho logStatus
  const logStatusColumns: IColumn<ILogStatus>[] = [
    { key: 'stt', width: 60, title: 'STT', align: 'center', render: (_: any, __: any, index: number) => index + 1 },
    { key: 'createdByName', dataIndex: 'createdByName', title: 'Người thực hiện', align: 'center', width: 150 },
    { key: 'description', dataIndex: 'description', title: 'Mô tả', align: 'left', width: 200 },
    {
      key: 'oldStatus',
      dataIndex: 'oldStatus',
      title: 'Trạng thái cũ',
      width: 160,
      type: ETableColumnType.STATUS,
      getTagInfo: (val: number) => DelegationStatusConst.getTag(val)
    },
    {
      key: 'newStatus',
      dataIndex: 'newStatus',
      title: 'Trạng thái mới',
      width: 160,
      type: ETableColumnType.STATUS,
      getTagInfo: (val: number) => DelegationStatusConst.getTag(val)
    },
    { key: 'reason', dataIndex: 'reason', title: 'Lý do' },
    {
      key: 'createdDate',
      dataIndex: 'createdDate',
      title: 'Thời gian',
      align: 'center',
      render: (value: string) => formatDateTimeView(value)
    }
  ];

  // Column cho logReceptionTime
  const receptionColumns: IColumn<ILogReceptionTime>[] = [
    { key: 'stt', fixed: 'left', width: 60, title: 'STT', align: 'center', render: (_: any, __: any, index: number) => index + 1 },
    { key: 'createdByName', dataIndex: 'createdByName', title: 'Người thực hiện', align: 'center', width: 150 },
    { key: 'type', dataIndex: 'type', title: 'Loại', align: 'center' },
    { key: 'description', dataIndex: 'description', title: 'Mô tả', align: 'left', width: 250 },
    { key: 'reason', dataIndex: 'reason', title: 'Lý do' },
    {
      key: 'createdDate',
      dataIndex: 'createdDate',
      title: 'Thời gian',
      align: 'center',
      render: (value: string) => formatDateTimeView(value)
    }
  ];

  // Pagination & query
  const {
    query: statusQuery,
    pagination: statusPagination,
    onFilterChange: onStatusFilter
  } = usePaginationWithFilter<IQueryLogStatus>({
    total: totalLogStatus,
    initialQuery: { PageIndex: 1, PageSize: 10, Keyword: '' },
    onQueryChange: (newQuery) => dispatch(getLogStatus(newQuery)),
    triggerFirstLoad: true
  });

  const {
    query: receptionQuery,
    pagination: receptionPagination,
    onFilterChange: onReceptionFilter
  } = usePaginationWithFilter<IQueryLogStatus>({
    total: totalReception,
    initialQuery: { PageIndex: 1, PageSize: 10, Keyword: '' },
    onQueryChange: (newQuery) => dispatch(getLogReceptionTime(newQuery)),
    triggerFirstLoad: true
  });

  const { debounced: handleDebouncedStatusSearch } = useDebouncedCallback((value: string) => {
    onStatusFilter({ Keyword: value });
  }, 500);

  const { debounced: handleDebouncedReceptionSearch } = useDebouncedCallback((value: string) => {
    onReceptionFilter({ Keyword: value });
  }, 500);

  useEffect(() => {
    dispatch(getLogStatus(statusQuery));
    dispatch(getLogReceptionTime(receptionQuery));
  }, [dispatch, statusQuery, receptionQuery]);

  return (
    <Card title="Nhật ký đoàn vào" className="h-full">
      <Tabs defaultActiveKey="1">
        <TabPane tab="Nhật ký đoàn vào" key="1">
          <AppTable
            loading={status === ReduxStatus.LOADING}
            rowKey="id"
            columns={logStatusColumns}
            dataSource={listLogStatus}
            pagination={{ position: ['bottomRight'], ...statusPagination }}
            scroll={{ x: 'max-content', y: 'calc(100vh - 370px)' }}
          />
        </TabPane>
        <TabPane tab="Nhật ký thời gian tiếp đoàn" key="2">
          <AppTable
            loading={status === ReduxStatus.LOADING}
            rowKey="id"
            columns={receptionColumns}
            dataSource={listLogReceptionTime}
            pagination={{ position: ['bottomRight'], ...receptionPagination }}
            scroll={{ x: 'max-content', y: 'calc(100vh - 370px)' }}
          />
        </TabPane>
      </Tabs>
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuDelegation);
