import { Card, Table } from 'antd';
import { IColumn } from '@models/common/table.model';
import { IVIewNsQuaTrinhCongTac } from '@models/nhansu/quaTrinhCongTac.model';
import { formatDateView } from '@utils/index';

const QuaTrinhCongTacDisplay = ({ data }: { data: any }) => {
  const columnsCongTac: IColumn<IVIewNsQuaTrinhCongTac>[] = [
    {
      key: 'tuNgay',
      dataIndex: 'tuNgay',
      title: 'Từ',
      align: 'center',
      width: 120,
      render: (value) => formatDateView(value)
    },
    {
      key: 'denNgay',
      dataIndex: 'denNgay',
      title: 'Đến',
      align: 'center',
      width: 120,
      render: (value) => (value ? formatDateView(value) : '-')
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: 'Mô tả',
      render: (value) => <p dangerouslySetInnerHTML={{ __html: value }}></p>
    }
  ];

  return (
    <Card title="Quá trình công tác">
      <Table
        size="small"
        bordered
        tableLayout="fixed"
        columns={columnsCongTac}
        dataSource={data}
        rowKey="id"
        pagination={false}
        locale={{ emptyText: 'Không có dữ liệu để hiển thị' }}
      />
    </Card>
  );
};

export default QuaTrinhCongTacDisplay;
