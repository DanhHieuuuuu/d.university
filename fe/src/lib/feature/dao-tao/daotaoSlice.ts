import { ReduxStatus } from '@redux/const';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { CRUD } from '@models/common/common';

import { IViewKhoa } from '@models/dao-tao/khoa.model';
import { IViewNganh } from '@models/dao-tao/nganh.model';
import { IViewChuyenNganh } from '@models/dao-tao/chuyenNganh.model';
import { IViewMonHoc } from '@models/dao-tao/monHoc.model';
import { IViewMonTienQuyet } from '@models/dao-tao/monTienQuyet.model';
import { IViewChuongTrinhKhung } from '@models/dao-tao/chuongTrinhKhung.model';
import { IViewChuongTrinhKhungMon } from '@models/dao-tao/chuongTrinhKhungMon.model';

import { getAllKhoa, getKhoaById, createKhoa, updateKhoa, deleteKhoa } from './khoaThunk';
import { getAllNganh, getNganhById, createNganh, updateNganh, deleteNganh } from './nganhThunk';
import {
  getAllChuyenNganh,
  getChuyenNganhById,
  createChuyenNganh,
  updateChuyenNganh,
  deleteChuyenNganh
} from './chuyenNganhThunk';
import { getAllMonHoc, getMonHocById, createMonHoc, updateMonHoc, deleteMonHoc } from './monHocThunk';
import {
  getAllMonTienQuyet,
  getMonTienQuyetById,
  createMonTienQuyet,
  updateMonTienQuyet,
  deleteMonTienQuyet
} from './monTienQuyetThunk';
import {
  getAllChuongTrinhKhung,
  getChuongTrinhKhungById,
  createChuongTrinhKhung,
  updateChuongTrinhKhung,
  deleteChuongTrinhKhung
} from './chuongTrinhKhungThunk';
import {
  getAllChuongTrinhKhungMon,
  getChuongTrinhKhungMonById,
  createChuongTrinhKhungMon,
  updateChuongTrinhKhungMon,
  deleteChuongTrinhKhungMon
} from './chuongTrinhKhungMonThunk';

