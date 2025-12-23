import { createAsyncThunk } from '@reduxjs/toolkit';
import {
  ICreateChuyenNganh,
  IQueryChuyenNganh,
  IUpdateChuyenNganh} from '@models/dao-tao/chuyenNganh.model';
import { DaoTaoService } from '@services/daotao.service';

// ChuyenNganh actions
export const getAllChuyenNganh = createAsyncThunk('daotao/list-chuyennganh', async (payload?: IQueryChuyenNganh) => {
  try {
    const res = await DaoTaoService.getListChuyenNganh(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const createChuyenNganh = createAsyncThunk('daotao/create-chuyennganh', async (payload: ICreateChuyenNganh) => {
  try {
    const res = await DaoTaoService.createChuyenNganh(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const updateChuyenNganh = createAsyncThunk('daotao/update-chuyennganh', async (payload: IUpdateChuyenNganh) => {
  try {
    const res = await DaoTaoService.updateChuyenNganh(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const deleteChuyenNganh = createAsyncThunk('daotao/delete-chuyennganh', async (payload: number) => {
  try {
    const res = await DaoTaoService.deleteChuyenNganh(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const getChuyenNganhById = createAsyncThunk('daotao/get-chuyennganh', async (payload: number) => {
  try {
    const res = await DaoTaoService.getChuyenNganhById(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
