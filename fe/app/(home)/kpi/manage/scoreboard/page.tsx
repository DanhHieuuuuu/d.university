'use client';
import { useEffect, useState, useRef, useLayoutEffect } from 'react';
import { Card, Select, Tag, Table, Button, Row, Col, Typography, Empty, Skeleton } from 'antd';
import {
  UserOutlined,
  TeamOutlined,
  ArrowRightOutlined,
  SyncOutlined,
  BankOutlined,
  FilterOutlined
} from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getKpiScoreBoard } from '@redux/feature/kpi/kpiThunk';
import { resetKpiScoreBoard } from '@redux/feature/kpi/kpiSlice';
import { ReduxStatus } from '@redux/const';
import { IUnitScore, IPersonalScore } from '@models/kpi/kpi-scoreboard.model';
import type { ColumnsType } from 'antd/es/table';

const { Title } = Typography;

const KpiScoreBoardPage = () => {
  const dispatch = useAppDispatch();
  const [namHoc, setNamHoc] = useState<string>('2026');
  const [viewUnitId, setViewUnitId] = useState<number | null>(null);
  const [scrollY, setScrollY] = useState<number>(0);
  const tableWrapperRef = useRef<HTMLDivElement>(null);
  const kpiScoreBoardState = useAppSelector((state) => state.kpiState?.kpiScoreBoard);
  const data = kpiScoreBoardState?.data;
  const status = kpiScoreBoardState?.status || ReduxStatus.IDLE;
  const isLoading = status === ReduxStatus.LOADING;

  useEffect(() => {
    fetchData();
    return () => {
      dispatch(resetKpiScoreBoard());
    };
  }, [dispatch, namHoc, viewUnitId]);

  const fetchData = () => {
    dispatch(getKpiScoreBoard({ NamHoc: namHoc, ViewUnitId: viewUnitId }));
  };
  useLayoutEffect(() => {
    const observer = new ResizeObserver((entries) => {
      const container = entries[0];
      if (container) {
        const height = container.contentRect.height;

        if (height > 50) {
          setScrollY(height - 45);
        }
      }
    });
    if (tableWrapperRef.current) {
      observer.observe(tableWrapperRef.current);
    }
    return () => observer.disconnect();
  }, []);
  const renderScoreBadge = (score: number | undefined, isFinalized: boolean) => {
    const val = score ?? 0;
    const color = isFinalized ? 'text-green-600' : 'text-orange-500';
    return (
      <div className="flex flex-col items-center">
        <span className={`text-xl font-bold ${color}`}>{val.toFixed(2)}</span>
        {isFinalized ? (
          <Tag color="success" className="mr-0 mt-1">
            CHÍNH THỨC
          </Tag>
        ) : (
          <Tag color="warning" className="mr-0 mt-1">
            TẠM TÍNH
          </Tag>
        )}
      </div>
    );
  };

  const renderMyScore = () => {
    if (isLoading && !data) {
      return (
        <div className="mb-4 shrink-0 rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
          <Skeleton avatar active paragraph={{ rows: 1 }} />
        </div>
      );
    }

    if (!data?.myScore) return null;
    const { diemTongKet, xepLoai, isFinalized, chucVuChinh, hoTen } = data.myScore;

    return (
      <div className="mb-4 shrink-0 rounded-lg border border-blue-100 bg-gradient-to-r from-blue-50 to-indigo-50 p-4 shadow-sm">
        <Row align="middle" justify="space-between" gutter={[16, 16]}>
          <Col xs={24} md={14}>
            <div className="flex items-center gap-4">
              <div className="rounded-full bg-white p-3 text-blue-600 shadow-md">
                <UserOutlined style={{ fontSize: '24px' }} />
              </div>
              <div>
                <Title level={5} style={{ margin: 0, color: '#1e3a8a' }}>
                  {hoTen}
                </Title>
                <div className="mt-1 flex flex-wrap items-center gap-2">
                  <Tag color="blue">{chucVuChinh}</Tag>
                </div>
              </div>
            </div>
          </Col>
          <Col xs={24} md={10}>
            <div className="flex items-center justify-end gap-6 rounded-xl border border-gray-100 bg-white px-4 py-2 shadow-sm">
              <div className="text-center">
                <div className="text-[10px] font-semibold uppercase text-gray-400">Điểm tổng kết</div>
                {renderScoreBadge(diemTongKet, isFinalized)}
              </div>
              <div className="h-8 w-[1px] bg-gray-200"></div>
              <div className="text-center">
                <div className="text-[10px] font-semibold uppercase text-gray-400">Xếp loại</div>
                <span className="text-lg font-bold text-blue-800">{xepLoai || '--'}</span>
              </div>
            </div>
          </Col>
        </Row>
      </div>
    );
  };

  const renderTable = () => {
    const isSchoolMode = data?.viewMode === 'SCHOOL';
    const isUnitMode = data?.viewMode === 'UNIT';

    if (data?.viewMode === 'PERSONAL') {
      return;
    }

    const schoolColumns: ColumnsType<IUnitScore> = [
      { title: 'STT', width: 50, align: 'center', fixed: 'left', render: (_, __, i) => i + 1 },
      {
        title: 'Tên Đơn vị',
        dataIndex: 'tenDonVi',
        key: 'tenDonVi',
        width: 280,
        render: (t) => <b className="text-gray-700">{t}</b>
      },
      {
        title: 'Điểm KPI',
        dataIndex: 'diemKpiDonVi',
        align: 'center',
        width: 100,
        render: (v, r) => (
          <span className={r.isFinalized ? 'font-bold text-green-600' : 'font-bold text-orange-500'}>
            {v?.toFixed(2)}
          </span>
        )
      },
      {
        title: 'Trạng thái',
        dataIndex: 'isFinalized',
        align: 'center',
        width: 120,
        render: (v) => (v ? <Tag color="success">Đã chốt</Tag> : <Tag color="warning">Tạm tính</Tag>)
      },
      {
        title: 'Xếp loại',
        dataIndex: 'xepLoaiDonVi',
        align: 'center',
        width: 180,
        render: (v) => <Tag color="geekblue">{v}</Tag>
      },
      {
        title: 'Thao tác',
        align: 'center',
        width: 90,
        fixed: 'right',
        render: (_, r) => (
          <Button type="link" size="small" icon={<ArrowRightOutlined />} onClick={() => setViewUnitId(r.idDonVi)}>
            Chi tiết
          </Button>
        )
      }
    ];

    const unitColumns: ColumnsType<IPersonalScore> = [
      { title: 'STT', width: 50, align: 'center', fixed: 'left', render: (_, __, i) => i + 1 },
      {
        title: 'Họ và tên',
        dataIndex: 'hoTen',
        key: 'hoTen',
        width: 220,
        render: (t) => <span className="font-medium">{t}</span>
      },
      { title: 'Chức vụ', dataIndex: 'chucVuChinh', key: 'chucVuChinh', width: 200 },
      {
        title: 'Điểm Tổng',
        dataIndex: 'diemTongKet',
        align: 'center',
        width: 100,
        render: (v, r) => (
          <span className={r.isFinalized ? 'font-bold text-green-600' : 'font-bold text-orange-500'}>
            {v?.toFixed(2)}
          </span>
        )
      },
      {
        title: 'Trạng thái',
        dataIndex: 'isFinalized',
        align: 'center',
        width: 120,
        render: (v) => (v ? <Tag color="success">Đã chốt</Tag> : <Tag color="warning">Tạm tính</Tag>)
      },
      {
        title: 'Xếp loại',
        dataIndex: 'xepLoai',
        align: 'center',
        width: 180,
        render: (v) => <Tag color="blue">{v}</Tag>
      }
    ];

    const title = isSchoolMode ? (
      <>
        <BankOutlined /> DANH SÁCH CÁC ĐƠN VỊ ({data?.allUnits?.length || 0})
      </>
    ) : (
      <>
        <TeamOutlined /> NHÂN SỰ: {data?.currentUnitScore?.tenDonVi} ({data?.staffScores?.length || 0})
      </>
    );

    const scoreBadge = isSchoolMode
      ? data?.schoolScore && (
          <div className="flex items-center gap-2 rounded border border-blue-100 bg-blue-50 px-2 py-1 text-xs">
            <span className="font-semibold text-gray-600">Điểm Trường:</span>
            {renderScoreBadge(data.schoolScore.diemKpiTruong, data.schoolScore.isFinalized)}
          </div>
        )
      : data?.currentUnitScore && (
          <div className="flex items-center gap-2 rounded border border-green-100 bg-green-50 px-2 py-1 text-xs">
            <span className="font-semibold text-gray-600">Điểm Đơn vị:</span>
            {renderScoreBadge(data.currentUnitScore.diemKpiDonVi, data.currentUnitScore.isFinalized)}
          </div>
        );

    const dataSource: any = isSchoolMode ? data?.allUnits : data?.staffScores;
    const columns: any = isSchoolMode ? schoolColumns : unitColumns;

    return (
      <div className="flex h-full flex-col">
        <div className="mb-2 flex shrink-0 items-center justify-between">
          <div className="flex items-center gap-2 overflow-hidden">
            {viewUnitId !== null && isUnitMode && (
              <Button
                icon={<ArrowRightOutlined className="rotate-180" />}
                size="small"
                onClick={() => setViewUnitId(null)}
              >
                Quay lại
              </Button>
            )}
            <h3 className="m-0 flex items-center gap-2 truncate text-base font-bold uppercase text-gray-800">
              {title}
            </h3>
          </div>
          <div className="ml-2 shrink-0">{scoreBadge}</div>
        </div>
        <div className="relative flex-1 overflow-hidden" ref={tableWrapperRef}>
          <Table
            dataSource={dataSource || []}
            columns={columns}
            rowKey={isSchoolMode ? 'idDonVi' : 'idNhanSu'}
            pagination={false}
            bordered
            size="small"
            loading={isLoading}
            scroll={{ x: 1000, y: scrollY }}
            rowClassName={(_, index) => (index % 2 === 0 ? 'bg-white' : 'bg-gray-50')}
            locale={{ emptyText: <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} description="Không có dữ liệu" /> }}
          />
        </div>
      </div>
    );
  };

  return (
    <Card
      className="kpi-scoreboard-card flex h-full flex-col shadow-md"
      styles={{
        body: { flex: 1, overflow: 'hidden', display: 'flex', flexDirection: 'column', height: '100%' }
      }}
      title={
        <div className="flex items-center gap-3">
          <div className="h-6 w-1 rounded-full bg-gradient-to-b from-blue-500 to-purple-600" />
          <span className="bg-gradient-to-r from-blue-700 to-purple-700 bg-clip-text text-lg font-bold text-transparent">
            BẢNG THÀNH TÍCH KPI
          </span>
        </div>
      }
    >
      <div className="mb-4 flex shrink-0 items-center gap-2">
        <div className="font-medium">Năm học:</div>
        <Select
          value={namHoc}
          onChange={setNamHoc}
          style={{ width: 120 }}
          options={[{ value: '2026', label: '2026' }]}
        />
        <Button icon={<SyncOutlined />} onClick={fetchData}>
          Tải lại
        </Button>
      </div>
      <div className="flex h-full min-h-0 flex-1 flex-col overflow-hidden">
        {renderMyScore()}

        <div className="relative min-h-0 flex-1 overflow-hidden">{renderTable()}</div>
      </div>
    </Card>
  );
};

export default KpiScoreBoardPage;
