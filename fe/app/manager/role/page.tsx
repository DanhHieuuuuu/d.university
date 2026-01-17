'use client';

import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { Breadcrumb, Button, Card, Form, Input, Modal } from 'antd';
import {
  DeleteOutlined,
  EditOutlined,
  LockOutlined,
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  UnlockOutlined
} from '@ant-design/icons';
import { KeyIcon } from '@components/custom-icon';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import AppTable from '@components/common/Table';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';

import { IQueryRole, IRole } from '@models/role';
import {
  deleteRole,
  getListPermissionTree,
  getListRole,
  resetStatusRole,
  setSelectedRoleId,
  updateRoleStatusThunk
} from '@redux/feature/roleConfigSlice';
import CreateRoleModal from './(dialog)/create-or-update';
import RolePermissionModal from './(dialog)/update-permission';
import { ETableColumnType } from '@/constants/e-table.consts';
import { RoleStatusConst } from '@/constants/auth/role.const';

const breadcrumbItems = [
  {
    title: 'Tổng quan',
    href: '/manager'
  },
  {
    title: 'Phân quyền website',
    href: '/manager/role'
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
  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
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
      centered: true,
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
      dispatch(resetStatusRole());
      onFilterChange(query);
    }
  };

  const handleActiveRole = async (roleId: number) => {
    const result = await dispatch(updateRoleStatusThunk({ id: roleId, status: RoleStatusConst.ACTIVE })).unwrap();
    if (result != undefined) {
      toast.success(result?.message || 'Thành công');
      dispatch(resetStatusRole());
    }
  };

  const handleDisableRole = async (roleId: number) => {
    const result = await dispatch(updateRoleStatusThunk({ id: roleId, status: RoleStatusConst.DISABLED })).unwrap();
    if (result != undefined) {
      toast.success(result?.message || 'Thành công');
      dispatch(resetStatusRole());
    }
  };

  const columns: IColumn<IRole>[] = [
    {
      key: 'name',
      dataIndex: 'name',
      title: 'Tên nhóm quyền',
      width: 200
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: 'Mô tả'
    },
    {
      key: 'totalUser',
      dataIndex: 'totalUser',
      title: 'Số người dùng',
      align: 'center',
      width: 150
    },
    {
      key: 'status',
      dataIndex: 'status',
      title: 'Trạng thái',
      align: 'center',
      type: ETableColumnType.STATUS,
      getTagInfo: (value) => RoleStatusConst.getTag(value)
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
      label: 'Kích hoạt',
      tooltip: 'Kích hoạt',
      icon: <UnlockOutlined />,
      hidden: (record: IRole) => record.status === RoleStatusConst.ACTIVE,
      command: (record: IRole) => handleActiveRole(record.id!)
    },
    {
      label: 'Khóa',
      tooltip: 'Khóa',
      icon: <LockOutlined />,
      hidden: (record: IRole) => record.status === RoleStatusConst.DISABLED,
      command: (record: IRole) => handleDisableRole(record.id!)
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
          dataSource={data}
          listActions={actions}
          pagination={{ position: ['bottomRight'], ...pagination }}
        />
      </Card>

      <CreateRoleModal
        isModalOpen={modalState.open}
        isUpdate={modalState.isUpdate}
        setIsModalOpen={(open) => setModalState((prev) => ({ ...prev, open }))}
        refreshData={() => onFilterChange(query)}
      />

      <RolePermissionModal
        isModalOpen={openPermission}
        setIsModalOpen={setOpenModalPermission}
        refreshData={() => onFilterChange(query)}
      />
    </div>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.UserMenuPermission);
