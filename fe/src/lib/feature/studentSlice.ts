import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IQueryStudent, IViewStudent, ICreateStudent } from '@models/student/student.model';
import { StudentService } from '@services/student.service';
import { ReduxStatus } from '@redux/const';

export const getListStudent = createAsyncThunk('student/list', async (args: IQueryStudent) => {
  try {
    const res = await StudentService.findPaging(args);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const getDetailStudent = createAsyncThunk('student/detail', async (keyword: string) => {
  try {
    const res = await StudentService.find(keyword);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const createStudent = createAsyncThunk('student/create', async (body: ICreateStudent) => {
  try {
    const res = await StudentService.createSinhVien(body);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const updateStudent = createAsyncThunk('student/update', async (body: Partial<IViewStudent>) => {
  try {
    const res = await StudentService.update(body);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const deleteStudent = createAsyncThunk('student/delete', async (idStudent: number) => {
  try {
    const res = await StudentService.remove(idStudent);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

interface StudentState {
  status: ReduxStatus;
  selected: {
    status: ReduxStatus;
    idStudent: number | null;
    data: IViewStudent | null;
  };
  list: IViewStudent[];
  total: number;
}

const initialState: StudentState = {
  status: ReduxStatus.IDLE,
  selected: {
    status: ReduxStatus.IDLE,
    idStudent: null,
    data: null,
  },
  list: [],
  total: 0,
};

const studentSlice = createSlice({
  name: 'student',
  initialState,
  selectors: {
    studentSelected: (state: StudentState) => state.selected,
  },
  reducers: {
    selectStudentId: (state, action: PayloadAction<number>) => {
      state.selected.idStudent = action.payload;
    },
    clearSelected: (state) => {
      state.selected = { idStudent: null, status: ReduxStatus.IDLE, data: null };
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getListStudent.pending, (state) => {
        state.status = ReduxStatus.LOADING;
      })
      .addCase(getListStudent.fulfilled, (state, action: PayloadAction<any>) => {
        state.status = ReduxStatus.SUCCESS;
        state.list = action.payload?.items || [];
        state.total = action.payload?.totalItems || 0;
      })
      .addCase(getListStudent.rejected, (state) => {
        state.status = ReduxStatus.FAILURE;
      })

      .addCase(getDetailStudent.pending, (state) => {
        state.selected.status = ReduxStatus.LOADING;
      })
      .addCase(getDetailStudent.fulfilled, (state, action: PayloadAction<IViewStudent | null>) => {
        state.selected.status = ReduxStatus.SUCCESS;
        state.selected.data = action.payload;
      })
      .addCase(getDetailStudent.rejected, (state) => {
        state.selected.status = ReduxStatus.FAILURE;
      })

      .addCase(createStudent.fulfilled, (state) => {
        state.status = ReduxStatus.SUCCESS;
      })

      .addCase(updateStudent.fulfilled, (state) => {
        state.status = ReduxStatus.SUCCESS;
      })

      .addCase(deleteStudent.fulfilled, (state) => {
        state.status = ReduxStatus.SUCCESS;
      });
  },
});

// export const { selectMssv, clearSelected } = studentSlice.actions;
// export const { studentSelected } = studentSlice.selectors;
// export default studentSlice.reducer;

const studentReducer = studentSlice.reducer;
export const { selectStudentId, clearSelected } = studentSlice.actions;
export default studentReducer;
