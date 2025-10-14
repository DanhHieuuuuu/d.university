'use client';

import { Form, Input, DatePicker, Select } from 'antd';
import { ICreateHopDongNs } from '@models/nhansu/nhansu.model';
import { useAppSelector } from '@redux/hooks';

export default function PersonalTab() {
  const form = Form.useFormInstance<ICreateHopDongNs>();
  const { listGioiTinh, listQuocTich, listDanToc, listTonGiao } = useAppSelector((state) => state.danhmucState);

  return (
    <div className="grid grid-cols-4 gap-x-5">
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'hoDem']}
        label="Họ đệm"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'ten']}
        label="Tên"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name={['thongTinNhanSu', 'gioiTinh']} label="Giới tính">
        <Select
          options={listGioiTinh?.map((item) => {
            return { label: item.tenGioiTinh, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'ngaySinh']}
        label="Ngày sinh"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker format="DD/MM/YYYY" needConfirm className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name={['thongTinNhanSu', 'quocTich']} label="Quốc tịch">
        <Select
          options={listQuocTich?.map((item) => {
            return { label: item.tenQuocGia, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'noiSinh']}
        label="Nơi sinh"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input placeholder="Nhập tính / thành phố" />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name={['thongTinNhanSu', 'danToc']} label="Dân tộc">
        <Select
          options={listDanToc?.map((item) => {
            return { label: item.tenDanToc, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name={['thongTinNhanSu', 'tonGiao']} label="Tôn giáo">
        <Select
          options={listTonGiao?.map((item) => {
            return { label: item.tenTonGiao, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'soCccd']}
        label="Số CMND / CCCD"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'ngayCapCccd']}
        label="Ngày cấp CMND / CCCD"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker format="DD/MM/YYYY" needConfirm className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name={['thongTinNhanSu', 'noiCapCccd']} label="Nơi cấp CMND / CCCD">
        <Input />
      </Form.Item>
      <div></div>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'email']}
        label="Email cá nhân"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'soDienThoai']}
        label="Số điện thoại"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs> name={['thongTinNhanSu', 'nguyenQuan']} label="Quê quán">
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'noiOHienTai']}
        label="Hộ khẩu thường trú"
        className="col-span-full"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'khanCapNguoiLienHe']}
        label="Liên hệ khẩn cấp"
        className="col-span-2"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input placeholder="Nhập tên người liên hệ khẩn cấp" />
      </Form.Item>
      <Form.Item<ICreateHopDongNs>
        name={['thongTinNhanSu', 'khanCapSoDienThoai']}
        label="Số điện thoại khẩn cấp"
        className="col-span-2"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input placeholder="Nhập số điện thoại người liên hệ khẩn cấp" />
      </Form.Item>
    </div>
  );
}
