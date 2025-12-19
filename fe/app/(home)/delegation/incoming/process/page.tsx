'use client';

import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Form, Input, message, Modal, Select } from 'antd';
import {
  DeleteOutlined,
  DeploymentUnitOutlined,
  EditOutlined,
  EyeOutlined,
  PlayCircleOutlined,
  PlusOutlined,
  SearchOutlined,
  SendOutlined,
  SyncOutlined
} from '@ant-design/icons';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { resetStatusCreate, selectMaNhanSu } from '@redux/feature/nhansu/nhansuSlice';
import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { IQueryGuestGroup, IViewGuestGroup } from '@models/delegation/delegation.model';
import {
  deleteDoanVao,
  getListGuestGroup,
  getListPhongBan,
  getListStatus,
  updateStatus
} from '@redux/feature/delegation/delegationThunk';
import { select } from '@redux/feature/delegation/delegationSlice';
import { ETableColumnType } from '@/constants/e-table.consts';
import { DelegationStatusConst } from '../../consts/delegation-status.consts';
import AutoCompleteAntd from '@components/hieu-custom/combobox';
import { toast } from 'react-toastify';
import { openConfirmStatusModal } from '../../modals/confirm-status-modal';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem, listPhongBan, listStatus } = useAppSelector((state) => state.delegationState);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);

  const columns: IColumn<IViewGuestGroup>[] = [
    {
      key: 'stt',
      dataIndex: 'stt',
      title: 'STT',
      align: 'center',
      render: (value, row, index) => index + 1
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
      align: 'center',
      render: (value: number) => {
        const pb = listPhongBan.find((p: any) => p.idPhongBan === value);
        return pb ? pb.tenPhongBan : '';
      }
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
      key: 'totalMoney',
      dataIndex: 'totalMoney',
      title: 'Tổng chi phí',
      align: 'right'
    },
    {
      key: 'status',
      dataIndex: 'status',
      title: 'Trạng thái',
      align: 'center',
      type: ETableColumnType.STATUS,
      getTagInfo: (status: number) => DelegationStatusConst.getInfo(status)
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Xem chi tiết',
      icon: <EyeOutlined />,
      command: (record: IViewGuestGroup) => onClickView(record)
    },
    {
      label: 'Đề xuất',
      icon: <SendOutlined />,
      command: (record: IViewGuestGroup) => onClickUpdateStatus(record)
    },
    {
      label: 'Tiếp đoàn',
      icon: <DeploymentUnitOutlined />,
      hidden: (r) => r.status !== DelegationStatusConst.PHE_DUYET,
      command: (record: IViewGuestGroup) => onClickTiepDoan(record)
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryGuestGroup>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
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
      dispatch(getListPhongBan());
      dispatch(getListStatus());
    }
  }, [isModalOpen]);

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  const onClickView = (data: IViewGuestGroup) => {
    dispatch(select(data));
    setIsModalView(true);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };
  const onClickUpdateStatus = (data: IViewGuestGroup) => {
    openConfirmStatusModal({
      title: 'Xác nhận đề xuất',
      content: `Bạn có muốn đề xuất đoàn vào "${data.name}" không?`,
      okText: 'Đề xuất',
      okAction: 'upgrade',
      data,
      dispatch,
      onSuccess: () => {
        dispatch(getListGuestGroup(query));
      }
    });
  };

  const onClickTiepDoan = (data: IViewGuestGroup) => {
    openConfirmStatusModal({
      title: 'Xác nhận tiếp đoàn',
      content: `Bạn có muốn tiếp đoàn vào "${data.name}" không?`,
      okText: 'Đồng ý',
      cancelText: 'Không đồng ý',
      okAction: 'upgrade',
      cancelAction: 'cancel',
      data,
      dispatch,
      onSuccess: () => {
        dispatch(getListGuestGroup(query));
      }
    });
  };

  return (
    <Card
      title="Xử lý Đoàn vào"
      className="h-full"
      //   extra={
      //     <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
      //       Thêm mới
      //     </Button>
      //   }
    >
      <Form form={form} layout="horizontal">
        <div className="mb-4 flex flex-row items-center space-x-3">
          <Form.Item name="idPhongBan" className="!mb-0 w-[350px]">
            <Select
              placeholder="Chọn phòng ban phụ trách"
              allowClear
              options={listPhongBan.map((pb: any) => ({
                value: pb.idPhongBan,
                label: pb.tenPhongBan
              }))}
              onChange={(value) => onFilterChange({ idPhongBan: value })}
            />
          </Form.Item>
          <Form.Item name="status" className="!mb-0 w-[200px]">
            <Select
              placeholder="Chọn trạng thái"
              allowClear
              options={listStatus.map((st: any) => ({
                value: st.status,
                label: DelegationStatusConst.getInfo(st.status, 'name') ?? ''
              }))}
              onChange={(value) => onFilterChange({ status: value })}
            />
          </Form.Item>

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
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuDelegation);
