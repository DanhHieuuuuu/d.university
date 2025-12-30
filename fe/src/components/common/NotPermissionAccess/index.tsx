'use client';

import React from 'react';
import { Button, Result } from 'antd';
import { useNavigate } from '@hooks/navigate';

const NotPermissionAccessComponent = () => {
  const { navigateTo } = useNavigate();

  return (
    <div className="flex h-full items-center justify-center">
      <Result
        status="403"
        title="403"
        subTitle="Xin lỗi, bạn không có quyền truy cập vào trang này."
        extra={
          <Button type="primary" onClick={() => navigateTo('/home')}>
            Quay lại Trang chủ
          </Button>
        }
      />
    </div>
  );
};

export default NotPermissionAccessComponent;
