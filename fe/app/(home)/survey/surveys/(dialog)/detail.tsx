'use client';
import React, { useEffect, useState } from 'react';
import { Button, Col, DatePicker, Form, Input, Modal, Row, Tabs, TabsProps, Card, Select, Checkbox, Tag } from 'antd';
import { CloseOutlined } from '@ant-design/icons';
import { ISurveyDetail } from '@models/survey/survey.model';
import dayjs from 'dayjs';

const { TextArea } = Input;
const { Option } = Select;

type SurveyDetailModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  survey: ISurveyDetail | null;
  onClose?: () => void;
};

const SurveyDetailModal: React.FC<SurveyDetailModalProps> = ({ isModalOpen, setIsModalOpen, survey, onClose }) => {
  const [form] = Form.useForm();
  const [activeTab, setActiveTab] = useState('1');

  useEffect(() => {
    if (isModalOpen && survey) {
      const detail = survey as ISurveyDetail;
      const questionsData = detail.surveyRequest?.questions || [];
      const maYeuCauGoc = detail.surveyRequest?.maYeuCau || detail.maYeuCauGoc || '';
      form.setFieldsValue({
        ...detail,
        thoiGianBatDau: dayjs(detail.thoiGianBatDau),
        thoiGianKetThuc: dayjs(detail.thoiGianKetThuc),
        maYeuCauGoc: maYeuCauGoc,
        questions: questionsData
      });
      setActiveTab('1');
    }
  }, [isModalOpen, survey, form]);

  const handleCancel = () => {
    setIsModalOpen(false);
    form.resetFields();
    onClose?.();
  };

  const renderGeneralInfo = () => (
    <>
      <Row gutter={16}>
        <Col span={12}>
          <Form.Item label="Mã khảo sát" name="maKhaoSat">
            <Input disabled style={{ color: '#000' }} />
          </Form.Item>
        </Col>
        <Col span={12}>
          <Form.Item label="Tên khảo sát" name="tenKhaoSat">
            <Input disabled style={{ color: '#000' }} />
          </Form.Item>
        </Col>
      </Row>

      <Form.Item label="Mô tả" name="moTa">
        <TextArea rows={3} disabled style={{ color: '#000' }} />
      </Form.Item>

      <Row gutter={16}>
        <Col span={12}>
          <Form.Item label="Thời gian bắt đầu" name="thoiGianBatDau">
            <DatePicker showTime format="DD/MM/YYYY HH:mm" style={{ width: '100%', color: '#000' }} disabled />
          </Form.Item>
        </Col>
        <Col span={12}>
          <Form.Item label="Thời gian kết thúc" name="thoiGianKetThuc">
            <DatePicker showTime format="DD/MM/YYYY HH:mm" style={{ width: '100%', color: '#000' }} disabled />
          </Form.Item>
        </Col>
      </Row>

      <Row gutter={16}>
        <Col span={12}>
          <Form.Item label="Trạng thái" name="statusName">
            <Input disabled style={{ color: '#000' }} />
          </Form.Item>
        </Col>
        <Col span={12}>
          <Form.Item label="Mã yêu cầu gốc" name="maYeuCauGoc">
            <Input disabled style={{ color: '#000' }} />
          </Form.Item>
        </Col>
      </Row>
    </>
  );

  const renderQuestions = () => (
    <Card
      title="Nội dung câu hỏi"
      size="small"
      variant="borderless"
      styles={{
        header: { display: 'none' },
        body: { padding: 0, maxHeight: '500px', overflowY: 'auto' }
      }}
    >
      <Form.List name="questions">
        {(fields) => (
          <div className="flex flex-col gap-4">
            {fields.map(({ key, name, ...restField }) => (
              <Card
                key={key}
                size="small"
                title={<span className="font-bold text-blue-600">Câu hỏi {name + 1}</span>}
                className="border-gray-200 bg-gray-50"
              >
                <Row gutter={16}>
                  <Col span={16}>
                    <Form.Item {...restField} name={[name, 'noiDung']} label="Nội dung" style={{ marginBottom: 8 }}>
                      <TextArea rows={2} disabled style={{ color: '#000', fontWeight: 500 }} />
                    </Form.Item>
                  </Col>
                  <Col span={8}>
                    <Form.Item
                      {...restField}
                      name={[name, 'loaiCauHoi']}
                      label="Loại câu hỏi"
                      style={{ marginBottom: 8 }}
                    >
                      <Select disabled style={{ color: '#000' }}>
                        <Option value={1}>Trắc nghiệm</Option>
                        <Option value={2}>Chọn nhiều đáp án</Option>
                        <Option value={3}>Tự luận</Option>
                      </Select>
                    </Form.Item>
                  </Col>
                </Row>

                {/* Answers */}
                <div className="pl-4 border-l-2 border-blue-200 mt-2">
                  <Form.List name={[name, 'answers']}>
                    {(answerFields) => (
                      <>
                        {answerFields.map((ans) => {
                          const { key, ...ansRestField } = ans;
                          return (
                            <Row key={key} gutter={8} align="middle" className="mb-2">
                              <Col span={1}>
                                <Form.Item name={[ans.name, 'isCorrect']} valuePropName="checked" noStyle>
                                  <Checkbox disabled />
                                </Form.Item>
                              </Col>
                              <Col span={16}>
                                <Form.Item name={[ans.name, 'noiDung']} noStyle>
                                  <Input size="small" disabled className="bg-transparent border-none text-black" />
                                </Form.Item>
                              </Col>
                              <Col span={7}>
                                <Form.Item name={[ans.name, 'value']} noStyle>
                                  <span className="text-xs text-gray-500 italic">
                                    (Điểm: {form.getFieldValue(['questions', name, 'answers', ans.name, 'value'])})
                                  </span>
                                </Form.Item>
                              </Col>
                            </Row>
                          );
                        })}
                        {answerFields.length === 0 && (
                          <span className="text-gray-400 italic text-xs">Không có đáp án (Tự luận)</span>
                        )}
                      </>
                    )}
                  </Form.List>
                </div>
              </Card>
            ))}
            {fields.length === 0 && (
              <div className="text-center py-8 text-gray-400">Chưa có dữ liệu câu hỏi</div>
            )}
          </div>
        )}
      </Form.List>
    </Card>
  );

  const items: TabsProps['items'] = [
    { key: '1', label: 'Thông tin chung', children: renderGeneralInfo() },
    { key: '2', label: `Danh sách câu hỏi`, children: renderQuestions() }
  ];

  return (
    <Modal
      title="Chi tiết khảo sát"
      open={isModalOpen}
      onCancel={handleCancel}
      width={900}
      footer={[
        <Button key="close" icon={<CloseOutlined />} onClick={handleCancel}>
          Đóng
        </Button>
      ]}
      maskClosable={true}
    >
      <Form form={form} layout="vertical">
        <Tabs defaultActiveKey="1" activeKey={activeTab} onChange={setActiveTab} items={items} type="card" />
      </Form>
    </Modal>
  );
};

export default SurveyDetailModal;
