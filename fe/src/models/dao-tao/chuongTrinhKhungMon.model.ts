import { IQueryPaging } from '@models/common/model.common';

export type IQueryChuongTrinhKhungMon = IQueryPaging & {

};

export type IViewChuongTrinhKhungMon = {
    id: number;
    chuongTrinhKhungId: number;
    monHocId: number;
    hocKy: string;
    namHoc: string;
    trangThai: boolean;
};

export type ICreateChuongTrinhKhungMon = {
    chuongTrinhKhungId: number;
    monHocId: number;
    hocKy: string;
    namHoc: string;
    trangThai: boolean;
};

export type IUpdateChuongTrinhKhungMon = {
    id: number;
    hocKy: string;
    namHoc: string;
    trangThai: boolean;
};
