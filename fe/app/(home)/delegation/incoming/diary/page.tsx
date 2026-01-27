'use client';

import { useEffect, useState } from 'react';
import { Card, Form, Select, Tabs } from 'antd';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getLogStatus, getLogReceptionTime, getListNhanSu } from '@redux/feature/delegation/delegationThunk';
import {
  ILogStatus,
  IQueryLogStatus,
  ILogReceptionTime,
  IQueryLogReceptionTime
} from '@models/delegation/delegation.model';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import AppTable from '@components/common/Table';
import { ReduxStatus } from '@redux/const';
import { IColumn } from '@models/common/table.model';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { formatDateTimeView } from '@utils/index';
import { DelegationStatusConst } from '../../../../../constants/core/delegation/delegation-status.consts';
import { ETableColumnType } from '@/constants/e-table.consts';
import { DelegationIncomingService } from '@services/delegation/delegationIncoming.service';

const { TabPane } = Tabs;

const Page = () => {
  const dispatch = useAppDispatch();

  // Lấy data từ redux
  const {
    status,
    total: totalLogStatus,
    total: totalReception,
    listLogStatus,
    listLogReceptionTime,
    listNhanSu
  } = useAppSelector((state) => state.delegationState);
  const [createdDates, setCreatedDates] = useState<{ label: string; value: string }[]>([]);
  const [activeTab, setActiveTab] = useState<'status' | 'reception'>('status');

  // Column cho logStatus
  const logStatusColumns: IColumn<ILogStatus>[] = [
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
    { key: 'createdByName', dataIndex: 'createdByName', title: 'Người thực hiện', align: 'center', width: 130 },
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
    triggerFirstLoad: false
  });

  const {
    query: receptionQuery,
    pagination: receptionPagination,
    onFilterChange: onReceptionFilter
  } = usePaginationWithFilter<IQueryLogReceptionTime>({
    total: totalReception,
    initialQuery: { PageIndex: 1, PageSize: 10, Keyword: '' },
    onQueryChange: (newQuery) => dispatch(getLogReceptionTime(newQuery)),
    triggerFirstLoad: false
  });

  const { debounced: handleDebouncedStatusSearch } = useDebouncedCallback((value: string) => {
    onStatusFilter({ Keyword: value });
  }, 500);

  const { debounced: handleDebouncedReceptionSearch } = useDebouncedCallback((value: string) => {
    onReceptionFilter({ Keyword: value });
  }, 500);

  useEffect(() => {
    if (activeTab === 'status') {
      dispatch(getLogStatus(statusQuery));
    }
  }, [dispatch, statusQuery, activeTab]);

  useEffect(() => {
    if (activeTab === 'reception') {
      dispatch(getLogReceptionTime(receptionQuery));
    }
  }, [dispatch, receptionQuery, activeTab]);

  useEffect(() => {
    dispatch(getListNhanSu());
  }, [dispatch]);
  useEffect(() => {
    const fetchDates = async () => {
      try {
        const res = await DelegationIncomingService.getListCreatedDate();
        setCreatedDates(res.data);
      } catch {}
    };

    fetchDates();
  }, []);

  return (
    <Card title="Nhật ký đoàn vào" className="h-full">
      <Tabs
        defaultActiveKey="status"
        onChange={(key) => setActiveTab(key as 'status' | 'reception')}
        tabBarExtraContent={
          <div className="flex gap-2">
            {/* Chọn ngày tạo */}
            <Form.Item className="!mb-0 w-[220px]">
              <Select
                showSearch
                placeholder="Chọn ngày tạo"
                allowClear
                options={createdDates}
                onChange={(value) => {
                  if (activeTab === 'status') {
                    onStatusFilter({ CreateDate: value || undefined });
                  } else {
                    onReceptionFilter({ CreateDate: value || undefined });
                  }
                }}
              />
            </Form.Item>

            {/* Chọn nhân sự */}
            <Form.Item className="!mb-0 w-[220px]">
              <Select
                showSearch
                placeholder="Chọn người thực hiện"
                optionFilterProp="label"
                allowClear
                options={listNhanSu.map((ns) => ({
                  label: ns.tenNhanSu,
                  value: ns.tenNhanSu
                }))}
                onChange={(value) => {
                  if (activeTab === 'status') {
                    onStatusFilter({ CreatedByName: value || undefined });
                  } else {
                    onReceptionFilter({ CreatedByName: value || undefined });
                  }
                }}
              />
            </Form.Item>
          </div>
        }
      >
        <TabPane tab="Nhật ký đoàn vào" key="status">
          <AppTable
            loading={activeTab === 'status' && status === ReduxStatus.LOADING}
            rowKey="id"
            columns={logStatusColumns}
            dataSource={listLogStatus}
            pagination={{ position: ['bottomRight'], ...statusPagination }}
            data-permission={PermissionCoreConst.CoreTableLog}
          />
        </TabPane>
        <TabPane tab="Nhật ký thời gian tiếp đoàn" key="reception">
          <AppTable
            loading={activeTab === 'reception' && status === ReduxStatus.LOADING}
            rowKey="id"
            columns={receptionColumns}
            dataSource={listLogReceptionTime}
            pagination={{ position: ['bottomRight'], ...receptionPagination }}
            data-permission={PermissionCoreConst.CoreTableLog}
          />
        </TabPane>
        <Form.Item name="status" className="!mb-0 w-[200px]">
          <Select
            data-permission={PermissionCoreConst.CoreButtonSearchDoanVao}
            placeholder="Chọn trạng thái"
            allowClear
          />
        </Form.Item>
      </Tabs>
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuDelegation);
