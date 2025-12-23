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
    label: 'Đào tạo',
    routerLink: '/training',
    icon: <BarsOutlined />,
    items: [
      {
        label: 'Khoa',
        routerLink: '/training/faculty',
        icon: <ApartmentOutlined />
      },
      {
        label: 'Ngành',
        routerLink: '/training/major',
        icon: <ApartmentOutlined />
      },
      {
        label: 'Chuyên ngành',
        routerLink: '/training/specialization',
        icon: <ApartmentOutlined />
      },
      {
        label: 'Môn học',
        routerLink: '/training/course',
        icon: <ApartmentOutlined />
      },
      {
        label: 'Môn học tiên quyết',
        routerLink: '/training/prerequisiteCourse',
        icon: <ApartmentOutlined />
      },
      {
        label: 'Khung chương trình',
        routerLink: '/training/curriculumFramework',
        icon: <ApartmentOutlined />
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
