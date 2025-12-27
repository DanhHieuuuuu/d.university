import { ReduxStatus } from '@redux/const';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { CRUD } from '@models/common/common';

import { IViewChucVu } from '@models/danh-muc/chuc-vu.model';
import { IViewToBoMon } from '@models/danh-muc/to-bo-mon.model';
import { IViewPhongBan } from '@models/danh-muc/phong-ban.model';
import { IViewKhoaHoc } from '@models/danh-muc/khoa-hoc.model';
import {
  IViewDanToc,
  IViewGioiTinh,
  IViewLoaiHopDong,
  IViewLoaiPhongBan,
  IViewQuanHeGiaDinh,
  IViewQuocTich,
  IViewTonGiao
} from '@models/danh-muc/common.model';

import {
  getAllChucVu,
  createChucVu,
  createPhongBan,
  createToBoMon,
  deleteChucVu,
  deletePhongBan,
  deleteToBoMon,
  getAllDanToc,
  getAllGioiTinh,
  getAllKhoaHoc,
  getAllLoaiHopDong,
  getAllLoaiPhongBan,
  getAllPhongBan,
  getAllQuanHeGiaDinh,
  getAllQuocTich,
  getAllToBoMon,
  getAllTonGiao,
  getChucVuById,
  getPhongBanById,
  getToBoMonById,
  updateChucVu,
  updatePhongBan,
  updateToBoMon
} from './danhmucThunk';

interface DanhMucState {
  chucVu: CRUD<IViewChucVu>;
  toBoMon: CRUD<IViewToBoMon>;
  phongBan: CRUD<IViewPhongBan>;
  listDanToc: IViewDanToc[];
  listGioiTinh: IViewGioiTinh[];
  listKhoaHoc: IViewKhoaHoc[];
  listLoaiHopDong: IViewLoaiHopDong[];
  listLoaiPhongBan: IViewLoaiPhongBan[];
  listQuanHe: IViewQuanHeGiaDinh[];
  listQuocTich: IViewQuocTich[];
  listToBoMon: IViewToBoMon[];
  listTonGiao: IViewTonGiao[];
}

const initialState: DanhMucState = {
  chucVu: {
    $create: { status: ReduxStatus.IDLE },
    $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    $update: { status: ReduxStatus.IDLE },
    $delete: { status: ReduxStatus.IDLE },
    $selected: { status: ReduxStatus.IDLE, id: null, data: null }
  },
  phongBan: {
    $create: { status: ReduxStatus.IDLE },
    $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    $update: { status: ReduxStatus.IDLE },
    $delete: { status: ReduxStatus.IDLE },
    $selected: { status: ReduxStatus.IDLE, id: null, data: null }
  },
  toBoMon: {
    $create: { status: ReduxStatus.IDLE },
    $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    $update: { status: ReduxStatus.IDLE },
    $delete: { status: ReduxStatus.IDLE },
    $selected: { status: ReduxStatus.IDLE, id: null, data: null }
  },
  listDanToc: [],
  listGioiTinh: [],
  listKhoaHoc: [],
  listLoaiHopDong: [],
  listLoaiPhongBan: [],
  listQuanHe: [],
  listQuocTich: [],
  listToBoMon: [],
  listTonGiao: []
};

