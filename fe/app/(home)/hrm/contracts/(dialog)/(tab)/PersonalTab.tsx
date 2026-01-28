'use client';

import { Form, Input, DatePicker, Select, InputNumber } from 'antd';
import { useAppSelector } from '@redux/hooks';
import { ICreateHopDong } from '@models/nhansu/hopdong.model';

export default function PersonalTab() {
  const form = Form.useFormInstance<ICreateHopDong>();
  const { listGioiTinh, listQuocTich, listDanToc, listTonGiao } = useAppSelector((state) => state.danhmucState);

  return (
    <div className="grid grid-cols-4 gap-x-5">
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'hoDem']}
        label="Họ đệm"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'ten']}
        label="Tên"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['thongTinNhanSu', 'gioiTinh']} label="Giới tính">
        <Select
          allowClear
          options={listGioiTinh?.map((item) => {
            return { label: item.tenGioiTinh, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'ngaySinh']}
        label="Ngày sinh"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker format="DD/MM/YYYY" needConfirm className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['thongTinNhanSu', 'quocTich']} label="Quốc tịch">
        <Select
          allowClear
          options={listQuocTich?.map((item) => {
            return { label: item.tenQuocGia, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'noiSinh']}
        label="Nơi sinh"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input placeholder="Nhập tỉnh / thành phố" />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['thongTinNhanSu', 'danToc']} label="Dân tộc">
        <Select
          allowClear
          options={listDanToc?.map((item) => {
            return { label: item.tenDanToc, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['thongTinNhanSu', 'tonGiao']} label="Tôn giáo">
        <Select
          allowClear
          options={listTonGiao?.map((item) => {
            return { label: item.tenTonGiao, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'soCccd']}
        label="Số CMND / CCCD"
        rules={[
          { required: true, message: 'Không được để trống!' },
          { len: 12, message: 'Số CCCD phải đủ 12 số' }
        ]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'ngayCapCccd']}
        label="Ngày cấp CMND / CCCD"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker format="DD/MM/YYYY" needConfirm className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['thongTinNhanSu', 'noiCapCccd']} label="Nơi cấp CMND / CCCD">
        <Input />
      </Form.Item>
      <div></div>
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'email']}
        label="Email cá nhân"
        rules={[
          { required: true, message: 'Không được để trống!' },
          {
            pattern: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/g,
            message: 'Email chưa đúng định dạng'
          }
        ]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'soDienThoai']}
        label="Số điện thoại"
        rules={[
          { required: true, message: 'Không được để trống!' },
          {
            pattern: /(?:\+84|0084|0)[235789][0-9]{1,2}[0-9]{7}(?:[^\d]+|$)/g,
            message: 'Số điện thoại không đúng định dạng'
          }
        ]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['thongTinNhanSu', 'nguyenQuan']} label="Quê quán">
        <Input />
      </Form.Item>
      <div></div>
      <Form.Item<ICreateHopDong> name={['thongTinNhanSu', 'chieuCao']} label="Chiều cao">
        <InputNumber className="!w-full" step={0.1} min={0} />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['thongTinNhanSu', 'canNang']} label="Cân nặng">
        <InputNumber className="!w-full" step={0.1} min={0} />
      </Form.Item>
      <Form.Item<ICreateHopDong> name={['thongTinNhanSu', 'nhomMau']} label="Nhóm máu">
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'noiOHienTai']}
        label="Hộ khẩu thường trú"
        className="col-span-full"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateHopDong>
        name={['thongTinNhanSu', 'khanCapNguoiLienHe']}
        label="Liên hệ khẩn cấp"
        className="col-span-2"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input placeholder="Nhập tên người liên hệ khẩn cấp" />
      </Form.Item>
      <Form.Item<ICreateHopDong>
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
