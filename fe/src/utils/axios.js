import axios from 'axios';

const refreshEndpoint = '/connect/token';

const api = axios.create({
  baseURL: process.env.baseApiUrl, // NestJS API
  headers: {
    'Content-Type': 'application/json',
    'Cache-Control': 'no-cache',
    Pragma: 'no-store',
    Expires: '0'
  }
});

api.interceptors.request.use(
  (config) => {
    const accessToken = localStorage.getItem('accessToken');
    if (accessToken && !config.url?.includes(refreshEndpoint)) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

api.interceptors.response.use(
  (response) => response,
  (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && !originalRequest.url.includes(refreshEndpoint)) {
      const refreshToken = localStorage.getItem('refreshToken');

      if (!refreshToken) {
        return Promise.reject(error);
      }

      return api
        .post(
          refreshEndpoint,
          {
            grant_type: 'refresh_token',
            refresh_token: refreshToken,
            client_id: process.env.NEXT_PUBLIC_AUTH_CLIENT_ID,
            client_secret: process.env.NEXT_PUBLIC_AUTH_CLIENT_SECRET
          },
          {
            headers: {
              'Content-Type': `application/x-www-form-urlencoded`
            },
            baseURL: process.env.NEXT_PUBLIC_AUTH_API_URL
          }
        )
        .then((res) => {
          localStorage.setItem('accessToken', res.data.access_token);
          localStorage.setItem('refreshToken', res.data.refresh_token);
          api.defaults.headers.Authorization = `Bearer ${res.data.access_token}`;
          return api(originalRequest);
        })
        .catch((err) => {
          console.error('Error refreshing token:', err);
          return Promise.reject(err);
        });
    }
    return Promise.reject(error);
  }
);

export default api;
