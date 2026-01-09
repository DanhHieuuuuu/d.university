import { configureStore } from '@reduxjs/toolkit';
import { persistStore, persistReducer, FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import authReducer from './feature/auth/authSlice';
import loadingReducer from './feature/loadingSlice';
import nhanSuReducer from './feature/nhansu/nhansuSlice';
import userReducer from './feature/userSlice';
import danhmucReducer from './feature/danh-muc/danhmucSlice';
import studentReducer from './feature/student/studentSlice';
import roleConfigReducer from './feature/roleConfigSlice';
import noticeReducer from './feature/noticeSlice';
import kpiReducer from './feature/kpi/kpiSlice';
import delegationReducer from './feature/delegation/delegationSlice';
import daotaoReducer from './feature/dao-tao/daotaoSlice';
import surveyReducer from './feature/survey/surveySlice';
import hopdongReducer from './feature/hrm/hopdong/hopdongSlice';
import quyetdinhReducer from './feature/hrm/quyetdinh/quyetdinhSlice';

const persistConfig = {
  key: 'auth',
  storage,
  whitelist: ['user', 'isAuthenticated', 'role', 'roleId', 'permissions']
};

const persistedAuthReducer = persistReducer(persistConfig, authReducer);

export const makeStore = () => {
  const store = configureStore({
    reducer: {
      authState: persistedAuthReducer,
      loadState: loadingReducer,
      nhanSuState: nhanSuReducer,
      delegationState: delegationReducer,
      userState: userReducer,
      danhmucState: danhmucReducer,
      studentState: studentReducer,
      roleConfigState: roleConfigReducer,
      noticeState: noticeReducer,
      kpiState: kpiReducer,
      daotaoState: daotaoReducer,
      surveyState: surveyReducer,
      hopdongState: hopdongReducer,
      quyetdinhState: quyetdinhReducer
    },
    middleware: (getDefaultMiddleware) =>
      getDefaultMiddleware({
        serializableCheck: {
          ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]
        }
      })
  });
  return store;
};

export const makePersistor = (store: AppStore) => persistStore(store);

// Infer the type of makeStore
export type AppStore = ReturnType<typeof makeStore>;
// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<AppStore['getState']>;
export type AppDispatch = AppStore['dispatch'];
