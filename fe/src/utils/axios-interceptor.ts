import axios, { AxiosError, AxiosInstance, InternalAxiosRequestConfig } from 'axios';
import { TOKEN } from '@/constants/base.const';
import { getItem, setItem as setToken, clearToken } from '@utils/token-storage';

// Axios riêng cho refresh token
const refreshApi: AxiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});

/**
 * =========================
 * Refresh Token Control
 * =========================
 */

let isRefreshing = false;
let refreshSubscribers: Array<(token: string) => void> = [];

const subscribeTokenRefresh = (cb: (token: string) => void) => {
  refreshSubscribers.push(cb);
};

const onRefreshed = (token: string) => {
  refreshSubscribers.forEach((cb) => cb(token));
  refreshSubscribers = [];
};

export const attachAuthInterceptor = (api: AxiosInstance) => {
  /**
   * =========================
   * Request Interceptor
   * =========================
   */ api.interceptors.request.use(
    (config: InternalAxiosRequestConfig) => {
      const token = sessionStorage.getItem(TOKEN) || localStorage.getItem(TOKEN);

      if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
      }

      return config;
    },
    (error) => Promise.reject(error)
  );

  /**
   * =========================
   * Response Interceptor
   * =========================
   */

  api.interceptors.response.use(
    (response) => response,
    async (error: AxiosError) => {
      const originalRequest = error.config as InternalAxiosRequestConfig & {
        _retry?: boolean;
      };

      // Không có response -> lỗi network
      if (!error.response) {
        return Promise.reject(error);
      }

      // Không phải 401 hoặc đã retry rồi -> reject
      if (error.response.status !== 401 || originalRequest._retry) {
        return Promise.reject(error);
      }

      originalRequest._retry = true;

      const { accessToken, refreshToken } = getItem();

      // Không có refresh token → logout
      if (!refreshToken) {
        clearToken();
        window.location.href = '/login';
        return Promise.reject(error);
      }

      /**
       * Nếu đang refresh -> đợi
       */
      if (isRefreshing) {
        return new Promise((resolve) => {
          subscribeTokenRefresh((newToken) => {
            originalRequest.headers.Authorization = `Bearer ${newToken}`;
            resolve(api(originalRequest));
          });
        });
      }

      /**
       * Bắt đầu refresh
       */
      isRefreshing = true;

      try {
        const res = await refreshApi.post('/nhansu/refresh-token', {
          token: accessToken,
          refreshToken
        });

        const result = res.data?.data;

        if (!result?.token) {
          throw new Error('Refresh token failed');
        }

        // Lưu token mới
        setToken({
          accessToken: result.token,
          refreshToken: result.refreshToken,
          expiredAccessToken: result.expiredToken,
          expiredRefreshToken: result.expiredRefreshToken,
          remember: true
        });

        // Set default header cho các request tiếp theo
        api.defaults.headers.common.Authorization = `Bearer ${result.token}`;

        // Thông báo cho các request đang chờ
        onRefreshed(result.token);

        // Retry request cũ
        originalRequest.headers.Authorization = `Bearer ${result.token}`;
        return api(originalRequest);
      } catch (refreshError) {
        // Nếu có lỗi chưa biết -> logout
        clearToken();
        window.location.href = '/login';
        return Promise.reject(refreshError);
      } finally {
        isRefreshing = false;
      }
    }
  );
};
