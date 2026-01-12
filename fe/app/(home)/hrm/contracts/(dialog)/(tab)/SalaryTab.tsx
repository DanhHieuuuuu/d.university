'use client';

import { useEffect } from 'react';
import { Form, Input, InputNumber } from 'antd';
import { ICreateHopDong } from '@models/nhansu/hopdong.model';

export default function SalaryTab() {
  const form = Form.useFormInstance<ICreateHopDong>();

  useEffect(() => {
    form.setFieldValue('currency', 'VNĐ');
    form.setFieldValue('payFrequency', 'Hàng tháng');
  }, []);

  return (
    <div className="grid grid-cols-2 gap-x-5">
      <Form.Item<ICreateHopDong>
        name="luongCoBan"
        label="Lương cơ bản"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <InputNumber<number>
          formatter={(value) => (value ? `${Number(value).toLocaleString('vi-VN')}` : '')}
          parser={(value) => (value ? Number(value.replace(/[₫\s.]/g, '')) : 0)}
          className="!w-full"
          min={0}
        />
      </Form.Item>
      <Form.Item label="Đơn vị">
        <Input defaultValue="VNĐ" disabled />
      </Form.Item>
      <Form.Item name="payFrequency" label="Trả lương" className="col-span-full">
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['tenNganHang1']}
        label="Tên ngân hàng"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['atm1']}
        label="Số tài khoản"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['tenNganHang2']} label="Tên ngân hàng 2">
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['atm2']} label="Số tài khoản 2">
        <Input />
      </Form.Item>
    </div>
  );
}
