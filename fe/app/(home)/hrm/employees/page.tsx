'use client';

import { ChangeEvent, useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { Button, Card, Form, Input, Select, Tooltip } from 'antd';
import { EditOutlined, EyeOutlined, PlusOutlined, SearchOutlined, SyncOutlined } from '@ant-design/icons';
import { MicroIcon } from '@components/custom-icon';
import { useNavigate } from '@hooks/navigate';

import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { resetStatusCreate, resetStatusUpdate, selectIdNhanSu } from '@redux/feature/hrm/nhansu/nhansuSlice';
import {
  getHoSoNhanSu,
  getListNhanSu,
  semanticSearchThunk,
  syncQdrantThunk
} from '@redux/feature/hrm/nhansu/nhansuThunk';
import { IQueryNhanSu, IViewNhanSu } from '@models/nhansu/nhansu.model';

import AppTable from '@components/common/Table';
import { IAction, IColumn } from '@models/common/table.model';
import { formatDateView } from '@utils/index';
import { useDebouncedCallback } from '@hooks/useDebounce';
import { usePaginationWithFilter } from '@hooks/usePagination';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { useIsGranted } from '@hooks/useIsGranted';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';

import CreateNhanSuModal from './(dialog)/create-or-update-ns';
import VoiceSearchModal from '@components/common/VoiceSearch';
import CreateContractWithNhanSuModal from '../contracts/(dialog)/CreateContractWithNhanSuModal';

type SearchMode = 'FILTER' | 'SEMANTIC';

const Page = () => {
  const { navigateTo } = useNavigate();
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { list, status, total: totalItem } = useAppSelector((state) => state.nhanSuState);
  const { phongBan } = useAppSelector((state) => state.danhmucState);
  const { status: syncStatus } = useAppSelector((state) => state.nhanSuState.$syncWithQdrant);

  // permission in page
  const hasPermisisonSyncNhanSu = useIsGranted(PermissionCoreConst.CoreButtonSyncNhanSu);
  const hasPermissionCreateNhanSu = useIsGranted(PermissionCoreConst.CoreButtonCreateNhanSu);
  const hasPermisisonUpdateNhanSu = useIsGranted(PermissionCoreConst.CoreButtonUpdateNhanSu);
  const hasPermisisonViewNhanSu = useIsGranted(PermissionCoreConst.CoreButtonViewNhanSu);
  const hasPermisisonCreateContract = useIsGranted(PermissionCoreConst.CoreButtonCreateHrmContract);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isUpdate, setIsModalUpdate] = useState<boolean>(false);
  const [isView, setIsModalView] = useState<boolean>(false);
  const [openContract, setOpenContract] = useState<boolean>(false);
  const [voiceText, setVoiceText] = useState<string | null>(null);

  const [openVoice, setOpenVoice] = useState<boolean>(false);
  const [searchMode, setSearchMode] = useState<SearchMode>('FILTER');

  const columns: IColumn<IViewNhanSu>[] = [
    {
      key: 'maNhanSu',
      dataIndex: 'maNhanSu',
      title: 'Mã NS',
      align: 'center'
    },
    {
      key: 'hoTen',
      dataIndex: 'hoTen',
      title: 'Họ và tên'
    },
    {
      key: 'ngaySinh',
      dataIndex: 'ngaySinh',
      title: 'Ngày sinh',
      render: (value) => {
        const date = formatDateView(value);
        return <p>{date}</p>;
      }
    },
    {
      key: 'noiSinh',
      dataIndex: 'noiSinh',
      title: 'Nơi sinh'
    },
    {
      key: 'soDienThoai',
      dataIndex: 'soDienThoai',
      title: 'Số điện thoại'
    },
    {
      key: 'email',
      dataIndex: 'email',
      title: 'Email'
    },
    {
      key: 'tenChucVu',
      dataIndex: 'tenChucVu',
      title: 'Chức vụ'
    },
    {
      key: 'tenPhongBan',
      dataIndex: 'tenPhongBan',
      title: 'Phòng / Ban'
    }
  ];

  const actions: IAction[] = [
    {
      label: 'Hồ sơ nhân sự',
      icon: <EyeOutlined />,
      hidden: () => !hasPermisisonViewNhanSu,
      command: (record: IViewNhanSu) => onClickView(record)
    },
    {
      label: 'Sửa',
      tooltip: 'Sửa thông tin nhân sự',
      icon: <EditOutlined />,
      hidden: () => !hasPermisisonUpdateNhanSu,
      command: (record: IViewNhanSu) => onClickUpdate(record)
    }
  ];

  const { query, pagination, onFilterChange, resetFilter } = usePaginationWithFilter<IQueryNhanSu>({
    total: totalItem,
    initialQuery: {
      PageIndex: 1,
      PageSize: 10,
      Keyword: ''
    },
    onQueryChange: (newQuery) => {
      if (searchMode === 'FILTER') {
        dispatch(getListNhanSu(newQuery));
      }
    },
    triggerFirstLoad: true
  });

  useEffect(() => {
    if (!isModalOpen) {
      dispatch(resetStatusCreate());
      dispatch(resetStatusUpdate());
      dispatch(getListNhanSu(query));
    }
  }, [isModalOpen]);

  const { debounced: handleDebouncedSearch } = useDebouncedCallback((value: string) => {
    onFilterChange({ Keyword: value });
  }, 500);

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    setSearchMode('FILTER');
    handleDebouncedSearch(event.target.value);
  };

  const onClickAdd = () => {
    setIsModalView(false);
    setIsModalUpdate(false);
    setIsModalOpen(true);
  };

  const onClickView = (data: IViewNhanSu) => {
    dispatch(selectIdNhanSu(data.idNhanSu));
    navigateTo(`/hrm/employees/${data.idNhanSu}`);
  };

  const onClickUpdate = (data: IViewNhanSu) => {
    dispatch(selectIdNhanSu(data.idNhanSu));
    dispatch(getHoSoNhanSu(data.idNhanSu));
    setIsModalView(false);
    setIsModalUpdate(true);
    setIsModalOpen(true);
  };

  const handleSync = async () => {
    try {
      const result = await dispatch(syncQdrantThunk()).unwrap();
      if (result) {
        toast.success(result?.data || 'Đồng bộ Qdrant thành công');
      } else {
        toast.error('Có lỗi phía server đã xảy ra, vui lòng kiểm tra và thử lại sau.');
      }
    } catch (error: any) {
      toast.error(error?.message || 'Có lỗi xảy ra. Vui lòng thử lại sau.');
    }
  };

  return (
    <Card
      title="Danh sách nhân sự"
      className="h-full"
      extra={
        <div className="flex items-center justify-center gap-4">
          <Button icon={<PlusOutlined />} onClick={() => setOpenContract(true)} hidden={!hasPermisisonCreateContract}>
            Thêm mới cùng hợp đồng
          </Button>
          <Button type="primary" icon={<PlusOutlined />} onClick={onClickAdd} hidden={!hasPermissionCreateNhanSu}>
            Thêm mới
          </Button>
          <Tooltip title="Đồng bộ với Qdrant">
            <Button
              hidden={!hasPermisisonSyncNhanSu}
              loading={syncStatus === ReduxStatus.LOADING}
              color="danger"
              variant="solid"
              icon={<SyncOutlined />}
              onClick={handleSync}
            />
          </Tooltip>
        </div>
      }
    >
      <Form form={form} layout="horizontal">
        <div className="grid grid-cols-2 gap-4">
          <Form.Item<IQueryNhanSu> label="Họ tên:" name="Keyword">
            <Input onChange={(e) => handleSearch(e)} allowClear />
          </Form.Item>
          <Form.Item<IQueryNhanSu> label="Phòng ban" name="idPhongBan">
            <Select
              allowClear
              options={phongBan.$list.data?.map((item) => {
                return { label: item.tenPhongBan, value: item.id };
              })}
              onChange={(val) => {
                setVoiceText(null);
                setSearchMode('FILTER');
                onFilterChange({ idPhongBan: val });
              }}
            />
          </Form.Item>
          {voiceText && (
            <div className="result-text-voice-search mb-4">
              <span className="mr-1 font-semibold">Bạn đang tìm:</span>
              <span className="italic">{voiceText}</span>
            </div>
          )}
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
                setVoiceText(null);
                setSearchMode('FILTER');
                resetFilter();
              }}
            >
              Tải lại
            </Button>
            <Button
              type="primary"
              shape="circle"
              aria-label="Nhấn để nỏi"
              icon={<MicroIcon />}
              onClick={() => {
                setVoiceText(null);
                setOpenVoice(true);
              }}
            ></Button>
          </div>
        </Form.Item>
      </Form>

      <AppTable
        loading={status === ReduxStatus.LOADING}
        rowKey="maNhanSu"
        columns={columns}
        dataSource={list}
        listActions={actions}
        pagination={{ position: ['bottomRight'], ...pagination }}
      />

      <CreateNhanSuModal
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
        isUpdate={isUpdate}
        isView={isView}
      />

      <CreateContractWithNhanSuModal isModalOpen={openContract} setIsModalOpen={setOpenContract} />

      <VoiceSearchModal
        open={openVoice}
        timeout={3000}
        onClose={async (resultText) => {
          if (resultText) {
            setSearchMode('SEMANTIC');
            setVoiceText(resultText);

            await dispatch(
              semanticSearchThunk({
                PageIndex: pagination.current!,
                PageSize: pagination.pageSize!,
                Keyword: resultText
              })
            );
          }

          setOpenVoice(false);
        }}
      />
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuHrmDanhSach);
