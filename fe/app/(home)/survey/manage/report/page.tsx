'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Tag, Select } from 'antd';
import { SearchOutlined, SyncOutlined, EyeOutlined } from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getPagingReport, generateReportAction, getReportDetail } from '@redux/feature/survey/surveyThunk';
import { resetReportStatus, setSelectedSurvey } from '@redux/feature/survey/surveySlice';

import { IQueryReport, IReportItem, IReportDetail } from '@models/survey/report.model';
import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { toast } from 'react-toastify';
import ReportDetailModal from './(dialog)/detail';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { report } = useAppSelector((state) => state.surveyState);
  const { $list } = report;
  const { data: list, status, total: totalItem } = $list;

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedReport, setSelectedReport] = useState<IReportDetail | null>(null);

  const columns: IColumn<IReportItem>[] = [
    {
      key: 'stt',
      dataIndex: 'stt',
      title: 'STT',
      showOnConfig: false,
      render: (text, record, index) => index + 1
    },
    // {
    //   key: 'surveyId',
    //   dataIndex: 'surveyId',
    //   title: 'ID Khảo sát'
    // },
    {
      key: 'tenKhaoSat',
      dataIndex: 'tenKhaoSat',
      title: 'Tên khảo sát'
    },
    {
      key: 'totalParticipants',
      dataIndex: 'totalParticipants',
      title: 'Số người tham gia'
    },
    {
      key: 'averageScore',
      dataIndex: 'averageScore',
      title: 'Điểm trung bình',
      render: (value) => `${value?.toFixed(2) || 0}`
    },
    {
      key: 'generatedAt',
      dataIndex: 'generatedAt',
      title: 'Thời gian tạo',
      render: (value) => {
        const date = formatDateView(value);
        return <p>{date}</p>;
      }
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Xem chi tiết',
      icon: <EyeOutlined />,
      command: async (record: IReportItem) => {
        try {
          const result = await dispatch(getReportDetail(record.reportId)).unwrap();
          setSelectedReport(result);
          setIsModalOpen(true);
        } catch (error: any) {
          console.error('Lỗi lấy chi tiết báo cáo:', error);
          toast.error('Không thể tải chi tiết báo cáo');
        }
      }
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryReport>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getPagingReport(newQuery));
    },
    triggerFirstLoad: true
  });

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };


  return (
    <Card title="Danh sách báo cáo khảo sát" className="h-full">
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2 gap-4">
          <Form.Item<IQueryReport> label="Tìm kiếm:" name="Keyword">
            <Input placeholder="Nhập tên khảo sát" onChange={(e) => handleSearch(e)} />
          </Form.Item>
        </div>
        <Form.Item>
          <div className="flex flex-row justify-center space-x-2">
            <Button type="primary" htmlType="submit" icon={<SearchOutlined />}>
              Tìm kiếm
            </Button>
            <Button
              color="default"
              variant="filled"
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                resetFilter();
              }}
            >
              Tải lại
            </Button>
          </div>
        </Form.Item>
      </Form>

      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="reportId"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />

      <ReportDetailModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        report={selectedReport}
        onClose={() => {
            setSelectedReport(null);
        }}
      />
    </Card>
  );
};

export default Page;
