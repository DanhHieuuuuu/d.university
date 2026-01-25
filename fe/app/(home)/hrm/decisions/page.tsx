'use client';

import { ChangeEvent, useState } from 'react';
import { Button, Card, Form, Input, Select } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  LockOutlined,
  UnlockOutlined
} from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';

import AppTable from '@components/common/Table';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { IAction, IColumn } from '@models/common/table.model';
import { getDetailQuyetDinhThunk, getListQuyetDinh } from '@redux/feature/hrm/quyetdinh/quyetdinhThunk';
import { formatDateTimeView } from '@utils/index';
import { IQueryQuyetDinh, IViewQuyetDinh } from '@models/nhansu/quyetdinh.model';
import { ETableColumnType } from '@/constants/e-table.consts';
import { NsQuyetDinhTypeConst } from '@/constants/core/hrm/quyet-dinh-type.const';
import { NsQuyetDinhStatusConst } from '@/constants/core/hrm/quyet-dinh-status.const';
import { selectIdQuyetDinh } from '@redux/feature/hrm/quyetdinh/quyetdinhSlice';

import CreateQuyetDinhModal from './(dialog)/create';
import ViewQuyetDinhModal from './(dialog)/view';
import { useIsGranted } from '@hooks/useIsGranted';

const Page = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { data, status, total: totalItem } = useAppSelector((state) => state.quyetdinhState.$list);

  const hasPermissionCreateDecision = useIsGranted(PermissionCoreConst.CoreButtonCreateHrmDecision);
  const hasPermissionViewDecision = useIsGranted(PermissionCoreConst.CoreButtonViewHrmDecision);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryQuyetDinh>({
    total: totalItem || 0,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      dispatch(getListQuyetDinh(newQuery));
    },
    triggerFirstLoad: true
  });

  const onClickAdd = () => {
    setIsModalOpen(true);
  };

  const handleApproveQuyetDinh = (id: number) => {};

  const onClickView = (id: number) => {
    dispatch(selectIdQuyetDinh(id));
    dispatch(getDetailQuyetDinhThunk(id));
    setIsModalView(true);
  };

  const handleRejectQuyetDinh = (id: number) => {};

  const refreshData = () => {
    dispatch(getListQuyetDinh(query));
  };

  const columns: IColumn<IViewQuyetDinh>[] = [
    {
      key: 'maNhanSu',
      dataIndex: 'maNhanSu',
      title: 'Mã nhân sự'
    },
    {
      key: 'hoTen',
      dataIndex: 'hoTen',
      title: 'Họ tên'
    },
    {
      key: 'loaiQuyetDinh',
      dataIndex: 'loaiQuyetDinh',
      title: 'Loại quyết định',
      type: ETableColumnType.STATUS,
      align: 'center',
      getTagInfo: (status: number) => NsQuyetDinhTypeConst.getTag(status)
    },
    {
      key: 'noiDungTomTat',
      dataIndex: 'noiDungTomTat',
      title: 'Nội dung'
    },
    {
      key: 'ngayHieuLuc',
      dataIndex: 'ngayHieuLuc',
      title: 'Bắt đầu hiệu lực từ',
      align: 'center',
      render: (val: string) => formatDateTimeView(val)
    },
    {
      key: 'status',
      dataIndex: 'status',
      title: 'Trạng thái',
      type: ETableColumnType.STATUS,
      align: 'center',
      getTagInfo: (status: number) => NsQuyetDinhStatusConst.getTag(status)
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Xem',
      tooltip: 'Xem',
      icon: <EyeOutlined />,
      hidden: () => !hasPermissionViewDecision,
      command: (record: IViewQuyetDinh) => onClickView(record.id!)
    },
    {
      label: 'Phê duyệt',
      tooltip: 'Phê duyệt',
      icon: <UnlockOutlined />,
      hidden: (record: IViewQuyetDinh) => {
        const canEdit = !(record.status === NsQuyetDinhStatusConst.TAO_MOI);

        return canEdit;
      },
      command: (record: IViewQuyetDinh) => handleApproveQuyetDinh(record.id!)
    },
    {
      label: 'Từ chối',
      tooltip: 'Từ chối',
      icon: <LockOutlined />,
      hidden: (record: IViewQuyetDinh) => {
        const canEdit = !(record.status === NsQuyetDinhStatusConst.TAO_MOI);

        return canEdit;
      },
      command: (record: IViewQuyetDinh) => handleRejectQuyetDinh(record.id!)
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
      title="Danh sách các quyết định"
      className="h-full"
      extra={
        <Button hidden={!hasPermissionCreateDecision} type="primary" icon={<PlusOutlined />} onClick={onClickAdd}>
          Thêm mới
        </Button>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-4 gap-4">
          <Form.Item<IQueryQuyetDinh> label="Nội dung:" name="Keyword">
            <Input onChange={(e) => handleSearch(e)} />
          </Form.Item>
          <Form.Item<IQueryQuyetDinh> label="Loại quyết định" name="loaiQuyetDinh">
            <Select
              allowClear
              options={NsQuyetDinhTypeConst.options}
              onChange={(value) => onFilterChange({ loaiQuyetDinh: value })}
            />
          </Form.Item>
          <Form.Item<IQueryQuyetDinh> label="Trạng thái" name="status">
            <Select
              allowClear
              options={NsQuyetDinhStatusConst.options}
              onChange={(value) => onFilterChange({ status: value })}
            />
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
                resetFilter();
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
        dataSource={data}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />

      <CreateQuyetDinhModal isModalOpen={isModalOpen} setIsModalOpen={setIsModalOpen} />
      <ViewQuyetDinhModal isModalOpen={isView} setIsModalOpen={setIsModalView} />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuHrmDecision);
