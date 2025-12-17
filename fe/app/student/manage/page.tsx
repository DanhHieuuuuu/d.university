'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Modal, Tag } from 'antd';
import { PlusOutlined, SearchOutlined, SyncOutlined, EditOutlined, StopOutlined, EyeOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ReduxStatus } from '@redux/const';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { IColumn, IAction } from '@models/common/table.model';
import { IQueryStudent, IViewStudent } from '@models/student/student.model';
import { getListStudent, deleteStudent } from '@redux/feature/studentSlice';
import { toast } from 'react-toastify';
import dayjs from 'dayjs';
import AppTable from '@components/common/Table';
import StudentDialog from './(dialog)/create-or-update';

const StudentPage = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total } = useAppSelector((state) => state.studentState);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [selectedStudent, setSelectedStudent] = useState<IViewStudent | null>(null);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);

  const [isView, setIsView] = useState(false);

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryStudent>({
    total: total,
    initialQuery: {
      SkipCount: 0,
      MaxResultCount: 10,
      mssv: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getListStudent(newQuery));
    },
    triggerFirstLoad: true
  });

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ mssv: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  const columns: IColumn<IViewStudent>[] = [
    { key: 'idStudent', dataIndex: 'idStudent', title: 'ID', showOnConfig: false },
    { key: 'mssv', dataIndex: 'mssv', title: 'MSSV' },
    { key: 'hoTen', dataIndex: 'hoTen', title: 'Họ tên' },
    { key: 'soCccd', dataIndex: 'soCccs', title: 'CCCD' },
    {
      key: 'ngaySinh',
      dataIndex: 'ngaySinh',
      title: 'Ngày sinh',
      render: (value) => (value ? dayjs(value).format('DD-MM-YYYY') : '')
    },
    { key: 'noiSinh', dataIndex: 'noiSinh', title: 'Nơi sinh' },
    { key: 'email', dataIndex: 'email', title: 'Email' },
    { key: 'soDienThoai', dataIndex: 'soDienThoai', title: 'Số điện thoại' },
    {
      key: 'gioiTinh',
      dataIndex: 'gioiTinh',
      title: 'Giới tính',
      render: (value) => (value ? 'Nam' : 'Nữ')
    },
    { key: 'quocTich', dataIndex: 'quocTich', title: 'Quốc tịch' },
    { key: 'danToc', dataIndex: 'danToc', title: 'Dân tộc' },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
      render: (val: boolean) => (val ? <Tag color="green">Hoạt động</Tag> : <Tag color="red">Khóa</Tag>)
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Xem',
      tooltip: 'Xem thông tin sinh viên',
      icon: <EyeOutlined />,
      command: (record: IViewStudent) => {
        setSelectedStudent(record);
        setIsView(true);
        setIsModalOpen(true);
      }
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin sinh viên',
      icon: <EditOutlined />,
      command: (record: IViewStudent) => {
        setSelectedStudent(record);
        setIsView(false);
        setIsModalOpen(true);
      }
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <StopOutlined />,
      command: (record: IViewStudent) => {
        Modal.confirm({
          title: 'Xác nhận xóa',
          content: 'Bạn có chắc chắn muốn xóa sinh viên này?',
          okText: 'Xóa',
          okButtonProps: { danger: true },
          cancelText: 'Hủy',
          onOk: async () => {
            try {
              await dispatch(deleteStudent(record.idStudent)).unwrap();
              toast.success('Xóa sinh viên thành công');
              dispatch(getListStudent(query));
            } catch {
              toast.error('Xóa sinh viên thất bại');
            }
          }
        });
      }
    }
  ];

  const onClickAdd = () => {
    setSelectedStudent(null);
    setIsView(false);
    setIsModalOpen(true);
  };

  return (
    <Card
      title="Danh sách sinh viên"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item<IQueryStudent> label="MSSV:" name="mssv">
            <Input placeholder="Nhập MSSV" onChange={(e) => handleSearch(e)} />
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
        loading={status === ReduxStatus.LOADING}
        rowKey="idStudent"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />

      <StudentDialog
        open={isModalOpen}
        student={selectedStudent}
        isView={isView}
        onClose={() => {
          setIsModalOpen(false);
          setSelectedStudent(null);
          setIsView(false);
        }}
        onSuccess={() => {
          dispatch(getListStudent(query));
        }}
      />
    </Card>
  );
};

export default StudentPage;
