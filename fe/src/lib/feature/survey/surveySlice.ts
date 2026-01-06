import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ReduxStatus } from '@redux/const';
import { CRUD } from '@models/common/common';
import { IViewRequest } from '@models/survey/request.model';
import { IViewSurvey, IStartSurveyResponse, ISurveyResult } from '@models/survey/survey.model';
import { IReportItem, IReportDetail } from '@models/survey/report.model';
import * as thunks from './surveyThunk';

interface SurveyState {
    request: CRUD<IViewRequest>;
    survey: CRUD<IViewSurvey>;
    mySurveys: CRUD<IViewSurvey>; 
    report: CRUD<IReportItem> & { 
        detail: { status: ReduxStatus, data: IReportDetail | null }
    };
    execution: {
        currentExam: IStartSurveyResponse | null; // Đề bài đang làm
        result: ISurveyResult | null;             // Kết quả sau khi nộp
        status: ReduxStatus;                      // Trạng thái load đề/nộp bài
        saveDraftStatus: ReduxStatus;             // Trạng thái lưu nháp (để hiện loading nhỏ)
    };
}

const initialState: SurveyState = {
    request: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null }
    },
    survey: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null },
    },
    mySurveys: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null },
    },
    report: {
        $create: { status: ReduxStatus.IDLE },
        $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
        $update: { status: ReduxStatus.IDLE },
        $delete: { status: ReduxStatus.IDLE },
        $selected: { status: ReduxStatus.IDLE, id: null, data: null },
        detail: { status: ReduxStatus.IDLE, data: null }
    },
    execution: {
        currentExam: null,
        result: null,
        status: ReduxStatus.IDLE,
        saveDraftStatus: ReduxStatus.IDLE
    }
};

