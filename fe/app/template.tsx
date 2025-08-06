'use client';

import { useEffect } from 'react';

export default function Template({ children }: { children: React.ReactNode }) {
  useEffect(() => {
    console.log('Đã xem trang');
  }, []);

  return <>{children}</>;
}
