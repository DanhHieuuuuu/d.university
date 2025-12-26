import { ReduxStatus } from '@redux/const';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { CRUD } from '@models/common/common';

import { ICreateKpiCaNhan, IUpdateKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import { ICreateKpiDonVi, IQueryKpiDonVi, IViewKpiDonVi } from '@models/kpi/kpi-don-vi.model';
import { IViewKpiRole } from '@models/kpi/kpi-role.model';
import {
    createKpiCaNhan, createKpiDonVi, createKpiRole, deleteKpiCaNhan, deleteKpiDonVi, deleteKpiRole, getAllKpiCaNhan, updateTrangThaiKpiCaNhan,
    updateKetQuaThucTeKpiCaNhan, getAllKpiDonVi, getAllKpiRole, updateKpiCaNhan, updateKpiDonVi, updateKpiRole,
    getListNamHocKpiDonVi,
    getListTrangThaiKpiDonVi,
    getListTrangThaiKpiCaNhan
} from './kpiThunk';

interface MetaList<T> {
    status: ReduxStatus;
    data: T[];
}

interface KpiState {
    kpiCaNhan: CRUD<IViewKpiCaNhan>;
    kpiDonVi: CRUD<IViewKpiDonVi>;
    kpiRole: CRUD<IViewKpiRole>;
    meta: {
        trangThai: {
            caNhan: MetaList<{ value: number; label: string }>;
            donVi: MetaList<{ value: number; label: string }>;
        };
        namHoc: {
            caNhan: MetaList<{ namHoc: string }>;
            donVi: MetaList<{ namHoc: string }>;
        };
    }
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
        $selected: { status: ReduxStatus.IDLE, id: null, data: null },
    },
    kpiRole: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null }
    },
    meta: {
        trangThai: {
            caNhan: { status: ReduxStatus.IDLE, data: [] },
            donVi: { status: ReduxStatus.IDLE, data: [] },
        },
        namHoc: {
            caNhan: { status: ReduxStatus.IDLE, data: [] },
            donVi: { status: ReduxStatus.IDLE, data: [] },
        },
    },

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
            .addCase(getListTrangThaiKpiCaNhan.pending, (state) => {
                state.meta.trangThai.caNhan.status = ReduxStatus.LOADING;
            })
            .addCase(getListTrangThaiKpiCaNhan.fulfilled, (state, action) => {
                state.meta.trangThai.caNhan.status = ReduxStatus.SUCCESS;
                state.meta.trangThai.caNhan.data = action.payload;
            })
            .addCase(updateTrangThaiKpiCaNhan.pending, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateTrangThaiKpiCaNhan.fulfilled, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateTrangThaiKpiCaNhan.rejected, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.FAILURE;
            })

            .addCase(updateKetQuaThucTeKpiCaNhan.pending, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateKetQuaThucTeKpiCaNhan.fulfilled, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateKetQuaThucTeKpiCaNhan.rejected, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.FAILURE;
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
            .addCase(getListTrangThaiKpiDonVi.pending, (state) => {
                state.meta.trangThai.donVi.status = ReduxStatus.LOADING;
            })
            .addCase(getListTrangThaiKpiDonVi.fulfilled, (state, action) => {
                state.meta.trangThai.donVi.status = ReduxStatus.SUCCESS;
                state.meta.trangThai.donVi.data = action.payload;
            })
            .addCase(getListNamHocKpiDonVi.pending, (state) => {
                state.meta.namHoc.donVi.status = ReduxStatus.LOADING;
            })
            .addCase(getListNamHocKpiDonVi.fulfilled, (state, action) => {
                state.meta.namHoc.donVi.status = ReduxStatus.SUCCESS;
                state.meta.namHoc.donVi.data = action.payload;
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
