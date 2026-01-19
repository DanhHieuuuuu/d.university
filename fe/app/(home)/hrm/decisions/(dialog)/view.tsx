'use client';

import React, { useEffect, useState } from 'react';
import { Button, Form, Input, Modal, Select, DatePicker, Table, Tabs, Tag } from 'antd';
import { CloseOutlined } from '@ant-design/icons';
import { formatDateTimeView } from '@utils/index';
import dayjs from 'dayjs';

import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { IColumn } from '@models/common/table.model';
import { ETableColumnType } from '@/constants/e-table.consts';
import { IDetailQuyetDinh, IViewQuyetDinhLog } from '@models/nhansu/quyetdinh.model';
import { NsQuyetDinhTypeConst } from '@/constants/core/hrm/quyet-dinh-type.const';
import { NsQuyetDinhStatusConst } from '@/constants/core/hrm/quyet-dinh-status.const';

const { TextArea } = Input;

type ViewQuyetDinhModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
};

const ViewQuyetDinhModal: React.FC<ViewQuyetDinhModalProps> = (props) => {
  const { selected } = useAppSelector((state) => state.quyetdinhState);
  const [form] = Form.useForm<IDetailQuyetDinh>();
  const [activeKey, setActiveKey] = useState<string>('1');

  useEffect(() => {
    if (!props.isModalOpen || !selected.data) return;

    const data = selected.data;
    form.setFieldsValue({
      ...data,
      ngayHieuLuc: data.ngayHieuLuc ? dayjs(data.ngayHieuLuc) : null
    });

    if (props.isModalOpen) {
      setActiveKey('1');
    }
  }, [props.isModalOpen, selected.data]);

  const onCloseModal = () => {
    form.resetFields();
    props.setIsModalOpen(false);
  };

  return (
    <Modal
      title="Chi tiết quyết định"
      open={props.isModalOpen}
      onCancel={onCloseModal}
      width={800}
      centered
      maskClosable={false}
      closable={true}
      footer={[
        <Button key="close" onClick={onCloseModal} icon={<CloseOutlined />}>
          Đóng
        </Button>
      ]}
      height={600}
    >
      <Tabs
        type="card"
        activeKey={activeKey}
        onChange={(key) => setActiveKey(key)}
        items={[
          {
            key: '1',
            label: 'Thông tin',
            children: (
              <Form form={form} layout="vertical" disabled={true}>
                <div className="grid grid-cols-2 gap-2">
                  <Form.Item<IDetailQuyetDinh> label="Loại quyết định" name="loaiQuyetDinh">
                    <Select options={NsQuyetDinhTypeConst.options} />
                  </Form.Item>

                  <Form.Item<IDetailQuyetDinh> label="Trạng thái" name="status">
                    <Select options={NsQuyetDinhStatusConst.options} />
                  </Form.Item>
                </div>

                <Form.Item<IDetailQuyetDinh> label="Nhân sự" name="idNhanSu">
                  <Select />
                </Form.Item>
                <Form.Item<IDetailQuyetDinh> label="Áp dụng từ" name="ngayHieuLuc">
                  <DatePicker format="DD/MM/YYYY HH:mm" className="!w-full" />
                </Form.Item>
                <Form.Item<IDetailQuyetDinh> label="Nội dung" name="noiDungTomTat">
                  <TextArea rows={3} />
                </Form.Item>
              </Form>
            )
          },
          {
            key: '2',
            label: 'Lịch sử',
            children: <HistoryTab data={selected.data?.history} />
          }
        ]}
      />
    </Modal>
  );
};

export default ViewQuyetDinhModal;

const HistoryTab = ({ data }: { data: any }) => {
  const colums: IColumn<IViewQuyetDinhLog>[] = [
    {
      key: 'stt',
      dataIndex: 'stt',
      title: 'STT',
      render: (_, record, index) => index + 1
    },
    {
      key: 'oldStatus',
      dataIndex: 'oldStatus',
      title: 'Trạng thái cũ',
      align: 'center',
      type: ETableColumnType.STATUS,
      render: (value) => {
        const _status = NsQuyetDinhStatusConst.getTag(value);
        return (
          <Tag bordered={false} className={_status?.className}>
            {_status?.label}
          </Tag>
        );
      }
    },
    {
      key: 'newStatus',
      dataIndex: 'newStatus',
      title: 'Trạng thái mới',
      align: 'center',
      type: ETableColumnType.STATUS,
      render: (value) => {
        const _status = NsQuyetDinhStatusConst.getTag(value);
        return (
          <Tag bordered={false} className={_status?.className}>
            {_status?.label}
          </Tag>
        );
      }
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: 'Mô tả'
    },
    {
      key: 'createdDate',
      dataIndex: 'createdDate',
      title: 'Thời gian',
      render: (value) => formatDateTimeView(value)
    }
  ];
  return (
    <Table
      className="mb-6"
      size="small"
      bordered
      columns={colums}
      dataSource={data}
      rowKey="id"
      pagination={false}
      locale={{ emptyText: 'Không có dữ liệu để hiển thị' }}
    />
  );
};
