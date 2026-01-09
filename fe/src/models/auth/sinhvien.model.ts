export type ISinhVienConnectToken = {
  grant_type: string;
  mssv: string;
  password: string;
  scope: string;
  client_id: string;
  client_secret: string;
};

export type ISinhVienLogin = {
  mssv: string;
  password: string;
  remember: boolean;
};

export type ISinhVien = {
  id?: string;
  mssv: string | null;
  hoDem: string | null;
  ten: string | null;
  email: string | null;
  imageLink?: string | null;
  userType?: number;
};
