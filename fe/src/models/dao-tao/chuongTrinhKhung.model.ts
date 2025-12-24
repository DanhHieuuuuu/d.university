import { IQueryPaging } from '@models/common/model.common';

export type IQueryChuongTrinhKhung = IQueryPaging & {

};

export type IViewChuongTrinhKhung = {
    id: number;
    maChuongTrinhKhung: string;
    tenChuongTrinhKhung: string;
    tongSoTinChi: number;
    moTa: string;
    trangThai: boolean;
    khoaHocId: number;
    nganhId: number;
    chuyenNganhId: number;
};

export type ICreateChuongTrinhKhung = {
    maChuongTrinhKhung: string;
    tenChuongTrinhKhung: string;
    tongSoTinChi: number;
    moTa: string;
    trangThai: boolean;
    khoaHocId: number;
    nganhId: number;
    chuyenNganhId: number;
};

export type IUpdateChuongTrinhKhung = ICreateChuongTrinhKhung & {
    id: number;
};
