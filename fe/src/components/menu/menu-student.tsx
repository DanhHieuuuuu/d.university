'use client';

import { Layout } from 'antd';
import { useTheme } from 'next-themes';

import { listMenuStudent } from '@/constants/menu/menu.student';
import AppMenu from '@components/common/Menu';
import '@styles/menu.style.scss';

const { Sider } = Layout;

const StudentMenuComponent = () => {
  const { resolvedTheme } = useTheme();
  const siderTheme: 'light' | 'dark' = resolvedTheme === 'dark' ? 'dark' : 'light';

  return (
    <Sider width="20%" theme={siderTheme} className="student-menu">
      <AppMenu data={listMenuStudent} />
    </Sider>
  );
};

export default StudentMenuComponent;
