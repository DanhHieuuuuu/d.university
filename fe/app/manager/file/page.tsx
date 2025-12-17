'use client';

import { useEffect, useState } from 'react';
import { Breadcrumb, Button, Card, Form, Input, Modal, Image } from 'antd';
import {
  DeleteOutlined,
  EditOutlined,
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EyeOutlined
} from '@ant-design/icons';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import AppTable from '@components/common/Table';
import { IQueryFile, IFile } from '@models/file/file.model';
import { FileService } from '@services/file.service';
import { toast } from 'react-toastify';
import CreateFileModal from './(dialog)/create-or-update';

const breadcrumbItems = [
  {
    title: 'Tổng quan',
    href: '/manager'
  },
  {
    title: 'Quản lý file',
    href: '/manager/file'
  },
  {
    title: 'Danh sách'
  }
];

const Page = () => {
  const [form] = Form.useForm();
  const [data, setData] = useState<IFile[]>([]);
  const [loading, setLoading] = useState(false);
  const [totalItem, setTotalItem] = useState(0);
  const [selectedFile, setSelectedFile] = useState<IFile | null>(null);

  const [modalState, setModalState] = useState<{ open: boolean; isUpdate: boolean }>({
    open: false,
    isUpdate: false
  });

  const [previewImage, setPreviewImage] = useState<string>('');

  const getImageUrl = (fileName: string) => {
    const baseUrl = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5268/api';
    return `${baseUrl}/s3-test/download?fileName=${encodeURIComponent(fileName)}`;
  };

  const { query, pagination, onFilterChange } = usePaginationWithFilter({
    total: totalItem,
    initialQuery: {
      SkipCount: 0,
      MaxResultCount: 10,
      Name: ''
    },
    onQueryChange: async (newQuery) => {
      await fetchFiles(newQuery);
    },
    triggerFirstLoad: true
  });

  const fetchFiles = async (queryParams: IQueryFile) => {
    try {
      setLoading(true);
      const response = await FileService.findPaging(queryParams);
      setData(response.data.items || []);
      setTotalItem(response.data.totalItem || 0);
    } catch (error) {
      toast.error('Không thể tải danh sách file');
    } finally {
      setLoading(false);
    }
  };

  // Sync form values với query hiện tại
  useEffect(() => {
    form.setFieldsValue({
      Name: query.Name || ''
    });
  }, [query.Name, form]);

  const onSearch = (values: IQueryFile) => {
    // Reset về trang đầu tiên khi search và trim name
    const searchQuery = {
      ...values,
      Name: values.Name?.trim() || '',
      SkipCount: 0
    };
    onFilterChange(searchQuery);
  };

  const onClickAdd = () => {
    setSelectedFile(null);
    setModalState({ open: true, isUpdate: false });
  };

  const onClickUpdate = (file: IFile) => {
    setSelectedFile(file);
    setModalState({ open: true, isUpdate: true });
  };

  const onClickPreview = (file: IFile) => {
    if (file.link) {
      const imageUrl = getImageUrl(file.link);
      setPreviewImage(imageUrl);
    } else {
      toast.warning('File không có link để preview');
    }
  };

  const onClickDelete = (file: IFile) => {
    Modal.confirm({
      title: 'Xác nhận xóa file',
      content: `Bạn có chắc muốn xóa file "${file.name}"?`,
      centered: true,
      okText: 'Xóa',
      okButtonProps: { danger: true },
      cancelText: 'Hủy',
      onOk: () => {
        handleDeleteFile(file.id!);
      }
    });
  };

  const handleDeleteFile = async (fileId: number) => {
    try {
      await FileService.deleteFile(fileId);
      toast.success('Xóa file thành công');
      await fetchFiles(query);
    } catch (error) {
      toast.error('Không thể xóa file');
    }
  };

  const columns: IColumn<IFile>[] = [
    {
      key: 'id',
      dataIndex: 'id',
      title: 'ID',
      width: 80
    },
    {
      key: 'name',
      dataIndex: 'name',
      title: 'Tên file'
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: 'Mô tả'
    },
    {
      key: 'applicationField',
      dataIndex: 'applicationField',
      title: 'Lĩnh vực ứng dụng'
    },
    {
      key: 'link',
      dataIndex: 'link',
      title: 'Link',
      render: (link: string) => link || '-'
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Preview',
      tooltip: 'Xem ảnh',
      icon: <EyeOutlined />,
      command: (record: IFile) => onClickPreview(record)
    },
    {
      label: 'Cập nhật',
      tooltip: 'Cập nhật',
      icon: <EditOutlined />,
      command: (record: IFile) => onClickUpdate(record)
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IFile) => onClickDelete(record)
    }
  ];

  return (
    <div className="flex h-full flex-col gap-4">
      <Breadcrumb separator=">" items={breadcrumbItems} />
      <Card
        title="Danh sách file"
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
            <Form.Item<IQueryFile> label="Tên file:" name="Name">
              <Input className="h-9 !w-full" placeholder="Nhập tên file" allowClear />
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
                  form.submit();
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
          dataSource={data}
          listActions={actions}
          pagination={{ position: ['bottomRight'], ...pagination }}
        />
      </Card>

      <CreateFileModal
        isModalOpen={modalState.open}
        isUpdate={modalState.isUpdate}
        setIsModalOpen={(open) => setModalState((prev) => ({ ...prev, open }))}
        refreshData={() => fetchFiles(query)}
        selectedFile={selectedFile}
      />

      <div style={{ display: 'none' }}>
        <Image
          src={previewImage}
          alt="File preview"
          preview={{
            visible: !!previewImage,
            src: previewImage,
            onVisibleChange: (visible) => {
              if (!visible) {
                setPreviewImage('');
              }
            }
          }}
        />
      </div>
    </div>
  );
};

export default Page;
