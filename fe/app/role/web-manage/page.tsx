'use client';

import { useEffect, useRef, useState } from 'react';
import { toast } from 'react-toastify';
import { Breadcrumb, Button, Card, Form, Input, Popover, Space, Table, TableProps } from 'antd';
import { EllipsisOutlined, PlusOutlined, SearchOutlined, SyncOutlined } from '@ant-design/icons';
import { IQueryPagingRoles, IRole } from '@models/role';

const breadcrumbItems = [
  {
    title: 'Tổng quan',
    href: '/role'
  },
  {
    title: 'Phân quyền website',
    href: '/role/web-manage'
  },
  {
    title: 'Danh sách'
  }
];

const Page = () => {
  const [form] = Form.useForm();

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const [totalItems, setTotalItems] = useState<number>(0);
  const [data, setData] = useState<IRole[]>([]);
  const [selected, setSelected] = useState<IRole>();

  const columns: TableProps<IRole>['columns'] = [
    {
      title: 'ID',
      dataIndex: 'id',
      key: 'id'
    },
    {
      title: 'Tên nhóm quyền',
      dataIndex: 'name',
      key: 'name'
    },
    {
      title: 'Ngày tạo',
      dataIndex: 'createdAt',
      key: 'createdAt'
    },
    {
      key: 'action',
      render: (_, record) => (
        <Popover trigger="click" arrow={false} placement="bottomRight">
          <Button type="text" icon={<EllipsisOutlined />}></Button>
        </Popover>
      )
    }
  ];

  // style for grid layout (filters)
  const containerRef = useRef(null);
  const [columnCount, setColumnCount] = useState(1);

  useEffect(() => {
    const observer = new ResizeObserver((entries) => {
      const entry = entries[0];
      if (entry) {
        const width = entry.contentRect.width;
        const cols = Math.max(1, Math.floor(width / 200));
        setColumnCount(cols);
      }
    });

    if (containerRef.current) observer.observe(containerRef.current);

    return () => observer.disconnect();
  }, []);

  const onClickAdd = () => {
    toast.info('Thêm mới');
  };

  const onSearch = (values: IQueryPagingRoles) => {
    // onFilterChange({ Keyword: values?.keyword || '' });
    toast.info('Từ khóa: ' + values?.Keyword);
  };

  return (
    <div className="flex h-full flex-col gap-4">
      <Breadcrumb separator=">" items={breadcrumbItems} />
      <Card
        title="Danh sách nhóm quyền Core"
        variant="borderless"
        className="h-full"
        extra={
          <Button type="primary" style={{ fontWeight: 500 }} icon={<PlusOutlined />} onClick={onClickAdd}>
            Thêm mới
          </Button>
        }
      >
        <Form layout="vertical" form={form} onFinish={onSearch}>
          <div
            ref={containerRef}
            style={{
              display: 'grid',
              gridTemplateColumns: `repeat(${columnCount}, minmax(0, 1fr))`,
              gap: '0 24px'
            }}
          >
            <Form.Item<IQueryPagingRoles> label="Từ khóa" name="Keyword">
              <Input className="h-9 !w-full" placeholder="Nhập thông tin" allowClear />
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

        <Table
          rowKey="id"
          bordered
          dataSource={data}
          columns={columns}
          size="small"
          loading={isLoading}
          pagination={{ position: ['bottomRight'] }}
        />
      </Card>
    </div>
  );
};

export default Page;
