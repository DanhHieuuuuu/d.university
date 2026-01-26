'use client';

import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, FormProps, Modal, Tabs } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateNhanSu, IUpdateNhanSu } from '@models/nhansu/nhansu.model';
import { clearSelected, resetStatusCreate, resetStatusUpdate } from '@redux/feature/hrm/nhansu/nhansuSlice';
import { createNhanSuThunk, updateNhanSuThunk } from '@redux/feature/hrm/nhansu/nhansuThunk';
import { EducationTab, FamilyTab, PersonalTab } from './(tab-ns)';

type NhanSuModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
};

const CreateNhanSuModal: React.FC<NhanSuModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const { selected, $create, $update } = useAppSelector((state) => state.nhanSuState);

  const [form] = Form.useForm<ICreateNhanSu>();
  const [title, setTitle] = useState<string>('');
  const [tabActive, setTabActive] = useState<string>('personal');

  useEffect(() => {
    if (props.isModalOpen) {
      if (props.isUpdate) setTitle('Chỉnh sửa');
      else if (props.isView) setTitle('Chi tiết');
      else setTitle('Thêm mới');

      setTabActive('personal');
    }
  }, [props.isModalOpen, props.isUpdate, props.isView]);

  useEffect(() => {
    if (props.isModalOpen && selected.status === ReduxStatus.SUCCESS && selected.data) {
      if (props.isUpdate) {
        const rawData = selected.data;

        form.setFieldsValue({
          ...rawData,
          ngaySinh: rawData.ngaySinh ? dayjs(rawData.ngaySinh) : null,
          ngayCapCccd: rawData.ngayCapCccd ? dayjs(rawData.ngayCapCccd) : null,
          thongTinGiaDinh: rawData.thongTinGiaDinh?.map((member: any) => ({
            ...member,
            ngaySinh: member.ngaySinh ? dayjs(member.ngaySinh) : null
          }))
        });
      } else {
        form.resetFields();
      }
    }
  }, [props.isModalOpen, selected.status, selected.data, form]);

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
      key: 'education',
      label: 'Học vấn',
      children: <EducationTab />
    }
  ];

  const onCloseModal = () => {
    dispatch(clearSelected());
    dispatch(resetStatusCreate());
    dispatch(resetStatusUpdate());
    form.resetFields();
    props.setIsModalOpen(false);
  };

  const onOkModal = () => {
    props.setIsModalOpen(true);
  };

  const onFinish: FormProps<ICreateNhanSu>['onFinish'] = async (values) => {
    if (props.isUpdate) {
      const body: IUpdateNhanSu = {
        idNhanSu: selected.idNhanSu,
        ...values
      };

      await dispatch(updateNhanSuThunk(body));

      if ($update.status === ReduxStatus.SUCCESS) {
        toast.success('Cập nhật thành công');
      }
      onCloseModal();
    } else {
      await dispatch(createNhanSuThunk(values));

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
        name="createUpdateNhanSu"
        layout="vertical"
        form={form}
        onFinish={onFinish}
        autoComplete="on"
        disabled={props.isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <Tabs type="card" items={tabItems} activeKey={tabActive} onChange={(key) => setTabActive(key)} />
      </Form>
    </Modal>
  );
};

export default CreateNhanSuModal;
