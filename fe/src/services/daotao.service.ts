import { ICreateKhoa, IQueryKhoa, IUpdateKhoa, IViewKhoa } from '@models/dao-tao/khoa.model';
import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseList, IResponseItem } from '@models/common/response.model';
import { ICreateNganh, IQueryNganh, IUpdateNganh, IViewNganh } from '@models/dao-tao/nganh.model';
import {
  ICreateChuyenNganh,
  IQueryChuyenNganh,
  IUpdateChuyenNganh,
  IViewChuyenNganh
} from '@models/dao-tao/chuyenNganh.model';
import { ICreateMonHoc, IQueryMonHoc, IUpdateMonHoc, IViewMonHoc } from '@models/dao-tao/monHoc.model';
import {
  ICreateMonTienQuyet,
  IQueryMonTienQuyet,
  IUpdateMonTienQuyet,
  IViewMonTienQuyet
} from '@models/dao-tao/monTienQuyet.model';
import {
  ICreateChuongTrinhKhung,
  IQueryChuongTrinhKhung,
  IUpdateChuongTrinhKhung,
  IViewChuongTrinhKhung
} from '@models/dao-tao/chuongTrinhKhung.model';
import {
  ICreateChuongTrinhKhungMon,
  IQueryChuongTrinhKhungMon,
  IUpdateChuongTrinhKhungMon,
  IViewChuongTrinhKhungMon
} from '@models/dao-tao/chuongTrinhKhungMon.model';

const apiDaoTaoEndpoint = 'daotao';
const apiKhoaEndpoint = `${apiDaoTaoEndpoint}/khoa`;
const apiNganhEndpoint = `${apiDaoTaoEndpoint}/nganh`;
const apiChuyenNganhEndpoint = `${apiDaoTaoEndpoint}/chuyen-nganh`;
const apiMonHocEndpoint = `${apiDaoTaoEndpoint}/mon-hoc`;
const apiMonTienQuyetEndpoint = `${apiDaoTaoEndpoint}/mon-tien-quyet`;
const apiChuongTrinhKhungEndpoint = `${apiDaoTaoEndpoint}/chuong-trinh-khung`;
const apiChuongTrinhKhungMonEndpoint = `${apiDaoTaoEndpoint}/chuong-trinh-khung-mon`;

