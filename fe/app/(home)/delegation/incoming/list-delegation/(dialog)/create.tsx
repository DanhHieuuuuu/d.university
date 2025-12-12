'use client';

import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, FormProps, Input, Modal, Select, DatePicker, InputNumber, Row, Col } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateDoanVao, IUpdateDoanVao } from '@models/delegation/delegation.model';
import { createDoanVao, updateDoanVao } from '@redux/feature/delegation/delegationThunk';
import { clearSelected } from '@redux/feature/delegation/delegationSlice';
import { DelegationStatusConst } from '../../../consts/delegation-status.consts';

type DoanVaoModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
};

const CreateDoanVaoModal: React.FC<DoanVaoModalProps> = ({ isModalOpen, setIsModalOpen, isUpdate, isView }) => {
  const dispatch = useAppDispatch();
  const { selected, $create, listPhongBan, listStatus } = useAppSelector((state) => state.delegationState);
  const [form] = Form.useForm<ICreateDoanVao>();
  const [title, setTitle] = useState<string>('');

  useEffect(() => {
    if (isModalOpen) {
      if (isUpdate && selected.data) {
        setTitle('Chỉnh sửa đoàn vào');
        initData();
      } else if (isView && selected.data) {
        setTitle('Chi tiết đoàn vào');
        initData();
      } else {
        setTitle('Thêm mới đoàn vào');
        form.resetFields();
      }
    }
  }, [isModalOpen, selected, isUpdate, isView]);

  const initData = async () => {
    if (selected.data) {
      const data = selected.data;
      form.setFieldsValue({
        ...data,
        requestDate: dayjs(data.requestDate),
        receptionDate: dayjs(data.receptionDate)
      });
    }
  };

  const onCloseModal = () => {
    dispatch(clearSelected());
    form.resetFields();
    setIsModalOpen(false);
  };

  const onFinish: FormProps<ICreateDoanVao>['onFinish'] = async (values) => {
    const payload = {
      ...values,
      requestDate: values.requestDate ? (values.requestDate as unknown as dayjs.Dayjs).format('YYYY-MM-DD') : '',
      receptionDate: values.receptionDate ? (values.receptionDate as unknown as dayjs.Dayjs).format('YYYY-MM-DD') : ''
    };

    try {
      if (isUpdate && selected.data) {
        const updatePayload: IUpdateDoanVao = {
          ...payload,
          id: selected.data.id
        };

        await dispatch(updateDoanVao(updatePayload)).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createDoanVao(payload)).unwrap();
        toast.success('Thêm mới thành công');
      }

      onCloseModal();
    } catch (error: any) {
      toast.error('Lỗi khi lưu dữ liệu');
    }
  };

  return (
    <Modal
      title={title}
      open={isModalOpen}
      onCancel={onCloseModal}
      width={800}
      centered
      maskClosable={false}
      closable={true}
      footer={[
        !isView && (
          <Button
            key="submit"
            type="primary"
            onClick={form.submit}
            icon={isUpdate ? <SaveOutlined /> : <PlusOutlined />}
          >
            {isUpdate ? 'Lưu' : 'Tạo mới'}
          </Button>
        ),
        <Button key="close" onClick={onCloseModal} icon={<CloseOutlined />}>
          Đóng
        </Button>
      ]}
      style={{
        top: 0,
        height: '85vh'
      }}
      bodyStyle={{
        maxHeight: 'calc(85vh - 55px - 52px)',
        overflowY: 'auto',
        paddingRight: '16px'
      }}
    >
      <Form form={form} layout="vertical" onFinish={onFinish} disabled={isView}>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item label="Mã đoàn" name="code" rules={[{ required: true, message: 'Nhập mã đoàn' }]}>
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="Tên đoàn vào" name="name" rules={[{ message: 'Nhập tên đoàn' }]}>
              <Input />
            </Form.Item>
          </Col>
        </Row>

        <Form.Item label="Nội dung" name="content">
          <Input.TextArea rows={2} />
        </Form.Item>

        <Row gutter={16}>
          <Col span={8}>
            <Form.Item label="Phòng ban phụ trách" name="idPhongBan" rules={[{ required: true }]}>
              <Select options={listPhongBan.map((pb: any) => ({ value: pb.idPhongBan, label: pb.tenPhongBan }))} />
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item label="Địa điểm" name="location">
              <Input />
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item label="Nhân sự tiếp đón" name="idStaffReception" rules={[{ required: true }]}>
              <InputNumber style={{ width: '100%' }} />
            </Form.Item>
          </Col>
        </Row>

        <Row gutter={16}>
          <Col span={8}>
            <Form.Item label="Tổng chi phí" name="totalMoney">
              <InputNumber style={{ width: '100%' }} formatter={(value) => (value ? `${value}` : '')} />
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item label="Tổng số người" name="totalPerson">
              <InputNumber style={{ width: '100%' }} />
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item label="SĐT liên hệ" name="phoneNumber">
              <Input />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item label="Ngày yêu cầu" name="requestDate" rules={[{ required: true }]}>
              <DatePicker style={{ width: '100%' }} />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="Ngày tiếp đón" name="receptionDate" rules={[{ required: true }]}>
              <DatePicker style={{ width: '100%' }} />
            </Form.Item>
          </Col>
        </Row>
      </Form>
    </Modal>
  );
};

export default CreateDoanVaoModal;
