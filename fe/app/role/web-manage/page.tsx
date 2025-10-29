'use client';

import { useEffect, useRef, useState } from 'react';
import { Breadcrumb, Button, Card, Form, Input, Modal, TableProps } from 'antd';
import { DeleteOutlined, EditOutlined, PlusOutlined, SearchOutlined, SyncOutlined } from '@ant-design/icons';
import { KeyIcon } from '@components/custom-icon';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import AppTable from '@components/common/Table';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';

import { IQueryRole, IRole } from '@models/role';
import { deleteRole, getListPermissionTree, getListRole, setSelectedRoleId } from '@redux/feature/roleConfigSlice';
import CreateRoleModal from './(dialog)/create-or-update';
import RolePermissionModal from './(dialog)/update-permission';
import { toast } from 'react-toastify';

const breadcrumbItems = [
  {
    title: 'Tổng quan',
    href: '/role'
  },
  {
    title: 'Phân quyền website',
    href: '/role/web-manage'
  },
  {
    title: 'Danh sách'
  }
];

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data, status, total: totalItem } = useAppSelector((state) => state.roleConfigState.roleGroup.$list);

  const [modalState, setModalState] = useState<{ open: boolean; isUpdate: boolean }>({
    open: false,
    isUpdate: false
  });

  useEffect(() => {
    (async () => {
      dispatch(getListPermissionTree());
    })();
  }, []);

  const [openPermission, setOpenModalPermission] = useState<boolean>(false);
  const { query, pagination, onFilterChange } = usePaginationWithFilter({
    total: totalItem,
    initialQuery: {
      SkipCount: 0,
      MaxResultCount: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getListRole(newQuery));
    },
    triggerFirstLoad: true
  });

  const onSearch = (values: IQueryRole) => {
    onFilterChange(values);
  };
  const onClickAdd = () => {
    setModalState({ open: true, isUpdate: false });
  };

  const onClickUpdate = (data: IRole) => {
    dispatch(setSelectedRoleId(data.id!));
    setModalState({ open: true, isUpdate: true });
  };

  const onClickDelete = (role: IRole) => {
    Modal.confirm({
      title: 'Xác nhận xóa nhóm quyền',
      content: `Bạn có chắc muốn xóa nhóm quyền "${role.name}"?`,
      okText: 'Xóa',
      okButtonProps: { danger: true },
      cancelText: 'Hủy',
      onOk: () => {
        handleDeleteRole(role.id!);
      }
    });
  };

  const onClickUpdatePermission = (data: IRole) => {
    dispatch(setSelectedRoleId(data.id!));
    setOpenModalPermission(true);
  };

  const handleDeleteRole = async (roleId: number) => {
    const result = await dispatch(deleteRole(roleId)).unwrap();
    if (result != undefined) {
      toast.success(result?.message || 'Xóa nhóm thành công');
      onFilterChange({})
    }
  };

  const columns: IColumn<IRole>[] = [
    {
      key: 'id',
      dataIndex: 'id',
      title: 'ID'
    },
    {
      key: 'name',
      dataIndex: 'name',
      title: 'Tên nhóm quyền'
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: 'Mô tả'
    },
    {
      key: 'totalUser',
      dataIndex: 'totalUser',
      title: 'Số người dùng'
    },
    {
      key: 'status',
      dataIndex: 'status',
      title: 'Trạng thái'
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Chỉnh sửa quyền',
      icon: <KeyIcon />,
      command: (record: IRole) => onClickUpdatePermission(record)
    },
    {
      label: 'Cập nhật',
      tooltip: 'Cập nhật',
      icon: <EditOutlined />,
      command: (record: IRole) => onClickUpdate(record)
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IRole) => onClickDelete(record)
    }
  ];

  return (
    <div className="flex h-full flex-col gap-4">
      <Breadcrumb separator=">" items={breadcrumbItems} />
      <Card
        title="Danh sách nhóm quyền Core"
        variant="borderless"
        className="h-full"
        extra={
          <Button type="primary" style={{ fontWeight: 500 }} icon={<PlusOutlined />} onClick={onClickAdd}>
            Thêm mới
          </Button>
        }
      >
        <Form form={form} layout="horizontal" onFinish={onSearch}>
          <div className="grid grid-cols-2">
            <Form.Item<IQueryRole> label="Từ khóa:" name="Keyword">
              <Input className="h-9 !w-full" placeholder="Nhập thông tin" allowClear />
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
          dataSource={data}
          listActions={actions}
          pagination={{ position: ['bottomRight'], ...pagination }}
        />
      </Card>

      <CreateRoleModal
        isModalOpen={modalState.open}
        isUpdate={modalState.isUpdate}
        setIsModalOpen={(open) => setModalState((prev) => ({ ...prev, open }))}
        refreshData={() => onFilterChange({})}
      />

      <RolePermissionModal
        isModalOpen={openPermission}
        setIsModalOpen={setOpenModalPermission}
        refreshData={() => onFilterChange({})}
      />
    </div>
  );
};

export default Page;
