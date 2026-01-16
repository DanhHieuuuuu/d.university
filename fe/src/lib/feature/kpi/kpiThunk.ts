import { createAsyncThunk } from '@reduxjs/toolkit';

import { KpiService } from '@services/kpi.service';
import { ICreateKpiCaNhan, IQueryKpiCaNhan, IUpdateKpiCaNhan, IUpdateTrangThaiKpiCaNhan, IUpdateKpiCaNhanThucTeList, IViewKpiCaNhan, IUpdateCapTrenDanhGiaList } from '@models/kpi/kpi-ca-nhan.model';
import { ICreateKpiDonVi, IGiaoKpiDonVi, IQueryKpiDonVi, IUpdateCapTrenDonViDanhGiaList, IUpdateKpiDonVi, IUpdateKpiDonViThucTeList, IUpdateTrangThaiKpiDonVi, IViewKpiDonVi } from '@models/kpi/kpi-don-vi.model';
import { ICreateKpiRole, IQueryKpiRole, IUpdateKpiRole, IViewKpiRole } from '@models/kpi/kpi-role.model';
import { ICreateKpiTruong, IQueryKpiTruong, IUpdateCapTrenTruongDanhGiaList, IUpdateKpiTruong, IUpdateKpiTruongThucTeList, IUpdateTrangThaiKpiTruong } from '@models/kpi/kpi-truong.model';
import { IQueryKpiLogStatus, KpiLogStatusResponse } from '@models/kpi/kpi-log.model';
import { IAskKpiChatCommand } from '@models/kpi/kpi-chat.model';
import { IQueryKpiCongThuc } from '@models/kpi/kpi-cong-thuc.model';

