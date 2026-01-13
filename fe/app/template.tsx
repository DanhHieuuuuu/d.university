'use client';

import { useEffect } from 'react';
import { useRouter, usePathname } from 'next/navigation';
import { useAppDispatch } from '@redux/hooks';
import { myPermission, refreshToken } from '@redux/feature/auth/authThunk';
import { processApiMsgError } from '@utils/index';
import { clearToken, getValidToken } from '@utils/token-storage';
import { $fetchNotification } from '@redux/feature/noticeSlice';

export default function Template({ children }: { children: React.ReactNode }) {
  const router = useRouter();
  const pathname = usePathname();
  const dispatch = useAppDispatch();

  useEffect(() => {
    checkAuth();
  }, [router, pathname]);

  const checkAuth = async () => {
    // Skip auth check for public routes
    if (pathname?.startsWith('/survey/user') || pathname?.startsWith('/user/chat-bot')) {
      return;
    }

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
      dispatch($fetchNotification({ PageIndex: 1, PageSize: 10, short: true }));
      console.log('Đã xem trang');
    }
  };

  return <>{children}</>;
}
