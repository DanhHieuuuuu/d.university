'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Tag, Select, Tooltip, Modal } from 'antd';
import {
  SearchOutlined,
  SyncOutlined,
  PlayCircleOutlined,
  StopOutlined,
  EyeOutlined,
  BarChartOutlined,
  ExclamationCircleOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import {
  getPagingSurvey,
  openSurveyAction,
  closeSurveyAction,
  getSurveyById,
  generateReportAction
} from '@redux/feature/survey/surveyThunk';
import { setSelectedSurvey, clearSelectedSurvey } from '@redux/feature/survey/surveySlice';
import { IQuerySurvey, IViewSurvey, ISurveyDetail } from '@models/survey/survey.model';
import { surveyStatusConst } from '@/constants/core/survey/surveyStatus.const';
import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateTimeView } from '@utils/index';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import SurveyDetailModal from './(dialog)/detail';
import { toast } from 'react-toastify';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { withAuthGuard } from '@src/hoc/withAuthGuard';

const { Option } = Select;

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { survey } = useAppSelector((state) => state.surveyState);
  const { $list } = survey;
  const { data: list, status, total: totalItem } = $list;

  const [isModalOpen, setIsModalOpen] = useState(false);

  const [selectedSurvey, setSelectedSurveyState] = useState<ISurveyDetail | null>(null);

  const columns: IColumn<IViewSurvey>[] = [
    {
      key: 'maKhaoSat',
      dataIndex: 'maKhaoSat',
      title: 'Mã khảo sát',
      width: 120
    },
    {
      key: 'tenKhaoSat',
      dataIndex: 'tenKhaoSat',
      title: 'Tên khảo sát',
      width: 250
    },
    {
      key: 'moTa',
      dataIndex: 'moTa',
      title: 'Mô tả',
      width: 200,
      ellipsis: {
        showTitle: false
      },
      render: (address) => (
        <Tooltip placement="topLeft" title={address}>
          {address}
        </Tooltip>
      )
    },
    {
      key: 'thoiGianBatDau',
      dataIndex: 'thoiGianBatDau',
      title: 'TG Bắt đầu',
      width: 150,
      render: (value) => formatDateTimeView(value)
    },
    {
      key: 'thoiGianKetThuc',
      dataIndex: 'thoiGianKetThuc',
      title: 'TG Kết thúc',
      width: 150,
      render: (value) => formatDateTimeView(value)
    },
    {
      key: 'status',
      dataIndex: 'status',
      title: 'Trạng thái',
      width: 120,
      align: 'center',
      render: (value, record) => {
        let color = 'default';
        if (value === surveyStatusConst.OPEN) color = 'green';
        else if (value === surveyStatusConst.CLOSE) color = 'red';
        else if (value === surveyStatusConst.COMPLETE) color = 'blue';
        return <Tag color={color}>{record.statusName || 'N/A'}</Tag>;
      }
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Xem chi tiết',
      icon: <EyeOutlined />,
      command: async (record: IViewSurvey) => {
        try {
          const result = await dispatch(getSurveyById(record.id)).unwrap();
          setSelectedSurveyState(result as ISurveyDetail);
          dispatch(setSelectedSurvey(result));
          setIsModalOpen(true);
        } catch (error: any) {
          console.error('Lỗi lấy chi tiết:', error);
          toast.error('Không thể tải chi tiết khảo sát');
        }
      }
    },
    {
      label: 'Mở khảo sát',
      icon: <PlayCircleOutlined />,
      command: (record: IViewSurvey) => {
        Modal.confirm({
          title: 'Xác nhận mở khảo sát: ' + record.tenKhaoSat,
          icon: <ExclamationCircleOutlined />,
          centered: true,
          okText: 'Mở khảo sát',
          okType: 'primary',
          cancelText: 'Hủy',
          onOk: async () => {
            try {
              await dispatch(openSurveyAction(record.id)).unwrap();
              toast.success('Đã mở khảo sát');
              dispatch(getPagingSurvey(query));
            } catch (error: any) {
              toast.error(error?.message || 'Lỗi mở khảo sát');
            }
          }
        });
      },
      permission: PermissionCoreConst.SurveyButtonSurveyOpen,
      hidden: (record: IViewSurvey) => 
        record.status !== surveyStatusConst.CLOSE && record.status !== surveyStatusConst.PAUSE
    },
    {
      label: 'Đóng khảo sát',
      icon: <StopOutlined />,
      command: (record: IViewSurvey) => {
        Modal.confirm({
          title: 'Xác nhận đóng khảo sát: ' + record.tenKhaoSat,
          icon: <ExclamationCircleOutlined />,
          centered: true,
          okText: 'Đóng khảo sát',
          okType: 'danger',
          cancelText: 'Hủy',
          onOk: async () => {
            try {
              await dispatch(closeSurveyAction(record.id)).unwrap();
              toast.success('Đã đóng khảo sát');
              dispatch(getPagingSurvey(query));
            } catch (error: any) {
              toast.error(error?.message || 'Lỗi đóng khảo sát');
            }
          }
        });
      },
      permission: PermissionCoreConst.SurveyButtonSurveyClose,
      hidden: (record: IViewSurvey) => 
        record.status !== surveyStatusConst.OPEN
    },
    {
      label: 'Tạo báo cáo',
      icon: <BarChartOutlined />,
      command: (record: IViewSurvey) => {
        Modal.confirm({
          title: 'Xác nhận tạo báo cáo: ' + record.tenKhaoSat,
          icon: <ExclamationCircleOutlined />,
          centered: true,
          okText: 'Tạo báo cáo',
          okType: 'primary',
          cancelText: 'Hủy',
          onOk: async () => {
            try {
              await dispatch(generateReportAction(record.id)).unwrap();
              toast.success('Đã tạo báo cáo thành công');
            } catch (error: any) {
              toast.error(error?.message || 'Lỗi tạo báo cáo');
            }
          }
        });
      },
      permission: PermissionCoreConst.SurveyButtonReportGenerate,
      hidden: (record: IViewSurvey) =>
        record.status !== surveyStatusConst.PAUSE && record.status !== surveyStatusConst.COMPLETE
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQuerySurvey>({
    total: totalItem,
    initialQuery: { PageIndex: 1, PageSize: 10, Keyword: '' },
    onQueryChange: (newQuery) => dispatch(getPagingSurvey(newQuery)),
    triggerFirstLoad: true
  });

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  return (
    <Card title="Quản lý khảo sát (Survey)" className="h-full">
      <Form form={form} layout="vertical" className='mb-4'>
        <div className="grid grid-cols-3 gap-3">
          <Form.Item name="Keyword">
            <Input placeholder="Mã/Tên khảo sát" onChange={(e) => handleDebouncedSearch(e.target.value)} prefix={<SearchOutlined />} />
          </Form.Item>
          <Form.Item name="status">
            <Select placeholder="Chọn trạng thái" allowClear onChange={(val) => onFilterChange({ status: val })}>
              {surveyStatusConst.list.map((s) => (
                <Option key={s.value} value={s.value}>
                  {s.name}
                </Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item colon={false}>
            <Button icon={<SyncOutlined />} onClick={() => { form.resetFields(); resetFilter(); }}>Tải lại</Button>
          </Form.Item>
        </div>
      </Form>

      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />

      <SurveyDetailModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        survey={selectedSurvey}
        onClose={() => {
          dispatch(clearSelectedSurvey());
        }}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.SurveyMenuManagement);
