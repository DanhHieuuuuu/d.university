import { IMenu } from '@models/common/menu.model';
import {
  ApartmentOutlined,
  BarsOutlined,
  DeploymentUnitOutlined,
  HomeOutlined,
  PartitionOutlined,
  TeamOutlined
} from '@ant-design/icons';
import { PermissionCoreConst } from '../permissionWeb/PermissionCore';
import { AdminIcon, StudentIcon } from '@components/custom-icon';

export const listMenuCore: IMenu[] = [
  {
    label: 'Trang chủ',
    routerLink: '/home',
    icon: <HomeOutlined />
  },
  {
    label: 'Quán lý nhân sự',
    routerLink: '/hrm',
    // permissionKeys: [PermissionCoreConst.CoreMenuNhanSu],
    icon: <TeamOutlined />
  },
  {
    label: 'Quản lý sinh viên',
    routerLink: '/student/manage',
    icon: <StudentIcon />
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
    label: 'KPI',
    routerLink: '/kpi',
    icon: <BarsOutlined />,
    items: [
      {
        label: 'Danh sách KPI',
        routerLink: '/kpi/list',
        icon: <ApartmentOutlined />,
        items: [
          {
            label: 'Kpi Role',
            routerLink: '/kpi/list/roles',
            icon: <PartitionOutlined />
          },
          {
            label: 'Kpi cá nhân',
            routerLink: '/kpi/list/personals',
            icon: <PartitionOutlined />
          },
          {
            label: 'Kpi đơn vị',
            routerLink: '/kpi/list/units',
            icon: <PartitionOutlined />
          },
        ],
        
      },
      {
        label: 'Quản lý KPI',
        routerLink: '/kpi/manage',
        icon: <PartitionOutlined />
      }
    ]
  },
  {
    label: 'Admin',
    routerLink: '/manager',
    permissionKeys: [PermissionCoreConst.UserMenuAdmin],
    icon: <AdminIcon />
  }
];
