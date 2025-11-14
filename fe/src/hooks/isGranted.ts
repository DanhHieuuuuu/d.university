'use client';

import { useSelector } from 'react-redux';
import type { RootState } from '@redux/store';
import { selectorIsGranted } from '@redux/feature/auth/authSelectors';

export function isGranted(permission?: string) {
  return useSelector((state: RootState) => (permission ? selectorIsGranted(state, permission) : true));
}
