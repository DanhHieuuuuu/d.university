import { createSelector } from '@reduxjs/toolkit';
import { RootState } from '@redux/store';

const selectAuthState = (state: RootState) => state.authState;

export const selectUser = createSelector([selectAuthState], (auth) => auth.user);

export const selectPermissions = createSelector([selectAuthState], (auth) => auth.permissions || []);

export const selectIsAuthenticated = createSelector([selectAuthState], (auth) => auth.isAuthenticated);

export const selectIsGranted = createSelector(
  [selectPermissions, (_: RootState, permission: string) => permission],
  (permissions, permission) => permissions.includes(permission)
);
