'use client';
import { useEffect, useMemo, useState } from 'react';
import { Card, Select, Table, Spin, Empty, Tag, Collapse } from 'antd';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getAllNganh } from '@redux/feature/dao-tao/nganhThunk';
import { getAllChuyenNganh } from '@redux/feature/dao-tao/chuyenNganhThunk';
import { getAllMonHoc } from '@redux/feature/dao-tao/monHocThunk';
import { getAllChuongTrinhKhung } from '@redux/feature/dao-tao/chuongTrinhKhungThunk';
import { getAllChuongTrinhKhungMon } from '@redux/feature/dao-tao/chuongTrinhKhungMonThunk';
import { getAllKhoaHoc } from '@redux/feature/danh-muc/danhmucThunk';
import { IViewChuongTrinhKhungMon } from '@models/dao-tao/chuongTrinhKhungMon.model';
import { IViewMonHoc } from '@models/dao-tao/monHoc.model';

import styles from './curriculum.module.css';

// Extended type with joined MonHoc data
interface IChuongTrinhKhungMonWithMonHoc extends IViewChuongTrinhKhungMon {
  monHoc?: IViewMonHoc;
}

interface GroupedCourse {
  hocKy: string;
  courses: IChuongTrinhKhungMonWithMonHoc[];
  totalCredits: number;
}

