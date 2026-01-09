'use client';

import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Select } from 'antd';
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
import { IQueryHopDong, IViewHopDong } from '@models/nhansu/hopdong.model';
import { getListHopDong } from '@redux/feature/hrm/hopdong/hopdongThunk';
import { formatCurrency, formatDateTimeView, formatDateView } from '@utils/index';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data, status, total: totalItem } = useAppSelector((state) => state.hopdongState.$list);
  const { listLoaiHopDong } = useAppSelector((state) => state.danhmucState);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [selectedId, setSelectedId] = useState<number>(0);

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryHopDong>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getListHopDong(newQuery));
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
    dispatch(getListHopDong(query));
  };

  const columns: IColumn<IViewHopDong>[] = [
    {
      key: 'Id',
      dataIndex: 'id',
      title: 'ID',
      showOnConfig: false
    },
    {
      key: 'soHopDong',
      dataIndex: 'soHopDong',
      title: 'Số hợp đồng'
    },
    {
      key: 'idLoaiHopDong',
      dataIndex: 'idLoaiHopDong',
      title: 'Loại hợp đồng',
      render: (val) => {
        const item = listLoaiHopDong.find((x) => x.id === val);
        return <span>{item?.tenLoaiHopDong}</span>;
      }
    },
    {
      key: 'hoTen',
      dataIndex: 'hoTen',
      title: 'Họ tên'
    },
    {
      key: 'ngayKyKet',
      dataIndex: 'ngayKyKet',
      title: 'Ngày ký kết',
      align: 'center',
      render: (val: string) => formatDateTimeView(val)
    },
    {
      key: 'hopDongCoThoiHanTuNgay',
      dataIndex: 'hopDongCoThoiHanTuNgay',
      title: 'Có hiệu lực từ',
      align: 'center',
      render: (val: string) => formatDateTimeView(val)
    },
    {
      key: 'hopDongCoThoiHanDenNgay',
      dataIndex: 'hopDongCoThoiHanDenNgay',
      title: 'Có hiệu lực đến',
      align: 'center',
      render: (val: string) => (val != null ? formatDateTimeView(val) : '')
    },
    {
      key: 'luongCoBan',
      dataIndex: 'luongCoBan',
      title: 'Lương cơ bản',
      align: 'center',
      render: (val: number) => formatCurrency(val)
    },
    {
      key: 'ghiChu',
      dataIndex: 'ghiChu',
      title: 'Ghi chú'
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
      title="Danh sách hợp đồng"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          
          <Form.Item<IQueryHopDong> label="Loại hợp đồng:" name="loaiHopDong">
            <Select
              allowClear
              options={listLoaiHopDong.map((item) => {
                return { value: item.id, label: item.tenLoaiHopDong };
              })}
              onChange={(val) => onFilterChange({ loaiHopDong: val })}
            />
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
        dataSource={data}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />
    </Card>
  );
};

export default Page;
