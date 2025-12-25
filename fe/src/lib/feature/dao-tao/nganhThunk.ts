import { createAsyncThunk } from '@reduxjs/toolkit';
import { ICreateNganh, IQueryNganh, IUpdateNganh } from '@models/dao-tao/nganh.model';
import { DaoTaoService } from '@services/daotao.service';

// Nganh actions
export const getAllNganh = createAsyncThunk(
  'daotao/list-nganh',
  async (payload: IQueryNganh | undefined, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.getListNganh(payload);

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

export const createNganh = createAsyncThunk(
  'daotao/create-nganh',
  async (payload: ICreateNganh, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.createNganh(payload);

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

export const updateNganh = createAsyncThunk(
  'daotao/update-nganh',
  async (payload: IUpdateNganh, { rejectWithValue }) => {
    try {
      const res = await DaoTaoService.updateNganh(payload);

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

export const deleteNganh = createAsyncThunk('daotao/delete-nganh', async (payload: number, { rejectWithValue }) => {
  try {
    const res = await DaoTaoService.deleteNganh(payload);

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

export const getNganhById = createAsyncThunk('daotao/get-nganh', async (payload: number, { rejectWithValue }) => {
  try {
    const res = await DaoTaoService.getNganhById(payload);

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
