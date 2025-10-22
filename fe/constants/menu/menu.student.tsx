import { GlobalOutlined, HomeOutlined, TeamOutlined } from '@ant-design/icons';
import { IMenu } from '@models/common/menu.model';

export const listMenuStudent: IMenu[] = [
  {
    label: 'Tổng quan',
    routerLink: '/student',
    icon: <HomeOutlined />
  },
  {
    label: 'Quản lý sinh viên',
    routerLink: '/student/student-list',
    icon: <GlobalOutlined />
  },

];
