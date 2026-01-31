'use client';

import React, { useEffect, useState } from 'react';
import { Card, Form, Input, Button, Select, Row, Col, Space } from 'antd';
import { useRouter, useSearchParams } from 'next/navigation';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { toast } from 'react-toastify';
import { ArrowLeftOutlined, PlusOutlined, MinusCircleOutlined, DeleteOutlined } from '@ant-design/icons';
import { ICreateSupporter } from '@models/delegation/delegation.model';
import { createSupporter, getListNhanSu } from '@redux/feature/delegation/delegationThunk';

const CreateSupporterPage: React.FC = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { listNhanSu } = useAppSelector((state) => state.delegationState);
  const router = useRouter();
  const [loading, setLoading] = useState(false);
  const searchParams = useSearchParams();

  const departmentSupportId = Number(searchParams.get('departmentSupportId'));

  const onFinish = async (values: any) => {
    try {
      setLoading(true);

      const payload: ICreateSupporter = {
        departmentSupportId,
        supporters: values.supporters
      };

      await dispatch(createSupporter(payload)).unwrap();
      toast.success('Thêm nhân sự hỗ trợ thành công');
      router.back();
    } catch (error: any) {
      toast.error(error?.message || 'Tạo thất bại');
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    // Nếu listNhanSu chưa có hoặc trống thì fetch 
    if (!listNhanSu || listNhanSu.length === 0) {
      dispatch(getListNhanSu());
    }
  }, [dispatch]);

  return (
    <Card
      title={
        <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
          <div
            onClick={() => router.back()}
            style={{
              width: 34,
              height: 34,
              borderRadius: 6,
              cursor: 'pointer',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center'
            }}
          >
            <ArrowLeftOutlined />
          </div>
          <span>Thêm nhân sự hỗ trợ</span>
        </div>
      }
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={onFinish}
        initialValues={{
          supporters: [{ supporterId: undefined, supporterCode: '' }]
        }}
      >
        <Form.List name="supporters">
          {(fields, { add, remove }) => (
            <>
              {fields.map(({ key, name, ...restField }) => (
                <Row gutter={16} key={key} align="middle">
                  <Col span={10}>
                    <Form.Item
                      {...restField}
                      label="Nhân sự"
                      name={[name, 'supporterId']}
                      rules={[{ required: true, message: 'Chọn nhân sự' }]}
                    >
                      <Select
                        placeholder="Chọn nhân sự"
                        options={listNhanSu.map((ns: any) => ({
                          value: ns.idNhanSu,
                          label: ns.tenNhanSu
                        }))}
                        onChange={(value) => {
                          const nhanSu = listNhanSu.find((ns: any) => ns.idNhanSu === value);

                          if (nhanSu) {
                            const supporters = form.getFieldValue('supporters') || [];
                            supporters[name] = {
                              ...supporters[name],
                              supporterCode: nhanSu.supporterCode // 👈 field mã nhân sự
                            };

                            form.setFieldsValue({ supporters });
                          }
                        }}
                      />
                    </Form.Item>
                  </Col>

                  <Col span={10}>
                    <Form.Item
                      {...restField}
                      label="Mã nhân sự hỗ trợ"
                      name={[name, 'supporterCode']}
                      rules={[{ required: true, message: 'Nhập mã hỗ trợ' }]}
                    >
                      <Input placeholder="VD: HT001" />
                    </Form.Item>
                  </Col>

                  <Col
                    span={4}
                    style={{
                      display: 'flex',
                      alignItems: 'flex-end',
                      justifyContent: 'center'
                    }}
                  >
                    {fields.length > 1 && (
                      <DeleteOutlined
                        style={{
                          fontSize: 20,
                          color: '#ff4d4f',
                          cursor: 'pointer'
                        }}
                        onClick={() => remove(name)}
                      />
                    )}
                  </Col>
                </Row>
              ))}

              <Form.Item>
                <Button
                  type="dashed"
                  size="small"
                  icon={<PlusOutlined />}
                  style={{ padding: '13px' }}
                  onClick={() => add()}
                >
                  Thêm người hỗ trợ
                </Button>
              </Form.Item>
            </>
          )}
        </Form.List>

        <Form.Item>
          <Space style={{ width: '100%', justifyContent: 'center' }}>
            <Button onClick={() => router.back()}>Hủy</Button>
            <Button type="primary" htmlType="submit" loading={loading}>
              Lưu
            </Button>
          </Space>
        </Form.Item>
      </Form>
    </Card>
  );
};

export default CreateSupporterPage;
