import { IQueryStudent, IViewStudent, ICreateStudent, IUpdateStudent } from '@models/student/student.model';
import { ISinhVienLogin, ISinhVienConnectToken } from '@models/auth/sinhvien.model';
import { IResponseList } from '@models/common/response.model';
import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';
import axiosBase from 'axios';

const apiStudentEndpoint = 'sinhvien';

const findPaging = async (query: IQueryStudent) => {
  try {
    const res = await axios.get(`${apiStudentEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewStudent> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tải danh sách sinh viên.');
    return Promise.reject(err);
  }
};

// const find = async (keyword: string) => {
//   try {
//     const res = await axios.get(`${apiStudentEndpoint}/get-all`, {
//       params: {
//         keyword: keyword
//       }
//     });
//     // Trả về data (object API)
//     return res.data;
//   } catch (err) {
//     processApiMsgError(err, 'Không thể tìm kiếm sinh viên.');
//     throw err;
//   }
// };

const createSinhVien = async (body: ICreateStudent) => {
  try {
    const res = await axios.post(`${apiStudentEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tạo sinh viên mới.');
    return Promise.reject(err);
  }
};

const update = async (body: Partial<IUpdateStudent>) => {
  try {
    const res = await axios.put(`${apiStudentEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật sinh viên.');
    return Promise.reject(err);
  }
};

const remove = async (mssv: string) => {
  try {
    const res = await axios.delete(`${apiStudentEndpoint}/delete`, {
      data: { mssv }
    });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể xóa sinh viên.');
    return Promise.reject(err);
  }
};

// Student Authentication APIs
const sinhVienLoginApi = async (body: ISinhVienLogin) => {
  try {
    const params = {
      mssv: body.mssv,
      password: body.password
    };

    const res = await axiosBase.post(`sinhvien/login`, params, { baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL });

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const sinhVienRefreshTokenApi = async (params: { token: string; refreshToken: string }) => {
  try {
    const res = await axios.post(`sinhvien/refresh-token`, params, { baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err);
    return Promise.reject(err);
  }
};

const sinhVienLogoutApi = async () => {
  try {
    const res = await axios.get(`sinhvien/logout`, { baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err);
    return Promise.reject(err);
  }
};

export const StudentService = {
  findPaging,
  // find,
  createSinhVien,
  update,
  remove,
  sinhVienLoginApi,
  sinhVienRefreshTokenApi,
  sinhVienLogoutApi
};
