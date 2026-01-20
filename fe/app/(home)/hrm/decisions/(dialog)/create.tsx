'use client';

import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, FormProps, Input, Modal, Select, DatePicker, InputNumber, Row, Col, Upload } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined, UploadOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateQuyetDinh, IViewQuyetDinh } from '@models/nhansu/quyetdinh.model';
import { NsQuyetDinhTypeConst } from '@/constants/core/hrm/quyet-dinh-type.const';
import { NsQuyetDinhStatusConst } from '@/constants/core/hrm/quyet-dinh-status.const';

const { TextArea } = Input;

type DoanVaoModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
};

const CreateQuyetDinhModal: React.FC<DoanVaoModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const { $create } = useAppSelector((state) => state.quyetdinhState);
  const [form] = Form.useForm<ICreateQuyetDinh>();

  const onCloseModal = () => {
    form.resetFields();
    props.setIsModalOpen(false);
  };

  const onFinish: FormProps<ICreateQuyetDinh>['onFinish'] = async (values) => {};

  return (
    <Modal
      title="Thêm mới quyết định"
      open={props.isModalOpen}
      onCancel={onCloseModal}
      width={800}
      centered
      maskClosable={false}
      closable={true}
      footer={
        <>
          <Button key="submit" type="primary" onClick={form.submit} icon={<PlusOutlined />}>
            Tạo mới
          </Button>

          <Button key="close" onClick={onCloseModal} icon={<CloseOutlined />}>
            Đóng
          </Button>
        </>
      }
    >
      <Form form={form} layout="vertical" onFinish={onFinish}>
        <Form.Item<ICreateQuyetDinh> label="Loại quyết định" name="loaiQuyetDinh">
          <Select allowClear options={NsQuyetDinhTypeConst.options} />
        </Form.Item>
        <Form.Item<ICreateQuyetDinh> label="Nhân sự" name="idNhanSu">
          <Select allowClear />
        </Form.Item>
        <Form.Item<ICreateQuyetDinh>
          label="Áp dụng từ"
          name="ngayHieuLuc"
          rules={[{ required: true, message: 'Không được để trống!' }]}
        >
          <DatePicker showTime format="DD/MM/YYYY HH:mm" needConfirm className="!w-full" />
        </Form.Item>
        <Form.Item<ICreateQuyetDinh> label="Nội dung" name="noiDungTomTat">
          <TextArea rows={3} />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CreateQuyetDinhModal;
