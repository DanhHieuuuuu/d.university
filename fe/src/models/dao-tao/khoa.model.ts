import { IQueryPaging } from '@models/common/model.common';

export type IQueryKhoa = IQueryPaging & {

};

export type IViewKhoa = {
    id: number;
    maKhoa: string;
    tenKhoa: string;
    tenTiengAnh: string;
    vietTat: string;
    email: string;
    sdt: string;
    diaChi: string;
    trangThai: boolean;
};

export type ICreateKhoa = {
    maKhoa: string;
    tenKhoa: string;
    tenTiengAnh: string;
    vietTat: string;
    email: string;
    sdt: string;
    diaChi: string;
    trangThai: boolean;
};

export type IUpdateKhoa = ICreateKhoa & {
    id: number;
};
