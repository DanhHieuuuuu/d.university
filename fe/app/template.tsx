'use client';

import { useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { useAppDispatch } from '@redux/hooks';
import { refreshToken } from '@redux/feature/authSlice';
import { processApiMsgError } from '@utils/index';
import { clearToken, getValidToken } from '@utils/token-storage';

export default function Template({ children }: { children: React.ReactNode }) {
  const router = useRouter();
  const dispatch = useAppDispatch();

  useEffect(() => {
    checkAuth();
  }, [router]);

  const checkAuth = async () => {
    const valid = getValidToken();

    if (!valid) {
      clearToken();
      router.push('/login');
      return;
    }

    if (valid.needRefresh) {
      try {
        await dispatch(refreshToken()).unwrap();
      } catch (err) {
        processApiMsgError(err, 'Có lỗi xảy ra khi refresh token.');
        clearToken();
        router.push('/login');
      }
    } else {
      // token còn hạn → có thể gọi API getMe
      console.log('Đã xem trang');
    }
  };

  return <>{children}</>;
}
