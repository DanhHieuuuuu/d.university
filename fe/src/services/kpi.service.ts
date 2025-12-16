import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseList, IResponseItem } from '@models/common/response.model';
import { ICreateKpiCaNhan, IQueryKpiCaNhan, IUpdateKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import { ICreateKpiDonVi, IQueryKpiDonVi, IUpdateKpiDonVi, IViewKpiDonVi } from '@models/kpi/kpi-don-vi.model';
import { ICreateKpiRole, IQueryKpiRole, IUpdateKpiRole, IViewKpiRole } from '@models/kpi/kpi-role.model';

const apiDanhMucEndpoint = 'kpi';

const apiKpiDonViEndpoint = `${apiDanhMucEndpoint}/kpi-donvi`;
const apiKpiCaNhanEndpoint = `${apiDanhMucEndpoint}/kpi-canhan`;
const apiKpiRoleEndpoint = `${apiDanhMucEndpoint}/kpi-role`;

//Kpi Cá nhân
const getListKpiCaNhan = async (query?: IQueryKpiCaNhan) => {
  try {
    const res = await axios.get(`${apiKpiCaNhanEndpoint}/find`, {
      params: {
        ...query
      }
    });
    const data: IResponseList<IViewKpiCaNhan> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createKpiCaNhan = async (body: ICreateKpiCaNhan) => {
  try {
    const res = await axios.post(`${apiKpiCaNhanEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateKpiCaNhan = async (body: IUpdateKpiCaNhan) => {
  try {
    const res = await axios.put(`${apiKpiCaNhanEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteKpiCaNhan = async (id: number) => {
  try {
    const res = await axios.delete(`${apiKpiCaNhanEndpoint}/delete` , {params: {id}});
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

//Kpi Don Vi
const getListKpiDonVi = async (query?: IQueryKpiDonVi) => {
  try {
    const res = await axios.get(`${apiKpiDonViEndpoint}/find`, {
      params: {
        ...query
      }
    });
    const data: IResponseList<IViewKpiDonVi> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createKpiDonVi = async (body: ICreateKpiDonVi) => {
  try {
    const res = await axios.post(`${apiKpiDonViEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateKpiDonVi = async (body: IUpdateKpiDonVi) => {
  try {
    const res = await axios.put(`${apiKpiDonViEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteKpiDonVi = async (id: number ) => {
  try {
    const res = await axios.delete(`${apiKpiDonViEndpoint}/delete` , {params: {id}} );
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

// KPI Role
const getListKpiRole = async (query?: IQueryKpiRole) => {
  try {
    const res = await axios.get(`${apiKpiRoleEndpoint}/find`, {
      params: {
        ...query
      }
    });
    const data: IResponseList<IViewKpiRole> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createKpiRole = async (body: ICreateKpiRole) => {
  try {
    const res = await axios.post(`${apiKpiRoleEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateKpiRole = async (body: IUpdateKpiRole) => {
  try {
    const res = await axios.put(`${apiKpiRoleEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const deleteKpiRole = async (ids: number[]) => {
  const res = await axios.delete(`${apiKpiRoleEndpoint}/delete`, {
    data: {
      ids
    }
  });
  return res.data;
};
export const KpiService = {
  getListKpiCaNhan,
  createKpiCaNhan,
  updateKpiCaNhan,
  deleteKpiCaNhan,
  getListKpiDonVi,
  createKpiDonVi,
  updateKpiDonVi,
  deleteKpiDonVi,
  getListKpiRole,
  createKpiRole,
  updateKpiRole,
  deleteKpiRole,
};
