'use client';

import { Layout } from 'antd';

import { listMenuSurvey } from '@/constants/menu/menu.survey';
import AppMenu from '@components/common/Menu';
import '@styles/menu.style.scss';

const { Sider } = Layout;

const SurveyMenuComponent = () => {
  return (
    <Sider width="20%" className="survey-menu" breakpoint="lg" collapsedWidth="80">
      <AppMenu data={listMenuSurvey} />
    </Sider>
  );
};

export default SurveyMenuComponent;
