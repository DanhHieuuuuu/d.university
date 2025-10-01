'use client';

import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input } from 'antd';
import { PlusOutlined, SearchOutlined, SyncOutlined } from '@ant-design/icons';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getListNhanSu, selectMaNhanSu } from '@redux/feature/nhansuSlice';
import { IQueryNhanSu, IViewNhanSu } from '@models/nhansu/nhansu.model';

import AppTable from '@components/common/Table';
import { IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';

import CreateNhanSuModal from './(dialog)/create';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem } = useAppSelector((state) => state.nhanSuState);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);

  const columns: IColumn<IViewNhanSu>[] = [
    {
      key: 'maNhanSu',
      dataIndex: 'maNhanSu',
      title: 'Mã NS',
      align: 'center',
      showOnConfig: false
    },
    {
      key: 'hoDem',
      dataIndex: 'hoDem',
      title: 'Họ đệm'
    },
    {
      key: 'ten',
      dataIndex: 'ten',
      title: 'Tên'
    },
    {
      key: 'soCccd',
      dataIndex: 'soCccd',
      title: 'Số CCCD'
    },
    {
      key: 'ngaySinh',
      dataIndex: 'ngaySinh',
      title: 'Ngày sinh',
      render: (value) => {
        const date = formatDateView(value);
        return <p>{date}</p>;
      }
    }
  ];

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryNhanSu>({
    total: totalItem,
    initialQuery: {
      SkipCount: 0,
      MaxResultCount: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getListNhanSu(newQuery));
    },
    triggerFirstLoad: true
  });

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ cccd: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickView = (data: IViewNhanSu) => {
    dispatch(selectMaNhanSu(data.maNhanSu!));
    setIsModalView(true);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickUpdate = (data: IViewNhanSu) => {
    dispatch(selectMaNhanSu(data.maNhanSu!));
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

  return (
    <Card
      title="Danh sách nhân sự"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item<IQueryNhanSu> label="Cccd:" name="cccd">
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
        rowKey="maNhanSu"
        columns={columns}
        dataSource={list}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />

      <CreateNhanSuModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        isUpdate={isUpdate}
        isView={isView}
      />
    </Card>
  );
};

export default Page;
