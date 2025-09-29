import { FileOutlined, HomeOutlined, TeamOutlined, UserOutlined } from '@ant-design/icons';
import { IMenu } from '@models/common/menu.model';

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
    label: 'Team',
    routerLink: 'sub2',
    items: [
      {
        label: 'Team 1',
        routerLink: '6'
      },
      {
        label: 'Team 2',
        routerLink: '8'
      }
    ]
  },
  {
    label: 'Files',

    routerLink: '9',
    icon: <FileOutlined />
  }
];
