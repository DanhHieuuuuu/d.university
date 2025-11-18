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
} from '@redux/feature/danhmucSlice';

import MenuComponent from '@components/menu/menu-home';
import AppHeader from '@components/common/Header';

const { Content, Footer } = Layout;

const Index = ({ children }: { children: React.ReactNode }) => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getAllChucVu());
    dispatch(getAllDanToc());
    dispatch(getAllGioiTinh());
    dispatch(getAllLoaiHopDong());
    dispatch(getAllLoaiPhongBan());
    dispatch(getAllPhongBan());
    dispatch(getAllQuanHeGiaDinh());
    dispatch(getAllQuocTich());
    dispatch(getAllToBoMon());
    dispatch(getAllTonGiao());
  }, []);

  return (
    <Layout hasSider style={{ minHeight: '100vh' }}>
      <MenuComponent />
      <Layout style={{ background: '#F5F5F5' }}>
        <AppHeader />
        <Content style={{ margin: 16, height: '100%', background: '#F5F5F5' }}>{children}</Content>
      </Layout>
    </Layout>
  );
};

export default Index;
