'use client';

import { Layout, Typography, Dropdown, Avatar, Modal, Form, Input, Button, Space, message } from 'antd';
import { UserOutlined, LockOutlined, LogoutOutlined, UserSwitchOutlined, EditOutlined } from '@ant-design/icons';
import { useNavigate } from '@hooks/navigate';
import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '@redux/store';
import { clearUser } from '@redux/feature/authSlice';
import { AuthService } from '@services/auth.service';
import { useState, useEffect, useRef } from 'react';
import { AvatarStorage } from '@utils/avatar-storage';
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
  const [avatarUrl, setAvatarUrl] = useState<string | null>(null);
  const [uploadingAvatar, setUploadingAvatar] = useState(false);
  const fileInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    if (user?.maNhanSu) {
      const savedAvatar = AvatarStorage.getAvatar(user.maNhanSu);
      setAvatarUrl(savedAvatar);
    }
  }, [user?.maNhanSu]);

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

  const handleChangePassword = async (values: { currentPassword: string; newPassword: string; confirmPassword: string }) => {
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

  const handleAvatarClick = () => {
    fileInputRef.current?.click();
  };

  const handleFileChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (!file || !user?.maNhanSu) return;

    const validation = AvatarStorage.validateImageFile(file);
    if (!validation.valid) {
      message.error(validation.error);
      return;
    }

    setUploadingAvatar(true);
    try {
      const base64String = await AvatarStorage.saveAvatar(user.maNhanSu, file);
      setAvatarUrl(base64String);
      message.success('Cập nhật ảnh đại diện thành công!');
    } catch (error) {
      console.error('Avatar upload error:', error);
      message.error('Không thể tải lên ảnh đại diện!');
    } finally {
      setUploadingAvatar(false);
      if (fileInputRef.current) {
        fileInputRef.current.value = '';
      }
    }
  };

  const menuItems = [
    {
      key: 'profile',
      label: 'Thông tin cá nhân',
      icon: <UserSwitchOutlined />,
      onClick: handleProfileClick,
    },
    {
      key: 'change-password',
      label: 'Đổi mật khẩu',
      icon: <LockOutlined />,
      onClick: handleChangePasswordClick,
    },
    {
      type: 'divider' as const,
    },
    {
      key: 'logout',
      label: 'Log out',
      icon: <LogoutOutlined />,
      onClick: handleLogoutClick,
    },
  ];

  const userDisplayName = user?.hoDem && user?.ten 
    ? `${user.hoDem} ${user.ten}` 
    : user?.ten || '';

  return (
    <>
      <Header
        style={{
          background: 'var(--background-header)',
          color: 'var(--foreground)',
          paddingInline: 0,
        }}
      >
        <div className="flex h-full items-center w-full" style={{ padding: '0 20px', position: 'relative' }}>
          <div>
            <Title level={2} style={{ cursor: 'pointer', margin: 0 }} onClick={() => navigateTo('/home')}>
              University
            </Title>
          </div>
          
          <div style={{ position: 'absolute', right: '20px', top: '50%', transform: 'translateY(-50%)' }}>
            <Dropdown
            menu={{ items: menuItems }}
            placement="bottomRight"
            trigger={['click']}
          >
            <div className="flex items-center cursor-pointer hover:bg-gray-100 rounded-lg p-2 transition-colors gap-2">
              <Avatar 
                size="large" 
                src={avatarUrl}
                icon={!avatarUrl && <UserOutlined />}
                className="mr-3"
              />
              <span className="font-medium">{userDisplayName}</span>
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
          </Button>,
        ]}
        width={500}
      >
        <div className="flex flex-col items-center">
          <div className="relative mb-4">
            <Avatar 
              size={120} 
              src={avatarUrl}
              icon={!avatarUrl && <UserOutlined />}
            />
            <input
              ref={fileInputRef}
              type="file"
              accept="image/*"
              onChange={handleFileChange}
              style={{ display: 'none' }}
            />
          </div>
          <Button
            type="default"
            icon={<EditOutlined />}
            onClick={handleAvatarClick}
            loading={uploadingAvatar}
            className="mb-4"
          >
            Đổi ảnh đại diện
          </Button>
          <div className="w-full space-y-3">
            <div className="flex justify-between py-2 border-b">
              <span className="font-medium">Mã nhân viên:</span>
              <span>{user?.maNhanSu || 'N/A'}</span>
            </div>
            <div className="flex justify-between py-2 border-b">
              <span className="font-medium">Họ đệm:</span>
              <span>{user?.hoDem || 'N/A'}</span>
            </div>
            <div className="flex justify-between py-2 border-b">
              <span className="font-medium">Tên:</span>
              <span>{user?.ten || 'N/A'}</span>
            </div>
            <div className="flex justify-between py-2 border-b">
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
        <Form
          form={passwordForm}
          layout="vertical"
          onFinish={handleChangePassword}
        >
          <Form.Item
            name="currentPassword"
            label="Mật khẩu hiện tại"
            rules={[
              { required: true, message: 'Vui lòng nhập mật khẩu hiện tại!' },
            ]}
          >
            <Input.Password placeholder="Nhập mật khẩu hiện tại" />
          </Form.Item>
          
          <Form.Item
            name="newPassword"
            label="Mật khẩu mới"
            rules={[
              { required: true, message: 'Vui lòng nhập mật khẩu mới!' },
              { min: 6, message: 'Mật khẩu phải có ít nhất 6 ký tự!' },
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
                },
              }),
            ]}
          >
            <Input.Password placeholder="Nhập lại mật khẩu mới" />
          </Form.Item>
          
          <Form.Item className="mb-0">
            <Space className="w-full justify-end">
              <Button onClick={() => {
                setChangePasswordModalVisible(false);
                passwordForm.resetFields();
              }}>
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
