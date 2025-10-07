import { ICreateHopDongNs, ICreateNhanSu, IQueryNhanSu, IViewNhanSu } from '@models/nhansu/nhansu.model';
import { IResponseList } from '@models/common/response.model';
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
        keyword: keyword
      }
    });
    // Trả về data (object API)
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tìm kiếm nhân sự.'); // 💡 ĐIỂM QUAN TRỌNG: Throws lỗi để Redux Thunk xử lý là rejected action
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

const createHopDong = async (body: ICreateHopDongNs) => {
  try {
    const res = await axios.post(`${apiNhanSuEndpoint}/create-hd`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

export const NhanSuService = { findPaging, find, createNhanSu, createHopDong };
