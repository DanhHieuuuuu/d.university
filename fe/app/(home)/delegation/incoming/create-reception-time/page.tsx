'use client';

import React, { useState } from 'react';
import { Card, Form, Input, Button, DatePicker, TimePicker, InputNumber, message, Row, Col } from 'antd';
import { useRouter, useSearchParams } from 'next/navigation';
import dayjs from 'dayjs';
import { useAppDispatch } from '@redux/hooks';
import { ICreateReceptionTime } from '@models/delegation/delegation.model';
import { createReceptionTime } from '@redux/feature/delegation/delegationThunk';
import { toast } from 'react-toastify';
import { ArrowLeftOutlined } from '@ant-design/icons';

const CreateReceptionTimePage: React.FC = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const router = useRouter();
  const [loading, setLoading] = useState(false);
  const searchParams = useSearchParams();
  const delegationIncomingId = Number(searchParams.get('delegationIncomingId')); 

  const onFinish = async (values: any) => {
    try {
      setLoading(true);

      const payload: ICreateReceptionTime = {
        delegationIncomingId, 
        date: values.date.format('YYYY-MM-DD'),
        startDate: values.startTime.format('HH:mm:ss'),
        endDate: values.endTime.format('HH:mm:ss'),
        content: values.content,
        totalPerson: values.totalPerson,
        address: values.address
      };

      await dispatch(createReceptionTime(payload)).unwrap();
      toast.success('Tạo thời gian tiếp đoàn thành công!');
      router.back();
    } catch (error: any) {
      toast.error(error?.message || 'Tạo thất bại!');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Card
      className="h-full"
      title={
        <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
          <div
            onClick={() => router.back()}
            style={{
              display: 'inline-flex',
              alignItems: 'center',
              justifyContent: 'center',
              width: 34,
              height: 34,
              borderRadius: 6,
              cursor: 'pointer',
              transition: '0.2s'
            }}
            className="hover-box"
          >
            <ArrowLeftOutlined style={{ fontSize: 16 }} />
          </div>
          <span>Thêm thời gian tiếp đoàn</span>
        </div>
      }
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={onFinish}
        initialValues={{
          totalPerson: 0
        }}
      >
        <Row gutter={16}>
          <Col span={8}>
            <Form.Item
              label="Ngày tiếp đoàn"
              name="date"
              rules={[{ required: true, message: 'Vui lòng chọn ngày tiếp đoàn' }]}
            >
              <DatePicker style={{ width: '100%' }} />
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item
              label="Thời gian bắt đầu"
              name="startTime"
              rules={[{ required: true, message: 'Vui lòng chọn thời gian bắt đầu' }]}
            >
              <TimePicker format="HH:mm:ss" style={{ width: '100%' }} />
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item
              label="Thời gian kết thúc"
              name="endTime"
              rules={[{ required: true, message: 'Vui lòng chọn thời gian kết thúc' }]}
            >
              <TimePicker format="HH:mm:ss" style={{ width: '100%' }} />
            </Form.Item>
          </Col>
        </Row>

        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tổng số người"
              name="totalPerson"
              rules={[{ message: 'Vui lòng nhập tổng số người' }]}
            >
              <InputNumber min={0} style={{ width: '100%' }} />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="Địa điểm" name="address" rules={[{message: 'Vui lòng nhập địa điểm' }]}>
              <Input />
            </Form.Item>
          </Col>
        </Row>

        <Form.Item label="Nội dung" name="content" rules={[{ message: 'Vui lòng nhập nội dung' }]}>
          <Input.TextArea rows={3} />
        </Form.Item>

        <Form.Item>
          <div className="flex justify-center gap-2">
            <Button type="primary" htmlType="submit" loading={loading}>
              Tạo mới
            </Button>
            <Button onClick={() => router.back()}>Hủy</Button>
          </div>
        </Form.Item>
      </Form>
    </Card>
  );
};

export default CreateReceptionTimePage;
