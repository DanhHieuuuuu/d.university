'use client'

import { Form, Input, InputNumber } from 'antd';
import { ICreateHopDongNs } from '@models/nhansu/nhansu.model';

export default function SalaryTab() {
  const form = Form.useFormInstance<ICreateHopDongNs>();

  return (
    <div className="grid grid-cols-2 gap-x-5">
      <Form.Item<ICreateHopDongNs>
        name="luongCoBan"
        label="Lương cơ bản"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <InputNumber className="!w-full" min={0} />
      </Form.Item>
      <Form.Item name="currency" label="Đơn vị">
        <Input defaultValue="VNĐ" disabled />
      </Form.Item>
      <Form.Item name="payFrequency" label="Trả lương" className="col-span-full">
        <Input defaultValue="Hàng tháng" />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'tenNganHang1']}
        label="Tên ngân hàng"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'atm1']}
        label="Số tài khoản"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name={['thongTinNhanSu', 'tenNganHang2']} label="Tên ngân hàng 2">
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name={['thongTinNhanSu', 'atm2']} label="Số tài khoản 2">
        <Input />
      </Form.Item>
    </div>
  );
}
