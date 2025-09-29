import { GlobalOutlined, HomeOutlined, TeamOutlined } from '@ant-design/icons';
import { IMenu } from '@models/common/menu.model';

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
