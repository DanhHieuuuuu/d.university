import { configureStore } from '@reduxjs/toolkit';
import { persistStore, persistReducer, FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import authReducer from './feature/authSlice';
import loadingReducer from './feature/loadingSlice';
import nhanSuReducer from './feature/nhansuSlice';
import userReducer from './feature/userSlice';
import danhmucReducer from './feature/danhmucSlice';
import studentReducer from './feature/studentSlice';
import roleConfigReducer from './feature/roleConfigSlice';
import noticeReducer from './feature/noticeSlice';

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
      userState: userReducer,
      danhmucState: danhmucReducer,
      studentState: studentReducer,
      roleConfigState: roleConfigReducer,
      noticeState: noticeReducer
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
