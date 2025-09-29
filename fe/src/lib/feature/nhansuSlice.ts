import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IQueryNhanSu, IViewNhanSu } from '@models/nhansu/nhansu.model';
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

interface NhanSuState {
  status: ReduxStatus;
  selected: IViewNhanSu | null;
  list: IViewNhanSu[];
  total: number;
}
const initialState: NhanSuState = {
  status: ReduxStatus.IDLE,
  selected: null,
  list: [],
  total: 0
};

const nhanSuSlice = createSlice({
  name: 'nhansu',
  initialState,
  reducers: {
    selectNhanSu: (state, action: PayloadAction<IViewNhanSu>) => {
      state.selected = action.payload;
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
        state.total = action.payload?.totalItems;
      })
      .addCase(getListNhanSu.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      });
  }
});

const nhanSuReducer = nhanSuSlice.reducer;

export const { selectNhanSu } = nhanSuSlice.actions;

export default nhanSuReducer;
