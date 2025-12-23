import { createAsyncThunk } from '@reduxjs/toolkit';
import { ICreateNganh, IQueryNganh, IUpdateNganh } from '@models/dao-tao/nganh.model';
import { DaoTaoService } from '@services/daotao.service';

// Nganh actions
export const getAllNganh = createAsyncThunk('daotao/list-nganh', async (payload?: IQueryNganh) => {
  try {
    const res = await DaoTaoService.getListNganh(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const createNganh = createAsyncThunk('daotao/create-nganh', async (payload: ICreateNganh) => {
  try {
    const res = await DaoTaoService.createNganh(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const updateNganh = createAsyncThunk('daotao/update-nganh', async (payload: IUpdateNganh) => {
  try {
    const res = await DaoTaoService.updateNganh(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const deleteNganh = createAsyncThunk('daotao/delete-nganh', async (payload: number) => {
  try {
    const res = await DaoTaoService.deleteNganh(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const getNganhById = createAsyncThunk('daotao/get-nganh', async (payload: number) => {
  try {
    const res = await DaoTaoService.getNganhById(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
