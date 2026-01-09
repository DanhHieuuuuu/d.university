import { createAsyncThunk } from '@reduxjs/toolkit';
import {
  ICreateDepartment,
  ICreateDoanVao,
  ICreatePrepare,
  ICreateReceptionTime,
  ICreateReceptionTimeList,
  ICreateSupporter,
  IQueryDepartmentSupport,
  IQueryGuestGroup,
  IQueryLogReceptionTime,
  IQueryLogStatus,
  IUpdateDepartmentSupport,
  IUpdateDoanVao,
  IUpdatePrepare,
  IUpdateReceptionTime,
  IUpdateReceptionTimes,
  IUpdateStatus
} from '@models/delegation/delegation.model';
import { DelegationIncomingService } from '@services/delegation/delegationIncoming.service';
import { rejects } from 'assert';

export const getListGuestGroup = createAsyncThunk(
  'delegation-incoming/list',
  async (args: IQueryGuestGroup, { rejectWithValue }) => {
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
  }
);
export const getListPhongBan = createAsyncThunk('delegation-incoming/getPhongBan', async (_, thunkAPI) => {
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
});
export const getListNhanSu = createAsyncThunk('delegation-incoming/getNhanSu', async (_, thunkAPI) => {
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
});
export const getListStatus = createAsyncThunk('delegation-incoming/getStatus', async (_, thunkAPI) => {
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
});

export const createDoanVao = createAsyncThunk(
  'delegation-incoming/create',
  async (formData: FormData, { rejectWithValue }) => {
    const data = await DelegationIncomingService.createDoanVao(formData);
    if (data?.code !== 200) {
      return rejectWithValue(data.message);
    }
    return data;
  }
);

export const deleteDoanVao = createAsyncThunk('delegation-incoming/delete', async (id: number, { rejectWithValue }) => {
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
});

export const updateDoanVao = createAsyncThunk(
  'delegation-incoming/update',
  async (formData: FormData, { rejectWithValue }) => {
    const data = await DelegationIncomingService.updateDoanVao(formData);
    if (data?.code !== 200) {
      return rejectWithValue(data.message);
    }
    return data;
  }
);
export const getByIdGuestGroup = createAsyncThunk(
  'delegation-incoming/getById',
  async (id: number, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.getByIdGuestGroup(id);

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
      const res = await DelegationIncomingService.getByIdDetailDelegation(delegationIncomingId);
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
      const res = await DelegationIncomingService.getByIdReceptionTime(delegationIncomingId);
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

export const getLogStatus = createAsyncThunk(
  'delegation-incoming/getLogStatus',
  async (args: IQueryLogStatus, { rejectWithValue }) => {
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
  }
);
export const getLogReceptionTime = createAsyncThunk(
  'delegation-incoming/getLogReceptionTime',
  async (args: IQueryLogReceptionTime, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.getLogReceptionTime(args);

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
export const updateReceptionTimes = createAsyncThunk(
  'delegation-incoming/updateReceptionTime',
  async (payload: IUpdateReceptionTimes, { rejectWithValue }) => {
    const data = await DelegationIncomingService.updateReceptionTimes(payload);
    if (data?.code !== 200) {
      return rejectWithValue(data.message);
    }
    return data;
  }
);
export const createReceptionTime = createAsyncThunk(
  'delegation-incoming/createReceptionTime',
  async (payload: ICreateReceptionTimeList, { rejectWithValue }) => {
    const data = await DelegationIncomingService.createReceptionTime(payload);
    if (data?.code !== 200) {
      return rejectWithValue(data.message);
    }
    return data;
  }
);

export const getListDepartmentSupport = createAsyncThunk(
  'delegation-incoming/listDepartmentSupport',
  async (args: IQueryDepartmentSupport, { rejectWithValue }) => {
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
  }
);
export const createDepartmentSupport = createAsyncThunk(
  'delegation-incoming/createDepartmentSupport',
  async (payload: ICreateDepartment, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.createDepartment(payload);

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
export const createSupporter = createAsyncThunk(
  'delegation-incoming/createSupporter',
  async (payload: ICreateSupporter, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.createSupporter(payload);

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
export const getListDelegationIncoming = createAsyncThunk(
  'delegation-incoming/getDelegationIncoming',
  async (_, thunkAPI) => {
    try {
      const res = await DelegationIncomingService.getListDelegationIncoming();

      return res.data;
    } catch (error: any) {
      return thunkAPI.rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);
export const updateStatus = createAsyncThunk('delegation-incoming/updateStatus', async (payload: IUpdateStatus) => {
  try {
    const res = await DelegationIncomingService.updateStatus(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const updateDepartmentSupport = createAsyncThunk(
  'delegation-incoming/updateDepartmentSupport',
  async (payload: IUpdateDepartmentSupport, { rejectWithValue }) => {
    const data = await DelegationIncomingService.updateDepartmentSupport(payload);
    if (data?.code !== 200) {
      return rejectWithValue(data.message);
    }
    return data;
  }
);

export const getByIdDepartmentSupport = createAsyncThunk(
  'delegation-incoming/getByIdDepartmentSupport',
  async (departmentSupportId: number, { rejectWithValue }) => {
    try {
      const res = await DelegationIncomingService.getByIdDepartmentSupport(departmentSupportId);
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
export const createPrepare = createAsyncThunk(
  'delegation-incoming/createPrepare',
  async (payload: ICreatePrepare, { rejectWithValue }) => {
    const data = await DelegationIncomingService.createPrepare(payload);
    if (data?.code !== 200) {
      return rejectWithValue(data.message);
    }
    return data;
  }
);
export const updatePrepare = createAsyncThunk(
  'delegation-incoming/updatePrepare',
  async (payload: IUpdatePrepare, { rejectWithValue }) => {
    const data = await DelegationIncomingService.updatePrepare(payload);
    if (data?.code !== 200) {
      return rejectWithValue(data.message);
    }
    return data;
  }
);