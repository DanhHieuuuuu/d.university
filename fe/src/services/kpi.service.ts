import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseList, IResponseItem } from '@models/common/response.model';
import { ICreateKpiCaNhan, IQueryKpiCaNhan, IUpdateKpiCaNhan, IUpdateKpiCaNhanThucTeList, IUpdateTrangThaiKpiCaNhan, IViewKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import { ICreateKpiDonVi, IQueryKpiDonVi, IUpdateKpiDonVi, IUpdateKpiDonViThucTeList, IUpdateTrangThaiKpiDonVi, IViewKpiDonVi } from '@models/kpi/kpi-don-vi.model';
import { ICreateKpiRole, IQueryKpiRole, IUpdateKpiRole, IViewKpiRole } from '@models/kpi/kpi-role.model';
import { ICreateKpiTruong, IQueryKpiTruong, IUpdateKpiTruong, IUpdateKpiTruongThucTeList, IUpdateTrangThaiKpiTruong, IViewKpiTruong } from '@models/kpi/kpi-truong.model';

const apiDanhMucEndpoint = 'kpi';
const apiKpiTruongEndpoint = `${apiDanhMucEndpoint}/kpi-truong`;
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

const getListKpiCaNhanKeKhai = async (query?: IQueryKpiCaNhan) => {
  try {
    const res = await axios.get(
      `${apiKpiCaNhanEndpoint}/find-ke-khai`,
      { params: { ...query } }
    );
    return res.data;
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createKpiCaNhan = async (body: ICreateKpiCaNhan) => {
  const res = await axios.post(`${apiKpiCaNhanEndpoint}/create`, body);
  if (res.data?.code != 200) {
    return Promise.reject({
      message: res.data?.message || 'Có lỗi xảy ra'
    });
  }
  return Promise.resolve(res.data);
};

const updateKpiCaNhan = async (body: IUpdateKpiCaNhan) => {
  const res = await axios.put(`${apiKpiCaNhanEndpoint}/update`, body);
  if (res.data?.code != 200) {
    return Promise.reject({
      message: res.data?.message || 'Có lỗi xảy ra'
    });
  }
  return Promise.resolve(res.data);
};

const deleteKpiCaNhan = async (id: number) => {
  try {
    const res = await axios.put(`${apiKpiCaNhanEndpoint}/delete`, { id });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

export const getListTrangThaiKpiCaNhan = async () => {
  try {
    const res = await axios.get(
      `${apiKpiCaNhanEndpoint}/trang-thai`
    );

    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }

    return res.data.data;
  } catch (err) {
    return Promise.reject(err);
  }
};

const updateTrangThaiKpiCaNhan = async (
  body: IUpdateTrangThaiKpiCaNhan
) => {
  try {
    const res = await axios.put(
      `${apiKpiCaNhanEndpoint}/update-trang-thai`,
      body
    );

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Cập nhật trạng thái thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi cập nhật trạng thái');
    return Promise.reject(err);
  }
};

const updateKetQuaThucTeKpiCaNhan = async (
  body: IUpdateKpiCaNhanThucTeList
) => {
  try {
    const res = await axios.put(
      `${apiKpiCaNhanEndpoint}/update-ket-qua-thuc-te`,
      body
    );

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Cập nhật kết quả thực tế thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi cập nhật kết quả thực tế');
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
  const res = await axios.post(`${apiKpiDonViEndpoint}/create`, body);
  if (res.data?.code != 200) {
    return Promise.reject({
      message: res.data?.message || 'Có lỗi xảy ra'
    });
  }
  return Promise.resolve(res.data);
};

const updateKpiDonVi = async (body: IUpdateKpiDonVi) => {
  const res = await axios.put(`${apiKpiDonViEndpoint}/update`, body);
  if (res.data?.code != 200) {
    return Promise.reject({
      message: res.data?.message || 'Có lỗi xảy ra'
    });
  }
  return Promise.resolve(res.data);
};

const deleteKpiDonVi = async (id: number) => {
  try {
    const res = await axios.put(`${apiKpiDonViEndpoint}/delete`, { id });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateTrangThaiKpiDonVi = async (
  body: IUpdateTrangThaiKpiDonVi
) => {
  try {
    const res = await axios.put(
      `${apiKpiDonViEndpoint}/update-trang-thai`,
      body
    );

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Cập nhật trạng thái thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi cập nhật trạng thái');
    return Promise.reject(err);
  }
};

const updateKetQuaThucTeKpiDonVi = async (
  body: IUpdateKpiDonViThucTeList
) => {
  try {
    const res = await axios.put(
      `${apiKpiDonViEndpoint}/update-ket-qua-thuc-te`,
      body
    );

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Cập nhật kết quả thực tế thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi cập nhật kết quả thực tế');
    return Promise.reject(err);
  }
};

export const getListNamHocKpiDonVi = async () => {
  try {
    const res = await axios.get(
      `${apiKpiDonViEndpoint}/list-nam-hoc`
    );

    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }

    return res.data.data;
  } catch (err) {
    return Promise.reject(err);
  }
};

export const getListTrangThaiKpiDonVi = async () => {
  try {
    const res = await axios.get(
      `${apiKpiDonViEndpoint}/trang-thai`
    );

    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }

    return res.data.data;
  } catch (err) {
    return Promise.reject(err);
  }
};

//KPI TRƯỜNG'
const getListKpiTruong = async (query?: IQueryKpiTruong) => {
  try {
    const res = await axios.get(`${apiKpiTruongEndpoint}/find`, {
      params: {
        ...query
      }
    });
    const data: IResponseList<IViewKpiTruong> = res.data;
    return Promise.resolve(data);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

const createKpiTruong = async (body: ICreateKpiTruong) => {
  const res = await axios.post(`${apiKpiTruongEndpoint}/create`, body);
  if (res.data?.code != 200) {
    return Promise.reject({
      message: res.data?.message || 'Có lỗi xảy ra'
    });
  }
  return Promise.resolve(res.data);
};

const updateKpiTruong = async (body: IUpdateKpiTruong) => {
  const res = await axios.put(`${apiKpiTruongEndpoint}/update`, body);
  if (res.data?.code != 200) {
    return Promise.reject({
      message: res.data?.message || 'Có lỗi xảy ra'
    });
  }
  return Promise.resolve(res.data);
};

const deleteKpiTruong = async (id: number) => {
  try {
    const res = await axios.put(`${apiKpiTruongEndpoint}/delete`, { id });
    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có sự cố xảy ra. Vui lòng thử lại sau.');
    return Promise.reject(err);
  }
};

const updateTrangThaiKpiTruong = async (
  body: IUpdateTrangThaiKpiTruong
) => {
  try {
    const res = await axios.put(
      `${apiKpiTruongEndpoint}/update-trang-thai`,
      body
    );

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Cập nhật trạng thái thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi cập nhật trạng thái');
    return Promise.reject(err);
  }
};

const updateKetQuaThucTeKpiTruong = async (
  body: IUpdateKpiTruongThucTeList
) => {
  try {
    const res = await axios.put(
      `${apiKpiTruongEndpoint}/update-ket-qua-thuc-te`,
      body
    );

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Cập nhật kết quả thực tế thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi cập nhật kết quả thực tế');
    return Promise.reject(err);
  }
};

export const getListNamHocKpiTruong = async () => {
  try {
    const res = await axios.get(
      `${apiKpiTruongEndpoint}/list-nam-hoc`
    );

    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }

    return res.data.data;
  } catch (err) {
    return Promise.reject(err);
  }
};

export const getListTrangThaiKpiTruong = async () => {
  try {
    const res = await axios.get(
      `${apiKpiTruongEndpoint}/trang-thai`
    );

    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }

    return res.data.data;
  } catch (err) {
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
  const res = await axios.post(`${apiKpiRoleEndpoint}/create`, body);
  if (res.data?.code != 200) {
    return Promise.reject({
      message: res.data?.message || 'Có lỗi xảy ra'
    });
  }
  return res.data;
};

const updateKpiRole = async (body: IUpdateKpiRole) => {
  const res = await axios.put(`${apiKpiRoleEndpoint}/update`, body);
  if (res.data?.code != 200) {
    return Promise.reject({
      message: res.data?.message || 'Có lỗi xảy ra'
    });
  }
  return Promise.resolve(res.data);
};

const deleteKpiRole = async (ids: number[]) => {
  const res = await axios.put(`${apiKpiRoleEndpoint}/delete`, {
    ids
  });
  return res.data;
};
export const KpiService = {
  getListKpiCaNhan,
  getListKpiCaNhanKeKhai,
  createKpiCaNhan,
  updateKpiCaNhan,
  deleteKpiCaNhan,
  getListTrangThaiKpiCaNhan,
  updateTrangThaiKpiCaNhan,
  updateKetQuaThucTeKpiCaNhan,
  getListKpiDonVi,
  createKpiDonVi,
  updateKpiDonVi,
  deleteKpiDonVi,
  getListTrangThaiKpiDonVi,
  getListNamHocKpiDonVi,
  updateTrangThaiKpiDonVi,
  updateKetQuaThucTeKpiDonVi,
  getListKpiTruong,
  createKpiTruong,
  updateKpiTruong,
  deleteKpiTruong,
  getListTrangThaiKpiTruong,
  getListNamHocKpiTruong,
  updateTrangThaiKpiTruong,
  updateKetQuaThucTeKpiTruong,
  getListKpiRole,
  createKpiRole,
  updateKpiRole,
  deleteKpiRole,
};
