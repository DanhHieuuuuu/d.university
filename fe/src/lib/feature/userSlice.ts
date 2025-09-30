import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IQueryUser, IUserCreate, IUserView } from '@models/user/user.model';
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

export const createUser = createAsyncThunk('user/create', async (body: IUserCreate) => {
  try {
    return await UserService.createUser(body);
  } catch (error: any) {
    console.error(error);
    throw error;
  }
});

export const updateUser = createAsyncThunk(
  'user/update',
  async (body: { Id: number; Email?: string; NewPassword?: string }) => {
    try {
      const res = await UserService.updateUser(body);
      return res.data; // API trả về true/false
    } catch (error: any) {
      console.error(error);
      throw error;
    }
  }
);


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
      })
      .addCase(createUser.fulfilled, (state, action: PayloadAction<any>) => {
        // append vào list sau khi tạo
        if (action.payload) {
          state.list.push(action.payload);
        }
      })
      .addCase(updateUser.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(updateUser.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        if (state.selected) {
          // Cập nhật list local
          const index = state.list.findIndex(u => u.id === state.selected?.id);
          if (index !== -1) {
            state.list[index] = { ...state.list[index], email: action.meta.arg.Email ?? state.list[index].email };
          }
        }
      })
      .addCase(updateUser.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      });
  }
});

const userReducer = userSlice.reducer;
export const { selectUser } = userSlice.actions;
export default userReducer;
