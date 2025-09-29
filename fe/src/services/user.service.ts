import { processApiMsgError } from '@utils/index';
import { IResponseList } from '@models/common/response.model';
import axios from '@utils/axios';
import { IQueryUser, IUserView } from '@models/user/user.model';

const apiNhanSuEndpoint = 'nhansu';

const getAll = async (query: IQueryUser) => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/get-all`, { params: query });

    const data: IResponseList<IUserView> = res.data;

    // map hoTen từ hoDem + ten
    data.data.items = data.data.items.map((x: any) => ({
      ...x,
      hoTen: `${x.hoDem ?? ''} ${x.ten ?? ''}`.trim()
    }));

    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

export const UserService = { getAll };
