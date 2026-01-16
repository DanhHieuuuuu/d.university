import {
  IQuerySurvey,
  IViewSurvey,
  IQueryMySurvey,
  ISubmitSurvey,
  ICreateSurvey,
  IUpdateSurvey
} from '@models/survey/survey.model';
import { IResponseList } from '@models/common/response.model';
import { processApiMsgError } from '@utils/index';
import axios from '@utils/axios';
import {
  IQueryRequest,
  IViewRequest,
  ICreateRequest,
  IUpdateRequest,
  IRejectRequest
} from '@models/survey/request.model';
import { IQueryReport } from '@models/survey/report.model';

const apiSurveyEndpoint = 'survey';

const pagingRequest = async (query: IQueryRequest) => {
  try {
    const res = await axios.get(`${apiSurveyEndpoint}/paging-request`, {
      params: {
        ...query
      }
    });

    const data: IResponseList<IViewRequest> = res.data;
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tải danh sách yêu cầu khảo sát.');
    throw err;
  }
};

const getRequestById = async (id: number) => {
  try {
    const res = await axios.get(`${apiSurveyEndpoint}/get-request-by-id/${id}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tải yêu cầu khảo sát.');
    throw err;
  }
};

const createRequest = async (body: ICreateRequest) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/create-request`, body);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tạo yêu cầu khảo sát mới.');
    throw err;
  }
};

const updateRequest = async (body: Partial<IUpdateRequest>) => {
  try {
    const res = await axios.put(`${apiSurveyEndpoint}/update-request`, body);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật yêu cầu khảo sát.');
    throw err;
  }
};

const removeRequest = async (id: number) => {
  try {
    const res = await axios.delete(`${apiSurveyEndpoint}/delete-request/${id}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể xóa khảo sát.');
    throw err;
  }
};

const submitRequest = async (id: number) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/submit-request/${id}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể gửi duyệt yêu cầu.');
    throw err;
  }
};

const cancelSubmitRequest = async (id: number) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/cancel-submit/${id}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể hủy gửi duyệt.');
    throw err;
  }
};

const approveRequest = async (id: number) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/approve-request/${id}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể duyệt yêu cầu khảo sát.');
    throw err;
  }
};

const rejectRequest = async (body: IRejectRequest) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/reject-request`, body);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể từ chối yêu cầu khảo sát.');
    throw err;
  }
};

const pagingSurvey = async (query: IQuerySurvey) => {
  try {
    const res = await axios.get(`${apiSurveyEndpoint}/paging-survey`, { params: query });
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tải danh sách khảo sát.');
    throw err;
  }
};

const getSurveyById = async (id: number) => {
  try {
    const res = await axios.get(`${apiSurveyEndpoint}/get-survey-by-id/${id}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tải chi tiết khảo sát.');
    throw err;
  }
};

const updateSurvey = async (body: IUpdateSurvey) => {
  try {
    const res = await axios.put(`${apiSurveyEndpoint}/update-survey`, body);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể cập nhật khảo sát.');
    throw err;
  }
};

const openSurvey = async (id: number) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/open-survey/${id}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể mở khảo sát.');
    throw err;
  }
};

const closeSurvey = async (id: number) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/close-survey/${id}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể đóng khảo sát.');
    throw err;
  }
};

const getMySurveys = async (query: IQueryMySurvey) => {
  try {
    const res = await axios.get(`${apiSurveyEndpoint}/paging-my-surveys`, { params: query });
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tải danh sách khảo sát của bạn.');
    throw err;
  }
};

const startSurvey = async (surveyId: number) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/start-survey/${surveyId}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể bắt đầu khảo sát.');
    throw err;
  }
};

const saveDraftSurvey = async (body: ISubmitSurvey) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/save-draft-survey`, body);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Lưu bản nháp thất bại.');
    throw err;
  }
};

const submitSurvey = async (body: ISubmitSurvey) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/submit-survey`, body);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Nộp bài khảo sát thất bại.');
    throw err;
  }
};

const generateReport = async (surveyId: number) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/generate-report/${surveyId}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể khởi tạo báo cáo.');
    throw err;
  }
};

const pagingReport = async (query: IQueryReport) => {
  try {
    const res = await axios.get(`${apiSurveyEndpoint}/paging-report`, { params: query });
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tải danh sách báo cáo.');
    throw err;
  }
};

const getReportDetail = async (id: number) => {
  try {
    const res = await axios.get(`${apiSurveyEndpoint}/get-report-by-id/${id}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tải chi tiết báo cáo.');
    throw err;
  }
};

const analyzeWithAI = async (reportId: number) => {
  try {
    const res = await axios.post(`${apiSurveyEndpoint}/analyze-survey-with-ai/${reportId}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể phân tích khảo sát với AI.');
    throw err;
  }
};

const getAIReportDetail = async (reportId: number) => {
  try {
    const res = await axios.get(`${apiSurveyEndpoint}/get-ai-by-id/${reportId}`);
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể tải kết quả phân tích AI.');
    throw err;
  }
};

const importExcelQuestions = async (file: File) => {
  try {
    const formData = new FormData();
    formData.append('file', file);
    
    const res = await axios.post(`${apiSurveyEndpoint}/import-excel-questions`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
    return res.data;
  } catch (err) {
    processApiMsgError(err, 'Không thể import file Excel.');
    throw err;
  }
};


export const SurveyService = {
  pagingRequest,
  getRequestById,
  createRequest,
  updateRequest,
  removeRequest,
  submitRequest,
  cancelSubmitRequest,
  approveRequest,
  rejectRequest,
  pagingSurvey,
  getSurveyById,
  updateSurvey,
  openSurvey,
  closeSurvey,
  getMySurveys,
  startSurvey,
  saveDraftSurvey,
  submitSurvey,
  generateReport,
  pagingReport,
  getReportDetail,
  analyzeWithAI,
  getAIReportDetail,
  importExcelQuestions
};
