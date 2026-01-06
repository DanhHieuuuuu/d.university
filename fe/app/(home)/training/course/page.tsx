'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Modal, message } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  ExclamationCircleOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getAllMonHoc, deleteMonHoc } from '@redux/feature/dao-tao/monHocThunk';
import { setSelectedIdMonHoc } from '@redux/feature/dao-tao/daotaoSlice';

import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryMonHoc, IViewMonHoc } from '@models/dao-tao/monHoc.model';
import CourseModal from './(dialog)/create-or-update';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.daotaoState.monHoc.$list);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [selectedId, setSelectedId] = useState<number>(0);

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryMonHoc>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllMonHoc(newQuery));
    },
    triggerFirstLoad: true
  });

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickUpdate = (id: number) => {
    setSelectedId(id);
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

  const onClickView = (id: number) => {
    setSelectedId(id);
    setIsModalView(true);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const refreshData = () => {
    dispatch(getAllMonHoc(query));
  };

  const handleDelete = (id: number, tenMonHoc: string) => {
    Modal.confirm({
      title: 'Xác nhận xóa',
      icon: <ExclamationCircleOutlined />,
      content: `Bạn có chắc chắn muốn xóa môn học "${tenMonHoc}"?`,
      okText: 'Xóa',
      okType: 'danger',
      cancelText: 'Hủy',
      onOk: async () => {
        try {
          await dispatch(deleteMonHoc(id)).unwrap();
          message.success('Xóa môn học thành công!');
          refreshData();
        } catch (error: any) {
          message.error(error?.message || 'Xóa môn học thất bại!');
        }
      }
    });
  };

  const columns: IColumn<IViewMonHoc>[] = [
    {
      key: 'Id',
      dataIndex: 'id',
      title: 'ID',
      showOnConfig: false
    },
    {
      key: 'maMonHoc',
      dataIndex: 'maMonHoc',
      title: 'Mã môn học'
    },
    {
      key: 'tenMonHoc',
      dataIndex: 'tenMonHoc',
      title: 'Tên môn học'
    },
    {
      key: 'soTinChi',
      dataIndex: 'soTinChi',
      title: 'Số tín chỉ'
    },
    {
      key: 'soTietLyThuyet',
      dataIndex: 'soTietLyThuyet',
      title: 'Số tiết lý thuyết'
    },
    {
      key: 'soTietThucHanh',
      dataIndex: 'soTietThucHanh',
      title: 'Số tiết thực hành'
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
      render: (value: boolean) => (
        <span className={value ? 'text-green-600' : 'text-red-600'}>{value ? 'Hoạt động' : 'Ngừng'}</span>
      )
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      tooltip: 'Xem thông tin môn học',
      icon: <EyeOutlined />,
      command: (record: IViewMonHoc) => {
        dispatch(setSelectedIdMonHoc(record.id));
        onClickView(record.id);
      }
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin môn học',
      icon: <EditOutlined />,
      command: (record: IViewMonHoc) => {
        dispatch(setSelectedIdMonHoc(record.id));
        onClickUpdate(record.id);
      }
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IViewMonHoc) => {
        handleDelete(record.id, record.tenMonHoc);
      }
    }
  ];

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  return (
    <Card
      title="Danh sách môn học"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item<IQueryMonHoc> label="Tên môn học:" name="Keyword">
            <Input onChange={(e) => handleSearch(e)} />
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
                resetFilter();
              }}
            >
              Tải lại
            </Button>
          </div>
        </Form.Item>
      </Form>

      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />

      <CourseModal
        isModalOpen={isModalOpen}
        isUpdate={isUpdate}
        isView={isView}
        setIsModalOpen={setIsModalOpen}
        refreshData={refreshData}
      />
    </Card>
  );
};

export default Page;
