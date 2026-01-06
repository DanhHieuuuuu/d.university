'use client';

import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  StopOutlined,
  DeleteOutlined,
  EyeOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedIdChucVu } from '@redux/feature/danh-muc/danhmucSlice';
import { getAllChucVu } from '@redux/feature/danh-muc/danhmucThunk';

import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryChucVu, IViewChucVu } from '@models/danh-muc/chuc-vu.model';
import PositionModal from './(dialog)/create-or-update';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.danhmucState.chucVu.$list);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [selectedId, setSelectedId] = useState<number>(0);

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryChucVu>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllChucVu(newQuery));
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
    dispatch(getAllChucVu(query));
  };

  const columns: IColumn<IViewChucVu>[] = [
    {
      key: 'Id',
      dataIndex: 'id',
      title: 'ID',
      showOnConfig: false
    },
    {
      key: 'maChucVu',
      dataIndex: 'maChucVu',
      title: 'Mã chức vụ'
    },
    {
      key: 'tenChucVu',
      dataIndex: 'tenChucVu',
      title: 'Tên chức vụ'
    },
    {
      key: 'hsChucVu',
      dataIndex: 'hsChucVu',
      title: 'Hệ số'
    },
    {
      key: 'hsTrachNhiem',
      dataIndex: 'hsTrachNhiem',
      title: 'Hệ số trách nhiệm'
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      tooltip: 'Xem thông tin chức vụ',
      icon: <EyeOutlined />,
      command: (record: IViewChucVu) => {
        dispatch(setSelectedIdChucVu(record.id));
        onClickView(record.id);
      }
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin chức vụ',
      icon: <EditOutlined />,
      command: (record: IViewChucVu) => {
        dispatch(setSelectedIdChucVu(record.id));
        onClickUpdate(record.id);
      }
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IViewChucVu) => {
        dispatch(setSelectedIdChucVu(record.id));
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
      title="Danh sách chức vụ"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item<IQueryChucVu> label="Tên chức vụ:" name="Keyword">
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
