import axios from '@utils/axios';
import { processApiMsgError } from '@utils/index';
import { IResponseItem, IResponseArray } from '@models/common/response.model';

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

export const ChatbotService = {
  sendMessage,
  getSessionHistory,
  getHistoryByMssv,
  getSessions,
  deleteSession
};
