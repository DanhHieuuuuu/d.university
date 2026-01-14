'use client';

import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Form, Input, message, Modal, Select } from 'antd';
import {
  DeleteOutlined,
  EditOutlined,
  EyeOutlined,
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  UserAddOutlined
} from '@ant-design/icons';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { IDepartmentSupport, IQueryGuestGroup, ISupporter, IViewGuestGroup } from '@models/delegation/delegation.model';
import {
  deleteDoanVao,
  getListDelegationIncoming,
  getListDepartmentSupport,
  getListGuestGroup,
  getListNhanSu,
  getListPhongBan,
  getListStatus
} from '@redux/feature/delegation/delegationThunk';
import { select } from '@redux/feature/delegation/delegationSlice';
import { ETableColumnType } from '@/constants/e-table.consts';
import { DelegationStatusConst } from '../../../../../constants/core/delegation/delegation-status.consts';
import AutoCompleteAntd from '@components/hieu-custom/combobox';
import { toast } from 'react-toastify';
import CreateDepartmentSupportModal from './(dialog)/create';
import router from 'next/router';
import { useRouter, useSearchParams } from 'next/navigation';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { status, total: totalItem, listDepartmentSupport } = useAppSelector((state) => state.delegationState);
  const router = useRouter();
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);

  const columns: IColumn<IDepartmentSupport>[] = [
    {
      key: 'departmentSupportName',
      dataIndex: 'departmentSupportName',
      title: 'Phòng ban hỗ trợ'
    },
    {
      key: 'delegationIncomingName',
      dataIndex: 'delegationIncomingName',
      title: 'Tên đoàn vào'
    },
    {
      key: 'content',
      dataIndex: 'content',
      title: 'Nội dung'
    },
    {
      key: 'supporters',
      dataIndex: 'supporters',
      title: ' Mã nhân sự hỗ trợ',
      render: (supporters: ISupporter[]) =>
        supporters.length > 0 ? supporters.map((s) => s.supporterCode).join(', ') : '-'
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Xem chi tiết',
      icon: <EyeOutlined />,
      command: (record: IDepartmentSupport) => onClickView(record)
    },
    {
      label: 'Thêm nhân viên',
      icon: <UserAddOutlined />,
      command: (record: IDepartmentSupport) => onClickCreateStaff(record)
    }
  ];
  const onClickCreateStaff = (data: IDepartmentSupport) => {
    router.push(`/delegation/incoming/support/create-staff-support?departmentSupportId=${data.id}`);
  };
  const onClickView = (data: IDepartmentSupport) => {
    router.push(`/delegation/incoming/support/edit?departmentSupportId=${data.id}`);
  };
  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryGuestGroup>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getListDepartmentSupport(newQuery));
    },
    triggerFirstLoad: true
  });

  useEffect(() => {
    if (!isModalOpen) {
      dispatch(getListDepartmentSupport(query));
      dispatch(getListPhongBan());
      dispatch(getListDelegationIncoming());
      dispatch(getListNhanSu());
    }
  }, [isModalOpen]);

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  return (
    <Card
      title=" Phòng ban hỗ trợ"
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
            <Input placeholder="Nhập tên đoàn vào…" onChange={(e) => handleSearch(e)} />
          </Form.Item>
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
      </Form>
      <AppTable
        loading={status === ReduxStatus.LOADING}
        columns={columns}
        dataSource={listDepartmentSupport}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />
      <CreateDepartmentSupportModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        isUpdate={isUpdate}
        isView={isView}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuDelegation);
