'use client';

import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, FormProps, Modal, Tabs } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateHopDongNs } from '@models/nhansu/nhansu.model';
import { clearSelected } from '@redux/feature/nhansu/nhansuSlice';
import { createNhanSu, getDetailNhanSu } from '@redux/feature/nhansu/nhansuThunk';
import { FamilyTab, JobTab, PersonalTab, SalaryTab } from './(tab)';

type NhanSuModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
};

const CreateNhanSuModal: React.FC<NhanSuModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const { selected, $create } = useAppSelector((state) => state.nhanSuState);

  const [form] = Form.useForm<ICreateHopDongNs>();
  const [title, setTitle] = useState<string>('');

  useEffect(() => {
    if (props.isModalOpen) {
      if (props.isUpdate) {
        if (selected) {
          setTitle('Chỉnh sửa');
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
  }, [props.isModalOpen]);

  const tabItems = [
    {
      key: 'personal',
      label: 'Thông tin cá nhân',
      children: <PersonalTab />
    },
    {
      key: 'family',
      label: 'Thông tin gia đình',
      children: <FamilyTab />
    },
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

  const initData = async () => {
    await dispatch(getDetailNhanSu(selected.idNhanSu));

    if (selected.status === ReduxStatus.SUCCESS) {
      console.log(selected.data);
      form.setFieldsValue(selected.data);
    }
  };

  const onCloseModal = () => {
    dispatch(clearSelected());
    form.resetFields();
    props.setIsModalOpen(false);
  };

  const onOkModal = () => {
    props.setIsModalOpen(true);
  };

  const onFinish: FormProps<ICreateHopDongNs>['onFinish'] = async (values) => {
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
      console.log('All form data:', values);
      onCloseModal();
    } else {
      console.log(values);

      await dispatch(createNhanSu(values));
      if ($create.status === ReduxStatus.SUCCESS) {
        toast.success('Thêm mới thành công');
      }
      onCloseModal();
    }
  };

  return (
    <Modal
      title={title}
      className="app-modal"
      width="80%"
      closable={{ 'aria-label': 'Custom Close Button' }}
      open={props.isModalOpen}
      onOk={onOkModal}
      onCancel={onCloseModal}
      footer={() => (
        <>
          {!props.isView && (
            <Button onClick={form.submit} icon={props.isUpdate ? <SaveOutlined /> : <PlusOutlined />} type="primary">
              {props.isUpdate ? 'Lưu' : 'Tạo mới'}
            </Button>
          )}
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
        disabled={props.isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <Tabs type="card" items={tabItems} />
      </Form>
    </Modal>
  );
};

export default CreateNhanSuModal;
