'use client';
import { toast } from 'react-toastify';
import React from 'react';
import { Button, Form, Input, Modal, Space, DatePicker, Select } from 'antd';
import { CloseOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch } from '@redux/hooks';
import { createStudent } from '@redux/feature/studentSlice';
import { ICreateStudent } from '@models/student/student.model';
import dayjs from 'dayjs';

type CreateStudentDialogProps = {
  open: boolean;
  onClose: () => void;
  onSuccess?: () => void;
};

const CreateStudentDialog: React.FC<CreateStudentDialogProps> = ({ open, onClose, onSuccess }) => {
  const [form] = Form.useForm<ICreateStudent>();
  const dispatch = useAppDispatch();

  const onFinish = async (values: ICreateStudent) => {
    try {
      const formattedValues: ICreateStudent = {
        ...values,
        ngaySinh: values.ngaySinh ? dayjs(values.ngaySinh).toDate() : undefined,
      };

      await dispatch(createStudent(formattedValues)).unwrap();
      toast.success('Thêm sinh viên thành công');
      onSuccess?.();
      onClose();
      form.resetFields();
    } catch {
      toast.error('Tạo sinh viên thất bại');
    }
  };

  return (
    <Modal
      title="Thêm sinh viên mới"
      open={open}
      onCancel={onClose}
      width={600}
      footer={
        <Space>
          <Button type="primary" onClick={form.submit} icon={<SaveOutlined />}>
            Lưu
          </Button>
          <Button onClick={onClose} icon={<CloseOutlined />}>
            Hủy
          </Button>
        </Space>
      }
    >
      <Form form={form} layout="vertical" onFinish={onFinish}>
        <Form.Item label="Mã số sinh viên" name="mssv" rules={[{ required: true, message: 'Vui lòng nhập MSSV!' }]}>
          <Input placeholder="Nhập mã số sinh viên" />
        </Form.Item>

        <Form.Item label="Họ đệm" name="hoDem" rules={[{ required: true, message: 'Vui lòng nhập họ đệm!' }]}>
          <Input placeholder="Nhập họ đệm" />
        </Form.Item>

        <Form.Item label="Tên" name="ten" rules={[{ required: true, message: 'Vui lòng nhập tên!' }]}>
          <Input placeholder="Nhập tên" />
        </Form.Item>

        <Form.Item label="Ngày sinh" name="ngaySinh">
          <DatePicker format="DD/MM/YYYY" style={{ width: '100%' }} />
        </Form.Item>

        <Form.Item label="Nơi sinh" name="noiSinh">
          <Input placeholder="Nhập nơi sinh" />
        </Form.Item>

        <Form.Item label="Giới tính" name="gioiTinh">
          <Select
            placeholder="Chọn giới tính"
            options={[
              { label: 'Nam', value: 1 },
              { label: 'Nữ', value: 0 },
            ]}
          />
        </Form.Item>

        <Form.Item label="Số CCCD" name="soCccd">
          <Input placeholder="Nhập số CCCD" />
        </Form.Item>

        <Form.Item label="Số điện thoại" name="soDienThoai">
          <Input placeholder="Nhập số điện thoại" />
        </Form.Item>

        <Form.Item label="Email" name="email">
          <Input placeholder="Nhập email" />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CreateStudentDialog;
