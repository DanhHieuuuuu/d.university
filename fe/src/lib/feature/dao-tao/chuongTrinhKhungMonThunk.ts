import { createAsyncThunk } from '@reduxjs/toolkit';
import {
    ICreateChuongTrinhKhungMon,
    IQueryChuongTrinhKhungMon,
    IUpdateChuongTrinhKhungMon
} from '@models/dao-tao/chuongTrinhKhungMon.model';
import { DaoTaoService } from '@services/daotao.service';

// ChuongTrinhKhungMon actions
export const getAllChuongTrinhKhungMon = createAsyncThunk(
    'daotao/list-chuongtrinhkhungmon',
    async (payload: IQueryChuongTrinhKhungMon | undefined, { rejectWithValue }) => {
        try {
            const res = await DaoTaoService.getListChuongTrinhKhungMon(payload);

            return res.data;
        } catch (error: any) {
            console.error(error);
            return rejectWithValue({
                message: error.message,
                code: error.code,
                response: error.response?.data
            });
        }
    }
);

export const createChuongTrinhKhungMon = createAsyncThunk(
    'daotao/create-chuongtrinhkhungmon',
    async (payload: ICreateChuongTrinhKhungMon, { rejectWithValue }) => {
        try {
            const res = await DaoTaoService.createChuongTrinhKhungMon(payload);

            return res.data;
        } catch (error: any) {
            console.error(error);
            return rejectWithValue({
                message: error.message,
                code: error.code,
                response: error.response?.data
            });
        }
    }
);

export const updateChuongTrinhKhungMon = createAsyncThunk(
    'daotao/update-chuongtrinhkhungmon',
    async (payload: IUpdateChuongTrinhKhungMon, { rejectWithValue }) => {
        try {
            const res = await DaoTaoService.updateChuongTrinhKhungMon(payload);

            return res.data;
        } catch (error: any) {
            console.error(error);
            return rejectWithValue({
                message: error.message,
                code: error.code,
                response: error.response?.data
            });
        }
    }
);

export const deleteChuongTrinhKhungMon = createAsyncThunk(
    'daotao/delete-chuongtrinhkhungmon',
    async (payload: number, { rejectWithValue }) => {
        try {
            const res = await DaoTaoService.deleteChuongTrinhKhungMon(payload);

            return res.data;
        } catch (error: any) {
            console.error(error);
            return rejectWithValue({
                message: error.message,
                code: error.code,
                response: error.response?.data
            });
        }
    }
);

export const getChuongTrinhKhungMonById = createAsyncThunk(
    'daotao/get-chuongtrinhkhungmon',
    async (payload: number, { rejectWithValue }) => {
        try {
            const res = await DaoTaoService.getChuongTrinhKhungMonById(payload);

            return res.data;
        } catch (error: any) {
            console.error(error);
            return rejectWithValue({
                message: error.message,
                code: error.code,
                response: error.response?.data
            });
        }
    }
);
