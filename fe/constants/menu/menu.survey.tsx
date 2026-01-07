import { GlobalOutlined, HomeOutlined, TeamOutlined, LoginOutlined, FormOutlined } from '@ant-design/icons';
import { IMenu } from '@models/common/menu.model';

export const listMenuSurvey: IMenu[] = [
  {
    label: 'Đăng nhập',
    routerLink: '/survey/user/login',
    icon: <LoginOutlined />
  },
  {
    label: 'Khảo sát của tôi',
    routerLink: '/survey/user/mysurvey',
    icon: <FormOutlined />
  }
];
