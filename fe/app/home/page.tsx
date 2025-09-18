'use client';

import React, { useState } from 'react';
import {
  DesktopOutlined,
  PieChartOutlined,
  TeamOutlined,
  UserOutlined,
} from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { Breadcrumb, Layout, Menu, theme, Card, Col, Row, Typography, Divider, Avatar } from 'antd';
import { useAppSelector } from '@redux/hooks';
import { UserOutlined as AntdUserOutlined, MailOutlined, IdcardOutlined, SolutionOutlined } from '@ant-design/icons';

const { Header, Content, Footer, Sider } = Layout;
const { Title, Text } = Typography;

type MenuItem = Required<MenuProps>['items'][number];

function getItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[],
): MenuItem {
  return {
    key,
    icon,
    children,
    label,
  } as MenuItem;
}

const items: MenuItem[] = [
  getItem('Option 1', '1', <PieChartOutlined />),
  getItem('Option 2', '2', <DesktopOutlined />),
  getItem('User', 'sub1', <UserOutlined />, [
    getItem('Tom', '3'),
    getItem('Bill', '4'),
    getItem('Alex', '5'),
  ]),
  getItem('Team', 'sub2', <TeamOutlined />, [getItem('Team 1', '6'), getItem('Team 2', '8')]),
  getItem('Files', '9', ' <FileOutlined />'),
];

const HomePage: React.FC = () => {
  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();
  const { user } = useAppSelector((state) => state.authState);

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
        <div className="demo-logo-vertical" />
        <Menu theme="dark" defaultSelectedKeys={['1']} mode="inline" items={items} />
      </Sider>
      <Layout>
        <Header style={{ padding: 0, background: colorBgContainer }} />
        <Content style={{ margin: '0 16px' }}>
          <Breadcrumb style={{ margin: '16px 0' }} items={[{ title: 'User' }, { title: user?.fullName }]} />
          <div
            style={{
              padding: 24,
              minHeight: 360,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
            }}
          >
            {user ? (
              <Card
                title={
                  <div style={{ display: 'flex', alignItems: 'center', gap: '12px' }}>
                    <AntdUserOutlined style={{ fontSize: '24px', color: '#1890ff' }} />
                    <Title level={4} style={{ margin: 0 }}>Hồ sơ người dùng</Title>
                  </div>
                }
                style={{ width: '100%' }}
              >
                <Row gutter={16}>
                  <Col span={8} style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                    <Avatar size={100} icon={<AntdUserOutlined />} style={{ marginBottom: '16px' }} />
                    <Title level={4}>{user.fullName}</Title>
                    <Text type="secondary">{user.position}</Text>
                  </Col>
                  <Col span={16}>
                    <Title level={5}>Thông tin cá nhân</Title>
                    <Divider style={{ margin: '12px 0' }} />
                    <Row gutter={[16, 16]} style={{ width: '100%' }}>
                      <Col span={24}>
                        <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
                          <IdcardOutlined style={{ color: '#1890ff' }} />
                          <Text>Mã nhân sự: <strong>{user.maNhanSu}</strong></Text>
                        </div>
                      </Col>
                      <Col span={24}>
                        <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
                          <MailOutlined style={{ color: '#1890ff' }} />
                          <Text>Email: <strong>{user.email}</strong></Text>
                        </div>
                      </Col>
                      <Col span={24}>
                        <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
                          <SolutionOutlined style={{ color: '#1890ff' }} />
                          <Text>Chức vụ: <strong>{user.position}</strong></Text>
                        </div>
                      </Col>
                    </Row>
                  </Col>
                </Row>
              </Card>
            ) : (
              <div style={{ textAlign: 'center', padding: '50px' }}>
                <Title level={3}>Không tìm thấy thông tin người dùng.</Title>
                <Text type="secondary">Vui lòng đăng nhập lại để xem hồ sơ.</Text>
              </div>
            )}
          </div>
        </Content>
        <Footer style={{ textAlign: 'center' }}>
          Ant Design ©{new Date().getFullYear()} Created by Ant UED
        </Footer>
      </Layout>
    </Layout>
  );
};

export default HomePage;