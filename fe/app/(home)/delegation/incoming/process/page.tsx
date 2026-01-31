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
import {
  ICreateDepartment,
  IDepartmentSupport,
  IQueryGuestGroup,
  IViewGuestGroup
} from '@models/delegation/delegation.model';
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
import {
  selectDelegationIncomingId,
  selectDepartmentSupport
} from '@redux/feature/delegation/department/departmentSlice';
import { ConfirmStatusModal } from '../../modals/ConfirmStatusModal';
import { useIsGranted } from '@hooks/useIsGranted';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem, listPhongBan, listStatus } = useAppSelector((state) => state.delegationState);
  const router = useRouter();
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [confirmOpen, setConfirmOpen] = useState(false);
  const [confirmContent, setConfirmContent] = useState('');
  const [confirmTitle, setConfirmTitle] = useState('');
  const [selectedData, setSelectedData] = useState<IViewGuestGroup | null>(null);
  //permission
  const hasPermisisonViewXuLyDoanVao = useIsGranted(PermissionCoreConst.CoreButtonViewXuLyDoanVao);
  const hasPermissionPheDuyetXuLyDoanVao = useIsGranted(PermissionCoreConst.CoreButtonPheDuyetXuLyDoanVao);
  const hasPermissisonTiepDoanXuLyDoanVao = useIsGranted(PermissionCoreConst.CoreButtonTiepDoanXuLyDoanVao);
  const hasPermissisonPhanCongXuLyDoanVao = useIsGranted(PermissionCoreConst.CoreButtonPhanCongXuLyDoanVao);
  const hasPermissisonBaoCaoDoanVao = useIsGranted(PermissionCoreConst.CoreButtonBaoCaoXuLyDoanVao);
  const hasPermissionXacNhanChinhSuaXuLyDoanVao = useIsGranted(
    PermissionCoreConst.CoreButtonXacNhanChinhSuaXuLyDoanVao
  );
  const hasPermissionCreateTimeXuLyDoanVao = useIsGranted(PermissionCoreConst.CoreButtonCreateTimeXuLyDoanVao);
  const hasPermissionSearchXuLyDoanVao = useIsGranted(PermissionCoreConst.CoreButtonSearchXuLyDoanVao);
  const hasPermissionXuatBaoCaoXuLyDoanVao = useIsGranted(PermissionCoreConst.CoreButtonXuatBaoCaoXuLyDoanVao);

  const [confirmActions, setConfirmActions] = useState<
    {
      action: 'upgrade' | 'cancel' | 'supplement';
      label: string;
      type?: 'primary' | 'default';
      danger?: boolean;
    }[]
  >([]);

  const columns: IColumn<IViewGuestGroup>[] = [
    {
      key: 'code',
      dataIndex: 'code',
      title: 'Mã đoàn',
      align: 'center',
      width: 120
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
      title: 'SĐT liên hệ',
      width: 120
    },
    {
      key: 'totalMoney',
      dataIndex: 'totalMoney',
      title: 'Tổng chi phí ước tính (VNĐ)',
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
      hidden: () => !hasPermisisonViewXuLyDoanVao
    },
    {
      label: 'Hoàn thành',
      icon: <CheckOutlined />,
      hidden: (r) => !hasPermissisonBaoCaoDoanVao || r.status !== DelegationStatusConst.DANG_TIEP_DOAN,
      command: (record: IViewGuestGroup) => onClickBaoCao(record)
    },
    {
      label: 'Xuất báo cáo',
      icon: <FileWordOutlined />,
      hidden: (r) => !hasPermissionXuatBaoCaoXuLyDoanVao || r.status !== DelegationStatusConst.DONE,
      command: (record: IViewGuestGroup) => onExport(record)
    },
    {
      label: 'Phê duyệt',
      icon: <CheckOutlined />,
      hidden: (r) =>
        !hasPermissionPheDuyetXuLyDoanVao ||
        (r.status !== DelegationStatusConst.DE_XUAT && r.status !== DelegationStatusConst.DA_CHINH_SUA),
      command: (record: IViewGuestGroup) => onClickPheDuyet(record)
    },
    {
      label: 'Tiếp đoàn',
      icon: <DeploymentUnitOutlined />,
      hidden: (r) =>
        !hasPermissisonTiepDoanXuLyDoanVao || r.status !== DelegationStatusConst.PHE_DUYET || !r.receptionTimes?.length,
      command: (record: IViewGuestGroup) => onClickTiepDoan(record)
    },
    {
      label: 'Phân công hỗ trợ',
      icon: <UserAddOutlined />,
      hidden: (r) => !hasPermissisonPhanCongXuLyDoanVao || r.status !== DelegationStatusConst.PHE_DUYET,
      command: (record: IDepartmentSupport) => onClickPhanCong(record)
    },
    {
      label: 'Xác nhận bổ sung',
      icon: <CheckOutlined />,
      hidden: (r) => !hasPermissionXacNhanChinhSuaXuLyDoanVao || r.status !== DelegationStatusConst.CAN_BO_SUNG,
      command: (record: IViewGuestGroup) => onClickXacNhan(record)
    },
    {
      label: 'Thêm thời gian',
      icon: <PlusOutlined />,
      hidden: (r) =>
        !hasPermissionCreateTimeXuLyDoanVao ||
        r.status !== DelegationStatusConst.PHE_DUYET,
      command: (record: IViewGuestGroup) => onClickCreateTime(record)
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryGuestGroup>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: '',
      differentStatus: `${DelegationStatusConst.TAO_MOI},
                        ${DelegationStatusConst.DA_HET_HAN},
                        ${DelegationStatusConst.BI_HUY}`,
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
      dispatch(getListStatus(1));
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
    setSelectedData(data);
    setConfirmTitle('Xác nhận tiếp đoàn');
    setConfirmContent(`Bạn có muốn tiếp đoàn "${data.name}" không?`);
    setConfirmActions([
      { action: 'cancel', label: 'Không đồng ý', danger: true },
      { action: 'upgrade', label: 'Đồng ý', type: 'primary' }
    ]);
    setConfirmOpen(true);
  };
  const onClickCreateTime = (data: IViewGuestGroup) => {
    router.push(`/delegation/incoming/list-delegation/create-reception-time?delegationIncomingId=${data.id}`);
  };
  const onClickPheDuyet = (data: IViewGuestGroup) => {
    setSelectedData(data);
    setConfirmTitle('Xác nhận phê duyệt');
    setConfirmContent(`Bạn có đồng ý phê duyệt đoàn vào "${data.name}" không?`);

    // ĐÃ CHỈNH SỬA: Đồng ý / Không đồng ý
    if (data.status === DelegationStatusConst.DA_CHINH_SUA) {
      setConfirmActions([
        { action: 'supplement', label: 'Cần bổ sung' },
        { action: 'cancel', label: 'Không đồng ý', danger: true },
        { action: 'upgrade', label: 'Đồng ý', type: 'primary' }
      ]);
    }
    // ĐỀ XUẤT : Cần bổ sung / Đồng ý
    else {
      setConfirmActions([
        { action: 'supplement', label: 'Cần bổ sung' },
        { action: 'cancel', label: 'Không đồng ý', danger: true },
        { action: 'upgrade', label: 'Đồng ý', type: 'primary' }
      ]);
    }

    setConfirmOpen(true);
  };

  const onClickBaoCao = (data: IViewGuestGroup) => {
    setSelectedData(data);
    setConfirmTitle('Xác nhận hoàn thành');
    setConfirmContent(`Bạn có xác nhận hoàn thành đoàn vào "${data.name}" không?`);
    setConfirmActions([
      { action: 'cancel', label: 'Không đồng ý', danger: true },
      { action: 'upgrade', label: 'Đồng ý', type: 'primary' }
    ]);
    setConfirmOpen(true);
  };

  const onClickXacNhan = (data: IViewGuestGroup) => {
    setSelectedData(data);
    setConfirmTitle('Xác nhận đã bổ sung');
    setConfirmContent(`Xác nhận đã bổ sung thông tin cho "${data.name}"`);
    setConfirmActions([{ action: 'upgrade', label: 'Xác nhận', type: 'primary' }]);
    setConfirmOpen(true);
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
        {hasPermissionSearchXuLyDoanVao && (
          <div className="mb-4 flex flex-row items-center space-x-3">
            <Form.Item name="idPhongBan" className="!mb-0 w-[350px]">
              <Select
                showSearch
                allowClear
                placeholder="Chọn phòng ban phụ trách"
                options={listPhongBan.map((pb: any) => ({
                  value: pb.idPhongBan,
                  label: pb.tenPhongBan
                }))}
                onChange={(value) => onFilterChange({ idPhongBan: value })}
              />
            </Form.Item>

            <Form.Item name="status" className="!mb-0 w-[200px]">
              <Select
                allowClear
                placeholder="Chọn trạng thái"
                options={listStatus.map((st: any) => ({
                  value: st.status,
                  label: DelegationStatusConst.getInfo(st.status, 'label') ?? ''
                }))}
                onChange={(value) => onFilterChange({ status: value })}
              />
            </Form.Item>

            <Form.Item name="name" className="!mb-0 w-[300px]">
              <Input placeholder="Nhập tên hoặc mã đoàn vào" onChange={handleSearch} />
            </Form.Item>

            <Button
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                resetFilter();
              }}
            >
              Tải lại
            </Button>
          </div>
        )}
      </Form>

      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
        scroll={{ x: 'max-content', y: 'calc(100vh - 350px)' }}
        data-permission={PermissionCoreConst.CoreButtonTableXuLyDoanVao}
      />
      <CreateDepartmentSupportModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        isUpdate={isUpdate}
        isView={isView}
      />
      {selectedData && (
        <ConfirmStatusModal
          open={confirmOpen}
          title={confirmTitle}
          content={confirmContent}
          data={selectedData}
          dispatch={dispatch}
          onClose={() => setConfirmOpen(false)}
          onSuccess={() => {
            dispatch(getListGuestGroup(query));
          }}
          actions={confirmActions}
        />
      )}
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuXuLyDoanVao);
