import { createAsyncThunk } from '@reduxjs/toolkit';
import { NhanSuService } from '@services/hrm/nhansu.service';
import { ICreateNhanSu, IQueryNhanSu, IViewNhanSu } from '@models/nhansu/nhansu.model';

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

export const findNhanSuBySdtThunk = createAsyncThunk(
  'nhansu/find-sdt',
  async (payload: string, { rejectWithValue }) => {
    try {
      const res = await NhanSuService.findBySdt(payload);

      return (res.data.items ?? []).map((ns: IViewNhanSu) => ({
        label: `${ns.hoTen} - ${ns.soDienThoai}`,
        value: ns.idNhanSu,
      }));
    } catch (error: any) {
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);
