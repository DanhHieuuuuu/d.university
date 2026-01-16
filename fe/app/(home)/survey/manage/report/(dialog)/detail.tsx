'use client';
import React, { useEffect } from 'react';
import { Button, Col, DatePicker, Form, Input, Modal, Row, List, Typography, Progress, Tag, Divider, Card, Tabs, Spin, Empty, Alert } from 'antd';
import { CloseOutlined, RobotOutlined, BarChartOutlined } from '@ant-design/icons';
import { IReportDetail, IAIReportDetail } from '@models/survey/report.model';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getAIReportDetail } from '@redux/feature/survey/surveyThunk';
import { ReduxStatus } from '@redux/const';
import dayjs from 'dayjs';

const { Title, Text, Paragraph } = Typography;

type ReportDetailModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  report: IReportDetail | null;
  onClose?: () => void;
};

const ReportDetailModal: React.FC<ReportDetailModalProps> = ({
  isModalOpen,
  setIsModalOpen,
  report,
  onClose
}) => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { aiReport } = useAppSelector((state) => state.surveyState);
  const { detail: aiDetail } = aiReport;

  useEffect(() => {
    if (isModalOpen && report) {
      form.setFieldsValue({
        ...report,
        lastGenerated: dayjs(report.lastGenerated),
      });
      // Load AI analysis data
      dispatch(getAIReportDetail(report.reportId));
    }
  }, [isModalOpen, report, form, dispatch]);

  const handleCancel = () => {
    setIsModalOpen(false);
    form.resetFields();
    onClose?.();
  };

  const renderQuestionStats = (item: any) => {
    if (item.type === 1 || item.type === 2) {
        return (
            <div style={{ marginTop: 8 }}>
                {item.answers?.map((ans: any, idx: number) => (
                    <div key={idx} style={{ marginBottom: 8 }}>
                        <div style={{ display: 'flex', justifyContent: 'space-between', fontSize: 13 }}>
                            <span>{ans.label}</span>
                            <span style={{ fontWeight: 500 }}>{ans.count} lượt ({ans.percent}%)</span>
                        </div>
                        <Progress 
                            percent={ans.percent} 
                            size="small" 
                            strokeColor={ans.percent > 0 ? '#1890ff' : '#f5f5f5'} 
                            showInfo={false} 
                        />
                    </div>
                ))}
            </div>
        );
    } 
    else if (item.type === 3) {
        const hasResponses = item.recentTextResponses && item.recentTextResponses.length > 0;
        return (
            <div style={{ marginTop: 8, backgroundColor: '#f9f9f9', padding: '8px 12px', borderRadius: 6 }}>
                <Text strong style={{fontSize: 13}}>Các câu trả lời gần đây:</Text>
                {hasResponses ? (
                    <ul style={{ paddingLeft: 20, marginTop: 4, marginBottom: 0 }}>
                        {item.recentTextResponses.map((res: string, idx: number) => (
                            <li key={idx} style={{ marginBottom: 4, fontSize: 13, color: '#555' }}>
                                {res}
                            </li>
                        ))}
                    </ul>
                ) : (
                    <div style={{fontStyle: 'italic', color: '#999', fontSize: 13, marginTop: 4}}>Chưa có câu trả lời nào.</div>
                )}
            </div>
        );
    }
    return null;
  };

  const renderAIAnalysis = () => {
    if (aiDetail.status === ReduxStatus.LOADING) {
      return (
        <div style={{ textAlign: 'center', padding: '40px 0' }}>
          <Spin size="large" tip="Đang tải kết quả phân tích AI..." />
        </div>
      );
    }

    if (aiDetail.status === ReduxStatus.FAILURE) {
      return (
        <Empty
          description="Không thể tải kết quả phân tích AI"
          image={Empty.PRESENTED_IMAGE_SIMPLE}
        />
      );
    }

    if (!aiDetail.data || aiDetail.data.length === 0) {
      return (
        <Empty
          description={
            <div>
              <p>Chưa có kết quả phân tích AI</p>
              <p style={{ fontSize: 12, color: '#888' }}>Vui lòng nhấn nút "Phân tích AI" ở danh sách báo cáo</p>
            </div>
          }
          image={Empty.PRESENTED_IMAGE_SIMPLE}
        />
      );
    }

    return (
      <List
        grid={{ gutter: 16, column: 1 }}
        dataSource={aiDetail.data}
        renderItem={(item: IAIReportDetail) => (
          <List.Item>
            <Card
              size="small"
              title={
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                  <span style={{ fontSize: 15, fontWeight: 600 }}>{item.tenTieuChi}</span>
                  <Tag color={item.diemCamXuc >= 0.7 ? 'green' : item.diemCamXuc >= 0.4 ? 'orange' : 'red'}>
                    Điểm: {(item.diemCamXuc * 10).toFixed(1)}/10
                  </Tag>
                </div>
              }
              style={{ boxShadow: '0 2px 4px rgba(0,0,0,0.08)' }}
            >
              <div style={{ marginBottom: 12 }}>
                <Text strong style={{ color: '#1890ff' }}>Nhận xét cảm xúc:</Text>
                <Paragraph style={{ marginTop: 4, marginBottom: 0 }}>{item.nhanCamXuc}</Paragraph>
              </div>

              <div style={{ marginBottom: 12 }}>
                <Text strong style={{ color: '#52c41a' }}>Tóm tắt nội dung:</Text>
                <Paragraph style={{ marginTop: 4, marginBottom: 0 }}>{item.tomTatNoiDung}</Paragraph>
              </div>

              <div style={{ marginBottom: 12 }}>
                <Text strong style={{ color: '#faad14' }}>Xu hướng:</Text>
                <Paragraph style={{ marginTop: 4, marginBottom: 0 }}>{item.xuHuong}</Paragraph>
              </div>

              <Alert
                message="Gợi ý cải thiện"
                description={item.goiYCaiThien}
                type="info"
                showIcon
                style={{ marginTop: 8 }}
              />
            </Card>
          </List.Item>
        )}
        style={{ maxHeight: '500px', overflowY: 'auto', paddingRight: 8 }}
      />
    );
  };

  const tabItems = [
    {
      key: '1',
      label: (
        <span>
          <BarChartOutlined /> Thống kê
        </span>
      ),
      children: (
        <Form form={form} layout="vertical">
          <Row gutter={16}>
            <Col span={24}>
              <Form.Item label="Tên khảo sát" name="tenKhaoSat" style={{marginBottom: 12}}>
                <Input disabled />
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={16} style={{ backgroundColor: '#f0f5ff', padding: '12px', borderRadius: 8, marginBottom: 24 }}>
            <Col span={8}>
              <Form.Item label="Tổng người tham gia" name="totalParticipants" style={{marginBottom: 0}}>
                <Input disabled />
              </Form.Item>
            </Col>
            <Col span={8}>
              <Form.Item label="Điểm trung bình" name="averageScore" style={{marginBottom: 0}}>
                  <Input disabled />
              </Form.Item>
            </Col>
            <Col span={8}>
              <Form.Item label="Ngày tạo báo cáo" name="lastGenerated" style={{marginBottom: 0}}>
                <DatePicker showTime format="DD/MM/YYYY HH:mm" disabled style={{ width: '100%', backgroundColor: 'transparent', color: '#000' }} />
              </Form.Item>
            </Col>
          </Row>
          
          <Divider orientation="left" style={{ borderColor: '#d9d9d9' }}>Thống kê chi tiết câu hỏi</Divider>
          <List
              grid={{ gutter: 16, column: 1 }}
              dataSource={report?.statistics?.questions}
              renderItem={(item, index) => (
                  <List.Item>
                      <Card 
                          size="small" 
                          title={<span style={{fontSize: 14, fontWeight: 600}}>Câu {index + 1}: {item.content}</span>}
                          extra={<Tag color={item.type === 3 ? "purple" : "blue"}>{item.type === 3 ? "Tự luận" : "Trắc nghiệm"}</Tag>}
                          style={{ boxShadow: '0 1px 2px rgba(0,0,0,0.05)' }}
                      >
                          {renderQuestionStats(item)}
                      </Card>
                  </List.Item>
              )}
              style={{ maxHeight: '400px', overflowY: 'auto', paddingRight: 8 }}
          />

          <Divider orientation="left" style={{ borderColor: '#d9d9d9', marginTop: 24 }}>Danh sách người trả lời ({report?.respondents?.length || 0})</Divider>
          <List
              size="small"
              bordered
              dataSource={report?.respondents}
              renderItem={(item, index) => (
                  <List.Item>
                      <List.Item.Meta
                          avatar={<Tag color="cyan">#{index + 1}</Tag>}
                          title={<Text strong>{item.fullName}</Text>}
                          description={<span>Mã: {item.userCode} | Điểm: <Tag color="gold">{item.totalScore}</Tag></span>}
                      />
                      <div style={{ fontSize: 12, color: '#888' }}>
                          {dayjs(item.submitTime).format('DD/MM/YYYY HH:mm')}
                      </div>
                  </List.Item>
              )}
              style={{ maxHeight: '200px', overflowY: 'auto' }}
          />
        </Form>
      )
    },
    {
      key: '2',
      label: (
        <span>
          <RobotOutlined /> Phân tích AI
        </span>
      ),
      children: renderAIAnalysis()
    }
  ];

  return (
    <Modal
      title={"Chi tiết báo cáo khảo sát"}
      open={isModalOpen}
      onCancel={handleCancel}
      width={900}
      footer={[
        <Button key="close" icon={<CloseOutlined />} onClick={handleCancel}>
          Đóng
        </Button>
      ]}
      maskClosable={false}
      style={{ top: 20 }}
    >
      <Tabs defaultActiveKey="1" items={tabItems} />
    </Modal>
  );
};

export default ReportDetailModal;