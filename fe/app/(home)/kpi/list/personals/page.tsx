'use client';
import { useEffect, useState } from 'react';
import { Tabs, Button, Card, Dropdown, Form, Input, MenuProps, Modal, Popover, Select, Tag } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  FilterOutlined,
  EllipsisOutlined,
  UndoOutlined,
  SaveOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiCaNhan } from '@redux/feature/kpi/kpiSlice';
import {
  deleteKpiCaNhan,
  getAllIdsKpiCaNhan,
  getAllKpiCaNhan,
  getKpiLogStatus,
  getListKpiCongThuc,
  getListTrangThaiKpiCaNhan,
  updateKetQuaCapTrenKpiCaNhan,
  updateTrangThaiKpiCaNhan
} from '@redux/feature/kpi/kpiThunk';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import PositionModal from './(dialog)/create-or-update';
import { KpiLoaiConst } from '@/constants/kpi/kpiType.const';
import { toast } from 'react-toastify';
import { getAllPhongBanByKpiRole } from '@redux/feature/danh-muc/danhmucThunk';
import { getAllUserByKpiRole } from '@redux/feature/userSlice';
import { ETableColumnType } from '@/constants/e-table.consts';
import KetQuaInput from '@components/bthanh-custom/kpiTableInput';
import { useKpiStatusAction } from '@hooks/kpi/UpdateStatusKPI';
import ConfirmScoredModal from '../../modal/ConfirmScoredModal';
import { formatKetQua } from '@helpers/kpi/formatResult.helper';
import { KpiTrangThaiConst } from '@/constants/kpi/kpiStatus.const';
import KpiLogModal from '../../modal/KpiLogModal';
import KpiAiChat from '@components/bthanh-custom/kpiChatAssist';
import { KpiRoleConst } from '@/constants/kpi/kpiRole.const';
import KpiReferenceTable from '@components/bthanh-custom/kpiComplianceTable';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { useIsGranted } from '@hooks/useIsGranted';

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const { processUpdateStatus } = useKpiStatusAction();
  const watchIdPhongBan = Form.useWatch('idPhongBan', form);

  const canScore = useIsGranted(PermissionCoreConst.CoreMenuKpiListPersonalActionScore);
  const canSyncScore = useIsGranted(PermissionCoreConst.CoreMenuKpiListPersonalActionSyncScore);
  const canPrincipalApprove = useIsGranted(PermissionCoreConst.CoreMenuKpiListPersonalActionPrincipalApprove);
  const canSaveScore = useIsGranted(PermissionCoreConst.CoreMenuKpiListPersonalActionSaveScore);
  const canUpdate = useIsGranted(PermissionCoreConst.CoreMenuKpiListPersonalUpdate);
  const canDelete = useIsGranted(PermissionCoreConst.CoreMenuKpiListPersonalDelete);

  const { data: list, status, total: totalItem, summary } = useAppSelector((state) => state.kpiState.kpiCaNhan.$list);
  const { list: listNhanSu } = useAppSelector((state) => state.userState.byKpiRole);
  const { data: listPhongBan } = useAppSelector((state) => state.danhmucState.phongBanByKpiRole.$list);
  const { data: trangThaiCaNhan, status: trangThaiStatus } = useAppSelector(
    (state) => state.kpiState.meta.trangThai.caNhan
  );

  type ModalMode = 'create' | 'update' | 'view' | null;
  const [modalMode, setModalMode] = useState<ModalMode>(null);
  const [openChamModal, setOpenChamModal] = useState(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);
  const [ketQuaCapTrenMap, setKetQuaCapTrenMap] = useState<Record<number, number | undefined>>({});
  const [openLogModal, setOpenLogModal] = useState(false);
  const [selectedKpiLogId, setSelectedKpiLogId] = useState<number | null>(null);
  const [activeLoaiKpi, setActiveLoaiKpi] = useState<number>(KpiLoaiConst.CHUC_NANG);
  const [isKpiComplianceTableOpen, setIsKpiComplianceTableOpen] = useState(false);
  const { data: logData, status: logStatus } = useAppSelector((state) => state.kpiState.kpiLog.$list);

  const KPI_TABS = KpiLoaiConst.list.map((x) => ({
    key: String(x.value),
    label: `KPI ${x.name}`
  }));

  useEffect(() => {
    dispatch(getAllPhongBanByKpiRole({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getAllUserByKpiRole({ PageIndex: 1, PageSize: 2000 }));
    dispatch(getListTrangThaiKpiCaNhan());
  }, [dispatch]);

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryKpiCaNhan>({
    total: totalItem || 0,
    initialQuery: { PageIndex: 1, PageSize: 10, Keyword: '', loaiKpi: KpiLoaiConst.CHUC_NANG },
    onQueryChange: (newQuery) => dispatch(getAllKpiCaNhan(newQuery)),
    triggerFirstLoad: true
  });

  const handlePhongBanChange = (value: number | undefined) => {
    onFilterChange({ idPhongBan: value, idNhanSu: undefined });
    form.setFieldValue('idNhanSu', undefined);
    dispatch(getAllUserByKpiRole({ IdPhongBan: value, PageIndex: 1, PageSize: 2000 }));
  };

  const getDataSourceForAction = async () => {
    if (selectedRowKeys.length > list.length) {
      const loadingMsg = toast.loading('Đang xử lý dữ liệu tất cả các trang...');
      try {
        const result = await dispatch(
          getAllKpiCaNhan({
            ...query,
            PageIndex: 1,
            PageSize: totalItem || 9999
          })
        ).unwrap();

        toast.dismiss(loadingMsg);
        return result?.items || [];
      } catch {
        toast.dismiss(loadingMsg);
        toast.error('Lỗi tải dữ liệu');
        return [];
      }
    }
    return list;
  };

  const scoreSelected = () => {
    if (!selectedRowKeys.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
      return;
    }
    const validItems = list.filter(
      (kpi) => selectedRowKeys.includes(kpi.id) && kpi.trangThai == KpiTrangThaiConst.DA_GUI_CHAM
    );

    if (selectedRowKeys.length <= list.length && !validItems.length) {
      toast.warning('Chỉ KPI đang ở trạng thái "Đã gửi chấm" mới được chấm');
      return;
    }
    setOpenChamModal(true);
  };

  const handleSubmitScore = async (note?: string) => {
    try {
      await dispatch(
        updateTrangThaiKpiCaNhan({
          ids: selectedRowKeys.map(Number),
          trangThai: KpiTrangThaiConst.DA_CHAM,
          note
        })
      ).unwrap();

      toast.success('Chấm KPI thành công');
      setOpenChamModal(false);
      setSelectedRowKeys([]);
      dispatch(getAllKpiCaNhan(query));
    } catch {
      toast.error('Chấm KPI thất bại');
    }
  };

  const cancelScoredSelected = async () => {
    const sourceData = await getDataSourceForAction();
    processUpdateStatus(selectedRowKeys.map(Number), sourceData, {
      validStatus: [KpiTrangThaiConst.DA_CHAM],
      invalidMsg: 'Chỉ KPI "Đã chấm" mới được hủy',
      confirmTitle: 'Hủy gửi chấm KPI',
      confirmMessage: 'Xác nhận hủy chấm các KPI đã chọn?',
      successMsg: 'Hủy kết quả chấm thành công',
      nextStatus: KpiTrangThaiConst.DA_GUI_CHAM,
      updateAction: updateTrangThaiKpiCaNhan,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiCaNhan(query));
      }
    });
  };

  const principalApprovedSelected = async () => {
    const sourceData = await getDataSourceForAction();
    processUpdateStatus(selectedRowKeys.map(Number), sourceData, {
      validStatus: [KpiTrangThaiConst.DA_CHAM],
      invalidMsg: 'Chỉ có KPI đang ở trạng thái "Đã chấm" mới được phê duyệt.',
      confirmTitle: 'Phê duyệt KPI',
      confirmMessage: 'Xác nhận phê duyệt các KPI đã chọn?',
      successMsg: 'Phê duyệt thành công',
      nextStatus: KpiTrangThaiConst.HIEU_TRUONG_PHE_DUYET,
      updateAction: updateTrangThaiKpiCaNhan,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiCaNhan(query));
      }
    });
  };

  const cancelPrincipalApprovedSelected = async () => {
    const sourceData = await getDataSourceForAction();
    processUpdateStatus(selectedRowKeys.map(Number), sourceData, {
      validStatus: [KpiTrangThaiConst.HIEU_TRUONG_PHE_DUYET],
      invalidMsg: 'Chỉ KPI "Đã phê duyệt kết quả chấm" mới được hủy',
      confirmTitle: 'Hủy phê duyệt KPI',
      confirmMessage: 'Hủy phê duyệt các KPI đã chọn?',
      successMsg: 'Hủy phê duyệt chấm thành công',
      nextStatus: KpiTrangThaiConst.DA_CHAM,
      updateAction: updateTrangThaiKpiCaNhan,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiCaNhan(query));
      }
    });
  };

  const updateKetQuaCapTren = (id: number, value?: number) => {
    setKetQuaCapTrenMap((prev) => ({
      ...prev,
      [id]: value
    }));
  };

  const syncKetQuaThucTeToCapTren = async () => {
    if (!selectedRowKeys.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
      return;
    }
    const sourceData = await getDataSourceForAction();
    const newMap = { ...ketQuaCapTrenMap };
    let count = 0;
    sourceData.forEach((item) => {
      if (selectedRowKeys.includes(item.id) && item.ketQuaThucTe !== null && item.ketQuaThucTe !== undefined) {
        newMap[item.id] = item.ketQuaThucTe;
        count++;
      }
    });

    setKetQuaCapTrenMap(newMap);
    toast.success(`Đã đồng bộ kết quả thực tế cho ${count} KPI`);
  };

  const handleSaveKetQuaCapTren = async () => {
    const selectedIds = new Set(selectedRowKeys.map(Number));
    const items = Object.entries(ketQuaCapTrenMap)
      .filter(([id, v]) => v !== undefined && selectedIds.has(Number(id)))
      .map(([id, value]) => ({
        id: Number(id),
        KetQuaCapTren: value
      }));

    if (!items.length) {
      toast.warning('Chọn KPI đánh giá đã thay đổi cần lưu');
      return;
    }
    try {
      await dispatch(updateKetQuaCapTrenKpiCaNhan({ items })).unwrap();
      toast.success('Lưu kết quả đánh giá thành công');
      setSelectedRowKeys([]);
      setKetQuaCapTrenMap({});
      dispatch(getAllKpiCaNhan(query));
    } catch {
      toast.error('Lưu kết quả thất bại');
    }
  };

  const requiredSelect = (cb: () => void) => {
    if (!selectedRowKeys.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
      return;
    }
    cb();
  };

  const loaiSummary = summary?.byLoaiKpi?.find((x) => x.loaiKpi === activeLoaiKpi);

  const tongTuDanhGia = loaiSummary?.tuDanhGia ?? summary?.tongTuDanhGia ?? 0;

  const tongCapTren = loaiSummary?.capTren ?? summary?.tongCapTren ?? 0;

  const bulkActionItems: MenuProps['items'] = [
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
          label: 'Hủy phê duyệt kết quả chấm',
          icon: <UndoOutlined style={{ color: '#00ff1a6b' }} />,
          onClick: () => requiredSelect(cancelPrincipalApprovedSelected)
        }
      ]
      : [])
  ];

  const filterContent = (
    <Form
      form={filterForm}
      layout="vertical"
      onValuesChange={(_, values) => {
        onFilterChange(values);
        setOpenFilter(false);
      }}
    >
      <Form.Item label="Trạng thái" name="trangThai">
        <Select
          allowClear
          placeholder="Chọn trạng thái"
          loading={trangThaiStatus === ReduxStatus.LOADING}
          options={trangThaiCaNhan}
        />
      </Form.Item>
    </Form>
  );

  const onClickAdd = () => setModalMode('create');
  const onClickUpdate = (record: IViewKpiCaNhan) => {
    dispatch(setSelectedKpiCaNhan(record));
    setModalMode('update');
  };
  const onClickView = (record: IViewKpiCaNhan) => {
    dispatch(setSelectedKpiCaNhan(record));
    setModalMode('view');
  };
  const onClickViewLog = (record: IViewKpiCaNhan) => {
    setSelectedKpiLogId(record.id);
    setOpenLogModal(true);

    dispatch(
      getKpiLogStatus({
        kpiId: record.id,
        PageIndex: 1,
        PageSize: 50,
        capKpi: 1
      })
    );
  };
  const onClickDelete = (record: IViewKpiCaNhan) => {
    Modal.confirm({
      title: `Xóa Kpi ${record.kpi} của ${record.nhanSu}?`,
      okText: 'Xóa',
      okType: 'danger',
      cancelText: 'Hủy',
      onOk: async () => {
        try {
          await dispatch(deleteKpiCaNhan(record.id)).unwrap();
          toast.success('Xóa thành công!');
          dispatch(getAllKpiCaNhan(query));
        } catch {
          toast.error('Xóa thất bại!');
        }
      }
    });
  };

  const columns: IColumn<IViewKpiCaNhan>[] = [
    {
      key: 'kpi',
      dataIndex: 'kpi',
      title: 'Tên KPI',
      width: 400
    },
    {
      key: 'nhanSu',
      dataIndex: 'nhanSu',
      title: 'Nhân sự',
      width: 140
    },
    {
      key: 'mucTieu',
      dataIndex: 'mucTieu',
      title: 'Mục tiêu',
      width: 100
    },
    {
      key: 'trongSo',
      dataIndex: 'trongSo',
      title: 'Trọng số',
      width: 100
    },
    {
      key: 'congThuc',
      dataIndex: 'congThuc',
      title: 'Công thức tính',
      width: 200,
      render: (val, record) => {
        if (record.loaiKpi === 3) {
          return (
            <div
              className="cursor-pointer font-semibold text-blue-600 underline hover:text-blue-900"
              onClick={() => setIsKpiComplianceTableOpen(true)}
            >
              {val || 'Xem phụ lục'}
            </div>
          );
        }
        return <span>{val || '-'}</span>;
      }
    },
    {
      key: 'ketQuaThucTe',
      dataIndex: 'ketQuaThucTe',
      title: 'Kết quả thực tế',
      width: 140,
      render: (val, record) => formatKetQua(val, record.loaiKetQua)
    },
    {
      key: 'diemKpi',
      dataIndex: 'diemKpi',
      title: 'Điểm tự đánh giá',
      width: 150
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
            editable={record.isActive != 0}
          />
        );
      }
    },
    {
      key: 'diemKpiCapTren',
      dataIndex: 'diemKpiCapTren',
      title: 'Điểm cấp trên',
      width: 130,
      align: 'center'
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
      width: 150,
      type: ETableColumnType.STATUS,
      render: (val) => <Tag color={KpiTrangThaiConst.get(val)?.color}>{KpiTrangThaiConst.get(val)?.text}</Tag>
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      icon: <EyeOutlined />,
      command: onClickView
    },
    {
      label: 'Sửa',
      color: 'blue',
      icon: <EditOutlined />,
      hidden: () => !canUpdate,
      command: onClickUpdate
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      hidden: () => !canDelete,
      command: onClickDelete
    },
    {
      label: 'Nhật ký trạng thái',
      icon: <EyeOutlined />,
      color: 'orange',
      command: onClickViewLog
    }
  ];

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSelectAllPages = async () => {
    try {
      const allIds = await dispatch(
        getAllIdsKpiCaNhan({
          ...query,
          PageIndex: 1,
          PageSize: totalItem || 9999
        })
      ).unwrap();

      setSelectedRowKeys(allIds);
      toast.success(`Đã chọn tất cả ${allIds.length} KPI`);
    } catch (error) {
      toast.error('Không thể lấy danh sách ID');
    }
  };

  const rowSelection = {
    selectedRowKeys,
    preserveSelectedRowKeys: true,
    onChange: setSelectedRowKeys,
    selections: [
      {
        key: 'current-page',
        text: 'Chọn trang hiện tại',
        onSelect: (keys: React.Key[]) => {
          setSelectedRowKeys(keys);
        }
      },
      {
        key: 'all-pages',
        text: 'Chọn tất cả các trang',
        onSelect: handleSelectAllPages
      }
    ]
  };

  return (
    <Card
      title="Danh sách KPI Cá nhân"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="mb-6 flex items-center justify-between gap-4">
          <div className="flex flex-1 items-center gap-2">
            <Input
              placeholder="Tìm KPI..."
              prefix={<SearchOutlined />}
              allowClear
              onChange={(e) => handleDebouncedSearch(e.target.value)}
              className="max-w-[250px]"
            />
            <Form.Item name="idPhongBan" noStyle>
              <Select
                placeholder="Tất cả đơn vị"
                style={{ width: 180 }}
                allowClear
                options={listPhongBan?.map((x) => ({ value: x.id, label: x.tenPhongBan }))}
                onChange={handlePhongBanChange}
              />
            </Form.Item>
            <Form.Item name="idNhanSu" noStyle>
              <Select
                disabled={!watchIdPhongBan}
                placeholder={watchIdPhongBan ? 'Chọn nhân sự' : 'Chọn nhân sự (Tất cả)'}
                style={{ width: 320 }}
                allowClear
                showSearch
                optionFilterProp="label"
                options={listNhanSu?.map((x: any) => {
                  const roleCode = KpiRoleConst.list.find(
                    (r) => r.name.trim().toLowerCase() === x.tenChucVu?.trim().toLowerCase()
                  )?.value;
                  const tenChucVuChuan = KpiRoleConst.getName(roleCode) || x.tenChucVu || '';
                  const hoTen = (x.hoTen || `${x.hoDem ?? ''} ${x.ten ?? ''}`).trim();
                  return {
                    value: x.id,
                    label: `${hoTen}${tenChucVuChuan ? ` - ${tenChucVuChuan}` : ''}`
                  };
                })}
                onChange={(val) => onFilterChange({ idNhanSu: val })}
              />
            </Form.Item>
            <Button
              color="default"
              variant="filled"
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                filterForm.resetFields();
                setKetQuaCapTrenMap({});
                onFilterChange({
                  Keyword: '',
                  idPhongBan: undefined,
                  idNhanSu: undefined,
                  loaiKpi: activeLoaiKpi,
                  trangThai: undefined
                });
                setSelectedRowKeys([]);
              }}
            >
              Tải lại
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
              <Button icon={<FilterOutlined />} type={openFilter ? 'primary' : 'default'}>
                Bộ lọc
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

          onFilterChange({
            loaiKpi: loai,
            PageIndex: 1
          });
        }}
      />

      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
        rowSelection={{
          ...rowSelection,
          fixed: 'left'
        }}
        scroll={{ x: 'max-content', y: 'calc(96vh - 400px)' }}
        footer={() => (
          <div className="flex justify-end gap-8">
            <div>
              <span className="font-medium">Tổng điểm tự đánh giá:</span>{' '}
              <span className="font-semibold text-blue-600">{tongTuDanhGia.toFixed(2)}</span>
            </div>

            <div>
              <span className="font-medium">Tổng điểm cấp trên:</span>{' '}
              <span className="font-semibold text-green-600">{tongCapTren.toFixed(2)}</span>
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
          dispatch(getAllKpiCaNhan(query));
          dispatch(getListTrangThaiKpiCaNhan());
          dispatch(getListKpiCongThuc({}));
        }}
      />
      <ConfirmScoredModal
        open={openChamModal}
        title="Chấm KPI Cá nhân"
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
      <KpiAiChat />
      <Modal
        title={<span className="text-lg font-bold text-blue-700">PHỤ LỤC: KPIs KỶ LUẬT LAO ĐỘNG CÁ NHÂN</span>}
        open={isKpiComplianceTableOpen}
        onCancel={() => setIsKpiComplianceTableOpen(false)}
        footer={[]}
        width={1000}
        style={{ top: 20 }}
      >
        <KpiReferenceTable />
      </Modal>
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuKpiListPersonal);