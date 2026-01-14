'use client';

import { useEffect } from 'react';
import { toast } from 'react-toastify';
import { Breadcrumb, Button, Card, Dropdown, Empty, MenuProps } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { formatDateView } from '@utils/index';

import { ReduxStatus } from '@redux/const';
import { getHoSoNhanSu } from '@redux/feature/hrm/nhansu/nhansuThunk';
import { selectedNhanSu, selectedNhanSuStatus } from '@redux/feature/hrm/nhansu/nhansuSelectors';

import HoSoNhanSuLoading from './loading';
import QuaTrinhDaoTaoDisplay from './QuaTrinhDaoTaoDisplay';
import QuaTrinhCongTacDisplay from './QuaTrinhCongTacDisplay';
import QuanHeGiaDinhDisplay from './QuanHeGiaDinhDisplay';
import '@styles/hrm/ho-so-ns.style.scss';

const breadcrumbItems = [
  {
    title: 'Trang chủ',
    href: '/home'
  },
  {
    title: 'Danh sách nhân sự',
    href: '/hrm/employees'
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

  useEffect(() => {
    if (!Number.isNaN(idNhanSu)) {
      dispatch(getHoSoNhanSu(Number.parseInt(idNhanSu)));
    }
  }, [dispatch, idNhanSu]);

  if (status === ReduxStatus.LOADING) {
    return <HoSoNhanSuLoading />;
  }

  if (status === ReduxStatus.FAILURE || !nhansu) {
    return <Empty description="Không tìm thấy nhân sự" />;
  }

  const itemsDropdown: MenuProps['items'] = [
    {
      key: '1',
      label: 'Thông tin lý lịch 2A',
      onClick: () => {
        toast.info('Click download form 2A');
      }
    },
    {
      key: '2',
      label: 'Thông tin lý lịch 2C',
      onClick: () => {
        toast.info('Click download form 2B');
      }
    },
    {
      key: '3',
      label: 'Sơ yếu lý lịch TT-BNV',
      onClick: () => {
        toast.info('Click download form 2C');
      }
    }
  ];

  return (
    <div className="ho-so-ns-page">
      <Breadcrumb separator=">" items={breadcrumbItems} />
      <div className="content">
        <div className="header">
          <p className="header">Hồ sơ nhân sự</p>
          <Dropdown menu={{ items: itemsDropdown }}>
            <Button type="primary" icon={<DownloadOutlined />}>
              Xuất mẫu hồ sơ
            </Button>
          </Dropdown>
        </div>

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

          <Card title="Thông tin khác">
            <div className="grid grid-cols-4 gap-4">
              <div className="field">
                <p className="label">Học hàm:</p>
                <p className="value">{nhansu.hocHam}</p>
              </div>
              <div className="field">
                <p className="label">Chuyên ngành học hàm:</p>
                <p className="value">{nhansu.chuyenNganhHocHam}</p>
              </div>
              <div className="field">
                <p className="label">Học vị:</p>
                <p className="value">{nhansu.tenHocVi}</p>
              </div>
              <div className="field">
                <p className="label">Chuyên ngành học vị:</p>
                <p className="value">{nhansu.chuyenNganhHocVi}</p>
              </div>
              <div className="field">
                <p className="label">Lý luận chính trị:</p>
                <p className="value">{nhansu.hocHam}</p>
              </div>
              <div className="field">
                <p className="label">Quản lý nhà nước:</p>
                <p className="value">{nhansu.hocHam}</p>
              </div>
              <div className="field">
                <p className="label">Ngoại ngữ:</p>
                <p className="value">{nhansu.hocHam}</p>
              </div>
              <div className="field">
                <p className="label">Tin học:</p>
                <p className="value">{nhansu.hocHam}</p>
              </div>
              <div className="field">
                <p className="label">Ngày vào ĐCS Việt Nam:</p>
                <p className="value">{nhansu.hocHam}</p>
              </div>
              <div className="field">
                <p className="label">Ngày chính thức:</p>
                <p className="value">{nhansu.hocHam}</p>
              </div>
              <div className="field">
                <p className="label">Ngày nhập ngũ:</p>
                <p className="value">{nhansu.hocHam}</p>
              </div>
              <div className="field">
                <p className="label">Ngày xuất ngũ:</p>
                <p className="value">{nhansu.hocHam}</p>
              </div>
            </div>
          </Card>

          <QuaTrinhDaoTaoDisplay data={nhansu.quaTrinhDaoTao} />

          <QuaTrinhCongTacDisplay data={nhansu.quaTrinhCongTac} />

          <QuanHeGiaDinhDisplay data={nhansu.thongTinGiaDinh} />
        </div>
      </div>
    </div>
  );
};

export default Page;
