import { IQueryPaging } from '@models/common/model.common';

export type IQueryMonHoc = IQueryPaging & {

};

export type IViewMonHoc = {
    id: number;
    maMonHoc: string;
    tenMonHoc: string;
    soTinChi: number;
    soTietLyThuyet: number;
    soTietThucHanh: number;
    moTa: string;
    trangThai: boolean;
    toBoMonId: number;
};

export type ICreateMonHoc = {
    maMonHoc: string;
    tenMonHoc: string;
    soTinChi: number;
    soTietLyThuyet: number;
    soTietThucHanh: number;
    moTa: string;
    trangThai: boolean;
    toBoMonId: number;
};

export type IUpdateMonHoc = ICreateMonHoc & {
    id: number;
};
