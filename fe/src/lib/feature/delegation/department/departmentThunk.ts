import { createAsyncThunk } from '@reduxjs/toolkit';
import { ICreateDepartment, IDepartmentSupport, IQueryDepartmentSupport, IUpdateDepartmentSupport } from '@models/delegation/delegation.model';
import { DelegationIncomingService } from '@services/delegation/delegationIncoming.service';

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
      const data = await DelegationIncomingService.createDepartment(payload);
      if (data?.code !== 200) {
      return rejectWithValue(data.message);
    }
    return data
  }
);

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