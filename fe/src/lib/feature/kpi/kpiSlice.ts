import { ReduxStatus } from '@redux/const';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { CRUD } from '@models/common/common';

import { ICreateKpiCaNhan, IKpiCaNhanSummary, IUpdateKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import { ICreateKpiDonVi, IKpiDonViSummary, IQueryKpiDonVi, IViewKpiDonVi, NhanSuDaGiaoDto } from '@models/kpi/kpi-don-vi.model';
import { IViewKpiRole } from '@models/kpi/kpi-role.model';
import {
    createKpiCaNhan, createKpiDonVi, createKpiRole, deleteKpiCaNhan, deleteKpiDonVi, deleteKpiRole, getAllKpiCaNhan, updateTrangThaiKpiCaNhan,
    updateKetQuaThucTeKpiCaNhan, getAllKpiDonVi, getAllKpiRole, updateKpiCaNhan, updateKpiDonVi, updateKpiRole,
    getListNamHocKpiDonVi,
    getListTrangThaiKpiDonVi,
    getListTrangThaiKpiCaNhan,
    getAllKpiTruong,
    createKpiTruong,
    updateKpiTruong,
    deleteKpiTruong,
    getListTrangThaiKpiTruong,
    getListNamHocKpiTruong,
    getKpiCaNhanKeKhai,
    getListKpiRoleByUser,
    updateKetQuaCapTrenKpiCaNhan,
    updateKetQuaThucTeKpiDonVi,
    updateKetQuaCapTrenKpiDonVi,
    getKpiDonViKeKhai,
    updateKetQuaCapTrenKpiTruong,
    updateKetQuaThucTeKpiTruong,
    getNhanSuDaGiaoByKpiDonVi,
    giaoKpiDonVi,
    getKpiLogStatus,
    askKpiAi,
    getListKpiCongThuc,
} from './kpiThunk';
import { IViewKpiTruong } from '@models/kpi/kpi-truong.model';
import { KpiLogStatusDto } from '@models/kpi/kpi-log.model';
import { IViewKpiCongThuc } from '@models/kpi/kpi-cong-thuc.model';


interface MetaList<T> {
    status: ReduxStatus;
    data: T[];
}

interface KpiState {
    kpiCaNhan: CRUD<IViewKpiCaNhan, IKpiCaNhanSummary>;
    kpiDonVi: CRUD<IViewKpiDonVi, IKpiDonViSummary> & {
        $assign?: {
            status: ReduxStatus;
        };
    };
    kpiTruong: CRUD<IViewKpiTruong>;
    kpiRole: CRUD<IViewKpiRole>;
    nhanSuDaGiao: {
        data: NhanSuDaGiaoDto[];
        status: ReduxStatus;
    };
    kpiLog: {
        $list: {
            status: ReduxStatus;
            data: KpiLogStatusDto[];
            total?: number;
        };
    }
    kpiChat: {
        status: ReduxStatus;
        answer: string | null;
        history: { role: 'user' | 'ai'; content: string }[];
    };
    listCongThuc: MetaList<IViewKpiCongThuc>;
    meta: {
        trangThai: {
            caNhan: MetaList<{ value: number; label: string }>;
            donVi: MetaList<{ value: number; label: string }>;
            truong: MetaList<{ value: number; label: string }>;
        };
        namHoc: {
            caNhan: MetaList<{ namHoc: string }>;
            donVi: MetaList<{ namHoc: string }>;
            truong: MetaList<{ value: number; label: string }>;
        };
        role: {
            caNhan: MetaList<{ role: string, tiLe: number }>
        }
    }
}

const initialState: KpiState = {
    kpiCaNhan: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0, summary: undefined },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null }
    },
    kpiDonVi: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0, summary: undefined },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null },
        $assign: { status: ReduxStatus.IDLE },
    },
    kpiTruong: {
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
    nhanSuDaGiao: {
        data: [],
        status: ReduxStatus.IDLE
    },
    kpiLog: {
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    },
    kpiChat: {
        status: ReduxStatus.IDLE,
        answer: null,
        history: [],
    },
    listCongThuc: { status: ReduxStatus.IDLE, data: [] },
    meta: {
        trangThai: {
            caNhan: { status: ReduxStatus.IDLE, data: [] },
            donVi: { status: ReduxStatus.IDLE, data: [] },
            truong: { status: ReduxStatus.IDLE, data: [] },
        },
        namHoc: {
            caNhan: { status: ReduxStatus.IDLE, data: [] },
            donVi: { status: ReduxStatus.IDLE, data: [] },
            truong: { status: ReduxStatus.IDLE, data: [] },
        },
        role: {
            caNhan: { status: ReduxStatus.IDLE, data: [] },
        }
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
            state.kpiDonVi.$assign!.status = ReduxStatus.IDLE;
        },
        //KPI TRƯỜNG
        clearSeletedKpiTruong: (state) => {
            state.kpiTruong.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
        },
        setSelectedKpiTruong: (state, action: PayloadAction<IViewKpiTruong>) => {
            state.kpiTruong.$selected = {
                status: ReduxStatus.SUCCESS,
                id: action.payload.id,
                data: action.payload
            };
        },
        resetStatusKpiTruong: (state) => {
            state.kpiTruong.$create.status = ReduxStatus.IDLE;
            state.kpiTruong.$update.status = ReduxStatus.IDLE;
            state.kpiTruong.$delete.status = ReduxStatus.IDLE;
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
        resetChat: (state) => {
            state.kpiChat.status = ReduxStatus.IDLE;
            state.kpiChat.answer = null;
            state.kpiChat.history = [];
        },
        addMessageToHistory: (state, action: PayloadAction<{ role: 'user' | 'ai'; content: string }>) => {
            state.kpiChat.history.push(action.payload);
        }
    },
    extraReducers: (buidler) => {
        buidler
            //Cá nhân
            .addCase(getAllKpiCaNhan.pending, (state) => {
                state.kpiCaNhan.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getAllKpiCaNhan.fulfilled, (state, action) => {
                state.kpiCaNhan.$list.status = ReduxStatus.SUCCESS;
                state.kpiCaNhan.$list.data = action.payload?.items || [];
                state.kpiCaNhan.$list.total = action.payload?.totalItem || 0;
                state.kpiCaNhan.$list.summary = action.payload?.summary;
            })
            .addCase(getAllKpiCaNhan.rejected, (state) => {
                state.kpiCaNhan.$list.status = ReduxStatus.FAILURE;
            })

            .addCase(getKpiCaNhanKeKhai.pending, (state) => {
                state.kpiCaNhan.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getKpiCaNhanKeKhai.fulfilled, (state, action) => {
                state.kpiCaNhan.$list.status = ReduxStatus.SUCCESS;
                state.kpiCaNhan.$list.data = action.payload?.items || [];
                state.kpiCaNhan.$list.total = action.payload?.totalItem || 0;
                state.kpiCaNhan.$list.summary = action.payload?.summary;
            })
            .addCase(getKpiCaNhanKeKhai.rejected, (state) => {
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
            .addCase(getListKpiRoleByUser.pending, (state) => {
                state.meta.role.caNhan.status = ReduxStatus.LOADING;
            })
            .addCase(getListKpiRoleByUser.fulfilled, (state, action) => {
                state.meta.role.caNhan.status = ReduxStatus.SUCCESS;
                state.meta.role.caNhan.data = action.payload;
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

            .addCase(updateKetQuaCapTrenKpiCaNhan.pending, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateKetQuaCapTrenKpiCaNhan.fulfilled, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateKetQuaCapTrenKpiCaNhan.rejected, (state) => {
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
                state.kpiDonVi.$list.summary = action.payload?.summary;
            })
            .addCase(getAllKpiDonVi.rejected, (state) => {
                state.kpiDonVi.$list.status = ReduxStatus.FAILURE;
            })
            .addCase(getKpiDonViKeKhai.pending, (state) => {
                state.kpiDonVi.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getKpiDonViKeKhai.fulfilled, (state, action) => {
                state.kpiDonVi.$list.status = ReduxStatus.SUCCESS;
                state.kpiDonVi.$list.data = action.payload?.items || [];
                state.kpiDonVi.$list.total = action.payload?.totalItem || 0;
            })
            .addCase(getKpiDonViKeKhai.rejected, (state) => {
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
            .addCase(updateKetQuaThucTeKpiDonVi.pending, (state) => {
                state.kpiDonVi.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateKetQuaThucTeKpiDonVi.fulfilled, (state) => {
                state.kpiDonVi.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateKetQuaThucTeKpiDonVi.rejected, (state) => {
                state.kpiDonVi.$update.status = ReduxStatus.FAILURE;
            })

            .addCase(updateKetQuaCapTrenKpiDonVi.pending, (state) => {
                state.kpiDonVi.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateKetQuaCapTrenKpiDonVi.fulfilled, (state) => {
                state.kpiDonVi.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateKetQuaCapTrenKpiDonVi.rejected, (state) => {
                state.kpiDonVi.$update.status = ReduxStatus.FAILURE;
            })
            .addCase(giaoKpiDonVi.pending, (state) => {
                state.kpiDonVi.$assign!.status = ReduxStatus.LOADING;
            })
            .addCase(giaoKpiDonVi.fulfilled, (state) => {
                state.kpiDonVi.$assign!.status = ReduxStatus.SUCCESS;
            })
            .addCase(giaoKpiDonVi.rejected, (state) => {
                state.kpiDonVi.$assign!.status = ReduxStatus.FAILURE;
            })
            .addCase(getNhanSuDaGiaoByKpiDonVi.pending, (state) => {
                state.nhanSuDaGiao.status = ReduxStatus.LOADING;
            })
            .addCase(getNhanSuDaGiaoByKpiDonVi.fulfilled, (state, action) => {
                state.nhanSuDaGiao.data = action.payload;
                state.nhanSuDaGiao.status = ReduxStatus.SUCCESS;
            })
            .addCase(getNhanSuDaGiaoByKpiDonVi.rejected, (state) => {
                state.nhanSuDaGiao.status = ReduxStatus.FAILURE;
            })
            //KPI TRƯỜNG
            .addCase(getAllKpiTruong.pending, (state) => {
                state.kpiTruong.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getAllKpiTruong.fulfilled, (state, action) => {
                state.kpiTruong.$list.status = ReduxStatus.SUCCESS;
                state.kpiTruong.$list.data = action.payload?.items || [];
                state.kpiTruong.$list.total = action.payload?.totalItem || 0;
            })
            .addCase(getAllKpiTruong.rejected, (state) => {
                state.kpiTruong.$list.status = ReduxStatus.FAILURE;
            })
            .addCase(createKpiTruong.pending, (state) => {
                state.kpiTruong.$create.status = ReduxStatus.LOADING;
            })
            .addCase(createKpiTruong.fulfilled, (state) => {
                state.kpiTruong.$create.status = ReduxStatus.SUCCESS;
            })
            .addCase(createKpiTruong.rejected, (state) => {
                state.kpiTruong.$create.status = ReduxStatus.FAILURE;
            })
            .addCase(updateKpiTruong.pending, (state) => {
                state.kpiTruong.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateKpiTruong.fulfilled, (state) => {
                state.kpiTruong.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateKpiTruong.rejected, (state) => {
                state.kpiTruong.$update.status = ReduxStatus.FAILURE;
            })

            .addCase(deleteKpiTruong.pending, (state) => {
                state.kpiTruong.$delete.status = ReduxStatus.LOADING;
            })
            .addCase(deleteKpiTruong.fulfilled, (state) => {
                state.kpiTruong.$delete.status = ReduxStatus.SUCCESS;
            })
            .addCase(deleteKpiTruong.rejected, (state) => {
                state.kpiTruong.$delete.status = ReduxStatus.FAILURE;
            })
            .addCase(getListTrangThaiKpiTruong.pending, (state) => {
                state.meta.trangThai.truong.status = ReduxStatus.LOADING;
            })
            .addCase(getListTrangThaiKpiTruong.fulfilled, (state, action) => {
                state.meta.trangThai.truong.status = ReduxStatus.SUCCESS;
                state.meta.trangThai.truong.data = action.payload;
            })
            .addCase(getListNamHocKpiTruong.pending, (state) => {
                state.meta.namHoc.truong.status = ReduxStatus.LOADING;
            })
            .addCase(getListNamHocKpiTruong.fulfilled, (state, action) => {
                state.meta.namHoc.truong.status = ReduxStatus.SUCCESS;
                state.meta.namHoc.truong.data = action.payload;

            })
            .addCase(updateKetQuaThucTeKpiTruong.pending, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateKetQuaThucTeKpiTruong.fulfilled, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateKetQuaThucTeKpiTruong.rejected, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.FAILURE;
            })
            .addCase(updateKetQuaCapTrenKpiTruong.pending, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateKetQuaCapTrenKpiTruong.fulfilled, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateKetQuaCapTrenKpiTruong.rejected, (state) => {
                state.kpiCaNhan.$update.status = ReduxStatus.FAILURE;
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
            // KPI Log 
            .addCase(getKpiLogStatus.pending, (state) => {
                state.kpiLog.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getKpiLogStatus.fulfilled, (state, action) => {
                console.log('API LOG RESPONSE', action.payload);
                console.log('ITEMS LENGTH', action.payload?.items?.length);
                state.kpiLog.$list.status = ReduxStatus.SUCCESS;
                state.kpiLog.$list.data = action.payload?.items ?? [];
                state.kpiLog.$list.total = action.payload?.totalItem ?? 0;
            })
            .addCase(getKpiLogStatus.rejected, (state) => {
                state.kpiLog.$list.status = ReduxStatus.FAILURE;
            })
            .addCase(askKpiAi.pending, (state) => {
                state.kpiChat.status = ReduxStatus.LOADING;
            })
            .addCase(askKpiAi.fulfilled, (state, action) => {
                state.kpiChat.status = ReduxStatus.SUCCESS;
                state.kpiChat.answer = action.payload; // Payload là string 'answer' từ AI
                state.kpiChat.history.push({ role: 'ai', content: action.payload });
            })
            .addCase(askKpiAi.rejected, (state) => {
                state.kpiChat.status = ReduxStatus.FAILURE;
                state.kpiChat.answer = "Xin lỗi, trợ lý AI đang gặp sự cố. Vui lòng thử lại sau.";
            })
            // Kpi Công Thức
            .addCase(getListKpiCongThuc.pending, (state) => {
                state.listCongThuc.status = ReduxStatus.LOADING;
            })
            .addCase(getListKpiCongThuc.fulfilled, (state, action) => {
                state.listCongThuc.status = ReduxStatus.SUCCESS;
                state.listCongThuc.data = action.payload || [];
            })
            .addCase(getListKpiCongThuc.rejected, (state) => {
                state.listCongThuc.status = ReduxStatus.FAILURE;
            });
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
    clearSeletedKpiTruong,
    setSelectedKpiTruong,
    resetStatusKpiTruong,
    clearSeletedKpiRole,
    setSelectedKpiRole,
    resetStatusKpiRole,
    resetChat,
    addMessageToHistory,
} = kpiSlice.actions;

export default kpiReducer;
