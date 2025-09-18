import axios from 'axios';
import { processApiMsgError } from '@utils/index';
import { IConnectToken, ILogin, IUser } from '@models/auth/auth.model';

const login = async (body: ILogin) => {
  try {
    const params: IConnectToken = {
      grant_type: process.env.NEXT_PUBLIC_AUTH_GRANT_TYPE || '',
      maNhanSu: body.maNhanSu,
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

    const res = await axios.post(`${process.env.NEXT_PUBLIC_LOGIN_API}`, body);

    const { token, refreshToken, maNhanSu, hoDem, ten, email, hienTaiChucVu } = res.data.data;
    const user: IUser = {
      // id: 1, 
      maNhanSu: maNhanSu,
      ho: hoDem,
      ten: ten,
      fullName: `${hoDem} ${ten}`,
      email: email,
      role: 'admin', 
      position: hienTaiChucVu 
    };

    return Promise.resolve({
      user: user,
      access_token: token,
      refresh_token: refreshToken
    });
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
