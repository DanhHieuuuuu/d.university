'use client';
import { useEffect, useMemo, useState } from 'react';
import { Button, Card, Dropdown, Form, Input, MenuProps, Modal, Popover, Select, Tag } from 'antd';
import {
  PlusOutlined, SearchOutlined, SyncOutlined, EditOutlined, DeleteOutlined,
  EyeOutlined, FilterOutlined, CheckCircleOutlined, EllipsisOutlined, SaveOutlined, UndoOutlined,
  RobotFilled
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiDonVi } from '@redux/feature/kpi/kpiSlice';
import { deleteKpiDonVi, getKpiDonViKeKhai, getListKpiRoleByUser, getListTrangThaiKpiCaNhan, getListTrangThaiKpiDonVi, getNhanSuDaGiaoByKpiDonVi, giaoKpiDonVi, updateKetQuaThucTeKpiDonVi, updateTrangThaiKpiDonVi } from '@redux/feature/kpi/kpiThunk';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import PositionModal from './(dialog)/create-or-update';
import { toast } from 'react-toastify';
import { getAllPhongBanByKpiRole } from '@redux/feature/danh-muc/danhmucThunk';
import { getAllUserByKpiRole } from '@redux/feature/userSlice';
import { buildKpiGroupedTable, KpiTableRow } from '@helpers/kpi/kpi.helper';
import KetQuaInput from '@components/bthanh-custom/kpiTableInput';
import { useKpiStatusAction } from '@hooks/kpi/UpdateStatusKPI';
import { formatKetQua } from '@helpers/kpi/formatResult.helper';
import { ETableColumnType } from '@/constants/e-table.consts';
import '@styles/kpi/table.kpi.scss'
import { IQueryKpiDonVi, IViewKpiDonVi, NhanSuDaGiaoDto } from '@models/kpi/kpi-don-vi.model';
import { KpiTrangThaiConst } from '@/constants/kpi/kpiStatus.const';
import { KPI_ORDER, KpiLoaiConst } from '@/constants/kpi/kpiType.const';
import AssignKpiModal from '../../modal/AssignKpiModal';
import { useIsGranted } from '@hooks/useIsGranted';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { withAuthGuard } from '@src/hoc/withAuthGuard';

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const { processUpdateStatus } = useKpiStatusAction();
  const { data: list, status, total: totalItem, summary } = useAppSelector((state) => state.kpiState.kpiDonVi.$list);
  const { data: listPhongBan } = useAppSelector((state) => state.danhmucState.phongBanByKpiRole.$list);
  const { data: trangThaiCaNhan, status: trangThaiStatus } = useAppSelector((state) => state.kpiState.meta.trangThai.donVi);
  const { data: namHocDonVi, status: namHocStatus } = useAppSelector((state) => state.kpiState.meta.namHoc.donVi);

  const canPropose = useIsGranted(PermissionCoreConst.CoreMenuKpiManageUnitActionPropose);
  const canCancelPropose = useIsGranted(PermissionCoreConst.CoreMenuKpiManageUnitActionCancelPropose);
  const canSendDeclared = useIsGranted(PermissionCoreConst.CoreMenuKpiManageUnitActionSendDeclared);
  const canCancelDeclared = useIsGranted(PermissionCoreConst.CoreMenuKpiManageUnitActionCancelDeclared);
  const canSaveScore = useIsGranted(PermissionCoreConst.CoreMenuKpiManageUnitActionSaveScore);
  const canAssign = useIsGranted(PermissionCoreConst.CoreMenuKpiManageUnitAssign);

  const [nhanSuDaGiao, setNhanSuDaGiao] = useState<NhanSuDaGiaoDto[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isUpdate, setIsModalUpdate] = useState(false);
  const [isView, setIsModalView] = useState(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);
  const [ketQuaMap, setKetQuaMap] = useState<Record<number, number | undefined>>({});
  const [openAssignModal, setOpenAssignModal] = useState(false);

  const selectedKpiDonVi = useAppSelector((state) => state.kpiState.kpiDonVi.$selected);

  const tableData = useMemo(() => {
    const sortedList = [...(list || [])].sort(
      (a, b) =>
        KPI_ORDER.indexOf(a.loaiKpi) -
        KPI_ORDER.indexOf(b.loaiKpi)
    );

    return buildKpiGroupedTable<IViewKpiDonVi>(sortedList);
  }, [list]);

  const { query, onFilterChange } = usePaginationWithFilter<IQueryKpiDonVi>({
    total: totalItem || 0,
    initialQuery: { PageIndex: 1, PageSize: 2000, Keyword: '' },
    onQueryChange: (newQuery) => dispatch(getKpiDonViKeKhai(newQuery)),
    triggerFirstLoad: true
  });

  useEffect(() => {
    dispatch(getAllPhongBanByKpiRole({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getListTrangThaiKpiCaNhan());
    dispatch(getListKpiRoleByUser());
    dispatch(getAllUserByKpiRole({ PageIndex: 1, PageSize: 1000 }));
  }, [dispatch]);


  const approveSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.DA_KE_KHAI],
      invalidMsg: 'Chỉ KPI "Đã kê khai" mới được gửi duyệt',
      confirmTitle: 'Gửi duyệt KPI',
      confirmMessage: 'Xác nhận gửi duyệt các KPI đã chọn?',
      successMsg: 'Gửi duyệt thành công',
      nextStatus: KpiTrangThaiConst.DA_GUI_CHAM,
      updateAction: updateTrangThaiKpiDonVi,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getKpiDonViKeKhai(query));
      },
    });

  const proposeSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.TAO_MOI || KpiTrangThaiConst.DA_CHINH_SUA],
      invalidMsg: 'Chỉ KPI "Tạo mới" mới được đề xuất',
      confirmTitle: 'Đề xuất KPI cho đơn vị',
      confirmMessage: 'Xác nhận đề xuất các KPI đã chọn?',
      successMsg: 'Đề xuất thành công',
      nextStatus: KpiTrangThaiConst.DE_XUAT,
      updateAction: updateTrangThaiKpiDonVi,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getKpiDonViKeKhai(query));
      },
    });

  const cancelProposeSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.DE_XUAT],
      invalidMsg: 'Chỉ KPI "Đề xuất" mới được hủy đề xuất',
      confirmTitle: 'Hủy đề xuất KPI cho đơn vị',
      confirmMessage: 'Xác nhận hủy đề xuất các KPI đã chọn?',
      successMsg: 'Hủy đề xuất thành công',
      nextStatus: KpiTrangThaiConst.TAO_MOI,
      updateAction: updateTrangThaiKpiDonVi,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getKpiDonViKeKhai(query));
      },
    });


  const updateKetQua = (id: number, value?: number) => {
    setKetQuaMap(prev => ({
      ...prev,
      [id]: value
    }));
  };

  const cancelApproveSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.DA_GUI_CHAM],
      invalidMsg: 'Chỉ KPI "Đã gửi chấm" mới được hủy duyệt',
      confirmTitle: 'Hủy gửi duyệt KPI',
      confirmMessage: 'Xác nhận hủy gửi duyệt các KPI đã chọn?',
      successMsg: 'Hủy duyệt thành công',
      nextStatus: KpiTrangThaiConst.DA_KE_KHAI,
      updateAction: updateTrangThaiKpiDonVi,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getKpiDonViKeKhai(query));
      },
    });

  const handleSaveKetQua = async () => {
    const selectedIds = new Set(selectedRowKeys.map(Number));
    const items = Object.entries(ketQuaMap)
      .filter(([id, v]) =>
        v !== undefined && selectedIds.has(Number(id))
      )
      .map(([id, value]) => ({
        id: Number(id),
        ketQuaThucTe: value
      }));

    if (!items.length) {
      toast.warning('Chọn KPI thay đổi cần lưu');
      return;
    }
    try {
      await dispatch(updateKetQuaThucTeKpiDonVi({ items })).unwrap();
      toast.success('Lưu kết quả thực tế thành công');
      setKetQuaMap({});
      setSelectedRowKeys([]);
      dispatch(getKpiDonViKeKhai(query));
    } catch {
      toast.error('Lưu kết quả thất bại');
    }
  };

  const requiredSelect = (cb: () => void) => {
    if (!selectedRowKeys.length) return toast.warning('Vui lòng chọn ít nhất một KPI');
    cb();
  };


  const bulkActionItems: MenuProps['items'] = [
    ...(canPropose ? [{
      key: 'propose',
      label: 'Đề xuất',
      icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />,
      onClick: () => requiredSelect(proposeSelected)
    }] : []),
    ...(canCancelPropose ? [{
      key: 'cancelPropose',
      label: 'Hủy đề xuất',
      icon: <CheckCircleOutlined style={{ color: 'yellow' }} />,
      onClick: () => requiredSelect(cancelProposeSelected)
    }] : []),
    ...(canSendDeclared ? [{
      key: 'sendScore',
      label: 'Gửi chấm',
      icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />,
      onClick: () => requiredSelect(approveSelected)
    }] : []),
    ...(canCancelDeclared ? [{
      key: 'cancelScore',
      label: 'Hủy gửi chấm',
      icon: <UndoOutlined style={{ color: '#1890ff' }} />,
      onClick: () => requiredSelect(cancelApproveSelected)
    }] : []),
  ];

  const filterContent = (
    <Form form={filterForm} layout="vertical" onValuesChange={(_, values) => { onFilterChange(values); setOpenFilter(false); }}>
      <Form.Item label="Loại KPI" name="loaiKpi">
        <Select allowClear placeholder="Chọn loại KPI" options={KpiLoaiConst.list.map(x => ({ value: x.value, label: x.name }))} />
      </Form.Item>
      <Form.Item name="namHoc" label="Năm học">
        <Select
          allowClear
          placeholder="Chọn năm học"
          loading={namHocStatus === ReduxStatus.LOADING}
          options={namHocDonVi.map(x => ({ value: x.namHoc, label: x.namHoc }))}
        />
      </Form.Item>
      <Form.Item label="Trạng thái" name="trangThai">
        <Select allowClear placeholder="Chọn trạng thái" loading={trangThaiStatus === ReduxStatus.LOADING} options={trangThaiCaNhan} />
      </Form.Item>
    </Form>
  );

  const onClickAdd = () => { setIsModalView(false); setIsModalUpdate(false); setIsModalOpen(true); };
  const onClickUpdate = (record: IViewKpiDonVi) => { dispatch(setSelectedKpiDonVi(record)); setIsModalUpdate(true); setIsModalOpen(true); };
  const onClickView = (record: IViewKpiDonVi) => { dispatch(setSelectedKpiDonVi(record)); setIsModalView(true); setIsModalOpen(true); };
  const onClickAssign = (record: IViewKpiDonVi) => {
    dispatch(setSelectedKpiDonVi(record));
    setOpenAssignModal(true);
  };
  const onClickDelete = (record: IViewKpiDonVi) => {
    Modal.confirm({
      title: `Xóa Kpi ${record.kpi} của ${record.donVi}?`,
      okText: 'Xóa', okType: 'danger', cancelText: 'Hủy',
      onOk: async () => {
        try { await dispatch(deleteKpiDonVi(record.id)).unwrap(); toast.success('Xóa thành công!'); dispatch(getKpiDonViKeKhai(query)); }
        catch { toast.error('Xóa thất bại!'); }
      }
    });
  };

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => onFilterChange({ Keyword: value }), 500);

  const columns: IColumn<KpiTableRow<IViewKpiDonVi>>[] = [
    {
      key: 'kpi', dataIndex: 'kpi', title: 'Tên KPI', width: 400,
      render: (value, record) => {
        if (record.rowType === 'group') {
          return {
            children: (
              <div style={{ fontSize: 17, fontWeight: 600, textAlign: 'center', color: '#0958d9' }}>
                {'KPI ' + KpiLoaiConst.getName(record.loaiKpi)}
              </div>
            ),
            props: { colSpan: columns.length },
          };
        }
        if (record.rowType === 'total') {
          return {
            children: (
              <div style={{ fontSize: 15, fontWeight: 600, textAlign: 'left' }}>
                TỔNG TRỌNG SỐ: <span style={{ color: '#d46b08' }}>{Number(record.trongSo || 0).toFixed(2)}%</span>
              </div>
            ),
            props: { colSpan: columns.length },
          };
        }
        return value;
      },
    },
    { key: 'mucTieu', dataIndex: 'mucTieu', title: 'Mục tiêu', width: 250, render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val), },
    { key: 'trongSo', dataIndex: 'trongSo', title: 'Trọng số', width: 80, render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val) },
    {
      key: 'congThuc', dataIndex: 'congThuc', title: 'Công thức tính', width: 200,
      render: (val, record) => {
        if (record.rowType !== 'data') return { props: { colSpan: 0 } };
        return <span className="text-gray-500">{val}</span>;
      },
    },
    {
      key: 'ketQuaThucTe', dataIndex: 'ketQuaThucTe', title: 'Kết quả thực tế', width: 200,
      render: (val, record) => {
        if (record.rowType !== 'data') return { props: { colSpan: 0 } };
        const value = ketQuaMap[record.id] ?? val;
        return (
          <KetQuaInput loaiKetQua={record.loaiKetQua} value={value} onChange={(v) => updateKetQua(record.id, v)} editable={record.isActive !== 0} />
        );
      },
    },
    {
      key: 'diemKpi', dataIndex: 'diemKpi', title: 'Điểm kê khai', width: 130,
      render: (val, record) => {
        if (record.rowType !== 'data') return { props: { colSpan: 0 } };
        return <span className={record.loaiKpi === 3 ? "text-red-500" : ""}>{record.loaiKpi === 3 && val ? `-${val}` : val}</span>;
      },
    },
    {
      key: 'capTrenDanhGia', dataIndex: 'capTrenDanhGia', title: 'Cấp trên đánh giá', width: 180,
      render: (val, record) => record.rowType !== 'data' ? { props: { colSpan: 0 } } : formatKetQua(val, record.loaiKetQua),
    },
    {
      key: 'diemKpiCapTren', dataIndex: 'diemKpiCapTren', title: 'Điểm cấp trên', width: 130,
      render: (val, record) => {
        if (record.rowType !== 'data') return { props: { colSpan: 0 } };
        return <span className={record.loaiKpi === 3 ? "text-red-500" : ""}>{record.loaiKpi === 3 && val ? `-${val}` : val}</span>;
      },
    },
    {
      key: 'trangThai', dataIndex: 'trangThai', title: 'Trạng thái', width: 150, type: ETableColumnType.STATUS,
      render: (val, record) => record.rowType !== 'data' ? { props: { colSpan: 0 } } : <Tag color={KpiTrangThaiConst.get(val)?.color}>{KpiTrangThaiConst.get(val)?.text}</Tag>,
    },
  ];

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      icon: <EyeOutlined />, command: onClickView,
      hidden: r => r.rowType !== 'data'
    },
    {
      label: 'Sửa',
      icon: <EyeOutlined />, command: onClickUpdate,
      hidden: (r: KpiTableRow<IViewKpiDonVi>) => r.rowType !== 'data'
    },
    ...(canAssign ? [{
      label: 'Giao KPI',
      icon: <RobotFilled />,
      command: onClickAssign,
      hidden: (r: KpiTableRow<IViewKpiDonVi>) => r.rowType !== 'data'
    }] : []),
  ];

  const rowSelection = {
    selectedRowKeys,
    preserveSelectedRowKeys: true,
    getCheckboxProps: (record: any) => ({ disabled: record.rowType !== 'data', style: record.rowType !== 'data' ? { display: 'none' } : {} }),
    onChange: setSelectedRowKeys,
  };

  return (
    <div className="space-y-4">
      <Card
        className="h-full"
        title="Kê khai KPI Đơn vị"
        extra={
          <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd} size="large" className="shadow-md hover:shadow-lg transition-shadow">
            Thêm mới
          </Button>
        }
      >
        <Form form={form} layout="horizontal">
          <div className="flex items-center justify-between mb-6 gap-4">
            <div className="flex items-center gap-2 flex-1">
              <Input placeholder="Tìm KPI..." prefix={<SearchOutlined />} allowClear onChange={(e) => handleDebouncedSearch(e.target.value)} className="max-w-[250px]" />
              <Button icon={<SyncOutlined />} onClick={() => { form.resetFields(); filterForm.resetFields(); onFilterChange({ Keyword: '', loaiKpi: undefined, trangThai: undefined, PageIndex: 1 }); setKetQuaMap({}); setSelectedRowKeys([]); }}>
                Tải lại
              </Button>
            </div>

            <div className="flex items-center gap-2">
              {canSaveScore && (
                <Button
                  icon={<SaveOutlined />}
                  type="primary"
                  onClick={handleSaveKetQua}
                >
                  Lưu kết quả
                </Button>
              )}
              <Dropdown menu={{ items: bulkActionItems }} trigger={['click']} disabled={selectedRowKeys.length === 0}>
                <Button size="large" type={selectedRowKeys.length > 0 ? 'primary' : 'default'} icon={<EllipsisOutlined />} className={selectedRowKeys.length > 0 ? "shadow-md hover:shadow-lg transition-shadow" : ""}>
                  Thao tác {selectedRowKeys.length > 0 && ` (${selectedRowKeys.length})`}
                </Button>
              </Dropdown>
              <Popover content={filterContent} title="Bộ lọc" trigger="click" open={openFilter} onOpenChange={setOpenFilter} placement="bottomRight" styles={{ body: { padding: 16, minWidth: 280 } }}>
                <Button size="large" icon={<FilterOutlined />} type={openFilter ? "primary" : "default"}> Bộ lọc </Button>
              </Popover>
            </div>
          </div>
        </Form>

        <div className="kpi-table-wrapper">
          <AppTable
            loading={status === ReduxStatus.LOADING}
            rowKey="id"
            columns={columns}
            dataSource={tableData}
            isGroupedTable={true}
            listActions={actions}
            pagination={false}
            rowSelection={{ ...rowSelection, fixed: 'left' }}
            scroll={{ x: 'max-content', y: 'calc(100vh - 400px)' }}
            footer={() => (
              <div className="bg-gradient-to-r from-gray-50 to-blue-50 p-4 rounded-lg border border-gray-200">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div className="flex flex-col items-center justify-center">
                    <span className="text-sm text-gray-600 mb-1">Tổng điểm kê khai</span>
                    <span className="text-2xl font-bold text-orange-600">
                      {summary?.tongTuDanhGia?.toFixed(2) ?? 0}
                    </span>
                  </div>

                  <div className="flex flex-col items-center justify-center border-l border-gray-300 pl-4">
                    <span className="text-sm text-gray-600 mb-1">Điểm cấp trên</span>
                    <span className="text-2xl font-bold text-green-600">
                      {summary?.tongCapTren?.toFixed(2) ?? 0}
                    </span>
                  </div>
                </div>
              </div>
            )}
          />
        </div>
        <PositionModal isModalOpen={isModalOpen} isUpdate={isUpdate} isView={isView} setIsModalOpen={setIsModalOpen} onSuccess={() => { dispatch(getKpiDonViKeKhai(query)); dispatch(getListTrangThaiKpiDonVi()); }} />
        <AssignKpiModal open={openAssignModal} onClose={() => setOpenAssignModal(false)} kpiId={selectedKpiDonVi?.id ?? undefined} donViId={selectedKpiDonVi?.data?.idDonVi ?? undefined} />
      </Card>
    </div>
  );
};
export default withAuthGuard(Page, PermissionCoreConst.CoreMenuKpiManageUnit);