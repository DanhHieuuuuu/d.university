import { createAsyncThunk } from '@reduxjs/toolkit';
import {
  ICreateMonTienQuyet,
  IQueryMonTienQuyet,
  IUpdateMonTienQuyet
} from '@models/dao-tao/monTienQuyet.model';
import { DaoTaoService } from '@services/daotao.service';

// MonTienQuyet actions
export const getAllMonTienQuyet = createAsyncThunk(
  'daotao/list-montienquyet',
  async (payload: IQueryMonTienQuyet | undefined, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.getListMonTienQuyet(payload);

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

export const createMonTienQuyet = createAsyncThunk(
  'daotao/create-montienquyet',
  async (payload: ICreateMonTienQuyet, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.createMonTienQuyet(payload);

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

export const updateMonTienQuyet = createAsyncThunk(
  'daotao/update-montienquyet',
  async (payload: IUpdateMonTienQuyet, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.updateMonTienQuyet(payload);

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

export const deleteMonTienQuyet = createAsyncThunk(
  'daotao/delete-montienquyet',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.deleteMonTienQuyet(payload);

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

export const getMonTienQuyetById = createAsyncThunk(
  'daotao/get-montienquyet',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.getMonTienQuyetById(payload);

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
