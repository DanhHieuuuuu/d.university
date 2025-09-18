export type IConnectToken = {
  grant_type: string;
  maNhanSu: string;
  password: string;
  scope: string;
  client_id: string;
  client_secret: string;
}

export type ILogin = {
  maNhanSu: string;
  password: string;
}

export type IUser = {
  // id: number;
  maNhanSu: string;
  ho: string | null;
  ten: string | null;
  fullName: string | null;
  email: string | null;
  role: string | null;
  position: string | null;
}