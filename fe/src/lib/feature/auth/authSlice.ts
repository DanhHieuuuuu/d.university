import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { login, refreshToken, myPermission } from './authThunk';
import { setItem as setToken, clearToken } from '@utils/token-storage';
import { IUser } from '@models/auth/auth.model';

interface AuthState {
  user: IUser | null;
  status: string | number | null;
  permissions: string[];
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
  isAuthenticated: false,
  $login: {
    loading: false,
    data: null
  }
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
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
      })
      .addCase(myPermission.fulfilled, (state, action) => {
        state.permissions = action.payload || [];
      });
  }
});

const authReducer = authSlice.reducer;

export const { setUser, clearUser } = authSlice.actions;

export default authReducer;
