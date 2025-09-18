import { ILogin, IUser } from '@models/auth/auth.model';
import { IRole } from '@models/role';
import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { AuthService } from '@src/service/auth.service';

export const login = createAsyncThunk(`${process.env.NEXT_PUBLIC_LOGIN_API}`, async (args: ILogin, { rejectWithValue }) => {
  try {
    const res = await AuthService.login(args);
    return res;
  } catch (error) {
    return rejectWithValue(error);
  }
});

interface AuthState {
  user: IUser | null;
  status: string | number | null;
  permissions: string[];
  role: IRole;
  roleId: number;
  isAuthenticated: boolean;
  $login: {
    loading?: boolean;
    data?: null;
  };
}

const initialState: AuthState = {
  user: null,
  status: null,
  permissions: [],
  role: {},
  roleId: 0,
  isAuthenticated: false,
  $login: {
    loading: false,
    data: null
  }
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  selectors: {
    isGranted: (state: AuthState, permission: string) => {
      const permissions = state.role?.permissions?.map((x) => x.name) || [];
      return permissions.includes(permission);
    }
  },
  reducers: {
    setUser(state, action: PayloadAction<Omit<AuthState, 'isAuthenticated'>>) {
      return {
        ...state,
        ...action.payload,
        isAuthenticated: true
      };
    },
    clearUser() {
      return { ...initialState };
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.pending, (state) => {
        state.$login.loading = true;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.$login.loading = false;
        state.user = action.payload.user;
      })
      .addCase(login.rejected, (state) => {
        state.$login.loading = false;
      });
  }
});

const authReducer = authSlice.reducer;

export const { setUser, clearUser } = authSlice.actions;

export default authReducer;
