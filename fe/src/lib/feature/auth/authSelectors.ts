import { createSelector } from '@reduxjs/toolkit';
import { RootState } from '@redux/store';


export const selectorUser = (state: RootState) => state.authState?.user ?? null;

export const selectorPermissions = (state: RootState) => state.authState?.permissions ?? [];

// chuyển đổi permissions từ Array sang Set để tìm kiếm nhanh hơn
const selectPermissionSet = createSelector([selectorPermissions], (permissions) => new Set(permissions));

export const selectorIsGranted = createSelector(
  [selectPermissionSet, (_: RootState, permission: string) => permission],
  (permissions, permission) => permissions.has(permission)
);
