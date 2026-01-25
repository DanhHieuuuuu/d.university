'use client';

import { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Layout, Dropdown, Button } from 'antd';
import { LogoutOutlined, UserOutlined } from '@ant-design/icons';
import { useRouter } from 'next/navigation';
import { RootState } from '@redux/store';
import { clearSinhVien } from '@redux/feature/student/studentSlice';
import { StudentService } from '@services/student.service';
import NotificationComponent from '@components/common/Notification';

const { Header } = Layout;

const SurveyHeader = () => {
  const router = useRouter();
  const dispatch = useDispatch();
  const { user } = useSelector((state: RootState) => state.studentState);

  const handleLogoutClick = async () => {
    try {
      await StudentService.sinhVienLogoutApi();
      dispatch(clearSinhVien());
      router.push('/survey/user/login');
    } catch (error) {
      console.error('Logout error:', error);
      // Still clear local user data and navigate even if API call fails
      dispatch(clearSinhVien());
      router.push('/survey/user/login');
    }
  };

  const menuItems = [
    {
      key: 'logout',
      label: 'Đăng xuất',
      icon: <LogoutOutlined />,
      onClick: handleLogoutClick
    }
  ];

  const userDisplayName = user?.hoDem && user?.ten ? `${user.hoDem} ${user.ten}` : user?.ten || user?.mssv || 'User';

  return (
    <Header
      style={{
        background: 'var(--background-header)',
        color: '#FFFFFF',
        paddingInline: 0,
        position: 'sticky',
        top: 0,
        zIndex: 100
      }}
    >
      <div className="flex h-full w-full items-center justify-between" style={{ padding: '0 20px' }}>
        <div className="left-header">
          <p
            style={{ fontSize: 30, fontWeight: 600, cursor: 'pointer', margin: 0, color: '#FFFFFF' }}
            onClick={() => router.push('/survey/user/mysurvey')}
          >
            University Survey
          </p>
        </div>

        <div className="right-header flex items-center gap-4">
          <NotificationComponent />
          <Dropdown menu={{ items: menuItems }} placement="bottomRight" trigger={['click']}>
            <div className="flex cursor-pointer items-center gap-2 rounded-lg p-2 transition-colors hover:bg-white hover:bg-opacity-20">
              <div
                style={{
                  width: 40,
                  height: 40,
                  borderRadius: '50%',
                  background: '#1890ff',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  color: 'white',
                  fontSize: 18,
                  fontWeight: 600
                }}
              >
                <UserOutlined />
              </div>
              <span className="font-medium" style={{ color: '#FFFFFF' }}>
                {userDisplayName}
              </span>
            </div>
          </Dropdown>
        </div>
      </div>
    </Header>
  );
};

export default SurveyHeader;