const Page = () => {
  const dispatch = useAppDispatch();

  // State for filters
  const [selectedNganhId, setSelectedNganhId] = useState<number | null>(null);
  const [selectedChuyenNganhId, setSelectedChuyenNganhId] = useState<number | null>(null);

  // Redux state
  const listKhoaHoc = useAppSelector((state) => state.danhmucState.listKhoaHoc);
  const { data: listNganh, status: nganhStatus } = useAppSelector((state) => state.daotaoState.nganh.$list);
  const { data: listChuyenNganh, status: chuyenNganhStatus } = useAppSelector(
    (state) => state.daotaoState.chuyenNganh.$list
  );
  const { data: listMonHoc, status: monHocStatus } = useAppSelector((state) => state.daotaoState.monHoc.$list);
  const { data: listChuongTrinhKhung, status: chuongTrinhKhungStatus } = useAppSelector(
    (state) => state.daotaoState.chuongTrinhKhung.$list
  );
  const { data: listChuongTrinhKhungMon, status: chuongTrinhKhungMonStatus } = useAppSelector(
    (state) => state.daotaoState.chuongTrinhKhungMon.$list
  );

  // Load danh sách khóa học, ngành và môn học khi mount
  useEffect(() => {
    dispatch(getAllKhoaHoc({ PageSize: 1000 }));
    dispatch(getAllNganh({ PageSize: 1000 }));
    dispatch(getAllMonHoc({ PageSize: 1000 }));
  }, [dispatch]);

  // Load danh sách chuyên ngành khi chọn ngành (sử dụng API filter)
  useEffect(() => {
    if (selectedNganhId) {
      dispatch(getAllChuyenNganh({ NganhId: selectedNganhId, PageSize: 1000 }));
    }
  }, [dispatch, selectedNganhId]);

  // Load chương trình khung khi có đủ filter
  useEffect(() => {
    if (selectedNganhId && selectedChuyenNganhId) {
      dispatch(
        getAllChuongTrinhKhung({
          NganhId: selectedNganhId,
          ChuyenNganhId: selectedChuyenNganhId,
          PageSize: 10
        })
      );
    }
  }, [dispatch, selectedNganhId, selectedChuyenNganhId]);

  // Get current ChuongTrinhKhung
  const currentChuongTrinhKhung = listChuongTrinhKhung?.[0];

  // Load ChuongTrinhKhungMon khi có ChuongTrinhKhung (sử dụng API filter)
  useEffect(() => {
    if (currentChuongTrinhKhung) {
      dispatch(
        getAllChuongTrinhKhungMon({
          ChuongTrinhKhungId: currentChuongTrinhKhung.id,
          PageSize: 1000
        })
      );
    }
  }, [dispatch, currentChuongTrinhKhung]);

  // Handle filter changes
  const handleNganhChange = (value: number) => {
    setSelectedNganhId(value);
    setSelectedChuyenNganhId(null); // Reset chuyên ngành khi đổi ngành
  };

  const handleChuyenNganhChange = (value: number) => {
    setSelectedChuyenNganhId(value);
  };

  // Join ChuongTrinhKhungMon with MonHoc data (không cần filter local vì đã filter từ API)
  const joinedChuongTrinhKhungMon = useMemo((): IChuongTrinhKhungMonWithMonHoc[] => {
    if (!listChuongTrinhKhungMon || !listMonHoc) return [];

    // Create a map for quick lookup
    const monHocMap = new Map(listMonHoc.map((mh) => [mh.id, mh]));

    // Join with MonHoc data
    return listChuongTrinhKhungMon.map((ctkm) => ({
      ...ctkm,
      monHoc: monHocMap.get(ctkm.monHocId)
    }));
  }, [listChuongTrinhKhungMon, listMonHoc]);

  // Group courses by học kỳ
  const groupedCourses = useMemo((): GroupedCourse[] => {
    if (!joinedChuongTrinhKhungMon || joinedChuongTrinhKhungMon.length === 0) {
      return [];
    }

    const grouped = joinedChuongTrinhKhungMon.reduce(
      (acc, course) => {
        const key = course.hocKy || 'Chưa xác định';
        if (!acc[key]) {
          acc[key] = {
            hocKy: key,
            courses: [],
            totalCredits: 0
          };
        }
        acc[key].courses.push(course);
        acc[key].totalCredits += course.monHoc?.soTinChi || 0;
        return acc;
      },
      {} as Record<string, GroupedCourse>
    );

    // Sort by học kỳ
    return Object.values(grouped).sort((a, b) => {
      const numA = parseInt(a.hocKy.replace(/\D/g, '')) || 0;
      const numB = parseInt(b.hocKy.replace(/\D/g, '')) || 0;
      return numA - numB;
    });
  }, [joinedChuongTrinhKhungMon]);

  // Tính tổng tín chỉ
  const totalCredits = useMemo(() => {
    return groupedCourses.reduce((sum, group) => sum + group.totalCredits, 0);
  }, [groupedCourses]);

  // Get Khóa học info
  const currentKhoaHoc = useMemo(() => {
    if (!currentChuongTrinhKhung?.khoaHocId || !listKhoaHoc) return null;
    return listKhoaHoc.find((kh) => kh.id === currentChuongTrinhKhung.khoaHocId);
  }, [currentChuongTrinhKhung, listKhoaHoc]);

  // Table columns
  const columns = [
    {
      title: 'STT',
      key: 'stt',
      width: 60,
      align: 'center' as const,
      render: (_: any, __: any, index: number) => index + 1
    },
    {
      title: 'Tên môn học/Học phần',
      key: 'tenMonHoc',
      width: 250,
      render: (_: any, record: IChuongTrinhKhungMonWithMonHoc) => record.monHoc?.tenMonHoc || '-'
    },
    {
      title: 'Mã Học phần',
      key: 'maMonHoc',
      width: 120,
      align: 'center' as const,
      render: (_: any, record: IChuongTrinhKhungMonWithMonHoc) => record.monHoc?.maMonHoc || '-'
    },
    {
      title: 'Học phần',
      key: 'loaiMonHoc',
      width: 100,
      align: 'center' as const,
      render: (_: any, record: IChuongTrinhKhungMonWithMonHoc) => {
        const trangThai = record.trangThai;
        return <Tag color={trangThai ? 'blue' : 'green'}>{trangThai ? 'Bắt buộc' : 'Tự chọn'}</Tag>;
      }
    },
    {
      title: 'Số TC',
      key: 'soTinChi',
      width: 80,
      align: 'center' as const,
      render: (_: any, record: IChuongTrinhKhungMonWithMonHoc) => record.monHoc?.soTinChi || 0
    },
    {
      title: 'Số tiết LT',
      key: 'soTietLyThuyet',
      width: 100,
      align: 'center' as const,
      render: (_: any, record: IChuongTrinhKhungMonWithMonHoc) => record.monHoc?.soTietLyThuyet || 0
    },
    {
      title: 'Số tiết TH',
      key: 'soTietThucHanh',
      width: 100,
      align: 'center' as const,
      render: (_: any, record: IChuongTrinhKhungMonWithMonHoc) => record.monHoc?.soTietThucHanh || 0
    }
  ];

  const isLoading =
    nganhStatus === ReduxStatus.LOADING ||
    chuyenNganhStatus === ReduxStatus.LOADING ||
    monHocStatus === ReduxStatus.LOADING ||
    chuongTrinhKhungStatus === ReduxStatus.LOADING ||
    chuongTrinhKhungMonStatus === ReduxStatus.LOADING;

  return (
    <Card title="Chương trình khung" className="min-h-full">
      {/* Filter Section */}
      <div className={styles.filterSection}>
        <div className={styles.filterItem}>
          <label>Ngành:</label>
          <Select
            placeholder="Chọn ngành"
            value={selectedNganhId}
            onChange={handleNganhChange}
            loading={nganhStatus === ReduxStatus.LOADING}
            style={{ width: 300 }}
            options={listNganh?.map((nganh) => ({
              value: nganh.id,
              label: nganh.tenNganh
            }))}
            allowClear
          />
        </div>

        <div className={styles.filterItem}>
          <label>Chuyên ngành:</label>
          <Select
            placeholder="Chọn chuyên ngành"
            value={selectedChuyenNganhId}
            onChange={handleChuyenNganhChange}
            loading={chuyenNganhStatus === ReduxStatus.LOADING}
            disabled={!selectedNganhId}
            style={{ width: 300 }}
            options={listChuyenNganh?.map((cn) => ({
              value: cn.id,
              label: cn.tenChuyenNganh
            }))}
            allowClear
          />
        </div>
      </div>

      {/* Content Section */}
      <Spin spinning={isLoading}>
        {!selectedNganhId || !selectedChuyenNganhId ? (
          <Empty description="Vui lòng chọn Ngành và Chuyên ngành để xem chương trình khung" />
        ) : groupedCourses.length === 0 ? (
          <Empty description="Không có dữ liệu chương trình khung" />
        ) : (
          <div className={styles.curriculumContent}>
            {/* Thông tin chương trình khung */}
            {currentChuongTrinhKhung && (
              <div className={styles.curriculumInfo}>
                <h3>{currentChuongTrinhKhung.tenChuongTrinhKhung}</h3>
                <p>Mã: {currentChuongTrinhKhung.maChuongTrinhKhung}</p>
                {currentKhoaHoc && <p>Khóa học: {currentKhoaHoc.tenKhoaHoc}</p>}
              </div>
            )}

            {/* Bảng theo học kỳ - sử dụng Collapse */}
            <Collapse
              className={styles.semesterCollapse}
              defaultActiveKey={[]}
              expandIcon={() => null}
              items={groupedCourses.map((group) => ({
                key: group.hocKy,
                label: (
                  <div className={styles.semesterHeader}>
                    <span className={styles.semesterTitle}>Học kỳ {group.hocKy}</span>
                    <span className={styles.semesterCredits}>{group.totalCredits} TC</span>
                  </div>
                ),
                children: (
                  <Table
                    rowKey="id"
                    columns={columns}
                    dataSource={group.courses}
                    pagination={false}
                    size="small"
                    bordered
                    className={styles.courseTable}
                  />
                )
              }))}
            />

            {/* Tổng kết */}
            <div className={styles.summarySection}>
              <div className={styles.summaryRow}>
                <span className={styles.summaryLabel}>Tổng TC yêu cầu</span>
                <span className={styles.summaryValue + ' ' + styles.required}>
                  {currentChuongTrinhKhung?.tongSoTinChi || totalCredits}
                </span>
              </div>
              <div className={styles.summaryRow}>
                <span className={styles.summaryLabel}>Tổng TC bắt buộc</span>
                <span className={styles.summaryValue + ' ' + styles.required}>{totalCredits}</span>
              </div>
              <div className={styles.summaryRow}>
                <span className={styles.summaryLabel}>Tổng TC tự chọn</span>
                <span className={styles.summaryValue + ' ' + styles.optional}>0</span>
              </div>
            </div>
          </div>
        )}
      </Spin>
    </Card>
  );
};

export default Page;
