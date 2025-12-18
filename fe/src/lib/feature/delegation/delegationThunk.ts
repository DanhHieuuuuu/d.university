import { createAsyncThunk } from '@reduxjs/toolkit';
import { ICreateHopDongNs, IQueryNhanSu } from '@models/nhansu/nhansu.model';
import { ICreateDoanVao, ICreateReceptionTime, IQueryDepartmentSupport, IQueryGuestGroup, IQueryLogStatus, IUpdateDoanVao, IUpdateReceptionTime } from '@models/delegation/delegation.model';
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
export const getListNhanSu = createAsyncThunk ('delegation-incoming/getNhanSu', async (_,thunkAPI) => {
  try {
    const res = await DelegationIncomingService.getListNhanSu();

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
  async (formData: FormData, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.createDoanVao(formData);

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
  async (formData: FormData, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.updateDoanVao(formData);
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
export const getByIdGuestGroup = createAsyncThunk(
  'delegation-incoming/getById',
  async (id: number, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.getByIdGuestGroup (id);

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
export const getByIdDetailDelegation = createAsyncThunk(
  'delegation-incoming/getByIdDetail',
  async (delegationIncomingId: number, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.getByIdDetailDelegation (delegationIncomingId); 
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
export const getByIdReceptionTime = createAsyncThunk(
  'delegation-incoming/getByIdReceptionTime',
  async (delegationIncomingId: number, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.getByIdReceptionTime (delegationIncomingId);
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

export const getLogStatus = createAsyncThunk('delegation-incoming/getLogStatus', async (args: IQueryLogStatus, { rejectWithValue }) => {
  try {
    const res = await DelegationIncomingService.getLogStatus(args);

    return res.data;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});
export const updateReceptionTime = createAsyncThunk('delegation-incoming/updateReceptionTime', async (payload: IUpdateReceptionTime) => {
  try {
    const res = await DelegationIncomingService.updateReceptionTime(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const createReceptionTime = createAsyncThunk('delegation-incoming/createReceptionTime',async (payload: ICreateReceptionTime, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.createReceptionTime(payload);

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

export const getListDepartmentSupport = createAsyncThunk('delegation-incoming/listDepartmentSupport', async (args: IQueryDepartmentSupport, { rejectWithValue }) => {
  try {
    const res = await DelegationIncomingService.pagingDepartmentSupport(args);

    return res.data;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});