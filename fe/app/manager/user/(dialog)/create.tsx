'use client';
import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Col, Form, Input, Modal, Row, Space, Switch } from 'antd';
import { CloseOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch } from '@redux/hooks';
import { createUser } from '@redux/feature/userSlice';
import { IUserCreate, INhanSuData } from '@models/user/user.model';
import { generateHuceEmail } from '@helpers/format.helper';
import { UserService } from '@services/user.service';

type CreateUserModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  onSuccess?: () => void;
};
type IUserCreateForm = IUserCreate & {
  hoDem?: string | null;
  ten?: string | null;
};

const CreateUser: React.FC<CreateUserModalProps> = ({ isModalOpen, setIsModalOpen, onSuccess }) => {
  const [form] = Form.useForm<IUserCreateForm>();
  const dispatch = useAppDispatch();

  const [autoPassword, setAutoPassword] = useState(true);
  const [isFetchingNhanSu, setIsFetchingNhanSu] = useState(false); // Trạng thái loading
  const maNhanSuValue = Form.useWatch('maNhanSu', form);

  useEffect(() => {
    if (maNhanSuValue && maNhanSuValue.length > 0) {
      const fetchData = async () => {
        setIsFetchingNhanSu(true);
        form.setFieldValue('email2', 'Đang tạo...');

        try {
          const data: INhanSuData = await UserService.getNhanSuByMaNhanSu(maNhanSuValue);

          if (data.hoDem && data.ten && data.tenChucVu) {
            const newEmail = generateHuceEmail(data.hoDem, data.ten, data.tenChucVu);
            form.setFieldValue('email2', newEmail);
            form.setFieldValue('hoDem', data.hoDem);
            form.setFieldValue('ten', data.ten);
          } else {
            toast.warn('Không đủ thông tin để tạo Email.');
            form.setFieldValue('email2', '');
            form.setFieldValue('hoDem', '');
            form.setFieldValue('ten', '');
          }
        } catch (error: any) {
          // Xử lý lỗi (ví dụ: không tìm thấy nhân sự)
          const errorMessage = error?.message || 'Lỗi khi lấy thông tin nhân sự';
          toast.error(errorMessage);
          form.setFieldValue('email2', '');
        } finally {
          setIsFetchingNhanSu(false);
        }
      };

      const timeoutId = setTimeout(fetchData, 400);

      return () => {
        clearTimeout(timeoutId);
        setIsFetchingNhanSu(false);
      };
    } else {
      form.setFieldValue('email2', '');
    }
  }, [maNhanSuValue, form]);

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

      await dispatch(createUser(dataToCreate)).unwrap();
      toast.success('Tạo mới thành công');
      onSuccess?.();
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
          <Button type="primary" onClick={form.submit} icon={<SaveOutlined />} disabled={isFetchingNhanSu}>
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
          <Input placeholder="Nhập mã nhân sự" disabled={isFetchingNhanSu} />
        </Form.Item>

        <Row gutter={12}>
          <Col span={14}>
            <Form.Item label="Họ đệm" name="hoDem">
              <Input placeholder="Tự động lấy từ nhân sự" disabled />
            </Form.Item>
          </Col>
          <Col span={10}>
            <Form.Item label="Tên" name="ten">
              <Input placeholder="Tự động lấy từ nhân sự" disabled />
            </Form.Item>
          </Col>
        </Row>

        <Form.Item<IUserCreate> label="Email (Được tạo)" name="email2">
          <Input
            placeholder="Email sẽ được tự động điền"
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

export default CreateUser;
