import { createAsyncThunk } from '@reduxjs/toolkit';
import { SurveyService } from '@services/survey.service';
import { IQueryRequest, ICreateRequest, IUpdateRequest, IRejectRequest } from '@models/survey/request.model';
import {
  IQuerySurvey,
  IQueryMySurvey,
  ISubmitSurvey,
  IUpdateSurvey,
  IQuerySurveyLog
} from '@models/survey/survey.model';
import { IQueryReport } from '@models/survey/report.model';

export const getPagingRequest = createAsyncThunk(
  'survey/paging-request',
  async (payload: IQueryRequest, { rejectWithValue }) => {
    try {
      const res = await SurveyService.pagingRequest(payload);
      return res.data;
    } catch (err: any) {
      return rejectWithValue(err);
    }
  }
);

export const getRequestById = createAsyncThunk('survey/get-request-by-id', async (id: number, { rejectWithValue }) => {
  try {
    const res = await SurveyService.getRequestById(id);
    return res.data;
  } catch (err: any) {
    return rejectWithValue(err);
  }
});

export const createRequest = createAsyncThunk(
  'survey/create-request',
  async (payload: ICreateRequest, { rejectWithValue }) => {
    try {
      const res = await SurveyService.createRequest(payload);
      return res;
    } catch (err: any) {
      return rejectWithValue(err?.response?.data || 'Tạo yêu cầu thất bại');
    }
  }
);

export const updateRequest = createAsyncThunk(
  'survey/update-request',
  async (payload: Partial<IUpdateRequest>, { rejectWithValue }) => {
    try {
      const res = await SurveyService.updateRequest(payload);
      return res;
    } catch (err: any) {
      return rejectWithValue(err?.response?.data || 'Cập nhật yêu cầu thất bại');
    }
  }
);

export const removeRequest = createAsyncThunk('survey/delete-request', async (id: number, { rejectWithValue }) => {
  try {
    const res = await SurveyService.removeRequest(id);
    return res;
  } catch (err: any) {
    return rejectWithValue(err?.response?.data || 'Xóa yêu cầu thất bại');
  }
});

export const submitRequestAction = createAsyncThunk(
  'survey/submit-request',
  async (id: number, { rejectWithValue }) => {
    try {
      return await SurveyService.submitRequest(id);
    } catch (err: any) {
      return rejectWithValue(err?.response?.data);
    }
  }
);

export const cancelSubmitRequestAction = createAsyncThunk(
  'survey/cancel-submit',
  async (id: number, { rejectWithValue }) => {
    try {
      return await SurveyService.cancelSubmitRequest(id);
    } catch (err: any) {
      return rejectWithValue(err?.response?.data);
    }
  }
);

export const approveRequestAction = createAsyncThunk(
  'survey/approve-request',
  async (id: number, { rejectWithValue }) => {
    try {
      return await SurveyService.approveRequest(id);
    } catch (err: any) {
      return rejectWithValue(err?.response?.data);
    }
  }
);

export const rejectRequestAction = createAsyncThunk(
  'survey/reject-request',
  async (payload: IRejectRequest, { rejectWithValue }) => {
    try {
      return await SurveyService.rejectRequest(payload);
    } catch (err: any) {
      return rejectWithValue(err?.response?.data);
    }
  }
);

export const getPagingSurvey = createAsyncThunk(
  'survey/paging-survey',
  async (payload: IQuerySurvey, { rejectWithValue }) => {
    try {
      const res = await SurveyService.pagingSurvey(payload);
      return res.data;
    } catch (err: any) {
      return rejectWithValue(err);
    }
  }
);

export const getSurveyById = createAsyncThunk('survey/get-survey-by-id', async (id: number, { rejectWithValue }) => {
  try {
    const res = await SurveyService.getSurveyById(id);
    return res.data;
  } catch (err: any) {
    return rejectWithValue(err);
  }
});

export const updateSurvey = createAsyncThunk(
  'survey/update-survey',
  async (payload: IUpdateSurvey, { rejectWithValue }) => {
    try {
      const res = await SurveyService.updateSurvey(payload);
      return res;
    } catch (err: any) {
      return rejectWithValue(err?.response?.data || 'Cập nhật khảo sát thất bại');
    }
  }
);

