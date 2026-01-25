import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ReduxStatus } from '@redux/const';
import { getDetailQuyetDinhThunk, getListQuyetDinh } from './quyetdinhThunk';
import { IDetailQuyetDinh, IViewQuyetDinh } from '@models/nhansu/quyetdinh.model';

interface QuyetDinhState {
  status: ReduxStatus;
  selected: {
    status: ReduxStatus;
    id: number;
    data: IDetailQuyetDinh | null;
  };
  $create: {
    status: ReduxStatus;
  };
  $list: {
    status: ReduxStatus;
    data: IViewQuyetDinh[];
    total: number;
  };
}
const initialState: QuyetDinhState = {
  status: ReduxStatus.IDLE,
  selected: {
    status: ReduxStatus.IDLE,
    id: 0,
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

const quyetdinhSlice = createSlice({
  name: 'quyetdinh',
  initialState,
  reducers: {
    selectQuyetDinh: (state, action: PayloadAction<IDetailQuyetDinh>) => {
      state.selected.data = action.payload;
    },
    selectIdQuyetDinh: (state, action: PayloadAction<number>) => {
      state.selected.id = action.payload;
    },
    clearSelected: (state) => {
      state.selected = { status: ReduxStatus.IDLE, id: 0, data: null };
    },
    resetStatusCreate: (state) => {
      state.$create = { status: ReduxStatus.IDLE };
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(getListQuyetDinh.pending, (state) => {
        state.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getListQuyetDinh.fulfilled, (state, action: PayloadAction<any>) => {
        state.$list.status = ReduxStatus.SUCCESS;
        state.$list.data = action.payload?.items;
        state.$list.total = action.payload?.totalItem;
      })
      .addCase(getListQuyetDinh.rejected, (state) => {
        state.$list.status = ReduxStatus.FAILURE;
      })
      .addCase(getDetailQuyetDinhThunk.pending, (state) => {
        state.selected.status = ReduxStatus.LOADING;
      })
      .addCase(getDetailQuyetDinhThunk.fulfilled, (state, action: PayloadAction<IDetailQuyetDinh>) => {
        state.selected.status = ReduxStatus.SUCCESS;
        state.selected.data = action.payload;
      })
      .addCase(getDetailQuyetDinhThunk.rejected, (state) => {
        state.selected.status = ReduxStatus.FAILURE;
      });
  }
});

const quyetdinhReducer = quyetdinhSlice.reducer;

export const { selectQuyetDinh, selectIdQuyetDinh, clearSelected, resetStatusCreate } = quyetdinhSlice.actions;

export default quyetdinhReducer;
