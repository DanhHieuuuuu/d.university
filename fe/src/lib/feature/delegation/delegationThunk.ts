import { createAsyncThunk } from '@reduxjs/toolkit';
import { ICreateHopDongNs, IQueryNhanSu } from '@models/nhansu/nhansu.model';
import { IQueryGuestGroup } from '@models/delegation/delegation.model';
import { DelegationIncomingService } from '@services/delegation/delegationIncoming.service';

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

