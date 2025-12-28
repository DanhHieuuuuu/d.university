'use client';
import { useEffect, useMemo, useState } from 'react';
import { Button, Card, Dropdown, Form, Input, MenuProps, Modal, Popover, Select, Tag } from 'antd';
import {
  PlusOutlined, SearchOutlined, SyncOutlined, EditOutlined, DeleteOutlined,
  EyeOutlined, FilterOutlined, CheckCircleOutlined, EllipsisOutlined, SaveOutlined, UndoOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiCaNhan } from '@redux/feature/kpi/kpiSlice';
import { deleteKpiCaNhan, getAllIdsKpiCaNhan, getKpiCaNhanKeKhai, getListTrangThaiKpiCaNhan, updateTrangThaiKpiCaNhan } from '@redux/feature/kpi/kpiThunk';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import PositionModal from './(dialog)/create-or-update';
import { KpiLoaiConst } from '../../const/kpiType.const';
import { KpiTrangThaiConst } from '../../const/kpiStatus.const';
import { toast } from 'react-toastify';
import { getAllPhongBan } from '@redux/feature/danh-muc/danhmucThunk';
import { getAllUser } from '@redux/feature/userSlice';
import { buildKpiGroupedTable, KpiTableRow } from '@helpers/kpi/kpi.helper';

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const watchIdPhongBan = Form.useWatch('idPhongBan', form);

  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiCaNhan.$list);
  const { list: listNhanSu } = useAppSelector((state) => state.userState);
  const { data: listPhongBan } = useAppSelector((state) => state.danhmucState.phongBan.$list);
  const { data: trangThaiCaNhan, status: trangThaiStatus } = useAppSelector((state) => state.kpiState.meta.trangThai.caNhan);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isUpdate, setIsModalUpdate] = useState(false);
  const [isView, setIsModalView] = useState(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  const tableData = useMemo(() => buildKpiGroupedTable<IViewKpiCaNhan>(list || []), [list]);

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryKpiCaNhan>({
    total: totalItem || 0,
    initialQuery: { PageIndex: 1, PageSize: 10, Keyword: '' },
    onQueryChange: (newQuery) => dispatch(getKpiCaNhanKeKhai(newQuery)),
    triggerFirstLoad: true
  });

  useEffect(() => {
    dispatch(getAllPhongBan({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getAllUser({ PageIndex: 1, PageSize: 2000 }));
    dispatch(getListTrangThaiKpiCaNhan());
  }, [dispatch]);

  const approveSelected = () => {
    const ids = selectedRowKeys.map(Number);
    const kpiToApprove = list.filter(
      (item) => ids.includes(item.id) && item.trangThai === KpiTrangThaiConst.DE_XUAT
    );
    if (!kpiToApprove.length) return toast.error('Chỉ KPI "Đề xuất" mới được phê duyệt.');
    Modal.confirm({
      title: 'Phê duyệt hàng loạt',
      content: `Xác nhận phê duyệt ${kpiToApprove.length} KPI đã chọn?`,
      okText: 'Duyệt',
      onOk: async () => {
        try {
          await dispatch(updateTrangThaiKpiCaNhan({ ids, trangThai: KpiTrangThaiConst.PHE_DUYET })).unwrap();
          toast.success('Phê duyệt thành công!');
          setSelectedRowKeys([]);
          dispatch(getKpiCaNhanKeKhai(query));
        } catch { toast.error('Phê duyệt thất bại!'); }
      }
    });
  };
  const scoreSelected = () => toast.info('Chức năng chấm KPI hàng loạt đang được cập nhật.');

  const requiredSelect = (cb: () => void) => {
    if (!selectedRowKeys.length) return toast.warning('Vui lòng chọn ít nhất một KPI');
    cb();
  };

  const bulkActionItems: MenuProps['items'] = [
    { key: 'approve', label: 'Gửi duyệt', icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />, onClick: () => requiredSelect(approveSelected) },
    { key: 'score', label: 'Hủy duyệt', icon: <UndoOutlined style={{ color: '#1890ff' }} />, onClick: () => requiredSelect(scoreSelected) },
  ];

  const filterContent = (
    <Form form={filterForm} layout="vertical" onValuesChange={(_, values) => { onFilterChange(values); setOpenFilter(false); }}>
      <Form.Item label="Loại KPI" name="loaiKpi">
        <Select allowClear placeholder="Chọn loại KPI" options={KpiLoaiConst.list.map(x => ({ value: x.value, label: x.name }))} />
      </Form.Item>
      <Form.Item label="Trạng thái" name="trangThai">
        <Select allowClear placeholder="Chọn trạng thái" loading={trangThaiStatus === ReduxStatus.LOADING} options={trangThaiCaNhan} />
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
        try { await dispatch(deleteKpiCaNhan(record.id)).unwrap(); toast.success('Xóa thành công!'); dispatch(getKpiCaNhanKeKhai(query)); }
        catch { toast.error('Xóa thất bại!'); }
      }
    });
  };
  const handlePhongBanChange = (value: number | undefined) => {
    onFilterChange({ idPhongBan: value, idNhanSu: undefined });
    form.setFieldValue('idNhanSu', undefined);
    dispatch(getAllUser({ IdPhongBan: value, PageIndex: 1, PageSize: 2000 }));
  };
  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => onFilterChange({ Keyword: value }), 500);

  const hideMeta = (record: any) => record.rowType !== 'data' ? { props: { colSpan: 0 } } : undefined;

  const columns: IColumn<KpiTableRow<IViewKpiCaNhan>>[] = [
    {
      key: 'linhVuc',
      dataIndex: 'linhVuc', title: 'Lĩnh Vực',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val),
    },
    {
      key: 'kpi', dataIndex: 'kpi', title: 'Tên KPI',
      render: (value, record) => {
        if (record.rowType === 'group') {
          return {
            children: (
              <div style={{
                fontSize: 14, // Giảm từ 16 xuống 14
                fontWeight: 600,
                textAlign: 'center', // Để left cho giống các hàng khác
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
      key: 'mucTieu',
      dataIndex: 'mucTieu',
      title: 'Mục tiêu',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val),
    },
    {
      key: 'trongSo',
      dataIndex: 'trongSo',
      title: 'Trọng số',
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val)
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
      render: (val, record) => (record.rowType !== 'data' ? { props: { colSpan: 0 } } : val)
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai', title: 'Trạng thái',
      render: (val, record) =>
        record.rowType !== 'data'
          ? { props: { colSpan: 0 } }
          : <Tag color={KpiTrangThaiConst.get(val)?.color}>{KpiTrangThaiConst.get(val)?.text}</Tag>,
    },
  ];

  const actions: IAction[] = [
    { label: 'Chi tiết', icon: <EyeOutlined />, command: onClickView, hidden: r => r.rowType !== 'data' },
    { label: 'Sửa', icon: <EditOutlined />, command: onClickUpdate, hidden: r => r.rowType !== 'data' },
    { label: 'Xóa', color: 'red', icon: <DeleteOutlined />, command: onClickDelete, hidden: r => r.rowType !== 'data' }
  ];

  const rowSelection = {
    selectedRowKeys,
    preserveSelectedRowKeys: true,
    getCheckboxProps: (record: any) => ({ disabled: record.rowType !== 'data', style: record.rowType !== 'data' ? { display: 'none' } : {} }),
    onChange: setSelectedRowKeys,
  };
  return (
    <Card
      title="Kê khai KPI cá nhân"
      style={{ height: 'calc(100vh - 40px)', display: 'flex', flexDirection: 'column' }}
      styles={{ body: { flex: 1, overflow: 'hidden', display: 'flex', flexDirection: 'column' } }}
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal" className="flex-none">
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
          </div>

          <div className="flex items-center gap-2">
            <Button icon={<SaveOutlined />}>Lưu kết quả</Button>
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
            <Button type="primary" htmlType="submit" icon={<SearchOutlined />} onClick={() => dispatch(getKpiCaNhanKeKhai(query))}>
              Tìm kiếm
            </Button>
            <Button
              color="default"
              variant="filled"
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                filterForm.resetFields();
                onFilterChange({ Keyword: '', idPhongBan: undefined, idNhanSu: undefined, loaiKpi: undefined, trangThai: undefined });
                setSelectedRowKeys([]);
              }}
            >
              Tải lại
            </Button>
          </div>
        </Form.Item>
      </Form>
      <div className="flex-1 overflow-auto mt-2 w-full">
        <AppTable
          loading={status === ReduxStatus.LOADING}
          rowKey="id"
          columns={columns}
          dataSource={tableData}
          listActions={actions}
          pagination={false}
          rowSelection={rowSelection}
          onRow={(record) => ({
            style: {
              cursor: record.rowType !== 'data' ? 'default' : 'pointer',
              backgroundColor: record.rowType === 'group' ? '#f0f5ff' : record.rowType === 'total' ? '#fafafa' : '#fff',
              fontWeight: record.rowType !== 'data' ? 600 : 'normal',
            }
          })}
        />
      </div>
      <PositionModal isModalOpen={isModalOpen} isUpdate={isUpdate} isView={isView} setIsModalOpen={setIsModalOpen} onSuccess={() => { dispatch(getKpiCaNhanKeKhai(query)); dispatch(getListTrangThaiKpiCaNhan()); }} />
    </Card>
  );
};

export default Page;
