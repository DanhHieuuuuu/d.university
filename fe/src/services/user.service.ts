import { processApiMsgError } from '@utils/index';
import { IResponseList } from '@models/common/response.model';
import axios from '@utils/axios';
import { INhanSuData, IQueryUser, IUserCreate, IUserView } from '@models/user/user.model';

const apiNhanSuEndpoint = 'nhansu';

const getAll = async (query: IQueryUser) => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/get-all`, { params: query });

    console.log('Raw response:', res); // xem toàn bộ AxiosResponse
    console.log('res.data:', res.data); // xem đúng dữ liệu server trả về
    const data: IResponseList<IUserView> = res.data;

    // map hoTen từ hoDem + ten
    data.data.items = data.data.items.map((x: IUserView) => ({
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

const getNhanSuByMaNhanSu = async (maNhanSu: string): Promise<INhanSuData> => {
  try {
    const res = await axios.get(`${apiNhanSuEndpoint}/get`, { params: { keyword: maNhanSu } });

    // Kiểm tra cấu trúc response: { status: 1, data: INhanSuData, ... }
    if (res.data.status === 1 && res.data.data) {
      return Promise.resolve(res.data.data as INhanSuData);
    }

    // Nếu API trả về thành công nhưng không có dữ liệu nhân sự
    return Promise.reject(new Error('Không tìm thấy thông tin nhân sự.'));
  } catch (err) {
    // Xử lý lỗi kết nối hoặc lỗi backend
    processApiMsgError(err, 'Lỗi khi lấy thông tin nhân sự');
    return Promise.reject(err);
  }
};
const updateRolesToUser = async (userId: number, roleIds: number[]) => {
  try {
    const res = await axios.post(
      'user/update-roles-to-user',
      { NhanSuId: userId, RoleIds: roleIds },
      { baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL }
    );
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật nhóm quyền cho user');
    return Promise.reject(err);
  }
};
const getRolesOfUser = async (userId: number) => {
  try {
    const res = await axios.get(`user/${userId}`, {
      baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL
    });
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không lấy được quyền của user');
    return Promise.reject(err);
  }
};
const changeStatusUser = async (userId: number) => {
  try {
    const res = await axios.post(
      `user/${userId}/change-status`,
      {}, 
      {
        baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL
      }
    );
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không đổi trạng thái được');
    return Promise.reject(err);
  }
};

export const UserService = { getAll, createUser, updateUser, getNhanSuByMaNhanSu, updateRolesToUser, getRolesOfUser, changeStatusUser };
