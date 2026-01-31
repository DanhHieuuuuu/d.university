'use client';

import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, FormProps, Input, Modal, Select, DatePicker, InputNumber, Row, Col, Upload } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined, UploadOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateDepartment } from '@models/delegation/delegation.model';
import { clearSelected } from '@redux/feature/delegation/delegationSlice';
import { createDepartmentSupport } from '@redux/feature/delegation/department/departmentThunk';

type DepartmentSupportModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
};

const CreateDepartmentSupportModal: React.FC<DepartmentSupportModalProps> = ({
  isModalOpen,
  setIsModalOpen,
  isUpdate,
  isView
}) => {
  const dispatch = useAppDispatch();
  const { selected, listPhongBan, listDelegationIncoming } = useAppSelector((state) => state.delegationState);
  const { selectedDelegationIncomingId } = useAppSelector((state) => state.departmentState);
  const [form] = Form.useForm<ICreateDepartment>();
  const [title, setTitle] = useState<string>('');

  useEffect(() => {
    if (isModalOpen) {
      if (isView && selected.data) {
        setTitle('Chi tiết phòng ban hỗ trợ');
        initData();
      } else {
        setTitle('Thêm phòng ban hỗ trợ');
        form.resetFields();
      }
    }
  }, [isModalOpen, selected, isView]);
  useEffect(() => {
  if (isModalOpen && selectedDelegationIncomingId) {
    form.setFieldsValue({
      delegationIncomingId: selectedDelegationIncomingId
    });
  }
}, [isModalOpen, selectedDelegationIncomingId, form]);

  const initData = () => {
    if (!selected.data) return;

    form.setFieldsValue({
        departmentSupportIds: selected.data.departmentSupportId
      ? [selected.data.departmentSupportId]
      : [],
      delegationIncomingId: selected.data.delegationIncomingId,
      content: selected.data.content
    });
  };

  const onCloseModal = () => {
    dispatch(clearSelected());
    form.resetFields();
    setIsModalOpen(false);
  };
  const onFinish: FormProps<ICreateDepartment>['onFinish'] = async (values) => {
    try {
      const payload: ICreateDepartment = {
        ...values,
        content: values.content?.trim() || ''
      };
      await dispatch(createDepartmentSupport(payload)).unwrap();
      toast.success('Thêm phòng ban thành công');
      onCloseModal();
    } catch (err) {
      toast.error(String(err));
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
        height: '80vh'
      }}
      styles={{
        body: {
          maxHeight: 'calc(80vh - 55px - 52px)',
          overflowY: 'auto',
          paddingRight: '16px'
        }
      }}
    >
      <Form form={form} layout="vertical" onFinish={onFinish} disabled={isView}>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Phòng ban hỗ trợ"
              name="departmentSupportIds"
              rules={[{ required: true, message: 'Chọn phòng ban' }]}
            >
              <Select
                mode="multiple"
                allowClear
                placeholder="Chọn phòng ban hỗ trợ"
                options={listPhongBan.map((pb: any) => ({
                  value: pb.idPhongBan,
                  label: pb.tenPhongBan
                }))}
              />
            </Form.Item>
          </Col>

          <Col span={12}>
            <Form.Item
              label="Đoàn vào"
              name="delegationIncomingId"
              rules={[{ required: true, message: 'Chọn đoàn vào' }]}
            >
              <Select
                disabled={!!selectedDelegationIncomingId}
                options={listDelegationIncoming.map((d: any) => ({
                  value: d.idDelegationIncoming,
                  label: `${d.tenDoanVao} - ${d.delegationIncomingCode}`
                }))}
              />
            </Form.Item>
          </Col>
        </Row>

        <Form.Item label="Nội dung" name="content" required>
          <Input.TextArea rows={3} />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CreateDepartmentSupportModal;
