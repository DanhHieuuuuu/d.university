'use client';
import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Dropdown, Form, Input, MenuProps, Modal, Popover, Select, Tabs, Tag } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  CheckCircleOutlined,
  EllipsisOutlined,
  FilterOutlined,
  UndoOutlined,
  SaveOutlined,
  CloseCircleOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiTruong } from '@redux/feature/kpi/kpiSlice';
import {
  deleteKpiTruong,
  getAllKpiTruong,
  getKpiLogStatus,
  getListNamHocKpiTruong,
  getListTrangThaiKpiTruong,
  updateKetQuaCapTrenKpiTruong,
  updateTrangThaiKpiTruong
} from '@redux/feature/kpi/kpiThunk';
import AppTable from '@components/common/Table';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryKpiTruong, IViewKpiTruong } from '@models/kpi/kpi-truong.model';
import PositionModal from './(dialog)/create-or-update';
import { toast } from 'react-toastify';
import { useKpiStatusAction } from '@hooks/kpi/UpdateStatusKPI';
import { formatKetQua } from '@helpers/kpi/formatResult.helper';
import KetQuaInput from '@components/bthanh-custom/kpiTableInput';
import ConfirmScoredModal from '../../modal/ConfirmScoredModal';
import { KpiTrangThaiConst } from '@/constants/kpi/kpiStatus.const';
import { KpiLoaiConst } from '@/constants/kpi/kpiType.const';
import KpiAiChat from '@components/bthanh-custom/kpiChatAssist';
import { ETableColumnType } from '@/constants/e-table.consts';
import KpiLogModal from '../../modal/KpiLogModal';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { useIsGranted } from '@hooks/useIsGranted';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { approveRequestAction } from '@redux/feature/survey/surveyThunk';

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const { processUpdateStatus } = useKpiStatusAction();

  const canScore = useIsGranted(PermissionCoreConst.CoreMenuKpiListSchoolActionScore);
  const canSyncScore = useIsGranted(PermissionCoreConst.CoreMenuKpiListSchoolActionSyncScore);
  const canPrincipalApprove = useIsGranted(PermissionCoreConst.CoreMenuKpiListSchoolActionPrincipalApprove);
  const canSaveScore = useIsGranted(PermissionCoreConst.CoreMenuKpiListSchoolActionSaveScore);
  const canUpdate = useIsGranted(PermissionCoreConst.CoreMenuKpiListSchoolUpdate);
  const canDelete = useIsGranted(PermissionCoreConst.CoreMenuKpiListSchoolDelete);

  const { data: list, status, total: totalItem, summary } = useAppSelector((state) => state.kpiState.kpiTruong.$list);
  const { data: trangThaiTruong, status: trangThaiStatus } = useAppSelector(
    (state) => state.kpiState.meta.trangThai.truong
  );
  const { data: namHocTruong, status: namHocStatus } = useAppSelector((state) => state.kpiState.meta.namHoc.truong);

  type ModalMode = 'create' | 'update' | 'view' | null;
  const [modalMode, setModalMode] = useState<ModalMode>(null);
  const [openChamModal, setOpenChamModal] = useState(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);
  const [ketQuaCapTrenMap, setKetQuaCapTrenMap] = useState<Record<number, number | undefined>>({});
  const [activeLoaiKpi, setActiveLoaiKpi] = useState<number>(KpiLoaiConst.CHUC_NANG);
  const [openLogModal, setOpenLogModal] = useState(false);
  const [selectedKpiLogId, setSelectedKpiLogId] = useState<number | null>(null);
  const { data: logData, status: logStatus } = useAppSelector((state) => state.kpiState.kpiLog.$list);

  const KPI_TABS = KpiLoaiConst.list.map((x) => ({
    key: String(x.value),
    label: `KPI ${x.name}`
  }));

  const STATUS_ALLOW_EDIT = [
    KpiTrangThaiConst.DA_GUI_CHAM,

  ];

  useEffect(() => {
    dispatch(getListTrangThaiKpiTruong());
    dispatch(getListNamHocKpiTruong());
  }, [dispatch]);

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryKpiTruong>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: '',
      loaiKpi: KpiLoaiConst.CHUC_NANG
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllKpiTruong(newQuery));
    },
    triggerFirstLoad: true
  });

  const requiredSelect = (cb: () => void) => {
    if (!selectedRowKeys.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
      return;
    }
    cb();
  };

  const scoreSelected = () => {
    if (!selectedRowKeys.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
      return;
    }
    const validItems = list.filter(
      (kpi) => selectedRowKeys.includes(kpi.id) && kpi.trangThai == KpiTrangThaiConst.DA_GUI_CHAM
    );
    if (!validItems.length) {
      toast.warning('Chỉ KPI đang ở trạng thái "Đã gửi chấm" mới được chấm');
      return;
    }
    setOpenChamModal(true);
  };

  const handleSubmitScore = async (note?: string) => {
    try {
      await dispatch(
        updateTrangThaiKpiTruong({ ids: selectedRowKeys.map(Number), trangThai: KpiTrangThaiConst.DA_CHAM, note })
      ).unwrap();
      toast.success('Chấm KPI thành công');
      setOpenChamModal(false);
      setSelectedRowKeys([]);
      dispatch(getAllKpiTruong(query));
    } catch {
      toast.error('Chấm KPI thất bại');
    }
  };

  const cancelScoredSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.DA_CHAM],
      invalidMsg: 'Chỉ KPI "Đã chấm" mới được hủy',
      confirmTitle: 'Hủy gửi chấm KPI',
      confirmMessage: 'Xác nhận hủy chấm các KPI đã chọn?',
      successMsg: 'Hủy kết quả chấm thành công',
      nextStatus: KpiTrangThaiConst.DA_GUI_CHAM,
      updateAction: updateTrangThaiKpiTruong,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiTruong(query));
      }
    });

  const principalApprovedSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.DA_CHAM],
      invalidMsg: 'Chỉ có KPI đang ở trạng thái "Đã chấm" mới được phê duyệt.',
      confirmTitle: 'Phê duyệt kết quả chấm',
      confirmMessage: 'Xác nhận phê duyệt các KPI đã chọn?',
      successMsg: 'Phê duyệt thành công',
      nextStatus: KpiTrangThaiConst.HIEU_TRUONG_PHE_DUYET,
      updateAction: updateTrangThaiKpiTruong,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiTruong(query));
      }
    });

  const cancelPrincipalApprovedSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.HIEU_TRUONG_PHE_DUYET],
      invalidMsg: 'Chỉ KPI "Đã phê duyệt kết quả chấm" mới được hủy',
      confirmTitle: 'Hủy phê duyệt kết quả chấm',
      confirmMessage: 'Hủy phê duyệt các KPI đã chọn?',
      successMsg: 'Hủy phê duyệt thành công',
      nextStatus: KpiTrangThaiConst.DA_CHAM,
      updateAction: updateTrangThaiKpiTruong,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiTruong(query));
      }
    });

  const approveSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.DE_XUAT],
      invalidMsg: 'Chỉ KPI "Đề xuất" mới được phê duyệt',
      confirmTitle: 'Phê duyệt',
      confirmMessage: 'Phê duyệt các KPI đã chọn?',
      successMsg: 'Phê duyệt thành công',
      nextStatus: KpiTrangThaiConst.DUOC_GIAO,
      updateAction: updateTrangThaiKpiTruong,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiTruong(query));
      }
    });

  const rejectSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.DE_XUAT],
      invalidMsg: 'Chỉ KPI "Đề xuất" mới có thể từ chối',
      confirmTitle: 'Từ chối đề xuất',
      confirmMessage: 'Xác nhận từ chối các KPI đã chọn?',
      successMsg: 'Từ chối thành công',
      nextStatus: KpiTrangThaiConst.TU_CHOI,
      updateAction: updateTrangThaiKpiTruong,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiTruong(query));
      }
    });
  const updateKetQuaCapTren = (id: number, value?: number) => {
    setKetQuaCapTrenMap((prev) => ({ ...prev, [id]: value }));
  };

  const syncKetQuaThucTeToCapTren = () => {
    if (!selectedRowKeys.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
      return;
    }
    const newMap = { ...ketQuaCapTrenMap };
    list
      .filter((item) => selectedRowKeys.includes(item.id))
      .forEach((item) => {
        if (item.ketQuaThucTe !== null && item.ketQuaThucTe !== undefined) {
          newMap[item.id] = item.ketQuaThucTe;
        }
      });
    setKetQuaCapTrenMap(newMap);
    toast.success('Đã đồng bộ kết quả thực tế');
  };

  const handleSaveKetQuaCapTren = async () => {
    const selectedIds = new Set(selectedRowKeys.map(Number));
    const items = Object.entries(ketQuaCapTrenMap)
      .filter(([id, v]) => v !== undefined && selectedIds.has(Number(id)))
      .map(([id, value]) => ({ id: Number(id), KetQuaCapTren: value }));

    if (!items.length) {
      toast.warning('Chọn KPI đánh giá đã thay đổi cần lưu');
      return;
    }
    try {
      await dispatch(updateKetQuaCapTrenKpiTruong({ items })).unwrap();
      toast.success('Lưu kết quả đánh giá thành công');
      setSelectedRowKeys([]);
      setKetQuaCapTrenMap({});
      dispatch(getAllKpiTruong(query));
    } catch {
      toast.error('Lưu kết quả thất bại');
    }
  };

  const bulkActionItems: MenuProps['items'] = [
    ...(canScore
      ? [
        {
          key: 'approve',
          label: 'Phê duyệt đề xuất',
          icon: <EditOutlined style={{ color: '#1890ff' }} />,
          onClick: () => requiredSelect(approveSelected)
        }
      ]
      : []),
    ...(canScore
      ? [
        {
          key: 'reject',
          label: 'Từ chối đề xuất',
          icon: <CloseCircleOutlined style={{ color: '#ff4d4f' }} />,
          onClick: () => requiredSelect(rejectSelected)
        }
      ]
      : []),
    ...(canScore
      ? [
        {
          key: 'score',
          label: 'Chấm KPI',
          icon: <EditOutlined style={{ color: '#1890ff' }} />,
          onClick: () => requiredSelect(scoreSelected)
        }
      ]
      : []),
    ...(canScore
      ? [
        {
          key: 'cancelScore',
          label: 'Hủy kết quả chấm KPI',
          icon: <UndoOutlined style={{ color: '#1890ff' }} />,
          onClick: () => requiredSelect(cancelScoredSelected)
        }
      ]
      : []),
    ...(canSyncScore
      ? [
        {
          key: 'syncKetQua',
          label: 'Đồng bộ kết quả thực tế',
          icon: <SyncOutlined style={{ color: '#1890ff' }} />,
          onClick: () => requiredSelect(syncKetQuaThucTeToCapTren)
        }
      ]
      : []),
    ...(canPrincipalApprove
      ? [
        {
          key: 'principalApprove',
          label: 'Phê duyệt kết quả chấm',
          icon: <EditOutlined style={{ color: '#00ff1a6b' }} />,
          onClick: () => requiredSelect(principalApprovedSelected)
        }
      ]
      : []),
    ...(canPrincipalApprove
      ? [
        {
          key: 'cancelPrincipalApprove',
          label: 'Hủy duyệt kết quả chấm',
          icon: <UndoOutlined style={{ color: '#00ff1a6b' }} />,
          onClick: () => requiredSelect(cancelPrincipalApprovedSelected)
        }
      ]
      : [])
  ];

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const filterContent = (
    <Form
      form={filterForm}
      layout="vertical"
      onValuesChange={(_, values) => {
        onFilterChange(values);
        setOpenFilter(false);
      }}
    >
      <Form.Item label="Loại KPI" name="loaiKpi">
        <Select
          allowClear
          placeholder="Chọn loại KPI"
          options={KpiLoaiConst.list.map((x) => ({ value: x.value, label: x.name }))}
        />
      </Form.Item>
      <Form.Item name="namHoc" label="Năm học">
        <Select
          allowClear
          placeholder="Chọn năm học"
          loading={namHocStatus === ReduxStatus.LOADING}
          options={namHocTruong.map((x: any) => ({ value: x.namHoc, label: x.namHoc }))}
        />
      </Form.Item>
      <Form.Item label="Trạng thái" name="trangThai">
        <Select
          allowClear
          placeholder="Chọn trạng thái"
          loading={trangThaiStatus === ReduxStatus.LOADING}
          options={trangThaiTruong}
        />
      </Form.Item>
      <div className="mt-2 flex justify-end gap-2">
        <Button
          size="small"
          onClick={() => {
            filterForm.resetFields();
            onFilterChange({ namHoc: undefined, loaiKpi: undefined, trangThai: undefined });
            setOpenFilter(false);
          }}
        >
          Reset
        </Button>
      </div>
    </Form>
  );

  const onClickAdd = () => setModalMode('create');
  const onClickUpdate = (record: IViewKpiTruong) => {
    dispatch(setSelectedKpiTruong(record));
    setModalMode('update');
  };
  const onClickView = (record: IViewKpiTruong) => {
    dispatch(setSelectedKpiTruong(record));
    setModalMode('view');
  };
  const onClickDelete = (record: IViewKpiTruong) => {
    Modal.confirm({
      title: `Xóa Kpi ${record.kpi} ?`,
      okText: 'Xóa',
      okType: 'danger',
      cancelText: 'Hủy',
      onOk: async () => {
        try {
          await dispatch(deleteKpiTruong(record.id)).unwrap();
          toast.success('Xóa thành công!');
          dispatch(getAllKpiTruong(query));
        } catch (error: any) {
          toast.error(error?.response?.message || 'Xóa thất bại!');
        }
      }
    });
  };

  const onClickViewLog = (record: IViewKpiTruong) => {
    setSelectedKpiLogId(record.id);
    setOpenLogModal(true);
    dispatch(getKpiLogStatus({ kpiId: record.id, capKpi: 3, PageIndex: 1, PageSize: 50 })); // CapKpi = 3 cho cấp Trường
  };

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      icon: <EyeOutlined />,
      command: onClickView
    },
    {
      label: 'Sửa',
      color: 'blue',
      hidden: () => !canUpdate,
      icon: <EditOutlined />,
      command: onClickUpdate
    },
    {
      label: 'Xóa',
      color: 'red',
      hidden: () => !canDelete,
      icon: <DeleteOutlined />,
      command: onClickDelete
    },
    {
      label: 'Nhật ký trạng thái',
      icon: <EyeOutlined />,
      color: 'orange',
      command: onClickViewLog
    }
  ];

  const columns: IColumn<IViewKpiTruong>[] = [
    { key: 'linhVuc', dataIndex: 'linhVuc', title: 'Lĩnh Vực', width: 150 },
    { key: 'chienLuoc', dataIndex: 'chienLuoc', title: 'Mục tiêu chiến lược', width: 150 },
    { key: 'kpi', dataIndex: 'kpi', title: 'Tên KPI', width: 400 },
    { key: 'mucTieu', dataIndex: 'mucTieu', title: 'Mục tiêu', width: 100 },
    { key: 'trongSo', dataIndex: 'trongSo', title: 'Trọng số', width: 100 },
    { key: 'congThuc', dataIndex: 'congThuc', title: 'Công thức tính', width: 200 },
    {
      key: 'loaiKpi',
      dataIndex: 'loaiKpi',
      title: 'Loại KPI',
      width: 140,
      render: (value: number) => KpiLoaiConst.getName(value)
    },
    {
      key: 'ketQuaThucTe',
      dataIndex: 'ketQuaThucTe',
      title: 'Kết quả thực tế',
      width: 180,
      render: (val, record) => formatKetQua(val, record.loaiKetQua)
    },
    {
      key: 'diemKpi',
      dataIndex: 'diemKpi',
      title: 'Điểm tự đánh giá',
      width: 130,
      align: 'center',
      render: (val, record) => (
        <span className={record.loaiKpi === 3 ? 'text-red-500' : ''}>
          {record.loaiKpi === 3 && val ? `-${val}` : val}
        </span>
      )
    },
    {
      key: 'capTrenDanhGia',
      dataIndex: 'capTrenDanhGia',
      title: 'Cấp trên đánh giá',
      width: 200,
      render: (val, record) => {
        const value = ketQuaCapTrenMap[record.id] ?? val;
        return (
          <KetQuaInput
            loaiKetQua={record.loaiKetQua}
            value={value}
            onChange={(v) => updateKetQuaCapTren(record.id, v)}
            editable={record.isActive !== 0 || record.trangThai == KpiTrangThaiConst.DA_GUI_CHAM}
          />
        );
      }
    },
    {
      key: 'diemKpiCapTren',
      dataIndex: 'diemKpiCapTren',
      title: 'Điểm cấp trên',
      width: 130,
      align: 'center',
      render: (val, record) => (
        <span className={record.loaiKpi === 3 ? 'text-red-500' : ''}>
          {record.loaiKpi === 3 && val ? `-${val}` : val}
        </span>
      )
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
      type: ETableColumnType.STATUS,
      render: (val) => <Tag color={KpiTrangThaiConst.get(val)?.color}>{KpiTrangThaiConst.get(val)?.text}</Tag>
    }
  ];

  const rowSelection = {
    selectedRowKeys,
    preserveSelectedRowKeys: true,
    onChange: setSelectedRowKeys,
    selections: [
      {
        key: 'current-page',
        text: 'Chọn trang hiện tại',
        onSelect: (changableRowKeys: React.Key[]) => {
          setSelectedRowKeys(changableRowKeys);
        }
      },
      { key: 'all-pages', text: 'Chọn tất cả các trang', onSelect: () => { } } // Logic chọn all trang cần gọi API getIds
    ]
  };

  return (
    <Card
      title="Danh sách KPI Trường"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          {' '}
          Thêm mới{' '}
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="mb-4 flex items-center justify-between gap-4">
          <div className="flex flex-1 items-center gap-2">
            <Form.Item name="Keyword" noStyle>
              <Input
                placeholder="Tìm KPI..."
                prefix={<SearchOutlined />}
                allowClear
                onChange={(e) => handleDebouncedSearch(e.target.value)}
                className="max-w-[250px]"
              />
            </Form.Item>
            <Button
              color="default"
              variant="filled"
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                filterForm.resetFields();
                onFilterChange({
                  Keyword: '',
                  loaiKpi: activeLoaiKpi,
                  trangThai: undefined,
                  namHoc: undefined,
                  PageIndex: 1
                });
                resetFilter();
                setSelectedRowKeys([]);
              }}
            >
              {' '}
              Tải lại{' '}
            </Button>
          </div>
          <div className="flex items-center gap-2">
            {canSaveScore && (
              <Button icon={<SaveOutlined />} type="primary" onClick={handleSaveKetQuaCapTren}>
                Lưu kết quả
              </Button>
            )}
            <Dropdown menu={{ items: bulkActionItems }} trigger={['click']} disabled={selectedRowKeys.length === 0}>
              <Button type={selectedRowKeys.length > 0 ? 'primary' : 'default'} icon={<EllipsisOutlined />}>
                {' '}
                Thao tác {selectedRowKeys.length > 0 && ` (${selectedRowKeys.length})`}{' '}
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
              <Button icon={<FilterOutlined />} type={openFilter ? 'primary' : 'default'}>
                {' '}
                Bộ lọc{' '}
              </Button>
            </Popover>
          </div>
        </div>
      </Form>

      <Tabs
        activeKey={String(activeLoaiKpi)}
        items={KPI_TABS}
        onChange={(key) => {
          const loai = Number(key);
          setActiveLoaiKpi(loai);
          setSelectedRowKeys([]);
          setKetQuaCapTrenMap({});
          onFilterChange({ loaiKpi: loai, PageIndex: 1 });
        }}
      />
      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
        rowSelection={{ ...rowSelection, fixed: 'left' }}
        scroll={{ x: 'max-content', y: 'calc(96vh - 400px)' }}
        footer={() => (
          <div className="flex justify-end gap-8">
            <div>
              <span className="font-medium">Tổng điểm tự đánh giá:</span>{' '}
              <span className="font-semibold text-blue-600">{summary?.tongTuDanhGia.toFixed(2)}</span>
            </div>

            <div>
              <span className="font-medium">Tổng điểm cấp trên:</span>{' '}
              <span className="font-semibold text-green-600">{summary?.tongCapTren.toFixed(2)}</span>
            </div>
          </div>
        )}
      />

      <PositionModal
        isModalOpen={!!modalMode}
        isUpdate={modalMode === 'update'}
        isView={modalMode === 'view'}
        setIsModalOpen={() => setModalMode(null)}
        onSuccess={() => {
          dispatch(getAllKpiTruong(query));
          dispatch(getListTrangThaiKpiTruong());
          dispatch(getListNamHocKpiTruong());
        }}
      />
      <ConfirmScoredModal
        open={openChamModal}
        title="Chấm KPI Trường"
        trangThai={KpiTrangThaiConst.DA_CHAM}
        onCancel={() => setOpenChamModal(false)}
        onSubmit={handleSubmitScore}
      />
      <KpiLogModal
        open={openLogModal}
        onCancel={() => setOpenLogModal(false)}
        data={logData}
        loading={logStatus === ReduxStatus.LOADING}
      />
      {/* <KpiAiChat /> */}
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuKpiListSchool);