const danhmucSlice = createSlice({
  name: 'danhmuc',
  initialState,
  reducers: {
    clearSeletedChucVu: (state) => {
      state.chucVu.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
    },
    setSelectedIdChucVu: (state, action: PayloadAction<number>) => {
      state.chucVu.$selected.id = action.payload;
    },
    resetStatusChucVu: (state) => {
      state.chucVu.$create.status = ReduxStatus.IDLE;
      state.chucVu.$update.status = ReduxStatus.IDLE;
      state.chucVu.$delete.status = ReduxStatus.IDLE;
    },

    clearSelectedPhongBan: (state) => {
      state.phongBan.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
    },
    setSelectedIdPhongBan: (state, action: PayloadAction<number>) => {
      state.phongBan.$selected.id = action.payload;
    },
    resetStatusPhongBan: (state) => {
      state.phongBan.$create.status = ReduxStatus.IDLE;
      state.phongBan.$update.status = ReduxStatus.IDLE;
      state.phongBan.$delete.status = ReduxStatus.IDLE;
    },

    clearSeletedToBoMon: (state) => {
      state.toBoMon.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
    },
    setSelectedIdToBoMon: (state, action: PayloadAction<number>) => {
      state.toBoMon.$selected.id = action.payload;
    },
    resetStatusToBoMon: (state) => {
      state.toBoMon.$create.status = ReduxStatus.IDLE;
      state.toBoMon.$update.status = ReduxStatus.IDLE;
      state.toBoMon.$delete.status = ReduxStatus.IDLE;
    }
  },
  extraReducers: (buidler) => {
    buidler
      .addCase(getAllChucVu.pending, (state) => {
        state.chucVu.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getAllChucVu.fulfilled, (state, action) => {
        state.chucVu.$list.status = ReduxStatus.SUCCESS;
        state.chucVu.$list.data = action.payload?.items || [];
        state.chucVu.$list.total = action.payload?.totalItem || 0;
      })
      .addCase(getAllChucVu.rejected, (state) => {
        state.chucVu.$list.status = ReduxStatus.FAILURE;
      })
      .addCase(getChucVuById.pending, (state) => {
        state.chucVu.$selected.status = ReduxStatus.LOADING;
      })
      .addCase(getChucVuById.fulfilled, (state, action) => {
        state.chucVu.$selected.status = ReduxStatus.SUCCESS;
        state.chucVu.$selected.data = action.payload || null;
      })
      .addCase(getChucVuById.rejected, (state) => {
        state.chucVu.$selected.status = ReduxStatus.FAILURE;
      })
      .addCase(createChucVu.pending, (state) => {
        state.chucVu.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createChucVu.fulfilled, (state, action) => {
        state.chucVu.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createChucVu.rejected, (state) => {
        state.chucVu.$create.status = ReduxStatus.FAILURE;
      })
      .addCase(updateChucVu.pending, (state) => {
        state.chucVu.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updateChucVu.fulfilled, (state, action) => {
        state.chucVu.$update.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateChucVu.rejected, (state) => {
        state.chucVu.$update.status = ReduxStatus.FAILURE;
      })
      .addCase(deleteChucVu.pending, (state) => {
        state.chucVu.$delete.status = ReduxStatus.LOADING;
      })
      .addCase(deleteChucVu.fulfilled, (state, action) => {
        state.chucVu.$delete.status = ReduxStatus.SUCCESS;
      })
      .addCase(deleteChucVu.rejected, (state) => {
        state.chucVu.$delete.status = ReduxStatus.FAILURE;
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
      .addCase(getAllPhongBan.pending, (state) => {
        state.phongBan.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getAllPhongBan.fulfilled, (state, action) => {
        state.phongBan.$list.status = ReduxStatus.SUCCESS;
        state.phongBan.$list.data = action.payload?.items || [];
        state.phongBan.$list.total = action.payload?.totalItem || 0;
      })
      .addCase(getAllPhongBan.rejected, (state) => {
        state.phongBan.$list.status = ReduxStatus.FAILURE;
      })
      .addCase(getPhongBanById.pending, (state) => {
        state.phongBan.$selected.status = ReduxStatus.LOADING;
      })
      .addCase(getPhongBanById.fulfilled, (state, action) => {
        state.phongBan.$selected.status = ReduxStatus.SUCCESS;
        state.phongBan.$selected.data = action.payload || null;
      })
      .addCase(getPhongBanById.rejected, (state) => {
        state.phongBan.$selected.status = ReduxStatus.FAILURE;
      })
      .addCase(createPhongBan.pending, (state) => {
        state.phongBan.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createPhongBan.fulfilled, (state, action) => {
        state.phongBan.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createPhongBan.rejected, (state) => {
        state.phongBan.$create.status = ReduxStatus.FAILURE;
      })
      .addCase(updatePhongBan.pending, (state) => {
        state.phongBan.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updatePhongBan.fulfilled, (state, action) => {
        state.phongBan.$update.status = ReduxStatus.SUCCESS;
      })
      .addCase(updatePhongBan.rejected, (state) => {
        state.phongBan.$update.status = ReduxStatus.FAILURE;
      })
      .addCase(deletePhongBan.pending, (state) => {
        state.phongBan.$delete.status = ReduxStatus.LOADING;
      })
      .addCase(deletePhongBan.fulfilled, (state, action) => {
        state.phongBan.$delete.status = ReduxStatus.SUCCESS;
      })
      .addCase(deletePhongBan.rejected, (state) => {
        state.phongBan.$delete.status = ReduxStatus.FAILURE;
      })
      .addCase(getAllQuanHeGiaDinh.fulfilled, (state, action) => {
        state.listQuanHe = action.payload!.items;
      })
      .addCase(getAllQuocTich.fulfilled, (state, action) => {
        state.listQuocTich = action.payload!.items;
      })
      //ToBoMon
      .addCase(getAllToBoMon.pending, (state) => {
        state.toBoMon.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getAllToBoMon.fulfilled, (state, action) => {
        state.toBoMon.$list.status = ReduxStatus.SUCCESS;
        state.toBoMon.$list.data = action.payload?.items || [];
        state.toBoMon.$list.total = action.payload?.totalItem || 0;
      })
      .addCase(getAllToBoMon.rejected, (state) => {
        state.toBoMon.$list.status = ReduxStatus.FAILURE;
      })
      .addCase(getToBoMonById.pending, (state) => {
        state.toBoMon.$selected.status = ReduxStatus.LOADING;
      })
      .addCase(getToBoMonById.fulfilled, (state, action) => {
        state.toBoMon.$selected.status = ReduxStatus.SUCCESS;
        state.toBoMon.$selected.data = action.payload || null;
      })
      .addCase(getToBoMonById.rejected, (state) => {
        state.toBoMon.$selected.status = ReduxStatus.FAILURE;
      })

      .addCase(createToBoMon.pending, (state) => {
        state.toBoMon.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createToBoMon.fulfilled, (state) => {
        state.toBoMon.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createToBoMon.rejected, (state) => {
        state.toBoMon.$create.status = ReduxStatus.FAILURE;
      })
      .addCase(updateToBoMon.pending, (state) => {
        state.toBoMon.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updateToBoMon.fulfilled, (state) => {
        state.toBoMon.$update.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateToBoMon.rejected, (state) => {
        state.toBoMon.$update.status = ReduxStatus.FAILURE;
      })

      .addCase(deleteToBoMon.pending, (state) => {
        state.toBoMon.$delete.status = ReduxStatus.LOADING;
      })
      .addCase(deleteToBoMon.fulfilled, (state) => {
        state.toBoMon.$delete.status = ReduxStatus.SUCCESS;
      })
      .addCase(deleteToBoMon.rejected, (state) => {
        state.toBoMon.$delete.status = ReduxStatus.FAILURE;
      })
      //TonGiao
      .addCase(getAllTonGiao.fulfilled, (state, action) => {
        state.listTonGiao = action.payload!.items;
      })
      //KhoaHoc
      .addCase(getAllKhoaHoc.fulfilled, (state, action) => {
        state.listKhoaHoc = action.payload!.items;
      });
  }
});

const danhmucReducer = danhmucSlice.reducer;

export const {
  clearSeletedChucVu,
  setSelectedIdChucVu,
  resetStatusChucVu,
  clearSelectedPhongBan,
  setSelectedIdPhongBan,
  resetStatusPhongBan,
  clearSeletedToBoMon,
  setSelectedIdToBoMon,
  resetStatusToBoMon
} = danhmucSlice.actions;

export default danhmucReducer;
