'use client';

import { useEffect, useState } from 'react';
import { Breadcrumb, Button, Card, Form, Input, Modal, Tag } from 'antd';
import {
    DeleteOutlined,
    EditOutlined,
    PlusOutlined,
    SearchOutlined,
    SyncOutlined,
    CheckCircleOutlined,
    CloseCircleOutlined,
    CloudOutlined,
    DesktopOutlined
} from '@ant-design/icons';
import { IAction, IColumn } from '@models/common/table.model';
import AppTable from '@components/common/Table';
import { IModelChatbot, IQueryModelChatbot } from '@models/chatbot/modelChatbot.model';
import { ChatbotService } from '@services/chatbot.service';
import { toast } from 'react-toastify';
import CreateModelModal from './(dialog)/create-or-update';

const breadcrumbItems = [
    {
        title: 'Trang chủ',
        href: '/'
    },
    {
        title: 'Quản lý Model Chatbot',
        href: '/chatbot'
    },
    {
        title: 'Danh sách'
    }
];

const Page = () => {
    const [form] = Form.useForm();
    const [data, setData] = useState<IModelChatbot[]>([]);
    const [filteredData, setFilteredData] = useState<IModelChatbot[]>([]);
    const [loading, setLoading] = useState(false);
    const [selectedModel, setSelectedModel] = useState<IModelChatbot | null>(null);
    const [searchName, setSearchName] = useState('');

    const [modalState, setModalState] = useState<{ open: boolean; isUpdate: boolean }>({
        open: false,
        isUpdate: false
    });

    const fetchModels = async () => {
        try {
            setLoading(true);
            const response = await ChatbotService.getModelList();
            setData(response || []);
            setFilteredData(response || []);
        } catch (error) {
            toast.error('Không thể tải danh sách model');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchModels();
    }, []);

    // Filter data when search changes
    useEffect(() => {
        if (searchName.trim()) {
            const filtered = data.filter((item) =>
                item.name?.toLowerCase().includes(searchName.toLowerCase())
            );
            setFilteredData(filtered);
        } else {
            setFilteredData(data);
        }
    }, [searchName, data]);

    const onSearch = (values: IQueryModelChatbot) => {
        setSearchName(values.Name?.trim() || '');
    };

    const onClickAdd = () => {
        setSelectedModel(null);
        setModalState({ open: true, isUpdate: false });
    };

    const onClickUpdate = (model: IModelChatbot) => {
        setSelectedModel(model);
        setModalState({ open: true, isUpdate: true });
    };

    const onClickDelete = (model: IModelChatbot) => {
        Modal.confirm({
            title: 'Xác nhận xóa model',
            content: `Bạn có chắc muốn xóa model "${model.name}"?`,
            centered: true,
            okText: 'Xóa',
            okButtonProps: { danger: true },
            cancelText: 'Hủy',
            onOk: () => {
                handleDeleteModel(model.id!);
            }
        });
    };

    const handleDeleteModel = async (modelId: number) => {
        try {
            await ChatbotService.deleteModel(modelId);
            toast.success('Xóa model thành công');
            await fetchModels();
        } catch (error) {
            toast.error('Không thể xóa model');
        }
    };

    const columns: IColumn<IModelChatbot>[] = [
        {
            key: 'name',
            dataIndex: 'name',
            title: 'Tên Model',
            width: 200
        },
        {
            key: 'description',
            dataIndex: 'description',
            title: 'Mô tả',
            width: 250,
            render: (text: string) => text || '-'
        },
        {
            key: 'baseURL',
            dataIndex: 'baseURL',
            title: 'Base URL',
            width: 250,
            render: (text: string) => text || '-'
        },
        {
            key: 'modelName',
            dataIndex: 'modelName',
            title: 'Model Name',
            width: 200,
            render: (text: string) => text || '-'
        },
        {
            key: 'isLocal',
            dataIndex: 'isLocal',
            title: 'Loại',
            width: 120,
            render: (isLocal: boolean) =>
                isLocal ? (
                    <Tag icon={<DesktopOutlined />} color="blue">
                        Local
                    </Tag>
                ) : (
                    <Tag icon={<CloudOutlined />} color="purple">
                        Cloud
                    </Tag>
                )
        },
        {
            key: 'isSelected',
            dataIndex: 'isSelected',
            title: 'Đang sử dụng',
            width: 120,
            render: (isSelected: boolean) =>
                isSelected ? (
                    <Tag icon={<CheckCircleOutlined />} color="success">
                        Đang dùng
                    </Tag>
                ) : (
                    <Tag icon={<CloseCircleOutlined />} color="default">
                        Không
                    </Tag>
                )
        }
    ];

    const actions: IAction[] = [
        {
            label: 'Cập nhật',
            tooltip: 'Cập nhật',
            icon: <EditOutlined />,
            command: (record: IModelChatbot) => onClickUpdate(record)
        },
        {
            label: 'Xóa',
            color: 'red',
            icon: <DeleteOutlined />,
            command: (record: IModelChatbot) => onClickDelete(record)
        }
    ];

    return (
        <div className="flex h-full flex-col gap-4">
            <Breadcrumb separator=">" items={breadcrumbItems} />
            <Card
                title="Danh sách Model Chatbot"
                variant="borderless"
                className="h-full"
                extra={
                    <Button type="primary" style={{ fontWeight: 500 }} icon={<PlusOutlined />} onClick={onClickAdd}>
                        Thêm mới
                    </Button>
                }
            >
                <Form form={form} layout="horizontal" onFinish={onSearch} initialValues={{ Name: '' }}>
                    <div className="grid grid-cols-2">
                        <Form.Item<IQueryModelChatbot> label="Tên model:" name="Name">
                            <Input className="h-9 !w-full" placeholder="Nhập tên model" allowClear />
                        </Form.Item>
                    </div>
                    <Form.Item>
                        <div className="flex flex-row justify-center space-x-2">
                            <Button type="primary" htmlType="submit" icon={<SearchOutlined />}>
                                Tìm kiếm
                            </Button>
                            <Button
                                color="default"
                                variant="filled"
                                icon={<SyncOutlined />}
                                onClick={() => {
                                    form.resetFields();
                                    setSearchName('');
                                    fetchModels();
                                }}
                            >
                                Tải lại
                            </Button>
                        </div>
                    </Form.Item>
                </Form>

                <AppTable
                    loading={loading}
                    rowKey="id"
                    columns={columns}
                    dataSource={filteredData}
                    listActions={actions}
                    pagination={{ position: ['bottomRight'], pageSize: 10 }}
                />
            </Card>

            <CreateModelModal
                isModalOpen={modalState.open}
                isUpdate={modalState.isUpdate}
                setIsModalOpen={(open: boolean) => setModalState((prev) => ({ ...prev, open }))}
                refreshData={fetchModels}
                selectedModel={selectedModel}
            />
        </div>
    );
};

export default Page;
