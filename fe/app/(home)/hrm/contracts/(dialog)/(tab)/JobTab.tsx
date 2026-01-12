'use client';

import { useState } from 'react';
import { Checkbox, DatePicker, Form, Input, Select } from 'antd';
import { ICreateHopDong } from '@models/nhansu/hopdong.model';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { DebounceSelect } from '@components/common/DebounceSelect';
import { findNhanSuBySdtThunk } from '@redux/feature/hrm/nhansu/nhansuThunk';

const { TextArea } = Input;

export default function JobTab() {
  const form = Form.useFormInstance<ICreateHopDong>();
  const [probation, setProbation] = useState<boolean>(false);
  const { phongBan, listLoaiHopDong, chucVu, toBoMon } = useAppSelector((state) => state.danhmucState);

  const dispatch = useAppDispatch();

  const fetchNhanSuOptions = async (keyword: string) => {
    const result = await dispatch(findNhanSuBySdtThunk(keyword));

    if (findNhanSuBySdtThunk.fulfilled.match(result)) {
      return result.payload;
    }

    return [];
  };

  return (
    <div className="grid grid-cols-3 gap-x-5">
      <Form.Item<ICreateHopDong>
        name={['idNhanSu']}
        label="Ký hợp đồng với"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DebounceSelect placeholder="Nhập số điện thoại" allowClear fetchOptions={fetchNhanSuOptions}  />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['maNhanSu']}
        label="Mã nhân sự"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['maSoThue']} label="Mã số thuế">
        <Input />
      </Form.Item>

      <Form.Item<ICreateHopDong> name="soHopDong" label="Số hợp đồng">
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> name="idLoaiHopDong" label="Loại hợp đồng">
        <Select
          allowClear
          options={listLoaiHopDong?.map((item) => {
            return { label: item.tenLoaiHopDong, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name="ngayKyKet"
        label="Ngày ký kết"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker showTime needConfirm format="HH:mm - DD/MM/YYYY" className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDong> name="idPhongBan" label="Phòng ban">
        <Select
          allowClear
          options={phongBan.$list.data?.map((item) => {
            return { label: item.tenPhongBan, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDong> name="idChucVu" label="Chức vụ">
        <Select
          allowClear
          options={chucVu.$list.data?.map((item) => {
            return { label: item.tenChucVu, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDong> name="idToBoMon" label="Tổ bộ môn">
        <Select
          allowClear
          options={toBoMon.$list.data?.map((item) => {
            return { label: item.tenBoMon, value: item.id };
          })}
        />
      </Form.Item>

      <Form.Item name="hasProbation" className="col-span-full !mb-3">
        <Checkbox checked={probation} onClick={() => setProbation(!probation)}>
          Có thử việc
        </Checkbox>
      </Form.Item>
      {probation && (
        <>
          <Form.Item<ICreateHopDong> name="ngayBatDauThuViec" label="Ngày bắt đầu thử việc">
            <DatePicker needConfirm format="DD/MM/YYYY" className="!w-full" />
          </Form.Item>
          <Form.Item<ICreateHopDong> name="ngayKetThucThuViec" label="Ngày kết thúc thử việc">
            <DatePicker needConfirm format="DD/MM/YYYY" className="!w-full" />
          </Form.Item>
          <div></div>
        </>
      )}
      <Form.Item<ICreateHopDong>
        name="hopDongCoThoiHanTuNgay"
        label="Hợp đồng có thời hạn từ ngày"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker needConfirm format="DD/MM/YYYY" className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDong> name="hopDongCoThoiHanDenNgay" label="Hợp đồng có thời hạn đến ngày">
        <DatePicker needConfirm format="DD/MM/YYYY" className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDong> name="ghiChu" label="Ghi chú" className="col-span-full">
        <TextArea rows={4} />
      </Form.Item>
    </div>
  );
}
