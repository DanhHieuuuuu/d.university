import { MenuProps } from 'antd';
import { GlobalOutlined, HomeOutlined, TeamOutlined } from '@ant-design/icons';

type MenuItem = Required<MenuProps>['items'][number];

export const listMenuRole: MenuItem[] = [
  {
    key: '/role',
    label: 'Tổng quan',
    icon: <HomeOutlined />
  },
  {
    key: '/role/web-manage',
    label: 'Phân quyền website',
    icon: <GlobalOutlined />
  },
  {
    key: '/role/user-manage',
    label: 'Quản lý tài khoản',
    icon: <TeamOutlined />
  }
];
