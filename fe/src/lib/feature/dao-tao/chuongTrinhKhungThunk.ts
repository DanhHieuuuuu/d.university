import { createAsyncThunk } from '@reduxjs/toolkit';
import {
  ICreateChuongTrinhKhung,
  IQueryChuongTrinhKhung,
  IUpdateChuongTrinhKhung
} from '@models/dao-tao/chuongTrinhKhung.model';
import { DaoTaoService } from '@services/daotao.service';

// ChuongTrinhKhung actions
export const getAllChuongTrinhKhung = createAsyncThunk(
  'daotao/list-chuongtrinhkhung',
  async (payload: IQueryChuongTrinhKhung | undefined, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.getListChuongTrinhKhung(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const createChuongTrinhKhung = createAsyncThunk(
  'daotao/create-chuongtrinhkhung',
  async (payload: ICreateChuongTrinhKhung, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.createChuongTrinhKhung(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const updateChuongTrinhKhung = createAsyncThunk(
  'daotao/update-chuongtrinhkhung',
  async (payload: IUpdateChuongTrinhKhung, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.updateChuongTrinhKhung(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const deleteChuongTrinhKhung = createAsyncThunk(
  'daotao/delete-chuongtrinhkhung',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.deleteChuongTrinhKhung(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const getChuongTrinhKhungById = createAsyncThunk(
  'daotao/get-chuongtrinhkhung',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.getChuongTrinhKhungById(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);
