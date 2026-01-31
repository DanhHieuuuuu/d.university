'use client';

import React, { useState } from 'react';
import { Card, Form, Input, Button, DatePicker, TimePicker, InputNumber, message, Row, Col } from 'antd';
import { useRouter, useSearchParams } from 'next/navigation';
import dayjs from 'dayjs';
import { useAppDispatch } from '@redux/hooks';
import { ICreateReceptionTime, ICreateReceptionTimeList } from '@models/delegation/delegation.model';
import { createPrepare, createReceptionTime } from '@redux/feature/delegation/delegationThunk';
import { toast } from 'react-toastify';
import { ArrowLeftOutlined, DeleteOutlined, PlusOutlined } from '@ant-design/icons';

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

      const items: ICreateReceptionTime[] = values.receptionTimes.map((rt: any) => ({
        delegationIncomingId,
        date: rt.date.format('YYYY-MM-DD'),
        startDate: rt.startTime.format('HH:mm:ss'),
        endDate: rt.endTime.format('HH:mm:ss'),
        content: rt.content,
        totalPerson: rt.totalPerson,
        address: rt.address
      }));

      const payload: ICreateReceptionTimeList = { items };

      const receptionResult = await dispatch(createReceptionTime(payload)).unwrap();

      // tạo prepares
      await Promise.all(
        receptionResult.data.map((rt: any, index: number) => {
          const prepares = values.receptionTimes[index]?.prepares;
          if (!prepares?.length) return Promise.resolve();

          return dispatch(
            createPrepare({
              items: prepares.map((p: any) => ({
                name: p.name,
                description: p.description,
                money: p.money,
                receptionTimeId: rt.id
              }))
            })
          ).unwrap();
        })
      );

      toast.success('Tạo mới thành công!');
      router.back();
    } catch (err: any) {
      toast.error(String(err));
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
      extra={
        <div className="flex justify-center gap-2">
          <Button type="primary" onClick={() => form.submit()} loading={loading}>
            Tạo mới
          </Button>
          <Button onClick={() => router.back()} disabled={loading} >Hủy</Button>
        </div>
      }
      bodyStyle={{ maxHeight: '90%', overflow: 'auto' }}
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={onFinish}
        initialValues={{
          receptionTimes: [
            {
              totalPerson: 0
            }
          ]
        }}
      >
        <Form.List name="receptionTimes">
          {(fields, { add, remove }) => (
            <>
              {fields.map(({ key, name, ...restField }, index) => (
                <Card
                  key={key}
                  size="small"
                  title={`Thời gian tiếp đoàn ${index + 1}`}
                  className="mb-3"
                  extra={
                    fields.length > 1 && (
                      <Button danger size="small" onClick={() => remove(name)}>
                        Xóa
                      </Button>
                    )
                  }
                >
                  <Row gutter={16}>
                    <Col span={8}>
                      <Form.Item
                        {...restField}
                        name={[name, 'date']}
                        label="Ngày tiếp đoàn"
                        rules={[{ required: true, message: 'Chọn ngày' }]}
                      >
                        <DatePicker style={{ width: '100%' }} />
                      </Form.Item>
                    </Col>

                    <Col span={8}>
                      <Form.Item
                        {...restField}
                        name={[name, 'startTime']}
                        label="Bắt đầu"
                        rules={[{ required: true, message: 'Chọn giờ bắt đầu' }]}
                      >
                        <TimePicker format="HH:mm:ss" style={{ width: '100%' }} />
                      </Form.Item>
                    </Col>

                    <Col span={8}>
                      <Form.Item
                        {...restField}
                        name={[name, 'endTime']}
                        label="Kết thúc"
                        rules={[{ required: true, message: 'Chọn giờ kết thúc' }]}
                      >
                        <TimePicker format="HH:mm:ss" style={{ width: '100%' }} />
                      </Form.Item>
                    </Col>
                  </Row>

                  <Row gutter={16}>
                    <Col span={12}>
                      <Form.Item {...restField} name={[name, 'totalPerson']} label="Tổng số người">
                        <InputNumber min={0} style={{ width: '100%' }} />
                      </Form.Item>
                    </Col>

                    <Col span={12}>
                      <Form.Item {...restField} name={[name, 'address']} label="Địa điểm">
                        <Input />
                      </Form.Item>
                    </Col>
                  </Row>

                  <Form.Item {...restField} name={[name, 'content']} label="Nội dung">
                    <Input.TextArea rows={2} />
                  </Form.Item>
                  {/* PREPARES */}
                  <Form.List name={[name, 'prepares']}>
                    {(prepareFields, { add: addPrepare, remove: removePrepare }) => (
                      <>
                        {prepareFields.map(({ key: pKey, name: pName }) => (
                          <Card
                            key={pKey}
                            size="small"
                            className="bg-gray-10 mb-2"
                            title="Đồ chuẩn bị"
                            extra={
                              <Button
                                type="text"
                                danger
                                icon={<DeleteOutlined />}
                                onClick={() => removePrepare(pName)}
                              />
                            }
                          >
                            <Row gutter={12}>
                              <Col span={8}>
                                <Form.Item
                                  name={[pName, 'name']}
                                  label="Tên"
                                  rules={[{ required: true, message: 'Nhập tên chuẩn bị' }]}
                                >
                                  <Input />
                                </Form.Item>
                              </Col>

                              <Col span={10}>
                                <Form.Item name={[pName, 'description']} label="Mô tả">
                                  <Input />
                                </Form.Item>
                              </Col>

                              <Col span={6}>
                                <Form.Item
                                  name={[pName, 'money']}
                                  label="Chi phí (VNĐ)"
                                  rules={[{ required: true, message: 'Nhập chi phí' }]}
                                >
                                  <InputNumber min={0} style={{ width: '100%' }} />
                                </Form.Item>
                              </Col>
                            </Row>
                          </Card>
                        ))}

                        <Button type="dashed" size="small" icon={<PlusOutlined />} onClick={() => addPrepare()}>
                          Thêm đồ chuẩn bị
                        </Button>
                      </>
                    )}
                  </Form.List>
                </Card>
              ))}
              <div style={{ textAlign: 'center' }}>
                <Button type="dashed" size="small" style={{ padding: '14px' }} onClick={() => add()}>
                  + Thêm thời gian tiếp đoàn
                </Button>
              </div>
            </>
          )}
        </Form.List>
      </Form>
    </Card>
  );
};

export default CreateReceptionTimePage;
