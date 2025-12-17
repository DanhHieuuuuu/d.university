'use client';
import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Modal } from 'antd';
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
import { deleteKpiCaNhan, getAllKpiCaNhan, setSelectedKpiCaNhan } from '@redux/feature/kpiSlice';
import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { IQueryKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import PositionModal from './(dialog)/create-or-update';
import { KpiLoaiConst } from '../../const/kpiType.const';
import { toast } from 'react-toastify';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data: list, status, total: totalItem } = useAppSelector((state) => state.kpiState.kpiCaNhan.$list);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);


  const { query, pagination, onFilterChange } = usePaginationWithFilter<IQueryKpiCaNhan>({
    total: totalItem || 0,
    initialQuery: {
      SkipCount: 0,
      MaxResultCount: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getAllKpiCaNhan(newQuery));
    },
    triggerFirstLoad: true
  });

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
      title: 'Nhân sự'
    },
    {
      key: 'ketQuaThucTe',
      dataIndex: 'ketQuaThucTe',
      title: 'Kết quả thực tế'
    },
    {
      key: 'trangThai',
      dataIndex: 'trangThaiText',
      title: 'Trạng thái'
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
      title="Danh sách KPI Cá nhân"
      className="h-full"
      extra={
        <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2">
          <Form.Item<IQueryKpiCaNhan> label="Tên phòng ban:" name="Keyword">
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
      />
    </Card>
  );
};

export default Page;
