'use client';
import { toast } from 'react-toastify';
import React, { useEffect, useState } from 'react';
import { Button, Col, DatePicker, Form, Input, Modal, Row, Space, Select, Card, Checkbox, Tabs, TabsProps } from 'antd';
import { CloseOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { createRequest, updateRequest } from '@redux/feature/survey/surveyThunk';
import { resetRequestStatus } from '@redux/feature/survey/surveySlice';
import { ICreateRequest, IUpdateRequest, IViewRequest } from '@models/survey/request.model';
import { surveyStatusConst } from '@/app/(home)/survey/const/targetType.const';
import { getListPhongBan } from '@redux/feature/delegation/delegationThunk';
import { IViewPhongBan } from '@models/danh-muc/phong-ban.model';
import { getAllKhoa } from '@redux/feature/dao-tao/khoaThunk';
import { IViewKhoa } from '@models/dao-tao/khoa.model';
import dayjs from 'dayjs';

const { Option } = Select;
const { TextArea } = Input;

type CreateOrUpdateRequestModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  request?: IViewRequest | null;
  onSuccess?: () => void;
  isViewMode?: boolean;
};

type IRequestForm = Omit<ICreateRequest, 'thoiGianBatDau' | 'thoiGianKetThuc'> & {
  thoiGianBatDau: dayjs.Dayjs;
  thoiGianKetThuc: dayjs.Dayjs;
};

