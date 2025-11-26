'use client';

import { Layout } from 'antd';

import { listMenuStudent } from '@/constants/menu/menu.student';
import AppMenu from '@components/common/Menu';
import '@styles/menu.style.scss';

const { Sider } = Layout;

const StudentMenuComponent = () => {
  return (
    <Sider width="20%" className="student-menu" breakpoint="lg" collapsedWidth="80">
      <AppMenu data={listMenuStudent} />
    </Sider>
  );
};

export default StudentMenuComponent;
