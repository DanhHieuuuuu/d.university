import { ICreateFile, IQueryFile, IFile, IUpdateFile } from '@models/file/file.model';
import { IResponseList } from '@models/common/response.model';
import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';

const apiFileEndpoint = 'file';

const findPaging = async (query: IQueryFile) => {
  try {
    const res = await axios.get(`${apiFileEndpoint}/find`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IFile> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const findById = async (id: number) => {
  try {
    const res = await axios.get(`${apiFileEndpoint}/${id}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const create = async (formData: FormData) => {
  try {
    const res = await axios.post(`${apiFileEndpoint}/create`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tạo file mới');
    return Promise.reject(err);
  }
};

const update = async (formData: FormData) => {
  try {
    const res = await axios.put(`${apiFileEndpoint}/update`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật file');
    return Promise.reject(err);
  }
};

const deleteFile = async (fileId: number) => {
  try {
    const res = await axios.delete(`${apiFileEndpoint}/${fileId}`);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể xóa file');
    return Promise.reject(err);
  }
};

const getFile = async (fileName: string) => {
  try {
    const res = await axios.get(fileName, { responseType: 'blob' });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tải file');
    return Promise.reject(err);
  }
};

export const FileService = { findPaging, findById, create, update, deleteFile, getFile  };