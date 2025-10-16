import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseList, IResponseItem } from '@models/common/response.model';
import { ICreateChucVu, IQueryChucVu, IViewChucVu } from '@models/danh-muc/chuc-vu.model';
import {
  IViewDanToc,
  IViewGioiTinh,
  IViewLoaiHopDong,
  IViewLoaiPhongBan,
  IViewQuanHeGiaDinh,
  IViewQuocTich,
  IViewTonGiao
} from '@models/danh-muc/common.model';
import { ICreatePhongBan, IQueryPhongBan, IViewPhongBan } from '@models/danh-muc/phong-ban.model';
import { ICreateToBoMon, IQueryToBoMon, IViewToBoMon } from '@models/danh-muc/to-bo-mon.model';

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

export const DanhMucService = {
  getListChucVu,
  getChucVuById,
  createChucVu,
  getListDanToc,
  getListGioiTinh,
  getListLoaiHopDong,
  getListLoaiPhongBan,
  getListPhongBan,
  getPhongBanById,
  createPhongBan,
  getListQuanHeGiaDinh,
  getListQuocTich,
  getListToBoMon,
  createToBoMon,
  getListTonGiao
};
