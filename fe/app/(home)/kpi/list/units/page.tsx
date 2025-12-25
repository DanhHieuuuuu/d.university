'use client';
import { ChangeEvent, useEffect, useState } from 'react';
import { Button, Card, Form, Input, Modal, Tag } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { setSelectedKpiDonVi } from '@redux/feature/kpi/kpiSlice';
import { deleteKpiDonVi, getAllKpiDonVi } from '@redux/feature/kpi/kpiThunk';
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
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiDonVi.$list);
  const { data: listPhongBan } = useAppSelector((state) => state.danhmucState.phongBan.$list);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [openFilter, setOpenFilter] = useState(false);
  const [openBulkAction, setOpenBulkAction] = useState(false); 
  const [selectedRowKeys, setSelectedRowKeys] = useState<React.Key[]>([]);

  useEffect(() => {
      dispatch(getAllPhongBan({ PageIndex: 1, PageSize: 1000 }));
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

  // const handlePhongBanChange = (value: number | undefined) => {
  //     onFilterChange({ idDonVi: value, idNhanSu: undefined });
  //     form.setFieldValue('idNhanSu', undefined);
  //     dispatch(getAllUser({ IdPhongBan: value, PageIndex: 1, PageSize: 2000 }));
  //   };

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
        <div className="grid grid-cols-2">
          <Form.Item<IQueryKpiDonVi> label="Tên đơn vị:" name="Keyword">
            <Input onChange={(e) => handleSearch(e)} />
          </Form.Item>
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
          dispatch(getAllKpiDonVi(query));
        }}
      />
    </Card>
  );
};

export default Page;