interface DaoTaoState {
  khoa: CRUD<IViewKhoa>;
  nganh: CRUD<IViewNganh>;
  chuyenNganh: CRUD<IViewChuyenNganh>;
  monHoc: CRUD<IViewMonHoc>;
  monTienQuyet: CRUD<IViewMonTienQuyet>;
  chuongTrinhKhung: CRUD<IViewChuongTrinhKhung>;
  chuongTrinhKhungMon: CRUD<IViewChuongTrinhKhungMon>;
  listKhoa: IViewKhoa[];
  listNganh: IViewNganh[];
  listChuyenNganh: IViewChuyenNganh[];
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
  chuongTrinhKhung: {
    $create: { status: ReduxStatus.IDLE },
    $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    $update: { status: ReduxStatus.IDLE },
    $delete: { status: ReduxStatus.IDLE },
    $selected: { status: ReduxStatus.IDLE, id: null, data: null }
  },
  chuongTrinhKhungMon: {
    $create: { status: ReduxStatus.IDLE },
    $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    $update: { status: ReduxStatus.IDLE },
    $delete: { status: ReduxStatus.IDLE },
    $selected: { status: ReduxStatus.IDLE, id: null, data: null }
  },
  listKhoa: [],
  listNganh: [],
  listChuyenNganh: [],
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
    },
    // ChuongTrinhKhung reducers
    clearSelectedChuongTrinhKhung: (state) => {
      state.chuongTrinhKhung.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
    },
    setSelectedIdChuongTrinhKhung: (state, action: PayloadAction<number>) => {
      state.chuongTrinhKhung.$selected.id = action.payload;
    },
    resetStatusChuongTrinhKhung: (state) => {
      state.chuongTrinhKhung.$create.status = ReduxStatus.IDLE;
      state.chuongTrinhKhung.$update.status = ReduxStatus.IDLE;
      state.chuongTrinhKhung.$delete.status = ReduxStatus.IDLE;
    },
    // ChuongTrinhKhungMon reducers
    clearSelectedChuongTrinhKhungMon: (state) => {
      state.chuongTrinhKhungMon.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
    },
    setSelectedIdChuongTrinhKhungMon: (state, action: PayloadAction<number>) => {
      state.chuongTrinhKhungMon.$selected.id = action.payload;
    },
    resetStatusChuongTrinhKhungMon: (state) => {
      state.chuongTrinhKhungMon.$create.status = ReduxStatus.IDLE;
      state.chuongTrinhKhungMon.$update.status = ReduxStatus.IDLE;
      state.chuongTrinhKhungMon.$delete.status = ReduxStatus.IDLE;
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
        state.listChuyenNganh = action.payload?.items || [];
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
      })
      // getAllChuongTrinhKhung
      .addCase(getAllChuongTrinhKhung.pending, (state) => {
        state.chuongTrinhKhung.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getAllChuongTrinhKhung.fulfilled, (state, action) => {
        state.chuongTrinhKhung.$list.status = ReduxStatus.SUCCESS;
        state.chuongTrinhKhung.$list.data = action.payload?.items || [];
        state.chuongTrinhKhung.$list.total = action.payload?.totalItem || 0;
      })
      .addCase(getAllChuongTrinhKhung.rejected, (state) => {
        state.chuongTrinhKhung.$list.status = ReduxStatus.FAILURE;
      })
      // getChuongTrinhKhungById
      .addCase(getChuongTrinhKhungById.pending, (state) => {
        state.chuongTrinhKhung.$selected.status = ReduxStatus.LOADING;
      })
      .addCase(getChuongTrinhKhungById.fulfilled, (state, action) => {
        state.chuongTrinhKhung.$selected.status = ReduxStatus.SUCCESS;
        state.chuongTrinhKhung.$selected.data = action.payload || null;
      })
      .addCase(getChuongTrinhKhungById.rejected, (state) => {
        state.chuongTrinhKhung.$selected.status = ReduxStatus.FAILURE;
      })
      // createChuongTrinhKhung
      .addCase(createChuongTrinhKhung.pending, (state) => {
        state.chuongTrinhKhung.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createChuongTrinhKhung.fulfilled, (state) => {
        state.chuongTrinhKhung.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createChuongTrinhKhung.rejected, (state) => {
        state.chuongTrinhKhung.$create.status = ReduxStatus.FAILURE;
      })
      // updateChuongTrinhKhung
      .addCase(updateChuongTrinhKhung.pending, (state) => {
        state.chuongTrinhKhung.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updateChuongTrinhKhung.fulfilled, (state) => {
        state.chuongTrinhKhung.$update.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateChuongTrinhKhung.rejected, (state) => {
        state.chuongTrinhKhung.$update.status = ReduxStatus.FAILURE;
      })
      // deleteChuongTrinhKhung
      .addCase(deleteChuongTrinhKhung.pending, (state) => {
        state.chuongTrinhKhung.$delete.status = ReduxStatus.LOADING;
      })
      .addCase(deleteChuongTrinhKhung.fulfilled, (state) => {
        state.chuongTrinhKhung.$delete.status = ReduxStatus.SUCCESS;
      })
      .addCase(deleteChuongTrinhKhung.rejected, (state) => {
        state.chuongTrinhKhung.$delete.status = ReduxStatus.FAILURE;
      })
      // getAllChuongTrinhKhungMon
      .addCase(getAllChuongTrinhKhungMon.pending, (state) => {
        state.chuongTrinhKhungMon.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getAllChuongTrinhKhungMon.fulfilled, (state, action) => {
        state.chuongTrinhKhungMon.$list.status = ReduxStatus.SUCCESS;
        state.chuongTrinhKhungMon.$list.data = action.payload?.items || [];
        state.chuongTrinhKhungMon.$list.total = action.payload?.totalItem || 0;
      })
      .addCase(getAllChuongTrinhKhungMon.rejected, (state) => {
        state.chuongTrinhKhungMon.$list.status = ReduxStatus.FAILURE;
      })
      // getChuongTrinhKhungMonById
      .addCase(getChuongTrinhKhungMonById.pending, (state) => {
        state.chuongTrinhKhungMon.$selected.status = ReduxStatus.LOADING;
      })
      .addCase(getChuongTrinhKhungMonById.fulfilled, (state, action) => {
        state.chuongTrinhKhungMon.$selected.status = ReduxStatus.SUCCESS;
        state.chuongTrinhKhungMon.$selected.data = action.payload || null;
      })
      .addCase(getChuongTrinhKhungMonById.rejected, (state) => {
        state.chuongTrinhKhungMon.$selected.status = ReduxStatus.FAILURE;
      })
      // createChuongTrinhKhungMon
      .addCase(createChuongTrinhKhungMon.pending, (state) => {
        state.chuongTrinhKhungMon.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createChuongTrinhKhungMon.fulfilled, (state) => {
        state.chuongTrinhKhungMon.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createChuongTrinhKhungMon.rejected, (state) => {
        state.chuongTrinhKhungMon.$create.status = ReduxStatus.FAILURE;
      })
      // updateChuongTrinhKhungMon
      .addCase(updateChuongTrinhKhungMon.pending, (state) => {
        state.chuongTrinhKhungMon.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updateChuongTrinhKhungMon.fulfilled, (state) => {
        state.chuongTrinhKhungMon.$update.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateChuongTrinhKhungMon.rejected, (state) => {
        state.chuongTrinhKhungMon.$update.status = ReduxStatus.FAILURE;
      })
      // deleteChuongTrinhKhungMon
      .addCase(deleteChuongTrinhKhungMon.pending, (state) => {
        state.chuongTrinhKhungMon.$delete.status = ReduxStatus.LOADING;
      })
      .addCase(deleteChuongTrinhKhungMon.fulfilled, (state) => {
        state.chuongTrinhKhungMon.$delete.status = ReduxStatus.SUCCESS;
      })
      .addCase(deleteChuongTrinhKhungMon.rejected, (state) => {
        state.chuongTrinhKhungMon.$delete.status = ReduxStatus.FAILURE;
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
  resetStatusMonTienQuyet,
  clearSelectedChuongTrinhKhung,
  setSelectedIdChuongTrinhKhung,
  resetStatusChuongTrinhKhung,
  clearSelectedChuongTrinhKhungMon,
  setSelectedIdChuongTrinhKhungMon,
  resetStatusChuongTrinhKhungMon
} = daotaoSlice.actions;

export default daotaoReducer;
