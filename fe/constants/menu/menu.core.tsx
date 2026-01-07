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
  ProfileOutlined,
  FileTextOutlined,
  BarChartOutlined,
  FormOutlined,
  InboxOutlined,
  UserOutlined
} from '@ant-design/icons';
import { PermissionCoreConst } from '../permissionWeb/PermissionCore';
import { AdminIcon, DelegationIcon, StudentIcon, SuggestIcon } from '@components/custom-icon';

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
    icon: <TeamOutlined />,
    items: [
      {
        label: 'Danh sách nhân sự',
        routerLink: '/hrm/employees',
        icon: <UserOutlined />
      },
      {
        label: 'Danh sách phòng ban',
        routerLink: '/hrm/departments',
        icon: <ApartmentOutlined />
      }
    ]
  },
  {
    label: 'Quản lý sinh viên',
    routerLink: '/student/manage',
    icon: <StudentIcon />
  },
  {
    label: 'Danh mục',
    routerLink: '/category',
    permissionKeys: [PermissionCoreConst.UserMenuAdmin],
    icon: <BarsOutlined />,
    items: [
      {
        label: 'Chức vụ',
        routerLink: '/category/positions',
        icon: <ApartmentOutlined />
      },
      {
        label: 'Phòng ban',
        routerLink: '/category/departments',
        icon: <PartitionOutlined />
      },
      {
        label: 'Tổ bộ môn',
        routerLink: '/category/divisions',
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
          {
            label: 'Kpi trường',
            routerLink: '/kpi/list/school',
            icon: <PartitionOutlined />
          }
        ]
      },
      {
        label: 'Quản lý KPI',
        routerLink: '/kpi/manage',
        icon: <PartitionOutlined />,
        items: [
          {
            label: 'Kê khai Kpi cá nhân',
            routerLink: '/kpi/manage/personals',
            icon: <PartitionOutlined />
          }
          // {
          //   label: 'Kê khai Kpi đơn vị',
          //   routerLink: '/kpi/list/units',
          //   icon: <PartitionOutlined />
          // },
          // {
          //   label: 'Kê khai Kpi trường',
          //   routerLink: '/kpi/list/school',
          //   icon: <PartitionOutlined />
          // },
        ]
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
    ]
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
        label: 'Chương trình khung',
        routerLink: '/training/curriculumFramework',
        icon: <ProfileOutlined />
      }
    ]
  },
  {
    label: 'Khảo sát',
    routerLink: '/survey',
    icon: <FormOutlined />,
    items: [
      {
        label: 'Danh sách yêu cầu',
        routerLink: '/survey/manage/request',
        icon: <InboxOutlined />,      
      },
      {
        label: 'Danh sách khảo sát',
        routerLink: '/survey/manage/surveys',
        icon: <FileTextOutlined />
      },
      {
        label: 'Báo cáo khảo sát',
        routerLink: '/survey/manage/report',
        icon: <BarChartOutlined />
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
