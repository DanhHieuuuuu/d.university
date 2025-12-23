import { IQueryPaging } from '@models/common/model.common';

export type IQueryMonTienQuyet = IQueryPaging & {

};

export type IViewMonTienQuyet = {
    id: number;
    monHocId: number;
    monTienQuyetId: number;
    loaiDieuKien: string;
    ghiChu: string;
};

export type ICreateMonTienQuyet = {
    monHocId: number;
    monTienQuyetId: number;
    loaiDieuKien: string;
    ghiChu: string;
};

export type IUpdateMonTienQuyet = {
    id: number;
    loaiDieuKien: string;
    ghiChu: string;
};