export const openSurveyAction = createAsyncThunk('survey/open-survey', async (id: number, { rejectWithValue }) => {
  try {
    return await SurveyService.openSurvey(id);
  } catch (err: any) {
    return rejectWithValue(err?.response?.data);
  }
});

export const closeSurveyAction = createAsyncThunk('survey/close-survey', async (id: number, { rejectWithValue }) => {
  try {
    return await SurveyService.closeSurvey(id);
  } catch (err: any) {
    return rejectWithValue(err?.response?.data);
  }
});

export const getMySurveys = createAsyncThunk(
  'survey/paging-my-surveys',
  async (payload: IQueryMySurvey, { rejectWithValue }) => {
    try {
      const res = await SurveyService.getMySurveys(payload);
      return res.data;
    } catch (err: any) {
      return rejectWithValue(err);
    }
  }
);

export const startSurveyAction = createAsyncThunk('survey/start-survey', async (id: number, { rejectWithValue }) => {
  try {
    const res = await SurveyService.startSurvey(id);
    return res.data;
  } catch (err: any) {
    return rejectWithValue(err?.response?.data);
  }
});

export const saveDraftSurveyAction = createAsyncThunk(
  'survey/save-draft',
  async (payload: ISubmitSurvey, { rejectWithValue }) => {
    try {
      return await SurveyService.saveDraftSurvey(payload);
    } catch (err: any) {
      return rejectWithValue(err?.response?.data);
    }
  }
);

export const submitSurveyAction = createAsyncThunk(
  'survey/submit-survey',
  async (payload: ISubmitSurvey, { rejectWithValue }) => {
    try {
      const res = await SurveyService.submitSurvey(payload);
      return res.data;
    } catch (err: any) {
      return rejectWithValue(err?.response?.data);
    }
  }
);

export const generateReportAction = createAsyncThunk(
  'survey/generate-report',
  async (id: number, { rejectWithValue }) => {
    try {
      return await SurveyService.generateReport(id);
    } catch (err: any) {
      return rejectWithValue(err?.response?.data);
    }
  }
);

export const getPagingReport = createAsyncThunk(
  'survey/paging-report',
  async (payload: IQueryReport, { rejectWithValue }) => {
    try {
      const res = await SurveyService.pagingReport(payload);
      return res.data;
    } catch (err: any) {
      return rejectWithValue(err);
    }
  }
);

export const getReportDetail = createAsyncThunk('survey/get-report-detail', async (id: number, { rejectWithValue }) => {
  try {
    const res = await SurveyService.getReportDetail(id);
    return res.data;
  } catch (err: any) {
    return rejectWithValue(err);
  }
});

export const analyzeWithAI = createAsyncThunk(
  'survey/analyze-with-ai',
  async (reportId: number, { rejectWithValue }) => {
    try {
      const res = await SurveyService.analyzeWithAI(reportId);
      return res.data;
    } catch (err: any) {
      return rejectWithValue(err);
    }
  }
);

export const getAIReportDetail = createAsyncThunk(
  'survey/get-ai-report-detail',
  async (reportId: number, { rejectWithValue }) => {
    try {
      const res = await SurveyService.getAIReportDetail(reportId);
      return res.data;
    } catch (err: any) {
      return rejectWithValue(err);
    }
  }
);

export const importExcelQuestions = createAsyncThunk(
  'survey/import-excel-questions',
  async (file: File, { rejectWithValue }) => {
    try {
      const res = await SurveyService.importExcelQuestions(file);
      return res.data;
    } catch (err: any) {
      return rejectWithValue(err?.response?.data || 'Import Excel thất bại');
    }
  }
);

export const getPagingSurveyLog = createAsyncThunk(
  'survey/paging-log',
  async (payload: IQuerySurveyLog, { rejectWithValue }) => {
    try {
      const res = await SurveyService.pagingSurveyLog(payload);
      return res.data;
    } catch (err: any) {
      return rejectWithValue(err);
    }
  }
);
