'use client';
import { Modal, Table, Tag } from 'antd';
import { KpiTrangThaiConst } from '@/constants/kpi/kpiStatus.const';
import { KpiLogStatusDto, KpiLogStatusResponse } from '@models/kpi/kpi-log.model';

interface KpiLogModalProps {
  open: boolean;
  onCancel: () => void;
  data: KpiLogStatusDto[];
  loading?: boolean;
}

const KpiLogModal = ({ open, onCancel, data, loading }: KpiLogModalProps) => {
  return (
    <Modal open={open} onCancel={onCancel} footer={null} width={1200} title="Nhật ký KPI">
      <Table
        rowKey="id"
        loading={loading}
        dataSource={data}
        pagination={false}
        scroll={{ x: 900 }}
        columns={[
          { title: 'Thời gian', dataIndex: 'createdDate', width: 180 },
          { title: 'Người thao tác', dataIndex: 'createdByName', width: 180 },
          { title: 'Nội dung', dataIndex: 'description' },
          {
            title: 'Trạng thái cũ',
            dataIndex: 'oldStatus',
            key: 'oldStatus',
            render: (value?: number) => {
              const status = KpiTrangThaiConst.get(value);
              return status ? <Tag color={status.color}>{status.text}</Tag> : '-';
            }
          },
          {
            title: 'Trạng thái mới',
            dataIndex: 'newStatus',
            key: 'newStatus',
            render: (value?: number) => {
              const status = KpiTrangThaiConst.get(value);
              return status ? <Tag color={status.color}>{status.text}</Tag> : '-';
            }
          },
          { title: 'Lý do', dataIndex: 'reason' }
        ]}
      />
    </Modal>
  );
};

export default KpiLogModal;
