'use client';

import { Form, Input, DatePicker, Select, InputNumber } from 'antd';
import { ICreateNhanSu } from '@models/nhansu/nhansu.model';
import { useAppSelector } from '@redux/hooks';

export default function PersonalTab() {
  const form = Form.useFormInstance<ICreateNhanSu>();
  const { listGioiTinh, listQuocTich, listDanToc, listTonGiao } = useAppSelector((state) => state.danhmucState);

  return (
    <div className="grid grid-cols-4 gap-x-5">
      <Form.Item<ICreateNhanSu>
        name={['hoDem']}
        label="Họ đệm"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateNhanSu>
        name={['ten']}
        label="Tên"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateNhanSu> name={['gioiTinh']} label="Giới tính">
        <Select
          allowClear
          showSearch
          optionFilterProp="label"
          options={listGioiTinh?.map((item) => {
            return { label: item.tenGioiTinh, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateNhanSu>
        name={['ngaySinh']}
        label="Ngày sinh"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker format="DD/MM/YYYY" needConfirm className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateNhanSu> name={['quocTich']} label="Quốc tịch">
        <Select
          allowClear
          showSearch
          optionFilterProp="label"
          options={listQuocTich?.map((item) => {
            return { label: item.tenQuocGia, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateNhanSu>
        name={['noiSinh']}
        label="Nơi sinh"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input placeholder="Nhập tỉnh / thành phố" />
      </Form.Item>
      <Form.Item<ICreateNhanSu> name={['danToc']} label="Dân tộc">
        <Select
          allowClear
          showSearch
          optionFilterProp="label"
          options={listDanToc?.map((item) => {
            return { label: item.tenDanToc, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateNhanSu> name={['tonGiao']} label="Tôn giáo">
        <Select
          allowClear
          showSearch
          optionFilterProp="label"
          options={listTonGiao?.map((item) => {
            return { label: item.tenTonGiao, value: item.id };
          })}
        />
      </Form.Item>
      <Form.Item<ICreateNhanSu>
        name={['soCccd']}
        label="Số CMND / CCCD"
        rules={[
          { required: true, message: 'Không được để trống!' },
          { len: 12, message: 'CCCD phải là 12 số' },
          { pattern: /^(\d{12})$/, message: 'CCCD phải toàn ký tự số' }
        ]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateNhanSu>
        name={['ngayCapCccd']}
        label="Ngày cấp CMND / CCCD"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <DatePicker format="DD/MM/YYYY" needConfirm className="!w-full" />
      </Form.Item>
      <Form.Item<ICreateNhanSu> name={['noiCapCccd']} label="Nơi cấp CMND / CCCD">
        <Input />
      </Form.Item>
      <div></div>
      <Form.Item<ICreateNhanSu>
        name={['email']}
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
      <Form.Item<ICreateNhanSu>
        name={['soDienThoai']}
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
      <Form.Item<ICreateNhanSu> name={['nguyenQuan']} label="Quê quán">
        <Input />
      </Form.Item>
      <div></div>
      <Form.Item<ICreateNhanSu> name={['chieuCao']} label="Chiều cao">
        <InputNumber className="!w-full" step={0.1} min={0} />
      </Form.Item>
      <Form.Item<ICreateNhanSu> name={['canNang']} label="Cân nặng">
        <InputNumber className="!w-full" step={0.1} min={0} />
      </Form.Item>
      <Form.Item<ICreateNhanSu> name={['nhomMau']} label="Nhóm máu">
        <Input />
      </Form.Item>
      <Form.Item<ICreateNhanSu>
        name={['noiOHienTai']}
        label="Hộ khẩu thường trú"
        className="col-span-full"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input />
      </Form.Item>
      <Form.Item<ICreateNhanSu>
        name={['khanCapNguoiLienHe']}
        label="Người liên hệ khẩn cấp"
        className="col-span-2"
        rules={[{ required: true, message: 'Không được để trống!' }]}
      >
        <Input placeholder="Nhập tên người liên hệ khẩn cấp" />
      </Form.Item>
      <Form.Item<ICreateNhanSu>
        name={['khanCapSoDienThoai']}
        label="Số điện thoại khẩn cấp"
        className="col-span-2"
        rules={[
          { required: true, message: 'Không được để trống!' },
          {
            pattern: /(?:\+84|0084|0)[235789][0-9]{1,2}[0-9]{7}(?:[^\d]+|$)/g,
            message: 'Số điện thoại không đúng định dạng'
          }
        ]}
      >
        <Input placeholder="Nhập số điện thoại người liên hệ khẩn cấp" />
      </Form.Item>
    </div>
  );
}
