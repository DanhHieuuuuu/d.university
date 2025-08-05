'use client';

import { Button } from 'antd';
import { useRouter } from 'next/navigation';

export default function Home() {
  const router = useRouter();

  return (
    <div>
      <div>Home page</div>
      <Button type="primary" onClick={() => router.push('/login')}>
        Go to login page
      </Button>
    </div>
  );
}