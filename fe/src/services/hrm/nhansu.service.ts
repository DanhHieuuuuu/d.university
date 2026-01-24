import {
  ICreateNhanSu,
  IQueryNhanSu,
  IUpdateNhanSu,
  IViewNhanSu
} from '@models/nhansu/nhansu.model';
import { IResponseItem, IResponseList } from '@models/common/response.model';
import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';

const apiNhanSuEndpoint = 'nhansu';

const findPaging = async (query: IQueryNhanSu) => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewNhanSu> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const find = async (keyword: string) => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/get`, {
      params: {
        key: keyword
      }
    });
    // Trả về data (object API)
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tìm kiếm nhân sự.');
    throw err;
  }
};

const findById = async (id: number) => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/${id}`);

    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tìm kiếm nhân sự.');
    throw err;
  }
};

const createNhanSu = async (body: ICreateNhanSu) => {
  try {
    const res = await axios.post(`${apiNhanSuEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateNhanSu = async (body: IUpdateNhanSu) => {
  try {
    const res = await axios.put(`${apiNhanSuEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const getHoSoNhanSu = async (id: number) => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/ho-so/${id}`);

    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tìm kiếm nhân sự.');
    throw err;
  }
};

const findBySdt = async (phone: string) => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/find?phone=${phone}`);

    const data: IResponseList<IViewNhanSu> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const thongKeNsTheoPhongBan = async () => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/thongke-theo-phongban`);

    const data = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const semanticSearch = async (args: IQueryNhanSu) => {
  try {    
    const res = await axios.get(`${apiNhanSuEndpoint}/search`, {
      params: {
        ...args
      }
    });

    const data = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

export const NhanSuService = {
  findPaging,
  find,
  findById,
  createNhanSu,
  updateNhanSu,
  getHoSoNhanSu,
  findBySdt,
  thongKeNsTheoPhongBan,
  semanticSearch
};
