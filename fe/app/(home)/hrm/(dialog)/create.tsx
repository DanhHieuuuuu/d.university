'use client';

import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, FormProps, Modal } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';

import { useAppSelector } from '@redux/hooks';
import { ICreateNhanSu } from '@models/nhansu/nhansu.model';

type NhanSuModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
};

const CreateNhanSuModal: React.FC<NhanSuModalProps> = (props) => {
  const [form] = Form.useForm<ICreateNhanSu>();
  const [title, setTitle] = useState<string>('');
  const { selected } = useAppSelector((state) => state.nhanSuState);

  useEffect(() => {
    if (props.isModalOpen) {
      if (props.isUpdate) {
        if (selected) {
          setTitle('Chỉnh sủa');
          initData();
        }
      } else if (props.isView) {
        if (selected) {
          setTitle('Chi tiết');
          initData();
        }
      } else {
        setTitle('Thêm mới');
      }
    }
    return () => form.resetFields();
  }, [props.isModalOpen]);

  const initData = () => {
    form.setFieldsValue({ ...selected });
  };

  const onFinish: FormProps<ICreateNhanSu>['onFinish'] = (values) => {
    // if (props.isUpdate) {
    //   JobPositionService.update(props.selectedId || '', {
    //     id: props.selectedId,
    //     ...values
    //   }).then(() => {
    //     toast.success('Cập nhật thành công');
    //     props.refreshData();
    //     props.setIsModalOpen(false);
    //   });
    // } else {
    //   JobPositionService.create({
    //     ...values
    //   }).then(() => {
    //     toast.success('Thêm mới thành công');
    //     props.refreshData();
    //     props.setIsModalOpen(false);
    //   });
    // }
    if (props.isUpdate) {
      toast.success('Cập nhật thành công');
      props.setIsModalOpen(false);
    } else {
      toast.success('Thêm mới thành công');
      props.setIsModalOpen(false);
    }
  };

  return (
    <Modal
      title={title}
      className="app-modal"
      width="60%"
      closable={{ 'aria-label': 'Custom Close Button' }}
      open={props.isModalOpen}
      onOk={() => props.setIsModalOpen(true)}
      onCancel={() => props.setIsModalOpen(false)}
      footer={() => (
        <>
          {!props.isView && (
            <Button onClick={form.submit} icon={props.isUpdate ? <SaveOutlined /> : <PlusOutlined />} type="primary">
              {props.isUpdate ? 'Lưu' : 'Tạo mới'}
            </Button>
          )}
          <Button color="default" variant="filled" onClick={() => props.setIsModalOpen(false)} icon={<CloseOutlined />}>
            Đóng
          </Button>
        </>
      )}
    >
      <Form
        name="documentType"
        layout="vertical"
        form={form}
        onFinish={onFinish}
        autoComplete="on"
        disabled={props.isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        xxxxx
      </Form>
    </Modal>
  );
};

export default CreateNhanSuModal;
