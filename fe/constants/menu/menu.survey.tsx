import { GlobalOutlined, HomeOutlined, TeamOutlined } from '@ant-design/icons';
import { IMenu } from '@models/common/menu.model';

export const listMenuSurvey: IMenu[] = [
  {
    label: 'Tổng quan',
    routerLink: '/survey',
    icon: <HomeOutlined />
  },
  {
    label: 'Danh sách khảo sát',
    routerLink: '/survey/survey-list',
    icon: <GlobalOutlined />
  }
];
