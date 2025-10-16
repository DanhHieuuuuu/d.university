import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { IViewChucVu } from '@models/danh-muc/chuc-vu.model';
import { IViewToBoMon } from '@models/danh-muc/to-bo-mon.model';
import { IViewPhongBan } from '@models/danh-muc/phong-ban.model';
import {
  IViewDanToc,
  IViewGioiTinh,
  IViewLoaiHopDong,
  IViewLoaiPhongBan,
  IViewQuanHeGiaDinh,
  IViewQuocTich,
  IViewTonGiao
} from '@models/danh-muc/common.model';
import { DanhMucService } from '@services/danhmuc.service';

export const getAllChucVu = createAsyncThunk('danhmuc/list-chucvu', async () => {
  try {
    const res = await DanhMucService.getListChucVu();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const getAllDanToc = createAsyncThunk('danhmuc/list-dantoc', async () => {
  try {
    const res = await DanhMucService.getListDanToc();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const getAllGioiTinh = createAsyncThunk('danhmuc/list-gioitinh', async () => {
  try {
    const res = await DanhMucService.getListGioiTinh();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const getAllLoaiHopDong = createAsyncThunk('danhmuc/list-loaihopdong', async () => {
  try {
    const res = await DanhMucService.getListLoaiHopDong();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const getAllLoaiPhongBan = createAsyncThunk('danhmuc/list-loaiphongban', async () => {
  try {
    const res = await DanhMucService.getListLoaiPhongBan();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const getAllPhongBan = createAsyncThunk('danhmuc/list-phongban', async () => {
  try {
    const res = await DanhMucService.getListPhongBan();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const getAllQuanHeGiaDinh = createAsyncThunk('danhmuc/list-qhgd', async () => {
  try {
    const res = await DanhMucService.getListQuanHeGiaDinh();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const getAllQuocTich = createAsyncThunk('danhmuc/list-quoctich', async () => {
  try {
    const res = await DanhMucService.getListQuocTich();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const getAllToBoMon = createAsyncThunk('danhmuc/list-tobomon', async () => {
  try {
    const res = await DanhMucService.getListToBoMon();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});
export const getAllTonGiao = createAsyncThunk('danhmuc/list-tongiao', async () => {
  try {
    const res = await DanhMucService.getListTonGiao();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

interface DanhMucState {
  listChucVu: IViewChucVu[];
  listDanToc: IViewDanToc[];
  listGioiTinh: IViewGioiTinh[];
  listLoaiHopDong: IViewLoaiHopDong[];
  listLoaiPhongBan: IViewLoaiPhongBan[];
  listPhongBan: IViewPhongBan[];
  listQuanHe: IViewQuanHeGiaDinh[];
  listQuocTich: IViewQuocTich[];
  listToBoMon: IViewToBoMon[];
  listTonGiao: IViewTonGiao[];
}

const initialState: DanhMucState = {
  listChucVu: [],
  listDanToc: [],
  listGioiTinh: [],
  listLoaiHopDong: [],
  listLoaiPhongBan: [],
  listPhongBan: [],
  listQuanHe: [],
  listQuocTich: [],
  listToBoMon: [],
  listTonGiao: []
};

const danhmucSlice = createSlice({
  name: 'danhmuc',
  initialState,
  reducers: {},
  extraReducers: (buidler) => {
    buidler
      .addCase(getAllChucVu.fulfilled, (state, action) => {
        state.listChucVu = action.payload!.items;
      })
      .addCase(getAllDanToc.fulfilled, (state, action) => {
        state.listDanToc = action.payload!.items;
      })
      .addCase(getAllGioiTinh.fulfilled, (state, action) => {
        state.listGioiTinh = action.payload!.items;
      })
      .addCase(getAllLoaiHopDong.fulfilled, (state, action) => {
        state.listLoaiHopDong = action.payload!.items;
      })
      .addCase(getAllLoaiPhongBan.fulfilled, (state, action) => {
        state.listLoaiPhongBan = action.payload!.items;
      })
      .addCase(getAllPhongBan.fulfilled, (state, action) => {
        state.listPhongBan = action.payload!.items;
      })
      .addCase(getAllQuanHeGiaDinh.fulfilled, (state, action) => {
        state.listQuanHe = action.payload!.items;
      })
      .addCase(getAllQuocTich.fulfilled, (state, action) => {
        state.listQuocTich = action.payload!.items;
      })
      .addCase(getAllToBoMon.fulfilled, (state, action) => {
        state.listToBoMon = action.payload!.items;
      })
      .addCase(getAllTonGiao.fulfilled, (state, action) => {
        state.listTonGiao = action.payload!.items;
      });
  }
});

const danhmucReducer = danhmucSlice.reducer;

export const {} = danhmucSlice.actions;

export default danhmucReducer;
