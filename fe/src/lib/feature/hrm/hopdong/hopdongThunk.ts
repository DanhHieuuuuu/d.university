import { createAsyncThunk } from '@reduxjs/toolkit';
import { HopDongService } from '@services/hrm/hopdopng.service';
import { ICreateHopDong, IQueryHopDong } from '@models/nhansu/hopdong.model';

export const getListHopDong = createAsyncThunk('hopdong/list', async (args: IQueryHopDong, { rejectWithValue }) => {
  try {
    const res = await HopDongService.findPaging(args);

    return res.data;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const createHopDong = createAsyncThunk(
  'hopdong/create',
  async (payload: ICreateHopDong, { rejectWithValue }) => {
    try {
      const res = await HopDongService.createHopDong(payload);

      return res.data;
    } catch (error: any) {
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);
