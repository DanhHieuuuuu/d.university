import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ReduxStatus } from '@redux/const';
import { getListQuyetDinh } from './quyetdinhThunk';
import { IViewQuyetDinh } from '@models/nhansu/quyetdinh.model';

interface QuyetDinhState {
  status: ReduxStatus;
  selected: {
    status: ReduxStatus;
    data: IViewQuyetDinh | null;
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
    selectQuyetDinh: (state, action: PayloadAction<IViewQuyetDinh>) => {
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
      });
  }
});

const quyetdinhReducer = quyetdinhSlice.reducer;

export const { selectQuyetDinh, clearSelected, resetStatusCreate } = quyetdinhSlice.actions;

export default quyetdinhReducer;
