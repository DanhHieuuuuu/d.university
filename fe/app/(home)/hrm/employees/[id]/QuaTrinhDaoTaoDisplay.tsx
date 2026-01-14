import { Card, Table } from 'antd';
import { IColumn } from '@models/common/table.model';

const QuaTrinhDaoTaoDisplay = ({ data }: { data: any }) => {
  const columnsDaoTao: IColumn<any>[] = [
    {
      key: 'hocViNoiHoc',
      dataIndex: 'hocViNoiHoc',
      title: 'Tên trường',
      width: 160
    },
    {
      key: 'hocViChuyenNganh',
      dataIndex: 'hocViChuyenNganh',
      title: 'Chuyên ngành đào tạo, bồi dưỡng',
      width: 180
    },
    {
      key: 'tuNgay',
      dataIndex: 'tuNgay',
      title: 'Từ',
      align: 'center',
      width: 120
    },
    {
      key: 'denNgay',
      dataIndex: 'denNgay',
      title: 'Đến',
      align: 'center',
      width: 120
    },
    {
      key: 'tenHinhThucDaoTao',
      dataIndex: 'tenHinhThucDaoTao',
      title: 'Hình thức đào tạo',
      width: 100
    },
    {
      key: 'tenHocVi',
      dataIndex: 'tenHocVi',
      title: 'Văn bằng, chứng chỉ, trình độ',
      width: 120
    }
  ];
  return (
    <Card title="Quá trình đào tạo">
      <Table
        size="small"
        bordered
        tableLayout="fixed"
        columns={columnsDaoTao}
        dataSource={data}
        rowKey="id"
        pagination={false}
        locale={{ emptyText: 'Không có dữ liệu để hiển thị' }}
      />
    </Card>
  );
};

export default QuaTrinhDaoTaoDisplay;
