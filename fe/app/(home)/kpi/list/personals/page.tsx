'use client';
import { useEffect, useState } from 'react';
import { Button, Card, Form, Input, Modal, Popover, Select, Space, Tag } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  FilterOutlined,
  CheckCircleOutlined,
  EllipsisOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiCaNhan } from '@redux/feature/kpi/kpiSlice';
import { deleteKpiCaNhan, getAllIdsKpiCaNhan, getAllKpiCaNhan, updateTrangThaiKpiCaNhan } from '@redux/feature/kpi/kpiThunk';
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

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const watchIdPhongBan = Form.useWatch('idPhongBan', form);

  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiCaNhan.$list);
  const { list: listNhanSu } = useAppSelector((state) => state.userState);
  const { data: listPhongBan } = useAppSelector((state) => state.danhmucState.phongBan.$list);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isUpdate, setIsModalUpdate] = useState(false);
  const [isView, setIsModalView] = useState(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [openBulkAction, setOpenBulkAction] = useState(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  useEffect(() => {
    dispatch(getAllPhongBan({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getAllUser({ PageIndex: 1, PageSize: 2000 }));
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

  const approveSelected = () => {
    const ids = selectedRowKeys.map(Number);
    const kpiToApprove = list.filter(
      (item) => ids.includes(item.id)
    );

    if (kpiToApprove.length === 0) {
      toast.error('Chỉ có KPI đang ở trạng thái "Đề xuất" mới được phê duyệt.');
      return;
    }

    Modal.confirm({
      title: 'Phê duyệt',
      content: `Xác nhận phê duyệt ${kpiToApprove.length} KPI đã chọn?`,
      okText: 'Duyệt',
      onOk: async () => {
        try {
          await dispatch(
            updateTrangThaiKpiCaNhan({
              ids,
              trangThai: KpiTrangThaiConst.PHE_DUYET
            })
          ).unwrap();

          toast.success('Phê duyệt thành công!');
          setSelectedRowKeys([]);
          dispatch(getAllKpiCaNhan(query));
        } catch {
          toast.error('Phê duyệt thất bại!');
        }
      }
    });
  };


  const scoreSelected = () => {
    toast.info('Chức năng chấm KPI đang được cập nhật.');
  };

  const actionLists = [
    {
      key: 'APPROVE',
      label: 'Phê duyệt',
      icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />,
      command: approveSelected,
    },
    {
      key: 'SCORE',
      label: 'Chấm KPI',
      icon: <EditOutlined style={{ color: '#1890ff' }} />,
      command: scoreSelected,
    },
  ];

  const onExecuteAction = (command: () => void) => {
    if (selectedRowKeys.length === 0) {
      toast.warning('Vui lòng chọn ít nhất một KPI để thực hiện.');
      return;
    }
    setOpenBulkAction(false);
    command();
  };

  const bulkActionMenu = (
    <div className="flex flex-col min-w-[180px] py-1">
      <div className="px-3 py-2 text-gray-400 text-[10px] font-bold uppercase border-b mb-1">
        Thao tác ({selectedRowKeys.length})
      </div>
      {actionLists.map((action) => (
        <Button
          key={action.key}
          type="text"
          icon={action.icon}
          className="flex items-center w-full text-left h-9 px-3 hover:bg-slate-50"
          onClick={() => onExecuteAction(action.command)}
        >
          {action.label}
        </Button>
      ))}
    </div>
  );

  const filterContent = (
    <Form
      form={filterForm}
      layout="vertical"
      onValuesChange={(_, values: Partial<IQueryKpiCaNhan>) => onFilterChange(values)}
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
          options={KpiTrangThaiConst.list.map(x => ({ value: x.value, label: x.text }))}
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

  const columns: IColumn<IViewKpiCaNhan>[] = [
    { key: 'linhVuc', dataIndex: 'linhVuc', title: 'Lĩnh Vực' },
    { key: 'kpi', dataIndex: 'kpi', title: 'Tên KPI' },
    { key: 'nhanSu', dataIndex: 'nhanSu', title: 'Nhân sự' },
    {
      key: 'trangThai', dataIndex: 'trangThai', title: 'Trạng thái',
      render: val => {
        const s = KpiTrangThaiConst.get(val);
        return s ? <Tag color={s.color}>{s.text}</Tag> : null;
      }
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
      // Giả định bác đã tạo thunk này trong kpiThunk.ts tương tự cái trước
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
            <Popover
              content={bulkActionMenu}
              trigger="click"
              open={openBulkAction}
              onOpenChange={setOpenBulkAction}
              placement="bottomRight"
              styles={{ body: { padding: 0 } }}
            >
              <Button
                icon={<EllipsisOutlined />}
                type={selectedRowKeys.length > 0 ? "primary" : "default"}
              >
                Thao tác {selectedRowKeys.length > 0 && `(${selectedRowKeys.length})`}
              </Button>
            </Popover>

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
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
        rowSelection={rowSelection}
      />

      <PositionModal
        isModalOpen={isModalOpen}
        isUpdate={isUpdate}
        isView={isView}
        setIsModalOpen={setIsModalOpen}
        onSuccess={() => dispatch(getAllKpiCaNhan(query))}
      />
    </Card>
  );
};

export default Page;