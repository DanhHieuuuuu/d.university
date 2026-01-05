'use client';
import { useEffect, useMemo, useState } from 'react';
import { Button, Card, Dropdown, Form, Input, MenuProps, Modal, Popover, Select, Tag } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  FilterOutlined,
  CheckCircleOutlined,
  EllipsisOutlined,
  UndoOutlined,
  SaveOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiCaNhan } from '@redux/feature/kpi/kpiSlice';
import { deleteKpiCaNhan, getAllIdsKpiCaNhan, getAllKpiCaNhan, getListTrangThaiKpiCaNhan, updateKetQuaCapTrenKpiCaNhan, updateTrangThaiKpiCaNhan } from '@redux/feature/kpi/kpiThunk';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import PositionModal from './(dialog)/create-or-update';
import { KPI_ORDER, KpiLoaiConst } from '../../const/kpiType.const';
import { KpiTrangThaiConst } from '../../const/kpiStatus.const';
import { toast } from 'react-toastify';
import { getAllPhongBan } from '@redux/feature/danh-muc/danhmucThunk';
import { getAllUser } from '@redux/feature/userSlice';
import { ETableColumnType } from '@/constants/e-table.consts';
import KetQuaInput from '@components/bthanh-custom/kpiTableInput';
import { buildKpiGroupedTable, KpiTableRow } from '@helpers/kpi/kpi.helper';
import { useKpiStatusAction } from '@hooks/kpi/UpdateStatusKPI';
import ConfirmScoredModal from '../../modal/ConfirmScoredModal';
import { formatKetQua } from '@helpers/kpi/formatResult.helper';

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const { processUpdateStatus } = useKpiStatusAction();
  const watchIdPhongBan = Form.useWatch('idPhongBan', form);

  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiCaNhan.$list);
  const { list: listNhanSu } = useAppSelector((state) => state.userState);
  const { data: listPhongBan } = useAppSelector((state) => state.danhmucState.phongBan.$list);
  const { data: trangThaiCaNhan, status: trangThaiStatus } = useAppSelector(
    (state) => state.kpiState.meta.trangThai.caNhan
  );

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isUpdate, setIsModalUpdate] = useState(false);
  const [isView, setIsModalView] = useState(false);
  const [openChamModal, setOpenChamModal] = useState(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);
  const [ketQuaCapTrenMap, setKetQuaCapTrenMap] = useState<Record<number, number | undefined>>({});

  const tableData = useMemo(() => {
    const sortedList = [...(list || [])].sort(
      (a, b) =>
        KPI_ORDER.indexOf(a.loaiKpi) -
        KPI_ORDER.indexOf(b.loaiKpi)
    );

    return buildKpiGroupedTable<IViewKpiCaNhan>(sortedList);
  }, [list]);

  useEffect(() => {
    dispatch(getAllPhongBan({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getAllUser({ PageIndex: 1, PageSize: 2000 }));
    dispatch(getListTrangThaiKpiCaNhan());
  }, [dispatch]);

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryKpiCaNhan>({
    total: totalItem || 0,
    initialQuery: { PageIndex: 1, PageSize: 10, Keyword: '' },
    onQueryChange: (newQuery) => dispatch(getAllKpiCaNhan(newQuery)),
    triggerFirstLoad: true
  });

  const handlePhongBanChange = (value: number | undefined) => {
    onFilterChange({ idPhongBan: value, idNhanSu: undefined });
    form.setFieldValue('idNhanSu', undefined);
    dispatch(getAllUser({ IdPhongBan: value, PageIndex: 1, PageSize: 2000 }));
  };

  const scoreSelected = () => {
    if (!selectedRowKeys.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
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
          note,
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

  const cancelScoredSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
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
      },
    });

  const principalApprovedSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
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
      },
    });

  const cancelPrincipalApprovedSelected = () =>
    processUpdateStatus(selectedRowKeys.map(Number), list, {
      validStatus: [KpiTrangThaiConst.HIEU_TRUONG_PHE_DUYET],
      invalidMsg: 'Chỉ KPI "Hiệu trưởng đã chấm" mới được hủy',
      confirmTitle: 'Hủy phê duyệt KPI',
      confirmMessage: 'Hủy phê duyệt các KPI đã chọn?',
      successMsg: 'Hủy phê duyệt chấm thành công',
      nextStatus: KpiTrangThaiConst.DA_CHAM,
      updateAction: updateTrangThaiKpiCaNhan,
      afterSuccess: () => {
        setSelectedRowKeys([]);
        dispatch(getAllKpiCaNhan(query));
      },
    });


  const updateKetQuaCapTren = (id: number, value?: number) => {
    setKetQuaCapTrenMap(prev => ({
      ...prev,
      [id]: value
    }));
  };

  const handleSaveKetQuaCapTren = async () => {
    const selectedIds = new Set(selectedRowKeys.map(Number));
    const items = Object.entries(ketQuaCapTrenMap)
      .filter(([id, v]) =>
        v !== undefined && selectedIds.has(Number(id))
      )
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
      setKetQuaCapTrenMap({})
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
  const bulkActionItems: MenuProps['items'] = [
    {
      key: 'score',
      label: 'Chấm KPI',
      icon: <EditOutlined style={{ color: '#1890ff' }} />,
      onClick: () => requiredSelect(scoreSelected),
    },
    {
      key: 'cancelScore',
      label: 'Hủy kết quả chấm KPI',
      icon: <UndoOutlined style={{ color: '#1890ff' }} />,
      onClick: () => requiredSelect(cancelScoredSelected),
    },
    {
      key: 'principalApprove',
      label: 'Hiệu trưởng phê duyệt',
      icon: <EditOutlined style={{ color: '#00ff1a6b' }} />,
      onClick: () => requiredSelect(principalApprovedSelected),
    },
    {
      key: 'cancelPrincipalApprove',
      label: 'Hiệu trưởng hủy duyệt',
      icon: <UndoOutlined style={{ color: '#00ff1a6b' }} />,
      onClick: () => requiredSelect(cancelPrincipalApprovedSelected),
    },
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
      <Form.Item label="Loại KPI" name="loaiKpi">
        <Select
          allowClear
          placeholder="Chọn loại KPI"
          options={KpiLoaiConst.list.map(x => ({ value: x.value, label: x.name }))}
        />
      </Form.Item>
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

  const onClickAdd = () => { setIsModalView(false); setIsModalUpdate(false); setIsModalOpen(true); };
  const onClickUpdate = (record: IViewKpiCaNhan) => { dispatch(setSelectedKpiCaNhan(record)); setIsModalUpdate(true); setIsModalOpen(true); };
  const onClickView = (record: IViewKpiCaNhan) => { dispatch(setSelectedKpiCaNhan(record)); setIsModalView(true); setIsModalOpen(true); };
  const onClickDelete = (record: IViewKpiCaNhan) => {
    Modal.confirm({
      title: `Xóa Kpi ${record.kpi} của ${record.nhanSu}?`,
      okText: 'Xóa', okType: 'danger', cancelText: 'Hủy',
      onOk: async () => {
        try {
          await dispatch(deleteKpiCaNhan(record.id)).unwrap();
          toast.success('Xóa thành công!');
          dispatch(getAllKpiCaNhan(query));
        } catch { toast.error('Xóa thất bại!'); }
      }
    });
  };

  const columns: IColumn<KpiTableRow<IViewKpiCaNhan>>[] = [
    {
      key: 'linhVuc',
      dataIndex: 'linhVuc',
      title: 'Lĩnh Vực',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val),
    },
    {
      key: 'kpi',
      dataIndex: 'kpi',
      title: 'Tên KPI',
      render: (value, record) => {
        if (record.rowType === 'group') {
          return {
            children: (
              <div style={{
                fontSize: 14,
                fontWeight: 600,
                textAlign: 'center',
                color: '#0958d9',
              }}>
                {KpiLoaiConst.getName(record.loaiKpi)}
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
      key: 'nhanSu',
      dataIndex: 'nhanSu',
      title: 'Nhân sự',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val),
    },
    {
      key: 'mucTieu',
      dataIndex: 'mucTieu',
      title: 'Mục tiêu',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val),
    },
    {
      key: 'trongSo',
      dataIndex: 'trongSo',
      title: 'Trọng số',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val),
    },
    {
      key: 'loaiKpi',
      dataIndex: 'loaiKpi',
      title: 'Loại KPI',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : KpiLoaiConst.getName(val))
    },
    {
      key: 'ketQuaThucTe',
      dataIndex: 'ketQuaThucTe',
      title: 'Kết quả thực tế',
      render: (val, record) =>
        record.rowType !== 'data'
          ? { props: { colSpan: 0 } }
          : formatKetQua(val, record.loaiCongThuc),
    },
    {
      key: 'diemKpi',
      dataIndex: 'diemKpi',
      title: 'Điểm tự đánh giá',
      align: 'center',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val),
    },
    {
      key: 'capTrenDanhGia',
      dataIndex: 'capTrenDanhGia',
      title: 'Cấp trên đánh giá',
      render: (val, record) => {
        if (record.rowType !== 'data') {
          return { props: { colSpan: 0 } };
        }
        const value = ketQuaCapTrenMap[record.id] ?? val;
        return (
          <KetQuaInput
            loaiCongThuc={record.loaiCongThuc}
            value={value}
            onChange={(v) => updateKetQuaCapTren(record.id, v)}
          />
        );
      },
    },
    {
      key: 'diemKpiCapTren',
      dataIndex: 'diemKpiCapTren',
      title: 'Điểm cấp trên',
      align: 'center',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val),
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
      type: ETableColumnType.STATUS,
      render: (val, record) =>
        record.rowType !== 'data'
          ? { props: { colSpan: 0 } }
          : <Tag color={KpiTrangThaiConst.get(val)?.color}>{KpiTrangThaiConst.get(val)?.text}</Tag>,
    },
  ];

  const actions: IAction[] = [
    { label: 'Chi tiết', icon: <EyeOutlined />, command: onClickView },
    { label: 'Sửa', icon: <EditOutlined />, command: onClickUpdate },
    { label: 'Xóa', color: 'red', icon: <DeleteOutlined />, command: onClickDelete }
  ];

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSelectAllPages = async () => {
    try {
      const allIds = await dispatch(getAllIdsKpiCaNhan({
        ...query,
        PageIndex: 1,
        PageSize: totalItem || 9999,
      })).unwrap();

      setSelectedRowKeys(allIds);
      toast.success(`Đã chọn tất cả ${allIds.length} KPI`);
    } catch (error) {
      toast.error('Không thể lấy danh sách ID');
    }
  };

  const rowSelection = {
    selectedRowKeys,
    preserveSelectedRowKeys: true,
    getCheckboxProps: (record: any) => ({ disabled: record.rowType !== 'data', style: record.rowType !== 'data' ? { display: 'none' } : {} }),
    onChange: (newSelectedRowKeys: React.Key[]) => {
      const currentPageIds = list?.map(item => item.id) || [];
      const isUnselectingAllOnPage = currentPageIds.every(id => !newSelectedRowKeys.includes(id));

      if (isUnselectingAllOnPage && selectedRowKeys.length > list?.length) {
        setSelectedRowKeys([]);
      } else {
        setSelectedRowKeys(newSelectedRowKeys);
      }
    },
    selections: [
      {
        key: 'current-page',
        text: 'Chọn trang hiện tại',
        onSelect: (changableRowKeys: React.Key[]) => {
          setSelectedRowKeys(changableRowKeys);
        },
      },
      {
        key: 'all-pages',
        text: 'Chọn tất cả các trang',
        onSelect: () => {
          handleSelectAllPages();
        },
      },
    ],
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
        <div className="flex items-center justify-between mb-4 gap-4">
          <div className="flex items-center gap-2 flex-1">
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
                options={listPhongBan?.map(x => ({ value: x.id, label: x.tenPhongBan }))}
                onChange={handlePhongBanChange}
              />
            </Form.Item>
            <Form.Item name="idNhanSu" noStyle>
              <Select
                placeholder={watchIdPhongBan ? "Chọn nhân sự" : "Chọn nhân sự (Tất cả)"}
                style={{ width: 220 }}
                allowClear
                showSearch
                optionFilterProp="label"
                options={listNhanSu?.map((x: any) => ({
                  value: x.id,
                  label: (x.hoTen || `${x.hoDem ?? ''} ${x.ten ?? ''}`).trim()
                }))}
                onChange={(val) => onFilterChange({ idNhanSu: val })}
              />
            </Form.Item>
          </div>

          <div className="flex items-center gap-2">
            <Button
              icon={<SaveOutlined />}
              type="primary"
              onClick={handleSaveKetQuaCapTren}

            >
              Lưu kết quả
            </Button>
            <Dropdown
              menu={{ items: bulkActionItems }}
              trigger={['click']}
              disabled={selectedRowKeys.length === 0}
            >
              <Button
                type={selectedRowKeys.length > 0 ? 'primary' : 'default'}
                icon={<EllipsisOutlined />}
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
              <Button icon={<FilterOutlined />} type={openFilter ? "primary" : "default"}>
                Bộ lọc
              </Button>
            </Popover>
          </div>
        </div>

        <Form.Item>
          <div className="flex flex-row justify-center space-x-2">
            <Button type="primary" htmlType="submit" icon={<SearchOutlined />} onClick={() => dispatch(getAllKpiCaNhan(query))}>
              Tìm kiếm
            </Button>
            <Button
              color="default"
              variant="filled"
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                filterForm.resetFields();
                setKetQuaCapTrenMap({});
                onFilterChange({ Keyword: '', idPhongBan: undefined, idNhanSu: undefined, loaiKpi: undefined, trangThai: undefined });
                setSelectedRowKeys([]);
              }}
            >
              Tải lại
            </Button>
          </div>
        </Form.Item>
      </Form>

      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="id"
        columns={columns}
        dataSource={tableData}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
        rowSelection={rowSelection}
        onRow={(record) => ({
          style: {
            cursor: record.rowType !== 'data' ? 'default' : 'pointer',
            backgroundColor: record.rowType === 'group' ? '#f0f5ff' : record.rowType === 'total' ? '#fafafa' : '#fff',
            fontWeight: record.rowType !== 'data' ? 600 : 'normal',
          }
        })}
      />

      <PositionModal
        isModalOpen={isModalOpen}
        isUpdate={isUpdate}
        isView={isView}
        setIsModalOpen={setIsModalOpen}
        onSuccess={() => {
          dispatch(getAllKpiCaNhan(query));
          dispatch(getListTrangThaiKpiCaNhan());
        }}
      />
      <ConfirmScoredModal
        open={openChamModal}
        title="Chấm KPI Cá nhân"
        trangThai={KpiTrangThaiConst.DA_CHAM}
        onCancel={() => setOpenChamModal(false)}
        onSubmit={handleSubmitScore}
      />
    </Card>
  );
};

export default Page;