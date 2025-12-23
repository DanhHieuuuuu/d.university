import { createAsyncThunk } from '@reduxjs/toolkit';
import { ICreateKhoa, IQueryKhoa, IUpdateKhoa } from '@models/dao-tao/khoa.model';
import { DaoTaoService } from '@services/daotao.service';

// Khoa actions
export const getAllKhoa = createAsyncThunk('daotao/list-khoa', async (payload?: IQueryKhoa) => {
  try {
    const res = await DaoTaoService.getListKhoa(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const createKhoa = createAsyncThunk('daotao/create-khoa', async (payload: ICreateKhoa) => {
  try {
    const res = await DaoTaoService.createKhoa(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const updateKhoa = createAsyncThunk('daotao/update-khoa', async (payload: IUpdateKhoa) => {
  try {
    const res = await DaoTaoService.updateKhoa(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const deleteKhoa = createAsyncThunk('daotao/delete-khoa', async (payload: number) => {
  try {
    const res = await DaoTaoService.deleteKhoa(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const getKhoaById = createAsyncThunk('daotao/get-khoa', async (payload: number) => {
  try {
    const res = await DaoTaoService.getKhoaById(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
