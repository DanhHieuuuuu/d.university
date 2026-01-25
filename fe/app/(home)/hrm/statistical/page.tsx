'use client';

import { useEffect } from 'react';
import { Card, Col, Row } from 'antd';
import { RootState } from '@redux/store';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { thongKeNhanSuTheoPhongBanThunk } from '@redux/feature/hrm/nhansu/nhansuThunk';

const Page = () => {
  const dispatch = useAppDispatch();
  const { data } = useAppSelector((state: RootState) => state.nhanSuState.$listThongKe);

  const total = data?.reduce((sum, item) => sum + (item.soLuongNhanSu ?? 0), 0) ?? 0;

  useEffect(() => {
    dispatch(thongKeNhanSuTheoPhongBanThunk());
  }, [dispatch]);

  return (
    <Card title="Thống kê nhân sự theo đơn vị" extra={<strong>{total}</strong>}>
      <div>
        {data?.map((item) => {
          return (
            <div key={item.id} className="border-b-solid flex items-center justify-between border-b-[1px] py-2">
              <p>{item.tenPhongBan}</p>
              <span>{item.soLuongNhanSu}</span>
            </div>
          );
        })}
      </div>
    </Card>
  );
};

export default Page;
