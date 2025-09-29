import { FileOutlined, HomeOutlined, TeamOutlined, UserOutlined } from '@ant-design/icons';
import { IMenu } from '@components/common/Menu';

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
    routerLink: 'sub1',
    icon: <UserOutlined />,
    items: [
      {
        label: 'Tom',
        routerLink: '3'
      },
      {
        label: 'Bill',
        routerLink: '4'
      },
      {
        label: 'Alexaxx',
        routerLink: '5'
      }
    ]
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
