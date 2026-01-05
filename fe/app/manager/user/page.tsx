'use client';

import { ChangeEvent, useState } from 'react';
import { toast } from 'react-toastify';
import { Button, Card, Form, Input, Tag } from 'antd';
import { PlusOutlined, SearchOutlined, SyncOutlined, EditOutlined, KeyOutlined } from '@ant-design/icons';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { formatDateView } from '@utils/index';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { changeStatusUserThunk, getAllUser } from '@redux/feature/userSlice';

import AppTable from '@components/common/Table';
import { IQueryUser, IUserView } from '@models/user/user.model';
import { IAction, IColumn } from '@models/common/table.model';

import CreateNhanSuModal from './(dialog)/create';
import UserRoleModal from './(dialog)/user-role';
import EditUserModal from './(dialog)/edit';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem } = useAppSelector((state) => state.userState);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [openUserRoleModal, setOpenUserRoleModal] = useState(false);
  const [selectedUser, setSelectedUser] = useState<IUserView | null>(null);

  const onClickAdd = () => setIsModalOpen(true);

  const columns: IColumn<IUserView>[] = [
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
      key: 'email2',
      dataIndex: 'email2',
      title: 'Email2'
    },
    {
      key: 'soCccd',
      dataIndex: 'soCccd',
      title: 'Số CCCD'
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
        const colors: Record<string, string> = {
          'Đang hoạt động': 'green',
          'Thôi việc': 'orange',
          'Đã về hưu': 'blue',
          'Chấm dứt hợp đồng': 'red'
        };
        return <Tag color={colors[value] || 'default'}>{value || 'Chưa có'}</Tag>;
      }
    }
  ];
  const actions: IAction[] = [
    {
      label: 'Cập nhật nhóm quyền',
      icon: <KeyOutlined />,
      command: (record: IUserView) => {
        setSelectedUser(record);
        setOpenUserRoleModal(true);
      }
    },
    {
      label: 'Sửa',
      icon: <EditOutlined />,
      command: (record: IUserView) => {
        setSelectedUser(record);
        setIsEditModalOpen(true);
      }
    },
    {
      label: 'Đổi trạng thái',
      color: 'primary',
      icon: <SyncOutlined />,
      command: (record: IUserView) => {
        if (!record.id) return;
        setSelectedUser(record);
        dispatch(changeStatusUserThunk(record.id))
          .unwrap()
          .then(() => {
            toast.success('Cập nhật trạng thái thành công');
          })
          .catch((err) => {
            console.error(err);
            toast.error('Không đổi trạng thái được');
          });
      }
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryUser>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllUser(newQuery));
    },
    triggerFirstLoad: true
  });

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
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
          <Form.Item<IQueryUser> label="Tìm kiếm:" name="Keyword">
            <Input placeholder="Nhập họ tên/CCCD/Mã nhân sự" onChange={(e) => handleSearch(e)} />
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
        rowKey="maNhanSu"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />
      <CreateNhanSuModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        onSuccess={() => dispatch(getAllUser(query))}
      />
      <EditUserModal
        isModalOpen={isEditModalOpen}
        setIsModalOpen={setIsEditModalOpen}
        user={selectedUser}
        onSuccess={() => dispatch(getAllUser(query))}
      />
      <UserRoleModal
        isModalOpen={openUserRoleModal}
        setIsModalOpen={setOpenUserRoleModal}
        userId={selectedUser?.id || null}
        refreshData={() => onFilterChange(query)}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.UserMenuAccountManager);
