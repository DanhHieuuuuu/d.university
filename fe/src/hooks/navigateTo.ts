'use client';

import { useRouter } from 'next/navigation';
import { useDispatch } from 'react-redux';
import { setLoading } from '@redux/feature/loadingSlice';

export function useNavigateTo() {
  const router = useRouter();
  const dispatch = useDispatch();

  const navigateTo = async (href: string) => {
    try {
      dispatch(setLoading(true));

      await router.prefetch(href);
      router.push(href);
    } catch (err) {
      console.error('Navigation error:', err);
      router.push(href);
    } finally {
      setTimeout(() => dispatch(setLoading(false)), 100);
    }
  };

  return { navigateTo };
}
