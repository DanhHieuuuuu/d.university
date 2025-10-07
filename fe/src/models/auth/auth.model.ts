export type IConnectToken = {
  grant_type: string;
  maNhanSu: string;
  password: string;
  scope: string;
  client_id: string;
  client_secret: string;
};

export type ILogin = {
  maNhanSu: string;
  password: string;
  remember: boolean;
};

export type IUser = {
  id: number | null;
  maNhanSu: string | null;
  hoDem: string | null;
  ten: string | null;
  email: string | null;
};
