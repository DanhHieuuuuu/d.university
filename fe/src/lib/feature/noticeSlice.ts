import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { ReduxStatus } from '@redux/const';
import { NotiService } from '@services/noti.service';
import { IFilterNotification, IViewNotification } from '@models/notice/notification.model';

export const $fetchNotification = createAsyncThunk(
  'notice/list',
  async (args: IFilterNotification, { rejectWithValue }) => {
    try {
      const res = await NotiService.fetchNotification(args);

      return res.data;
    } catch (error: any) {
      console.error(error);
      return rejectWithValue(error.response?.data);
    }
  }
);

export const $markAllAsRead = createAsyncThunk('notice/all-read', async (_, { rejectWithValue }) => {
  try {
    const res = await NotiService.markAllAsRead();

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue(error.response?.data);
  }
});

export const $markAsRead = createAsyncThunk('notice/read', async (notiId: number, { rejectWithValue }) => {
  try {
    const res = await NotiService.markRead(notiId);

    return res.data;
  } catch (error: any) {
    console.error(error);
    return rejectWithValue(error.response?.data || 'Đánh dấu đã đọc thất bại.');
  }
});

interface NotificationState {
  $list: {
    data: IViewNotification[];
    total: number;
    totalUnread: number;
    status: ReduxStatus;
    loadedPages: number[];
  };
}

const initialState: NotificationState = {
  $list: {
    data: [],
    total: 0,
    totalUnread: 0,
    status: ReduxStatus.IDLE,
    loadedPages: []
  }
};

const noticeSlice = createSlice({
  name: 'notice',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase($fetchNotification.pending, (state) => {
        state.$list.status = ReduxStatus.LOADING;
      })
      .addCase($fetchNotification.fulfilled, (state, action) => {
        state.$list.status = ReduxStatus.SUCCESS;

        const pageIndex = action.meta.arg.PageIndex;
        const newItems = action.payload?.items ?? [];

        if (state.$list.loadedPages.includes(pageIndex)) {
          return;
        }

        state.$list.loadedPages.push(pageIndex);

        if (pageIndex === 0) {
          state.$list.data = newItems;
        } else {
          state.$list.data = [...state.$list.data, ...newItems];
        }

        state.$list.total = action.payload?.totalItem ?? 0;
        state.$list.totalUnread = action.payload?.totalUnread ?? 0;
      })
      .addCase($fetchNotification.rejected, (state) => {
        state.$list.status = ReduxStatus.FAILURE;
      })
      .addCase($markAllAsRead.fulfilled, (state) => {
        state.$list.data = state.$list.data.map((item) => ({
          ...item,
          isRead: true
        }));
        state.$list.totalUnread = 0;
      })
      .addCase($markAsRead.fulfilled, (state, action) => {
        const notiId = action.meta.arg;
        let unreadCount = 0;

        state.$list.data = state.$list.data.map((item) => {
          const isRead = item.id == notiId ? true : item.isRead;
          if (!isRead) unreadCount++;
          return { ...item, isRead };
        });

        state.$list.totalUnread = unreadCount;
      });
  }
});

const noticeReducer = noticeSlice.reducer;

export const {} = noticeSlice.actions;

export default noticeReducer;
