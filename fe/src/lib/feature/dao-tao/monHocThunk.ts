import { createAsyncThunk } from '@reduxjs/toolkit';
import { ICreateMonHoc, IQueryMonHoc, IUpdateMonHoc } from '@models/dao-tao/monHoc.model';
import { DaoTaoService } from '@services/daotao.service';

// MonHoc actions
export const getAllMonHoc = createAsyncThunk(
  'daotao/list-monhoc',
  async (payload: IQueryMonHoc | undefined, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.getListMonHoc(payload);

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

export const createMonHoc = createAsyncThunk(
  'daotao/create-monhoc',
  async (payload: ICreateMonHoc, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.createMonHoc(payload);

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

export const updateMonHoc = createAsyncThunk(
  'daotao/update-monhoc',
  async (payload: IUpdateMonHoc, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.updateMonHoc(payload);

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

export const deleteMonHoc = createAsyncThunk(
  'daotao/delete-monhoc',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.deleteMonHoc(payload);

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

export const getMonHocById = createAsyncThunk(
  'daotao/get-monhoc',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.getMonHocById(payload);

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
