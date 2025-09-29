import axios from 'axios';
import { TOKEN } from '@/constants/base.const';

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_BASE_API_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});

api.interceptors.request.use(
  (config) => {
    // Lấy token từ  sessionStorage trước, nếu không có thì lấy từ localStorage
    const token = sessionStorage.getItem(TOKEN) || localStorage.getItem(TOKEN);

    if (token) {
      config.headers = config.headers || {};
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => Promise.reject(error)
);

export default api;
