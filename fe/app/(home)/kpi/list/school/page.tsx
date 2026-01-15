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
  SaveOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiTruong } from '@redux/feature/kpi/kpiSlice';
import { deleteKpiTruong, getAllKpiTruong, getListNamHocKpiTruong, getListTrangThaiKpiTruong, updateKetQuaCapTrenKpiTruong, updateTrangThaiKpiTruong } from '@redux/feature/kpi/kpiThunk';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
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

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const { processUpdateStatus } = useKpiStatusAction();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiTruong.$list);
  const { data: trangThaiTruong, status: trangThaiStatus } = useAppSelector(
    (state) => state.kpiState.meta.trangThai.truong
  );

  const { data: namHocTruong, status: namHocStatus } = useAppSelector(
    (state) => state.kpiState.meta.namHoc.truong
  );

  type ModalMode = 'create' | 'update' | 'view' | null;
  const [modalMode, setModalMode] = useState<ModalMode>(null);
  const [openChamModal, setOpenChamModal] = useState(false);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);
  const [ketQuaCapTrenMap, setKetQuaCapTrenMap] = useState<Record<number, number | undefined>>({});
  const [activeLoaiKpi, setActiveLoaiKpi] = useState<number>(KpiLoaiConst.CHUC_NANG);
  const KPI_TABS = KpiLoaiConst.list.map(x => ({
    key: String(x.value),
    label: `KPI ${x.name}`,
  }));
  useEffect(() => {
    dispatch(getListTrangThaiKpiTruong());
    dispatch(getListNamHocKpiTruong());
  }, [dispatch]);

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryKpiTruong>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: '',
      loaiKpi: KpiLoaiConst.CHUC_NANG,
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



  const approveSelected = () => {
    const ids = selectedRowKeys.map(Number);
    const kpiToApprove = list.filter(
      (item) => ids.includes(item.id) && item.trangThai === KpiTrangThaiConst.DE_XUAT
    );

    if (kpiToApprove.length === 0) {
      toast.error('Chỉ có KPI đang ở trạng thái "Đề xuất" mới được phê duyệt.');
      return;
    }

    Modal.confirm({
      title: 'Phê duyệt hàng loạt',
      content: `Xác nhận phê duyệt ${kpiToApprove.length} KPI đã chọn?`,
      okText: 'Duyệt',
      onOk: async () => {
        try {
          await dispatch(
            updateTrangThaiKpiTruong({
              ids,
              trangThai: KpiTrangThaiConst.PHE_DUYET
            })
          ).unwrap();
          toast.success('Phê duyệt thành công!');
          setSelectedRowKeys([]);
          dispatch(getAllKpiTruong(query));
          dispatch(getListTrangThaiKpiTruong());
          dispatch(getListNamHocKpiTruong());
        } catch {
          toast.error('Phê duyệt thất bại!');
        }
      }
    });
  };

  const scoreSelected = () => {
    if (!selectedRowKeys.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
      return;
    }
    const validItems = list.filter(
      kpi =>
        selectedRowKeys.includes(kpi.id) &&
        kpi.trangThai == KpiTrangThaiConst.DA_GUI_CHAM
    );

    if (!validItems.length) {
      toast.warning('Chỉ KPI đang ở trạng thái "Đã gửi chấm" mới được chấm');
      return;
    }
    setOpenChamModal(true);
    setSelectedRowKeys(validItems.map(k => k.id));
  };

  const handleSubmitScore = async (note?: string) => {
    try {
      await dispatch(
        updateTrangThaiKpiTruong({
          ids: selectedRowKeys.map(Number),
          trangThai: KpiTrangThaiConst.DA_CHAM,
          note,
        })
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
      },
    });

  const updateKetQuaCapTren = (id: number, value?: number) => {
    setKetQuaCapTrenMap(prev => ({
      ...prev,
      [id]: value
    }));
  };

  const syncKetQuaThucTeToCapTren = () => {
    if (!selectedRowKeys.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
      return;
    }
    const newMap = { ...ketQuaCapTrenMap };
    list
      .filter(item => selectedRowKeys.includes(item.id))
      .forEach(item => {
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
      await dispatch(updateKetQuaCapTrenKpiTruong({ items })).unwrap();
      toast.success('Lưu kết quả đánh giá thành công');
      setSelectedRowKeys([]);
      setKetQuaCapTrenMap({})
      dispatch(getAllKpiTruong(query));
    } catch {
      toast.error('Lưu kết quả thất bại');
    }
  };

  const bulkActionItems: MenuProps['items'] = [
    {
      key: 'cancelScore',
      label: 'Hủy kết quả chấm KPI',
      icon: <UndoOutlined style={{ color: '#1890ff' }} />,
      onClick: () => requiredSelect(cancelScoredSelected),
    },
    {
      key: 'score',
      label: 'Chấm KPI',
      icon: <EditOutlined style={{ color: '#1890ff' }} />,
      onClick: () => requiredSelect(scoreSelected),
    },
    {
      key: 'SYNC',
      label: 'Đồng bộ kết quả thực tế',
      icon: <EditOutlined style={{ color: '#1890ff' }} />,
      onClick: () => requiredSelect(syncKetQuaThucTeToCapTren),
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

      <Form.Item name="namHoc" label="Năm học">
        <Select
          allowClear
          placeholder="Chọn năm học"
          loading={namHocStatus === ReduxStatus.LOADING}
          options={namHocTruong.map((x: any) => ({
            value: x.namHoc,
            label: x.namHoc
          }))}
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

      <div className="flex justify-end gap-2 mt-2">
        <Button
          size="small"
          onClick={() => {
            filterForm.resetFields();
            onFilterChange({ namHoc: undefined });
            setOpenFilter(false);
          }}
        >
          Reset
        </Button>
      </div>
    </Form>
  );


  const onClickAdd = () => setModalMode('create');
  const onClickUpdate = (record: IViewKpiTruong) => { dispatch(setSelectedKpiTruong(record)); setModalMode('update'); };
  const onClickView = (record: IViewKpiTruong) => {
    dispatch(setSelectedKpiTruong(record)); setModalMode('view');
  };

  const onClickDelete = (record: IViewKpiTruong) => {
    console.log(record);
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
          dispatch(getListTrangThaiKpiTruong());
          dispatch(getListNamHocKpiTruong());
        } catch (error: any) {
          toast.error(error?.response?.message || 'Xóa thất bại!');
        }
      }
    });
  };

  const columns: IColumn<IViewKpiTruong>[] = [
    {
      key: 'linhVuc',
      dataIndex: 'linhVuc',
      title: 'Lĩnh Vực',
      width: 150
    },
    {
      key: 'chienLuoc',
      dataIndex: 'chienLuoc',
      title: 'Mục tiêu chiến lược',
      width: 150
    },
    {
      key: 'kpi',
      dataIndex: 'kpi',
      title: 'Tên KPI',
      width: 400,
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
    },
    {
      key: 'loaiKpi',
      dataIndex: 'loaiKpi',
      title: 'Loại KPI',
      width: 140,
      render: (value: number) => KpiLoaiConst.getName(value),
    },
    {
      key: 'ketQuaThucTe',
      dataIndex: 'ketQuaThucTe',
      title: 'Kết quả thực tế',
      width: 180,
      render: (val, record) =>
        formatKetQua(val, record.loaiKetQua),
    },
    {
      key: 'diemKpi',
      dataIndex: 'diemKpi',
      title: 'Điểm tự đánh giá',
      width: 130,
      align: 'center',
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
          />
        );
      },
    },
    {
      key: 'diemKpiCapTren',
      dataIndex: 'diemKpiCapTren',
      title: 'Điểm cấp trên',
      width: 130,
      align: 'center',
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
      type: ETableColumnType.STATUS,
      render: (value: number) => {
        const status = KpiTrangThaiConst.get(value);
        return status ? (
          <Tag color={status.color}>{status.text}</Tag>
        ) : null;
      },
    },
  ];

  const actions: IAction[] = [
    {
      label: 'Chi tiết',
      tooltip: 'Xem thông tin phòng ban',
      icon: <EyeOutlined />,
      command: onClickView
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin phòng ban',
      icon: <EditOutlined />,
      command: onClickUpdate
    },
    {
      label: 'Xóa',
      color: 'red',
      icon: <DeleteOutlined />,
      command: onClickDelete
    }
  ];

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    handleDebouncedSearch(event.target.value);
  };

  // const handleSelectAllPages = async () => {
  //     try {
  //       const allIds = await dispatch(getAllIdsKpiCaNhan({
  //         ...query,
  //         PageIndex: 1,
  //         PageSize: totalItem || 9999,
  //       })).unwrap();

  //       setSelectedRowKeys(allIds);
  //       toast.success(`Đã chọn tất cả ${allIds.length} KPI`);
  //     } catch (error) {
  //       toast.error('Không thể lấy danh sách ID');
  //     }
  //   };

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
        },
      },
    ],
  };

  return (
    <Card
      title="Danh sách KPI Trường"
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
            <Button
              color="default"
              variant="filled"
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                filterForm.resetFields();
                onFilterChange({ Keyword: '', loaiKpi: undefined, trangThai: undefined });
                setSelectedRowKeys([]);
              }}
            >
              Tải lại
            </Button>
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
            PageIndex: 1,
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
          fixed: 'left',
        }}
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
        title="Chấm KPI Đơn vị"
        trangThai={KpiTrangThaiConst.DA_CHAM}
        onCancel={() => setOpenChamModal(false)}
        onSubmit={handleSubmitScore}
      />
      <KpiAiChat />
    </Card>
  );
};

export default Page;
