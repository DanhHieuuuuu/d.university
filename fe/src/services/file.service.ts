import { ICreateFile, IQueryFile, IFile, IUpdateFile } from '@models/file/file.model';
import { IResponseList } from '@models/common/response.model';
import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';
import { getItem } from '@utils/token-storage';

const apiFileEndpoint = 'file';
const s3Endpoint = `${process.env.NEXT_PUBLIC_AUTH_API_URL}s3-test`;

export interface IS3UploadResponse {
  files: Array<{
    fileName: string;
    url: string;
  }>;
}

export interface IS3FoldersResponse {
  folders: string[];
  count: number;
}

export interface IS3File {
  fileName: string;
  size: number;
  lastModified: string;
}

export interface IS3FilesResponse {
  files: IS3File[];
  count: number;
}

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

const getFileById = async (id: number): Promise<IFile> => {
  try {
    const res = await axios.get(`${apiFileEndpoint}/get`, {
      params: { id }
    });
    return Promise.resolve(res.data.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể lấy thông tin file');
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
    const res = await axios.delete(`${apiFileEndpoint}/delete`, {
      data: { Id: fileId }
    });
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

// ========== S3 Service Methods ==========

const uploadFile = async (file: File, folderPath?: string): Promise<IS3UploadResponse> => {
  try {
    const { accessToken } = getItem();
    const formData = new FormData();
    formData.append('file', file);

    if (folderPath) {
      formData.append('folderPath', folderPath);
    }

    const axiosInstance = (await import('axios')).default;
    const res = await axiosInstance.post(`${s3Endpoint}/upload`, formData, {
      headers: {
        Authorization: `Bearer ${accessToken}`,
        'Content-Type': 'multipart/form-data',
        accept: '*/*'
      }
    });

    return Promise.resolve(res.data.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tải file lên');
    return Promise.reject(err);
  }
};

const uploadMultipleFiles = async (files: File[], folderPath?: string): Promise<IS3UploadResponse> => {
  try {
    const { accessToken } = getItem();
    const formData = new FormData();

    files.forEach((file) => {
      formData.append('files', file);
    });

    if (folderPath) {
      formData.append('folderPath', folderPath);
    }

    const axiosInstance = (await import('axios')).default;
    const res = await axiosInstance.post(`${s3Endpoint}/upload-multiple`, formData, {
      headers: {
        Authorization: `Bearer ${accessToken}`,
        'Content-Type': 'multipart/form-data',
        accept: '*/*'
      }
    });

    return Promise.resolve(res.data.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tải nhiều file lên');
    return Promise.reject(err);
  }
};

const downloadFile = async (fileName: string, bustCache: boolean = false): Promise<Blob> => {
  try {
    const { accessToken } = getItem();

    const params: any = { fileName };
    if (bustCache) {
      params.t = Date.now();
    }

    const axiosInstance = (await import('axios')).default;
    const res = await axiosInstance.get(`${s3Endpoint}/download`, {
      params,
      headers: {
        Authorization: `Bearer ${accessToken}`,
        accept: '*/*'
      },
      responseType: 'blob'
    });

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tải file xuống');
    return Promise.reject(err);
  }
};

const deleteS3File = async (fileName: string): Promise<void> => {
  try {
    const { accessToken } = getItem();

    const axiosInstance = (await import('axios')).default;
    await axiosInstance.delete(`${s3Endpoint}/delete`, {
      params: { fileName },
      headers: {
        Authorization: `Bearer ${accessToken}`,
        accept: '*/*'
      }
    });

    return Promise.resolve();
  } catch (err) {
    processApiMsgError(err, 'Không thể xóa file');
    return Promise.reject(err);
  }
};

const listFiles = async (prefix?: string): Promise<IS3FilesResponse> => {
  try {
    const { accessToken } = getItem();
    const params = prefix ? { prefix } : {};

    const axiosInstance = (await import('axios')).default;
    const res = await axios.get(`${apiFileEndpoint}/list-files`, {
      params,
      headers: {
        Authorization: `Bearer ${accessToken}`,
        accept: '*/*'
      }
    });

    return Promise.resolve(res.data.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể lấy danh sách file');
    return Promise.reject(err);
  }
};

const listFolders = async (prefix?: string): Promise<IS3FoldersResponse> => {
  try {
    const { accessToken } = getItem();
    const params = prefix ? { prefix } : {};

    const axiosInstance = (await import('axios')).default;
    const res = await axiosInstance.get(`${s3Endpoint}/folders`, {
      params,
      headers: {
        Authorization: `Bearer ${accessToken}`,
        accept: '*/*'
      }
    });

    return Promise.resolve(res.data.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể lấy danh sách thư mục');
    return Promise.reject(err);
  }
};

const testConnection = async (): Promise<boolean> => {
  try {
    const { accessToken } = getItem();

    const axiosInstance = (await import('axios')).default;
    const res = await axiosInstance.get(`${s3Endpoint}/test-connection`, {
      headers: {
        Authorization: `Bearer ${accessToken}`,
        accept: '*/*'
      }
    });

    return Promise.resolve(res.data.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể kiểm tra kết nối S3');
    return Promise.reject(err);
  }
};

// ========== Image Service Methods ==========

const updateUserImage = async (maNhanSu: string, file: File): Promise<Blob> => {
  try {
    const { accessToken } = getItem();

    const formData = new FormData();
    formData.append('File', file);

    const axiosInstance = (await import('axios')).default;
    const res = await axiosInstance.put(`${process.env.NEXT_PUBLIC_AUTH_API_URL}user/update-image-user`, formData, {
      params: { MaNhanSu: maNhanSu },
      headers: {
        Authorization: `Bearer ${accessToken}`,
        'Content-Type': 'multipart/form-data',
        accept: '*/*'
      },
      responseType: 'blob'
    });

    return Promise.resolve(res.data);
  } catch (err) {
    console.error('Error updating user image:', err);
    processApiMsgError(err, 'Không thể cập nhật ảnh đại diện');
    return Promise.reject(err);
  }
};

export const FileService = {
  findPaging,
  findById,
  getFileById,
  create,
  update,
  deleteFile,
  getFile,
  // S3 methods
  uploadFile,
  uploadMultipleFiles,
  downloadFile,
  deleteS3File,
  listFiles,
  listFolders,
  testConnection,
  // Image methods
  updateUserImage
};
