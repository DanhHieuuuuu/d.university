import { FormOutlined } from '@ant-design/icons';
import { IMenu } from '@models/common/menu.model';

export const listMenuSurvey: IMenu[] = [
  {
    label: 'Khảo sát của tôi',
    routerLink: '/survey/user/mysurvey',
    icon: <FormOutlined />
  }
];
