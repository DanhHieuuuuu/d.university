import { IMenu } from '@models/common/menu.model';
import {
  ApartmentOutlined,
  BarsOutlined,
  DeploymentUnitOutlined,
  FileOutlined,
  HomeOutlined,
  PartitionOutlined,
  TeamOutlined,
  UserOutlined
} from '@ant-design/icons';

export const listMenuCore: IMenu[] = [
  {
    label: 'Trang chủ',
    routerLink: '/home',
    icon: <HomeOutlined />
  },
  {
    label: 'Nhân sự',
    routerLink: '/hrm',
    icon: <TeamOutlined />
  },
  {
    label: 'User',
    routerLink: '/role/user-manage',
    icon: <UserOutlined />
  },
  {
    label: 'Student',
    routerLink: '/student/manage',
    icon: <TeamOutlined />
  },
  {
    label: 'Danh mục',
    routerLink: '/list',
    icon: <BarsOutlined />,
    items: [
      {
        label: 'Chức vụ',
        routerLink: '/list/positions',
        icon: <ApartmentOutlined />
      },
      {
        label: 'Phòng ban',
        routerLink: '/list/departments',
        icon: <PartitionOutlined />
      },
      {
        label: 'Tổ bộ môn',
        routerLink: '/list/divisions',
        icon: <DeploymentUnitOutlined />
      }
    ]
  },
  {
    label: 'Files',
    routerLink: '9',
    icon: <FileOutlined />
  }
];
