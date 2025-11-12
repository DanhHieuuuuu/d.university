import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IQueryStudent, IViewStudent, ICreateStudent, IUpdateStudent } from '@models/student/student.model';
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

export const getDetailStudent = createAsyncThunk('student/find', async (keyword: string) => {
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

export const updateStudent = createAsyncThunk('student/update', async (body: Partial<IUpdateStudent>) => {
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
    studentId: string | null;
    data: any | null;
  };
  list: IViewStudent[];
  total: number;
  $create: {
    status: ReduxStatus;
  };
  $update: {
    status: ReduxStatus;
  };
  $delete: {
    status: ReduxStatus;
  };
}
const initialState: StudentState = {
  status: ReduxStatus.IDLE,
  selected: {
    status: ReduxStatus.IDLE,
    studentId: null,
    data: null
  },
  list: [],
  total: 0,
  $create: {
    status: ReduxStatus.IDLE
  },
  $update: {
    status: ReduxStatus.IDLE
  },
  $delete: {
    status: ReduxStatus.IDLE
  }
};

const studentSlice = createSlice({
  name: 'student',
  initialState,
  selectors: {
    studentSelected: (state: StudentState) => state.selected
  },
  reducers: {
      selectStudentId: (state, action: PayloadAction<string>) => {
        state.selected.studentId = action.payload;
      },
      clearSelected: (state) => {
        state.selected = { studentId: '', status: ReduxStatus.IDLE, data: null };
      },
      resetStatusCreate: (state) => {
        state.$create = { status: ReduxStatus.IDLE };
      }
    },
    extraReducers: (builder) => {
      builder
        .addCase(getListStudent.pending, (state) => {
          state.status = ReduxStatus.LOADING;
        })
        .addCase(getListStudent.fulfilled, (state, action: PayloadAction<any>) => {
          state.status = ReduxStatus.SUCCESS;
          state.list = action.payload?.items;
          state.total = action.payload?.totalItem;
        })
        .addCase(getListStudent.rejected, (state) => {
          state.status = ReduxStatus.FAILURE;
        })
        .addCase(getDetailStudent.pending, (state) => {
          state.selected.status = ReduxStatus.LOADING;
        })
        .addCase(getDetailStudent.fulfilled, (state, action: PayloadAction<any>) => {
          state.selected.status = ReduxStatus.SUCCESS;
          state.selected.data = action.payload;
        })
        .addCase(getDetailStudent.rejected, (state) => {
          state.selected.status = ReduxStatus.FAILURE;
        })
        .addCase(createStudent.pending, (state) => {
          state.$create.status = ReduxStatus.LOADING;
        })
        .addCase(createStudent.fulfilled, (state) => {
          state.$create.status = ReduxStatus.SUCCESS;
        })
        .addCase(createStudent.rejected, (state) => {
          state.$create.status = ReduxStatus.FAILURE;
        })
        .addCase(updateStudent.pending, (state) => {
          state.$update = { status: ReduxStatus.LOADING };
        })
        .addCase(updateStudent.fulfilled, (state) => {
          state.$update = { status: ReduxStatus.SUCCESS };
        })
        .addCase(updateStudent.rejected, (state) => {
          state.$update = { status: ReduxStatus.FAILURE };
        })
        .addCase(deleteStudent.pending, (state) => {
          state.$delete = { status: ReduxStatus.LOADING };
        })
        .addCase(deleteStudent.fulfilled, (state) => {
          state.$delete = { status: ReduxStatus.SUCCESS };
        })
        .addCase(deleteStudent.rejected, (state) => {
          state.$delete = { status: ReduxStatus.FAILURE };
        });
        }
      });
  
  const studentReducer = studentSlice.reducer;
  
  export const { selectStudentId, clearSelected, resetStatusCreate } = studentSlice.actions;
  
  export default studentReducer;
