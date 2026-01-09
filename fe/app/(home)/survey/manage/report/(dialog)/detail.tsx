'use client';
import React, { useEffect } from 'react';
import { Button, Col, DatePicker, Form, Input, Modal, Row, List, Typography, Progress, Tag, Divider, Card } from 'antd';
import { CloseOutlined } from '@ant-design/icons';
import { IReportDetail } from '@models/survey/report.model';
import dayjs from 'dayjs';

const { Title, Text } = Typography;

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

  useEffect(() => {
    if (isModalOpen && report) {
      form.setFieldsValue({
        ...report,
        lastGenerated: dayjs(report.lastGenerated),
      });
    }
  }, [isModalOpen, report, form]);

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
    </Modal>
  );
};

export default ReportDetailModal;