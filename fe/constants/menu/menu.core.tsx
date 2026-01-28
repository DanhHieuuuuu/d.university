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
  UserOutlined,
  RiseOutlined,
  UnorderedListOutlined,
  SafetyCertificateOutlined,
  SolutionOutlined,
  LineChartOutlined
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
    permissionKeys: [PermissionCoreConst.CoreMenuHrm],
    icon: <TeamOutlined />,
    items: [
      {
        label: 'Danh sách nhân sự',
        routerLink: '/hrm/employees',
        icon: <UserOutlined />,
        permissionKeys: [PermissionCoreConst.CoreMenuHrmDanhSach]
      },
      {
        label: 'Thống kê',
        routerLink: '/hrm/statistical',
        icon: <LineChartOutlined />
      },
      {
        label: 'Danh sách hợp đồng',
        routerLink: '/hrm/contracts',
        icon: <SolutionOutlined />,
        permissionKeys: [PermissionCoreConst.CoreMenuHrmContract]
      },
      {
        label: 'Danh sách quyết định',
        routerLink: '/hrm/decisions',
        icon: <ProfileOutlined />,
        permissionKeys: [PermissionCoreConst.CoreMenuHrmDecision]
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
    permissionKeys: [PermissionCoreConst.CoreMenuKpi],
    icon: <RiseOutlined />,
    items: [
      {
        label: 'Danh sách KPI',
        routerLink: '/kpi/list',
        permissionKeys: [PermissionCoreConst.CoreMenuKpiList],
        icon: <UnorderedListOutlined />,
        items: [
          {
            label: 'Kpi Role',
            routerLink: '/kpi/list/roles',
            permissionKeys: [PermissionCoreConst.CoreMenuKpiListRole],
            icon: <SafetyCertificateOutlined />
          },
          {
            label: 'Kpi cá nhân',
            routerLink: '/kpi/list/personals',
            permissionKeys: [PermissionCoreConst.CoreMenuKpiListPersonal],
            icon: <UserOutlined />
          },
          {
            label: 'Kpi đơn vị',
            routerLink: '/kpi/list/units',
            permissionKeys: [PermissionCoreConst.CoreMenuKpiListUnit],
            icon: <TeamOutlined />
          },
          {
            label: 'Kpi trường',
            routerLink: '/kpi/list/school',
            permissionKeys: [PermissionCoreConst.CoreMenuKpiListSchool],
            icon: <BankOutlined />
          }
        ]
      },
      {
        label: 'Quản lý KPI',
        routerLink: '/kpi/manage',
        permissionKeys: [PermissionCoreConst.CoreMenuKpiManage],
        icon: <FormOutlined />,
        items: [
          {
            label: 'Kê khai Kpi cá nhân',
            routerLink: '/kpi/manage/personals',
            permissionKeys: [PermissionCoreConst.CoreMenuKpiManagePersonal],
            icon: <UserOutlined />
          },
          {
            label: 'Kê khai Kpi đơn vị',
            routerLink: '/kpi/manage/units',
            permissionKeys: [PermissionCoreConst.CoreMenuKpiManageUnit],
            icon: <FileTextOutlined />
          },
          {
            label: 'Kê khai Kpi trường',
            routerLink: '/kpi/manage/school',
            permissionKeys: [PermissionCoreConst.CoreMenuKpiManageSchool],
            icon: <SolutionOutlined />
          }
          // {
          //   label: 'Điểm KPI',
          //   routerLink: '/kpi/manage/scoreboard',
          //   icon: <CalculatorOutlined />
          // }
        ]
      }
    ]
  },
  {
    label: 'Đoàn vào',
    routerLink: '/delegation/incoming',
    permissionKeys: [PermissionCoreConst.CoreMenuDelegation],
    icon: <DelegationIcon />,
    items: [
      {
        label: 'Danh sách đoàn vào',
        routerLink: '/delegation/incoming/list-delegation',
        permissionKeys: [PermissionCoreConst.CoreMenuListDoanVao],
        icon: <SuggestIcon />
      },
      {
        label: 'Xử lý đoàn vào',
        routerLink: '/delegation/incoming/process',
        permissionKeys: [PermissionCoreConst.CoreMenuXuLyDoanVao],
        icon: <HourglassOutlined />
      },
      {
        label: 'Phòng ban hỗ trợ',
        routerLink: '/delegation/incoming/support',
        permissionKeys: [PermissionCoreConst.CoreMenuDepartment],
        icon: <TeamOutlined />
      },
      {
        label: 'Nhật ký đoàn vào',
        routerLink: '/delegation/incoming/diary',
        permissionKeys: [PermissionCoreConst.CoreMenuLog],
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
    permissionKeys: [PermissionCoreConst.CoreMenuKhaoSat],
    icon: <FormOutlined />,
    items: [
      {
        label: 'Danh sách yêu cầu',
        routerLink: '/survey/request',
        permissionKeys: [PermissionCoreConst.SurveyMenuRequest],
        icon: <InboxOutlined />
      },
      {
        label: 'Quản lý yêu cầu',
        routerLink: '/survey/request-manage',
        permissionKeys: [PermissionCoreConst.SurveyMenuRequestApproval],
        icon: <InboxOutlined />
      },
      {
        label: 'Danh sách khảo sát',
        routerLink: '/survey/surveys',
        permissionKeys: [PermissionCoreConst.SurveyMenuManagement],
        icon: <FileTextOutlined />
      },
      {
        label: 'Báo cáo khảo sát',
        routerLink: '/survey/report',
        permissionKeys: [PermissionCoreConst.SurveyMenuReport],
        icon: <BarChartOutlined />
      },
      {
        label: 'Nhật ký hoạt động',
        routerLink: '/survey/logging',
        permissionKeys: [PermissionCoreConst.SurveyMenuLogging],
        icon: <HistoryOutlined />
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