// Khoa
const getListKhoa = async (query?: IQueryKhoa) => {
  try {
    const res = await axios.get(`${apiKhoaEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewKhoa> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getKhoaById = async (idKhoa: number) => {
  try {
    const res = await axios.get(`${apiKhoaEndpoint}/get-by-id?Id=${idKhoa}`);

    const data: IResponseItem<IViewKhoa> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createKhoa = async (body: ICreateKhoa) => {
  try {
    const res = await axios.post(`${apiKhoaEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateKhoa = async (body: IUpdateKhoa) => {
  try {
    const res = await axios.put(`${apiKhoaEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteKhoa = async (id: number) => {
  try {
    const res = await axios.delete(`${apiKhoaEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

// Ngành
const getListNganh = async (query?: IQueryNganh) => {
  try {
    const res = await axios.get(`${apiNganhEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewNganh> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getNganhById = async (idNganh: number) => {
  try {
    const res = await axios.get(`${apiNganhEndpoint}/get-by-id?Id=${idNganh}`);

    const data: IResponseItem<IViewNganh> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createNganh = async (body: ICreateNganh) => {
  try {
    const res = await axios.post(`${apiNganhEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateNganh = async (body: IUpdateNganh) => {
  try {
    const res = await axios.put(`${apiNganhEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteNganh = async (id: number) => {
  try {
    const res = await axios.delete(`${apiNganhEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

// Chuyên ngành
const getListChuyenNganh = async (query?: IQueryChuyenNganh) => {
  try {
    const res = await axios.get(`${apiChuyenNganhEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewChuyenNganh> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getChuyenNganhById = async (idChuyenNganh: number) => {
  try {
    const res = await axios.get(`${apiChuyenNganhEndpoint}/get-by-id?Id=${idChuyenNganh}`);

    const data: IResponseItem<IViewChuyenNganh> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createChuyenNganh = async (body: ICreateChuyenNganh) => {
  try {
    const res = await axios.post(`${apiChuyenNganhEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateChuyenNganh = async (body: IUpdateChuyenNganh) => {
  try {
    const res = await axios.put(`${apiChuyenNganhEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteChuyenNganh = async (id: number) => {
  try {
    const res = await axios.delete(`${apiChuyenNganhEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

// Môn học
const getListMonHoc = async (query?: IQueryMonHoc) => {
  try {
    const res = await axios.get(`${apiMonHocEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewMonHoc> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getMonHocById = async (idMonHoc: number) => {
  try {
    const res = await axios.get(`${apiMonHocEndpoint}/get-by-id?Id=${idMonHoc}`);

    const data: IResponseItem<IViewMonHoc> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createMonHoc = async (body: ICreateMonHoc) => {
  try {
    const res = await axios.post(`${apiMonHocEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateMonHoc = async (body: IUpdateMonHoc) => {
  try {
    const res = await axios.put(`${apiMonHocEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteMonHoc = async (id: number) => {
  try {
    const res = await axios.delete(`${apiMonHocEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

// Môn Tiên Quyết
const getListMonTienQuyet = async (query?: IQueryMonTienQuyet) => {
  try {
    const res = await axios.get(`${apiMonTienQuyetEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewMonTienQuyet> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getMonTienQuyetById = async (idMonTienQuyet: number) => {
  try {
    const res = await axios.get(`${apiMonTienQuyetEndpoint}/get-by-id?Id=${idMonTienQuyet}`);

    const data: IResponseItem<IViewMonTienQuyet> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createMonTienQuyet = async (body: ICreateMonTienQuyet) => {
  try {
    const res = await axios.post(`${apiMonTienQuyetEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateMonTienQuyet = async (body: IUpdateMonTienQuyet) => {
  try {
    const res = await axios.put(`${apiMonTienQuyetEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteMonTienQuyet = async (id: number) => {
  try {
    const res = await axios.delete(`${apiMonTienQuyetEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

// Chương trình khung
const getListChuongTrinhKhung = async (query?: IQueryChuongTrinhKhung) => {
  try {
    const res = await axios.get(`${apiChuongTrinhKhungEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewChuongTrinhKhung> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getChuongTrinhKhungById = async (idChuongTrinhKhung: number) => {
  try {
    const res = await axios.get(`${apiChuongTrinhKhungEndpoint}/get-by-id?Id=${idChuongTrinhKhung}`);

    const data: IResponseItem<IViewChuongTrinhKhung> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createChuongTrinhKhung = async (body: ICreateChuongTrinhKhung) => {
  try {
    const res = await axios.post(`${apiChuongTrinhKhungEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateChuongTrinhKhung = async (body: IUpdateChuongTrinhKhung) => {
  try {
    const res = await axios.put(`${apiChuongTrinhKhungEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteChuongTrinhKhung = async (id: number) => {
  try {
    const res = await axios.delete(`${apiChuongTrinhKhungEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

// Chương trình khung môn
const getListChuongTrinhKhungMon = async (query?: IQueryChuongTrinhKhungMon) => {
  try {
    const res = await axios.get(`${apiChuongTrinhKhungMonEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewChuongTrinhKhungMon> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const getChuongTrinhKhungMonById = async (idChuongTrinhKhungMon: number) => {
  try {
    const res = await axios.get(`${apiChuongTrinhKhungMonEndpoint}/get-by-id?Id=${idChuongTrinhKhungMon}`);

    const data: IResponseItem<IViewChuongTrinhKhungMon> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createChuongTrinhKhungMon = async (body: ICreateChuongTrinhKhungMon) => {
  try {
    const res = await axios.post(`${apiChuongTrinhKhungMonEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateChuongTrinhKhungMon = async (body: IUpdateChuongTrinhKhungMon) => {
  try {
    const res = await axios.put(`${apiChuongTrinhKhungMonEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteChuongTrinhKhungMon = async (id: number) => {
  try {
    const res = await axios.delete(`${apiChuongTrinhKhungMonEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

export const DaoTaoService = {
  getListKhoa,
  getKhoaById,
  createKhoa,
  updateKhoa,
  deleteKhoa,

  getListNganh,
  getNganhById,
  createNganh,
  updateNganh,
  deleteNganh,

  getListChuyenNganh,
  getChuyenNganhById,
  createChuyenNganh,
  updateChuyenNganh,
  deleteChuyenNganh,

  getListMonHoc,
  getMonHocById,
  createMonHoc,
  updateMonHoc,
  deleteMonHoc,

  getListMonTienQuyet,
  getMonTienQuyetById,
  createMonTienQuyet,
  updateMonTienQuyet,
  deleteMonTienQuyet,

  getListChuongTrinhKhung,
  getChuongTrinhKhungById,
  createChuongTrinhKhung,
  updateChuongTrinhKhung,
  deleteChuongTrinhKhung,

  getListChuongTrinhKhungMon,
  getChuongTrinhKhungMonById,
  createChuongTrinhKhungMon,
  updateChuongTrinhKhungMon,
  deleteChuongTrinhKhungMon
};
