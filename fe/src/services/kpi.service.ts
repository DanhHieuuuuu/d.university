import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseList, IResponseItem } from '@models/common/response.model';
import {
  ICreateKpiCaNhan,
  IQueryKpiCaNhan,
  IUpdateCapTrenDanhGiaList,
  IUpdateKpiCaNhan,
  IUpdateKpiCaNhanThucTeList,
  IUpdateTrangThaiKpiCaNhan,
  IViewKpiCaNhan
} from '@models/kpi/kpi-ca-nhan.model';
import {
  ICreateKpiDonVi,
  IGiaoKpiDonVi,
  IQueryKpiDonVi,
  IUpdateCapTrenDonViDanhGiaList,
  IUpdateKpiDonVi,
  IUpdateKpiDonViThucTeList,
  IUpdateTrangThaiKpiDonVi,
  IViewKpiDonVi
} from '@models/kpi/kpi-don-vi.model';
import { ICreateKpiRole, IQueryKpiRole, IUpdateKpiRole, IViewKpiRole } from '@models/kpi/kpi-role.model';
import {
  ICreateKpiTruong,
  IQueryKpiTruong,
  IUpdateCapTrenTruongDanhGiaList,
  IUpdateKpiTruong,
  IUpdateKpiTruongThucTeList,
  IUpdateTrangThaiKpiTruong,
  IViewKpiTruong
} from '@models/kpi/kpi-truong.model';
import { IViewNhanSu } from '@models/nhansu/nhansu.model';
import { IQueryKpiLogStatus } from '@models/kpi/kpi-log.model';
import { IAskKpiChatCommand } from '@models/kpi/kpi-chat.model';
import { IQueryKpiCongThuc, IViewKpiCongThuc } from '@models/kpi/kpi-cong-thuc.model';
import { IKpiScoreBoardResponse, IQueryKpiScoreBoard } from '@models/kpi/kpi-scoreboard.model';

const apiDanhMucEndpoint = 'kpi';
const apiKpiTruongEndpoint = `${apiDanhMucEndpoint}/kpi-truong`;
const apiKpiDonViEndpoint = `${apiDanhMucEndpoint}/kpi-donvi`;
const apiKpiCaNhanEndpoint = `${apiDanhMucEndpoint}/kpi-canhan`;
const apiKpiRoleEndpoint = `${apiDanhMucEndpoint}/kpi-role`;
const apiKpiLogEndpoint = `${apiDanhMucEndpoint}/kpi-log`;
const apiKPiChatEndPoint = `${apiDanhMucEndpoint}/kpi-chat`;
const apiKpiCongThucEndpoint = `${apiDanhMucEndpoint}/kpi-congthuc`;
const apiKpiScoreBoardEndpoint = `${apiDanhMucEndpoint}/kpi-tinhdiem`;
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
    const res = await axios.get(`${apiKpiCaNhanEndpoint}/find-ke-khai`, { params: { ...query } });
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
    const res = await axios.get(`${apiKpiCaNhanEndpoint}/trang-thai`);

    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }

    return res.data.data;
  } catch (err) {
    return Promise.reject(err);
  }
};

