'use client';

import { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Layout, Typography, Dropdown, Modal, Form, Input, Button, Space, message } from 'antd';
import { UserOutlined, LockOutlined, LogoutOutlined, UserSwitchOutlined } from '@ant-design/icons';
import { useNavigate } from '@hooks/navigate';
import { RootState } from '@redux/store';
import { clearUser } from '@redux/feature/auth/authSlice';
import { AuthService } from '@services/auth.service';

import NotificationComponent from '@components/common/Notification';
import UserAvatar from '@components/common/UserAvatar';
import '@src/styles/globals.scss';

const { Header } = Layout;
const { Title } = Typography;

const AppHeader = () => {
  const { navigateTo } = useNavigate();
  const dispatch = useDispatch();
  const { user } = useSelector((state: RootState) => state.authState);

  const [profileModalVisible, setProfileModalVisible] = useState(false);
  const [changePasswordModalVisible, setChangePasswordModalVisible] = useState(false);
  const [passwordForm] = Form.useForm();
  const [avatarRefreshKey, setAvatarRefreshKey] = useState(0);

  const handleAvatarUploadSuccess = (blob: Blob, timestamp: number) => {
    // Sau khi upload thành công, API đã trả về blob và ảnh trong modal đã được cập nhật
    // Trigger refresh cho avatar trong header bằng cách thay đổi key để fetch ảnh mới từ server
    setAvatarRefreshKey(timestamp);
  };

  const handleProfileClick = () => {
    setProfileModalVisible(true);
  };

  const handleChangePasswordClick = () => {
    setChangePasswordModalVisible(true);
  };

  const handleLogoutClick = async () => {
    try {
      await AuthService.logoutApi();
      dispatch(clearUser());
      navigateTo('/login');
    } catch (error) {
      console.error('Logout error:', error);
      // Still clear local user data and navigate even if API call fails
      dispatch(clearUser());
      navigateTo('/login');
    }
  };

  const handleChangePassword = async (values: {
    currentPassword: string;
    newPassword: string;
    confirmPassword: string;
  }) => {
    try {
      await AuthService.changePasswordApi({
        oldPassword: values.currentPassword,
        newPassword: values.newPassword
      });
      message.success('Đổi mật khẩu thành công!');
      setChangePasswordModalVisible(false);
      passwordForm.resetFields();
    } catch (error) {
      console.error('Change password error:', error);
      message.error('Đổi mật khẩu thất bại! Vui lòng kiểm tra lại mật khẩu hiện tại.');
    }
  };

  const menuItems = [
    {
      key: 'profile',
      label: 'Thông tin cá nhân',
      icon: <UserSwitchOutlined />,
      onClick: handleProfileClick
    },
    {
      key: 'change-password',
      label: 'Đổi mật khẩu',
      icon: <LockOutlined />,
      onClick: handleChangePasswordClick
    },
    {
      type: 'divider' as const
    },
    {
      key: 'logout',
      label: 'Log out',
      icon: <LogoutOutlined />,
      onClick: handleLogoutClick
    }
  ];

  const userDisplayName = user?.hoDem && user?.ten ? `${user.hoDem} ${user.ten}` : user?.ten || '';

  return (
    <>
      <Header
        style={{
          background: 'var(--background-header)',
          color: '#FFFFFF',
          paddingInline: 0
        }}
      >
        <div className="flex h-full w-full items-center justify-between" style={{ padding: '0 20px' }}>
          <div className='left-header'>
            <Title level={2} style={{ cursor: 'pointer', margin: 0, color: '#FFFFFF' }} onClick={() => navigateTo('/home')}>
              University
            </Title>
          </div>

          <div className='right-header flex items-center gap-4'>
            <NotificationComponent />
            <Dropdown menu={{ items: menuItems }} placement="bottomRight" trigger={['click']}>
              <div className="flex cursor-pointer items-center gap-2 rounded-lg p-2 transition-colors hover:bg-opacity-20 hover:bg-white">
                <UserAvatar 
                  key={`header-avatar-${avatarRefreshKey}`}
                  imageLink={user?.imageLink}
                  maNhanSu={user?.maNhanSu}
                  size="large"
                  className="mr-3"
                />
                <span className="font-medium" style={{ color: '#FFFFFF' }}>{userDisplayName}</span>
              </div>
            </Dropdown>
          </div>
        </div>
      </Header>

      {/* Profile Modal */}
      <Modal
        title="Thông tin cá nhân"
        open={profileModalVisible}
        onCancel={() => setProfileModalVisible(false)}
        footer={[
          <Button key="close" onClick={() => setProfileModalVisible(false)}>
            Đóng
          </Button>
        ]}
        width={500}
      >
        <div className="flex flex-col items-center">
          <UserAvatar 
            key={`modal-avatar-${avatarRefreshKey}`}
            imageLink={user?.imageLink}
            maNhanSu={user?.maNhanSu}
            tenNhanSu={user?.ten}
            size={120}
            editable={true}
            showEditButton={true}
            onUploadSuccess={handleAvatarUploadSuccess}
            className="mb-4 flex flex-col items-center"
          />
          <div className="w-full space-y-3">
            <div className="flex justify-between border-b py-2">
              <span className="font-medium">Mã nhân viên:</span>
              <span>{user?.maNhanSu || 'N/A'}</span>
            </div>
            <div className="flex justify-between border-b py-2">
              <span className="font-medium">Họ đệm:</span>
              <span>{user?.hoDem || 'N/A'}</span>
            </div>
            <div className="flex justify-between border-b py-2">
              <span className="font-medium">Tên:</span>
              <span>{user?.ten || 'N/A'}</span>
            </div>
            <div className="flex justify-between border-b py-2">
              <span className="font-medium">Email:</span>
              <span>{user?.email || 'N/A'}</span>
            </div>
          </div>
        </div>
      </Modal>

      {/* Change Password Modal */}
      <Modal
        title="Đổi mật khẩu"
        open={changePasswordModalVisible}
        onCancel={() => {
          setChangePasswordModalVisible(false);
          passwordForm.resetFields();
        }}
        footer={null}
        width={450}
      >
        <Form form={passwordForm} layout="vertical" onFinish={handleChangePassword}>
          <Form.Item
            name="currentPassword"
            label="Mật khẩu hiện tại"
            rules={[{ required: true, message: 'Vui lòng nhập mật khẩu hiện tại!' }]}
          >
            <Input.Password placeholder="Nhập mật khẩu hiện tại" />
          </Form.Item>

          <Form.Item
            name="newPassword"
            label="Mật khẩu mới"
            rules={[
              { required: true, message: 'Vui lòng nhập mật khẩu mới!' },
              { min: 6, message: 'Mật khẩu phải có ít nhất 6 ký tự!' }
            ]}
          >
            <Input.Password placeholder="Nhập mật khẩu mới" />
          </Form.Item>

          <Form.Item
            name="confirmPassword"
            label="Xác nhận mật khẩu mới"
            dependencies={['newPassword']}
            rules={[
              { required: true, message: 'Vui lòng xác nhận mật khẩu mới!' },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue('newPassword') === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(new Error('Mật khẩu xác nhận không khớp!'));
                }
              })
            ]}
          >
            <Input.Password placeholder="Nhập lại mật khẩu mới" />
          </Form.Item>

          <Form.Item className="mb-0">
            <Space className="w-full justify-end">
              <Button
                onClick={() => {
                  setChangePasswordModalVisible(false);
                  passwordForm.resetFields();
                }}
              >
                Hủy
              </Button>
              <Button type="primary" htmlType="submit">
                Đổi mật khẩu
              </Button>
            </Space>
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};

export default AppHeader;
