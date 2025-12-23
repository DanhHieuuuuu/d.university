'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Modal, Popover, Select, Tag } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  FilterOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { deleteKpiCaNhan, getAllKpiCaNhan, setSelectedKpiCaNhan } from '@redux/feature/kpiSlice';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import PositionModal from './(dialog)/create-or-update';
import { KpiLoaiConst } from '../../const/kpiType.const';
import { toast } from 'react-toastify';
import { KpiTrangThaiConst } from '../../const/kpiStatus.const';

const Page = () => {
  const [form] = Form.useForm();
  const [filterForm] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiCaNhan.$list);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [openFilter, setOpenFilter] = useState(false);


  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryKpiCaNhan>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllKpiCaNhan(newQuery));
    },
    triggerFirstLoad: true
  });

  const filterContent = (
    <Form
      form={filterForm}
      layout="vertical"
      // ⭐ thay đổi là query luôn
      onValuesChange={(_, values: Partial<IQueryKpiCaNhan>) => {
        onFilterChange(values);
      }}
    >
      {/* ===== LOẠI KPI ===== */}
      <Form.Item<IQueryKpiCaNhan>
        label="Loại KPI"
        name="loaiKpi"
      >
        <Select
          allowClear
          placeholder="Chọn loại KPI"
          options={KpiLoaiConst.list.map(x => ({
            value: x.value,
            label: x.name,
          }))}
        />
      </Form.Item>

      {/* ===== TRẠNG THÁI ===== */}
      <Form.Item<IQueryKpiCaNhan>
        label="Trạng thái"
        name="trangThai"
      >
        <Select
          allowClear
          placeholder="Chọn trạng thái"
          options={KpiTrangThaiConst.list.map(x => ({
            value: x.value,
            label: x.text,
          }))}
        />
      </Form.Item>

      {/* ===== RESET ===== */}
      <div className="flex justify-end">
        <Button
          size="small"
          onClick={() => {
            filterForm.resetFields();
            onFilterChange({
              loaiKpi: undefined,
              trangThai: undefined,
            });
          }}
        >
          Xóa lọc
        </Button>
      </div>
    </Form>
  );

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickUpdate = (record: IViewKpiCaNhan) => {
    dispatch(setSelectedKpiCaNhan(record));
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

  const onClickView = (record: IViewKpiCaNhan) => {
    dispatch(setSelectedKpiCaNhan(record));
    setIsModalView(true);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickDelete = (record: IViewKpiCaNhan) => {
    console.log(record);
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
        } catch (error: any) {
          toast.error(error?.response?.message || 'Xóa thất bại!');
        }
      }
    });
  };

  const columns: IColumn<IViewKpiCaNhan>[] = [
    {
      key: 'Id',
      dataIndex: 'id',
      title: 'ID',
      showOnConfig: false
    },
    {
      key: 'linhVuc',
      dataIndex: 'linhVuc',
      title: 'Lĩnh Vực'
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
      dataIndex: 'loaiKPI',
      title: 'Loại KPI',
      render: (value: number) => KpiLoaiConst.getName(value),
    },
    {
      key: 'nhanSu',
      dataIndex: 'nhanSu',
      title: 'Nhân sự',
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

  const { debounced: debouncedFilter } = useDebouncedCallback(
    (values: Partial<IQueryKpiCaNhan>) => {
      onFilterChange(values);
    },
    300
  );
  const handleSearch = (value: string) => {
    handleDebouncedSearch(value);
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
        {/* HÀNG 1: Ô TÌM KIẾM, CÁC FILTER THÊM VÀ NÚT BỘ LỌC */}
        <div className="flex items-center justify-between mb-4 gap-4">

          {/* Cụm bên trái: Bạn có thể thêm bao nhiêu trường filter vào đây tùy thích */}
          <div className="flex items-center gap-2 flex-1">
            <Input
              placeholder="Tìm KPI..."
              prefix={<SearchOutlined />}
              allowClear
              onChange={(e) => handleSearch(e.target.value)}
              className="max-w-[250px]" // Khống chế độ rộng ô tìm kiếm
            />

            {/* VÍ DỤ: Thêm trường Filter khác ở đây (Select, DatePicker...) */}
            <Select
              placeholder="Chọn loại nhanh"
              style={{ width: 160 }}
              allowClear
              options={KpiLoaiConst.list.map(x => ({ value: x.value, label: x.name }))}
              onChange={(val) => onFilterChange({ loaiKpi: val })}
            />

            {/* Bạn có thể thêm tiếp các Select khác vào đây, chúng sẽ tự dàn hàng ngang */}
          </div>

          {/* Cụm bên phải: Chỉ chứa nút Bộ lọc (Popover) */}
          <div className="flex items-center">
            <Popover
              content={filterContent}
              title="Bộ lọc nâng cao"
              trigger="click"
              open={openFilter}
              onOpenChange={setOpenFilter}
              placement="bottomRight" // Quan trọng để không lệch màn hình
              styles={{ body: { padding: 16, minWidth: 280 } }}
            >
              <Button
                icon={<FilterOutlined />}
                type={openFilter ? "primary" : "default"}
              >
                Bộ lọc
              </Button>
            </Popover>
          </div>
        </div>


        <Form.Item>
          <div className="flex flex-row justify-center space-x-2">
            <Button type="primary" htmlType="submit" icon={<SearchOutlined />}>
              Tìm kiếm
            </Button>
            <Button
              color="default"
              variant="filled"
              icon={<SyncOutlined />}
              onClick={() => {
                form.resetFields();
                form.submit();
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
      />

      <PositionModal
        isModalOpen={isModalOpen}
        isUpdate={isUpdate}
        isView={isView}
        setIsModalOpen={setIsModalOpen}
        onSuccess={() => {
          dispatch(getAllKpiCaNhan(query));
        }}
      />
    </Card>
  );
};

export default Page;
