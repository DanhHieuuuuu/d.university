'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Tag, Select, Modal, Dropdown, Space, MenuProps } from 'antd';
import { PlusOutlined, SearchOutlined, SyncOutlined, EditOutlined, CheckOutlined, CloseOutlined, EyeOutlined, ExclamationCircleOutlined, DownOutlined, FileExcelOutlined } from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getPagingRequest, submitRequestAction, cancelSubmitRequestAction, removeRequest, getRequestById } from '@redux/feature/survey/surveyThunk';
import { resetRequestStatus, setSelectedRequest, clearSelectedRequest } from '@redux/feature/survey/surveySlice';

import { IQueryRequest, IViewRequest } from '@models/survey/request.model';

import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import CreateOrUpdateRequestModal from './(dialog)/create-or-update';
import { toast } from 'react-toastify';
import { requestStatusConst } from '@/constants/core/survey/requestStatus.const';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { isGranted } from '@hooks/isGranted';
import { FileService } from '@services/file.service';

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

  const canCreate = isGranted(PermissionCoreConst.SurveyButtonRequestCreate);
  const canUpdate = isGranted(PermissionCoreConst.SurveyButtonRequestUpdate);
  const canSubmit = isGranted(PermissionCoreConst.SurveyButtonRequestSubmit);
  const canCancelSubmit = isGranted(PermissionCoreConst.SurveyButtonRequestCancelSubmit);
  const canDelete = isGranted(PermissionCoreConst.SurveyButtonRequestDelete);


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
      hidden: (record: IViewRequest) => 
        !canUpdate || 
        (record.trangThai !== requestStatusConst.DRAFT && record.trangThai !== requestStatusConst.REJECTED)
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
      hidden: (record: IViewRequest) => 
        !canSubmit || 
        (record.trangThai !== requestStatusConst.DRAFT && record.trangThai !== requestStatusConst.REJECTED)
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
      hidden: (record: IViewRequest) => 
        !canCancelSubmit || 
        record.trangThai !== requestStatusConst.PENDING
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
      hidden: (record: IViewRequest) => 
        !canDelete || 
        (record.trangThai !== requestStatusConst.DRAFT && record.trangThai !== requestStatusConst.REJECTED)
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

  const handleDownloadTemplate = async () => {
    try {
      const blob = await FileService.downloadFile('survey/fa86d703-cdef-4e8b-993a-683126300d4d.xlsx');
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = 'survey_template.xlsx';
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      window.URL.revokeObjectURL(url);
    } catch (error: any) {
      console.error('Download error:', error);
      toast.error(error?.message || 'Không thể tải xuống file mẫu');
    }
  };

  const menuItems: MenuProps['items'] = [
    {
      key: 'download-template',
      label: 'Tải mẫu Excel',
      icon: <FileExcelOutlined />,
      onClick: handleDownloadTemplate
    }
  ];

  return (
    <Card
      title="Danh sách yêu cầu khảo sát"
      className="h-full"
      extra={
        canCreate ? (
          <Space>
            <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
              Thêm mới
            </Button>           
          </Space>
        ) : null
      }
    >
      <Form form={form} layout="vertical" initialValues={{ trangThai: requestStatusConst.DRAFT }}>
        <div className="grid grid-cols-4 gap-3">
          <Form.Item<IQueryRequest>  name="Keyword">
            <Input placeholder="Nhập mã yêu cầu/tên khảo sát" onChange={(e) => handleSearch(e)} />
          </Form.Item>
          <Form.Item  name="trangThai">
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
          <Form.Item colon={false} className="justify-self-end">
            <Dropdown menu={{ items: menuItems }}>
              <Button>
                Chức năng <DownOutlined />
              </Button>
            </Dropdown>
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

export default withAuthGuard(Page, PermissionCoreConst.SurveyMenuRequest);