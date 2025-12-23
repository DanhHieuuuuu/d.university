'use client';
import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Form, Input } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getAllMonHoc } from '@redux/feature/dao-tao/monHocThunk';
import { getAllMonTienQuyet } from '@redux/feature/dao-tao/monTienQuyetThunk';
import { setSelectedIdMonTienQuyet } from '@redux/feature/dao-tao/daotaoSlice';

import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryMonTienQuyet, IViewMonTienQuyet } from '@models/dao-tao/monTienQuyet.model';
import PrerequisiteCourseModal from './(dialog)/create-or-update';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.daotaoState.monTienQuyet.$list);
  const listMonHoc = useAppSelector((state) => state.daotaoState.listMonHoc);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [selectedId, setSelectedId] = useState<number>(0);

  // Load list MonHoc for display
  useEffect(() => {
    dispatch(getAllMonHoc({ PageIndex: 1, PageSize: 100 }));
  }, [dispatch]);

  const getMonHocName = (monHocId: number) => {
    const monHoc = listMonHoc.find((m) => m.id === monHocId);
    return monHoc ? monHoc.tenMonHoc : monHocId;
  };

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryMonTienQuyet>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllMonTienQuyet(newQuery));
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
    dispatch(getAllMonTienQuyet(query));
  };

  const columns: IColumn<IViewMonTienQuyet>[] = [
    {
      key: 'Id',
      dataIndex: 'id',
      title: 'ID',
      showOnConfig: false
    },
    {
      key: 'monHocId',
      dataIndex: 'monHocId',
      title: 'Môn học',
      render: (value: number) => getMonHocName(value)
    },
    {
      key: 'monTienQuyetId',
      dataIndex: 'monTienQuyetId',
      title: 'Môn tiên quyết',
      render: (value: number) => getMonHocName(value)
    },
    {
      key: 'loaiDieuKien',
      dataIndex: 'loaiDieuKien',
      title: 'Loại điều kiện'
    },
    {
      key: 'ghiChu',
      dataIndex: 'ghiChu',
      title: 'Ghi chú'
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      tooltip: 'Xem thông tin môn tiên quyết',
      icon: <EyeOutlined />,
      command: (record: IViewMonTienQuyet) => {
        dispatch(setSelectedIdMonTienQuyet(record.id));
        onClickView(record.id);
      }
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin môn tiên quyết',
      icon: <EditOutlined />,
      command: (record: IViewMonTienQuyet) => {
        dispatch(setSelectedIdMonTienQuyet(record.id));
        onClickUpdate(record.id);
      }
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IViewMonTienQuyet) => {
        dispatch(setSelectedIdMonTienQuyet(record.id));
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
      title="Danh sách môn tiên quyết"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item<IQueryMonTienQuyet> label="Tìm kiếm:" name="Keyword">
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

      <PrerequisiteCourseModal
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
