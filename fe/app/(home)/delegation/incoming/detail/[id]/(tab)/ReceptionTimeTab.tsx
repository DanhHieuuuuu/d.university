'use client';

import React, { useEffect } from 'react';
import dayjs from 'dayjs';
import { Form, Input, InputNumber, DatePicker, TimePicker, Card, Button, Row, Col } from 'antd';
import { IReceptionTime } from '@models/delegation/delegation.model';
import { toast } from 'react-toastify';
import { useAppDispatch } from '@redux/hooks';
import { updateReceptionTimes } from '@redux/feature/delegation/delegationThunk';

type ReceptionTimeTabProps = {
  data: IReceptionTime[] | null;
  isEdit?: boolean;
  onUpdated?: () => void;
};

const ReceptionTimeTab: React.FC<ReceptionTimeTabProps> = ({ data, isEdit = false, onUpdated }, ref) => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  useEffect(() => {
    if (!data) return;
    form.setFieldsValue({
      items: data.map((item) => ({
        ...item,
        date: dayjs(item.date),
        startDate: dayjs(item.startDate, 'HH:mm:ss'),
        endDate: dayjs(item.endDate, 'HH:mm:ss')
      }))
    });
  }, [data]);

  const onFinish = async (values: any) => {
    try {
      const payload = {
        items: values.items.map((x: any) => ({
          id: x.id,
          delegationIncomingId: x.delegationIncomingId,
          date: x.date.format('YYYY-MM-DD'),
          startDate: x.startDate.format('HH:mm:ss'),
          endDate: x.endDate.format('HH:mm:ss'),
          content: x.content,
          totalPerson: x.totalPerson,
          address: x.address
        }))
      };

      await dispatch(updateReceptionTimes(payload)).unwrap();
      toast.success('Cập nhật thời gian tiếp đoàn thành công');
      onUpdated?.();
    } catch {
      toast.error('Cập nhật thất bại');
    }
  };

  if (!data || data.length === 0) {
    return <div className="text-center text-gray-400">Không có thời gian tiếp đoàn</div>;
  }

  return (
    <Form form={form} layout="vertical" onFinish={onFinish}>
      <Form.List name="items">
        {(fields, { remove }) => (
          <>
            {fields.map(({ key, name }, index) => (
              <Card
                key={key}
                size="small"
                className="mb-4"
                title={`Lần tiếp đoàn ${index + 1}`}
                extra={
                  isEdit && (
                    <Button
                      type="default"
                      danger
                      style={{ borderColor: 'red', color: 'red' }}
                      onClick={() => remove(name)}
                    >
                      Xoá
                    </Button>
                  )
                }
              >
                <Form.Item name={[name, 'id']} hidden />
                <Form.Item name={[name, 'delegationIncomingId']} hidden />
                <Row gutter={16}>
                  <Col span={8}>
                    <Form.Item label="Ngày tiếp đoàn" name={[name, 'date']}>
                      <DatePicker style={{ width: '100%' }} disabled={!isEdit} />
                    </Form.Item>
                  </Col>
                  <Col span={8}>
                    <Form.Item label="Thời gian bắt đầu" name={[name, 'startDate']}>
                      <TimePicker format="HH:mm" style={{ width: '100%' }} disabled={!isEdit} />
                    </Form.Item>
                  </Col>
                  <Col span={8}>
                    <Form.Item label="Thời gian kết thúc" name={[name, 'endDate']}>
                      <TimePicker format="HH:mm" style={{ width: '100%' }} disabled={!isEdit} />
                    </Form.Item>
                  </Col>
                </Row>
                <Row gutter={16}>
                  <Col span={8}>
                    <Form.Item label="Địa điểm" name={[name, 'address']}>
                      <Input disabled={!isEdit} />
                    </Form.Item>
                  </Col>
                  <Col span={8}>
                    <Form.Item label="Tổng số người" name={[name, 'totalPerson']}>
                      <InputNumber style={{ width: '100%' }} disabled={!isEdit} />
                    </Form.Item>
                  </Col>
                  <Col span={8}>
                    <Form.Item label="Nội dung" name={[name, 'content']}>
                      <Input disabled={!isEdit} />
                    </Form.Item>
                  </Col>
                </Row>
              </Card>
            ))}
          </>
        )}
      </Form.List>
    </Form>
  );
};

export default ReceptionTimeTab;
