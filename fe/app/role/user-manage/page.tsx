'use client';

import { ChangeEvent, useState, useEffect } from 'react';
import { Breadcrumb, Button, Card, Form, Input, Tag } from 'antd';
import { PlusOutlined, SearchOutlined, SyncOutlined } from '@ant-design/icons';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getAllUser } from '@redux/feature/userSlice';

import { IQueryUser, IUserView } from '@models/user/user.model';
import AppTable from '@components/common/Table';
import { IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import CreateNhanSuModal from './(dialog)/create';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem } = useAppSelector((state) => state.userState);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);

  const columns: IColumn<IUserView>[] = [
    {
      key: 'Id',
      dataIndex: 'id',
      title: 'ID'
    },
    {
      key: 'maNhanSu',
      dataIndex: 'maNhanSu',
      title: 'Mã nhân sự'
    },
    {
      key: 'hoTen',
      dataIndex: 'hoTen',
      title: 'Họ tên'
    },
    {
      key: 'ngaySinh',
      dataIndex: 'ngaySinh',
      title: 'Ngày sinh',
      render: (value) => {
        const date = formatDateView(value);
        return <p>{date}</p>;
      }
    },
    {
      key: 'noiSinh',
      dataIndex: 'noiSinh',
      title: 'Nơi sinh'
    },
    {
      key: 'email',
      dataIndex: 'email',
      title: 'Email'
    },
    {
      key: 'soDienThoai',
      dataIndex: 'soDienThoai',
      title: 'Số điện thoại'
    },
    {
      key: 'tenChucVu',
      dataIndex: 'tenChucVu',
      title: 'Chức vụ'
    },
    {
      key: 'tenPhongBan',
      dataIndex: 'tenPhongBan',
      title: 'Phòng ban'
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
      width: 150,
      render: (value) => {
        let color = 'default';
        switch (value) {
          case 'Đang hoạt động':
            color = 'green';
            break;
          case 'Thôi việc':
            color = 'orange';
            break;
          case 'Đã về hưu':
            color = 'blue';
            break;
          case 'Chấm dứt hợp đồng':
            color = 'red';
            break;
          default:
            color = 'default';
        }
        return <Tag color={color}>{value || 'Chưa có'}</Tag>;
      }
    }
  ];

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryUser>({
    total: totalItem,
    initialQuery: {
      SkipCount: 0,
      MaxResultCount: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllUser(newQuery));
    },
    triggerFirstLoad: true
  });

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ maNhanSu: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  return (
    <Card
      title="Danh sách tài khoản"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item<IQueryUser> label="Mã nhân sự:" name="maNhanSu">
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
