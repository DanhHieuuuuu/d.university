import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IQueryUser, IUserCreate, IUserView } from '@models/user/user.model';
import { UserService } from '@services/user.service';
import { ReduxStatus } from '@redux/const';

export const getAllUser = createAsyncThunk('user/getAll', async (args: IQueryUser) => {
  try {
    const res = await UserService.getAll(args);
    return res.data; 
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
      return res.data; 
    } catch (error: any) {
      console.error(error);
      throw error;
    }
  }
);

export const updateRolesToUserThunk = createAsyncThunk(
  'user/updateRolesToUser',
  async ({ userId, roleIds }: { userId: number; roleIds: number[] }) => {
    try {
      const res = await UserService.updateRolesToUser(userId, roleIds);
      return { userId, roleIds, res };
    } catch (error: any) {
      console.error(error);
      throw error;
    }
  }
);

export const getUserRolesByIdThunk = createAsyncThunk('user/getRolesByUserId', async (userId: number) => {
  const res = await UserService.getRolesOfUser(userId);
  return res;
});

export const changeStatusUserThunk = createAsyncThunk('user/changeStatus', async (userId: number) => {
  const res = await UserService.changeStatusUser(userId);
  return { userId, newStatus: res.data };
});

interface UserState {
  status: ReduxStatus;
  selected: IUserView | null;
  list: IUserView[];
  total: number;
  $addRoles: {
    status: ReduxStatus;
  };
}

const initialState: UserState = {
  status: ReduxStatus.IDLE,
  selected: null,
  list: [],
  total: 0,
  $addRoles: { status: ReduxStatus.IDLE }
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
        state.total = action.payload?.totalItem;
      })
      .addCase(getAllUser.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      .addCase(createUser.fulfilled, (state, action: PayloadAction<any>) => {
        if (action.payload?.id) {
          state.list.push(action.payload);
        }
      })
      .addCase(updateUser.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(updateUser.fulfilled, (state, action) => {
        state.status = ReduxStatus.SUCCESS;
        if (state.selected) {
          // Cập nhật list local
          const index = state.list.findIndex((u) => u.id === state.selected?.id);
          if (index !== -1) {
            state.list[index] = { ...state.list[index], email: action.meta.arg.Email ?? state.list[index].email };
          }
        }
      })
      .addCase(updateUser.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })

      //
      .addCase(updateRolesToUserThunk.pending, (state) => {
        state.$addRoles.status = ReduxStatus.LOADING;
      })
      .addCase(updateRolesToUserThunk.fulfilled, (state, action) => {
        state.$addRoles.status = ReduxStatus.SUCCESS;
        const { userId, roleIds, res } = action.payload;
        if (res.status === 1) {
          const index = state.list.findIndex((u) => u.id === userId);
          if (index !== -1) {
            state.list[index] = { ...state.list[index], roleIds };
          }
          if (state.selected?.id === userId) {
            state.selected.roleIds = roleIds;
          }
        }
      })
      .addCase(updateRolesToUserThunk.rejected, (state) => {
        state.$addRoles.status = ReduxStatus.FAILURE;
      })

      .addCase(getUserRolesByIdThunk.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getUserRolesByIdThunk.fulfilled, (state, action) => {
        state.status = ReduxStatus.SUCCESS;
        const { userId, roleIds } = action.payload?.data ?? {};
        const index = state.list.findIndex((u) => u.id === userId);
        if (index !== -1) {
          state.list[index] = { ...state.list[index], roleIds };
        }
        if (state.selected && state.selected.id === userId) {
          state.selected = { ...state.selected, roleIds };
        }
      })
      .addCase(getUserRolesByIdThunk.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      .addCase(changeStatusUserThunk.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(changeStatusUserThunk.fulfilled, (state, action) => {
        state.status = ReduxStatus.SUCCESS;
        const { userId, newStatus } = action.payload;
        const newTrangThai = newStatus ? 'Đang hoạt động' : 'Vô hiệu hóa';

        const index = state.list.findIndex((u) => u.id === userId);
        if (index !== -1) {
          state.list[index] = { ...state.list[index], trangThai: newTrangThai };
        }
        if (state.selected?.id === userId) {
          state.selected.trangThai = newTrangThai;
        }
      })
      .addCase(changeStatusUserThunk.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      });
  }
});

const userReducer = userSlice.reducer;
export const { selectUser } = userSlice.actions;
export default userReducer;
