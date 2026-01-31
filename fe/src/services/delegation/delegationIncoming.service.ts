import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';
import {
  BaoCaoDoanVao,
  ICreateDepartment,
  ICreateDoanVao,
  ICreatePrepare,
  ICreateReceptionTime,
  ICreateReceptionTimeList,
  ICreateSupporter,
  IDateOption,
  IDepartmentSupport,
  IDetailDelegationIncoming,
  ILogStatus,
  IReceptionTime,
  IStatistical,
  IUpdateDepartmentSupport,
  IUpdateDetailDelegationRequest,
  IUpdateDoanVao,
  IUpdatePrepare,
  IUpdateReceptionTime,
  IUpdateReceptionTimes,
  IUpdateStatus,
  IViewGuestGroup
} from '@models/delegation/delegation.model';
import { IResponseArray, IResponseItem, IResponseList } from '@models/common/response.model';
import { IViewPhongBan } from '@models/danh-muc/phong-ban.model';
import { IViewNhanSu } from '@models/nhansu/nhansu.model';

const apiDelegationEndpoint = 'delegation-incoming';

const paging = async (query: any) => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/paging`, {
      params: {
        ...query
      }
    });

    const data = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const getListPhongBan = async () => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-phongban`);

    const data: IResponseList<IViewPhongBan> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const getListNhanSu = async () => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-nhansu`);

    const data: IResponseList<IViewNhanSu> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const getListStatus = async () => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-status`);

    const data: IResponseList<IViewGuestGroup> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const createDoanVao = async (formData: FormData) => {
  try {
    const res = await axios.post(`${apiDelegationEndpoint}/create`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    throw err;
  }
};
const updateDoanVao = async (formData: FormData) => {
  try {
    const res = await axios.put(`${apiDelegationEndpoint}/update`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    throw err;
  }
};
const deleteDoanVao = async (id: number) => {
  try {
    const res = await axios.delete(`${apiDelegationEndpoint}/delete/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};
const getByIdGuestGroup = async (id: number) => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-by-id?Id=${id}`);

    const data: IResponseItem<IViewGuestGroup> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const getByIdDetailDelegation = async (delegationIncomingId: number) => {
  try {
    const res = await axios.get(
      `${apiDelegationEndpoint}/get-staff-by-id?DelegationIncomingId=${delegationIncomingId}`
    );

    const data: IResponseItem<IDetailDelegationIncoming> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const getByIdReceptionTime = async (delegationIncomingId: number) => {
  try {
    const res = await axios.get(
      `${apiDelegationEndpoint}/get-reception-time-by-id?DelegationIncomingId=${delegationIncomingId}`
    );
    const data: IResponseItem<IReceptionTime[]> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    throw err;
  }
};
const downloadTemplateExcel = async () => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/download-excel`, {
      responseType: 'blob'
    });
    return Promise.resolve(res);
  } catch (err) {
    processApiMsgError(err, 'Không tải được file Excel mẫu');
    return Promise.reject(err);
  }
};
const getLogStatus = async (query: any) => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-log-status`, {
      params: {
        ...query
      }
    });

    const data = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const getLogReceptionTime = async (query: any) => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-log-reception-time`, {
      params: {
        ...query
      }
    });

    const data = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const updateReceptionTimes = async (body: IUpdateReceptionTimes) => {
  try {
    const res = await axios.put(`${apiDelegationEndpoint}/update-reception-time`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};
const updateDetailDelegation = async (body: IUpdateDetailDelegationRequest) => {
  try {
    const res = await axios.put(`${apiDelegationEndpoint}/update-detail-delegation`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};
const createReceptionTime = async (body: ICreateReceptionTimeList) => {
  try {
    const res = await axios.post(`${apiDelegationEndpoint}/create-reception-time`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const pagingSupporter = async (query: any) => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/paging-supporter`, {
      params: {
        ...query
      }
    });

    const data = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const pagingDepartmentSupport = async (query: any) => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/paging-department-support`, {
      params: {
        ...query
      }
    });

    const data = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const createSupporter = async (body: ICreateSupporter) => {
  try {
    const res = await axios.post(`${apiDelegationEndpoint}/create-supporter`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};
const createDepartment = async (body: ICreateDepartment) => {
  try {
    const res = await axios.post(`${apiDelegationEndpoint}/create-department-support`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};
const getListDelegationIncoming = async () => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-delegation-incoming`);

    const data: IResponseList<IViewGuestGroup> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const updateStatus = async (body: IUpdateStatus) => {
  try {
    const res = await axios.post(`${apiDelegationEndpoint}/next-status`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateDepartmentSupport = async (body: IUpdateDepartmentSupport) => {
  try {
    const res = await axios.put(`${apiDelegationEndpoint}/update-department-support`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};
const getByIdDepartmentSupport = async (departmentSupportId: number) => {
  try {
    const res = await axios.get(
      `${apiDelegationEndpoint}/get-id-department-support?DepartmentSupportId=${departmentSupportId}`
    );

    const data: IResponseItem<IDepartmentSupport> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const createPrepare = async (body: ICreatePrepare) => {
  try {
    const res = await axios.post(`${apiDelegationEndpoint}/create-prepare`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};
const updatePrepare = async (body: IUpdatePrepare) => {
  try {
    const res = await axios.put(`${apiDelegationEndpoint}/update-prepare`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const baoCaoDoanVao = async (payload: BaoCaoDoanVao) => {
  try {
    const res = await axios.post(`${apiDelegationEndpoint}/bao_cao_doan_vao`,payload, {
      responseType: 'blob'
    });
    return Promise.resolve(res);
  } catch (err) {
    processApiMsgError(err, 'Không tải được báo cáo');
    return Promise.reject(err);
  }
};

const getStatistical = async () => {
  try {
    const res = await axios.get(
      `${apiDelegationEndpoint}/statistical`
    );
    const data: IResponseItem<IStatistical> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const getListCreatedDate = async () => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-created-dates`);

    const data: IResponseArray<IDateOption> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
export const DelegationIncomingService = {
  paging,
  getListPhongBan,
  getListStatus,
  updateDoanVao,
  deleteDoanVao,
  createDoanVao,
  getByIdGuestGroup,
  getByIdDetailDelegation,
  getByIdReceptionTime,
  downloadTemplateExcel,
  getLogStatus,
  getListNhanSu,
  updateReceptionTimes,
  createReceptionTime,
  pagingSupporter,
  createSupporter,
  pagingDepartmentSupport,
  createDepartment,
  getListDelegationIncoming,
  updateStatus,
  getLogReceptionTime,
  updateDepartmentSupport,
  getByIdDepartmentSupport,
  createPrepare,
  updatePrepare,
  baoCaoDoanVao,
  getStatistical,
  getListCreatedDate,
  updateDetailDelegation
};
