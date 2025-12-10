import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';
import { ICreateDoanVao, IUpdateDoanVao, IViewGuestGroup } from '@models/delegation/delegation.model';
import { IResponseList } from '@models/common/response.model';
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
const createDoanVao = async (body: ICreateDoanVao) => {
  try {
    const res = await axios.post(`${apiDelegationEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};
const updateDoanVao = async (body: IUpdateDoanVao) => {
  try {
    const res = await axios.put(`${apiDelegationEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
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
export const DelegationIncomingService = { paging , getListPhongBan, getListStatus,updateDoanVao,deleteDoanVao,createDoanVao};