const updateTrangThaiKpiCaNhan = async (body: IUpdateTrangThaiKpiCaNhan) => {
  try {
    const res = await axios.put(`${apiKpiCaNhanEndpoint}/update-trang-thai`, body);

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

const updateKetQuaThucTeKpiCaNhan = async (body: IUpdateKpiCaNhanThucTeList) => {
  try {
    const res = await axios.put(`${apiKpiCaNhanEndpoint}/update-ket-qua-thuc-te`, body);

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

const updateKetQuaCapTrenKpiCaNhan = async (body: IUpdateCapTrenDanhGiaList) => {
  try {
    const res = await axios.put(`${apiKpiCaNhanEndpoint}/update-ket-qua-cap-tren`, body);

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Cập nhật kết quả đánh giá thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi cập nhật kết quả đánh giá');
    return Promise.reject(err);
  }
};

export const getAllNhanSuKiemNhiem = async (idPhongBan?: number) => {
  try {
    const res = await axios.get(`${apiKpiCaNhanEndpoint}/danh-sach-nhan-su-kiem-nhiem`, {
      params: { idPhongBan }
    });
    return res.data as IResponseList<IViewNhanSu>;
  } catch (err) {
    processApiMsgError(err, '');
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

const getListKpiDonViKeKhai = async (query?: IQueryKpiDonVi) => {
  try {
    const res = await axios.get(`${apiKpiDonViEndpoint}/find-ke-khai`, { params: { ...query } });
    return res.data;
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

const updateTrangThaiKpiDonVi = async (body: IUpdateTrangThaiKpiDonVi) => {
  try {
    const res = await axios.put(`${apiKpiDonViEndpoint}/update-trang-thai`, body);

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

const updateKetQuaThucTeKpiDonVi = async (body: IUpdateKpiDonViThucTeList) => {
  try {
    const res = await axios.put(`${apiKpiDonViEndpoint}/update-ket-qua-thuc-te`, body);

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

const updateKetQuaCapTrenKpiDonVi = async (body: IUpdateCapTrenDonViDanhGiaList) => {
  try {
    const res = await axios.put(`${apiKpiDonViEndpoint}/update-ket-qua-cap-tren`, body);

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Cập nhật kết quả đánh giá thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi cập nhật kết quả đánh giá');
    return Promise.reject(err);
  }
};

export const getListNamHocKpiDonVi = async () => {
  try {
    const res = await axios.get(`${apiKpiDonViEndpoint}/list-nam-hoc`);

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
    const res = await axios.get(`${apiKpiDonViEndpoint}/trang-thai`);

    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }

    return res.data.data;
  } catch (err) {
    return Promise.reject(err);
  }
};

const giaoKpiDonVi = async (body: IGiaoKpiDonVi) => {
  try {
    const res = await axios.put(`${apiKpiDonViEndpoint}/giao-kpi`, body);

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Giao KPI thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi giao KPI');
    return Promise.reject(err);
  }
};

const getNhanSuDaGiaoByKpiDonVi = async (idKpiDonVi: number) => {
  try {
    const res = await axios.get(`${apiKpiDonViEndpoint}/nhan-su-da-giao`, {
      params: { idKpiDonVi }
    });

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Không lấy được danh sách nhân sự đã giao'
      });
    }

    return Promise.resolve(res.data.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi lấy nhân sự đã giao KPI');
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

const updateTrangThaiKpiTruong = async (body: IUpdateTrangThaiKpiTruong) => {
  try {
    const res = await axios.put(`${apiKpiTruongEndpoint}/update-trang-thai`, body);

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

const updateKetQuaThucTeKpiTruong = async (body: IUpdateKpiTruongThucTeList) => {
  try {
    const res = await axios.put(`${apiKpiTruongEndpoint}/update-ket-qua-thuc-te`, body);

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

const updateKetQuaCapTrenKpiTruong = async (body: IUpdateCapTrenTruongDanhGiaList) => {
  try {
    const res = await axios.put(`${apiKpiTruongEndpoint}/update-ket-qua-cap-tren`, body);

    if (res.data?.code !== 200) {
      return Promise.reject({
        message: res.data?.message || 'Cập nhật kết quả đánh giá thất bại'
      });
    }

    return Promise.resolve(res.data);
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi cập nhật kết quả đánh giá');
    return Promise.reject(err);
  }
};

export const getListNamHocKpiTruong = async () => {
  try {
    const res = await axios.get(`${apiKpiTruongEndpoint}/list-nam-hoc`);

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
    const res = await axios.get(`${apiKpiTruongEndpoint}/trang-thai`);

    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }

    return res.data.data;
  } catch (err) {
    return Promise.reject(err);
  }
};

const getListAllKpiTruongApi = async () => {
  try {
    const res = await axios.get(`${apiKpiTruongEndpoint}/get-list-all`);
    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }
    return res.data.data;
  } catch (err) {
    return Promise.reject(err);
  }
};

const getListAllKpiTruongApi = async () => {
  try {
    const res = await axios.get(`${apiKpiTruongEndpoint}/get-list-all`);
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

export const getListKpiRoleByUser = async () => {
  try {
    const res = await axios.get(`${apiKpiRoleEndpoint}/list-role-by-user`);

    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message);
    }

    return res.data.data;
  } catch (err) {
    return Promise.reject(err);
  }
};
//KPi log

const getKpiLogs = async (query?: IQueryKpiLogStatus) => {
  try {
    const res = await axios.get(`${apiKpiLogEndpoint}/get-log-status`, {
      params: query
    });
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Lỗi khi lấy log KPI');
    return Promise.reject(err);
  }
};

const askKpiAi = async (payload: IAskKpiChatCommand) => {
  try {
    const res = await axios.post(`${apiKPiChatEndPoint}/ask`, payload);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Có lỗi xảy ra khi gọi AI');
    return Promise.reject(err);
  }
};

const getListKpiCongThuc = async (query?: IQueryKpiCongThuc) => {
  try {
    const res = await axios.get(`${apiKpiCongThucEndpoint}/danh-sach`, {
      params: { ...query }
    });
    return res.data.data as IViewKpiCongThuc[];
  } catch (err) {
    processApiMsgError(err, 'Không thể lấy danh sách công thức');
    return Promise.reject(err);
  }
};

const getKpiScoreBoard = async (query: IQueryKpiScoreBoard) => {
  try {
    const res = await axios.get(`${apiKpiScoreBoardEndpoint}/kpi-scoreboard`, {
      params: {
        ...query
      }
    });
    if (res.data?.code !== 200) {
      return Promise.reject(res.data?.message || 'Lỗi lấy bảng điểm');
    }
    const data: IKpiScoreBoardResponse = res.data.data;
    return Promise.resolve(data);
  } catch (err) {
    return Promise.reject(err);
  }
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
  updateKetQuaCapTrenKpiCaNhan,
  getListKpiDonVi,
  getListKpiDonViKeKhai,
  createKpiDonVi,
  updateKpiDonVi,
  deleteKpiDonVi,
  getListTrangThaiKpiDonVi,
  getListNamHocKpiDonVi,
  updateTrangThaiKpiDonVi,
  updateKetQuaThucTeKpiDonVi,
  updateKetQuaCapTrenKpiDonVi,
  giaoKpiDonVi,
  getNhanSuDaGiaoByKpiDonVi,
  getListKpiTruong,
  createKpiTruong,
  updateKpiTruong,
  deleteKpiTruong,
  getListTrangThaiKpiTruong,
  getListNamHocKpiTruong,
  updateTrangThaiKpiTruong,
  updateKetQuaThucTeKpiTruong,
  updateKetQuaCapTrenKpiTruong,
  getListAllKpiTruongApi,
  getListKpiRole,
  createKpiRole,
  updateKpiRole,
  deleteKpiRole,
  getListKpiRoleByUser,
  getKpiLogs,
  askKpiAi,
  getListKpiCongThuc,
  getKpiScoreBoard
};
