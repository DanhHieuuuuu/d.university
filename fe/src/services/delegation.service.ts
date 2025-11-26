import { ICreateHopDongNs, ICreateNhanSu, IQueryNhanSu, IViewNhanSu } from '@models/nhansu/nhansu.model';
import { IResponseList } from '@models/common/response.model';
import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';

const apiNhanSuEndpoint = 'delegation-incoming';

const paging = async (query: any) => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/paging`, {
      params: {
        ...query
      }
    });

    const data = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
export const NhanSuService = { paging };
