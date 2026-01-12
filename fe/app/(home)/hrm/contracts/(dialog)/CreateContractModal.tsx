'use client';

import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, FormProps, Modal, Tabs } from 'antd';
import { CloseOutlined, PlusOutlined } from '@ant-design/icons';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateHopDong } from '@models/nhansu/hopdong.model';
import { clearSelected } from '@redux/feature/hrm/nhansu/nhansuSlice';
import { JobTab, SalaryTab } from './(tab)';
import { createHopDong } from '@redux/feature/hrm/hopdong/hopdongThunk';

type ContractModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
};

const CreateContractModal: React.FC<ContractModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const { selected, $create } = useAppSelector((state) => state.hopdongState);

  const [form] = Form.useForm<ICreateHopDong>();
  const [tabActive, setTabActive] = useState<string>('job');

  useEffect(() => {
    if (props.isModalOpen) {
      setTabActive('job');
    }

    return () => {
      setTabActive('');
    };
  }, [props.isModalOpen]);

  const tabItems = [
    {
      key: 'job',
      label: 'Thông tin vị trí làm việc',
      children: <JobTab />
    },
    {
      key: 'salary',
      label: 'Mức lương',
      children: <SalaryTab />
    }
  ];

  const onCloseModal = () => {
    dispatch(clearSelected());
    form.resetFields();
    props.setIsModalOpen(false);
  };

  const onOkModal = () => {
    props.setIsModalOpen(true);
  };

  const onFinish: FormProps<ICreateHopDong>['onFinish'] = async (values) => {
    console.log(values);

    await dispatch(createHopDong(values));

    if ($create.status === ReduxStatus.SUCCESS) {
      toast.success('Thêm mới thành công');
    }
    onCloseModal();
  };

  return (
    <Modal
      title="Thêm hợp đồng mới"
      className="app-modal"
      width="80%"
      closable={{ 'aria-label': 'Custom Close Button' }}
      open={props.isModalOpen}
      onOk={onOkModal}
      onCancel={onCloseModal}
      footer={() => (
        <>
          <Button onClick={form.submit} icon={<PlusOutlined />} type="primary">
            Tạo mới
          </Button>
          <Button color="default" variant="filled" onClick={onCloseModal} icon={<CloseOutlined />}>
            Đóng
          </Button>
        </>
      )}
    >
      <Form
        name="hopDongNhanSu"
        layout="vertical"
        form={form}
        onFinish={onFinish}
        autoComplete="on"
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <Tabs type="card" items={tabItems} activeKey={tabActive} onChange={(key) => setTabActive(key)} />
      </Form>
    </Modal>
  );
};

export default CreateContractModal;
