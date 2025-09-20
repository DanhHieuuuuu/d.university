'use client';

import { Spin } from 'antd';
import { useSelector } from 'react-redux';
import { RootState } from '@redux/store';

const GlobalLoading = () => {
  const { isLoading } = useSelector((state: RootState) => state.loadState);

  if (!isLoading) return null;

  return <Spin spinning={isLoading} size="large" tip="Đang tải..." fullscreen />;
};

export default GlobalLoading;
