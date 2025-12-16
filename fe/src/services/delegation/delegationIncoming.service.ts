import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';
import {
  ICreateDoanVao,
  IDetailDelegationIncoming,
  ILogStatus,
  IReceptionTime,
  IUpdateDoanVao,
  IViewGuestGroup
} from '@models/delegation/delegation.model';
import { IResponseItem, IResponseList } from '@models/common/response.model';
import { IViewPhongBan } from '@models/danh-muc/phong-ban.model';

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
    const res = await axios.post(`${apiDelegationEndpoint}/create`,formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};
const updateDoanVao = async (formData: FormData) => {
  try {
    const res = await axios.put(`${apiDelegationEndpoint}/update`, formData);
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
const getByIdDetailDelegation = async (id: number) => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-staff-by-id?Id=${id}`);

    const data: IResponseItem<IDetailDelegationIncoming> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const getByIdReceptionTime = async (id: number) => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-reception-time-by-id?Id=${id}`);

    const data: IResponseItem<IReceptionTime> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};
const downloadTemplateExcel = async () => {
  try {
    const res = await axios.get(
      `${apiDelegationEndpoint}/download-excel`,
      {
        responseType: 'blob',
      }
    );
    return Promise.resolve(res);
  } catch (err) {
    processApiMsgError(err, 'Không tải được file Excel mẫu');
    return Promise.reject(err);
  }
};
const getLogStatus = async () => {
  try {
    const res = await axios.get(`${apiDelegationEndpoint}/get-log-status`);

    const data: IResponseList<ILogStatus> = res.data;
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
  getLogStatus
};
