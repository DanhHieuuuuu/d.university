'use client';

import React from 'react';
import { useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { useAppSelector } from '@redux/hooks';
import { selectorPermissions, selectorUser } from '@redux/feature/auth/authSelectors';

export function withAuthGuard(WrappedComponent: React.ComponentType, requiredPermission: string) {
  const GuardedComponent = (props: any) => {
    const router = useRouter();

    const user = useAppSelector(selectorUser);
    const permissions = useAppSelector(selectorPermissions);

    const hasAccess = permissions.includes(requiredPermission);

    useEffect(() => {
      if (user?.id && !hasAccess) {
        router.replace('/forbidden');
      }
    }, [router, permissions]);

    if (!hasAccess) return null;

    return <WrappedComponent {...props} />;
  };

  return GuardedComponent;
}
