'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Tag, Select, Modal } from 'antd';
import { PlusOutlined, SearchOutlined, SyncOutlined, EditOutlined, CheckOutlined, CloseOutlined, EyeOutlined, ExclamationCircleOutlined } from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getPagingRequest, approveRequestAction, rejectRequestAction, submitRequestAction, cancelSubmitRequestAction, removeRequest, getRequestById } from '@redux/feature/survey/surveyThunk';
import { resetRequestStatus, setSelectedRequest, clearSelectedRequest } from '@redux/feature/survey/surveySlice';

import { IQueryRequest, IViewRequest } from '@models/survey/request.model';

import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import CreateOrUpdateRequestModal from './(dialog)/create-or-update';
import { toast } from 'react-toastify';
import { requestStatusConst } from '../../const/requestStatus.const';

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

  const onClickAdd = () => {
    setSelectedRequestState(null);
    setIsViewMode(false);
    setIsModalOpen(true);
  };

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
      label: 'Sửa',
      icon: <EditOutlined />,
      command: async (record: IViewRequest) => {
        try {
          const result = await dispatch(getRequestById(record.id)).unwrap();
          setSelectedRequestState(result);
          dispatch(setSelectedRequest(result));
          setIsViewMode(false);
          setIsModalOpen(true);
        } catch (error: any) {
          console.error('Lỗi khi lấy chi tiết request:', error);
          toast.error('Không thể tải chi tiết yêu cầu');
        }
      },
      hidden: (record: IViewRequest) => record.trangThai !== requestStatusConst.DRAFT && record.trangThai !== requestStatusConst.REJECTED
    },
    {
      label: 'Gửi duyệt',
      icon: <CheckOutlined />,
      command: (record: IViewRequest) => {
        dispatch(submitRequestAction(record.id))
          .unwrap()
          .then(() => {
            toast.success('Gửi duyệt thành công');
            dispatch(getPagingRequest(query));
          })
          .catch((err) => {
            console.error(err);
            toast.error('Gửi duyệt thất bại');
          });
      },
      hidden: (record: IViewRequest) => record.trangThai !== requestStatusConst.DRAFT && record.trangThai !== requestStatusConst.REJECTED
    },
    {
      label: 'Hủy gửi duyệt',
      icon: <CloseOutlined />,
      command: (record: IViewRequest) => {
        dispatch(cancelSubmitRequestAction(record.id))
          .unwrap()
          .then(() => {
            toast.success('Hủy gửi duyệt thành công');
            dispatch(getPagingRequest(query));
          })
          .catch((err) => {
            console.error(err);
            toast.error('Hủy gửi duyệt thất bại');
          });
      },
      hidden: (record: IViewRequest) => record.trangThai !== requestStatusConst.PENDING // Chỉ cho phép khi chờ duyệt
    },
    {
      label: 'Duyệt',
      icon: <CheckOutlined />,
      command: (record: IViewRequest) => {
        dispatch(approveRequestAction(record.id))
          .unwrap()
          .then(() => {
            toast.success('Duyệt yêu cầu thành công');
            dispatch(getPagingRequest(query));
          })
          .catch((err) => {
            console.error(err);
            toast.error('Duyệt yêu cầu thất bại');
          });
      },
      hidden: (record: IViewRequest) => record.trangThai !== requestStatusConst.PENDING // Chỉ cho phép khi chờ duyệt
    },
    {
      label: 'Từ chối',
      icon: <CloseOutlined />,
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
      hidden: (record: IViewRequest) => record.trangThai !== requestStatusConst.PENDING
    },
    {
      label: 'Xóa',
      icon: <CloseOutlined />,
      color: 'danger',
      command: (record: IViewRequest) => {
        Modal.confirm({
          title: 'Xác nhận xóa',
          icon: <ExclamationCircleOutlined />,
          centered: true,
          content: (
            <div>
              <p>Bạn có chắc chắn muốn xóa yêu cầu khảo sát này không?</p>
              <p style={{ marginTop: 8 }}>
                <strong>Tên yêu cầu:</strong> {record.tenKhaoSatYeuCau}
              </p>
            </div>
          ),
          okText: 'Xóa',
          okType: 'danger',
          cancelText: 'Hủy',
          onOk: async () => {
            try {
              await dispatch(removeRequest(record.id)).unwrap();
              toast.success('Xóa yêu cầu thành công');
              dispatch(getPagingRequest(query));
            } catch (error: any) {
              toast.error(error?.message || 'Xóa yêu cầu thất bại');
            }
          }
        });
      },
      hidden: (record: IViewRequest) => record.trangThai !== requestStatusConst.DRAFT && record.trangThai !== requestStatusConst.REJECTED
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryRequest>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: '',
      trangThai: requestStatusConst.DRAFT
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
      title="Danh sách yêu cầu khảo sát"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal" initialValues={{ trangThai: requestStatusConst.DRAFT }}>
        <div className="grid grid-cols-3 gap-4">
          <Form.Item<IQueryRequest> label="Tìm kiếm:" name="Keyword">
            <Input placeholder="Mã yêu cầu/tên khảo sát" onChange={(e) => handleSearch(e)} />
          </Form.Item>
          <Form.Item label="Trạng thái:" name="trangThai">
            <Select placeholder="Chọn trạng thái" allowClear onChange={handleStatusFilter}>
              {requestStatusConst.list.map((status) => (
                <Option key={status.value} value={status.value}>
                  {status.name}
                </Option>
              ))}
            </Select>
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

export default Page;
