import { ReduxStatus } from '@redux/const';
import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { CRUD } from '@models/common/common';
import { ICreateChucVu, IQueryChucVu, IUpdateChucVu, IViewChucVu } from '@models/danh-muc/chuc-vu.model';
import { ICreateToBoMon, IQueryToBoMon, IUpdateToBoMon, IViewToBoMon } from '@models/danh-muc/to-bo-mon.model';
import { ICreatePhongBan, IQueryPhongBan, IUpdatePhongBan, IViewPhongBan } from '@models/danh-muc/phong-ban.model';

import { KpiService } from '@services/kpi.service';
import { ICreateKpiCaNhan, IQueryKpiCaNhan, IUpdateKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import { ICreateKpiDonVi, IQueryKpiDonVi, IUpdateKpiDonVi, IViewKpiDonVi } from '@models/kpi/kpi-don-vi.model';
import { ICreateKpiRole, IQueryKpiRole, IUpdateKpiRole, IViewKpiRole } from '@models/kpi/kpi-role.model';

export const getAllKpiCaNhan = createAsyncThunk('kpi/list-canhan', async (payload?: IQueryKpiCaNhan) => {
  try {
    const res = await KpiService.getListKpiCaNhan(payload);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const createKpiCaNhan = createAsyncThunk(
  'kpi/create-canhan',
  async (payload: ICreateKpiCaNhan, { rejectWithValue }) => {
    try {
      const res = await KpiService.createKpiCaNhan(payload);
      return res;
    } catch (err: any) {
      return rejectWithValue(err?.response?.data);
    }
  }
);
export const updateKpiCaNhan = createAsyncThunk('kpi/update-canhan', async (payload: IUpdateKpiCaNhan) => {
  try {
    const res = await KpiService.updateKpiCaNhan(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const deleteKpiCaNhan = createAsyncThunk('kpi/delete-canhan', async (payload: number) => {
  try {
    const res = await KpiService.deleteKpiCaNhan(payload);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});


export const getAllKpiDonVi = createAsyncThunk('kpi/list-donvi', async (payload?: IQueryKpiDonVi) => {
  try {
    const res = await KpiService.getListKpiDonVi(payload);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});


export const createKpiDonVi = createAsyncThunk('kpi/create-donvi', async (payload: ICreateKpiDonVi) => {
  try {
    const res = await KpiService.createKpiDonVi(payload);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const updateKpiDonVi = createAsyncThunk('kpi/update-donvi', async (payload: IUpdateKpiDonVi) => {
  try {
    const res = await KpiService.updateKpiDonVi(payload);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const deleteKpiDonVi = createAsyncThunk('kpi/delete-donvi', async (payload: number, { rejectWithValue }) => {
  try {
    const res = await KpiService.deleteKpiDonVi(payload);
    return res.data;
  } catch (error: any) {
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
});
//Kpi Role
export const getAllKpiRole = createAsyncThunk('kpi-role/list', async (payload?: IQueryKpiRole) => {
  // async (payload?: IQueryKpiRole, { rejectWithValue }) => {
  try {
    return await KpiService.getListKpiRole(payload);
  } catch (error: any) {
    // return rejectWithValue(err?.response?.data);
    console.error(error);
  }
}
);

export const createKpiRole = createAsyncThunk('kpi-role/create', async (payload: ICreateKpiRole, { rejectWithValue }) => {
  try {
    return await KpiService.createKpiRole(payload);
  } catch (error: any) {
    return rejectWithValue(
      error?.message || ' Đã xảy ra lỗi, vui lòng thử lại!'
    );
  }
}
);

export const updateKpiRole = createAsyncThunk('kpi-role/update', async (payload: IUpdateKpiRole, { rejectWithValue }) => {
  try {
    return await KpiService.updateKpiRole(payload);
  } catch (error: any) {
    return rejectWithValue(
      error?.message || ' Đã xảy ra lỗi, vui lòng thử lại!'
    );
  }
}
);

export const deleteKpiRole = createAsyncThunk('kpi-role/delete', async (ids: number[], { rejectWithValue }) => {
  try {
    return await KpiService.deleteKpiRole(ids);
  } catch (error: any) {
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
}
);

interface KpiState {
  kpiCaNhan: CRUD<IViewKpiCaNhan>;
  kpiDonVi: CRUD<IViewKpiDonVi>;
  kpiRole: CRUD<IViewKpiRole>;
}

const initialState: KpiState = {
  kpiCaNhan: {
    $create: { status: ReduxStatus.IDLE },
    $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    $update: { status: ReduxStatus.IDLE },
    $delete: { status: ReduxStatus.IDLE },
    $selected: { status: ReduxStatus.IDLE, id: null, data: null }
  },
  kpiDonVi: {
    $create: { status: ReduxStatus.IDLE },
    $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    $update: { status: ReduxStatus.IDLE },
    $delete: { status: ReduxStatus.IDLE },
    $selected: { status: ReduxStatus.IDLE, id: null, data: null }
  },
  kpiRole: {
    $create: { status: ReduxStatus.IDLE },
    $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    $update: { status: ReduxStatus.IDLE },
    $delete: { status: ReduxStatus.IDLE },
    $selected: { status: ReduxStatus.IDLE, id: null, data: null }
  }

};

const kpiSlice = createSlice({
  name: 'kpi',
  initialState,
  reducers: {
    //kpi Ca Nhan
    clearSeletedKpiCaNhan: (state) => {
      state.kpiCaNhan.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
    },
    setSelectedKpiCaNhan: (state, action: PayloadAction<IViewKpiCaNhan>) => {
      state.kpiCaNhan.$selected = {
        status: ReduxStatus.SUCCESS,
        id: action.payload.id,
        data: action.payload
      };
    },
    resetStatusKpiCaNhan: (state) => {
      state.kpiCaNhan.$create.status = ReduxStatus.IDLE;
      state.kpiCaNhan.$update.status = ReduxStatus.IDLE;
      state.kpiCaNhan.$delete.status = ReduxStatus.IDLE;
    },
    //kpi Don Vi
    clearSeletedKpiDonVi: (state) => {
      state.kpiDonVi.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
    },
    setSelectedKpiDonVi: (state, action: PayloadAction<IViewKpiDonVi>) => {
      state.kpiDonVi.$selected = {
        status: ReduxStatus.SUCCESS,
        id: action.payload.id,
        data: action.payload
      };
    },
    resetStatusKpiDonVi: (state) => {
      state.kpiDonVi.$create.status = ReduxStatus.IDLE;
      state.kpiDonVi.$update.status = ReduxStatus.IDLE;
      state.kpiDonVi.$delete.status = ReduxStatus.IDLE;
    },
    //kpi Role
    clearSeletedKpiRole: (state) => {
      state.kpiRole.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
    },
    setSelectedKpiRole: (state, action: PayloadAction<IViewKpiRole>) => {
      state.kpiRole.$selected = {
        status: ReduxStatus.SUCCESS,
        id: action.payload.id,
        data: action.payload
      };
    },
    resetStatusKpiRole: (state) => {
      state.kpiRole.$create.status = ReduxStatus.IDLE;
      state.kpiRole.$update.status = ReduxStatus.IDLE;
      state.kpiRole.$delete.status = ReduxStatus.IDLE;
    },
  },
  extraReducers: (buidler) => {
    buidler
      .addCase(getAllKpiCaNhan.pending, (state) => {
        state.kpiCaNhan.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getAllKpiCaNhan.fulfilled, (state, action) => {
        state.kpiCaNhan.$list.status = ReduxStatus.SUCCESS;
        state.kpiCaNhan.$list.data = action.payload?.items || [];
        state.kpiCaNhan.$list.total = action.payload?.totalItem || 0;
      })
      .addCase(getAllKpiCaNhan.rejected, (state) => {
        state.kpiCaNhan.$list.status = ReduxStatus.FAILURE;
      })

      .addCase(createKpiCaNhan.pending, (state) => {
        state.kpiCaNhan.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createKpiCaNhan.fulfilled, (state, action) => {
        state.kpiCaNhan.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createKpiCaNhan.rejected, (state) => {
        state.kpiCaNhan.$create.status = ReduxStatus.FAILURE;
      })
      .addCase(updateKpiCaNhan.pending, (state) => {
        state.kpiCaNhan.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updateKpiCaNhan.fulfilled, (state) => {
        state.kpiCaNhan.$update.status = ReduxStatus.SUCCESS;
        state.kpiCaNhan.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
      })
      .addCase(updateKpiCaNhan.rejected, (state) => {
        state.kpiCaNhan.$update.status = ReduxStatus.FAILURE;
      })
      .addCase(deleteKpiCaNhan.pending, (state) => {
        state.kpiCaNhan.$delete.status = ReduxStatus.LOADING;
      })
      .addCase(deleteKpiCaNhan.fulfilled, (state, action) => {
        state.kpiCaNhan.$delete.status = ReduxStatus.SUCCESS;
      })
      .addCase(deleteKpiCaNhan.rejected, (state) => {
        state.kpiCaNhan.$delete.status = ReduxStatus.FAILURE;
      })

      //Kpi Don Vi
      .addCase(getAllKpiDonVi.pending, (state) => {
        state.kpiDonVi.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getAllKpiDonVi.fulfilled, (state, action) => {
        state.kpiDonVi.$list.status = ReduxStatus.SUCCESS;
        state.kpiDonVi.$list.data = action.payload?.items || [];
        state.kpiDonVi.$list.total = action.payload?.totalItem || 0;
      })
      .addCase(getAllKpiDonVi.rejected, (state) => {
        state.kpiDonVi.$list.status = ReduxStatus.FAILURE;
      })
      .addCase(createKpiDonVi.pending, (state) => {
        state.kpiDonVi.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createKpiDonVi.fulfilled, (state) => {
        state.kpiDonVi.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createKpiDonVi.rejected, (state) => {
        state.kpiDonVi.$create.status = ReduxStatus.FAILURE;
      })
      .addCase(updateKpiDonVi.pending, (state) => {
        state.kpiDonVi.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updateKpiDonVi.fulfilled, (state) => {
        state.kpiDonVi.$update.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateKpiDonVi.rejected, (state) => {
        state.kpiDonVi.$update.status = ReduxStatus.FAILURE;
      })

      .addCase(deleteKpiDonVi.pending, (state) => {
        state.kpiDonVi.$delete.status = ReduxStatus.LOADING;
      })
      .addCase(deleteKpiDonVi.fulfilled, (state) => {
        state.kpiDonVi.$delete.status = ReduxStatus.SUCCESS;
      })
      .addCase(deleteKpiDonVi.rejected, (state) => {
        state.kpiDonVi.$delete.status = ReduxStatus.FAILURE;
      })
      //Kpi Role
      .addCase(getAllKpiRole.pending, (state) => {
        state.kpiRole.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getAllKpiRole.fulfilled, (state, action) => {
        state.kpiRole.$list.status = ReduxStatus.SUCCESS;
        state.kpiRole.$list.data = action.payload?.data.items || [];
        state.kpiRole.$list.total = action.payload?.data.totalItem || 0;
      })
      .addCase(getAllKpiRole.rejected, (state) => {
        state.kpiRole.$list.status = ReduxStatus.FAILURE;
      })
      .addCase(createKpiRole.pending, (state) => {
        state.kpiRole.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createKpiRole.fulfilled, (state) => {
        state.kpiRole.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createKpiRole.rejected, (state) => {
        state.kpiRole.$create.status = ReduxStatus.FAILURE;
      })
      .addCase(updateKpiRole.pending, (state) => {
        state.kpiRole.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updateKpiRole.fulfilled, (state) => {
        state.kpiRole.$update.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateKpiRole.rejected, (state) => {
        state.kpiRole.$update.status = ReduxStatus.FAILURE;
      })

      .addCase(deleteKpiRole.pending, (state) => {
        state.kpiRole.$delete.status = ReduxStatus.LOADING;
      })
      .addCase(deleteKpiRole.fulfilled, (state) => {
        state.kpiRole.$delete.status = ReduxStatus.SUCCESS;
      })
      .addCase(deleteKpiRole.rejected, (state) => {
        state.kpiRole.$delete.status = ReduxStatus.FAILURE;
      })
  }
});

const kpiReducer = kpiSlice.reducer;

export const {
  clearSeletedKpiCaNhan,
  setSelectedKpiCaNhan,
  resetStatusKpiCaNhan,
  clearSeletedKpiDonVi,
  setSelectedKpiDonVi,
  resetStatusKpiDonVi,
  clearSeletedKpiRole,
  setSelectedKpiRole,
  resetStatusKpiRole
} = kpiSlice.actions;

export default kpiReducer;
