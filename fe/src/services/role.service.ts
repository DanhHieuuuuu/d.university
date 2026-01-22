import axios from 'axios';
import { attachAuthInterceptor } from '@utils/axios-interceptor';

import { processApiMsgError } from '@utils/index';
import { IResponseItem, IResponseList } from '@models/common/response.model';
import { IPermissionTree } from '@models/permission';
import { ICreateRole, IQueryRole, IRole, IUpdateRole, IUpdateRolePermission, IUpdateRoleStatus } from '@models/role';

/**
 * Cấu hình riêng axios cho role service
 */

const authApi = axios.create({
  baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL
});

attachAuthInterceptor(authApi);

const apiRoleEndpoint = 'role';

const findPaging = async (query: IQueryRole) => {
  try {
    const res = await authApi.get(`${apiRoleEndpoint}/find`, {
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
    const res = await authApi.get(`${apiRoleEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const create = async (body: ICreateRole) => {
  try {
    const res = await authApi.post(`${apiRoleEndpoint}/create-role`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tạo role mới');
    return Promise.reject(err);
  }
};

const update = async (body: IUpdateRole) => {
  try {
    const res = await authApi.put(`${apiRoleEndpoint}/${body.id}`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật role');
    return Promise.reject(err);
  }
};

const updateStatus = async (body: IUpdateRoleStatus) => {
  try {
    const res = await authApi.put(`${apiRoleEndpoint}/${body.id}/change-status`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật trạng thái của role.');
    return Promise.reject(err);
  }
}

const deleteRole = async (roleId: number) => {
  try {
    const res = await authApi.delete(`${apiRoleEndpoint}/${roleId}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể xóa role');
    return Promise.reject(err);
  }
};

const updatePermission = async (body: IUpdateRolePermission) => {
  try {
    const res = await authApi.post(`${apiRoleEndpoint}/${body.roleId}/permissions`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật permission cho role');
    return Promise.reject(err);
  }
};

const getPermissionTree = async () => {
  try {
    const res = await authApi.get(`${apiRoleEndpoint}/tree-permissions`);
    const data: IResponseItem<IPermissionTree[]> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật role');
    return Promise.reject(err);
  }
};

const getMyPermission = async () => {
  try {
    const res = await authApi.get(`${apiRoleEndpoint}/my-permissions`);
    const data: IResponseItem<string[]> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, 'Không lấy được my-permisison');
    return Promise.reject(err);
  }
};

export const RoleService = {
  findPaging,
  findById,
  create,
  update,
  updateStatus,
  deleteRole,
  updatePermission,
  getPermissionTree,
  getMyPermission
};
