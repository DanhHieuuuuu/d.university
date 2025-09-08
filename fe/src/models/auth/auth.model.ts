export type IConnectToken = {
  grant_type: string;
  username: string;
  password: string;
  scope: string;
  client_id: string;
  client_secret: string;
}

export type ILogin = {
  username: string;
  password: string;
}

export type IUser = {
  id: number;
  username: string;
  ho: string | null;
  ten: string | null;
  fullName: string | null;
  email: string | null;
  role: string | null;
  position: string | null;
}