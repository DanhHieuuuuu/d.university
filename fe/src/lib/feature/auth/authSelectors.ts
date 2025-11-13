import { createSelector } from '@reduxjs/toolkit';
import { RootState } from '@redux/store';

const selectAuthState = (state: RootState) => state.authState;

export const selectorUser = createSelector([selectAuthState], (auth) => auth.user);

export const selectorPermissions = createSelector([selectAuthState], (auth) => auth.permissions || []);

export const selectorIsGranted = createSelector(
  [selectorPermissions, (_: RootState, permission: string) => permission],
  (permissions, permission) => permissions.includes(permission)
);
