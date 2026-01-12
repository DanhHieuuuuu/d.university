import { RootState } from '@redux/store';
import { createSelector } from '@reduxjs/toolkit';

const stateNhanSu = (state: RootState) => state.nhanSuState;

export const selectedNhanSu = createSelector([stateNhanSu], (ns) => ns.selected.data);
export const selectedNhanSuStatus = createSelector([stateNhanSu], (ns) => ns.selected.status);
