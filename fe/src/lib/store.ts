import { configureStore } from '@reduxjs/toolkit';
import authReducer from './feature/authSlice';
import loadingReducer from './feature/loadingSlice';
import nhanSuReducer from './feature/nhansuSlice';

export const makeStore = () => {
  return configureStore({
    reducer: {
      authState: authReducer,
      loadState: loadingReducer,
      nhanSuState: nhanSuReducer,
    }
  });
};

// Infer the type of makeStore
export type AppStore = ReturnType<typeof makeStore>;
// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<AppStore['getState']>;
export type AppDispatch = AppStore['dispatch'];
