'use client';

import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Form, Input } from 'antd';
import {
  DeleteOutlined,
  EditOutlined,
  EyeOutlined,
  PlusOutlined,
  SearchOutlined,
  SyncOutlined
} from '@ant-design/icons';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { resetStatusCreate, selectMaNhanSu } from '@redux/feature/nhansu/nhansuSlice';
import { getListNhanSu } from '@redux/feature/nhansu/nhansuThunk';
import { IQueryNhanSu, IViewNhanSu } from '@models/nhansu/nhansu.model';

import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import InputAntdWithTitle from '@components/hieu-custom/input';
import AutoCompleteAntd from '@components/hieu-custom/combobox';
import { IQueryGuestGroup, IViewGuestGroup } from '@models/delegation/delegation.model';
import { getListGuestGroup } from '@redux/feature/delegation/delegationThunk';
import { select } from '@redux/feature/delegation/delegationSlice';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem } = useAppSelector((state) => state.delegationState);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);

  const columns: IColumn<IViewGuestGroup>[] = [
    {
      key: 'id',
      dataIndex: 'id',
      title: 'ID',
      align: 'center',
      showOnConfig: false
    },
    {
      key: 'code',
      dataIndex: 'code',
      title: 'Mã đoàn',
      align: 'center'
    },
    {
      key: 'name',
      dataIndex: 'name',
      title: 'Tên đoàn vào'
    },
    {
      key: 'content',
      dataIndex: 'content',
      title: 'Nội dung'
    },
    {
      key: 'idPhongBan',
      dataIndex: 'idPhongBan',
      title: 'Phòng ban phụ trách',
      align: 'center'
    },
    {
      key: 'location',
      dataIndex: 'location',
      title: 'Địa điểm'
    },
    {
      key: 'idStaffReception',
      dataIndex: 'idStaffReception',
      title: 'Nhân sự tiếp đón',
      align: 'center'
    },
    {
      key: 'totalPerson',
      dataIndex: 'totalPerson',
      title: 'Tổng số người',
      align: 'center'
    },
    {
      key: 'phoneNumber',
      dataIndex: 'phoneNumber',
      title: 'SĐT liên hệ'
    },
    {
      key: 'status',
      dataIndex: 'status',
      title: 'Trạng thái',
      align: 'center'
    },
    {
      key: 'requestDate',
      dataIndex: 'requestDate',
      title: 'Ngày yêu cầu',
      render: (value) => <p>{formatDateView(value)}</p>
    },
    {
      key: 'receptionDate',
      dataIndex: 'receptionDate',
      title: 'Ngày tiếp đón',
      render: (value) => <p>{formatDateView(value)}</p>
    },
    {
      key: 'totalMoney',
      dataIndex: 'totalMoney',
      title: 'Tổng chi phí',
      align: 'right'
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Sửa',
      tooltip: 'Sửa danh sách đoàn vào',
      icon: <EditOutlined />,
      command: (record: IViewGuestGroup) => onClickUpdate(record)
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IViewGuestGroup) => console.log('delete', record)
    }
  ];

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryGuestGroup>({
    total: totalItem,
    initialQuery: {
      SkipCount: 0,
      MaxResultCount: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getListGuestGroup(newQuery));
    },
    triggerFirstLoad: true
  });

  useEffect(() => {
    if (!isModalOpen) {
      dispatch(resetStatusCreate());
      dispatch(getListGuestGroup(query));
    }
  }, [isModalOpen]);

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ name: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickUpdate = (data: IViewGuestGroup) => {
    dispatch(select(data.id));
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

  const options = [
    { value: 'Hà Nội', key: 'hn' },
    { value: 'Hồ Chí Minh', key: 'hcm' }
  ];

  return (
    <Card
      title="Danh sách Đoàn vào"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="mb-4 flex flex-row items-center space-x-3">
          <Form.Item name="name" className="!mb-0 w-[300px]">
            <Input placeholder="Tên đoàn vào…" onChange={(e) => handleSearch(e)} />
          </Form.Item>

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
      </Form>

      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />

      {/* <InputAntdWithTitle title="Tài khoản" name="username" label="Tên đăng nhập" />
      <AutoCompleteAntd
        name="city"
        title="Thành phố"
        placeholder="Nhập tên thành phố..."
        options={options}
      /> */}
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuDelegation);
