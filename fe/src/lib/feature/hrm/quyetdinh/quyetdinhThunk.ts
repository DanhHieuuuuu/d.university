import { createAsyncThunk } from '@reduxjs/toolkit';
import { HopDongService } from '@services/hrm/hopdopng.service';
import { ICreateHopDong, IQueryHopDong } from '@models/nhansu/hopdong.model';
import { NsDecisionService } from '@services/hrm/quyetdinh.service';

export const getListQuyetDinh = createAsyncThunk('quyetdinh/list', async (args: IQueryHopDong, { rejectWithValue }) => {
  try {
    const res = await NsDecisionService.findPaging(args);

    return res.data;
  } catch (error: any) {
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

