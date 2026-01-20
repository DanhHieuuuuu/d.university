import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseItem, IResponseList } from '@models/common/response.model';
import { IDetailQuyetDinh, IQueryQuyetDinh, IViewQuyetDinh } from '@models/nhansu/quyetdinh.model';

const apiDecisionEndpoint = 'decision';

const findPaging = async (query: IQueryQuyetDinh) => {
  try {
    const res = await axios.get(`${apiDecisionEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewQuyetDinh> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const findById = async (id: number) => {
  try {
    const res = await axios.get(`${apiDecisionEndpoint}/${id}`);

    const data: IResponseItem<IDetailQuyetDinh> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

export const NsDecisionService = { findPaging, findById };
