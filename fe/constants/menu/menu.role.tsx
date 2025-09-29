import { IMenu } from '@components/common/Menu';
import { GlobalOutlined, HomeOutlined, TeamOutlined } from '@ant-design/icons';

export const listMenuRole: IMenu[] = [
  {
    label: 'Tổng quan',
    routerLink: '/role',
    icon: <HomeOutlined />
  },
  {
    label: 'Phân quyền website',
    routerLink: '/role/web-manage',
    icon: <GlobalOutlined />
  },
  {
    label: 'Quản lý tài khoản',
    routerLink: '/role/user-manage',
    icon: <TeamOutlined />
  }
];
