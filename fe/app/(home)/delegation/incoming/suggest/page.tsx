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

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem } = useAppSelector((state) => state.nhanSuState);

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

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryNhanSu>({
    total: totalItem,
    initialQuery: {
      SkipCount: 0,
      MaxResultCount: 10,
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
    setIsModalView(true);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickUpdate = (data: IViewNhanSu) => {
    dispatch(selectMaNhanSu(data.idNhanSu));
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

const options = [
  { value: "Hà Nội", key: "hn" },
  { value: "Hồ Chí Minh", key: "hcm" },
];


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
      <InputAntdWithTitle title="Tài khoản" name="username" label="Tên đăng nhập" />
      <AutoCompleteAntd
        name="city"
        title="Thành phố"
        placeholder="Nhập tên thành phố..."
        options={options}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuNhanSu);
