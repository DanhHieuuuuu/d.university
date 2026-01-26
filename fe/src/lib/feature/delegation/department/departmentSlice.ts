import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ReduxStatus } from '@redux/const';

import { IDepartmentSupport } from '@models/delegation/delegation.model';
import {
  createDepartmentSupport,
  getByIdDepartmentSupport,
  getListDepartmentSupport,
  updateDepartmentSupport
} from './departmentThunk';

interface DepartmentState {
  status: ReduxStatus;
  list: IDepartmentSupport[];
  selected: {
    status: ReduxStatus;
    id: number;
    data: IDepartmentSupport | null;
  };
  selectedDelegationIncomingId?: number;
  total: number;
  $create: {
    status: ReduxStatus;
  };
  $update: {
    status: ReduxStatus;
    error?: string;
  };
}

const initialState: DepartmentState = {
  status: ReduxStatus.IDLE,
  list: [],
  selected: {
    status: ReduxStatus.IDLE,
    id: 0,
    data: null
  },
  selectedDelegationIncomingId: undefined,
  total: 0,
  $create: {
    status: ReduxStatus.IDLE
  },
  $update: {
    status: ReduxStatus.IDLE
  }
};

const departmentSlice = createSlice({
  name: 'departmentSupport',
  initialState,
  reducers: {
    selectDepartmentSupport: (state, action: PayloadAction<IDepartmentSupport>) => {
      state.selected = {
        status: ReduxStatus.SUCCESS,
        id: action.payload.id,
        data: action.payload
      };
    },
    selectDelegationIncomingId: (state, action: PayloadAction<number>) => {
      state.selectedDelegationIncomingId = action.payload;
    },
    clearSelectedDepartmentSupport: (state) => {
      state.selected = {
        status: ReduxStatus.IDLE,
        id: 0,
        data: null
      };
      state.selectedDelegationIncomingId = undefined;
    },
    resetCreateStatus: (state) => {
      state.$create.status = ReduxStatus.IDLE;
    }
  },
  extraReducers: (builder) => {
    builder
      // ===== Danh sách phòng ban hỗ trợ =====
      .addCase(getListDepartmentSupport.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getListDepartmentSupport.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.list = action.payload?.items ?? [];
        state.total = action.payload?.totalItem ?? 0;
      })
      .addCase(getListDepartmentSupport.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })

      // ===== Get by id =====
      .addCase(getByIdDepartmentSupport.pending, (state) => {
        state.selected.status = ReduxStatus.LOADING;
      })
      .addCase(getByIdDepartmentSupport.fulfilled, (state, action: PayloadAction<IDepartmentSupport>) => {
        state.selected.status = ReduxStatus.SUCCESS;
        state.selected.data = action.payload;
        state.selected.id = action.payload.id;
      })
      .addCase(getByIdDepartmentSupport.rejected, (state) => {
        state.selected.status = ReduxStatus.FAILURE;
      })

      // ===== Create =====
      .addCase(createDepartmentSupport.pending, (state) => {
        state.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createDepartmentSupport.fulfilled, (state) => {
        state.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createDepartmentSupport.rejected, (state) => {
        state.$create.status = ReduxStatus.FAILURE;
      })

      // ===== Update =====
      .addCase(updateDepartmentSupport.pending, (state) => {
        state.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updateDepartmentSupport.fulfilled, (state) => {
        state.$update.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateDepartmentSupport.rejected, (state, action: any) => {
        state.$update.status = ReduxStatus.FAILURE;
        state.$update.error = action.error?.message;
      });
  }
});
const departmentReducer = departmentSlice.reducer;
export const { selectDepartmentSupport, clearSelectedDepartmentSupport, resetCreateStatus, selectDelegationIncomingId } = departmentSlice.actions;

export default departmentReducer;
