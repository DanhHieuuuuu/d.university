import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IViewNhanSu } from '@models/nhansu/nhansu.model';
import { ReduxStatus } from '@redux/const';
import { getListGuestGroup} from './delegationThunk';
import { IViewGuestGroup } from '@models/delegation/delegation.model';

interface DelegationState {
  status: ReduxStatus;
  selected: {
    status: ReduxStatus;
    id: number;
    data: any | null;
  };
  list: IViewGuestGroup[];
  total: number;
  $create: {
    status: ReduxStatus;
  };
}
const initialState: DelegationState = {
  status: ReduxStatus.IDLE,
  selected: {
    status: ReduxStatus.IDLE,
    id: 0,
    data: null
  },
  list: [],
  total: 0,
  $create: {
    status: ReduxStatus.IDLE
  }
};

const delegationSlice = createSlice({
  name: 'delegation',
  initialState,
  selectors: {
    delegationSelected: (state: DelegationState) => {
      return state.selected;
    }
  },
  reducers: {
    select: (state, action: PayloadAction<number>) => {
      state.selected.id = action.payload;
    },
    clearSelected: (state) => {
      state.selected = { id: 0, status: ReduxStatus.IDLE, data: null };
    },
    resetStatusCreate: (state) => {
      state.$create = { status: ReduxStatus.IDLE };
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(getListGuestGroup.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getListGuestGroup.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.list = action.payload?.items;
        state.total = action.payload?.totalItem;
      })
      .addCase(getListGuestGroup.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })
      
  }
});

const delegationReducer = delegationSlice.reducer;

export const { select, clearSelected, resetStatusCreate } = delegationSlice.actions;

export default delegationReducer;
