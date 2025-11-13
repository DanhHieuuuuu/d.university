'use client';

import { useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { useAppDispatch } from '@redux/hooks';
import { myPermission, refreshToken } from '@redux/feature/auth/authThunk';
import { processApiMsgError } from '@utils/index';
import { clearToken, getValidToken } from '@utils/token-storage';
import { $fetchNotification } from '@redux/feature/noticeSlice';

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
      dispatch(myPermission());
      dispatch($fetchNotification({ short: true }));
      console.log('Đã xem trang');
    }
  };

  return <>{children}</>;
}
