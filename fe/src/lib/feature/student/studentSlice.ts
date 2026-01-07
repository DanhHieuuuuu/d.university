import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IQueryStudent, IViewStudent, ICreateStudent, IUpdateStudent } from '@models/student/student.model';
import { ISinhVien } from '@models/auth/sinhvien.model';
import { StudentService } from '@services/student.service';
import { sinhVienLogin, sinhVienRefreshToken, sinhVienLogout } from './studentThunk';
import { setItem as setToken, clearToken } from '@utils/token-storage';
import { ReduxStatus } from '@redux/const';

export const getListStudent = createAsyncThunk('student/list', async (args: IQueryStudent) => {
  try {
    const res = await StudentService.findPaging(args);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

// export const getDetailStudent = createAsyncThunk('student/find', async (keyword: string) => {
//   try {
//     const res = await StudentService.find(keyword);
//     return res.data;
//   } catch (error: any) {
//     console.error(error);
//   }
// });

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

export const deleteStudent = createAsyncThunk('student/delete', async (mssv: string) => {
  try {
    const res = await StudentService.remove(mssv);
    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

interface StudentState {
  status: ReduxStatus;
  // Authentication state
  user: ISinhVien | null;
  isAuthenticated: boolean;
  $login: {
    loading?: boolean;
    data?: null;
  };
  // Student management state
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
  // Authentication state
  user: null,
  isAuthenticated: false,
  $login: {
    loading: false,
    data: null
  },
  // Student management state
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
    // Authentication reducers
    setSinhVien: (state, action: PayloadAction<Omit<StudentState, 'isAuthenticated'>>) => {
      return {
        ...state,
        ...action.payload,
        isAuthenticated: true
      };
    },
    clearSinhVien: (state) => {
      clearToken();
      return {
        ...initialState,
        // Keep student management state
        list: state.list,
        total: state.total,
        selected: state.selected
      };
    },
    // Student management reducers
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
      // Authentication thunks
      .addCase(sinhVienLogin.pending, (state) => {
        state.$login.loading = true;
      })
      .addCase(sinhVienLogin.fulfilled, (state, action) => {
        const { remember } = action.meta.arg;
        const { data: result, code } = action.payload;

        // save token
        setToken({
          accessToken: result.token,
          refreshToken: result.refreshToken,
          expiredAccessToken: result.expiredToken,
          expiredRefreshToken: result.expiredRefreshToken,
          remember
        });

        // update state
        state.$login.loading = false;
        state.user = result.user;
        state.isAuthenticated = true;
      })
      .addCase(sinhVienLogin.rejected, (state) => {
        state.$login.loading = false;
      })
      .addCase(sinhVienRefreshToken.pending, (state) => {
        state.$login.loading = true;
      })
      .addCase(sinhVienRefreshToken.fulfilled, (state, action) => {
        state.$login.loading = false;
        const result = action.payload.data;

        // always save as "remember" to ensure user doesn't get kicked out
        setToken({
          accessToken: result.token,
          refreshToken: result.refreshToken,
          expiredAccessToken: result.expiredToken,
          expiredRefreshToken: result.expiredRefreshToken,
          remember: true
        });
      })
      .addCase(sinhVienRefreshToken.rejected, (state) => {
        state.$login.loading = false;
      })
      .addCase(sinhVienLogout.fulfilled, (state) => {
        clearToken();
        state.user = null;
        state.isAuthenticated = false;
      })
      // Student management thunks
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
      // .addCase(getDetailStudent.pending, (state) => {
      //   state.selected.status = ReduxStatus.LOADING;
      // })
      // .addCase(getDetailStudent.fulfilled, (state, action: PayloadAction<any>) => {
      //   state.selected.status = ReduxStatus.SUCCESS;
      //   state.selected.data = action.payload;
      // })
      // .addCase(getDetailStudent.rejected, (state) => {
      //   state.selected.status = ReduxStatus.FAILURE;
      // })
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

export const { setSinhVien, clearSinhVien, selectStudentId, clearSelected, resetStatusCreate } = studentSlice.actions;

export default studentReducer;
