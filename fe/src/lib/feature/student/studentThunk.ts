import { createAsyncThunk } from '@reduxjs/toolkit';
import { StudentService } from '@services/student.service';
import { ISinhVienLogin } from '@models/auth/sinhvien.model';
import { getItem } from '@utils/token-storage';

export const sinhVienLogin = createAsyncThunk(`student/login`, async (args: ISinhVienLogin, { rejectWithValue }) => {
  try {
    const res = await StudentService.sinhVienLoginApi(args);
    return res;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const sinhVienRefreshToken = createAsyncThunk('student/refresh', async (_, { rejectWithValue }) => {
  try {
    const { accessToken, refreshToken } = getItem();

    if (!accessToken || !refreshToken) {
      return rejectWithValue('Không có token');
    }

    const res = await StudentService.sinhVienRefreshTokenApi({
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

export const sinhVienLogout = createAsyncThunk('student/logout', async (_, { rejectWithValue }) => {
  try {
    const res = await StudentService.sinhVienLogoutApi();
    return res;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});
