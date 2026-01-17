'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Tag, Select, Modal } from 'antd';
import { SearchOutlined, SyncOutlined, CheckOutlined, CloseOutlined, EyeOutlined, ExclamationCircleOutlined } from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getPagingRequest, approveRequestAction, rejectRequestAction, getRequestById } from '@redux/feature/survey/surveyThunk';
import { setSelectedRequest, clearSelectedRequest } from '@redux/feature/survey/surveySlice';
import { requestStatusConst } from '@/constants/core/survey/requestStatus.const';
import { IQueryRequest, IViewRequest } from '@models/survey/request.model';

import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import CreateOrUpdateRequestModal from '../request/(dialog)/create-or-update';
import { toast } from 'react-toastify';

import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { isGranted } from '@hooks/isGranted';
import { withAuthGuard } from '@src/hoc/withAuthGuard';


const { Option } = Select;

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { request } = useAppSelector((state) => state.surveyState);
  const { $list } = request;
  const { data: list, status, total: totalItem } = $list;

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedRequest, setSelectedRequestState] = useState<IViewRequest | null>(null);
  const [isViewMode, setIsViewMode] = useState(false);

  const canApprove = isGranted(PermissionCoreConst.SurveyButtonRequestApprove);
  const canReject = isGranted(PermissionCoreConst.SurveyButtonRequestReject);
  
  const columns: IColumn<IViewRequest>[] = [
    {
      key: 'maYeuCau',
      dataIndex: 'maYeuCau',
      title: 'Mã yêu cầu'
    },
    {
      key: 'tenKhaoSatYeuCau',
      dataIndex: 'tenKhaoSatYeuCau',
      title: 'Tên khảo sát'
    },
    {
      key: 'moTa',
      dataIndex: 'moTa',
      title: 'Mô tả'
    },
    {
      key: 'thoiGianBatDau',
      dataIndex: 'thoiGianBatDau',
      title: 'Thời gian bắt đầu',
      render: (value) => {
        const date = formatDateView(value);
        return <p>{date}</p>;
      }
    },
    {
      key: 'thoiGianKetThuc',
      dataIndex: 'thoiGianKetThuc',
      title: 'Thời gian kết thúc',
      render: (value) => {
        const date = formatDateView(value);
        return <p>{date}</p>;
      }
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
      width: 150,
      render: (value) => {
        const statusName = requestStatusConst.getName(value);
        const colors: Record<string, string> = {
          'Bản nháp': 'default',
          'Chờ duyệt': 'orange',
          'Đã duyệt': 'green',
          'Từ chối': 'red',
          'Hủy': 'gray'
        };
        return <Tag color={colors[statusName] || 'default'}>{statusName || 'Chưa có'}</Tag>;
      }
    }
  ];

  // Actions
  const actions: IAction[] = [
    {
      label: 'Xem chi tiết',
      icon: <EyeOutlined />,
      command: async (record: IViewRequest) => {
        try {
          const result = await dispatch(getRequestById(record.id)).unwrap();
          setSelectedRequestState(result);
          dispatch(setSelectedRequest(result));
          setIsViewMode(true);
          setIsModalOpen(true);
        } catch (error: any) {
          console.error('Lỗi khi lấy chi tiết request:', error);
          toast.error('Không thể tải chi tiết yêu cầu');
        }
      }
    },
    {
      label: 'Duyệt',
      icon: <CheckOutlined />,
      command: (record: IViewRequest) => {
        Modal.confirm({
          title: 'Xác nhận duyệt',
          icon: <ExclamationCircleOutlined />,
          centered: true,
          content: (
            <div>
              <p>Bạn có chắc chắn muốn duyệt yêu cầu "{record.tenKhaoSatYeuCau}"?</p>
            </div>
          ),
          okText: 'Duyệt',
          okType: 'primary',
          cancelText: 'Hủy',
          onOk: async () => {
            try {
              await dispatch(approveRequestAction(record.id)).unwrap();
              toast.success('Duyệt yêu cầu thành công');
              dispatch(getPagingRequest(query));
            } catch (error: any) {
              toast.error(error?.message || 'Duyệt yêu cầu thất bại');
            }
          }
        });
      },
      hidden: (record: IViewRequest) => 
        !canApprove || 
        record.trangThai !== requestStatusConst.PENDING
    },
    {
      label: 'Từ chối',
      icon: <CloseOutlined />,
      color: 'danger',
      command: (record: IViewRequest) => {
        let rejectReason = '';
        Modal.confirm({
          title: 'Từ chối yêu cầu khảo sát "' + record.tenKhaoSatYeuCau + '"',
          icon: <ExclamationCircleOutlined />,
          centered: true,
          content: (
            <div>
              <Input.TextArea
                rows={4}
                placeholder="Nhập lý do từ chối..."
                onChange={(e) => (rejectReason = e.target.value)}
                style={{ marginTop: 12 }}
              />
            </div>
          ),
          okText: 'Từ chối',
          okType: 'danger',
          cancelText: 'Hủy',
          onOk: async () => {
            if (!rejectReason || !rejectReason.trim()) {
              toast.error('Vui lòng nhập lý do từ chối');
              return Promise.reject();
            }
            try {
              await dispatch(rejectRequestAction({ id: record.id, reason: rejectReason })).unwrap();
              toast.success('Từ chối yêu cầu thành công');
              dispatch(getPagingRequest(query));
            } catch (error: any) {
              toast.error(error?.message || 'Từ chối yêu cầu thất bại');
              return Promise.reject();
            }
          }
        });
      },
      hidden: (record: IViewRequest) => 
        !canReject || 
        record.trangThai !== requestStatusConst.PENDING
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryRequest>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: '',
    },
    onQueryChange: (newQuery) => {
      dispatch(getPagingRequest(newQuery));
    },
    triggerFirstLoad: true
  });

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  const handleStatusFilter = (value: number) => {
    onFilterChange({ trangThai: value });
  };

  return (
    <Card
      title="Quản lý yêu cầu khảo sát"
      className="h-full"
    >
      <Form form={form} layout="vertical">
        <div className="grid grid-cols-3 gap-3">
          <Form.Item<IQueryRequest> name="Keyword">
            <Input placeholder="Mã yêu cầu/tên khảo sát" onChange={(e) => handleSearch(e)} />
          </Form.Item>
          <Form.Item name="trangThai">
            <Select placeholder="Chọn trạng thái" allowClear onChange={handleStatusFilter}>
              {requestStatusConst.list.map((status) => (
                <Option key={status.value} value={status.value}>
                  {status.name}
                </Option>
              ))}
            </Select>
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
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />
      <CreateOrUpdateRequestModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        request={selectedRequest}
        isViewMode={isViewMode}
        onSuccess={() => {
          dispatch(getPagingRequest(query));
          dispatch(clearSelectedRequest());
        }}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.SurveyMenuRequestApproval);
