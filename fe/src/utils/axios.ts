import axios, { AxiosInstance } from 'axios';
import { attachAuthInterceptor } from './axios-interceptor';

// Axios chính cho toàn bộ API của dự án
const coreApi: AxiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_BASE_API_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});

attachAuthInterceptor(coreApi);

export default coreApi;
