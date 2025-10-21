import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';
import { ICreateRole } from '@models/role';

const apiRoleEndpoint = 'role';

const getAllRoles = async () => {
  try {
    const res = await axios.get(`${apiRoleEndpoint}/get-all-role`, {
      baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL
    });

    // Response structure: { status: 1, data: ["admin","all2","string"], code: 200, message: "Ok" }
    if (res.data.status === 1 && res.data.data) {
      return Promise.resolve(res.data.data);
    }

    return Promise.reject(new Error('Không lấy được danh sách role.'));
  } catch (err) {
    processApiMsgError(err, 'Lỗi khi lấy danh sách role');
    return Promise.reject(err);
  }
};

const createRole = async (body: ICreateRole) => {
  try {
    const res = await axios.post(`${apiRoleEndpoint}/create-role`, body, {
      baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL
    });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tạo role mới');
    return Promise.reject(err);
  }
};

export const RoleService = { getAllRoles, createRole };
