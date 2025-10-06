import { IQueryPaging } from '@models/common/model.common';

export type IQueryUser = IQueryPaging & {
  maNhanSu?: string;
};

export type IUserView = {
  id?: number;
  maNhanSu?: string;
  hoTen?: string;
  ngaySinh?: Date;
  noiSinh?: string;
  soDienThoai?: string;
  email?: string;
  tenChucVu?: string;
  tenPhongBan?: string;
  trangThai?: string;
};
