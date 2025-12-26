'use client';

import { Button, DatePicker, Form, Input, Select } from 'antd';
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { ICreateNhanSu } from '@models/nhansu/nhansu.model';
import { useAppSelector } from '@redux/hooks';

export default function FamilyTab() {
  const form = Form.useFormInstance<ICreateNhanSu>();
  const { listQuanHe, listQuocTich } = useAppSelector((state) => state.danhmucState);

  return (
    <Form.List name={['thongTinGiaDinh']}>
      {(fields, { add, remove }) => (
        <div>
          {fields.map((field, index) => (
            <div key={field.key} className="mb-3 rounded-lg p-2" style={{ border: '1px solid #0fd' }}>
              <div className="grid grid-cols-4 gap-x-5">
                <Form.Item
                  name={[field.name, 'hoTen']}
                  label="Họ tên nhân thân"
                  rules={[{ required: true, message: 'Không được để trống!' }]}
                >
                  <Input />
                </Form.Item>
                <Form.Item
                  name={[field.name, 'quanHe']}
                  label="Mối quan hệ"
                  rules={[{ required: true, message: 'Không được để trống!' }]}
                >
                  <Select
                    showSearch
                    options={listQuanHe?.map((item) => {
                      return { label: item.tenQuanHe, value: item.id };
                    })}
                  />
                </Form.Item>
                <Form.Item
                  name={[field.name, 'ngaySinh']}
                  label="Ngày sinh"
                  rules={[{ required: true, message: 'Không được để trống!' }]}
                >
                  <DatePicker format="DD/MM/YYYY" className="!w-full" />
                </Form.Item>
                <Form.Item name={[field.name, 'quocTich']} label="Quốc tịch">
                  <Select
                    showSearch
                    options={listQuocTich?.map((item) => {
                      return { label: item.tenQuocGia, value: item.id };
                    })}
                  />
                </Form.Item>
                <Form.Item name={[field.name, 'queQuan']} label="Quê quán">
                  <Input />
                </Form.Item>
                <Form.Item name={[field.name, 'soDienThoai']} label="Số điện thoại">
                  <Input />
                </Form.Item>
                <Form.Item name={[field.name, 'ngheNghiep']} label="Nghề nghiệp">
                  <Input />
                </Form.Item>
                <Form.Item name={[field.name, 'donViCongTac']} label="Đơn vị công tác">
                  <Input />
                </Form.Item>
              </div>
              <div className="flex justify-end">
                <Button type="primary" danger onClick={() => remove(field.name)} icon={<DeleteOutlined />}>
                  Xóa
                </Button>
              </div>
            </div>
          ))}

          <Button type="primary" color="primary" onClick={() => add()} icon={<PlusOutlined />}>
            Thêm người thân
          </Button>
        </div>
      )}
    </Form.List>
  );
}