const CreateOrUpdateRequestModal: React.FC<CreateOrUpdateRequestModalProps> = ({
  isModalOpen,
  setIsModalOpen,
  request,
  onSuccess,
  isViewMode = false
}) => {
  const [form] = Form.useForm<IRequestForm>();
  const dispatch = useAppDispatch();
  const { request: requestState } = useAppSelector((state) => state.surveyState);

  const [activeTab, setActiveTab] = useState<string>('1');
  const [departments, setDepartments] = React.useState<IViewPhongBan[]>([]);
  const [faculties, setFaculties] = useState<IViewKhoa[]>([]);
  
  const isEdit = !!request;

  useEffect(() => {
    if (isModalOpen) {
      if (isEdit && request) {
        form.setFieldsValue({
          ...request,
          thoiGianBatDau: dayjs(request.thoiGianBatDau),
          thoiGianKetThuc: dayjs(request.thoiGianKetThuc),
          targets: (request.targets && request.targets.length > 0) ? request.targets : [{}],
          questions: request.questions || [],
          criterias: request.criterias || []
        });
      } else {
        form.resetFields();
        form.setFieldsValue({
          targets: [{}],
          questions: [{ answers: [{ thuTu: 1 }, { thuTu: 2 }] }], // 1 question with 2 answers with order
          criterias: [{}] // 1 criteria
        });
      }
      setActiveTab('1');
    }
  }, [isModalOpen, isEdit, request, form]);

  useEffect(() => {
    if (isModalOpen) {
      dispatch(getListPhongBan())
        .unwrap()
        .then((res: any) => {
          let deptList = [];
          deptList = res;
          setDepartments(deptList);
        })
        .catch((err: any) => {
          console.error('Lỗi khi lấy danh sách phòng ban:', err);
        });

      dispatch(getAllKhoa())
        .unwrap()
        .then((res: any) => {
          let khoaList = [];
          khoaList = res.items;
          setFaculties(khoaList);
        })
        .catch((err: any) => {
          console.error('Lỗi khi lấy danh sách khoa:', err);
        })
    }
  }, [isModalOpen, dispatch]);

  const handleSubmit = async (values: IRequestForm) => {
    if (!values.questions || values.questions.length < 1) {
      toast.error('Phải có ít nhất 1 câu hỏi');
      setActiveTab('2'); 
      return;
    }

    for (let i = 0; i < values.questions.length; i++) {
      const question = values.questions[i];
      // Only validate answers for multiple choice questions (type 1 or 2), not essay (type 3)
      if ((question.loaiCauHoi === 1 || question.loaiCauHoi === 2) && (!question.answers || question.answers.length < 2)) {
        toast.error(`Câu hỏi ${i + 1}: Câu hỏi trắc nghiệm phải có ít nhất 2 đáp án`);
        setActiveTab('2');
        return;
      }
    }

    if (!values.criterias || values.criterias.length < 1) {
      toast.error('Phải có ít nhất 1 tiêu chí đánh giá');
      setActiveTab('1');
      return;
    }

    try {
      const payload: ICreateRequest = {
        ...values,
        thoiGianBatDau: dayjs(values.thoiGianBatDau).format('YYYY-MM-DDTHH:mm:ss'),
        thoiGianKetThuc: dayjs(values.thoiGianKetThuc).format('YYYY-MM-DDTHH:mm:ss')
      };

      if (isEdit && request) {
        const updatePayload: Partial<IUpdateRequest> = {
          ...payload,
          id: request.id
        };
        await dispatch(updateRequest(updatePayload)).unwrap();
        toast.success('Cập nhật yêu cầu thành công');
      } else {
        await dispatch(createRequest(payload)).unwrap();
        toast.success('Tạo yêu cầu thành công');
      }

      setIsModalOpen(false);
      dispatch(resetRequestStatus());
      onSuccess?.();
    } catch (error: any) {
      console.error(error);
      toast.error(error?.message || 'Có lỗi xảy ra');
    }
  };

  const onFinishFailed = ({ errorFields }: any) => {
    const tab1Fields = ['maYeuCau', 'tenKhaoSatYeuCau', 'moTa', 'thoiGianBatDau', 'thoiGianKetThuc', 'idPhongBan', 'targets', 'criterias'];
    
    const hasErrorInTab1 = errorFields.some((field: any) => {
      const fieldName = field.name[0];
      return tab1Fields.includes(fieldName) || fieldName === 'targets' || fieldName === 'criterias';
    });

    if (hasErrorInTab1) {
      setActiveTab('1');
    } else {
      setActiveTab('2');
    }
    toast.error('Vui lòng kiểm tra lại thông tin nhập liệu');
  };

  const handleCancel = () => {
    setIsModalOpen(false);
    form.resetFields();
    dispatch(resetRequestStatus());
  };

  const renderGeneralInfoTab = () => (
    <>
      <Row gutter={16}>
        <Col span={12}>
          <Form.Item
            label="Mã yêu cầu"
            name="maYeuCau"
            rules={[{ required: true, message: 'Vui lòng nhập mã yêu cầu' }]}
          >
            <Input placeholder="Nhập mã yêu cầu" disabled={isViewMode} />
          </Form.Item>
        </Col>
        <Col span={12}>
          <Form.Item
            label="Tên khảo sát"
            name="tenKhaoSatYeuCau"
            rules={[{ required: true, message: 'Vui lòng nhập tên khảo sát' }]}
          >
            <Input placeholder="Nhập tên khảo sát" disabled={isViewMode} />
          </Form.Item>
        </Col>
      </Row>

      <Form.Item
        label="Mô tả"
        name="moTa"
        rules={[{ required: true, message: 'Vui lòng nhập mô tả' }]}
      >
        <TextArea rows={3} placeholder="Nhập mô tả khảo sát" disabled={isViewMode} />
      </Form.Item>

      {isViewMode && request?.lyDoTuChoi && (
        <Form.Item label="Lý do từ chối" name="lyDoTuChoi">
          <TextArea 
            rows={3} 
            disabled 
            style={{ 
              backgroundColor: '#fff1f0', 
              borderColor: '#ffa39e',
              color: '#cf1322'
            }} 
          />
        </Form.Item>
      )}

      <Row gutter={16}>
        <Col span={12}>
          <Form.Item
            label="Thời gian bắt đầu"
            name="thoiGianBatDau"
            rules={[{ required: true, message: 'Vui lòng chọn thời gian bắt đầu' }]}
          >
            <DatePicker
              showTime
              format="DD/MM/YYYY HH:mm"
              placeholder="Chọn thời gian bắt đầu"
              style={{ width: '100%' }}
              disabled={isViewMode}
            />
          </Form.Item>
        </Col>
        <Col span={12}>
          <Form.Item
            label="Thời gian kết thúc"
            name="thoiGianKetThuc"
            rules={[{ required: true, message: 'Vui lòng chọn thời gian kết thúc' }]}
          >
            <DatePicker
              showTime
              format="DD/MM/YYYY HH:mm"
              placeholder="Chọn thời gian kết thúc"
              style={{ width: '100%' }}
              disabled={isViewMode}
            />
          </Form.Item>
        </Col>
      </Row>

      <Form.Item
        label="Phòng ban"
        name="idPhongBan"
        rules={[{ required: true, message: 'Vui lòng chọn phòng ban' }]}
      >
        <Select placeholder="Chọn phòng ban" allowClear disabled={isViewMode}>
          {departments.map((dept: any) => (
            <Option key={dept.idPhongBan} value={dept.idPhongBan}>
              {dept.tenPhongBan}
            </Option>
          ))}
        </Select>
      </Form.Item>

      <Card title="Đối tượng tham gia" size="small" style={{ marginBottom: 16 }}>
        <Row gutter={16}>
          <Col span={8}>
            <Form.Item
              label="Loại đối tượng"
              name={['targets', 0, 'loaiDoiTuong']}
              rules={[{ required: true, message: 'Chọn loại đối tượng' }]}
            >
              <Select placeholder="Chọn loại đối tượng" disabled={isViewMode}>
                <Option value={surveyStatusConst.ALL}>{surveyStatusConst.getName(surveyStatusConst.ALL)}</Option>
                <Option value={surveyStatusConst.STUDENT}>{surveyStatusConst.getName(surveyStatusConst.STUDENT)}</Option>
                <Option value={surveyStatusConst.LECTURER}>{surveyStatusConst.getName(surveyStatusConst.LECTURER)}</Option>
              </Select>
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item noStyle shouldUpdate={(prevValues, currentValues) =>
              prevValues.targets?.[0]?.loaiDoiTuong !== currentValues.targets?.[0]?.loaiDoiTuong
            }>
              {({ getFieldValue }) => {
                const targetType = getFieldValue(['targets', 0, 'loaiDoiTuong']);
                return (
                  <Row gutter={16}>
                    {(targetType === surveyStatusConst.ALL || targetType === surveyStatusConst.STUDENT) && (
                      <Col span={12}>
                        <Form.Item
                          label="Khoa"
                          name={['targets', 0, 'idKhoa']}
                        >
                          <Select placeholder="Chọn khoa" allowClear disabled={isViewMode}>
                            {faculties.map((faculty: any) => (
                              <Option key={faculty.id} value={faculty.id}>
                                {faculty.tenKhoa}
                              </Option>
                            ))}
                          </Select>                         
                        </Form.Item>
                      </Col>
                    )}
                    {(targetType === surveyStatusConst.ALL || targetType === surveyStatusConst.LECTURER) && (
                      <Col span={12}>
                        <Form.Item
                          label="Phòng ban"
                          name={['targets', 0, 'idPhongBan']}
                        >
                          <Select placeholder="Chọn phòng ban" allowClear disabled={isViewMode}>
                            {departments.map((dept: any) => (
                              <Option key={dept.idPhongBan} value={dept.idPhongBan}>
                                {dept.tenPhongBan}
                              </Option>
                            ))}
                          </Select>
                        </Form.Item>
                      </Col>
                    )}
                  </Row>
                );
              }}
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item
              label="Mô tả"
              name={['targets', 0, 'moTa']}
            >
              <Input placeholder="Mô tả đối tượng" disabled={isViewMode}/>
            </Form.Item>
          </Col>
        </Row>
      </Card>

      <Card title="Tiêu chí đánh giá" size="small">
        <Form.List name="criterias">
          {(fields, { add, remove }) => (
            <>
              {fields.map(({ key, name, ...restField }, index) => (
                <Row key={key} gutter={16} align="middle" style={{ marginBottom: 12 }}>
                  <Col span={6}>
                    <Form.Item
                      {...restField}
                      label={index === 0 ? "Tên tiêu chí" : null}
                      name={[name, 'tenTieuChi']}
                      rules={[{ required: true, message: 'Nhập tên tiêu chí' }]}
                      style={{ marginBottom: 0 }}
                    >
                      <Input placeholder="Nhập tên tiêu chí" disabled={isViewMode} />
                    </Form.Item>
                  </Col>
                  <Col span={4}>
                    <Form.Item 
                      {...restField} 
                      label={index === 0 ? "Trọng số" : null} 
                      name={[name, 'weight']} 
                      style={{ marginBottom: 0 }}
                      rules={[
                        { required: true, message: 'Nhập trọng số' },
                        { type: 'number', min: 1, max: 10, message: 'Trọng số từ 1-10', transform: (value) => Number(value) }
                      ]}
                      tooltip="Trọng số càng lớn, mức độ đánh giá càng cao (1-10)"
                    >
                      <Input type="number" placeholder="1-10" disabled={isViewMode} min={1} max={10} />
                    </Form.Item>
                  </Col>
                  <Col span={6}>
                    <Form.Item {...restField} label={index === 0 ? "Từ khóa" : null} name={[name, 'keyword']} style={{ marginBottom: 0 }}>
                      <Input placeholder="keyword " disabled={isViewMode} />
                    </Form.Item>
                  </Col>
                  <Col span={7}>
                    <Form.Item {...restField} label={index === 0 ? "Mô tả" : null} name={[name, 'moTa']} style={{ marginBottom: 0 }}>
                      <Input placeholder="Mô tả chi tiết" disabled={isViewMode} />
                    </Form.Item>
                  </Col>
                  <Col span={1}>
                    {!isViewMode && (
                      <Button type="link" danger onClick={() => remove(name)} style={index === 0 ? { marginTop: 30 } : {}}>
                        Xóa
                      </Button>
                    )}
                  </Col>
                </Row>
              ))}
              {!isViewMode && (
                <Button type="dashed" onClick={() => add()} block style={{ marginTop: 8 }}>
                  + Thêm tiêu chí
                </Button>
              )}
            </>
          )}
        </Form.List>
      </Card>
    </>
  );

  const renderQuestionsTab = () => (
    <Card title="Danh sách câu hỏi" size="small">
      <Form.List name="questions">
        {(fields, { add, remove }) => (
          <>
            {fields.map(({ key, name, ...restField }) => (
              <Card 
                key={key} 
                size="small" 
                style={{ marginBottom: 16, backgroundColor: '#fafafa' }} 
                title={`Câu hỏi ${name + 1}`}
                extra={
                  !isViewMode && (
                    <Button type="link" danger onClick={() => remove(name)}>
                      Xóa câu hỏi này
                    </Button>
                  )
                }
              >
                <Row gutter={16}>
                  <Col span={8}>
                    <Form.Item
                      {...restField}
                      label="Mã câu hỏi"
                      name={[name, 'maCauHoi']}
                      rules={[{ required: true, message: 'Nhập mã câu hỏi' }]}
                    >
                      <Input placeholder="Mã câu hỏi" disabled={isViewMode} />
                    </Form.Item>
                  </Col>
                  <Col span={8}>
                    <Form.Item
                      {...restField}
                      label="Loại câu hỏi"
                      name={[name, 'loaiCauHoi']}
                      rules={[{ required: true, message: 'Chọn loại câu hỏi' }]}
                    >
                      <Select 
                        placeholder="Loại câu hỏi" 
                        disabled={isViewMode}
                        onChange={(value) => {
                          // Clear answers when switching to essay type
                          if (value === 3) {
                            form.setFieldValue(['questions', name, 'answers'], []);
                          }
                        }}
                      >
                        <Option value={1}>Trắc nghiệm</Option>
                        <Option value={2}>Chọn nhiều đáp án</Option>
                        <Option value={3}>Tự luận</Option>
                      </Select>
                    </Form.Item>
                  </Col>
                  {!isViewMode && (
                    <Col span={8}>
                      <Form.Item {...restField} label="Thứ tự hiển thị" name={[name, 'thuTu']} initialValue={name + 1}>
                        <Input type="number" placeholder="Thứ tự" disabled />
                      </Form.Item>
                    </Col>
                  )}
                </Row>
                <Form.Item
                  {...restField}
                  label="Nội dung câu hỏi"
                  name={[name, 'noiDung']}
                  rules={[{ required: true, message: 'Nhập nội dung câu hỏi' }]}
                >
                  <TextArea rows={2} placeholder="Nội dung câu hỏi" disabled={isViewMode} />
                </Form.Item>
                <Form.Item {...restField} label="Câu hỏi bắt buộc" name={[name, 'batBuoc']} valuePropName="checked">
                  <Checkbox disabled={isViewMode}>Bắt buộc trả lời</Checkbox>
                </Form.Item>

                {/* Only show answers section for multiple choice questions (type 1 or 2), not essay (type 3) */}
                <Form.Item noStyle shouldUpdate>
                  {({ getFieldValue }) => {
                    const questionType = getFieldValue(['questions', name, 'loaiCauHoi']);
                    
                    // Only show answers for type 1 (Trắc nghiệm) or type 2 (Chọn nhiều đáp án)
                    if (questionType === 3) {
                      return null; // Hide answers for essay questions
                    }

                    return (
                      <div style={{ marginTop: 8, paddingLeft: 16, borderLeft: '2px solid #1677ff' }}>
                        <Form.List name={[name, 'answers']}>
                          {(answerFields, { add: addAnswer, remove: removeAnswer }) => (
                            <>
                              {answerFields.map((answerField, index) => (
                          <Row key={answerField.key} gutter={16} align="middle" style={{ marginBottom: 8 }}>
                            <Col span={7}>
                              <Form.Item
                                {...answerField}
                                label={index === 0 ? "Nội dung đáp án" : null}
                                name={[answerField.name, 'noiDung']}
                                rules={[{ required: true, message: 'Nhập nội dung' }]}
                                style={{ marginBottom: 0 }}
                              >
                                <Input placeholder="Nội dung" disabled={isViewMode} />
                              </Form.Item>
                            </Col>
                            <Col span={5}>
                              <Form.Item {...answerField} label={index === 0 ? "Giá trị" : null} name={[answerField.name, 'value']} style={{ marginBottom: 0 }}>
                                <Input type="number" placeholder="Số" disabled={isViewMode} />
                              </Form.Item>
                            </Col>
                            {!isViewMode && (
                              <Col span={5}>
                                <Form.Item {...answerField} label={index === 0 ? "Thứ tự" : null} name={[answerField.name, 'thuTu']} style={{ marginBottom: 0 }}>
                                  <Input type="number" placeholder="Thứ tự" disabled value={index + 1} />
                                </Form.Item>
                              </Col>
                            )}
                            <Col span={5}>
                              <Form.Item {...answerField} label={index === 0 ? "Đáp án đúng" : null} name={[answerField.name, 'isCorrect']} valuePropName="checked" style={{ marginBottom: 0 }}>
                                <Checkbox disabled={isViewMode}>Đúng</Checkbox>
                              </Form.Item>
                            </Col>
                            <Col span={2}>
                              {!isViewMode && (
                                <Button type="link" danger onClick={() => removeAnswer(answerField.name)} style={index === 0 ? { marginTop: 30 } : {}}>
                                  X
                                </Button>
                              )}
                            </Col>
                          </Row>
                        ))}
                        {!isViewMode && (
                          <Button type="dashed" size="small" onClick={() => addAnswer({ thuTu: answerFields.length + 1 })} style={{ marginTop: 8 }}>
                            + Thêm đáp án
                          </Button>
                        )}
                              </>
                            )}
                          </Form.List>
                        </div>
                      );
                    }}
                  </Form.Item>
              </Card>
            ))}
            {!isViewMode && (
              <Button type="dashed" onClick={() => add()} block icon={<span style={{ fontSize: 16 }}>+</span>}>
                Thêm câu hỏi mới
              </Button>
            )}
          </>
        )}
      </Form.List>
    </Card>
  );

  const items: TabsProps['items'] = [
    {
      key: '1',
      label: 'Thông tin chung & Tiêu chí',
      children: renderGeneralInfoTab(),
      forceRender: true,
    },
    {
      key: '2',
      label: 'Câu hỏi khảo sát',
      children: renderQuestionsTab(),
      forceRender: true,
    },
  ];

  return (
    <Modal
      title={isViewMode ? 'Chi tiết yêu cầu khảo sát' : isEdit ? 'Cập nhật yêu cầu khảo sát' : 'Tạo yêu cầu khảo sát'}
      open={isModalOpen}
      onCancel={handleCancel}
      width={1200}
      footer={null}
      maskClosable={false}
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={handleSubmit}
        onFinishFailed={onFinishFailed}
        initialValues={{
          targets: [{}],
          questions: [{ answers: [{ thuTu: 1 }, { thuTu: 2 }] }],
          criterias: [{}]
        }}
      >
        <Tabs
          activeKey={activeTab}
          onChange={setActiveTab}
          items={items}
          type="card"
        />

        <div style={{ textAlign: 'right', marginTop: 16, borderTop: '1px solid #f0f0f0', paddingTop: 16 }}>
          <Space>
            <Button icon={<CloseOutlined />} onClick={handleCancel}>
              {isViewMode ? 'Đóng' : 'Hủy'}
            </Button>
            {!isViewMode && (
              <Button
                type="primary"
                icon={<SaveOutlined />}
                htmlType="submit"
                loading={requestState.$create.status === 'loading' || requestState.$update.status === 'loading'}
              >
                {isEdit ? 'Cập nhật' : 'Tạo mới'}
              </Button>
            )}
          </Space>
        </div>
      </Form>
    </Modal>
  );
};

export default CreateOrUpdateRequestModal;