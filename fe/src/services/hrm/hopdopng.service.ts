import { ICreateHopDong, IQueryHopDong, IViewHopDong } from '@models/nhansu/hopdong.model';
import { IResponseList } from '@models/common/response.model';
import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';

const apiContractEndpoint = 'nhansu/contract';

const createHopDong = async (body: ICreateHopDong) => {
  try {
    const res = await axios.post(`${apiContractEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const findPaging = async (query: IQueryHopDong) => {
  try {
    const res = await axios.get(`${apiContractEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewHopDong> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

export const HopDongService = { createHopDong, findPaging };
