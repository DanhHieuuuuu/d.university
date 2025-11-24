import {
  ContainerOutlined,
  FileOutlined,
  GlobalOutlined,
  HomeOutlined,
  NotificationOutlined,
  TeamOutlined
} from '@ant-design/icons';
import { IMenu } from '@models/common/menu.model';
import { PermissionCoreConst } from '../permissionWeb/PermissionCore';

export const listMenuManager: IMenu[] = [
  {
    label: 'Tổng quan',
    routerLink: '/manager',
    icon: <HomeOutlined />,
    permissionKeys: [PermissionCoreConst.UserMenuAdmin],
  },
  {
    label: 'Phân quyền website',
    routerLink: '/manager/role',
    icon: <GlobalOutlined />,
    permissionKeys: [PermissionCoreConst.UserMenuPermission],
  },
  {
    label: 'Quản lý tài khoản',
    routerLink: '/manager/user',
    icon: <TeamOutlined />,
    permissionKeys: [PermissionCoreConst.UserMenuAccountManager],
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
