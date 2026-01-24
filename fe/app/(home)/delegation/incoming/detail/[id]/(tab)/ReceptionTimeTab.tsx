'use client';

import React, { useEffect, forwardRef, useImperativeHandle } from 'react';
import dayjs from 'dayjs';
import { Form, Input, InputNumber, DatePicker, TimePicker, Card, Button, Row, Col, FormInstance } from 'antd';
import { IReceptionTime } from '@models/delegation/delegation.model';
import { toast } from 'react-toastify';
import { useAppDispatch } from '@redux/hooks';
import { updatePrepare, updateReceptionTimes } from '@redux/feature/delegation/delegationThunk';
import { DeleteOutlined } from '@ant-design/icons';
type ReceptionTimeTabProps = {
  data: IReceptionTime[] | null;
  isEdit?: boolean;
  onUpdated?: () => void;
};

const ReceptionTimeTab = forwardRef<FormInstance, ReceptionTimeTabProps>(({ data, isEdit = false, onUpdated }, ref) => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  useImperativeHandle(ref, () => form);
  useEffect(() => {
    if (!data) return;
    form.setFieldsValue({
      items: data.map((item) => ({
        ...item,
        date: dayjs(item.date),
        startDate: dayjs(item.startDate, 'HH:mm:ss'),
        endDate: dayjs(item.endDate, 'HH:mm:ss'),
        prepares: item.prepares || []
      }))
    });
  }, [data]);

  const onFinish = async (values: any) => {
    try {
      // Update ReceptionTime
      const receptionPayload = {
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

      await dispatch(updateReceptionTimes(receptionPayload)).unwrap();
      //Update Prepare theo ReceptionTime
      for (const item of values.items) {
        const preparePayload = {
          receptionTimeId: item.id,
          items: (item.prepares || []).map((p: any) => ({
            id: p.id,
            name: p.name,
            description: p.description,
            money: p.money
          }))
        };
        await dispatch(updatePrepare(preparePayload)).unwrap();
      }
      onUpdated?.();
    } catch (err) {}
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
                className="mb-4 overflow-hidden"
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
                {/* PREPARES */}
                <Form.List name={[name, 'prepares']}>
                  {(prepareFields, { remove: removePrepare }) => (
                    <>
                      {prepareFields.map(({ key: pKey, name: pName }, pIndex) => (
                        <Card
                          key={pKey}
                          size="small"
                          className="bg-gray-10 mb-2"
                          title={`${pIndex + 1}. Đồ chuẩn bị tiếp đoàn `}
                          extra={
                            isEdit && (
                              <Button
                                type="text"
                                danger
                                icon={<DeleteOutlined />}
                                onClick={() => removePrepare(pName)}
                              />
                            )
                          }
                        >
                          <Form.Item name={[pName, 'id']} hidden />
                          <Row gutter={12}>
                            <Col span={8}>
                              <Form.Item
                                label="Tên"
                                name={[pName, 'name']}
                                rules={[{ required: true, message: 'Nhập tên chuẩn bị' }]}
                              >
                                <Input disabled={!isEdit} />
                              </Form.Item>
                            </Col>

                            <Col span={10}>
                              <Form.Item label="Mô tả" name={[pName, 'description']}>
                                <Input disabled={!isEdit} />
                              </Form.Item>
                            </Col>

                            <Col span={6}>
                              <Form.Item
                                label="Chi phí"
                                name={[pName, 'money']}
                                rules={[{ required: true, message: 'Nhập chi phí' }]}
                              >
                                <InputNumber style={{ width: '100%' }} min={0} disabled={!isEdit} />
                              </Form.Item>
                            </Col>
                          </Row>
                        </Card>
                      ))}
                    </>
                  )}
                </Form.List>
              </Card>
            ))}
          </>
        )}
      </Form.List>
    </Form>
  );
});
export default ReceptionTimeTab;
