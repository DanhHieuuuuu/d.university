'use client';
import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Form, Input, Modal, Select, Table } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiRole } from '@redux/feature/kpi/kpiSlice';
import { deleteKpiRole, getAllIdsKpiRole, getAllKpiRole } from '@redux/feature/kpi/kpiThunk';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryKpiRole, IViewKpiRole } from '@models/kpi/kpi-role.model';
import PositionModal from './(dialog)/create-or-update';
import { KpiRoleConst } from '../../const/kpiRole.const';
import { toast } from 'react-toastify';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiRole.$list);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryKpiRole>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllKpiRole(newQuery));
    },
    triggerFirstLoad: true
  });

  useEffect(() => {
    if (!isModalOpen) {
      dispatch(getAllKpiRole(query));
    }
  }, [isModalOpen]);

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickUpdate = (record: IViewKpiRole) => {
    dispatch(setSelectedKpiRole(record));
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

  const onClickView = (record: IViewKpiRole) => {
    dispatch(setSelectedKpiRole(record));
    setIsModalView(true);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickDelete = (record: IViewKpiRole) => {
    console.log(record);
    Modal.confirm({
      title: `Xóa Role ${record.role} của ${record.tenNhanSu} với ${record.tenDonViKiemNhiem}?`,
      okText: 'Xóa',
      okType: 'danger',
      cancelText: 'Hủy',
      onOk: async () => {
        try {
          await dispatch(deleteKpiRole([record.id])).unwrap();
          toast.success('Xóa thành công!');
          dispatch(getAllKpiRole(query));
        } catch (error: any) {
          toast.error(error?.response?.message || 'Xóa thất bại!');
        }
      }
    });
  };

  const columns: IColumn<IViewKpiRole>[] = [
    {
      key: 'stt',
      dataIndex: 'stt',
      title: 'STT',
      align: 'center',
      render: (value, row, index) => index + 1
    },
    {
      key: 'tenNhanSu',
      dataIndex: 'tenNhanSu',
      title: 'Tên Nhân sự'
    },
    {
      key: 'tenPhongBan',
      dataIndex: 'tenPhongBan',
      title: 'Phòng ban hiện tại'
    },
    {
      key: 'tenDonViKiemNhiem',
      dataIndex: 'tenDonViKiemNhiem',
      title: 'Đơn vị kiêm nhiệm'
    },
    {
      key: 'tiLe',
      dataIndex: 'tiLe',
      title: 'Tỉ lệ',
    },
    {
      key: 'role',
      dataIndex: 'role',
      title: 'Chức vụ',
      render: (value: string) => KpiRoleConst.getName(value),
    },
  ];

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      tooltip: 'Xem thông tin phòng ban',
      icon: <EyeOutlined />,
      command: onClickView
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin phòng ban',
      icon: <EditOutlined />,
      command: onClickUpdate
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: onClickDelete
    }
  ];

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };
  const rowSelection = {
    selectedRowKeys,
    preserveSelectedRowKeys: true,
    onChange: (newSelectedRowKeys: React.Key[]) => {      
      const currentPageIds = list?.map(item => item.id) || [];
      const isUnselectingAllOnPage = currentPageIds.every(id => !newSelectedRowKeys.includes(id));
      if (isUnselectingAllOnPage && selectedRowKeys.length > list?.length) {
        setSelectedRowKeys([]);
      } else {
        setSelectedRowKeys(newSelectedRowKeys);
      }
    },
    selections: [
      {
        key: 'current-page',
        text: 'Chọn 1 trang',
        onSelect: (changableRowKeys: React.Key[]) => {
          setSelectedRowKeys(changableRowKeys);
        },
      },
      {
        key: 'all-pages',
        text: 'Chọn tất cả',
        onSelect: () => {
          handleSelectAllPages();
        },
      },
    ]
  };

  const handleSelectAllPages = async () => {
    try {
      const allIds = await dispatch(getAllIdsKpiRole({
        ...query,
        PageIndex: 1,
        PageSize: totalItem
      })).unwrap();

      setSelectedRowKeys(allIds);
      toast.success(`Đã chọn tất cả ${allIds.length} bản ghi`);
    } catch (error) {
      toast.error("Không thể chọn tất cả");
    }
  };
  return (
    <Card
      title="Danh sách KPI Cá nhân"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item name="role">
            <Select
              showSearch
              placeholder="Chọn chức vụ"
              allowClear
              optionFilterProp="label"
              options={KpiRoleConst.list.map((x) => ({
                value: x.value,
                label: x.name,
              }))}
              onChange={(value) => onFilterChange({ chucVu: value })}
            />
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
                form.submit();
              }}
            >
              Tải lại
            </Button>
          </div>
        </Form.Item>
      </Form>
      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
        rowSelection={rowSelection}
      />

      <PositionModal
        isModalOpen={isModalOpen}
        isUpdate={isUpdate}
        isView={isView}
        setIsModalOpen={setIsModalOpen}
      />
    </Card>
  );
};

export default Page;
