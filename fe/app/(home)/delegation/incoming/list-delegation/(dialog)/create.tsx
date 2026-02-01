'use client';

import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Form, FormProps, Input, Modal, Select, DatePicker, InputNumber, Row, Col, Upload } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined, UploadOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateDoanVao, IUpdateDoanVao } from '@models/delegation/delegation.model';
import { createDoanVao, updateDoanVao } from '@redux/feature/delegation/delegationThunk';
import { clearSelected } from '@redux/feature/delegation/delegationSlice';
import { DelegationStatusConst } from '../../../../../../constants/core/delegation/delegation-status.consts';
import type { UploadProps } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';
import { downloadDelegationTemplateExcel } from '@utils/file-download.helper';

type DoanVaoModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
};

const CreateDoanVaoModal: React.FC<DoanVaoModalProps> = ({ isModalOpen, setIsModalOpen, isUpdate, isView }) => {
  const dispatch = useAppDispatch();
  const { selected, $create, listPhongBan, listNhanSu } = useAppSelector((state) => state.delegationState);
  const [form] = Form.useForm<ICreateDoanVao>();
  
  const [title, setTitle] = useState<string>('');
  const [excelFile, setExcelFile] = useState<File | null>(null);
  const requestDate = Form.useWatch('requestDate', form);
  const idPhongBan = Form.useWatch('idPhongBan', form);

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
  const filteredNhanSu = idPhongBan ? listNhanSu.filter((ns: any) => ns.idPhongBan === idPhongBan) : [];
  useEffect(() => {
    if (idPhongBan) {
      form.setFieldValue('idStaffReception', undefined);
    }
  }, [idPhongBan]);
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
    try {
      if (!isUpdate && !excelFile) {
        toast.error('Vui lòng upload file');
        return;
      }

      const formData = new FormData();

      formData.append('Code', values.code);
      formData.append('Name', values.name);
      formData.append('Content', values.content ?? '');
      formData.append('IdPhongBan', values.idPhongBan.toString() ?? '0');
      formData.append('IdStaffReception', values.idStaffReception.toString() ?? '0');
      formData.append('Location', values.location ?? '');
      formData.append('PhoneNumber', values.phoneNumber ?? '');
      formData.append('RequestDate', dayjs(values.requestDate).format('YYYY-MM-DD'));
      formData.append('ReceptionDate', dayjs(values.receptionDate).format('YYYY-MM-DD'));
      formData.append('TotalMoney', values.totalMoney?.toString() ?? '0');
      formData.append('TotalPerson', values.totalPerson?.toString() ?? '0');
      if (excelFile) {
        formData.append('DetailDelegation', excelFile, excelFile.name);
      }

      if (isUpdate && selected.data) {
        formData.append('Id', selected.data.id.toString());
        await dispatch(updateDoanVao(formData)).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createDoanVao(formData)).unwrap();
        toast.success('Thêm mới thành công');
      }

      onCloseModal();
    } catch (err) {
      toast.error(String(err));
    }
  };

  const uploadProps: UploadProps = {
    beforeUpload: (file) => {
      setExcelFile(file as File);
      return false;
    },
    maxCount: 1
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
            loading={$create.status === ReduxStatus.LOADING}
          >
            {isUpdate ? 'Lưu' : 'Tạo mới'}
          </Button>
        ),
        <Button key="close" onClick={onCloseModal} icon={<CloseOutlined />} disabled={$create.status === ReduxStatus.LOADING}>
          Đóng
        </Button>
      ]}
      style={{
        top: 0,
        height: '95vh',
      }}
      styles={{
        body: {
          maxHeight: 'calc(95vh - 55px - 52px)',
          overflowY: 'auto',
          paddingRight: '16px'
        }
      }}
    >
      <Form form={form} layout="vertical" onFinish={onFinish} disabled={isView}>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item label="Mã đoàn" name="code" rules={[{ required: true, message: 'Vui lòng nhập mã đoàn' }]}>
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="Tên đoàn vào" name="name" rules={[{ required: true, message: 'Vui lòng nhập tên đoàn' }]}>
              <Input />
            </Form.Item>
          </Col>
        </Row>

        <Row gutter={16}>
          <Col span={8}>
            <Form.Item label="Phòng ban phụ trách" name="idPhongBan" rules={[{ required: true, message: 'Vui lòng nhập phòng ban phụ trách' }]}>
              <Select options={listPhongBan.map((pb: any) => ({ value: pb.idPhongBan, label: pb.tenPhongBan }))} />
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item label="Địa điểm" name="location">
              <Input />
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item label="Nhân sự tiếp đón" name="idStaffReception" rules={[{ required: true,  message: 'Vui lòng nhập nhân sự tiếp đón'}]}>
              <Select
                disabled={!idPhongBan}
                placeholder={idPhongBan ? 'Chọn nhân sự' : 'Chọn phòng ban trước'}
                options={filteredNhanSu.map((ns: any) => ({
                  value: ns.idNhanSu,
                  label: `${ns.tenNhanSu} - ${ns.supporterCode}`
                }))}
              />
            </Form.Item>
          </Col>
        </Row>

        <Row gutter={16}>
          <Col span={8}>
            <Form.Item label="Tổng chi phí ước tính (VNĐ)" name="totalMoney">
              <InputNumber style={{ width: '100%' }} formatter={(value) => (value ? `${value}` : '')} />
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item label="SĐT liên hệ" name="phoneNumber" rules={[
                          { required: true, message: 'Số điện thoại liên hệ không được để trống' },
                          {
                            pattern: /^(0[3|5|7|8|9])[0-9]{8}$/,
                            message: 'Số điện thoại không hợp lệ'
                          }
                        ]}>
              <Input />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item label="Ngày yêu cầu" name="requestDate" rules={[{ required: true, message: 'Vui lòng nhập ngày yêu cầu' }]}>
              <DatePicker style={{ width: '100%' }} disabledDate={(current) => current && current < dayjs().startOf('day')}/>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="Ngày tiếp đón" name="receptionDate" rules={[{ required: true, message: 'Vui lòng nhập ngày tiếp đón' }]}>
              <DatePicker
                disabled={!requestDate}
                style={{ width: '100%' }}
                disabledDate={(current) => {
                  if (!requestDate) return false;
                  return current.isBefore(dayjs(requestDate), 'day');
                }}
              />
            </Form.Item>
          </Col>
        </Row>
        <Form.Item label="Nội dung" name="content">
          <Input.TextArea rows={2} />
        </Form.Item>
        {!isUpdate && !isView && (
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item label="Danh sách thành viên">
                <Upload {...uploadProps}>
                  <Button
                    icon={<UploadOutlined />}
                    style={{
                      background: '#69b1ff',
                      borderColor: '#69b1ff',
                      color: '#fff'
                    }}
                  >
                    Upload file
                  </Button>
                </Upload>
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item label="Tải mẫu thành viên tham gia">
                <Button
                  icon={<DownloadOutlined />}
                  onClick={downloadDelegationTemplateExcel}
                  style={{
                    background: '#52c41a',
                    borderColor: '#52c41a',
                    color: '#fff'
                  }}
                  type="default"
                >
                  Dowload file
                </Button>
              </Form.Item>
            </Col>
          </Row>
        )}
      </Form>
    </Modal>
  );
};

export default CreateDoanVaoModal;
