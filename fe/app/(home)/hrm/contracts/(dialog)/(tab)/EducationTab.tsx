import { ICreateHopDong } from '@models/nhansu/hopdong.model';
import { DatePicker, Form, Input, Select } from 'antd';

const EducationTab = () => {
  const form = Form.useFormInstance<ICreateHopDong>();

  return (
    <div className="grid grid-cols-2 gap-x-5">
      <Form.Item<ICreateHopDong> label="Trình độ giáo dục phổ thông" name={['thongTinNhanSu', 'trinhDoHocVan']}>
        <Select
          allowClear
          options={[
            { value: '6/12', label: '6/12' },
            { value: '7/12', label: '7/12' },
            { value: '8/12', label: '8/12' },
            { value: '9/12', label: '9/12' },
            { value: '10/12', label: '10/12' },
            { value: '11/12', label: '11/12' },
            { value: '12/12', label: '12/12' }
          ]}
        />
      </Form.Item>
      <Form.Item<ICreateHopDong> label="Trình độ ngoại ngữ" name={['thongTinNhanSu', 'trinhDoNgoaiNgu']}>
        <Input placeholder="Tiếng Anh - B1, Tiếng Trung - HSK2, ..." />
      </Form.Item>
      <Form.Item<ICreateHopDong> label="Học vị" name={['thongTinNhanSu', 'tenHocVi']}>
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> label="Chuyên ngành học vị" name={['thongTinNhanSu', 'tenChuyenNganhHocVi']}>
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> label="Học hàm" name={['thongTinNhanSu', 'tenHocHam']}>
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> label="Chuyên ngành học hàm" name={['thongTinNhanSu', 'tenChuyenNganhHocHam']}>
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> label="Ngày vào Đảng Cộng sản Việt Nam" name={['thongTinNhanSu', 'ngayVaoDang']}>
        <DatePicker allowClear format="DD/MM/YYYY" className="!w-full" />
      </Form.Item>
    </div>
  );
};

export default EducationTab;
