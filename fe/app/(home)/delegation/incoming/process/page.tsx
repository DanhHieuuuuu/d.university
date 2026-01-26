'use client';

import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Form, Input, message, Modal, Select } from 'antd';
import {
  CheckOutlined,
  DeleteOutlined,
  DeploymentUnitOutlined,
  EditOutlined,
  EyeOutlined,
  FileWordOutlined,
  PlayCircleOutlined,
  PlusOutlined,
  SearchOutlined,
  SendOutlined,
  SyncOutlined,
  UserAddOutlined
} from '@ant-design/icons';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { resetStatusCreate, selectIdNhanSu } from '@redux/feature/hrm/nhansu/nhansuSlice';
import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { ICreateDepartment, IDepartmentSupport, IQueryGuestGroup, IViewGuestGroup } from '@models/delegation/delegation.model';
import {
  deleteDoanVao,
  getListDelegationIncoming,
  getListGuestGroup,
  getListPhongBan,
  getListStatus,
  updateStatus
} from '@redux/feature/delegation/delegationThunk';
import { select } from '@redux/feature/delegation/delegationSlice';
import { ETableColumnType } from '@/constants/e-table.consts';
import { DelegationStatusConst } from '../../../../../constants/core/delegation/delegation-status.consts';
import AutoCompleteAntd from '@components/hieu-custom/combobox';
import { toast } from 'react-toastify';
import { useRouter } from 'next/navigation';
import { exportBaoCaoDoanVao } from '@helpers/delegation/action.helper';
import CreateDepartmentSupportModal from '../support/(dialog)/create';
import { selectDelegationIncomingId, selectDepartmentSupport } from '@redux/feature/delegation/department/departmentSlice';
import { openConfirmStatusModal } from '../../modals/confirm-status-modal';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem, listPhongBan, listStatus } = useAppSelector((state) => state.delegationState);
  const router = useRouter();
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);

  const columns: IColumn<IViewGuestGroup>[] = [
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
      width: 160,
      render: (value: number) => {
        const pb = listPhongBan.find((p: any) => p.idPhongBan === value);
        return pb ? pb.tenPhongBan : '';
      }
    },
    {
      key: 'location',
      dataIndex: 'location',
      title: 'Địa điểm',
      align: 'center',
      width: 120
    },
    {
      key: 'idStaffReception',
      dataIndex: 'staffReceptionName',
      title: 'Nhân sự tiếp đón',
      align: 'center',
      width: 160
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
      title: 'SĐT liên hệ'
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
      command: (record: IViewGuestGroup) => onClickView(record),
      permission: PermissionCoreConst.CoreButtonViewXuLyDoanVao,
    },
    {
      label: 'Báo cáo kết quả',
      icon: <CheckOutlined />,
      hidden: (r) => r.status !== DelegationStatusConst.DANG_TIEP_DOAN,
      command: (record: IViewGuestGroup) => onClickBaoCao(record),
      permission: PermissionCoreConst.CoreButtonBaoCaoXuLyDoanVao,
    },
    {
      label: 'Xuất báo cáo',
      icon: <FileWordOutlined/>,
      hidden: (r) => r.status !== DelegationStatusConst.DONE,
      command: (record: IViewGuestGroup) => onExport(record),
      permission: PermissionCoreConst.CoreButtonUpdateDoanVao,
    },
    {
      label: 'Phê duyệt',
      icon: <CheckOutlined />,
      hidden: (r) => r.status !== DelegationStatusConst.DE_XUAT,
      command: (record: IViewGuestGroup) => onClickPheDuyet(record),
      permission: PermissionCoreConst.CoreButtonPheDuyetXuLyDoanVao,
    },
    {
      label: 'Tiếp đoàn',
      icon: <DeploymentUnitOutlined />,
      hidden: (r) => r.status !== DelegationStatusConst.PHE_DUYET || !r.receptionTimes?.length,
      command: (record: IViewGuestGroup) => onClickTiepDoan(record),
      permission: PermissionCoreConst.CoreButtonTiepDoanXuLyDoanVao,
    },
    {
      label: 'Phân công hỗ trợ',
      icon: <UserAddOutlined/>,
      // hidden: (r) => r.status !== DelegationStatusConst.DANG_TIEP_DOAN,
      command: (record: IDepartmentSupport) => onClickPhanCong(record),
      permission: PermissionCoreConst.CoreButtonBaoCaoXuLyDoanVao,
    },
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
      dispatch(getListDelegationIncoming());
    }
  }, [isModalOpen]);

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };
const onClickPhanCong = (data: IDepartmentSupport) => {
  dispatch(selectDelegationIncomingId(data.id));
  setIsModalView(false);
  setIsModalUpdate(false);
  setIsModalOpen(true);
};
  const onClickView = (data: IViewGuestGroup) => {
    router.push(`/delegation/incoming/detail/${data.id}`);
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
  const onClickPheDuyet = (data: IViewGuestGroup) => {
    openConfirmStatusModal({
      title: 'Xác nhận tiếp đoàn',
      content: `Bạn có muốn tiếp đoàn vào "${data.name}" không?`,
      okText: 'Đồng ý',
      cancelText: 'Không đồng ý',
      okAction: 'upgrade',
      cancelAction: 'supplement',
      data,
      dispatch,
      onSuccess: () => {
        dispatch(getListGuestGroup(query));
      }
    });
  };
  const onClickBaoCao = (data: IViewGuestGroup) => {
    openConfirmStatusModal({
      title: 'Xác nhận',
      content: `Bạn có hoàn thành đoàn vào "${data.name}" không?`,
      okText: 'Hoàn thành',
      okAction: 'upgrade',
      data,
      dispatch,
      onSuccess: () => {
        dispatch(getListGuestGroup(query));
      }
    });
  };
  const onExport = (record: IViewGuestGroup) => {
      exportBaoCaoDoanVao(record);
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
              data-permission={PermissionCoreConst.CoreButtonSearchXuLyDoanVao}
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
              data-permission={PermissionCoreConst.CoreButtonSearchXuLyDoanVao}
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
            <Input 
              data-permission={PermissionCoreConst.CoreButtonSearchXuLyDoanVao} 
              placeholder="Nhập tên đoàn vào…" 
              onChange={(e) => handleSearch(e)} />
          </Form.Item>
          <Button
            color="default"
            variant="filled"
            icon={<SyncOutlined />}
            onClick={() => {
              form.resetFields();
              resetFilter();
            }}
            data-permission={PermissionCoreConst.CoreButtonSearchXuLyDoanVao}
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
        scroll={{x: 'max-content', y: 'calc(100vh - 350px)'}}
        data-permission={PermissionCoreConst.CoreButtonTableXuLyDoanVao}
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

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuXuLyDoanVao);
