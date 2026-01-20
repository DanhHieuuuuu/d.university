'use client';
import { useEffect, useMemo, useState } from 'react';
import { Button, Card, Dropdown, Form, Input, MenuProps, Modal, Popover, Select, Tag } from 'antd';
import {
  PlusOutlined, SearchOutlined, SyncOutlined, EditOutlined, DeleteOutlined,
  EyeOutlined, FilterOutlined, CheckCircleOutlined, EllipsisOutlined, SaveOutlined, UndoOutlined,
  FileTextOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiTruong } from '@redux/feature/kpi/kpiSlice';
import { deleteKpiTruong, getAllKpiTruong, getListKpiRoleByUser, getListTrangThaiKpiCaNhan, getListTrangThaiKpiTruong, updateKetQuaThucTeKpiTruong, updateTrangThaiKpiTruong } from '@redux/feature/kpi/kpiThunk';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { toast } from 'react-toastify';
import { getAllPhongBan } from '@redux/feature/danh-muc/danhmucThunk';
import { getAllUser } from '@redux/feature/userSlice';
import { buildKpiGroupedTable, KpiTableRow } from '@helpers/kpi/kpi.helper';
import KetQuaInput from '@components/bthanh-custom/kpiTableInput';
import { useKpiStatusAction } from '@hooks/kpi/UpdateStatusKPI';
import { formatKetQua } from '@helpers/kpi/formatResult.helper';
import { ETableColumnType } from '@/constants/e-table.consts';
import '@styles/kpi/table.kpi.scss'
import { IQueryKpiTruong, IViewKpiTruong } from '@models/kpi/kpi-truong.model';
import { KPI_ORDER, KpiLoaiConst } from '@/constants/kpi/kpiType.const';
import { KpiTrangThaiConst } from '@/constants/kpi/kpiStatus.const';
import PositionModal from './(dialog)/create-or-update copy';
import KpiAiChat from '@components/bthanh-custom/kpiChatAssist';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { useIsGranted } from '@hooks/useIsGranted';
import { withAuthGuard } from '@src/hoc/withAuthGuard';

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const { processUpdateStatus } = useKpiStatusAction();
  const { data: list, status, total: totalItem, summary } = useAppSelector((state) => state.kpiState.kpiTruong.$list);
  const { data: trangThaiTruong, status: trangThaiStatus } = useAppSelector((state) => state.kpiState.meta.trangThai.truong);

  const canSendDeclared= useIsGranted(PermissionCoreConst.CoreMenuKpiManageSchoolActionSendDeclared);
  const canCancelDeclared = useIsGranted(PermissionCoreConst.CoreMenuKpiManageSchoolActionCancelDeclared);
  const canSaveScore = useIsGranted(PermissionCoreConst.CoreMenuKpiManageSchoolActionSaveScore);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isUpdate, setIsModalUpdate] = useState(false);
  const [isView, setIsModalView] = useState(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);
  const [ketQuaMap, setKetQuaMap] = useState<Record<number, number | undefined>>({});

  const tableData = useMemo(() => {
    const sortedList = [...(list || [])].sort(
      (a, b) =>
        KPI_ORDER.indexOf(a.loaiKpi) -
        KPI_ORDER.indexOf(b.loaiKpi)
    );

    return buildKpiGroupedTable<IViewKpiTruong>(sortedList);
  }, [list]);

  const { query, onFilterChange } = usePaginationWithFilter<IQueryKpiTruong>({
    total: totalItem || 0,
    initialQuery: { PageIndex: 1, PageSize: 1000, Keyword: '' },
    onQueryChange: (newQuery) => dispatch(getAllKpiTruong(newQuery)),
    triggerFirstLoad: true
  });

  useEffect(() => {
    dispatch(getAllPhongBan({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getAllUser({ PageIndex: 1, PageSize: 2000 }));
    dispatch(getListTrangThaiKpiCaNhan());
    dispatch(getListKpiRoleByUser());
  }, [dispatch]);


  const approveSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.DA_KE_KHAI],
      invalidMsg: 'Chỉ KPI "Đã kê khai" mới được gửi duyệt',
      confirmTitle: 'Gửi duyệt KPI',
      confirmMessage: 'Xác nhận gửi duyệt các KPI đã chọn?',
      successMsg: 'Gửi duyệt thành công',
      nextStatus: KpiTrangThaiConst.DA_GUI_CHAM,
      updateAction: updateTrangThaiKpiTruong,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiTruong(query));
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
      updateAction: updateTrangThaiKpiTruong,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiTruong(query));
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
      await dispatch(updateKetQuaThucTeKpiTruong({ items })).unwrap();
      toast.success('Lưu kết quả thực tế thành công');
      setKetQuaMap({});
      setSelectedRowKeys([]);
      dispatch(getAllKpiTruong(query));
    } catch {
      toast.error('Lưu kết quả thất bại');
    }
  };

  const requiredSelect = (cb: () => void) => {
    if (!selectedRowKeys.length) return toast.warning('Vui lòng chọn ít nhất một KPI');
    cb();
  };
  const bulkActionItems: MenuProps['items'] = [
    ...(canSendDeclared ? [{ 
      key: 'send-approve', 
      label: 'Gửi duyệt', 
      icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />, 
      onClick: () => requiredSelect(approveSelected) 
    }] : []),
    ...(canCancelDeclared ? [{ 
      key: 'cancel-send-approve', 
      label: 'Hủy duyệt', 
      icon: <UndoOutlined style={{ color: '#1890ff' }} />, 
      onClick: () => requiredSelect(cancelApproveSelected) 
    }] : []),
  ];

  const filterContent = (
    <Form form={filterForm} layout="vertical" onValuesChange={(_, values) => { onFilterChange(values); setOpenFilter(false); }}>
      <Form.Item label="Loại KPI" name="loaiKpi">
        <Select allowClear placeholder="Chọn loại KPI" options={KpiLoaiConst.list.map(x => ({ value: x.value, label: x.name }))} />
      </Form.Item>
      <Form.Item label="Trạng thái" name="trangThai">
        <Select allowClear placeholder="Chọn trạng thái" loading={trangThaiStatus === ReduxStatus.LOADING} options={trangThaiTruong} />
      </Form.Item>
    </Form>
  );

  const onClickAdd = () => { setIsModalView(false); setIsModalUpdate(false); setIsModalOpen(true); };
  const onClickUpdate = (record: IViewKpiTruong) => { dispatch(setSelectedKpiTruong(record)); setIsModalUpdate(true); setIsModalOpen(true); };
  const onClickView = (record: IViewKpiTruong) => { dispatch(setSelectedKpiTruong(record)); setIsModalView(true); setIsModalOpen(true); };
  const onClickDelete = (record: IViewKpiTruong) => {
    Modal.confirm({
      title: `Xóa Kpi ${record.kpi}?`,
      okText: 'Xóa', okType: 'danger', cancelText: 'Hủy',
      onOk: async () => {
        try { await dispatch(deleteKpiTruong(record.id)).unwrap(); toast.success('Xóa thành công!'); dispatch(getAllKpiTruong(query)); }
        catch { toast.error('Xóa thất bại!'); }
      }
    });
  };

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => onFilterChange({ Keyword: value }), 500);

  const columns: IColumn<KpiTableRow<IViewKpiTruong>>[] = [
    {
      key: 'kpi', dataIndex: 'kpi', title: 'Tên KPI', width: 400,
      render: (value, record) => {
        if (record.rowType === 'group') {
          return {
            children: (
              <div style={{
                fontSize: 17,
                fontWeight: 600,
                textAlign: 'center',
                color: '#0958d9',
              }}>
                {'KPI ' + KpiLoaiConst.getName(record.loaiKpi)}
              </div>
            ),
            props: { colSpan: columns.length },
          };
        }

        if (record.rowType === 'total') {
          return {
            children: (
              <div style={{
                fontSize: 15,
                fontWeight: 600,
                textAlign: 'left',
              }}>
                TỔNG TRỌNG SỐ: <span style={{ color: '#d46b08' }}>{record.trongSo}%</span>
              </div>
            ),
            props: { colSpan: columns.length },
          };
        }

        return value;
      },
    },
    {
      key: 'mucTieu',
      dataIndex: 'mucTieu',
      title: 'Mục tiêu',
      width: 250,
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val),
    },
    {
      key: 'trongSo',
      dataIndex: 'trongSo',
      title: 'Trọng số',
      width: 80,
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val)
    },
    {
      key: 'congThuc',
      dataIndex: 'congThuc',
      title: 'Công thức tính',
      width: 200,
      render: (val, record) => {
        if (record.rowType !== 'data') return { props: { colSpan: 0 } };
        return <span className="text-gray-500">{val}</span>;
      },
    },
    {
      key: 'ketQuaThucTe',
      dataIndex: 'ketQuaThucTe',
      title: 'Kết quả thực tế',
      width: 200,
      render: (val, record) => {
        if (record.rowType !== 'data') {
          return { props: { colSpan: 0 } };
        }

        const value = ketQuaMap[record.id] ?? val;

        return (
          <KetQuaInput
            loaiKetQua={record.loaiKetQua}
            value={value}
            onChange={(v) => updateKetQua(record.id, v)}
            editable={record.isActive !== 0}
          />
        );
      },
    },
    {
      key: 'diemKpi',
      dataIndex: 'diemKpi',
      title: 'Điểm kê khai',
      width: 130,
      render: (val, record) => {
        if (record.rowType !== 'data') return { props: { colSpan: 0 } };
        return <span className={record.loaiKpi === 3 ? "text-red-500" : ""}>{record.loaiKpi === 3 && val ? `-${val}` : val}</span>;
      },
    },
    {
      key: 'capTrenDanhGia',
      dataIndex: 'capTrenDanhGia',
      title: 'Cấp trên đánh giá',
      width: 180,
      render: (val, record) =>
        record.rowType !== 'data'
          ? { props: { colSpan: 0 } }
          : formatKetQua(val, record.loaiKetQua),
    },
    {
      key: 'diemKpiCapTren',
      dataIndex: 'diemKpiCapTren',
      title: 'Điểm cấp trên',
      width: 130,
      render: (val, record) => {
        if (record.rowType !== 'data') return { props: { colSpan: 0 } };
        return <span className={record.loaiKpi === 3 ? "text-red-500" : ""}>{record.loaiKpi === 3 && val ? `-${val}` : val}</span>;
      },
    },

    {
      key: 'trangThai',
      dataIndex: 'trangThai', title: 'Trạng thái',
      width: 150,
      type: ETableColumnType.STATUS,
      render: (val, record) =>
        record.rowType !== 'data'
          ? { props: { colSpan: 0 } }
          : <Tag color={KpiTrangThaiConst.get(val)?.color}>{KpiTrangThaiConst.get(val)?.text}</Tag>,
    },
  ];

  const actions: IAction[] = [
    { label: 'Chi tiết', icon: <EyeOutlined />, command: onClickView, hidden: r => r.rowType !== 'data' },
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
        title="Kê khai KPI Trường"
        extra={
          <Button
            type="primary"
            icon={<PlusOutlined />}
            onClick={onClickAdd}
            className="shadow-md hover:shadow-lg transition-shadow"
          >
            Thêm mới
          </Button>
        }
      >
        <Form form={form} layout="horizontal">
          <div className="flex items-center justify-between mb-6 gap-4">
            <div className="flex items-center gap-2 flex-1">
              <Input
                placeholder="Tìm KPI..."
                prefix={<SearchOutlined />}
                allowClear
                onChange={(e) => handleDebouncedSearch(e.target.value)}
                className="max-w-[250px]"
              />
              <Button
                icon={<SyncOutlined />}
                onClick={() => {
                  form.resetFields();
                  filterForm.resetFields();
                  onFilterChange({ Keyword: '', loaiKpi: undefined, trangThai: undefined });
                  setKetQuaMap({});
                  setSelectedRowKeys([]);
                }}
              >
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
              <Dropdown
                menu={{ items: bulkActionItems }}
                trigger={['click']}
                disabled={selectedRowKeys.length === 0}
              >
                <Button
                  size="large"
                  type={selectedRowKeys.length > 0 ? 'primary' : 'default'}
                  icon={<EllipsisOutlined />}
                  className={selectedRowKeys.length > 0 ? "shadow-md hover:shadow-lg transition-shadow" : ""}
                >
                  Thao tác
                  {selectedRowKeys.length > 0 && ` (${selectedRowKeys.length})`}
                </Button>
              </Dropdown>

              <Popover
                content={filterContent}
                title="Bộ lọc"
                trigger="click"
                open={openFilter}
                onOpenChange={setOpenFilter}
                placement="bottomRight"
                styles={{ body: { padding: 16, minWidth: 280 } }}
              >
                <Button
                  size="large"
                  icon={<FilterOutlined />}
                  type={openFilter ? "primary" : "default"}
                >
                  Bộ lọc
                </Button>
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
            listActions={actions}
            pagination={false}
            rowSelection={{
              ...rowSelection,
              fixed: 'left',
            }}
            scroll={{ x: 'max-content', y: 'calc(100vh - 520px)' }}
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
        <PositionModal isModalOpen={isModalOpen} isUpdate={isUpdate} isView={isView} setIsModalOpen={setIsModalOpen} onSuccess={() => { dispatch(getAllKpiTruong(query)); dispatch(getListTrangThaiKpiTruong()); }} />
        <KpiAiChat />
      </Card>
    </div>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuKpiManageSchool);