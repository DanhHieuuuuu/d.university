import { IQueryPaging } from '@models/common/model.common';

export type IQueryNganh = IQueryPaging & {

};

export type IViewNganh = {
    id: number;
    maNganh: string;
    tenNganh: string;
    tenTiengAnh: string;
    moTa: string;
    trangThai: boolean;
    khoaId: number;
};

export type ICreateNganh = {
    maNganh: string;
    tenNganh: string;
    tenTiengAnh: string;
    moTa: string;
    trangThai: boolean;
    khoaId: number;
};

export type IUpdateNganh = ICreateNganh & {
    id: number;
};
