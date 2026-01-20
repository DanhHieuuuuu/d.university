'use client';

import React, { useEffect } from 'react';
import { Layout } from 'antd';

import { useAppDispatch } from '@redux/hooks';
import {
  getAllChucVu,
  getAllDanToc,
  getAllGioiTinh,
  getAllLoaiHopDong,
  getAllLoaiPhongBan,
  getAllPhongBan,
  getAllQuanHeGiaDinh,
  getAllQuocTich,
  getAllToBoMon,
  getAllTonGiao
} from '@redux/feature/danh-muc/danhmucThunk';

import MenuComponent from '@components/menu/menu-home';
import AppHeader from '@components/common/Header';
import NotificationRealtime from './notification/real_time/NotificationRealtime';

const { Content, Footer } = Layout;

const Index = ({ children }: { children: React.ReactNode }) => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getAllChucVu({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getAllDanToc());
    dispatch(getAllGioiTinh());
    dispatch(getAllLoaiHopDong());
    dispatch(getAllLoaiPhongBan());
    dispatch(getAllPhongBan({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getAllQuanHeGiaDinh());
    dispatch(getAllQuocTich());
    dispatch(getAllToBoMon({ PageIndex: 1, PageSize: 1000 }));
    dispatch(getAllTonGiao());
  }, []);

  return (
    <Layout hasSider style={{ height: '100vh' }}>
      <NotificationRealtime />
      <MenuComponent />
      <Layout style={{ background: '#F5F5F5' }}>
        <AppHeader />
        <Content style={{ height: 'calc(100vh - 64px)', padding: 16, background: '#F5F5F5', overflowY: 'auto' }}>
          {children}
        </Content>
      </Layout>
    </Layout>
  );
};

export default Index;
