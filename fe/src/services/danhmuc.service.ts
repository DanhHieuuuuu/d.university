import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseList, IResponseItem } from '@models/common/response.model';
import { ICreateChucVu, IQueryChucVu, IUpdateChucVu, IViewChucVu } from '@models/danh-muc/chuc-vu.model';
import {
  IViewDanToc,
  IViewGioiTinh,
  IViewLoaiHopDong,
  IViewLoaiPhongBan,
  IViewQuanHeGiaDinh,
  IViewQuocTich,
  IViewTonGiao
} from '@models/danh-muc/common.model';
import { ICreatePhongBan, IQueryPhongBan, IUpdatePhongBan, IViewPhongBan } from '@models/danh-muc/phong-ban.model';
import { ICreateToBoMon, IQueryToBoMon, IUpdateToBoMon, IViewToBoMon } from '@models/danh-muc/to-bo-mon.model';
import { IQueryKhoaHoc, IViewKhoaHoc } from '@models/danh-muc/khoa-hoc.model';

const apiDanhMucEndpoint = 'danhmuc';

const apiChucVuEndpoint = `${apiDanhMucEndpoint}/chuc-vu`;
const apiDanTocEndpoint = `${apiDanhMucEndpoint}/dan-toc`;
const apiGioiTinhEndpoint = `${apiDanhMucEndpoint}/gioi-tinh`;
const apiLoaiHopDongEndpoint = `${apiDanhMucEndpoint}/loai-hop-dong`;
const apiLoaiPhongBanEndpoint = `${apiDanhMucEndpoint}/loai-phong-ban`;
const apiPhongBanEndpoint = `${apiDanhMucEndpoint}/phong-ban`;
const apiQuanHeEndpoint = `${apiDanhMucEndpoint}/quan-he`;
const apiQuocTichEndpoint = `${apiDanhMucEndpoint}/quoc-tich`;
const apiToBoMonEndpoint = `${apiDanhMucEndpoint}/to-bo-mon`;
const apiTonGiaoEndpoint = `${apiDanhMucEndpoint}/ton-giao`;
const apiKhoaHocEndpoint = `${apiDanhMucEndpoint}/khoa-hoc`;

const getListChucVu = async (query?: IQueryChucVu) => {
  try {
    const res = await axios.get(`${apiChucVuEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewChucVu> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getChucVuById = async (idChucVu: number) => {
  try {
    const res = await axios.get(`${apiChucVuEndpoint}/get-by-id?Id=${idChucVu}`);

    const data: IResponseItem<IViewChucVu> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createChucVu = async (body: ICreateChucVu) => {
  try {
    const res = await axios.post(`${apiChucVuEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateChucVu = async (body: IUpdateChucVu) => {
  try {
    const res = await axios.put(`${apiChucVuEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteChucVu = async (id: number) => {
  try {
    const res = await axios.delete(`${apiChucVuEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const getListDanToc = async () => {
  try {
    const res = await axios.get(`${apiDanTocEndpoint}`);

    const data: IResponseList<IViewDanToc> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getListGioiTinh = async () => {
  try {
    const res = await axios.get(`${apiGioiTinhEndpoint}`);

    const data: IResponseList<IViewGioiTinh> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getListLoaiHopDong = async () => {
  try {
    const res = await axios.get(`${apiLoaiHopDongEndpoint}`);

    const data: IResponseList<IViewLoaiHopDong> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getListLoaiPhongBan = async () => {
  try {
    const res = await axios.get(`${apiLoaiPhongBanEndpoint}`);

    const data: IResponseList<IViewLoaiPhongBan> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getListPhongBan = async (query?: IQueryPhongBan) => {
  try {
    const res = await axios.get(`${apiPhongBanEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewPhongBan> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getListPhongBanByKpiRole = async (query?: IQueryPhongBan) => {
  try {
    const res = await axios.get(`${apiPhongBanEndpoint}/get-by-kpi-role`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewPhongBan> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getPhongBanById = async (idPhongBan: number) => {
  try {
    const res = await axios.get(`${apiPhongBanEndpoint}/get-by-id?Id=${idPhongBan}`);

    const data: IResponseItem<IViewPhongBan> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createPhongBan = async (body: ICreatePhongBan) => {
  try {
    const res = await axios.post(`${apiPhongBanEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updatePhongBan = async (body: IUpdatePhongBan) => {
  try {
    const res = await axios.put(`${apiPhongBanEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deletePhongBan = async (id: number) => {
  try {
    const res = await axios.delete(`${apiPhongBanEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const getListQuanHeGiaDinh = async () => {
  try {
    const res = await axios.get(`${apiQuanHeEndpoint}`);

    const data: IResponseList<IViewQuanHeGiaDinh> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getListQuocTich = async () => {
  try {
    const res = await axios.get(`${apiQuocTichEndpoint}`);

    const data: IResponseList<IViewQuocTich> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getListToBoMon = async (query?: IQueryToBoMon) => {
  try {
    const res = await axios.get(`${apiToBoMonEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewToBoMon> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createToBoMon = async (body: ICreateToBoMon) => {
  try {
    const res = await axios.post(`${apiToBoMonEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateToBoMon = async (body: IUpdateToBoMon) => {
  try {
    const res = await axios.put(`${apiToBoMonEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteToBoMon = async (id: number) => {
  try {
    const res = await axios.delete(`${apiToBoMonEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const getToBoMonById = async (idToBoMon: number) => {
  try {
    const res = await axios.get(`${apiToBoMonEndpoint}/${idToBoMon}`);

    const data: IResponseItem<IViewToBoMon> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getListTonGiao = async () => {
  try {
    const res = await axios.get(`${apiTonGiaoEndpoint}`);

    const data: IResponseList<IViewTonGiao> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getListKhoaHoc = async (query?: IQueryKhoaHoc) => {
  try {
    const res = await axios.get(`${apiKhoaHocEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewKhoaHoc> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getKhoaHocById = async (idKhoaHoc: number) => {
  try {
    const res = await axios.get(`${apiKhoaHocEndpoint}/get-by-id?Id=${idKhoaHoc}`);

    const data: IResponseItem<IViewKhoaHoc> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

export const DanhMucService = {
  getListChucVu,
  getChucVuById,
  createChucVu,
  updateChucVu,
  deleteChucVu,
  getListDanToc,
  getListGioiTinh,
  getListLoaiHopDong,
  getListLoaiPhongBan,
  getListPhongBan,
  getListPhongBanByKpiRole,
  getPhongBanById,
  createPhongBan,
  updatePhongBan,
  deletePhongBan,
  getListQuanHeGiaDinh,
  getListQuocTich,
  getListToBoMon,
  createToBoMon,
  updateToBoMon,
  deleteToBoMon,
  getToBoMonById,
  getListTonGiao,
  getListKhoaHoc,
  getKhoaHocById
};
