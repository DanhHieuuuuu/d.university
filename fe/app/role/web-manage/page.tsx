'use client';

import { useEffect, useRef, useState } from 'react';
import { toast } from 'react-toastify';
import { Breadcrumb, Button, Card, Form, Input, Modal, Popover, Space, Table, TableProps } from 'antd';
import { DeleteOutlined, EllipsisOutlined, PlusOutlined, SearchOutlined, SyncOutlined } from '@ant-design/icons';
import { ICreateRole, IQueryPagingRoles, IRole } from '@models/role';
import { RoleService } from '@services/role.service';

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
  const [modalForm] = Form.useForm<ICreateRole>();

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);

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

  const fetchRoles = async () => {
    setIsLoading(true);
    try {
      const roles = await RoleService.getAllRoles();
      // Transform string array to IRole array
      const transformedData: IRole[] = roles.map((roleName: string, index: number) => ({
        id: String(index + 1),
        name: roleName,
        createdAt: new Date().toLocaleDateString('vi-VN')
      }));
      setData(transformedData);
      setTotalItems(transformedData.length);
    } catch (error) {
      toast.error('Không thể lấy danh sách role');
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchRoles();
  }, []);

  const onClickAdd = () => {
    setIsModalOpen(true);
  };

  const handleModalCancel = () => {
    setIsModalOpen(false);
    modalForm.resetFields();
  };

  const handleModalSubmit = async (values: ICreateRole) => {
    setIsSubmitting(true);
    try {
      await RoleService.createRole(values);
      toast.success('Tạo role mới thành công');
      setIsModalOpen(false);
      modalForm.resetFields();
      fetchRoles();
    } catch (error) {
      toast.error('Không thể tạo role mới');
    } finally {
      setIsSubmitting(false);
    }
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
                  fetchRoles();
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

      <Modal
        title="Thêm nhóm quyền mới"
        open={isModalOpen}
        onCancel={handleModalCancel}
        onOk={() => modalForm.submit()}
        confirmLoading={isSubmitting}
        okText="Thêm mới"
        cancelText="Hủy"
        width={700}
      >
        <Form form={modalForm} layout="vertical" onFinish={handleModalSubmit} initialValues={{ rolePermissions: [] }}>
          <Form.Item<ICreateRole>
            label="Tên nhóm quyền"
            name="name"
            rules={[
              { required: true, message: 'Vui lòng nhập tên nhóm quyền' },
              { max: 100, message: 'Tên không được vượt quá 100 ký tự' }
            ]}
          >
            <Input placeholder="Nhập tên nhóm quyền" />
          </Form.Item>

          <Form.Item<ICreateRole> label="Mô tả" name="description">
            <Input.TextArea rows={3} placeholder="Nhập mô tả (không bắt buộc)" />
          </Form.Item>

          <Form.Item label="Quyền (Permissions)">
            <Form.List name="rolePermissions">
              {(fields, { add, remove }) => (
                <>
                  {fields.map(({ key, name, ...restField }) => (
                    <Space key={key} style={{ display: 'flex', marginBottom: 8 }} align="baseline">
                      <Form.Item
                        {...restField}
                        name={[name, 'permissonKey']}
                        rules={[{ required: true, message: 'Nhập permission key' }]}
                        style={{ marginBottom: 0, width: 250 }}
                      >
                        <Input placeholder="Permission Key" />
                      </Form.Item>
                      <Form.Item
                        {...restField}
                        name={[name, 'permissionName']}
                        rules={[{ required: true, message: 'Nhập permission name' }]}
                        style={{ marginBottom: 0, width: 250 }}
                      >
                        <Input placeholder="Permission Name" />
                      </Form.Item>
                      <Button type="text" danger icon={<DeleteOutlined />} onClick={() => remove(name)} />
                    </Space>
                  ))}
                  <Button type="dashed" onClick={() => add()} block icon={<PlusOutlined />}>
                    Thêm quyền
                  </Button>
                </>
              )}
            </Form.List>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default Page;
