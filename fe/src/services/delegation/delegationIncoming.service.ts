import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';

const apiNhanSuEndpoint = 'delegation-incoming';

const paging = async (query: any) => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/find`, {
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

export const DelegationIncomingService = { paging };
