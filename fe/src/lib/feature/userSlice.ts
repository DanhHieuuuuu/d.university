import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IQueryUser, IUserView } from '@models/user/user.model';
import { UserService } from '@services/user.service';
import { ReduxStatus } from '@redux/const';

export const getAllUser = createAsyncThunk('user/getAll', async (args: IQueryUser) => {
  try {
    const res = await UserService.getAll(args);
    return res.data; // { items, totalItem }
  } catch (error: any) {
    console.error(error);
  }
});

interface UserState {
  status: ReduxStatus;
  selected: IUserView | null;
  list: IUserView[];
  total: number;
}

const initialState: UserState = {
  status: ReduxStatus.IDLE,
  selected: null,
  list: [],
  total: 0
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    selectUser: (state, action: PayloadAction<IUserView>) => {
      state.selected = action.payload;
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(getAllUser.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getAllUser.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.list = action.payload?.items;
        state.total = action.payload?.totalItem; // chú ý API trả về totalItem (ko phải totalItems)
      })
      .addCase(getAllUser.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      });
  }
});

const userReducer = userSlice.reducer;
export const { selectUser } = userSlice.actions;
export default userReducer;
