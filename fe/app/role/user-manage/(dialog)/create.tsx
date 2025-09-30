'use client';
import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, Input, Modal, Space, Switch } from 'antd';
import { CloseOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch } from '@redux/hooks';
import { createUser } from '@redux/feature/userSlice';
import { IUserCreate } from '@models/user/user.model';

type CreateUserModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
};

const CreateUser: React.FC<CreateUserModalProps> = ({ isModalOpen, setIsModalOpen }) => {
  const [form] = Form.useForm<IUserCreate>();
  const dispatch = useAppDispatch();

  const [autoPassword, setAutoPassword] = useState(true);

  // Khi nhập password thủ công → autoPassword tắt
  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.value && autoPassword) {
      setAutoPassword(false);
    }
  };

  // Khi bật autoPassword → xóa password input
  const handleAutoPasswordChange = (checked: boolean) => {
    setAutoPassword(checked);
    if (checked) {
      form.setFieldValue('password', '');
    }
  };

  const onFinish = async (values: IUserCreate) => {
    try {
      await dispatch(createUser(values)).unwrap();
      toast.success('Tạo mới thành công');
      setIsModalOpen(false);
      form.resetFields();
      setAutoPassword(true);
    } catch {
      toast.error('Tạo user thất bại');
    }
  };

  return (
    <Modal
      title="Thêm mới tài khoản"
      open={isModalOpen}
      onCancel={() => setIsModalOpen(false)}
      width={500}
      footer={
        <Space>
          <Button type="primary" onClick={form.submit} icon={<SaveOutlined />}>
            Tạo mới
          </Button>
          <Button onClick={() => setIsModalOpen(false)} icon={<CloseOutlined />}>
            Hủy
          </Button>
        </Space>
      }
    >
      <Form form={form} layout="vertical" onFinish={onFinish} autoComplete="off">
        <Form.Item<IUserCreate>
          label="Mã nhân sự"
          name="maNhanSu"
          rules={[{ required: true, message: 'Vui lòng nhập mã nhân sự!' }]}
        >
          <Input placeholder="Nhập mã nhân sự" />
        </Form.Item>

        <Form.Item<IUserCreate> label="Email" name="email">
          <Input placeholder="Email sẽ được tự động điền" />
        </Form.Item>

        <Form.Item<IUserCreate> label="Mật khẩu" name="password">
          <Input.Password
            placeholder="Nhập mật khẩu (nếu muốn tự tạo, bật switch ở trên)"
            disabled={autoPassword}
            onChange={handlePasswordChange}
          />
        </Form.Item>

        <Form.Item label="Tùy chọn">
          <Space>
            <Switch checked={autoPassword} onChange={handleAutoPasswordChange} />
            <span>Tạo mật khẩu tự động</span>
          </Space>
        </Form.Item>

      </Form>
    </Modal>
  );
};

export default CreateUser;
