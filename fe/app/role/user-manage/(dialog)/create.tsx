'use client';

import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, Input, Modal, Switch, Space } from 'antd';
import { CloseOutlined, SaveOutlined } from '@ant-design/icons';

import { useAppSelector } from '@redux/hooks';
import { IUserView } from '@models/user/user.model';

type CreateUserModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
};

const CreateUser: React.FC<CreateUserModalProps> = ({ isModalOpen, setIsModalOpen, isUpdate, isView }) => {
  const [form] = Form.useForm<IUserView>();
  const [title, setTitle] = useState('');
  const { selected } = useAppSelector((state) => state.userState);

  const [autoPassword, setAutoPassword] = useState(true);
  const [forceChangePassword, setForceChangePassword] = useState(true);

  useEffect(() => {
    if (isModalOpen) {
      if (isUpdate && selected) {
        setTitle('Chỉnh sửa tài khoản');
        form.setFieldsValue({ ...selected });
      } else if (isView && selected) {
        setTitle('Chi tiết tài khoản');
        form.setFieldsValue({ ...selected });
      } else {
        setTitle('Thêm mới tài khoản');
        form.resetFields();
      }
    }
  }, [isModalOpen, isUpdate, isView, selected]);

  const onFinish = (values: IUserView) => {
    console.log('Form submit:', { ...values, autoPassword, forceChangePassword });
    if (isUpdate) {
      toast.success('Cập nhật thành công');
    } else {
      toast.success('Thêm mới thành công');
    }
    setIsModalOpen(false);
  };

  return (
    <Modal
      title={title}
      open={isModalOpen}
      onCancel={() => setIsModalOpen(false)}
      width={500}
      footer={
        <Space>
          {!isView && (
            <Button type="primary" onClick={form.submit} icon={<SaveOutlined />}>
              {isUpdate ? 'Lưu' : 'Tạo mới'}
            </Button>
          )}
          <Button onClick={() => setIsModalOpen(false)} icon={<CloseOutlined />}>
            Hủy
          </Button>
        </Space>
      }
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={onFinish}
        disabled={isView}
        autoComplete="off"
      >
        <Form.Item
          label="Tài khoản xác thực"
          name="maNhanSu"
          rules={[{ required: true, message: 'Vui lòng nhập tài khoản xác thực!' }]}
        >
          <Input placeholder="Nhập mã nhân sự" />
        </Form.Item>

        <Form.Item
          label="Tên hiển thị"
          name="hoTen"
          rules={[{ required: true, message: 'Vui lòng nhập tên hiển thị!' }]}
        >
          <Input placeholder="Nhập tên hiển thị" />
        </Form.Item>

        <Form.Item
          label="Tên đăng nhập"
          name="Tên đăng nhập"
          rules={[{ required: true, message: 'Vui lòng nhập tên đăng nhập!' }]}
        >
          <Input placeholder="Nhập tên đăng nhập" />
        </Form.Item>

        <Form.Item
          label="Password"
          name="Mật khẩu"
          rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}
        >
          <Input.Password placeholder="Nhập mật khẩu" />
        </Form.Item>

        <Form.Item label="Tùy chọn">
          <Space direction="vertical">
            <Space>
              <Switch checked={autoPassword} onChange={setAutoPassword} />
              <span>Tạo mật khẩu tự động</span>
            </Space>
            <Space>
              <Switch checked={forceChangePassword} onChange={setForceChangePassword} />
              <span>Yêu cầu thay đổi mật khẩu khi đăng nhập</span>
            </Space>
          </Space>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CreateUser;
