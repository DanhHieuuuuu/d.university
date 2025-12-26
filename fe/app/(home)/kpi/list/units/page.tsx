'use client';
import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Dropdown, Form, Input, MenuProps, Modal, Popover, Select, Tag } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  CheckCircleOutlined,
  EllipsisOutlined,
  FilterOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiDonVi } from '@redux/feature/kpi/kpiSlice';
import { deleteKpiDonVi, getAllKpiDonVi, getListNamHocKpiDonVi, getListTrangThaiKpiDonVi, updateTrangThaiKpiDonVi } from '@redux/feature/kpi/kpiThunk';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryKpiDonVi, IViewKpiDonVi } from '@models/kpi/kpi-don-vi.model';
import PositionModal from './(dialog)/create-or-update';
import { KpiLoaiConst } from '../../const/kpiType.const';
import { toast } from 'react-toastify';
import { KpiTrangThaiConst } from '../../const/kpiStatus.const';
import { getAllPhongBan } from '@redux/feature/danh-muc/danhmucThunk';

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiDonVi.$list);
  const { data: listPhongBan } = useAppSelector((state) => state.danhmucState.phongBan.$list);
  const { data: trangThaiDonVi, status: trangThaiStatus } = useAppSelector(
    (state) => state.kpiState.meta.trangThai.donVi
  );

  const { data: namHocDonVi, status: namHocStatus } = useAppSelector(
    (state) => state.kpiState.meta.namHoc.donVi
  );

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  useEffect(() => {
    dispatch(getAllPhongBan({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getListTrangThaiKpiDonVi());
    dispatch(getListNamHocKpiDonVi());
  }, [dispatch]);

  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryKpiDonVi>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllKpiDonVi(newQuery));
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
            updateTrangThaiKpiDonVi({
              ids,
              trangThai: KpiTrangThaiConst.PHE_DUYET
            })
          ).unwrap();
          toast.success('Phê duyệt thành công!');
          setSelectedRowKeys([]);
          dispatch(getAllKpiDonVi(query));
          dispatch(getListTrangThaiKpiDonVi());
          dispatch(getListNamHocKpiDonVi());
        } catch {
          toast.error('Phê duyệt thất bại!');
        }
      }
    });
  };

  const scoreSelected = () => {
    toast.info('Chức năng chấm KPI hàng loạt đang được cập nhật.');
  };

  const bulkActionItems: MenuProps['items'] = [
    {
      key: 'approve',
      label: 'Phê duyệt',
      icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />,
      onClick: () => requiredSelect(approveSelected),
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
      onClick: () => { },
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
          options={namHocDonVi.map(x => ({
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
          options={trangThaiDonVi}
        />
      </Form.Item>

      <div className="flex justify-end gap-2 mt-2">
        <Button
          size="small"
          onClick={() => {
            filterForm.resetFields();
            onFilterChange({ namHoc: undefined, idDonVi: undefined });
            setOpenFilter(false);
          }}
        >
          Reset
        </Button>
      </div>
    </Form>
  );


  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickUpdate = (record: IViewKpiDonVi) => {
    dispatch(setSelectedKpiDonVi(record));
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

  const onClickView = (record: IViewKpiDonVi) => {
    dispatch(setSelectedKpiDonVi(record));
    setIsModalView(true);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickDelete = (record: IViewKpiDonVi) => {
    console.log(record);
    Modal.confirm({
      title: `Xóa Kpi ${record.kpi} của ${record.donVi}?`,
      okText: 'Xóa',
      okType: 'danger',
      cancelText: 'Hủy',
      onOk: async () => {
        try {
          await dispatch(deleteKpiDonVi(record.id)).unwrap();
          toast.success('Xóa thành công!');
          dispatch(getAllKpiDonVi(query));
        } catch (error: any) {
          toast.error(error?.response?.message || 'Xóa thất bại!');
        }
      }
    });
  };

  const columns: IColumn<IViewKpiDonVi>[] = [
    {
      key: 'Id',
      dataIndex: 'id',
      title: 'ID',
      showOnConfig: false
    },
    {
      key: 'kpi',
      dataIndex: 'kpi',
      title: 'Tên KPI'
    },
    {
      key: 'mucTieu',
      dataIndex: 'mucTieu',
      title: 'Mục tiêu'
    },
    {
      key: 'trongSo',
      dataIndex: 'trongSo',
      title: 'Trọng số'
    },
    {
      key: 'loaiKpi',
      dataIndex: 'loaiKpi',
      title: 'Loại KPI',
      render: (value: number) => KpiLoaiConst.getName(value),
    },
    {
      key: 'ketQuaThucTe',
      dataIndex: 'ketQuaThucTe',
      title: 'Kết quả thực tế'
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThai',
      title: 'Trạng thái',
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
      title="Danh sách KPI Đơn vị"
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
            <Form.Item name="idDonVi" noStyle>
              <Select
                placeholder="Tất cả đơn vị"
                style={{ width: 180 }}
                allowClear
                options={listPhongBan?.map(x => ({
                  value: x.id,
                  label: x.tenPhongBan
                }))}
                onChange={(value) => {
                  onFilterChange({ idDonVi: value });
                }}
              />
            </Form.Item>
          </div>

          <div className="flex items-center gap-2">
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
            <Button type="primary" htmlType="submit" icon={<SearchOutlined />} onClick={() => dispatch(getAllKpiDonVi(query))}>
              Tìm kiếm
            </Button>
            <Button
              color="default"
              variant="filled"
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                filterForm.resetFields();
                onFilterChange({ Keyword: '', idDonVi: undefined, loaiKpi: undefined, trangThai: undefined });
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
        onSuccess={() => {
          dispatch(getAllKpiDonVi(query));
          dispatch(getListTrangThaiKpiDonVi());
        }}
      />
    </Card>
  );
};

export default Page;
