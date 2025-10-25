'use client';
import { toast } from 'react-toastify';
import React, { useState } from 'react';
import { Button, Form, Input, Modal, Space, DatePicker, Select } from 'antd';
import { CloseOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { createStudent } from '@redux/feature/studentSlice';
import { ICreateStudent } from '@models/student/student.model';
import dayjs from 'dayjs';

type CreateStudentDialogProps = {
  open: boolean;
  onClose: () => void;
};

const CreateStudentDialog: React.FC<CreateStudentDialogProps> = ({ open, onClose }) => {
  const [form] = Form.useForm<ICreateStudent>();
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(false);

  const { listGioiTinh, listQuocTich, listDanToc } = useAppSelector(
    (s) => s.danhmucState
  );

  const onFinish = async (values: ICreateStudent) => {
    setLoading(true);
    try {
      const payload: ICreateStudent = {
        ...values,
        // Convert ngày sinh nếu có
        ngaySinh: values.ngaySinh ? dayjs(values.ngaySinh).toDate() : undefined,
      };

      await dispatch(createStudent(payload)).unwrap();
      toast.success('Thêm sinh viên thành công');
      onClose();
      form.resetFields();
    } catch {
      toast.error('Tạo sinh viên thất bại');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Modal
      title="Thêm sinh viên mới"
      open={open}
      onCancel={onClose}
      width={900}
      footer={
        <Space>
          <Button type="primary" onClick={() => form.submit()} icon={<SaveOutlined />} loading={loading}>
            Lưu
          </Button>
          <Button onClick={onClose} icon={<CloseOutlined />}>
            Hủy
          </Button>
        </Space>
      }
    >
      <Form form={form} layout="vertical" onFinish={onFinish} autoComplete="off">
        <div className="grid grid-cols-4 gap-x-5">
          <Form.Item
            label="Họ đệm"
            name="hoDem"
            rules={[{ required: true, message: 'Không được để trống!' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Tên"
            name="ten"
            rules={[{ required: true, message: 'Không được để trống!' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Ngày sinh"
            name="ngaySinh"
          >
            <DatePicker format="DD/MM/YYYY" style={{ width: '100%' }} />
          </Form.Item>

          <Form.Item
            label="Nơi sinh"
            name="noiSinh"
          >
            <Input placeholder="Nhập tỉnh/thành phố" />
          </Form.Item>

          <Form.Item
            label="Giới tính"
            name="gioiTinh"
          >
            <Select
              placeholder="Chọn giới tính"
              options={(listGioiTinh || []).map((i: any) => ({ label: i.tenGioiTinh || i.name || i.label, value: i.id }))}
            />
          </Form.Item>

          <Form.Item
            label="Quốc tịch"
            name="quocTich"
          >
            <Select
              placeholder="Chọn quốc tịch"
              options={listQuocTich?.map((item) => {
                return { label: item.tenQuocGia, value: item.id };
              })}
            />
          </Form.Item>

          <Form.Item
            label="Dân tộc"
            name="danToc"
          >
            <Select
            placeholder="Chọn dân tộc"
              options={listDanToc?.map((item) => {
                return { label: item.tenDanToc, value: item.id };
              })}
            />
          </Form.Item>

          <Form.Item
            label="Số CCCD"
            name="soCccd"
          >
            <Input placeholder="Nhập số CCCD" />
          </Form.Item>

          <Form.Item
            label="Số điện thoại"
            name="soDienThoai"
          >
            <Input placeholder="Nhập số điện thoại" />
          </Form.Item>

          <Form.Item
            label="Email"
            name="email"
            rules={[{ type: 'email', message: 'Email không hợp lệ' }]}
          >
            <Input placeholder="Nhập email" />
          </Form.Item>

        </div>
      </Form>
    </Modal>
  );
};

export default CreateStudentDialog;
