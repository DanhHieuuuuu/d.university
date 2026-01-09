import { createAsyncThunk } from '@reduxjs/toolkit';
import { DanhMucService } from '@services/danhmuc.service';
import { ICreateChucVu, IQueryChucVu, IUpdateChucVu } from '@models/danh-muc/chuc-vu.model';
import { ICreateToBoMon, IQueryToBoMon, IUpdateToBoMon } from '@models/danh-muc/to-bo-mon.model';
import { ICreatePhongBan, IQueryPhongBan, IUpdatePhongBan } from '@models/danh-muc/phong-ban.model';
import { IQueryKhoaHoc } from '@models/danh-muc/khoa-hoc.model';

export const getAllChucVu = createAsyncThunk(
  'danhmuc/list-chucvu',
  async (payload: IQueryChucVu | undefined, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.getListChucVu(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const createChucVu = createAsyncThunk(
  'danhmuc/create-chucvu',
  async (payload: ICreateChucVu, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.createChucVu(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const updateChucVu = createAsyncThunk(
  'danhmuc/update-chucvu',
  async (payload: IUpdateChucVu, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.updateChucVu(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const deleteChucVu = createAsyncThunk('danhmuc/delete-chucvu', async (payload: number, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.deleteChucVu(payload);

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getChucVuById = createAsyncThunk('danhmuc/get-chucvu', async (payload: number, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.getChucVuById(payload);

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getAllDanToc = createAsyncThunk('danhmuc/list-dantoc', async (_, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.getListDanToc();

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getAllGioiTinh = createAsyncThunk('danhmuc/list-gioitinh', async (_, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.getListGioiTinh();

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getAllLoaiHopDong = createAsyncThunk('danhmuc/list-loaihopdong', async (_, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.getListLoaiHopDong();

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getAllLoaiPhongBan = createAsyncThunk('danhmuc/list-loaiphongban', async (_, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.getListLoaiPhongBan();

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getAllPhongBan = createAsyncThunk(
  'danhmuc/list-phongban',
  async (payload: IQueryPhongBan | undefined, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.getListPhongBan(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const getAllPhongBanByKpiRole = createAsyncThunk(
  'danhmuc/list-phongban-by-kpi-role',
  async (payload: IQueryPhongBan | undefined, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.getListPhongBanByKpiRole(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const createPhongBan = createAsyncThunk(
  'danhmuc/create-phongban',
  async (payload: ICreatePhongBan, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.createPhongBan(payload);
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

export const updatePhongBan = createAsyncThunk(
  'danhmuc/update-phongban',
  async (payload: IUpdatePhongBan, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.updatePhongBan(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const deletePhongBan = createAsyncThunk(
  'danhmuc/delete-phongban',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.deletePhongBan(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const getPhongBanById = createAsyncThunk(
  'danhmuc/get-phongban',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.getPhongBanById(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const getAllQuanHeGiaDinh = createAsyncThunk('danhmuc/list-qhgd', async (_, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.getListQuanHeGiaDinh();

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getAllQuocTich = createAsyncThunk('danhmuc/list-quoctich', async (_, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.getListQuocTich();

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getAllToBoMon = createAsyncThunk(
  'danhmuc/list-tobomon',
  async (payload: IQueryToBoMon | undefined, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.getListToBoMon(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const createToBoMon = createAsyncThunk(
  'danhmuc/create-toBoMon',
  async (payload: ICreateToBoMon, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.createToBoMon(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const updateToBoMon = createAsyncThunk(
  'danhmuc/update-toBoMon',
  async (payload: IUpdateToBoMon, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.updateToBoMon(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const deleteToBoMon = createAsyncThunk(
  'danhmuc/delete-toBoMon',
  async (payload: number, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.deleteToBoMon(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);

export const getToBoMonById = createAsyncThunk('danhmuc/get-toBoMon', async (payload: number, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.getToBoMonById(payload);

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getAllTonGiao = createAsyncThunk('danhmuc/list-tongiao', async (_, { rejectWithValue }) => {
  try {
    const res = await DanhMucService.getListTonGiao();

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue({
      message: error.message,
      code: error.code,
      response: error.response?.data
    });
  }
});

export const getAllKhoaHoc = createAsyncThunk(
  'danhmuc/list-khoahoc',
  async (payload: IQueryKhoaHoc | undefined, { rejectWithValue }) => {
    try {
      const res = await DanhMucService.getListKhoaHoc(payload);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue({
        message: error.message,
        code: error.code,
        response: error.response?.data
      });
    }
  }
);
