'use client';

import { useEffect } from 'react';
import { usePathname } from 'next/navigation';
import { useAppDispatch } from '@redux/hooks';
import { myPermissionThunk } from '@redux/feature/auth/authThunk';
import { $fetchNotification } from '@redux/feature/noticeSlice';

export default function Template({ children }: { children: React.ReactNode }) {
  const pathname = usePathname();
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (
      pathname?.startsWith('/login') ||
      pathname?.startsWith('/survey/user') ||
      pathname?.startsWith('/user/chat-bot')
    ) {
      return;
    }

    dispatch(myPermissionThunk());

    // disable notification
    // dispatch($fetchNotification({ PageIndex: 1, PageSize: 10, short: true }));
  }, [pathname]);

  return <>{children}</>;
}
