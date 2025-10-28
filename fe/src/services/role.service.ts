import axios from 'axios';
import { TOKEN } from '@/constants/base.const';
import { processApiMsgError } from '@utils/index';
import { ICreateRole, IQueryRole, IRole, IUpdateRole, IUpdateRolePermission } from '@models/role';
import { IResponseItem, IResponseList } from '@models/common/response.model';
import { IPermissionTree } from '@models/permission';

/**
 * Cấu hình riêng axios cho role service
 */

const _axios = axios.create({
  baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});

_axios.interceptors.request.use(
  (config) => {
    // Lấy token từ  sessionStorage trước, nếu không có thì lấy từ localStorage
    const token = sessionStorage.getItem(TOKEN) || localStorage.getItem(TOKEN);

    if (token) {
      config.headers = config.headers || {};
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => Promise.reject(error)
);

const apiRoleEndpoint = 'role';

const findPaging = async (query: IQueryRole) => {
  try {
    const res = await _axios.get(`${apiRoleEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IRole> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const findById = async (id: number) => {
  try {
    const res = await _axios.get(`${apiRoleEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const create = async (body: ICreateRole) => {
  try {
    const res = await _axios.post(`${apiRoleEndpoint}/create-role`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tạo role mới');
    return Promise.reject(err);
  }
};

const update = async (body: IUpdateRole) => {
  try {
    const res = await _axios.put(`${apiRoleEndpoint}/${body.id}`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật role');
    return Promise.reject(err);
  }
};

const updatePermission = async (body: IUpdateRolePermission) => {
  try {
    const res = await _axios.post(`${apiRoleEndpoint}/${body.roleId}/permissions`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật permission cho role');
    return Promise.reject(err);
  }
};

const getPermissionTree = async () => {
  try {
    const res = await _axios.get(`${apiRoleEndpoint}/tree-permissions`);
    const data: IResponseItem<IPermissionTree[]> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật role');
    return Promise.reject(err);
  }
};

const getMyPermission = async () => {
  try {
    const res = await _axios.get(`${apiRoleEndpoint}/my-permissions`);
    const data: IResponseItem<string[]> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật role');
    return Promise.reject(err);
  }
};

export const RoleService = {
  findPaging,
  findById,
  create,
  update,
  updatePermission,
  getPermissionTree,
  getMyPermission
};
