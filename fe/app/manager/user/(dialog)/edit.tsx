'use client';
import { useEffect } from 'react';
import { Modal, Form, Input, message } from 'antd';
import { useAppDispatch } from '@redux/hooks';
import { updateUser } from '@redux/feature/userSlice';
import { IUserView } from '@models/user/user.model';
import { toast } from 'react-toastify';

type EditUserModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  user: IUserView | null;
  onSuccess?: () => void;
};

const EditUserModal = ({ isModalOpen, setIsModalOpen, user, onSuccess }: EditUserModalProps) => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (user) {
      form.setFieldsValue({ email: user.email, password: '' });
    }
  }, [user]);

  const handleOk = async () => {
    try {
      const values = await form.validateFields();
      if (!values.email && !values.password) {
        message.warning('Vui lòng nhập Email hoặc Password');
        return;
      }

      await dispatch(
        updateUser({
          Id: user?.id!,
          Email: values.email || undefined,
          NewPassword: values.password || undefined
        })
      ).unwrap();

      toast.success('Cập nhật thành công');
      onSuccess?.();
      setIsModalOpen(false);
    } catch {
      toast.error('Cập nhật thất bại');
    }
  };

  return (
    <Modal
      title="Chỉnh sửa người dùng"
      open={isModalOpen}
      onOk={handleOk}
      onCancel={() => setIsModalOpen(false)}
      okText="Lưu"
      cancelText="Hủy"
    >
      <Form form={form} layout="vertical">
        <Form.Item label="Email" name="email">
          <Input placeholder="Nhập email mới" />
        </Form.Item>
        <Form.Item label="Password" name="password">
          <Input.Password placeholder="Nhập password mới" />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default EditUserModal;
