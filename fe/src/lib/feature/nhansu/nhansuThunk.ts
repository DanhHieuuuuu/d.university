import { createAsyncThunk } from '@reduxjs/toolkit';
import { NhanSuService } from '@services/hrm/nhansu.service';
import { ICreateNhanSu, IQueryNhanSu } from '@models/nhansu/nhansu.model';
import { ICreateHopDongNs } from '@models/nhansu/hopdong.model';

export const getListNhanSu = createAsyncThunk('nhansu/list', async (args: IQueryNhanSu, { rejectWithValue }) => {
  try {
    const res = await NhanSuService.findPaging(args);

    return res.data;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getDetailNhanSu = createAsyncThunk('nhansu/get', async (idNhanSu: number, { rejectWithValue }) => {
  try {
    const res = await NhanSuService.findById(idNhanSu);

    return res.data;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getHoSoNhanSu = createAsyncThunk('nhansu/ho-so', async (idNhanSu: number, { rejectWithValue }) => {
  try {
    const res = await NhanSuService.getHoSoNhanSu(idNhanSu);

    return res.data;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const createHopDongNhanSu = createAsyncThunk(
  'nhansu/create-hd',
  async (payload: ICreateHopDongNs, { rejectWithValue }) => {
    try {
      const res = await NhanSuService.createHopDong(payload);

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

export const createNhanSu = createAsyncThunk('nhansu/create', async (payload: ICreateNhanSu, { rejectWithValue }) => {
  try {
    const res = await NhanSuService.createNhanSu(payload);

    return res.data;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});
