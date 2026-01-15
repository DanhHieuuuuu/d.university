import axiosBase from 'axios';

const CHATBOT_API_BASE = 'http://localhost:8000/api';

// Types
export interface IChatSession {
    session_id: string;
    title: string;
    message_count: number;
    created_at: string;
    last_access: string;
}

export interface IChatMessage {
    role: 'user' | 'assistant';
    content: string;
}

export interface ISessionsResponse {
    total: number;
    sessions: IChatSession[];
}

export interface ISessionHistoryResponse {
    session_id: string;
    title: string;
    message_count: number;
    created_at: string;
    last_access: string;
    history: IChatMessage[];
}

export interface IChatRequest {
    message: string;
    session_id: string;
    conversation_history: IChatMessage[];
}

export interface IChatResponse {
    response: string;
    context_used: any[];
    session_id: string;
}

// API Functions
const getSessions = async (): Promise<ISessionsResponse> => {
    try {
        const res = await axiosBase.get(`${CHATBOT_API_BASE}/sessions`, {
            headers: { 'accept': 'application/json' }
        });
        return Promise.resolve(res.data);
    } catch (err) {
        console.error('Failed to fetch sessions:', err);
        return Promise.reject(err);
    }
};

const getSessionHistory = async (sessionId: string): Promise<ISessionHistoryResponse> => {
    try {
        const res = await axiosBase.get(`${CHATBOT_API_BASE}/session/${sessionId}/history`, {
            headers: { 'accept': 'application/json' }
        });
        return Promise.resolve(res.data);
    } catch (err) {
        console.error('Failed to fetch session history:', err);
        return Promise.reject(err);
    }
};

const sendMessage = async (request: IChatRequest): Promise<IChatResponse> => {
    try {
        const res = await axiosBase.post(`${CHATBOT_API_BASE}/chat`, request, {
            headers: {
                'accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
        return Promise.resolve(res.data);
    } catch (err) {
        console.error('Failed to send message:', err);
        return Promise.reject(err);
    }
};

const deleteSession = async (sessionId: string): Promise<void> => {
    try {
        await axiosBase.delete(`${CHATBOT_API_BASE}/session/${sessionId}`, {
            headers: { 'accept': 'application/json' }
        });
        return Promise.resolve();
    } catch (err) {
        console.error('Failed to delete session:', err);
        return Promise.reject(err);
    }
};

export const ChatbotService = {
    getSessions,
    getSessionHistory,
    sendMessage,
    deleteSession
};
