'use client';

import { useRouter } from 'next/navigation';
import { useSelector } from 'react-redux';
import { useEffect } from 'react';
import type { RootState } from '@redux/store';
import React from 'react';

export function withAuth(WrappedComponent: React.ComponentType, requiredPermission: string) {
  const GuardedComponent = (props: any) => {
    const router = useRouter();
    const userId = useSelector((state: RootState) => state.user.id);
    const permissions = useSelector((state: RootState) => state.user.role?.permissions?.map((x: any) => x.name)) || [];

    useEffect(() => {
      if (userId && !permissions.includes(requiredPermission)) {
        router.replace('/forbidden');
      }
    }, [router, permissions]);

    if (!permissions.includes(requiredPermission)) {
      return null; // or a spinner/loading state while redirecting
    }

    return <WrappedComponent {...props} />;
  };

  return GuardedComponent;
}
