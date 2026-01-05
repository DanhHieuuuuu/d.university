'use client';

import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Form, Input, Select } from 'antd';
import {
  DeleteOutlined,
  EditOutlined,
  EyeOutlined,
  PlusOutlined,
  SearchOutlined,
  SyncOutlined
} from '@ant-design/icons';
import { useNavigate } from '@hooks/navigate';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { resetStatusCreate, selectMaNhanSu } from '@redux/feature/nhansu/nhansuSlice';
import { getHoSoNhanSu, getListNhanSu } from '@redux/feature/nhansu/nhansuThunk';
import { IQueryNhanSu, IViewNhanSu } from '@models/nhansu/nhansu.model';

import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';

import CreateNhanSuModal from './(dialog)/create-or-update-ns';

const Page = () => {
  const { navigateTo } = useNavigate();
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem } = useAppSelector((state) => state.nhanSuState);
  const { phongBan } = useAppSelector((state) => state.danhmucState);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);

  const columns: IColumn<IViewNhanSu>[] = [
    {
      key: 'idNhanSu',
      dataIndex: 'idNhanSu',
      title: 'ID',
      align: 'center',
      showOnConfig: false
    },
    {
      key: 'maNhanSu',
      dataIndex: 'maNhanSu',
      title: 'Mã NS',
      align: 'center'
    },
    {
      key: 'hoTen',
      dataIndex: 'hoTen',
      title: 'Họ và tên'
    },
    {
      key: 'ngaySinh',
      dataIndex: 'ngaySinh',
      title: 'Ngày sinh',
      render: (value) => {
        const date = formatDateView(value);
        return <p>{date}</p>;
      }
    },
    {
      key: 'noiSinh',
      dataIndex: 'noiSinh',
      title: 'Nơi sinh'
    },
    {
      key: 'soDienThoai',
      dataIndex: 'soDienThoai',
      title: 'Số điện thoại'
    },
    {
      key: 'email',
      dataIndex: 'email',
      title: 'Email'
    },
    {
      key: 'tenChucVu',
      dataIndex: 'tenChucVu',
      title: 'Chức vụ'
    },
    {
      key: 'tenPhongBan',
      dataIndex: 'tenPhongBan',
      title: 'Phòng / Ban'
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Hồ sơ nhân sự',
      icon: <EyeOutlined />,
      command: (record: IViewNhanSu) => onClickView(record)
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin nhân viên',
      icon: <EditOutlined />,
      command: (record: IViewNhanSu) => onClickUpdate(record)
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IViewNhanSu) => console.log('delete', record)
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryNhanSu>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getListNhanSu(newQuery));
    },
    triggerFirstLoad: true
  });

  useEffect(() => {
    if (!isModalOpen) {
      dispatch(resetStatusCreate());
      dispatch(getListNhanSu(query));
    }
  }, [isModalOpen]);

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ cccd: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickView = (data: IViewNhanSu) => {
    dispatch(selectMaNhanSu(data.idNhanSu));
    dispatch(getHoSoNhanSu(data.idNhanSu));
    navigateTo(`/hrm/employee/${data.idNhanSu}`);
  };

  const onClickUpdate = (data: IViewNhanSu) => {
    dispatch(selectMaNhanSu(data.idNhanSu));
    dispatch(getHoSoNhanSu(data.idNhanSu));
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

  return (
    <Card
      title="Danh sách nhân sự"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2 gap-2">
          <Form.Item<IQueryNhanSu> label="Cccd:" name="cccd">
            <Input onChange={(e) => handleSearch(e)} />
          </Form.Item>
          <Form.Item<IQueryNhanSu> label="Phòng ban" name="idPhongBan">
            <Select
              allowClear
              options={phongBan.$list.data?.map((item) => {
                return { label: item.tenPhongBan, value: item.id };
              })}
              onChange={(val) => onFilterChange({ idPhongBan: val })}
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

      <CreateNhanSuModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        isUpdate={isUpdate}
        isView={isView}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuNhanSu);
