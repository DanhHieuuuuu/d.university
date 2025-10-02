'use client';

import { useState } from 'react';
import { Checkbox, DatePicker, Form, Input, Select } from 'antd';
import { ICreateHopDongNs } from '@models/nhansu/nhansu.model';

const { TextArea } = Input;

export default function JobTab() {
  const form = Form.useFormInstance<ICreateHopDongNs>();
  const [probation, setProbation] = useState<boolean>(false);

  return (
    <div className="grid grid-cols-3 gap-x-5">
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'maNhanSu']}
        label="Mã nhân sự"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name={['thongTinNhanSu', 'maSoThue']} label="Mã số thuế">
        <Input />
      </Form.Item>
      <div></div>
      <Form.Item<ICreateHopDongNs> name="soHopDong" label="Số hợp đồng">
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name="idLoaiHopDong" label="Loại hợp đồng">
        <Select />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name="ngayKyKet"
        label="Ngày ký kết"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker showTime needConfirm format="HH:mm - DD/MM/YYYY" className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name="idPhongBan" label="Phòng ban">
        <Select />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name="idChucVu"
        label="Chức vụ"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Select />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name="idToBoMon" label="Tổ bộ môn">
        <Select />
      </Form.Item>

      <Form.Item name="hasProbation" className="col-span-full !mb-3">
        <Checkbox checked={probation} onClick={() => setProbation(!probation)}>
          Có thử việc
        </Checkbox>
      </Form.Item>
      {probation && (
        <>
          <Form.Item<ICreateHopDongNs> name="ngayBatDauThuViec" label="Ngày bắt đầu thử việc">
            <DatePicker needConfirm format="DD/MM/YYYY" className="!w-full" />
          </Form.Item>
          <Form.Item<ICreateHopDongNs> name="ngayKetThucThuViec" label="Ngày kết thúc thử việc">
            <DatePicker needConfirm format="DD/MM/YYYY" className="!w-full" />
          </Form.Item>
          <div></div>
        </>
      )}
      <Form.Item<ICreateHopDongNs>
        name="hopDongCoThoiHanTuNgay"
        label="Hợp đồng có thời hạn từ ngày"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker needConfirm format="DD/MM/YYYY" className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name="hopDongCoThoiHanDenNgay" label="Hợp đồng có thời hạn đến ngày">
        <DatePicker needConfirm format="DD/MM/YYYY" className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name="ghiChu" label="Ghi chú" className="col-span-full">
        <TextArea rows={4} />
      </Form.Item>
    </div>
  );
}
