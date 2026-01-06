'use client';
import { toast } from 'react-toastify';
import React, { useEffect } from 'react';
import { Button, Form, Input, Modal, Space, DatePicker, Select } from 'antd';
import { CloseOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { createStudent, updateStudent } from '@redux/feature/studentSlice';
import { ICreateStudent, IUpdateStudent, IViewStudent } from '@models/student/student.model';
import { formatDateCallApi } from '@utils/index';
import { ReduxStatus } from '@redux/const';
import dayjs from 'dayjs';

type StudentDialogProps = {
  open: boolean;
  onClose: () => void;
  onSuccess?: () => void;
  student?: IViewStudent | null;
  isView?: boolean;
};

const StudentDialog: React.FC<StudentDialogProps> = ({
  open,
  onClose,
  onSuccess,
  student = null,
  isView = false
}) => {
  const [form] = Form.useForm<ICreateStudent>();
  const dispatch = useAppDispatch();

  const { $create, $update } = useAppSelector((state) => state.studentState);
  const { listGioiTinh, listQuocTich, listDanToc, listTonGiao } = useAppSelector((s) => s.danhmucState);

  const isLoading =
    $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;
  const title = student ? (isView ? 'Xem sinh viên' : 'Cập nhật sinh viên') : 'Thêm sinh viên mới';

  useEffect(() => {
    if (open && student) {
      form.setFieldsValue({
        mssv: student.mssv,
        hoDem: student.hoDem,
        ten: student.ten,
        ngaySinh: student.ngaySinh ? dayjs(student.ngaySinh) : undefined,
        noiSinh: student.noiSinh,
        gioiTinh: student.gioiTinh,
        quocTich: student.quocTich,
        danToc: student.danToc,
        tonGiao: student.tonGiao,
        nguyenQuan: student.nguyenQuan,
        noiOHienTai: student.noiOHienTai,
        soCccd: student.soCccd,
        soDienThoai: student.soDienThoai,
        email: student.email
      });
    } else if (open && !student) {
      form.resetFields();
    }
  }, [open, student, form]);

  const onFinish = (values: ICreateStudent) => {
    const formattedDate = values.ngaySinh ? (formatDateCallApi(values.ngaySinh) as any) : undefined;

    if (student?.idStudent) {
      const updatePayload: Partial<IUpdateStudent> = {
        idStudent: student.idStudent,
        hoDem: values.hoDem,
        ten: values.ten,
        ngaySinh: formattedDate,
        noiSinh: values.noiSinh,
        gioiTinh: values.gioiTinh,
        quocTich: values.quocTich,
        danToc: values.danToc,
        tonGiao: values.tonGiao,
        nguyenQuan: values.nguyenQuan,
        noiOHienTai: values.noiOHienTai,
        soCccd: values.soCccd,
        soDienThoai: values.soDienThoai,
        email: values.email,
        mssv: values.mssv,
        khoaHoc: values.khoaHoc,
        khoa: values.khoa,
        nganh: values.nganh,
        chuyenNganh: values.chuyenNganh
      };

      dispatch(updateStudent(updatePayload))
        .unwrap()
        .then(() => {
          toast.success('Cập nhật sinh viên thành công');
          onSuccess?.();
          onClose();
          form.resetFields();
        })
        .catch(() => {
          toast.error('Cập nhật sinh viên thất bại');
        });
    } else {
      const createPayload: ICreateStudent = {
        ...values,
        ngaySinh: formattedDate,
        mssv: values.mssv
      };

      dispatch(createStudent(createPayload))
        .unwrap()
        .then(() => {
          toast.success('Thêm sinh viên thành công');
          onSuccess?.();
          onClose();
          form.resetFields();
        })
        .catch(() => {
          toast.error('Tạo sinh viên thất bại');
        });
    }
  };

  return (
    <Modal
      title={title}
      open={open}
      onCancel={onClose}
      width={900}
      footer={
        <Space>
          {!isView && (
            <Button
              type="primary"
              onClick={() => form.submit()}
              icon={<SaveOutlined />}
              loading={isLoading}
            >
              {student ? 'Cập nhật' : 'Lưu'}
            </Button>
          )}
          <Button onClick={onClose} icon={<CloseOutlined />}>
            {isView ? 'Đóng' : 'Hủy'}
          </Button>
        </Space>
      }
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={onFinish}
        autoComplete="off"
        disabled={isView}
      >
        <div className="grid grid-cols-4 gap-x-5">
          <Form.Item
            label="MSSV"
            name="mssv"
            rules={[{ required: true, message: 'Không được để trống!' }]}
          >
            <Input placeholder="Nhập MSSV" disabled={!!student} />
          </Form.Item>
          <Form.Item
            label="Họ đệm"
            name="hoDem"
            rules={[{ required: true, message: 'Không được để trống!' }]}
          >
            <Input placeholder="Nhập họ đệm" />
          </Form.Item>

          <Form.Item
            label="Tên"
            name="ten"
            rules={[{ required: true, message: 'Không được để trống!' }]}
          >
            <Input placeholder="Nhập tên" />
          </Form.Item>

          <Form.Item label="Ngày sinh" name="ngaySinh">
            <DatePicker format="DD/MM/YYYY" style={{ width: '100%' }} />
          </Form.Item>

          <Form.Item label="Nơi sinh" name="noiSinh">
            <Input placeholder="Nhập tỉnh/thành phố" />
          </Form.Item>

          <Form.Item label="Giới tính" name="gioiTinh">
            <Select
              placeholder="Chọn giới tính"
              options={(listGioiTinh || []).map((i: any) => ({
                key: i.id,
                label: i.tenGioiTinh,
                value: i.id
              }))}
            />
          </Form.Item>

          <Form.Item label="Quốc tịch" name="quocTich">
            <Select
              placeholder="Chọn quốc tịch"
              options={(listQuocTich || []).map((i: any) => ({
                key: i.id,
                label: i.tenQuocGia,
                value: i.id
              }))}
            />
          </Form.Item>

          <Form.Item label="Dân tộc" name="danToc">
            <Select
              placeholder="Chọn dân tộc"
              options={(listDanToc || []).map((i: any) => ({
                key: i.id,
                label: i.tenDanToc,
                value: i.id
              }))}
            />
          </Form.Item>

          <Form.Item label="Tôn giáo" name="tonGiao">
            <Select
              placeholder="Chọn tôn giáo"
              options={(listTonGiao || []).map((i: any) => ({
                key: i.id,
                label: i.tenTonGiao,
                value: i.id
              }))}
            />
          </Form.Item>

          <Form.Item label="Nguyên quán" name="nguyenQuan">
            <Input placeholder="Nhập nguyên quán" />
          </Form.Item>

          <Form.Item label="Nơi ở hiện tại" name="noiOHienTai">
            <Input placeholder="Nhập nơi ở hiện tại" />
          </Form.Item>

          <Form.Item label="Số CCCD" name="soCccd">
            <Input placeholder="Nhập số CCCD" maxLength={12} />
          </Form.Item>

          <Form.Item
            label="Số điện thoại"
            name="soDienThoai"
            rules={[
              {
                pattern: /^(0|\+84)(\d{9})$/,
                message: 'Số điện thoại không hợp lệ!'
              }
            ]}
          >
            <Input placeholder="Nhập số điện thoại" maxLength={11} />
          </Form.Item>

          <Form.Item
            label="Email"
            name="email"
            rules={[
              { type: 'email', message: 'Email không hợp lệ' },
              { required: true, message: 'Vui lòng nhập email!' }
            ]}
          >
            <Input placeholder="Nhập email" />
          </Form.Item>

          
        </div>
      </Form>
    </Modal>
  );
};

export default StudentDialog;
