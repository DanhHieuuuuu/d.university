import { IMenu } from '@models/common/menu.model';
import {
  ApartmentOutlined,
  BarsOutlined,
  DeploymentUnitOutlined,
  HistoryOutlined,
  HomeOutlined,
  HourglassOutlined,
  PartitionOutlined,
  TeamOutlined,
  ReadOutlined,
  BankOutlined,
  BranchesOutlined,
  ForkOutlined,
  BookOutlined,
  LinkOutlined,
  ProfileOutlined
} from '@ant-design/icons';
import { PermissionCoreConst } from '../permissionWeb/PermissionCore';
import { AdminIcon, DelegationIcon, StudentIcon, SuggestIcon } from '@components/custom-icon';
import { HistoryIcon } from 'lucide-react';

export const listMenuCore: IMenu[] = [
  {
    label: 'Trang chủ',
    routerLink: '/home',
    icon: <HomeOutlined />
  },
  {
    label: 'Quản lý nhân sự',
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
    label: 'Đoàn vào',
    routerLink: '/delegation/incoming',
    icon: <DelegationIcon />,
    items: [
      {
        label: 'Danh sách đoàn vào',
        routerLink: '/delegation/incoming/list-delegation',
        icon: <SuggestIcon />
      },
      {
        label: 'Xử lý đoàn vào',
        routerLink: '/delegation/incoming/process',
        icon: <HourglassOutlined />
      },
      {
        label: 'Phòng ban hỗ trợ',
        routerLink: '/delegation/incoming/support',
        icon: <TeamOutlined />
      },
      {
        label: 'Nhật ký đoàn vào',
        routerLink: '/delegation/incoming/diary',
        icon: <HistoryOutlined />

      }
    ],
  },
  {
    label: 'Đào tạo',
    routerLink: '/training',
    icon: <ReadOutlined />,
    items: [
      {
        label: 'Khoa',
        routerLink: '/training/faculty',
        icon: <BankOutlined />
      },
      {
        label: 'Ngành',
        routerLink: '/training/major',
        icon: <BranchesOutlined />
      },
      {
        label: 'Chuyên ngành',
        routerLink: '/training/specialization',
        icon: <ForkOutlined />
      },
      {
        label: 'Môn học',
        routerLink: '/training/course',
        icon: <BookOutlined />
      },
      {
        label: 'Môn học tiên quyết',
        routerLink: '/training/prerequisiteCourse',
        icon: <LinkOutlined />
      },
      {
        label: 'Khung chương trình',
        routerLink: '/training/curriculumFramework',
        icon: <ProfileOutlined />
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
