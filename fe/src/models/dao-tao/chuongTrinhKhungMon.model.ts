import { IQueryPaging } from '@models/common/model.common';

export type IQueryChuongTrinhKhungMon = IQueryPaging & {
    ChuongTrinhKhungId?: number;
    HocKy?: string;
};

export type IViewChuongTrinhKhungMon = {
    id: number;
    chuongTrinhKhungId: number;
    monHocId: number;
    hocKy: string;
    namHoc: string;
    trangThai: boolean;
    // Joined fields from MonHoc
    maMonHoc?: string;
    tenMonHoc?: string;
    soTinChi?: number;
    soTietLyThuyet?: number;
    soTietThucHanh?: number;
    loaiMonHoc?: string; // 'Bắt buộc' | 'Tự chọn'
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
