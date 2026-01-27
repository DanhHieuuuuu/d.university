'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Tag, Select } from 'antd';
import { SearchOutlined, SyncOutlined, EyeOutlined, RobotOutlined } from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getPagingReport, generateReportAction, getReportDetail, analyzeWithAI } from '@redux/feature/survey/surveyThunk';
import { resetReportStatus, setSelectedSurvey } from '@redux/feature/survey/surveySlice';

import { IQueryReport, IReportItem, IReportDetail } from '@models/survey/report.model';
import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { toast } from 'react-toastify';
import ReportDetailModal from './(dialog)/detail';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { withAuthGuard } from '@src/hoc/withAuthGuard';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { report } = useAppSelector((state) => state.surveyState);
  const { $list } = report;
  const { data: list, status, total: totalItem } = $list;

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedReport, setSelectedReport] = useState<IReportDetail | null>(null);

  const columns: IColumn<IReportItem>[] = [
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
    },
    {
      label: 'Phân tích AI',
      icon: <RobotOutlined />,
      command: async (record: IReportItem) => {
        try {
          await dispatch(analyzeWithAI(record.reportId)).unwrap();
          console.log('data', record)
          toast.success('Phân tích AI thành công!');
        } catch (error: any) {
          console.error('Lỗi phân tích AI:', error);
          toast.error('Không thể phân tích với AI');
        }
      },
      permission: PermissionCoreConst.SurveyButtonAIReportGenerate
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
      <Form form={form} layout="vertical">
        <div className="grid grid-cols-2 gap-3">
          <Form.Item<IQueryReport> name="Keyword">
            <Input placeholder="Nhập tên khảo sát" onChange={(e) => handleSearch(e)} />
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

export default withAuthGuard(Page, PermissionCoreConst.SurveyMenuReport);
