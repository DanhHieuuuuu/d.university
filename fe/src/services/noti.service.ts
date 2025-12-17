import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseList } from '@models/common/response.model';
import { IFilterNotification, IViewNotification } from '@models/notice/notification.model';

const apiNotificationEndpoint = 'noti';

const fetchNotification = async (query?: IFilterNotification) => {
  try {
    const res = await axios.get(`${apiNotificationEndpoint}/find`, {
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

const markRead = async (notiId: number) => {
  try {
    const res = await axios.put(`${apiNotificationEndpoint}/read/${notiId}`);

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const markAllAsRead = async () => {
  try {
    const res = await axios.put(`${apiNotificationEndpoint}/mark-all-as-read`);

    return Promise.resolve(res.data);
  } catch (err) {
    console.error(err);

    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

export const NotiService = {
  fetchNotification,
  markRead,
  markAllAsRead
};
