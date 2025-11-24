import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IViewNhanSu } from '@models/nhansu/nhansu.model';
import { ReduxStatus } from '@redux/const';
import { getListNhanSu, createNhanSu, getDetailNhanSu } from './nhansuThunk';

interface NhanSuState {
  status: ReduxStatus;
  selected: {
    status: ReduxStatus;
    idNhanSu: number;
    data: any | null;
  };
  list: IViewNhanSu[];
  total: number;
  $create: {
    status: ReduxStatus;
  };
}
const initialState: NhanSuState = {
  status: ReduxStatus.IDLE,
  selected: {
    status: ReduxStatus.IDLE,
    idNhanSu: 0,
    data: null
  },
  list: [],
  total: 0,
  $create: {
    status: ReduxStatus.IDLE
  }
};

const nhanSuSlice = createSlice({
  name: 'nhansu',
  initialState,
  selectors: {
    nhanSuSelected: (state: NhanSuState) => {
      return state.selected;
    }
  },
  reducers: {
    selectMaNhanSu: (state, action: PayloadAction<number>) => {
      state.selected.idNhanSu = action.payload;
    },
    clearSelected: (state) => {
      state.selected = { idNhanSu: 0, status: ReduxStatus.IDLE, data: null };
    },
    resetStatusCreate: (state) => {
      state.$create = { status: ReduxStatus.IDLE };
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(getListNhanSu.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getListNhanSu.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.list = action.payload?.items;
        state.total = action.payload?.totalItem;
      })
      .addCase(getListNhanSu.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      .addCase(getDetailNhanSu.pending, (state) => {
        state.selected.status = ReduxStatus.LOADING;
      })
      .addCase(getDetailNhanSu.fulfilled, (state, action: PayloadAction<any>) => {
        state.selected.status = ReduxStatus.SUCCESS;
        state.selected.data = action.payload;
      })
      .addCase(getDetailNhanSu.rejected, (state) => {
        state.selected.status = ReduxStatus.FAILURE;
      })
      .addCase(createNhanSu.pending, (state) => {
        state.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createNhanSu.fulfilled, (state) => {
        state.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createNhanSu.rejected, (state) => {
        state.$create.status = ReduxStatus.FAILURE;
      });
  }
});

const nhanSuReducer = nhanSuSlice.reducer;

export const { selectMaNhanSu, clearSelected, resetStatusCreate } = nhanSuSlice.actions;

export default nhanSuReducer;
