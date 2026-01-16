'use client';

import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Form, Input, message, Modal, Select } from 'antd';
import {
  AudioOutlined,
  CheckOutlined,
  DeleteOutlined,
  EditOutlined,
  EyeOutlined,
  PlusOutlined,
  SearchOutlined,
  SendOutlined,
  SyncOutlined
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
import { ICreateReceptionTime, IQueryGuestGroup, IViewGuestGroup } from '@models/delegation/delegation.model';
import {
  deleteDoanVao,
  getListGuestGroup,
  getListNhanSu,
  getListPhongBan,
  getListStatus,
  updateStatus
} from '@redux/feature/delegation/delegationThunk';
import { resetStatusCreate, select } from '@redux/feature/delegation/delegationSlice';
import { ETableColumnType } from '@/constants/e-table.consts';
import { DelegationStatusConst } from '../../../../../constants/core/delegation/delegation-status.consts';
import AutoCompleteAntd from '@components/hieu-custom/combobox';
import CreateDoanVaoModal from './(dialog)/create';
import { toast } from 'react-toastify';
import { useRouter } from 'next/navigation';
import { openConfirmStatusModal } from '../../modals/confirm-status-modal';
import VoiceSearch from '../../../../../src/components/hieu-custom/voice-search';

const Page = () => {
  const [form] = Form.useForm();
  const router = useRouter();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem, listPhongBan, listStatus } = useAppSelector((state) => state.delegationState);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [showVoiceSearch, setShowVoiceSearch] = useState<boolean>(false);
  const [voiceData, setVoiceData] = useState<IViewGuestGroup[] | null>(null);

  const columns: IColumn<IViewGuestGroup>[] = [
    {
      key: 'code',
      dataIndex: 'code',
      title: 'Mã đoàn',
      align: 'center',
      width: 100
    },
    {
      key: 'name',
      dataIndex: 'name',
      title: 'Tên đoàn vào',
      width: 200
    },
    {
      key: 'content',
      dataIndex: 'content',
      title: 'Nội dung',
      width: 200
    },
    {
      key: 'phongBan',
      dataIndex: 'phongBan',
      title: 'Phòng ban phụ trách',
      width: 200
    },
    {
      key: 'location',
      dataIndex: 'location',
      title: 'Địa điểm',
      width: 100
    },
    {
      key: 'staffReceptionName',
      dataIndex: 'staffReceptionName',
      title: 'Nhân sự tiếp đón',
      align: 'center',
      width: 200
    },
    {
      key: 'totalPerson',
      dataIndex: 'totalPerson',
      title: 'Tổng số người',
      align: 'center',
      width: 120
    },
    {
      key: 'phoneNumber',
      dataIndex: 'phoneNumber',
      title: 'SĐT liên hệ',
      width: 200
    },
    {
      key: 'requestDate',
      dataIndex: 'requestDate',
      title: 'Ngày yêu cầu',
      width: 200,
      render: (value) => <p>{formatDateView(value)}</p>
    },
    {
      key: 'receptionDate',
      dataIndex: 'receptionDate',
      title: 'Ngày tiếp đón',
      width: 200,
      render: (value) => <p>{formatDateView(value)}</p>
    },
    {
      key: 'totalMoney',
      dataIndex: 'totalMoney',
      title: 'Tổng chi phí',
      align: 'left',
      width: 120
    },
    {
      key: 'status',
      dataIndex: 'status',
      title: 'Trạng thái',
      align: 'center',
      type: ETableColumnType.STATUS,
      getTagInfo: (status: number) => DelegationStatusConst.getTag(status)
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Xem chi tiết',
      icon: <EyeOutlined />,
      hidden: (r) => r.status == DelegationStatusConst.DONE || r.status == DelegationStatusConst.DA_HET_HAN,
      command: (record: IViewGuestGroup) => onClickView(record)
    },
    {
      label: 'Chỉnh sửa',
      tooltip: 'Sửa danh sách đoàn vào',
      icon: <EditOutlined />,
      hidden: (r) => r.status == DelegationStatusConst.DONE || r.status == DelegationStatusConst.DA_HET_HAN,
      command: (record: IViewGuestGroup) => onClickUpdate(record)
    },
    {
      label: 'Đề xuất',
      icon: <SendOutlined />,
      hidden: (r) => r.status !== DelegationStatusConst.TAO_MOI,
      command: (record: IViewGuestGroup) => onClickUpdateStatus(record)
    },

    {
      label: 'Thêm thời gian',
      icon: <PlusOutlined />,
      hidden: (r) => r.status == DelegationStatusConst.TAO_MOI || r.status === DelegationStatusConst.DONE || r.status == DelegationStatusConst.DA_HET_HAN,
      command: (record: IViewGuestGroup) => onClickCreateTime(record)
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: (record: IViewGuestGroup) => onClickDelete(record)
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryGuestGroup>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: '',
      status: DelegationStatusConst.TAO_MOI
    },
    onQueryChange: (newQuery) => {
      dispatch(getListGuestGroup(newQuery));
    },
    triggerFirstLoad: true
  });
  useEffect(() => {
    form.setFieldsValue({
      status: DelegationStatusConst.TAO_MOI
    });
  }, []);

  useEffect(() => {
    if (!isModalOpen) {
      dispatch(resetStatusCreate());
      dispatch(getListGuestGroup(query));
      dispatch(getListPhongBan());
      dispatch(getListNhanSu());
      dispatch(getListStatus());
      setVoiceData(null);
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

  const onClickView = (data: IViewGuestGroup) => {
    router.push(`/delegation/incoming/detail/${data.id}`);
  };
  const onClickCreateTime = (data: IViewGuestGroup) => {
    router.push(`/delegation/incoming/list-delegation/create-reception-time?delegationIncomingId=${data.id}`);
  };
  const onClickUpdate = (data: IViewGuestGroup) => {
    dispatch(select(data));
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };
  const onClickDelete = (record: IViewGuestGroup) => {
    console.log(record);
    Modal.confirm({
      title: `Xóa đoàn vào "${record.name}"?`,
      okText: 'Xóa',
      okType: 'danger',
      cancelText: 'Hủy',
      onOk: async () => {
        try {
          await dispatch(deleteDoanVao(record.id)).unwrap();
          toast.success('Xóa thành công!');
        } catch (error: any) {
          toast.error(error?.response?.message || 'Xóa thất bại!');
        }
      }
    });
  };

  return (
    <Card
      title="Danh sách Đoàn vào"
      className="min-h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="mb-4 flex flex-row items-center space-x-3">
          <Form.Item name="idPhongBan" className="!mb-0 w-[350px]">
            <Select
              showSearch
              allowClear
              placeholder="Chọn phòng ban phụ trách"
              optionFilterProp="label"
              filterOption={(input, option) => (option?.label ?? '').toLowerCase().includes(input.toLowerCase())}
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
                label: DelegationStatusConst.getInfo(st.status, 'label') ?? ''
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
              setVoiceData(null);
              dispatch(getListGuestGroup(query));
            }}
          >
            Tải lại
          </Button>
          <Button
            color="default"
            variant="filled"
            icon={<AudioOutlined />}
            onClick={() => setShowVoiceSearch((prev) => !prev)}
          ></Button>
        </div>
      </Form>
      {showVoiceSearch && (
        <div className="voice-fixed-wrapper">
          <VoiceSearch onResult={(data) => setVoiceData(data)} />
        </div>
      )}

      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={voiceData?.length ? voiceData : list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
        height={450}
      />

      <CreateDoanVaoModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        isUpdate={isUpdate}
        isView={isView}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuDelegation);
