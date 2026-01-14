'use client';

import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  LockOutlined,
  UnlockOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';

import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { getListQuyetDinh } from '@redux/feature/hrm/quyetdinh/quyetdinhThunk';
import { formatDateTimeView } from '@utils/index';
import { IQueryQuyetDinh, IViewQuyetDinh } from '@models/nhansu/quyetdinh.model';
import { ETableColumnType } from '@/constants/e-table.consts';
import { NsQuyetDinhTypeConst } from '@/constants/core/hrm/quyet-dinh-type.const';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data, status, total: totalItem } = useAppSelector((state) => state.quyetdinhState.$list);
  const { listLoaiHopDong } = useAppSelector((state) => state.danhmucState);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [selectedId, setSelectedId] = useState<number>(0);

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryQuyetDinh>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getListQuyetDinh(newQuery));
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
    dispatch(getListQuyetDinh(query));
  };

  const columns: IColumn<IViewQuyetDinh>[] = [
    {
      key: 'maNhanSu',
      dataIndex: 'maNhanSu',
      title: 'Mã nhân sự'
    },
    {
      key: 'hoTen',
      dataIndex: 'hoTen',
      title: 'Họ tên'
    },
    {
      key: 'loaiQuyetDinh',
      dataIndex: 'loaiQuyetDinh',
      title: 'Loại quyết định',
      type: ETableColumnType.STATUS,
      align: 'center',
      getTagInfo: (status: number) => NsQuyetDinhTypeConst.getTag(status)
    },
    {
      key: 'noiDungTomTat',
      dataIndex: 'noiDungTomTat',
      title: 'Nội dung'
    },
    {
      key: 'ngayHieuLuc',
      dataIndex: 'ngayHieuLuc',
      title: 'Bắt đầu hiệu lực từ',
      align: 'center',
      render: (val: string) => formatDateTimeView(val)
    }
  ];

  const actions: IAction[] = [];

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  return (
    <Card
      title="Danh sách các quyết định"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item<IQueryQuyetDinh> label="Nội dung:" name="Keyword">
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
        columns={columns}
        dataSource={data}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />
    </Card>
  );
};

export default Page;
