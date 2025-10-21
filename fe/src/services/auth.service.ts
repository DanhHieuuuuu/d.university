import axios from 'axios';
import api from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IConnectToken, ILogin } from '@models/auth/auth.model';

const loginApi = async (body: ILogin) => {
  try {
    const params: IConnectToken = {
      grant_type: process.env.NEXT_PUBLIC_AUTH_GRANT_TYPE || '',
      maNhanSu: body.maNhanSu,
      password: body.password,
      scope: process.env.NEXT_PUBLIC_AUTH_SCOPE || '',
      client_id: process.env.NEXT_PUBLIC_AUTH_CLIENT_ID || '',
      client_secret: process.env.NEXT_PUBLIC_AUTH_CLIENT_SECRET || ''
    };

    const res = await axios.post(`nhansu/login`, params, { baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL });

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const refreshTokenApi = async (params: { token: string; refreshToken: string }) => {
  try {
    const res = await api.post(`nhansu/refresh-token`, params, { baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err);
    return Promise.reject(err);
  }
};

const logoutApi = async () => {
  try {
    const res = await api.get(`nhansu/logout`, { baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err);
    return Promise.reject(err);
  }
};

const changePasswordApi = async (body: { oldPassword: string; newPassword: string }) => {
  try {
    const { data } = await api.post('user/change-password', body, { baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL });
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err);
    return Promise.reject(err);
  }
};

const forgotPasswordApi = async (body: { maNhanSu: string }) => {
  try {
    const { data } = await axios.post(
      'user/reset-password',
      { MaNhanSu: body.maNhanSu },
      { baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL }
    );
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err);
    return Promise.reject(err);
  }
};

export const AuthService = {
  loginApi,
  refreshTokenApi,
  logoutApi,
  changePasswordApi,
  forgotPasswordApi
};
