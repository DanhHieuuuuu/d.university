import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ICreateHopDongNs, IQueryNhanSu, IViewNhanSu } from '@models/nhansu/nhansu.model';
import { NhanSuService } from '@services/nhansu.service';
import { ReduxStatus } from '@redux/const';

export const getListNhanSu = createAsyncThunk('nhansu/list', async (args: IQueryNhanSu) => {
  try {
    const res = await NhanSuService.findPaging(args);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const getDetailNhanSu = createAsyncThunk('nhansu/get', async (keyword: string) => {
  try {
    const res = await NhanSuService.find(keyword);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const createNhanSu = createAsyncThunk('nhansu/create-hd', async (payload: ICreateHopDongNs) => {
  try {
    const res = await NhanSuService.createHopDong(payload);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

interface NhanSuState {
  status: ReduxStatus;
  selected: {
    status: ReduxStatus;
    maNhanSu: string;
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
    maNhanSu: '',
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
    selectMaNhanSu: (state, action: PayloadAction<string>) => {
      state.selected.maNhanSu = action.payload;
    },
    clearSelected: (state) => {
      state.selected = { maNhanSu: '', status: ReduxStatus.IDLE, data: null };
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
