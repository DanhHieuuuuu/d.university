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
import { getAllChuyenNganh, setSelectedIdChuyenNganh } from '@redux/feature/daotaoSlice';

import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryChuyenNganh, IViewChuyenNganh } from '@models/dao-tao/chuyenNganh.model';
import SpecializationModal from './(dialog)/create-or-update';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.daotaoState.chuyenNganh.$list);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [selectedId, setSelectedId] = useState<number>(0);

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryChuyenNganh>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllChuyenNganh(newQuery));
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
    dispatch(getAllChuyenNganh(query));
  };

  const columns: IColumn<IViewChuyenNganh>[] = [
    {
      key: 'Id',
      dataIndex: 'id',
      title: 'ID',
      showOnConfig: false
    },
    {
      key: 'maChuyenNganh',
      dataIndex: 'maChuyenNganh',
      title: 'Mã chuyên ngành'
    },
    {
      key: 'tenChuyenNganh',
      dataIndex: 'tenChuyenNganh',
      title: 'Tên chuyên ngành'
    },
    {
      key: 'tenTiengAnh',
      dataIndex: 'tenTiengAnh',
      title: 'Tên tiếng Anh'
    },
    {
      key: 'moTa',
      dataIndex: 'moTa',
      title: 'Mô tả'
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
      render: (value: boolean) => (
        <span className={value ? 'text-green-600' : 'text-red-600'}>
          {value ? 'Hoạt động' : 'Ngừng'}
        </span>
      )
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      tooltip: 'Xem thông tin chuyên ngành',
      icon: <EyeOutlined />,
      command: (record: IViewChuyenNganh) => {
        dispatch(setSelectedIdChuyenNganh(record.id));
        onClickView(record.id);
      }
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin chuyên ngành',
      icon: <EditOutlined />,
      command: (record: IViewChuyenNganh) => {
        dispatch(setSelectedIdChuyenNganh(record.id));
        onClickUpdate(record.id);
      }
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IViewChuyenNganh) => {
        dispatch(setSelectedIdChuyenNganh(record.id));
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
      title="Danh sách chuyên ngành"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item<IQueryChuyenNganh> label="Tên chuyên ngành:" name="Keyword">
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
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />

      <SpecializationModal
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
