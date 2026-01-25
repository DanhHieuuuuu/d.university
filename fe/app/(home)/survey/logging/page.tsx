'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, DatePicker, Tag } from 'antd';
import { SyncOutlined } from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getPagingSurveyLog } from '@redux/feature/survey/surveyThunk';

import { IQuerySurveyLog, IViewSurveyLog } from '@models/survey/survey.model';
import SimpleTable from '@components/dhieu-custom/SimpleTable';
import { IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import dayjs from 'dayjs';

const { RangePicker } = DatePicker;

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { surveyLog } = useAppSelector((state) => state.surveyState);
  const { $list } = surveyLog;
  const { data: logs, status, total: totalItem } = $list;

  const columns: IColumn<IViewSurveyLog>[] = [
    {
      key: 'tenNguoiThaoTac',
      dataIndex: 'tenNguoiThaoTac',
      title: 'Người thực hiện',
      width: 180,
      render: (value) => value || 'Hệ thống'
    },
    {
      key: 'loaiHanhDong',
      dataIndex: 'loaiHanhDong',
      title: 'Hành động',
      width: 150,
      render: (value) => {
        const colorMap: Record<string, string> = {
          'Create': 'green',
          'Update': 'blue',
          'Cancel': 'red',
          'Submit': 'cyan',
          'CancelSubmit': 'orange',
          'Approve': 'green',
          'Reject': 'red',
          'Open': 'blue',
          'Close': 'orange',
          'GenerateReport': 'purple',
          'UpdateReport': 'geekblue'
        };
        return <Tag color={colorMap[value] || 'default'}>{value}</Tag>;
      }
    },
    {
      key: 'moTa',
      dataIndex: 'moTa',
      title: 'Mô tả',
      width: 400,
      ellipsis: false,
      render: (value) => <div style={{ whiteSpace: 'normal', wordBreak: 'break-word' }}>{value}</div>
    },
    // {
    //   key: 'tenBang',
    //   dataIndex: 'tenBang',
    //   title: 'Bảng',
    //   render: (value) => <Tag>{value}</Tag>
    // },
    {
      key: 'idDoiTuong',
      dataIndex: 'idDoiTuong',
      title: 'Mã đối tượng',
      width: 150
    },
    {
      key: 'createdAt',
      dataIndex: 'createdAt',
      title: 'Thời gian',
      width: 180,
      render: (value) => formatDateView(value)
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQuerySurveyLog>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getPagingSurveyLog(newQuery));
    },
    triggerFirstLoad: true
  });

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  const handleDateRangeChange = (dates: any) => {
    if (dates) {
      onFilterChange({
        tuNgay: dates[0]?.toISOString(),
        denNgay: dates[1]?.toISOString()
      });
    } else {
      onFilterChange({ tuNgay: undefined, denNgay: undefined });
    }
  };

  return (
    <Card title="Nhật ký hoạt động khảo sát" className="h-full">
      <Form form={form} layout="vertical">
        <div className="grid grid-cols-3 gap-3">
          <Form.Item<IQuerySurveyLog> name="Keyword">
            <Input placeholder="Người thực hiện, mô tả" onChange={(e) => handleSearch(e)} />
          </Form.Item>
          <Form.Item name="dateRange">
            <RangePicker
              style={{ width: '100%' }}
              placeholder={['Từ ngày', 'Đến ngày']}
              onChange={handleDateRangeChange}
              format="DD/MM/YYYY"
            />
          </Form.Item>
          <Form.Item colon={false}>
            <Button
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                resetFilter();
              }}
            >
              Tải lại
            </Button>
          </Form.Item>
        </div>
      </Form>

      <SimpleTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={logs}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.SurveyMenuLogging);

