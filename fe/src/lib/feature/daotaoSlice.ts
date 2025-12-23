import { ReduxStatus } from '@redux/const';
import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { CRUD } from '@models/common/common';
import { ICreateKhoa, IQueryKhoa, IUpdateKhoa, IViewKhoa } from '@models/dao-tao/khoa.model';
import { ICreateNganh, IQueryNganh, IUpdateNganh, IViewNganh } from '@models/dao-tao/nganh.model';
import { ICreateChuyenNganh, IQueryChuyenNganh, IUpdateChuyenNganh, IViewChuyenNganh } from '@models/dao-tao/chuyenNganh.model';
import { ICreateMonHoc, IQueryMonHoc, IUpdateMonHoc, IViewMonHoc } from '@models/dao-tao/monHoc.model';
import { ICreateMonTienQuyet, IQueryMonTienQuyet, IUpdateMonTienQuyet, IViewMonTienQuyet } from '@models/dao-tao/monTienQuyet.model';
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

// Nganh actions
export const getAllNganh = createAsyncThunk('daotao/list-nganh', async (payload?: IQueryNganh) => {
    try {
        const res = await DaoTaoService.getListNganh(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const createNganh = createAsyncThunk('daotao/create-nganh', async (payload: ICreateNganh) => {
    try {
        const res = await DaoTaoService.createNganh(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const updateNganh = createAsyncThunk('daotao/update-nganh', async (payload: IUpdateNganh) => {
    try {
        const res = await DaoTaoService.updateNganh(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const deleteNganh = createAsyncThunk('daotao/delete-nganh', async (payload: number) => {
    try {
        const res = await DaoTaoService.deleteNganh(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const getNganhById = createAsyncThunk('daotao/get-nganh', async (payload: number) => {
    try {
        const res = await DaoTaoService.getNganhById(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

// ChuyenNganh actions
export const getAllChuyenNganh = createAsyncThunk('daotao/list-chuyennganh', async (payload?: IQueryChuyenNganh) => {
    try {
        const res = await DaoTaoService.getListChuyenNganh(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const createChuyenNganh = createAsyncThunk('daotao/create-chuyennganh', async (payload: ICreateChuyenNganh) => {
    try {
        const res = await DaoTaoService.createChuyenNganh(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const updateChuyenNganh = createAsyncThunk('daotao/update-chuyennganh', async (payload: IUpdateChuyenNganh) => {
    try {
        const res = await DaoTaoService.updateChuyenNganh(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const deleteChuyenNganh = createAsyncThunk('daotao/delete-chuyennganh', async (payload: number) => {
    try {
        const res = await DaoTaoService.deleteChuyenNganh(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const getChuyenNganhById = createAsyncThunk('daotao/get-chuyennganh', async (payload: number) => {
    try {
        const res = await DaoTaoService.getChuyenNganhById(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

// MonHoc actions
export const getAllMonHoc = createAsyncThunk('daotao/list-monhoc', async (payload?: IQueryMonHoc) => {
    try {
        const res = await DaoTaoService.getListMonHoc(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const createMonHoc = createAsyncThunk('daotao/create-monhoc', async (payload: ICreateMonHoc) => {
    try {
        const res = await DaoTaoService.createMonHoc(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const updateMonHoc = createAsyncThunk('daotao/update-monhoc', async (payload: IUpdateMonHoc) => {
    try {
        const res = await DaoTaoService.updateMonHoc(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const deleteMonHoc = createAsyncThunk('daotao/delete-monhoc', async (payload: number) => {
    try {
        const res = await DaoTaoService.deleteMonHoc(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const getMonHocById = createAsyncThunk('daotao/get-monhoc', async (payload: number) => {
    try {
        const res = await DaoTaoService.getMonHocById(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

// MonTienQuyet actions
export const getAllMonTienQuyet = createAsyncThunk('daotao/list-montienquyet', async (payload?: IQueryMonTienQuyet) => {
    try {
        const res = await DaoTaoService.getListMonTienQuyet(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const createMonTienQuyet = createAsyncThunk('daotao/create-montienquyet', async (payload: ICreateMonTienQuyet) => {
    try {
        const res = await DaoTaoService.createMonTienQuyet(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const updateMonTienQuyet = createAsyncThunk('daotao/update-montienquyet', async (payload: IUpdateMonTienQuyet) => {
    try {
        const res = await DaoTaoService.updateMonTienQuyet(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const deleteMonTienQuyet = createAsyncThunk('daotao/delete-montienquyet', async (payload: number) => {
    try {
        const res = await DaoTaoService.deleteMonTienQuyet(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const getMonTienQuyetById = createAsyncThunk('daotao/get-montienquyet', async (payload: number) => {
    try {
        const res = await DaoTaoService.getMonTienQuyetById(payload);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

interface DaoTaoState {
    khoa: CRUD<IViewKhoa>;
    nganh: CRUD<IViewNganh>;
    chuyenNganh: CRUD<IViewChuyenNganh>;
    monHoc: CRUD<IViewMonHoc>;
    monTienQuyet: CRUD<IViewMonTienQuyet>;
    listKhoa: IViewKhoa[];
    listNganh: IViewNganh[];
    listMonHoc: IViewMonHoc[];
}

const initialState: DaoTaoState = {
    khoa: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null }
    },
    nganh: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null }
    },
    chuyenNganh: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null }
    },
    monHoc: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null }
    },
    monTienQuyet: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null }
    },
    listKhoa: [],
    listNganh: [],
    listMonHoc: []
};

const daotaoSlice = createSlice({
    name: 'daotao',
    initialState,
    reducers: {
        // Khoa reducers
        clearSelectedKhoa: (state) => {
            state.khoa.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
        },
        setSelectedIdKhoa: (state, action: PayloadAction<number>) => {
            state.khoa.$selected.id = action.payload;
        },
        resetStatusKhoa: (state) => {
            state.khoa.$create.status = ReduxStatus.IDLE;
            state.khoa.$update.status = ReduxStatus.IDLE;
            state.khoa.$delete.status = ReduxStatus.IDLE;
        },
        // Nganh reducers
        clearSelectedNganh: (state) => {
            state.nganh.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
        },
        setSelectedIdNganh: (state, action: PayloadAction<number>) => {
            state.nganh.$selected.id = action.payload;
        },
        resetStatusNganh: (state) => {
            state.nganh.$create.status = ReduxStatus.IDLE;
            state.nganh.$update.status = ReduxStatus.IDLE;
            state.nganh.$delete.status = ReduxStatus.IDLE;
        },
        // ChuyenNganh reducers
        clearSelectedChuyenNganh: (state) => {
            state.chuyenNganh.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
        },
        setSelectedIdChuyenNganh: (state, action: PayloadAction<number>) => {
            state.chuyenNganh.$selected.id = action.payload;
        },
        resetStatusChuyenNganh: (state) => {
            state.chuyenNganh.$create.status = ReduxStatus.IDLE;
            state.chuyenNganh.$update.status = ReduxStatus.IDLE;
            state.chuyenNganh.$delete.status = ReduxStatus.IDLE;
        },
        // MonHoc reducers
        clearSelectedMonHoc: (state) => {
            state.monHoc.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
        },
        setSelectedIdMonHoc: (state, action: PayloadAction<number>) => {
            state.monHoc.$selected.id = action.payload;
        },
        resetStatusMonHoc: (state) => {
            state.monHoc.$create.status = ReduxStatus.IDLE;
            state.monHoc.$update.status = ReduxStatus.IDLE;
            state.monHoc.$delete.status = ReduxStatus.IDLE;
        },
        // MonTienQuyet reducers
        clearSelectedMonTienQuyet: (state) => {
            state.monTienQuyet.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
        },
        setSelectedIdMonTienQuyet: (state, action: PayloadAction<number>) => {
            state.monTienQuyet.$selected.id = action.payload;
        },
        resetStatusMonTienQuyet: (state) => {
            state.monTienQuyet.$create.status = ReduxStatus.IDLE;
            state.monTienQuyet.$update.status = ReduxStatus.IDLE;
            state.monTienQuyet.$delete.status = ReduxStatus.IDLE;
        }
    },
    extraReducers: (builder) => {
        builder
            // getAllKhoa
            .addCase(getAllKhoa.pending, (state) => {
                state.khoa.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getAllKhoa.fulfilled, (state, action) => {
                state.khoa.$list.status = ReduxStatus.SUCCESS;
                state.khoa.$list.data = action.payload?.items || [];
                state.khoa.$list.total = action.payload?.totalItem || 0;
                state.listKhoa = action.payload?.items || [];
            })
            .addCase(getAllKhoa.rejected, (state) => {
                state.khoa.$list.status = ReduxStatus.FAILURE;
            })
            // getKhoaById
            .addCase(getKhoaById.pending, (state) => {
                state.khoa.$selected.status = ReduxStatus.LOADING;
            })
            .addCase(getKhoaById.fulfilled, (state, action) => {
                state.khoa.$selected.status = ReduxStatus.SUCCESS;
                state.khoa.$selected.data = action.payload || null;
            })
            .addCase(getKhoaById.rejected, (state) => {
                state.khoa.$selected.status = ReduxStatus.FAILURE;
            })
            // createKhoa
            .addCase(createKhoa.pending, (state) => {
                state.khoa.$create.status = ReduxStatus.LOADING;
            })
            .addCase(createKhoa.fulfilled, (state) => {
                state.khoa.$create.status = ReduxStatus.SUCCESS;
            })
            .addCase(createKhoa.rejected, (state) => {
                state.khoa.$create.status = ReduxStatus.FAILURE;
            })
            // updateKhoa
            .addCase(updateKhoa.pending, (state) => {
                state.khoa.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateKhoa.fulfilled, (state) => {
                state.khoa.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateKhoa.rejected, (state) => {
                state.khoa.$update.status = ReduxStatus.FAILURE;
            })
            // deleteKhoa
            .addCase(deleteKhoa.pending, (state) => {
                state.khoa.$delete.status = ReduxStatus.LOADING;
            })
            .addCase(deleteKhoa.fulfilled, (state) => {
                state.khoa.$delete.status = ReduxStatus.SUCCESS;
            })
            .addCase(deleteKhoa.rejected, (state) => {
                state.khoa.$delete.status = ReduxStatus.FAILURE;
            })
            // getAllNganh
            .addCase(getAllNganh.pending, (state) => {
                state.nganh.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getAllNganh.fulfilled, (state, action) => {
                state.nganh.$list.status = ReduxStatus.SUCCESS;
                state.nganh.$list.data = action.payload?.items || [];
                state.nganh.$list.total = action.payload?.totalItem || 0;
                state.listNganh = action.payload?.items || [];
            })
            .addCase(getAllNganh.rejected, (state) => {
                state.nganh.$list.status = ReduxStatus.FAILURE;
            })
            // getNganhById
            .addCase(getNganhById.pending, (state) => {
                state.nganh.$selected.status = ReduxStatus.LOADING;
            })
            .addCase(getNganhById.fulfilled, (state, action) => {
                state.nganh.$selected.status = ReduxStatus.SUCCESS;
                state.nganh.$selected.data = action.payload || null;
            })
            .addCase(getNganhById.rejected, (state) => {
                state.nganh.$selected.status = ReduxStatus.FAILURE;
            })
            // createNganh
            .addCase(createNganh.pending, (state) => {
                state.nganh.$create.status = ReduxStatus.LOADING;
            })
            .addCase(createNganh.fulfilled, (state) => {
                state.nganh.$create.status = ReduxStatus.SUCCESS;
            })
            .addCase(createNganh.rejected, (state) => {
                state.nganh.$create.status = ReduxStatus.FAILURE;
            })
            // updateNganh
            .addCase(updateNganh.pending, (state) => {
                state.nganh.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateNganh.fulfilled, (state) => {
                state.nganh.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateNganh.rejected, (state) => {
                state.nganh.$update.status = ReduxStatus.FAILURE;
            })
            // deleteNganh
            .addCase(deleteNganh.pending, (state) => {
                state.nganh.$delete.status = ReduxStatus.LOADING;
            })
            .addCase(deleteNganh.fulfilled, (state) => {
                state.nganh.$delete.status = ReduxStatus.SUCCESS;
            })
            .addCase(deleteNganh.rejected, (state) => {
                state.nganh.$delete.status = ReduxStatus.FAILURE;
            })
            // getAllChuyenNganh
            .addCase(getAllChuyenNganh.pending, (state) => {
                state.chuyenNganh.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getAllChuyenNganh.fulfilled, (state, action) => {
                state.chuyenNganh.$list.status = ReduxStatus.SUCCESS;
                state.chuyenNganh.$list.data = action.payload?.items || [];
                state.chuyenNganh.$list.total = action.payload?.totalItem || 0;
            })
            .addCase(getAllChuyenNganh.rejected, (state) => {
                state.chuyenNganh.$list.status = ReduxStatus.FAILURE;
            })
            // getChuyenNganhById
            .addCase(getChuyenNganhById.pending, (state) => {
                state.chuyenNganh.$selected.status = ReduxStatus.LOADING;
            })
            .addCase(getChuyenNganhById.fulfilled, (state, action) => {
                state.chuyenNganh.$selected.status = ReduxStatus.SUCCESS;
                state.chuyenNganh.$selected.data = action.payload || null;
            })
            .addCase(getChuyenNganhById.rejected, (state) => {
                state.chuyenNganh.$selected.status = ReduxStatus.FAILURE;
            })
            // createChuyenNganh
            .addCase(createChuyenNganh.pending, (state) => {
                state.chuyenNganh.$create.status = ReduxStatus.LOADING;
            })
            .addCase(createChuyenNganh.fulfilled, (state) => {
                state.chuyenNganh.$create.status = ReduxStatus.SUCCESS;
            })
            .addCase(createChuyenNganh.rejected, (state) => {
                state.chuyenNganh.$create.status = ReduxStatus.FAILURE;
            })
            // updateChuyenNganh
            .addCase(updateChuyenNganh.pending, (state) => {
                state.chuyenNganh.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateChuyenNganh.fulfilled, (state) => {
                state.chuyenNganh.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateChuyenNganh.rejected, (state) => {
                state.chuyenNganh.$update.status = ReduxStatus.FAILURE;
            })
            // deleteChuyenNganh
            .addCase(deleteChuyenNganh.pending, (state) => {
                state.chuyenNganh.$delete.status = ReduxStatus.LOADING;
            })
            .addCase(deleteChuyenNganh.fulfilled, (state) => {
                state.chuyenNganh.$delete.status = ReduxStatus.SUCCESS;
            })
            .addCase(deleteChuyenNganh.rejected, (state) => {
                state.chuyenNganh.$delete.status = ReduxStatus.FAILURE;
            })
            // getAllMonHoc
            .addCase(getAllMonHoc.pending, (state) => {
                state.monHoc.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getAllMonHoc.fulfilled, (state, action) => {
                state.monHoc.$list.status = ReduxStatus.SUCCESS;
                state.monHoc.$list.data = action.payload?.items || [];
                state.monHoc.$list.total = action.payload?.totalItem || 0;
                state.listMonHoc = action.payload?.items || [];
            })
            .addCase(getAllMonHoc.rejected, (state) => {
                state.monHoc.$list.status = ReduxStatus.FAILURE;
            })
            // getMonHocById
            .addCase(getMonHocById.pending, (state) => {
                state.monHoc.$selected.status = ReduxStatus.LOADING;
            })
            .addCase(getMonHocById.fulfilled, (state, action) => {
                state.monHoc.$selected.status = ReduxStatus.SUCCESS;
                state.monHoc.$selected.data = action.payload || null;
            })
            .addCase(getMonHocById.rejected, (state) => {
                state.monHoc.$selected.status = ReduxStatus.FAILURE;
            })
            // createMonHoc
            .addCase(createMonHoc.pending, (state) => {
                state.monHoc.$create.status = ReduxStatus.LOADING;
            })
            .addCase(createMonHoc.fulfilled, (state) => {
                state.monHoc.$create.status = ReduxStatus.SUCCESS;
            })
            .addCase(createMonHoc.rejected, (state) => {
                state.monHoc.$create.status = ReduxStatus.FAILURE;
            })
            // updateMonHoc
            .addCase(updateMonHoc.pending, (state) => {
                state.monHoc.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateMonHoc.fulfilled, (state) => {
                state.monHoc.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateMonHoc.rejected, (state) => {
                state.monHoc.$update.status = ReduxStatus.FAILURE;
            })
            // deleteMonHoc
            .addCase(deleteMonHoc.pending, (state) => {
                state.monHoc.$delete.status = ReduxStatus.LOADING;
            })
            .addCase(deleteMonHoc.fulfilled, (state) => {
                state.monHoc.$delete.status = ReduxStatus.SUCCESS;
            })
            .addCase(deleteMonHoc.rejected, (state) => {
                state.monHoc.$delete.status = ReduxStatus.FAILURE;
            })
            // getAllMonTienQuyet
            .addCase(getAllMonTienQuyet.pending, (state) => {
                state.monTienQuyet.$list.status = ReduxStatus.LOADING;
            })
            .addCase(getAllMonTienQuyet.fulfilled, (state, action) => {
                state.monTienQuyet.$list.status = ReduxStatus.SUCCESS;
                state.monTienQuyet.$list.data = action.payload?.items || [];
                state.monTienQuyet.$list.total = action.payload?.totalItem || 0;
            })
            .addCase(getAllMonTienQuyet.rejected, (state) => {
                state.monTienQuyet.$list.status = ReduxStatus.FAILURE;
            })
            // getMonTienQuyetById
            .addCase(getMonTienQuyetById.pending, (state) => {
                state.monTienQuyet.$selected.status = ReduxStatus.LOADING;
            })
            .addCase(getMonTienQuyetById.fulfilled, (state, action) => {
                state.monTienQuyet.$selected.status = ReduxStatus.SUCCESS;
                state.monTienQuyet.$selected.data = action.payload || null;
            })
            .addCase(getMonTienQuyetById.rejected, (state) => {
                state.monTienQuyet.$selected.status = ReduxStatus.FAILURE;
            })
            // createMonTienQuyet
            .addCase(createMonTienQuyet.pending, (state) => {
                state.monTienQuyet.$create.status = ReduxStatus.LOADING;
            })
            .addCase(createMonTienQuyet.fulfilled, (state) => {
                state.monTienQuyet.$create.status = ReduxStatus.SUCCESS;
            })
            .addCase(createMonTienQuyet.rejected, (state) => {
                state.monTienQuyet.$create.status = ReduxStatus.FAILURE;
            })
            // updateMonTienQuyet
            .addCase(updateMonTienQuyet.pending, (state) => {
                state.monTienQuyet.$update.status = ReduxStatus.LOADING;
            })
            .addCase(updateMonTienQuyet.fulfilled, (state) => {
                state.monTienQuyet.$update.status = ReduxStatus.SUCCESS;
            })
            .addCase(updateMonTienQuyet.rejected, (state) => {
                state.monTienQuyet.$update.status = ReduxStatus.FAILURE;
            })
            // deleteMonTienQuyet
            .addCase(deleteMonTienQuyet.pending, (state) => {
                state.monTienQuyet.$delete.status = ReduxStatus.LOADING;
            })
            .addCase(deleteMonTienQuyet.fulfilled, (state) => {
                state.monTienQuyet.$delete.status = ReduxStatus.SUCCESS;
            })
            .addCase(deleteMonTienQuyet.rejected, (state) => {
                state.monTienQuyet.$delete.status = ReduxStatus.FAILURE;
            });
    }
});

const daotaoReducer = daotaoSlice.reducer;

export const {
    clearSelectedKhoa,
    setSelectedIdKhoa,
    resetStatusKhoa,
    clearSelectedNganh,
    setSelectedIdNganh,
    resetStatusNganh,
    clearSelectedChuyenNganh,
    setSelectedIdChuyenNganh,
    resetStatusChuyenNganh,
    clearSelectedMonHoc,
    setSelectedIdMonHoc,
    resetStatusMonHoc,
    clearSelectedMonTienQuyet,
    setSelectedIdMonTienQuyet,
    resetStatusMonTienQuyet
} = daotaoSlice.actions;

export default daotaoReducer;
