import { createAsyncThunk } from '@reduxjs/toolkit';
import {
  ICreateChuyenNganh,
  IQueryChuyenNganh,
  IUpdateChuyenNganh
} from '@models/dao-tao/chuyenNganh.model';
import { DaoTaoService } from '@services/daotao.service';

// ChuyenNganh actions
export const getAllChuyenNganh = createAsyncThunk(
  'daotao/list-chuyennganh',
  async (payload: IQueryChuyenNganh | undefined, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.getListChuyenNganh(payload);

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

export const createChuyenNganh = createAsyncThunk(
  'daotao/create-chuyennganh',
  async (payload: ICreateChuyenNganh, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.createChuyenNganh(payload);

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

export const updateChuyenNganh = createAsyncThunk(
  'daotao/update-chuyennganh',
  async (payload: IUpdateChuyenNganh, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.updateChuyenNganh(payload);

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

export const deleteChuyenNganh = createAsyncThunk(
  'daotao/delete-chuyennganh',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.deleteChuyenNganh(payload);

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

export const getChuyenNganhById = createAsyncThunk(
  'daotao/get-chuyennganh',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.getChuyenNganhById(payload);

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
