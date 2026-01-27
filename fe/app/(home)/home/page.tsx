'use client';

import React, { useEffect, useState } from 'react';
import { Row, Col, Card, Statistic, Table, Tag } from 'antd';
import {
  TeamOutlined,
  UserOutlined,
  BookOutlined,
  BankOutlined,
  RiseOutlined,
  ClockCircleOutlined
} from '@ant-design/icons';
import { useAppSelector } from '@redux/hooks';
import { Column, Pie } from '@ant-design/plots';
import { IStatistical } from '@models/delegation/delegation.model';
import { ISurveyStatistics } from '@models/survey/statistics.model';
import { toast } from 'react-toastify';
import { DelegationIncomingService } from '@/src/services/delegation/delegationIncoming.service';
import { SurveyService } from '@/src/services/survey.service';
import { DelegationStatusConst } from '@/constants/core/delegation/delegation-status.consts';
import { requestStatusConst } from '@/constants/core/survey/requestStatus.const';
import { surveyStatusConst } from '@/constants/core/survey/surveyStatus.const';

const HomePage: React.FC = () => {
  const { user } = useAppSelector((state) => state.authState);

  const [statisticalData, setStatisticalData] = useState<IStatistical | null>(null);
  const [surveyStats, setSurveyStats] = useState<ISurveyStatistics | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchStatistical = async () => {
      try {
        setLoading(true);
        const res = await DelegationIncomingService.getStatistical();
        setStatisticalData(res.data);
      } catch {
        toast.error('Không tải được thống kê');
      } finally {
        setLoading(false);
      }
    };

    const fetchSurveyStats = async () => {
      try {
        const res = await SurveyService.getStatistics();
        setSurveyStats(res.data);
      } catch {
        toast.error('Không tải được thống kê khảo sát');
      }
    };

    fetchStatistical();
    fetchSurveyStats();
  }, []);

  // Mock data - Thay thế bằng API call thực tế
  const statsData = {
    totalStaff: 245,
    totalStudents: 1520,
    totalCourses: 68,
    totalFaculties: 8,
    staffGrowth: 12.5,
    studentGrowth: 8.3,
    courseGrowth: 15.2,
    facultyGrowth: 0
  };

  // Dữ liệu biểu đồ cột - Số lượng sinh viên theo khoa
  const studentsByFacultyData = [
    { faculty: 'Công nghệ TT', students: 320 },
    { faculty: 'Kinh tế', students: 280 },
    { faculty: 'Ngoại ngữ', students: 240 },
    { faculty: 'Kỹ thuật', students: 210 },
    { faculty: 'Y khoa', students: 190 },
    { faculty: 'Luật', students: 150 },
    { faculty: 'Sư phạm', students: 80 },
    { faculty: 'Nghệ thuật', students: 50 }
  ];

  const statusChartData = (statisticalData?.byStatus || []).map((item) => ({
    status: DelegationStatusConst.getInfo(item.status, 'label'),
    total: item.total
  }));
  const columnConfig: any = {
    data: statusChartData,
    xField: 'status',
    yField: 'total',
    label: {
      text: 'total',
      position: 'top',
      style: {
        fill: '#000000',
        opacity: 0.6
      }
    },
    axis: {
      x: {
        title: 'Trạng thái',
        labelAutoRotate: false
      },
      y: {
        title: 'Số đoàn vào'
      }
    },
    style: {
      fill: '#1890ff'
    }
  };

  // // Dữ liệu biểu đồ tròn - Phân bố nhân sự theo chức vụ
  // const staffByPositionData = [
  //   { type: 'Giảng viên', value: 180 },
  //   { type: 'Trợ giảng', value: 35 },
  //   { type: 'Quản lý', value: 15 },
  //   { type: 'Hành chính', value: 15 }
  // ];

  const pieConfig: any = {
    data: statusChartData,
    angleField: 'total',
    colorField: 'status',
    radius: 0.8,
    label: {
      text: 'status',
      position: 'outside'
    },
    legend: {
      color: {
        title: false,
        position: 'bottom'
      }
    },
    tooltip: {
      title: 'status'
    }
  };

  const allRequestStatuses = requestStatusConst.list.map(status => ({
    status: status.name,
    total: 0
  }));
  
  const apiRequestData = (surveyStats?.surveyRequests?.byStatus || []).map(item => ({
    status: item.statusName,
    total: item.count
  }));
  
  const surveyRequestChartData = allRequestStatuses.map(defaultStatus => {
    const apiData = apiRequestData.find(api => api.status === defaultStatus.status);
    return apiData || defaultStatus;
  });

  const requestColumnConfig: any = {
    data: surveyRequestChartData,
    xField: 'status',
    yField: 'total',
    label: {
      text: 'total',
      position: 'top',
      style: {
        fill: '#000000',
        opacity: 0.6
      }
    },
    axis: {
      x: {
        title: 'Trạng thái',
        labelAutoRotate: false
      },
      y: {
        title: 'Số lượng'
      }
    },
    style: {
      fill: '#52c41a'
    }
  };

  const allSurveyStatuses = surveyStatusConst.list.map(status => ({
    status: status.name,
    //total: 0
  }));
  
  const apiSurveyData = (surveyStats?.surveys?.byStatus || []).map(item => ({
    status: item.statusName,
    total: item.count
  }));
  
  const surveyChartData = allSurveyStatuses.map(defaultStatus => {
    const apiData = apiSurveyData.find(api => api.status === defaultStatus.status);
    return apiData || defaultStatus;
  });

  const surveyPieConfig: any = {
    data: surveyChartData,
    angleField: 'total',
    colorField: 'status',
    radius: 0.8,
    label: {
      text: 'status',
      position: 'outside'
    },
    legend: {
      color: {
        title: false,
        position: 'bottom'
      }
    },
    tooltip: {
      title: 'status'
    }
  };

  // Bảng hoạt động gần đây
  const recentActivities = [
    {
      key: '1',
      action: 'Thêm sinh viên mới',
      user: 'Nguyễn Văn A',
      time: '5 phút trước',
      status: 'success'
    },
    {
      key: '2',
      action: 'Cập nhật chương trình khung',
      user: 'Trần Thị B',
      time: '15 phút trước',
      status: 'success'
    },
    {
      key: '3',
      action: 'Xóa môn học',
      user: 'Lê Văn C',
      time: '1 giờ trước',
      status: 'warning'
    },
    {
      key: '4',
      action: 'Thêm khoa mới',
      user: 'Phạm Thị D',
      time: '2 giờ trước',
      status: 'success'
    },
    {
      key: '5',
      action: 'Cập nhật KPI',
      user: 'Hoàng Văn E',
      time: '3 giờ trước',
      status: 'processing'
    }
  ];

  const activityColumns: any = [
    {
      title: 'Hoạt động',
      dataIndex: 'action',
      key: 'action'
    },
    {
      title: 'Người thực hiện',
      dataIndex: 'user',
      key: 'user'
    },
    {
      title: 'Thời gian',
      dataIndex: 'time',
      key: 'time',
      render: (time: string) => (
        <span>
          <ClockCircleOutlined /> {time}
        </span>
      )
    },
    {
      title: 'Trạng thái',
      dataIndex: 'status',
      key: 'status',
      render: (status: string) => {
        const colorMap: any = {
          success: 'green',
          warning: 'orange',
          processing: 'blue'
        };
        const labelMap: any = {
          success: 'Thành công',
          warning: 'Cảnh báo',
          processing: 'Đang xử lý'
        };
        return <Tag color={colorMap[status]}>{labelMap[status]}</Tag>;
      }
    }
  ];

  return (
    <div style={{ padding: 24 }}>
      {/* Welcome Message */}
      <p style={{ marginBottom: 24, fontSize: 28, fontWeight: 600 }}>Chào mừng trở lại, {user?.ten || 'Admin'}! 👋</p>

      {/* Statistics Cards */}
      <Row gutter={[16, 16]} style={{ marginBottom: 24 }}>
        <Col xs={24} sm={12} lg={6}>
          <Card variant="borderless" style={{ background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)' }}>
            <Statistic
              title={<span style={{ color: 'white' }}>Tổng Đoàn vào</span>}
              value={statisticalData?.totalAll}
              prefix={<TeamOutlined />}
              valueStyle={{ color: 'white' }}
            />
          </Card>
        </Col>

        <Col xs={24} sm={12} lg={6}>
          <Card variant="borderless" style={{ background: 'linear-gradient(135deg, #f093fb 0%, #f5576c 100%)' }}>
            <Statistic
              title={<span style={{ color: 'white' }}>Tổng Sinh Viên</span>}
              value={statsData.totalStudents}
              prefix={<UserOutlined />}
              suffix={
                statsData.studentGrowth > 0 ? (
                  <span style={{ fontSize: '14px', color: '#95de64' }}>
                    <RiseOutlined /> {statsData.studentGrowth}%
                  </span>
                ) : null
              }
              valueStyle={{ color: 'white' }}
            />
          </Card>
        </Col>

        <Col xs={24} sm={12} lg={6}>
          <Card variant="borderless" style={{ background: 'linear-gradient(135deg, #4facfe 0%, #00f2fe 100%)' }}>
            <Statistic
              title={<span style={{ color: 'white' }}>Tổng Môn Học</span>}
              value={statsData.totalCourses}
              prefix={<BookOutlined />}
              suffix={
                statsData.courseGrowth > 0 ? (
                  <span style={{ fontSize: '14px', color: '#95de64' }}>
                    <RiseOutlined /> {statsData.courseGrowth}%
                  </span>
                ) : null
              }
              valueStyle={{ color: 'white' }}
            />
          </Card>
        </Col>

        <Col xs={24} sm={12} lg={6}>
          <Card variant="borderless" style={{ background: 'linear-gradient(135deg, #43e97b 0%, #38f9d7 100%)' }}>
            <Statistic
              title={<span style={{ color: 'white' }}>Tổng Khoa</span>}
              value={statsData.totalFaculties}
              prefix={<BankOutlined />}
              valueStyle={{ color: 'white' }}
            />
          </Card>
        </Col>
      </Row>

      {/* Delegation Charts Row */}
      <Row gutter={[16, 16]} style={{ marginBottom: '24px' }}>
        <Col xs={24} lg={16}>
          <Card title="Số lượng đoàn vào " variant="borderless">
            <Column {...columnConfig} />
          </Card>
        </Col>

        <Col xs={24} lg={8}>
          <Card title="Phân bố số lượng đoàn vào" variant="borderless">
            <Pie {...pieConfig} />
          </Card>
        </Col>
      </Row>

      {/* Survey Statistics Row */}
      <Row gutter={[16, 16]} style={{ marginBottom: '24px' }}>
        <Col xs={24} lg={12}>
          <Card title="Thống kê yêu cầu khảo sát" variant="borderless">
            <Column {...requestColumnConfig} />
          </Card>
        </Col>

        <Col xs={24} lg={12}>
          <Card title="Phân bố khảo sát" variant="borderless">
            <Pie {...surveyPieConfig} />
          </Card>
        </Col>
      </Row>

      {/* Recent Activities */}
      <Row gutter={[16, 16]}>
        <Col span={24}>
          <Card title="Hoạt động gần đây" variant="borderless">
            <Table columns={activityColumns} dataSource={recentActivities} pagination={false} />
          </Card>
        </Col>
      </Row>
    </div>
  );
};

export default HomePage;
