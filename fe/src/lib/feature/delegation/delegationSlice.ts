import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IViewNhanSu } from '@models/nhansu/nhansu.model';
import { ReduxStatus } from '@redux/const';
import {
  createDoanVao,
  createReceptionTime,
  deleteDoanVao,
  getByIdDetailDelegation,
  getByIdGuestGroup,
  getByIdReceptionTime,
  getListDepartmentSupport,
  getListGuestGroup,
  getListNhanSu,
  getListPhongBan,
  getListStatus,
  getLogStatus,
  updateDoanVao,
  updateReceptionTime
} from './delegationThunk';
import { IDepartmentSupport, ILogStatus, IViewGuestGroup } from '@models/delegation/delegation.model';

interface DelegationState {
  status: ReduxStatus;
  selected: {
    status: ReduxStatus;
    id: number;
    data: any | null;
  };
  list: IViewGuestGroup[];
  listDepartmentSupport: IDepartmentSupport[];
  listLogStatus: ILogStatus[];
  listPhongBan: any[];
  listNhanSu: any[];
  listStatus: any[];
  total: number;
  $create: {
    status: ReduxStatus;
  };
}
const initialState: DelegationState = {
  status: ReduxStatus.IDLE,
  selected: {
    status: ReduxStatus.IDLE,
    id: 0,
    data: null
  },
  list: [],
  listDepartmentSupport:[],
  listLogStatus: [],
  listPhongBan: [],
  listNhanSu: [],
  listStatus: [],
  total: 0,
  $create: {
    status: ReduxStatus.IDLE
  }
};

const delegationSlice = createSlice({
  name: 'delegation',
  initialState,
  selectors: {
    delegationSelected: (state: DelegationState) => {
      return state.selected;
    }
  },
  reducers: {
    select: (state, action: PayloadAction<IViewGuestGroup>) => {
      state.selected = {
        id: action.payload.id,
        status: ReduxStatus.SUCCESS,
        data: action.payload
      };
    },

    clearSelected: (state) => {
      state.selected = { id: 0, status: ReduxStatus.IDLE, data: null };
    },
    resetStatusCreate: (state) => {
      state.$create = { status: ReduxStatus.IDLE };
    }
  },
  extraReducers: (builder) => {
    builder
      // Danh sach doan vao
      .addCase(getListGuestGroup.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getListGuestGroup.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.list = action.payload?.items;
        state.total = action.payload?.totalItem;
      })
      .addCase(getListGuestGroup.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      // List Phong Ban
      .addCase(getListPhongBan.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getListPhongBan.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.listPhongBan = action.payload;
      })
      .addCase(getListPhongBan.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      // List Nhan su
      .addCase(getListNhanSu.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getListNhanSu.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.listNhanSu = action.payload;
      })
      .addCase(getListNhanSu.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })

      // List Trang thai
      .addCase(getListStatus.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getListStatus.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.listStatus = action.payload;
      })
      .addCase(getListStatus.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      // Create Đoàn vào
      .addCase(createDoanVao.pending, (state) => {
        state.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createDoanVao.fulfilled, (state) => {
        state.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createDoanVao.rejected, (state) => {
        state.$create.status = ReduxStatus.FAILURE;
      })
      // Update Đoàn vào
      .addCase(updateDoanVao.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(updateDoanVao.fulfilled, (state, action: PayloadAction<IViewGuestGroup>) => {
        state.status = ReduxStatus.SUCCESS;
        const index = state.list.findIndex((item) => item.id === action.payload.id);
        if (index !== -1) {
          state.list[index] = action.payload;
        }
      })
      .addCase(updateDoanVao.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })

      // Delete Đoàn vào
      .addCase(deleteDoanVao.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(deleteDoanVao.fulfilled, (state, action: PayloadAction<number>) => {
        state.status = ReduxStatus.SUCCESS;
        state.list = state.list.filter((item) => item.id !== action.payload);
        state.total -= 1;
      })
      .addCase(deleteDoanVao.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      // Get by Id
      .addCase(getByIdGuestGroup.pending, (state) => {
        state.selected.status = ReduxStatus.LOADING;
      })
      .addCase(getByIdGuestGroup.fulfilled, (state, action: PayloadAction<any>) => {
        state.selected.status = ReduxStatus.SUCCESS;
        state.selected.data = action.payload;
        state.selected.id = action.payload?.id;
      })
      .addCase(getByIdGuestGroup.rejected, (state) => {
        state.selected.status = ReduxStatus.FAILURE;
      })
      // Get by Id Detail
      .addCase(getByIdDetailDelegation.pending, (state) => {
        state.selected.status = ReduxStatus.LOADING;
      })
      .addCase(getByIdDetailDelegation.fulfilled, (state, action: PayloadAction<any>) => {
        state.selected.status = ReduxStatus.SUCCESS;
        state.selected.data = action.payload;
        state.selected.id = action.payload?.id;
      })
      .addCase(getByIdDetailDelegation.rejected, (state) => {
        state.selected.status = ReduxStatus.FAILURE;
      })
      // Get by Id ReceptionTime
      .addCase(getByIdReceptionTime.pending, (state) => {
        state.selected.status = ReduxStatus.LOADING;
      })
      .addCase(getByIdReceptionTime.fulfilled, (state, action: PayloadAction<any>) => {
        state.selected.status = ReduxStatus.SUCCESS;
        state.selected.data = action.payload;
        state.selected.id = action.payload?.id;
      })
      .addCase(getByIdReceptionTime.rejected, (state) => {
        state.selected.status = ReduxStatus.FAILURE;
      })
      // Log Status
      .addCase(getLogStatus.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getLogStatus.fulfilled, (state, action: PayloadAction<any>) => {
        console.log('getLogStatus payload', action.payload);
        state.status = ReduxStatus.SUCCESS;
        state.listLogStatus = action.payload?.items ?? [];
        state.total = action.payload?.totalItem ?? 0;
      })
      .addCase(getLogStatus.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      // update receptionTime
      .addCase(updateReceptionTime.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(updateReceptionTime.fulfilled, (state, action) => {
        state.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateReceptionTime.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      // create ReceptionTime
      .addCase(createReceptionTime.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(createReceptionTime.fulfilled, (state) => {
        state.status = ReduxStatus.SUCCESS;
      })
      .addCase(createReceptionTime.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      // Danh sach phòng ban hõ trợ
      .addCase(getListDepartmentSupport.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getListDepartmentSupport.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.listDepartmentSupport = action.payload?.items;
        state.total = action.payload?.totalItem;
      })
      .addCase(getListDepartmentSupport.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
  }
});

const delegationReducer = delegationSlice.reducer;

export const { select, clearSelected, resetStatusCreate } = delegationSlice.actions;

export default delegationReducer;
