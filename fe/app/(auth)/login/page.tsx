'use client';

import { Button } from 'antd';
import Image from 'next/image';
import { useRouter } from 'next/navigation';

function Index() {
  const router = useRouter();
  return (
    <div>
      <div>Login page</div>
      <Image src="/images/logo.png" alt="logo" width={64} height={64} />
      <Button type="primary" onClick={() => router.back()}>
        Back
      </Button>
    </div>
  );
}

export default Index;
