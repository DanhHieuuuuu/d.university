'use client';

import { useEffect } from 'react';
import { Breadcrumb, Card, Empty, Typography } from 'antd';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { formatDateView } from '@utils/index';

import { ReduxStatus } from '@redux/const';
import { getHoSoNhanSu } from '@redux/feature/nhansu/nhansuThunk';
import { selectedNhanSu, selectedNhanSuStatus } from '@redux/feature/nhansu/nhansuSelectors';

import HoSoNhanSuLoading from './loading';
import '@styles/hrm/ho-so-ns.style.scss';

const breadcrumbItems = [
  {
    title: 'Trang chủ',
    href: '/home'
  },
  {
    title: 'Danh sách nhân sự',
    href: '/hrm'
  },
  {
    title: 'Hồ sơ'
  }
];

const Page = ({ params }: { params: { id: string } }) => {
  const idNhanSu = params.id;
  const dispatch = useAppDispatch();
  const nhansu = useAppSelector(selectedNhanSu);
  const status = useAppSelector(selectedNhanSuStatus);

  if (status === ReduxStatus.LOADING) {
    return <HoSoNhanSuLoading />;
  }

  if (!nhansu) {
    return <Empty description="Không tìm thấy nhân sự" />;
  }

  return (
    <div className="ho-so-ns-page">
      <Breadcrumb separator=">" items={breadcrumbItems} />
      <div className="content">
        <Typography.Title level={3} className="header">
          Mẫu - Hồ sơ nhân sự
        </Typography.Title>
        <div className="container">
          <Card title="Thông tin cá nhân">
            <div className="grid grid-cols-4 gap-4">
              <div className="field">
                <p className="label">Mã nhân sự:</p>
                <p className="value">{nhansu.maNhanSu}</p>
              </div>
              <div className="field">
                <p className="label">Họ và tên:</p>
                <p className="value">{nhansu.hoDem}</p>
              </div>
              <div className="field">
                <p className="label">Ngày sinh:</p>
                <p className="value">{formatDateView(nhansu.ngaySinh)}</p>
              </div>
              <div className="field">
                <p className="label">Giới tính:</p>
                <p className="value">{nhansu.gioiTinh}</p>
              </div>
              <div className="field">
                <p className="label">Nơi sinh:</p>
                <p className="value">{nhansu.noiSinh}</p>
              </div>
              <div className="field">
                <p className="label">Quê quán:</p>
                <p className="value">{nhansu.nguyenQuan}</p>
              </div>
              <div className="field">
                <p className="label">Nơi đăng ký hộ khẩu:</p>
                <p className="value">{nhansu.hoKhau}</p>
              </div>
              <div className="field">
                <p className="label">Dân tộc:</p>
                <p className="value">{nhansu.danToc}</p>
              </div>
              <div className="field">
                <p className="label">Chiều cao:</p>
                <p className="value">{nhansu.chieuCao}</p>
              </div>
              <div className="field">
                <p className="label">Cân nặng:</p>
                <p className="value">{nhansu.canNang}</p>
              </div>
              <div className="field col-span-2">
                <p className="label">Nhóm máu:</p>
                <p className="value">{nhansu.nhomMau}</p>
              </div>
              <div className="field">
                <p className="label">Số CCCD:</p>
                <p className="value">{nhansu.soCccd}</p>
              </div>
              <div className="field">
                <p className="label">Ngày cấp:</p>
                <p className="value">{formatDateView(nhansu.ngayCapCccd)}</p>
              </div>
              <div className="field col-span-2">
                <p className="label">Nơi cấp:</p>
                <p className="value">{nhansu.noiCapCccd}</p>
              </div>
            </div>
          </Card>
          <Card title="Thông tin liên lạc">
            <div className="grid grid-cols-2 gap-2">
              <div className="field">
                <p className="label">Email:</p>
                <p className="value">{nhansu.email}</p>
              </div>
              <div className="field">
                <p className="label">Điện thoại:</p>
                <p className="value">{nhansu.soDienThoai}</p>
              </div>
              <div className="field">
                <p className="label">Địa chỉ hiện tại:</p>
                <p className="value">{nhansu.noiOHienTai}</p>
              </div>
            </div>
          </Card>
          <Card title="Thông tin khác"></Card>
          <Card title="Quá trình đào tạo"></Card>
          <Card title="Quá trình công tác"></Card>
          <Card title="Quan hệ gia đình"></Card>
        </div>
      </div>
    </div>
  );
};

export default Page;
