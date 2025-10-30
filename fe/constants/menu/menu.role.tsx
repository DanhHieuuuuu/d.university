import {
  ContainerOutlined,
  FileOutlined,
  GlobalOutlined,
  HomeOutlined,
  NotificationOutlined,
  TeamOutlined
} from '@ant-design/icons';
import { IMenu } from '@models/common/menu.model';

export const listMenuManager: IMenu[] = [
  {
    label: 'Tổng quan',
    routerLink: '/manager',
    icon: <HomeOutlined />,
    hidden: false
  },
  {
    label: 'Phân quyền website',
    routerLink: '/manager/role',
    icon: <GlobalOutlined />,
    hidden: false
  },
  {
    label: 'Quản lý tài khoản',
    routerLink: '/manager/user',
    icon: <TeamOutlined />,
    hidden: false
  },
  {
    label: 'Quản lý thông báo',
    routerLink: '/manager/notification',
    icon: <NotificationOutlined />,
    hidden: false
  },
  {
    label: 'Quản lý log',
    routerLink: '/manager/log',
    icon: <ContainerOutlined />,
    hidden: false
  },
  {
    label: 'Quản lý file',
    routerLink: '/manager/file',
    icon: <FileOutlined />,
    hidden: false
  }
];
