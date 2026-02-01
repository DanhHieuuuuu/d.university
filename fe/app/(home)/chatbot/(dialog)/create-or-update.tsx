import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { Form, FormProps, Input, Modal, Switch, Tooltip } from 'antd';
import { QuestionCircleOutlined } from '@ant-design/icons';

import { ICreateModelChatbot, IUpdateModelChatbot, IModelChatbot } from '@models/chatbot/modelChatbot.model';
import { ChatbotService } from '@services/chatbot.service';

type CreateModelModalProps = {
    isModalOpen: boolean;
    setIsModalOpen: (value: boolean) => void;
    refreshData: () => void;
    isUpdate: boolean;
    selectedModel?: IModelChatbot | null;
};

const CreateModelModal: React.FC<CreateModelModalProps> = (props) => {
    const [form] = Form.useForm<ICreateModelChatbot>();
    const [isSubmitting, setIsSubmitting] = useState<boolean>(false);

    const title = props.isUpdate ? 'Cập nhật Model Chatbot' : 'Thêm Model Chatbot mới';
    const okText = props.isUpdate ? 'Lưu lại' : 'Thêm mới';

    useEffect(() => {
        if (props.isModalOpen && props.isUpdate && props.selectedModel) {
            form.setFieldsValue({
                name: props.selectedModel.name || '',
                description: props.selectedModel.description || '',
                baseURL: props.selectedModel.baseURL || '',
                modelName: props.selectedModel.modelName || '',
                isLocal: props.selectedModel.isLocal || false,
                isSelected: props.selectedModel.isSelected || false
            });
        } else {
            form.resetFields();
            form.setFieldsValue({
                isLocal: false,
                isSelected: false
            });
        }
    }, [props.isModalOpen, props.isUpdate, props.selectedModel, form]);

    const onCloseModal = () => {
        form.resetFields();
        props.setIsModalOpen(false);
    };

    const handleSubmit: FormProps<ICreateModelChatbot>['onFinish'] = async (values) => {
        setIsSubmitting(true);
        try {
            if (props.isUpdate && props.selectedModel?.id) {
                const updateData: IUpdateModelChatbot = {
                    id: props.selectedModel.id,
                    ...values
                };
                await ChatbotService.updateModel(updateData);
                toast.success('Cập nhật model thành công');
            } else {
                await ChatbotService.createModel(values);
                toast.success('Tạo model thành công');
            }

            props.refreshData();
            onCloseModal();
        } catch (err: any) {
            toast.error(err?.message || (props.isUpdate ? 'Không thể cập nhật model' : 'Không thể tạo model'));
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <Modal
            title={title}
            className="app-modal"
            width={700}
            open={props.isModalOpen}
            onCancel={onCloseModal}
            onOk={() => form.submit()}
            confirmLoading={isSubmitting}
            okText={okText}
            cancelText="Hủy"
            maskClosable={false}
        >
            <Form form={form} layout="vertical" onFinish={handleSubmit}>
                <Form.Item<ICreateModelChatbot>
                    label="Tên Model"
                    name="name"
                    rules={[
                        { required: true, message: 'Vui lòng nhập tên model' },
                        { max: 200, message: 'Tên không được vượt quá 200 ký tự' }
                    ]}
                >
                    <Input placeholder="Nhập tên model (VD: GPT-4, Claude, Llama 3...)" />
                </Form.Item>

                <Form.Item<ICreateModelChatbot> label="Mô tả" name="description">
                    <Input.TextArea rows={3} placeholder="Nhập mô tả model (không bắt buộc)" />
                </Form.Item>

                <Form.Item<ICreateModelChatbot>
                    label={
                        <span>
                            Base URL{' '}
                            <Tooltip title="URL cơ sở của API (VD: https://api.openai.com/v1)">
                                <QuestionCircleOutlined style={{ color: '#999' }} />
                            </Tooltip>
                        </span>
                    }
                    name="baseURL"
                    rules={[{ required: true, message: 'Vui lòng nhập Base URL' }]}
                >
                    <Input placeholder="Nhập Base URL API" />
                </Form.Item>

                <Form.Item<ICreateModelChatbot>
                    label={
                        <span>
                            API Key{' '}
                            <Tooltip title="API Key để xác thực với provider (để trống nếu là model local)">
                                <QuestionCircleOutlined style={{ color: '#999' }} />
                            </Tooltip>
                        </span>
                    }
                    name="apiKey"
                >
                    <Input.Password placeholder="Nhập API Key (không bắt buộc)" />
                </Form.Item>

                <Form.Item<ICreateModelChatbot>
                    label={
                        <span>
                            Model Name{' '}
                            <Tooltip title="Tên model cụ thể (VD: gpt-4-turbo, claude-3-sonnet...)">
                                <QuestionCircleOutlined style={{ color: '#999' }} />
                            </Tooltip>
                        </span>
                    }
                    name="modelName"
                    rules={[{ required: true, message: 'Vui lòng nhập Model Name' }]}
                >
                    <Input placeholder="Nhập tên model cụ thể" />
                </Form.Item>

                <div className="grid grid-cols-2 gap-4">
                    <Form.Item<ICreateModelChatbot>
                        label={
                            <span>
                                Model Local{' '}
                                <Tooltip title="Đánh dấu nếu model chạy trên máy cục bộ">
                                    <QuestionCircleOutlined style={{ color: '#999' }} />
                                </Tooltip>
                            </span>
                        }
                        name="isLocal"
                        valuePropName="checked"
                    >
                        <Switch checkedChildren="Local" unCheckedChildren="Cloud" />
                    </Form.Item>

                    <Form.Item<ICreateModelChatbot>
                        label={
                            <span>
                                Đang sử dụng{' '}
                                <Tooltip title="Chọn model này làm mặc định cho chatbot">
                                    <QuestionCircleOutlined style={{ color: '#999' }} />
                                </Tooltip>
                            </span>
                        }
                        name="isSelected"
                        valuePropName="checked"
                    >
                        <Switch checkedChildren="Có" unCheckedChildren="Không" />
                    </Form.Item>
                </div>
            </Form>
        </Modal>
    );
};

export default CreateModelModal;
