'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input } from 'antd';
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
import { getAllKpiRole, setSelectedKpiRole } from '@redux/feature/kpiSlice';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryKpiRole, IViewKpiRole } from '@models/kpi/kpi-role.model';
import PositionModal from './(dialog)/create-or-update';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiRole.$list);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [selectedId, setSelectedId] = useState<number>(0);


  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryKpiRole>({
    total: totalItem || 0,
    initialQuery: {
      SkipCount: 0,
      MaxResultCount: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllKpiRole(newQuery));
    },
    triggerFirstLoad: true
  });

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickUpdate = (id: number) => {
    setSelectedId(id);
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

  const onClickView = (id: number) => {
    setSelectedId(id);
    setIsModalView(true);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const refreshData = () => {
    dispatch(getAllKpiRole(query));
  };

  const columns: IColumn<IViewKpiRole>[] = [
    {
      key: 'Id',
      dataIndex: 'id',
      title: 'ID',
      showOnConfig: false
    },
    {
      key: 'maNhanSu',
      dataIndex: 'maNhanSu',
      title: 'Mã nhân sự'
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
    },
  ];

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      tooltip: 'Xem thông tin phòng ban',
      icon: <EyeOutlined />,
      command: (record: IViewKpiRole) => {
        dispatch(setSelectedKpiRole(record));
        onClickView(record.id);
      }
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin phòng ban',
      icon: <EditOutlined />,
      command: (record: IViewKpiRole) => {
        dispatch(setSelectedKpiRole(record));
        onClickUpdate(record.id);
      }
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IViewKpiRole) => {
        dispatch(setSelectedKpiRole(record));
      }
    }
  ];

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
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
          <Form.Item<IQueryKpiRole> label="Tên phòng ban:" name="Keyword">
            <Input onChange={(e) => handleSearch(e)} />
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
      />

      <PositionModal
        isModalOpen={isModalOpen}
        isUpdate={isUpdate}
        isView={isView}
        setIsModalOpen={setIsModalOpen}
        refreshData={refreshData}
      />
    </Card>
  );
};

export default Page;
