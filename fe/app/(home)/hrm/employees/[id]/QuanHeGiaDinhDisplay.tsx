import { Card, Table } from "antd";
import { IColumn } from "@models/common/table.model";
import { IViewNsQuanHe } from "@models/nhansu/quanHeGiaDinh.model";
import { formatDateView } from "@utils/index";

const QuanHeGiaDinhDisplay = ({data} : {data: any}) => {
  
  

  const columsQuanHe: IColumn<IViewNsQuanHe>[] = [
    {
      key: 'tenQuanHe',
      dataIndex: 'tenQuanHe',
      title: 'Mối quan hệ'
    },
    {
      key: 'hoTen',
      dataIndex: 'hoTen',
      title: 'Họ tên'
    },
    {
      key: 'ngaySinh',
      dataIndex: 'ngaySinh',
      title: 'Ngày sinh',
      align: 'center',
      render: (value) => formatDateView(value)
    },
    {
      key: 'queQuan',
      dataIndex: 'queQuan',
      title: 'Quê quán',
      align: 'center',
      ellipsis: true,
      width: 120
    },
    {
      key: 'soDienThoai',
      dataIndex: 'soDienThoai',
      title: 'Số diện thoại',
      align: 'center'
    },
    {
      key: 'ngheNghiep',
      dataIndex: 'ngheNghiep',
      title: 'Nghề nghiệp'
    },
    {
      key: 'donViCongTac',
      dataIndex: 'donViCongTac',
      title: 'Đơn vị công tác',
      ellipsis: true,
      width: 150
    }
  ];
  return (
    <Card title="Quan hệ gia đình">
      <Table
        size="small"
        bordered
        columns={columsQuanHe}
        dataSource={data}
        rowKey="quanHe"
        pagination={false}
        locale={{ emptyText: 'Không có dữ liệu để hiển thị' }}
      />
    </Card>
  );
}
 
export default QuanHeGiaDinhDisplay;