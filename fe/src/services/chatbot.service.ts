import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseItem, IResponseArray } from '@models/common/response.model';
import { IModelChatbot, ICreateModelChatbot, IUpdateModelChatbot } from '@models/chatbot/modelChatbot.model';

const apiChatbotEndpoint = 'chatbot';

// Types
export interface IChatSession {
  id: number;
  sessionId: string;
  mssv: string;
  title: string | null;
  role: 'user' | 'assistant';
  content: string;
  lastAccess: string;
  createdDate: string;
}

export interface IChatMessage {
  role: 'user' | 'assistant';
  content: string;
}

export interface IChatHistoryItem {
  id: number;
  sessionId: string;
  mssv: string;
  title: string | null;
  role: 'user' | 'assistant';
  content: string;
  lastAccess: string;
  createdDate: string;
}

export interface IChatRequest {
  message: string;
  sessionId: string;
  mssv: string;
}

export interface IChatResponseData {
  response: string;
  contextUsed: string[];
  rewrittenQuery: string | null;
  sessionId: string;
}

// API Functions

/**
 * Gui tin nhan chat
 * POST /api/chatbot/chat
 */
const sendMessage = async (request: IChatRequest): Promise<IChatResponseData> => {
  try {
    const res = await axios.post(`${apiChatbotEndpoint}/chat`, request);
    const data: IResponseItem<IChatResponseData> = res.data;
    return Promise.resolve(data.data);
  } catch (err) {
    processApiMsgError(err, 'Khong the gui tin nhan. Vui long thu lai.');
    return Promise.reject(err);
  }
};

/**
 * Lay lich su chat theo session
 * GET /api/chatbot/by-session?SessionId=xxx
 */
const getSessionHistory = async (sessionId: string): Promise<IChatHistoryItem[]> => {
  try {
    const res = await axios.get(`${apiChatbotEndpoint}/by-session`, {
      params: { SessionId: sessionId }
    });
    const data: IResponseArray<IChatHistoryItem> = res.data;
    return Promise.resolve(data.data || []);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

/**
 * Lay lich su chat theo mssv
 * GET /api/chatbot/by-mssv?Mssv=xxx
 */
const getHistoryByMssv = async (mssv: string): Promise<IChatHistoryItem[]> => {
  try {
    const res = await axios.get(`${apiChatbotEndpoint}/by-mssv`, {
      params: { Mssv: mssv }
    });
    const data: IResponseArray<IChatHistoryItem> = res.data;
    return Promise.resolve(data.data || []);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

/**
 * Lay danh sach session cua sinh vien
 * GET /api/chatbot/sessions?Mssv=xxx
 */
const getSessions = async (mssv: string): Promise<IChatSession[]> => {
  try {
    const res = await axios.get(`${apiChatbotEndpoint}/sessions`, {
      params: { Mssv: mssv }
    });
    const data: IResponseArray<IChatSession> = res.data;
    return Promise.resolve(data.data || []);
  } catch (err) {
    processApiMsgError(err, '');
    return Promise.reject(err);
  }
};

/**
 * Xoa lich su chat
 * DELETE /api/chatbot/delete
 */
const deleteSession = async (sessionId: string): Promise<void> => {
  try {
    await axios.delete(`${apiChatbotEndpoint}/delete`, {
      params: { SessionId: sessionId }
    });
    return Promise.resolve();
  } catch (err) {
    processApiMsgError(err, 'Khong the xoa cuoc tro chuyen. Vui long thu lai.');
    return Promise.reject(err);
  }
};

// ==================== Model Chatbot APIs ====================

/**
 * Lay danh sach tat ca Chatbot Model
 * GET /api/chatbot/model/list
 */
const getModelList = async (): Promise<IModelChatbot[]> => {
  try {
    const res = await axios.get(`${apiChatbotEndpoint}/model/list`);
    const data: IResponseArray<IModelChatbot> = res.data;
    return Promise.resolve(data.data || []);
  } catch (err) {
    processApiMsgError(err, 'Khong the tai danh sach model');
    return Promise.reject(err);
  }
};

/**
 * Lay Chatbot Model theo Id
 * GET /api/chatbot/model/{id}
 */
const getModelById = async (id: number): Promise<IModelChatbot> => {
  try {
    const res = await axios.get(`${apiChatbotEndpoint}/model/${id}`);
    const data: IResponseItem<IModelChatbot> = res.data;
    return Promise.resolve(data.data);
  } catch (err) {
    processApiMsgError(err, 'Khong the tai thong tin model');
    return Promise.reject(err);
  }
};

/**
 * Them moi Chatbot Model
 * POST /api/chatbot/model/create
 */
const createModel = async (model: ICreateModelChatbot): Promise<IModelChatbot> => {
  try {
    const res = await axios.post(`${apiChatbotEndpoint}/model/create`, model);
    const data: IResponseItem<IModelChatbot> = res.data;
    return Promise.resolve(data.data);
  } catch (err) {
    processApiMsgError(err, 'Khong the tao model moi');
    return Promise.reject(err);
  }
};

/**
 * Cap nhat Chatbot Model
 * PUT /api/chatbot/model/update
 */
const updateModel = async (model: IUpdateModelChatbot): Promise<IModelChatbot> => {
  try {
    const res = await axios.put(`${apiChatbotEndpoint}/model/update`, model);
    const data: IResponseItem<IModelChatbot> = res.data;
    return Promise.resolve(data.data);
  } catch (err) {
    processApiMsgError(err, 'Khong the cap nhat model');
    return Promise.reject(err);
  }
};

/**
 * Xoa Chatbot Model
 * DELETE /api/chatbot/model/delete/{id}
 */
const deleteModel = async (id: number): Promise<void> => {
  try {
    await axios.delete(`${apiChatbotEndpoint}/model/delete/${id}`);
    return Promise.resolve();
  } catch (err) {
    processApiMsgError(err, 'Khong the xoa model');
    return Promise.reject(err);
  }
};

export const ChatbotService = {
  sendMessage,
  getSessionHistory,
  getHistoryByMssv,
  getSessions,
  deleteSession,
  // Model APIs
  getModelList,
  getModelById,
  createModel,
  updateModel,
  deleteModel
};
