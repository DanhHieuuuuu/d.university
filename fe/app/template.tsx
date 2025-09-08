'use client';

import { useEffect } from 'react';
import { useRouter } from 'next/navigation';

export default function Template({ children }: { children: React.ReactNode }) {
  const router = useRouter();

  useEffect(() => {
    checkAuth();
  }, [router]);

  const checkAuth = async () => {
    const accessToken = localStorage.getItem('accessToken');

    if (!accessToken) {
      router.push('/login');
      return;
    } else {
      // call api get me here
    }

    console.log('Đã xem trang');
  };

  return <>{children}</>;
}
