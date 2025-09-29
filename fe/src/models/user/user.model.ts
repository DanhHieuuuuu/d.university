import { IQueryPaging } from "@models/common/model.common";

export type IQueryUser = IQueryPaging & {
  maNhanSu?: string
}

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

// "id": 2,
//         "maNhanSu": "2",
//         "hoDem": "Nguyen Van",
//         "ten": "A",
//         "ngaySinh": "2003-01-01T00:00:00",
//         "noiSinh": null,
//         "soDienThoai": null,
//         "email": "2",
//         "tenChucVu": null,
//         "tenPhongBan": null,
//         "trangThai": "Đang hoạt động"