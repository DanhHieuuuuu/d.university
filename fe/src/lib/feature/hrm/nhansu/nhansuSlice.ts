import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IViewNhanSu, IViewThongKeNsTheoPhongBan } from '@models/nhansu/nhansu.model';
import { ReduxStatus } from '@redux/const';
import {
  getListNhanSu,
  getHoSoNhanSu,
  createNhanSuThunk,
  updateNhanSuThunk,
  thongKeNhanSuTheoPhongBanThunk,
  semanticSearchThunk
} from './nhansuThunk';

interface NhanSuState {
  status: ReduxStatus;
  selected: {
    status: ReduxStatus;
    idNhanSu: number;
    data: any | null;
  };
  $create: {
    status: ReduxStatus;
  };
  $update: {
    status: ReduxStatus;
  };
  list: IViewNhanSu[];
  total: number;
  $listThongKe: {
    status: ReduxStatus;
    data: IViewThongKeNsTheoPhongBan[];
  };
}
const initialState: NhanSuState = {
  status: ReduxStatus.IDLE,
  selected: {
    status: ReduxStatus.IDLE,
    idNhanSu: 0,
    data: null
  },
  $create: {
    status: ReduxStatus.IDLE
  },
  $update: {
    status: ReduxStatus.IDLE
  },
  list: [],
  total: 0,
  $listThongKe: {
    status: ReduxStatus.IDLE,
    data: []
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
    selectIdNhanSu: (state, action: PayloadAction<number>) => {
      state.selected.idNhanSu = action.payload;
    },
    clearSelected: (state) => {
      state.selected = { idNhanSu: 0, status: ReduxStatus.IDLE, data: null };
    },
    resetStatusCreate: (state) => {
      state.$create = { status: ReduxStatus.IDLE };
    },
    resetStatusUpdate: (state) => {
      state.$update = { status: ReduxStatus.IDLE };
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
      .addCase(getHoSoNhanSu.pending, (state) => {
        state.selected.status = ReduxStatus.LOADING;
        state.selected.data = null;
      })
      .addCase(getHoSoNhanSu.fulfilled, (state, action: PayloadAction<any>) => {
        state.selected.status = ReduxStatus.SUCCESS;
        state.selected.data = action.payload;
      })
      .addCase(getHoSoNhanSu.rejected, (state) => {
        state.selected.status = ReduxStatus.FAILURE;
        state.selected.data = null;
      })
      .addCase(createNhanSuThunk.pending, (state) => {
        state.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createNhanSuThunk.fulfilled, (state) => {
        state.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createNhanSuThunk.rejected, (state) => {
        state.$create.status = ReduxStatus.FAILURE;
      })
      .addCase(updateNhanSuThunk.pending, (state) => {
        state.$create.status = ReduxStatus.LOADING;
      })
      .addCase(updateNhanSuThunk.fulfilled, (state) => {
        state.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateNhanSuThunk.rejected, (state) => {
        state.$create.status = ReduxStatus.FAILURE;
      })
      .addCase(thongKeNhanSuTheoPhongBanThunk.pending, (state) => {
        state.$listThongKe.status = ReduxStatus.LOADING;
      })
      .addCase(thongKeNhanSuTheoPhongBanThunk.fulfilled, (state, action) => {
        state.$listThongKe.status = ReduxStatus.SUCCESS;
        state.$listThongKe.data = action.payload ?? [];
      })
      .addCase(thongKeNhanSuTheoPhongBanThunk.rejected, (state) => {
        state.$listThongKe.status = ReduxStatus.FAILURE;
      })
      .addCase(semanticSearchThunk.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(semanticSearchThunk.fulfilled, (state, action: PayloadAction<IViewNhanSu[]>) => {
        state.status = ReduxStatus.SUCCESS;
        state.list = action.payload || [];
        state.total = action.payload?.length ?? 0;
      })
      .addCase(semanticSearchThunk.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      });
  }
});

const nhanSuReducer = nhanSuSlice.reducer;

export const { selectIdNhanSu, clearSelected, resetStatusCreate, resetStatusUpdate } = nhanSuSlice.actions;

export default nhanSuReducer;
