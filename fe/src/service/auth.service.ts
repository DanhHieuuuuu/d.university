import axios from 'axios';
import { processApiMsgError } from '@utils/index';
import { IConnectToken, ILogin, IUser } from '@models/auth/auth.model';

const login = async (body: ILogin) => {
  try {
    const params: IConnectToken = {
      grant_type: process.env.NEXT_PUBLIC_AUTH_GRANT_TYPE || '',
      username: body.username,
      password: body.password,
      scope: process.env.NEXT_PUBLIC_AUTH_SCOPE || '',
      client_id: process.env.NEXT_PUBLIC_AUTH_CLIENT_ID || '',
      client_secret: process.env.NEXT_PUBLIC_AUTH_CLIENT_SECRET || ''
    };

    // const res = await axios.post(`connect/token`, params, {
    //   headers: {
    //     'Content-Type': 'application/x-www-form-urlencoded'
    //   },
    //   baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL
    // });

    const draft_data: IUser = {
      id: 1,
      ho: 'Phó Đức',
      ten: 'Thành',
      fullName: 'Phó Đức Thành',
      email: 'phoducthanh@gmail.com',
      role: 'admin',
      username: 'admin',
      position: 'Giảng viên'
    };

    const res = {
      status: 200,
      messsage: 'Ok',
      data: {
        user: draft_data,
        access_token: 'abcxyz',
        refresh_token: 'zyxcba'
      }
    };

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const changePassword = async (body: { currentPassword: string; newPassword: string; confirmPassword: string }) => {
  try {
    const { data } = await axios.post('auth/change-password', body);
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

export const AuthService = {
  login,
  changePassword
};
