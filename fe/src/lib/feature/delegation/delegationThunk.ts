import { createAsyncThunk } from '@reduxjs/toolkit';
import { ICreateHopDongNs, IQueryNhanSu } from '@models/nhansu/nhansu.model';
import { ICreateDoanVao, IQueryGuestGroup, IUpdateDoanVao } from '@models/delegation/delegation.model';
import { DelegationIncomingService } from '@services/delegation/delegationIncoming.service';
import { rejects } from 'assert';

export const getListGuestGroup = createAsyncThunk('delegation-incoming/list', async (args: IQueryGuestGroup, { rejectWithValue }) => {
  try {
    const res = await DelegationIncomingService.paging(args);

    return res.data;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});
export const getListPhongBan = createAsyncThunk ('delegation-incoming/getPhongBan', async (_,thunkAPI) => {
  try {
    const res = await DelegationIncomingService.getListPhongBan();

    return res.data;
  } catch (error: any) {
    return thunkAPI.rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
})
export const getListStatus = createAsyncThunk ('delegation-incoming/getStatus', async (_,thunkAPI) => {
  try {
    const res = await DelegationIncomingService.getListStatus();

    return res.data;
  } catch (error: any) {
    return thunkAPI.rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
})

export const createDoanVao = createAsyncThunk(
  'delegation-incoming/create',
  async (payload: ICreateDoanVao, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.createDoanVao(payload);

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

export const deleteDoanVao = createAsyncThunk(
  'delegation-incoming/delete',
  async (id: number, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.deleteDoanVao(id);
      return id;
    } catch (error: any) {
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const updateDoanVao = createAsyncThunk(
  'delegation-incoming/update',
  async (payload: IUpdateDoanVao, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.updateDoanVao(payload);
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

