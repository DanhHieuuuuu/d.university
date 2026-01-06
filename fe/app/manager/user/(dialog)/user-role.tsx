'use client';

import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { Form, Input, Modal, TreeSelect } from 'antd';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getListRole } from '@redux/feature/roleConfigSlice';
import { getUserRolesByIdThunk, updateRolesToUserThunk } from '@redux/feature/userSlice';
import { IRole } from '@models/role';
import { IUserView } from '@models/user/user.model';

type UserRoleModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  userId: number | null;
  refreshData: () => void;
};

const UserRoleModal: React.FC<UserRoleModalProps> = ({ isModalOpen, setIsModalOpen, userId, refreshData }) => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();

  const roleList = useAppSelector((state) => state.roleConfigState.roleGroup.$list.data);
  const userList = useAppSelector((state) => state.userState.list);

  const [treeData, setTreeData] = useState<{ title: string; value: number; key: number }[]>([]);
  const [userData, setUserData] = useState<IUserView | null>(null);

  useEffect(() => {
    if (roleList?.length) {
      const transformed = roleList.map((role: IRole) => ({
        title: role.name ?? '',
        value: role.id ?? 0,
        key: role.id ?? 0
      }));
      setTreeData(transformed);
    }
  }, [roleList]);

  useEffect(() => {
    if (isModalOpen && userId) {
      const user = userList.find((u) => u.id === userId) ?? null;
      setUserData(user);

      // Lấy danh sách role
      dispatch(getListRole({ PageIndex: 0, PageSize: 1000, Keyword: '' }));

      // Lấy role hiện tại của user
      dispatch(getUserRolesByIdThunk(userId))
        .unwrap()
        .then((res) => {
          form.setFieldsValue({
            hoTen: user?.hoDem && user?.ten ? `${user.hoDem} ${user.ten}` : '',
            email: user?.email ?? '',
            tenPhongBan: user?.tenPhongBan ?? '',
            tenChucVu: user?.tenChucVu ?? '',
            RoleIds: res.data?.roleIds ?? []
          });
        })
        .catch((err) => {
          toast.error('Không lấy được nhóm quyền người dùng');
          console.error(err);
        });
    }
  }, [isModalOpen, userId]);

  const handleClose = () => {
    form.resetFields();
    setIsModalOpen(false);
  };

  const handleSubmit = async (values: { RoleIds: number[] }) => {
    if (!userId) return;

    try {
      await dispatch(updateRolesToUserThunk({ userId, roleIds: values.RoleIds })).unwrap();
      toast.success('Cập nhật nhóm quyền cho user thành công');
      refreshData();
      handleClose();
    } catch (error: any) {
      toast.error(error?.message || 'Cập nhật thất bại');
    }
  };

  return (
    <Modal
      title="Cập nhật nhóm quyền cho nhân sự"
      className="app-modal"
      width={700}
      open={isModalOpen}
      onCancel={handleClose}
      onOk={() => form.submit()}
      okText="Lưu"
      cancelText="Hủy"
    >
      <Form form={form} layout="vertical" onFinish={handleSubmit}>
        <Form.Item label="Họ tên" name="hoTen">
          <Input disabled />
        </Form.Item>
        <Form.Item label="Email" name="email">
          <Input disabled />
        </Form.Item>
        <Form.Item label="Phòng ban" name="tenPhongBan">
          <Input disabled />
        </Form.Item>
        <Form.Item label="Chức vụ" name="tenChucVu">
          <Input disabled />
        </Form.Item>
        <Form.Item label="Nhóm quyền" name="RoleIds">
          <TreeSelect
            treeLine
            treeCheckable
            treeDefaultExpandAll
            showCheckedStrategy={TreeSelect.SHOW_ALL}
            placeholder="Chọn nhóm quyền"
            treeData={treeData}
            style={{ width: '100%' }}
          />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default UserRoleModal;
