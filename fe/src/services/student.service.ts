import { IQueryStudent, IViewStudent, ICreateStudent, IUpdateStudent } from '@models/student/student.model';
import { IResponseList } from '@models/common/response.model';
import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';

const apiStudentEndpoint = 'sinhvien';

const findPaging = async (query: IQueryStudent) => {
  try {
    const res = await axios.get(`${apiStudentEndpoint}/find`, {
      params: {
        ...query,
      },
    });

    const data: IResponseList<IViewStudent> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tải danh sách sinh viên.');
    return Promise.reject(err);
  }
};

const find = async (keyword: string) => {
  try {
    const res = await axios.get(`${apiStudentEndpoint}/get`, {
      params: {
        keyword: keyword
      }
    });
    // Trả về data (object API)
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tìm kiếm sinh viên.');
    throw err;
  }
};

const createSinhVien = async (body: ICreateStudent) => {
  try {
    const res = await axios.post(`${apiStudentEndpoint}/create`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể tạo sinh viên mới.');
    return Promise.reject(err);
  }
};

const update = async (body: Partial<IUpdateStudent>) => {
  try {
    const res = await axios.put(`${apiStudentEndpoint}/update`, body);
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật sinh viên.');
    return Promise.reject(err);
  }
};

const remove = async (idStudent: number) => {
  try {
    const res = await axios.delete(`${apiStudentEndpoint}/delete`, {
      data: { idStudent },
    });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Không thể xóa sinh viên.');
    return Promise.reject(err);
  }
};

export const StudentService = {
  findPaging,
  find,
  createSinhVien,
  update,
  remove,
};
