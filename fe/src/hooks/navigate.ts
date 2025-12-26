'use client';

import { usePathname, useRouter } from 'next/navigation';
import { useEffect, useTransition } from 'react';
import { useDispatch } from 'react-redux';
import { setLoading } from '@redux/feature/loadingSlice';

export function useNavigate() {
  const router = useRouter();
  const pathname = usePathname();
  const dispatch = useDispatch();
  const [isPending, startTransition] = useTransition();

  const navigateTo = (href: string) => {
    dispatch(setLoading(true));
    startTransition(() => {
      router.push(href);
    });
  };

  useEffect(() => {
    if (!isPending) {
      dispatch(setLoading(false));
    }
  }, [isPending, dispatch, pathname]);

  return { navigateTo };
}
