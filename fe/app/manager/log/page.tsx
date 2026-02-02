'use client';

import { useEffect, useState } from 'react';
import { Breadcrumb, Button, Card, Form, Input, Modal, Tag, Space } from 'antd';
import {
  DownloadOutlined,
  SearchOutlined,
  SyncOutlined,
  FileTextOutlined
} from '@ant-design/icons';
import { IColumn, IAction } from '@models/common/table.model';
import AppTable from '@components/common/Table';
import { FileService } from '@services/file.service';
import { toast } from 'react-toastify';

const breadcrumbItems = [
  {
    title: 'Tổng quan',
    href: '/manager'
  },
  {
    title: 'Quản lý log',
    href: '/manager/log'
  },
  {
    title: 'Danh sách'
  }
];

interface ILogFile {
  key: string;
  fileName: string;
  size: number;
  folder: string;
}

const Page = () => {
  const [form] = Form.useForm();
  const [data, setData] = useState<ILogFile[]>([]);
  const [filteredData, setFilteredData] = useState<ILogFile[]>([]);
  const [loading, setLoading] = useState(false);
  const [searchText, setSearchText] = useState('');

  const fetchLogFiles = async () => {
    try {
      setLoading(true);
      
      // Fetch auth-api and core-api
      const [authLogs, coreLogs] = await Promise.all([
        FileService.listFiles('logs/auth-api/'),
        FileService.listFiles('logs/core-api/')
      ]);

      const authData = Array.isArray(authLogs) ? authLogs : [];
      const coreData = Array.isArray(coreLogs) ? coreLogs : [];


      // Combine and map files
      const allFiles = [
        ...authData.map((file: any) => ({ ...file, folder: 'logs/auth-api' })),
        ...coreData.map((file: any) => ({ ...file, folder: 'logs/core-api' }))
      ];

      const logFiles: ILogFile[] = allFiles.map((file: any, index: number) => {

        let cleanFileName = file.fileName || '';
        if (cleanFileName.includes('/logs/')) {
          cleanFileName = cleanFileName.substring(cleanFileName.indexOf('/logs/') + 1);
        }

        return {
          key: `${cleanFileName}-${index}`,
          fileName: cleanFileName,
          size: file.size || 0,
          folder: file.folder
        };
      });


      logFiles.sort((a, b) => a.fileName.localeCompare(b.fileName));

      setData(logFiles);
      setFilteredData(logFiles);
    } catch (error) {
      console.error('Error fetching log files:', error);
      toast.error('Không thể tải danh sách file log');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchLogFiles();
  }, []);

  const handleDownload = async (fileName: string) => {
    try {
      const blob = await FileService.downloadFile(fileName);
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = fileName.split('/').pop() || 'log.txt';
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      window.URL.revokeObjectURL(url);
      toast.success('Tải file thành công');
    } catch (error) {
      toast.error('Không thể tải file');
    }
  };

  const handleSearch = (values: { fileName: string }) => {
    const searchValue = values.fileName?.trim().toLowerCase() || '';
    setSearchText(searchValue);
    
    if (!searchValue) {
      setFilteredData(data);
    } else {
      const filtered = data.filter(file => 
        file.fileName.toLowerCase().includes(searchValue)
      );
      setFilteredData(filtered);
    }
  };

  const handleReset = () => {
    form.resetFields();
    setSearchText('');
    setFilteredData(data);
  };

  const formatFileSize = (bytes: number): string => {
    if (bytes === 0) return '0 B';
    const k = 1024;
    const sizes = ['B', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return Math.round((bytes / Math.pow(k, i)) * 100) / 100 + ' ' + sizes[i];
  };

  const columns: IColumn<ILogFile>[] = [
    {
      key: 'fileName',
      dataIndex: 'fileName',
      title: 'Tên file',
      width: 350,
      render: (fileName: string) => {
        const displayName = fileName.split('/').pop() || fileName;
        return (
          <Space>
            <FileTextOutlined style={{ color: '#1890ff' }} />
            <span>{displayName}</span>
          </Space>
        );
      }
    },
    {
      key: 'folder',
      dataIndex: 'folder',
      title: 'API',
      width: 120,
      render: (folder: string) => {
        const api = folder.includes('auth-api') ? 'Auth API' : 'Core API';
        const color = folder.includes('auth-api') ? 'purple' : 'green';
        return <Tag color={color}>{api}</Tag>;
      }
    }
  ];

  const actionList: IAction[] = [
    {
      label: 'Tải về',
      icon: <DownloadOutlined />,
      command: (record: ILogFile) => handleDownload(record.fileName)
    }
  ];

  return (
    <div className="flex h-full flex-col gap-4">
      <Breadcrumb separator=">" items={breadcrumbItems} />
      <Card
        title="Danh sách file log"
        variant="borderless"
        className="h-full"
        extra={
          <Button 
          type="default" 
          icon={<SyncOutlined />} 
          onClick={fetchLogFiles}
          loading={loading}
          >
            Làm mới
          </Button>
        }
        >
        <Form form={form} layout="horizontal" onFinish={handleSearch} initialValues={{ fileName: '' }}>
          <div className="grid grid-cols-2">
            <Form.Item label="Tên file:" name="fileName">
              <Input className="h-9 !w-full" placeholder="Nhập tên file log" allowClear />
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
                onClick={handleReset}
                >
                Đặt lại
              </Button>
            </div>
          </Form.Item>
        </Form>

        <AppTable
          loading={loading}
          rowKey="key"
          columns={columns}
          dataSource={filteredData}
          listActions={actionList}
          pagination={{
            position: ['bottomRight'],
            pageSize: 20,
            showSizeChanger: true,
            showTotal: (total) => `Tổng ${total} file`
          }}
          scroll={{ x: 'max-content', y: 'calc(100vh - 450px)' }}
        />
      </Card>
    </div>
  );
};

export default Page;
