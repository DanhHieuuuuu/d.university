import { createAsyncThunk } from '@reduxjs/toolkit';
import { AuthService } from '@services/auth.service';
import { RoleService } from '@services/role.service';
import { ILogin } from '@models/auth/auth.model';
import { getItem } from '@utils/token-storage';

export const login = createAsyncThunk(`auth/login`, async (args: ILogin, { rejectWithValue }) => {
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

export const myPermissionThunk = createAsyncThunk('auth/permission', async (_, { rejectWithValue }) => {
  try {
    const res = await RoleService.getMyPermission();
    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});
