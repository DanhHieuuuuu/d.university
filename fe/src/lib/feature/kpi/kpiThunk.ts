import { createAsyncThunk } from '@reduxjs/toolkit';

import { KpiService } from '@services/kpi.service';
import { ICreateKpiCaNhan, IQueryKpiCaNhan, IUpdateKpiCaNhan, IUpdateTrangThaiKpiCaNhan, IUpdateKpiCaNhanThucTeList, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import { ICreateKpiDonVi, IQueryKpiDonVi, IUpdateKpiDonVi } from '@models/kpi/kpi-don-vi.model';
import { ICreateKpiRole, IQueryKpiRole, IUpdateKpiRole, IViewKpiRole } from '@models/kpi/kpi-role.model';

export const getAllKpiCaNhan = createAsyncThunk('kpi/list-canhan', async (payload?: IQueryKpiCaNhan) => {
    try {
        const res = await KpiService.getListKpiCaNhan(payload);

        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const getAllIdsKpiCaNhan = createAsyncThunk(
    'kpi-canhan/getAllIds',
    async (payload: IQueryKpiRole) => {
        try {
            const res = await KpiService.getListKpiCaNhan(payload);
            const items = res?.data?.items;

            if (!items || !Array.isArray(items)) {
                return [];
            }
            return items.map((item: IViewKpiCaNhan) => item.id);
        } catch (error) {
            console.error("Lỗi lấy ID hàng loạt:", error);
            throw error;
        }
    }
);
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
export const updateTrangThaiKpiCaNhan = createAsyncThunk(
    'kpi/update-trang-thai-canhan',
    async (payload: IUpdateTrangThaiKpiCaNhan, { rejectWithValue }) => {
        try {
            const res = await KpiService.updateTrangThaiKpiCaNhan(payload);
            return res;
        } catch (error: any) {
            return rejectWithValue(
                error?.response?.data || {
                    message: 'Cập nhật trạng thái KPI thất bại'
                }
            );
        }
    }
);

export const updateKetQuaThucTeKpiCaNhan = createAsyncThunk(
    'kpi/update-ketqua-thucte-canhan',
    async (payload: IUpdateKpiCaNhanThucTeList, { rejectWithValue }) => {
        try {
            const res = await KpiService.updateKetQuaThucTeKpiCaNhan(payload);
            return res;
        } catch (error: any) {
            return rejectWithValue(
                error?.response?.data || {
                    message: 'Cập nhật kết quả thực tế KPI thất bại'
                }
            );
        }
    }
);


//Kpi Don Vi
export const getAllKpiDonVi = createAsyncThunk('kpi/list-donvi', async (payload?: IQueryKpiDonVi) => {
    try {
        const res = await KpiService.getListKpiDonVi(payload);
        console.log('res', res);
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
export const getAllIdsKpiRole = createAsyncThunk(
    'kpi-role/getAllIds',
    async (payload: IQueryKpiRole) => {
        try {
            const res = await KpiService.getListKpiRole(payload);
            const items = res?.data?.items;

            if (!items || !Array.isArray(items)) {
                return [];
            }
            return items.map((item: IViewKpiRole) => item.id);
        } catch (error) {
            console.error("Lỗi lấy ID hàng loạt:", error);
            throw error;
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
