'use client';
import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Col, Form, Input, Modal, Row, Space, Switch } from 'antd';
import { CloseOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch } from '@redux/hooks';
import { createUser, createUser2 } from '@redux/feature/userSlice';
import { IUserCreate, IUserView } from '@models/user/user.model';
import { generateHuceEmail } from '@helpers/format.helper';
import { UserService } from '@services/user.service';

type AddUserModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  onSuccess?: () => void;
};
type IUserCreateForm = IUserCreate & {
  hoDem?: string | null;
  ten?: string | null;
};

const AddUser: React.FC<AddUserModalProps> = ({ isModalOpen, setIsModalOpen, onSuccess }) => {
  const [form] = Form.useForm<IUserCreateForm>();
  const dispatch = useAppDispatch();

  const [autoPassword, setAutoPassword] = useState(true);
  const [isFetchingNhanSu, setIsFetchingNhanSu] = useState(false); // Trạng thái loading
  const maNhanSuValue = Form.useWatch('maNhanSu', form);

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.value && autoPassword) {
      setAutoPassword(false);
    }
  };

  const handleAutoPasswordChange = (checked: boolean) => {
    setAutoPassword(checked);
    if (checked) {
      form.setFieldValue('password', '');
    }
  };

  const onFinish = async (values: IUserCreate) => {
    try {
      const dataToCreate: IUserCreate = {
        ...values,
        password: autoPassword ? null : values.password,
        email2: values.email2 || null
      };

      const res = await dispatch(createUser2(dataToCreate)).unwrap();
      if (res) {
        toast.success('Tạo mới thành công');
        onSuccess?.();
        setIsModalOpen(false);
        form.resetFields();
        setAutoPassword(true);
      } else {
        toast.error(res?.message || 'Tài khoản đã tồn tại hoặc không tạo được');
      }
    } catch (error: any) {
      toast.error(error?.response?.data?.message || error?.message || 'Tạo user thất bại');
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
          label="Mã đăng nhập"
          name="maNhanSu"
          rules={[{ required: true, message: 'Vui lòng nhập mã đăng nhập!' }]}
        >
          <Input placeholder="Nhập mã đăng nhập"/>
        </Form.Item>

        <Row gutter={12}>
          <Col span={14}>
            <Form.Item label="Họ đệm" name="hoDem">
              <Input placeholder="Nhập họ đệm" />
            </Form.Item>
          </Col>
          <Col span={10}>
            <Form.Item label="Tên" name="ten">
              <Input placeholder="Nhâp tên" />
            </Form.Item>
          </Col>
        </Row>

        <Form.Item<IUserCreate> label="Email" name="email2"          
        rules={[{ required: true, message: 'Vui lòng email!' }]}
>
          <Input
            placeholder="Nhập email"
            disabled={false}
            suffix={isFetchingNhanSu ? '...' : undefined}
          />
        </Form.Item>

        <Form.Item<IUserCreate>
          label="Mật khẩu"
          name="password"
          rules={[{ required: !autoPassword, message: 'Vui lòng nhập mật khẩu!' }]}
        >
          <Input.Password
            placeholder="Nhập mật khẩu (nếu muốn tự tạo, tắt switch ở trên)"
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

export default AddUser;