export const getAllKpiCaNhan = createAsyncThunk('kpi/list-canhan', async (payload?: IQueryKpiCaNhan) => {
    try {
        const res = await KpiService.getListKpiCaNhan(payload);

        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const getKpiCaNhanKeKhai = createAsyncThunk(
    'kpi-canhan/ke-khai',
    async (payload: IQueryKpiCaNhan | undefined, { rejectWithValue }) => {
        try {
            const res = await KpiService.getListKpiCaNhanKeKhai(payload);
            return res.data;
        } catch (err: any) {
            return rejectWithValue(err?.response?.data);
        }
    }
);

export const getAllIdsKpiCaNhan = createAsyncThunk(
    'kpi-canhan/getAllIds',
    async (payload: IQueryKpiCaNhan) => {
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

export const getListTrangThaiKpiCaNhan = createAsyncThunk(
    'kpiCaNhan/getListTrangThai',
    async () => {
        return await KpiService.getListTrangThaiKpiCaNhan();
    }
);

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

export const updateKetQuaCapTrenKpiCaNhan = createAsyncThunk(
    'kpi/update-ketqua-captren-canhan',
    async (payload: IUpdateCapTrenDanhGiaList, { rejectWithValue }) => {
        try {
            const res = await KpiService.updateKetQuaCapTrenKpiCaNhan(payload);
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

export const getKpiDonViKeKhai = createAsyncThunk(
    'kpi-donvi/ke-khai',
    async (payload: IQueryKpiDonVi | undefined, { rejectWithValue }) => {
        try {
            const res = await KpiService.getListKpiDonViKeKhai(payload);
            return res.data;
        } catch (err: any) {
            return rejectWithValue(err?.response?.data);
        }
    }
);

export const getAllIdsKpiDonVi = createAsyncThunk(
    'kpi-donvi/getAllIds',
    async (payload: IQueryKpiDonVi) => {
        try {
            const res = await KpiService.getListKpiDonVi(payload);
            const items = res?.data?.items;

            if (!items || !Array.isArray(items)) {
                return [];
            }
            return items.map((item: IViewKpiDonVi) => item.id);
        } catch (error) {
            console.error("Lỗi lấy ID hàng loạt:", error);
            throw error;
        }
    }
);

export const createKpiDonVi = createAsyncThunk(
    'kpi/create-donvi',
    async (payload: ICreateKpiDonVi, { rejectWithValue }) => {
        try {
            const res = await KpiService.createKpiDonVi(payload);
            return res;
        } catch (err: any) {
            return rejectWithValue(err?.response?.data);
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

export const deleteKpiDonVi = createAsyncThunk(
    'kpi/delete-donvi',
    async (id: number, { rejectWithValue }) => {
        try {
            const res = await KpiService.deleteKpiDonVi(id);
            return res.data;
        } catch (error: any) {
            return rejectWithValue(error?.response?.data || 'Delete failed');
        }
    }
);


export const updateTrangThaiKpiDonVi = createAsyncThunk(
    'kpi/update-trang-thai-donvi',
    async (payload: IUpdateTrangThaiKpiDonVi, { rejectWithValue }) => {
        try {
            const res = await KpiService.updateTrangThaiKpiDonVi(payload);
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

export const updateKetQuaThucTeKpiDonVi = createAsyncThunk(
    'kpi/update-ketqua-thucte-donvi',
    async (payload: IUpdateKpiDonViThucTeList, { rejectWithValue }) => {
        try {
            const res = await KpiService.updateKetQuaThucTeKpiDonVi(payload);
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

export const updateKetQuaCapTrenKpiDonVi = createAsyncThunk(
    'kpi/update-ketqua-captren-donvi',
    async (payload: IUpdateCapTrenDonViDanhGiaList, { rejectWithValue }) => {
        try {
            const res = await KpiService.updateKetQuaCapTrenKpiDonVi(payload);
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

export const getListNamHocKpiDonVi = createAsyncThunk(
    'kpiDonVi/getListNamHoc',
    async () => {
        return await KpiService.getListNamHocKpiDonVi();
    }
);

export const getListTrangThaiKpiDonVi = createAsyncThunk(
    'kpiDonVi/getListTrangThai',
    async () => {
        return await KpiService.getListTrangThaiKpiDonVi();
    }
);

export const giaoKpiDonVi = createAsyncThunk(
    'kpi/giao-kpi-donvi',
    async (payload: IGiaoKpiDonVi, { rejectWithValue }) => {
        try {
            return await KpiService.giaoKpiDonVi(payload);
        } catch (error: any) {
            return rejectWithValue(
                error?.response?.data || { message: 'Giao KPI thất bại' }
            );
        }
    }
);

export const getNhanSuDaGiaoByKpiDonVi = createAsyncThunk(
    'kpi-donvi/get-nhan-su-da-giao',
    async (idKpiDonVi: number, { rejectWithValue }) => {
        try {
            const res = await KpiService.getNhanSuDaGiaoByKpiDonVi(idKpiDonVi);
            return res;
        } catch (err: any) {
            return rejectWithValue(
                err?.message || 'Không lấy được nhân sự đã giao KPI'
            );
        }
    }
);
// Kpi trường

export const getAllKpiTruong = createAsyncThunk('kpi/list-truong', async (payload?: IQueryKpiTruong) => {
    try {
        const res = await KpiService.getListKpiTruong(payload);
        console.log('res', res);
        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const createKpiTruong = createAsyncThunk(
    'kpi/create-truong',
    async (payload: ICreateKpiTruong, { rejectWithValue }) => {
        try {
            const res = await KpiService.createKpiTruong(payload);
            return res;
        } catch (err: any) {
            return rejectWithValue(err?.response?.data);
        }
    });

export const updateKpiTruong = createAsyncThunk('kpi/update-truong', async (payload: IUpdateKpiTruong) => {
    try {
        const res = await KpiService.updateKpiTruong(payload);

        return res.data;
    } catch (error: any) {
        console.error(error);
    }
});

export const deleteKpiTruong = createAsyncThunk(
    'kpi/delete-truong',
    async (id: number, { rejectWithValue }) => {
        try {
            const res = await KpiService.deleteKpiTruong(id);
            return res.data;
        } catch (error: any) {
            return rejectWithValue(error?.response?.data || 'Delete failed');
        }
    }
);


export const updateTrangThaiKpiTruong = createAsyncThunk(
    'kpi/update-trang-thai-truong',
    async (payload: IUpdateTrangThaiKpiTruong, { rejectWithValue }) => {
        try {
            const res = await KpiService.updateTrangThaiKpiTruong(payload);
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

export const updateKetQuaThucTeKpiTruong = createAsyncThunk(
    'kpi/update-ketqua-thucte-truong',
    async (payload: IUpdateKpiTruongThucTeList, { rejectWithValue }) => {
        try {
            const res = await KpiService.updateKetQuaThucTeKpiTruong(payload);
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

export const updateKetQuaCapTrenKpiTruong = createAsyncThunk(
    'kpi/update-ketqua-captren-truong',
    async (payload: IUpdateCapTrenTruongDanhGiaList, { rejectWithValue }) => {
        try {
            const res = await KpiService.updateKetQuaCapTrenKpiTruong(payload);
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

export const getListNamHocKpiTruong = createAsyncThunk(
    'kpiTruong/getListNamHoc',
    async () => {
        return await KpiService.getListNamHocKpiTruong();
    }
);

export const getListTrangThaiKpiTruong = createAsyncThunk(
    'kpiTruong/getListTrangThai',
    async () => {
        return await KpiService.getListTrangThaiKpiTruong();
    }
);
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

export const getListKpiRoleByUser = createAsyncThunk(
    'kpi-role/list-role-by-user',
    async () => {
        return await KpiService.getListKpiRoleByUser();
    }
);

//Kpi log
export const getKpiLogStatus = createAsyncThunk<KpiLogStatusResponse,IQueryKpiLogStatus>(
  'kpi-log/getList',
  async (query, { rejectWithValue }) => {
    try {
      const res = await KpiService.getKpiLogs(query);
      return res.data; 
    } catch (err: any) {
      return rejectWithValue(err?.response?.data || { message: 'Lỗi' });
    }
  }
);

//Kpi AI Chat
export const askKpiAi = createAsyncThunk(
    'kpi/chat-ai',
    async (payload: IAskKpiChatCommand, { rejectWithValue }) => {
        try {
            const res = await KpiService.askKpiAi(payload);
            return res.data; 
        } catch (err: any) {
            return rejectWithValue(err?.response?.data || 'Không thể kết nối với trợ lý AI');
        }
    }
);
//Kpi Công Thức
export const getListKpiCongThuc = createAsyncThunk(
  'kpi/getListKpiCongThuc',
  async (query: IQueryKpiCongThuc | undefined, { rejectWithValue }) => {
    try {
      const data = await KpiService.getListKpiCongThuc(query);
      return data;
    } catch (err) {
      return rejectWithValue(err);
    }
  }
);