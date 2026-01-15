import { TOKEN, REFRESH_TOKEN, EXPIRED_TOKEN, EXPIRED_REFRESH_TOKEN } from '@/constants/base.const';

type TokenPayload = {
  accessToken: string;
  refreshToken: string;
  expiredAccessToken: string;
  expiredRefreshToken: string;
  remember: boolean;
};

const storage = (remember?: boolean) => (remember ? localStorage : sessionStorage);

export const setItem = (payload: TokenPayload) => {
  const s = storage(payload.remember);

  s.setItem(TOKEN, payload.accessToken);
  s.setItem(REFRESH_TOKEN, payload.refreshToken);
  s.setItem(EXPIRED_TOKEN, payload.expiredAccessToken);
  s.setItem(EXPIRED_REFRESH_TOKEN, payload.expiredRefreshToken);
};

export const getItem = () => {
  // Ưu tiên sessionStorage trước
  const s = sessionStorage.getItem(TOKEN) ? sessionStorage : localStorage;

  return {
    accessToken: s.getItem(TOKEN),
    refreshToken: s.getItem(REFRESH_TOKEN),
    expiredAccessToken: s.getItem(EXPIRED_TOKEN),
    expiredRefreshToken: s.getItem(EXPIRED_REFRESH_TOKEN)
  };
};

export const clearToken = () => {
  [localStorage, sessionStorage].forEach((s) => {
    s.removeItem(TOKEN);
    s.removeItem(REFRESH_TOKEN);
    s.removeItem(EXPIRED_TOKEN);
    s.removeItem(EXPIRED_REFRESH_TOKEN);
  });
};

export const getRememberFlag = (): boolean => {
  return !!localStorage.getItem(TOKEN);
};

export const getValidToken = () => {
  const { accessToken, refreshToken, expiredAccessToken, expiredRefreshToken } = getItem();

  if (!accessToken || !refreshToken || !expiredAccessToken || !expiredRefreshToken) {
    return null;
  }

  const now = new Date();
  const expAccess = new Date(expiredAccessToken);
  const expRefresh = new Date(expiredRefreshToken);

  if (now < expAccess) {
    // access token còn hạn
    return { token: accessToken, needRefresh: false };
  } else if (now < expRefresh) {
    // access hết hạn nhưng refresh còn hạn
    return { token: accessToken, needRefresh: true };
  }

  // Cả hai đều hết hạn
  return null;
};
