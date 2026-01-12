import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ReduxStatus } from '@redux/const';
import { IViewHopDong } from '@models/nhansu/hopdong.model';
import { createHopDong, getListHopDong } from './hopdongThunk';

interface HopDongState {
  status: ReduxStatus;
  selected: {
    status: ReduxStatus;
    data: IViewHopDong | null;
  };
  $create: {
    status: ReduxStatus;
  };
  $list: {
    status: ReduxStatus;
    data: IViewHopDong[];
    total: number;
  };
}
const initialState: HopDongState = {
  status: ReduxStatus.IDLE,
  selected: {
    status: ReduxStatus.IDLE,
    data: null
  },
  $create: {
    status: ReduxStatus.IDLE
  },
  $list: {
    status: ReduxStatus.IDLE,
    data: [],
    total: 0
  }
};

const hopdongSlice = createSlice({
  name: 'hopdong',
  initialState,
  reducers: {
    selectNsHopDong: (state, action: PayloadAction<IViewHopDong>) => {
      state.selected.data = action.payload;
    },
    clearSelected: (state) => {
      state.selected = { status: ReduxStatus.IDLE, data: null };
    },
    resetStatusCreate: (state) => {
      state.$create = { status: ReduxStatus.IDLE };
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(getListHopDong.pending, (state) => {
        state.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getListHopDong.fulfilled, (state, action: PayloadAction<any>) => {
        state.$list.status = ReduxStatus.SUCCESS;
        state.$list.data = action.payload?.items;
        state.$list.total = action.payload?.totalItem;
      })
      .addCase(getListHopDong.rejected, (state) => {
        state.$list.status = ReduxStatus.FAILURE;
      })
      .addCase(createHopDong.pending, (state) => {
        state.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createHopDong.fulfilled, (state) => {
        state.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createHopDong.rejected, (state) => {
        state.$create.status = ReduxStatus.FAILURE;
      });
  }
});

const hopdongReducer = hopdongSlice.reducer;

export const { selectNsHopDong, clearSelected, resetStatusCreate } = hopdongSlice.actions;

export default hopdongReducer;
