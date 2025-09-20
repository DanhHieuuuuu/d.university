import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { AuthService } from '@services/auth.service';
import { ILogin, IUser } from '@models/auth/auth.model';
import { IRole } from '@models/role';
import { setItem as setToken, clearToken, getItem } from '@utils/token-storage';

export const login = createAsyncThunk(`${process.env.NEXT_PUBLIC_LOGIN_API}`, async (args: ILogin, { rejectWithValue }) => {
  try {
    const res = await AuthService.loginApi(args);
    return res;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const refreshToken = createAsyncThunk('auth/refresh', async (_, { rejectWithValue }) => {
  try {
    const { accessToken, refreshToken } = getItem();

    if (!accessToken || !refreshToken) {
      return rejectWithValue('Không có token');
    }

    const res = await AuthService.refreshTokenApi({
      token: accessToken,
      refreshToken
    });
    return res;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
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
      clearToken();
      return { ...initialState };
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.pending, (state) => {
        state.$login.loading = true;
      })
      .addCase(login.fulfilled, (state, action) => {
        const { remember } = action.meta.arg;
        const { data: result, code } = action.payload;

        // save token
        setToken({
          accessToken: result.token,
          refreshToken: result.refreshToken,
          expiredAccessToken: result.expiredToken,
          expiredRefreshToken: result.expiredRefreshToken,
          remember
        });

        // update state
        state.$login.loading = false;
        state.user = result.user;
        state.isAuthenticated = true;
        state.status = code;
      })
      .addCase(login.rejected, (state) => {
        state.$login.loading = false;
      })
      .addCase(refreshToken.pending, (state) => {
        state.$login.loading = true;
      })
      .addCase(refreshToken.fulfilled, (state, action) => {
        state.$login.loading = false;
        const result = action.payload.data;

        // luôn lưu như "remember" để đảm bảo user không bị đá ra giữa chừng
        setToken({
          accessToken: result.token,
          refreshToken: result.refreshToken,
          expiredAccessToken: result.expiredToken,
          expiredRefreshToken: result.expiredRefreshToken,
          remember: true
        });
      })
      .addCase(refreshToken.rejected, (state) => {
        state.$login.loading = false;
      });
  }
});

const authReducer = authSlice.reducer;

export const { setUser, clearUser } = authSlice.actions;

export default authReducer;