const surveySlice = createSlice({
    name: 'survey',
    initialState,
    reducers: {

        resetRequestStatus: (state) => {
            state.request.$create.status = ReduxStatus.IDLE;
            state.request.$update.status = ReduxStatus.IDLE;
            state.request.$delete.status = ReduxStatus.IDLE;
        },
        setSelectedRequest: (state, action: PayloadAction<IViewRequest>) => {
            state.request.$selected = {
                status: ReduxStatus.SUCCESS,
                id: action.payload.id,
                data: action.payload
            };
        },
        clearSelectedRequest: (state) => {
            state.request.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
        },

        // --- SURVEY REDUCERS ---
        resetSurveyStatus: (state) => {
            state.survey.$update.status = ReduxStatus.IDLE;
        },
        setSelectedSurvey: (state, action: PayloadAction<IViewSurvey>) => {
            state.survey.$selected = {
                status: ReduxStatus.SUCCESS,
                id: action.payload.id,
                data: action.payload
            };
        },
        clearSelectedSurvey: (state) => {
            state.survey.$selected = { status: ReduxStatus.IDLE, id: null, data: null };
        },

        // --- EXECUTION REDUCERS ---
        clearExecution: (state) => {
            state.execution = { 
                currentExam: null, 
                result: null, 
                status: ReduxStatus.IDLE,
                saveDraftStatus: ReduxStatus.IDLE 
            };
        },
        resetReportStatus: (state) => {
             state.report.$create.status = ReduxStatus.IDLE;
        }
    },
    extraReducers: (builder) => {
        builder
            // 1. REQUEST
            // List
            .addCase(thunks.getPagingRequest.pending, (state) => { state.request.$list.status = ReduxStatus.LOADING; })
            .addCase(thunks.getPagingRequest.fulfilled, (state, action) => {
                state.request.$list.status = ReduxStatus.SUCCESS;
                state.request.$list.data = action.payload?.items || [];
                state.request.$list.total = action.payload?.totalItem || 0;
            })
            .addCase(thunks.getPagingRequest.rejected, (state) => { state.request.$list.status = ReduxStatus.FAILURE; })
            
            // GetById
            .addCase(thunks.getRequestById.fulfilled, (state, action) => {
                state.request.$selected = { status: ReduxStatus.SUCCESS, id: action.payload.id, data: action.payload };
            })

            // Create
            .addCase(thunks.createRequest.pending, (state) => { state.request.$create.status = ReduxStatus.LOADING; })
            .addCase(thunks.createRequest.fulfilled, (state) => { state.request.$create.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.createRequest.rejected, (state) => { state.request.$create.status = ReduxStatus.FAILURE; })

            // Update & Approve & Reject & Submit
            .addCase(thunks.updateRequest.pending, (state) => { state.request.$update.status = ReduxStatus.LOADING; })
            .addCase(thunks.updateRequest.fulfilled, (state) => { state.request.$update.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.updateRequest.rejected, (state) => { state.request.$update.status = ReduxStatus.FAILURE; })
            
            .addCase(thunks.approveRequestAction.pending, (state) => { state.request.$update.status = ReduxStatus.LOADING; })
            .addCase(thunks.approveRequestAction.fulfilled, (state) => { state.request.$update.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.approveRequestAction.rejected, (state) => { state.request.$update.status = ReduxStatus.FAILURE; })

            .addCase(thunks.rejectRequestAction.pending, (state) => { state.request.$update.status = ReduxStatus.LOADING; })
            .addCase(thunks.rejectRequestAction.fulfilled, (state) => { state.request.$update.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.rejectRequestAction.rejected, (state) => { state.request.$update.status = ReduxStatus.FAILURE; })

            .addCase(thunks.submitRequestAction.pending, (state) => { state.request.$update.status = ReduxStatus.LOADING; })
            .addCase(thunks.submitRequestAction.fulfilled, (state) => { state.request.$update.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.submitRequestAction.rejected, (state) => { state.request.$update.status = ReduxStatus.FAILURE; })
            
            .addCase(thunks.cancelSubmitRequestAction.pending, (state) => { state.request.$update.status = ReduxStatus.LOADING; })
            .addCase(thunks.cancelSubmitRequestAction.fulfilled, (state) => { state.request.$update.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.cancelSubmitRequestAction.rejected, (state) => { state.request.$update.status = ReduxStatus.FAILURE; })

            // Delete
            .addCase(thunks.removeRequest.pending, (state) => { state.request.$delete.status = ReduxStatus.LOADING; })
            .addCase(thunks.removeRequest.fulfilled, (state) => { state.request.$delete.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.removeRequest.rejected, (state) => { state.request.$delete.status = ReduxStatus.FAILURE; })

            // 2. SURVEY (ADMIN)
            // List
            .addCase(thunks.getPagingSurvey.pending, (state) => { state.survey.$list.status = ReduxStatus.LOADING; })
            .addCase(thunks.getPagingSurvey.fulfilled, (state, action) => {
                state.survey.$list.status = ReduxStatus.SUCCESS;
                state.survey.$list.data = action.payload?.items || [];
                state.survey.$list.total = action.payload?.totalItem || 0;
            })
            // GetById
            .addCase(thunks.getSurveyById.fulfilled, (state, action) => {
                state.survey.$selected = { status: ReduxStatus.SUCCESS, id: action.payload.id, data: action.payload };
            })

            // Create
            .addCase(thunks.createSurvey.pending, (state) => { state.survey.$create.status = ReduxStatus.LOADING; })
            .addCase(thunks.createSurvey.fulfilled, (state) => { state.survey.$create.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.createSurvey.rejected, (state) => { state.survey.$create.status = ReduxStatus.FAILURE; })

            // Update
            .addCase(thunks.updateSurvey.pending, (state) => { state.survey.$update.status = ReduxStatus.LOADING; })
            .addCase(thunks.updateSurvey.fulfilled, (state) => { state.survey.$update.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.updateSurvey.rejected, (state) => { state.survey.$update.status = ReduxStatus.FAILURE; })

            // Open/Close
            .addCase(thunks.openSurveyAction.pending, (state) => { state.survey.$update.status = ReduxStatus.LOADING; })
            .addCase(thunks.openSurveyAction.fulfilled, (state) => { state.survey.$update.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.openSurveyAction.rejected, (state) => { state.survey.$update.status = ReduxStatus.FAILURE; })

            .addCase(thunks.closeSurveyAction.pending, (state) => { state.survey.$update.status = ReduxStatus.LOADING; })
            .addCase(thunks.closeSurveyAction.fulfilled, (state) => { state.survey.$update.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.closeSurveyAction.rejected, (state) => { state.survey.$update.status = ReduxStatus.FAILURE; })


            // 3. MY SURVEY & EXECUTION (USER)
            .addCase(thunks.getMySurveys.pending, (state) => { state.mySurveys.$list.status = ReduxStatus.LOADING; })
            .addCase(thunks.getMySurveys.fulfilled, (state, action) => {
                state.mySurveys.$list.status = ReduxStatus.SUCCESS;
                state.mySurveys.$list.data = action.payload?.items || [];
                state.mySurveys.$list.total = action.payload?.totalItem || 0;
            })

            // Start Survey
            .addCase(thunks.startSurveyAction.pending, (state) => { state.execution.status = ReduxStatus.LOADING; })
            .addCase(thunks.startSurveyAction.fulfilled, (state, action) => {
                state.execution.status = ReduxStatus.SUCCESS;
                state.execution.currentExam = action.payload;
            })
            .addCase(thunks.startSurveyAction.rejected, (state) => { state.execution.status = ReduxStatus.FAILURE; })

            // Save Draft 
            .addCase(thunks.saveDraftSurveyAction.pending, (state) => { state.execution.saveDraftStatus = ReduxStatus.LOADING; })
            .addCase(thunks.saveDraftSurveyAction.fulfilled, (state) => { state.execution.saveDraftStatus = ReduxStatus.SUCCESS; })
            
            // Submit Survey
            .addCase(thunks.submitSurveyAction.pending, (state) => { state.execution.status = ReduxStatus.LOADING; })
            .addCase(thunks.submitSurveyAction.fulfilled, (state, action) => {
                state.execution.status = ReduxStatus.SUCCESS;
                state.execution.result = action.payload;
            })
            .addCase(thunks.submitSurveyAction.rejected, (state) => { state.execution.status = ReduxStatus.FAILURE; })


            // 4. REPORT
            // List
            .addCase(thunks.getPagingReport.pending, (state) => { state.report.$list.status = ReduxStatus.LOADING; })
            .addCase(thunks.getPagingReport.fulfilled, (state, action) => {
                state.report.$list.status = ReduxStatus.SUCCESS;
                state.report.$list.data = action.payload?.items || [];
                state.report.$list.total = action.payload?.totalItem || 0;
            })

            // Generate Report
            .addCase(thunks.generateReportAction.pending, (state) => { state.report.$create.status = ReduxStatus.LOADING; })
            .addCase(thunks.generateReportAction.fulfilled, (state) => { state.report.$create.status = ReduxStatus.SUCCESS; })
            .addCase(thunks.generateReportAction.rejected, (state) => { state.report.$create.status = ReduxStatus.FAILURE; })

            // Report Detail
            .addCase(thunks.getReportDetail.pending, (state) => { state.report.detail.status = ReduxStatus.LOADING; })
            .addCase(thunks.getReportDetail.fulfilled, (state, action) => { 
                state.report.detail.status = ReduxStatus.SUCCESS;
                state.report.detail.data = action.payload;
            });
    }
});

export const { 
    resetRequestStatus, setSelectedRequest, clearSelectedRequest,
    resetSurveyStatus, setSelectedSurvey, clearSelectedSurvey,
    clearExecution,
    resetReportStatus
} = surveySlice.actions;

export default surveySlice.reducer;