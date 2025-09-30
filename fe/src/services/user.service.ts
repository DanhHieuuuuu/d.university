import { processApiMsgError } from '@utils/index';
import { IResponseList } from '@models/common/response.model';
import axios from '@utils/axios';
import { IQueryUser, IUserCreate, IUserView } from '@models/user/user.model';

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

const createUser = async (body: IUserCreate) => {
  try {
    const res = await axios.post('user/create-user', body, {
      baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL
    });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không tạo được user');
    return Promise.reject(err);
  }
};

const updateUser = async (body: { Id: number; Email?: string; NewPassword?: string }) => {
  try {
    const res = await axios.put('user/update-user', body, {
      baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL
    });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không cập nhật được user');
    return Promise.reject(err);
  }
};

export const UserService = { getAll, createUser, updateUser };
