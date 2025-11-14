import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ReduxStatus } from '@redux/const';
import { CRUD } from '@models/common/common';
import { ICreateRole, IQueryRole, IRole, IRoleDetail, IUpdateRole, IUpdateRolePermission } from '@models/role';
import { RoleService } from '@services/role.service';
import { IPermissionTree } from '@models/permission';

export const getListRole = createAsyncThunk('role-config/list', async (args: IQueryRole) => {
  try {
    const res = await RoleService.findPaging(args);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const getDetailRole = createAsyncThunk('role-config/detail', async (args: number) => {
  try {
    const res = await RoleService.findById(args);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const createRole = createAsyncThunk('role-config/create', async (args: ICreateRole) => {
  try {
    const res = await RoleService.create(args);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const updateRole = createAsyncThunk('role-config/update', async (args: IUpdateRole) => {
  try {
    const res = await RoleService.update(args);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const deleteRole = createAsyncThunk('role-config/delete', async (args: number) => {
  try {
    const res = await RoleService.deleteRole(args);

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const getListPermissionTree = createAsyncThunk('role-config/permission-tree', async () => {
  try {
    const res = await RoleService.getPermissionTree();

    return res.data;
  } catch (error: any) {
    console.error(error);
  }
});

export const updateRolePermisison = createAsyncThunk(
  'role-config/permission-update',
  async (args: IUpdateRolePermission) => {
    try {
      const res = await RoleService.updatePermission(args);

      return res.data;
    } catch (error: any) {
      console.error(error);
    }
  }
);

interface RoleConfigState {
  roleGroup: CRUD<IRole>;
  selected: {
    status: ReduxStatus;
    data: IRoleDetail | null;
    id: number | null;
  };
  permissionTree: IPermissionTree[];
  $updateRolePermission: {
    status: ReduxStatus;
  };
}

const initialState: RoleConfigState = {
  roleGroup: {
    $list: { status: ReduxStatus.IDLE, data: [], total: 0 },
    $create: { status: ReduxStatus.IDLE },
    $update: { status: ReduxStatus.IDLE },
    $delete: { status: ReduxStatus.IDLE },
    $selected: { status: ReduxStatus.IDLE, id: null, data: null }
  },
  selected: { status: ReduxStatus.IDLE, id: null, data: null },
  permissionTree: [],
  $updateRolePermission: {
    status: ReduxStatus.IDLE
  }
};

const roleConfigSlice = createSlice({
  name: 'role-config',
  initialState,
  reducers: {
    setSelectedRoleId: (state, action: PayloadAction<number>) => {
      state.selected.id = action.payload;
    },
    clearSelected: (state) => {
      state.selected = { status: ReduxStatus.IDLE, id: null, data: null };
    },
    resetStatus: (state) => {
      state.roleGroup.$create.status = ReduxStatus.IDLE;
      state.roleGroup.$update.status = ReduxStatus.IDLE;
      state.roleGroup.$delete.status = ReduxStatus.IDLE;
      state.$updateRolePermission.status = ReduxStatus.IDLE;
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(getListRole.pending, (state) => {
        state.roleGroup.$list.status = ReduxStatus.LOADING;
      })
      .addCase(getListRole.fulfilled, (state, action) => {
        state.roleGroup.$list.status = ReduxStatus.SUCCESS;
        state.roleGroup.$list.data = action.payload?.items || [];
        state.roleGroup.$list.total = action.payload?.totalItem || 0;
      })
      .addCase(getListRole.rejected, (state) => {
        state.roleGroup.$list.status = ReduxStatus.FAILURE;
      })
      .addCase(createRole.pending, (state) => {
        state.roleGroup.$create.status = ReduxStatus.LOADING;
      })
      .addCase(createRole.fulfilled, (state) => {
        state.roleGroup.$create.status = ReduxStatus.SUCCESS;
      })
      .addCase(createRole.rejected, (state) => {
        state.roleGroup.$create.status = ReduxStatus.FAILURE;
      })
      .addCase(updateRole.pending, (state) => {
        state.roleGroup.$update.status = ReduxStatus.LOADING;
      })
      .addCase(updateRole.fulfilled, (state) => {
        state.roleGroup.$update.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateRole.rejected, (state) => {
        state.roleGroup.$update.status = ReduxStatus.FAILURE;
      })
      .addCase(deleteRole.pending, (state) => {
        state.roleGroup.$delete.status = ReduxStatus.LOADING;
      })
      .addCase(deleteRole.fulfilled, (state) => {
        state.roleGroup.$delete.status = ReduxStatus.SUCCESS;
      })
      .addCase(deleteRole.rejected, (state) => {
        state.roleGroup.$delete.status = ReduxStatus.FAILURE;
      })
      .addCase(getDetailRole.pending, (state) => {
        state.selected.status = ReduxStatus.LOADING;
      })
      .addCase(getDetailRole.fulfilled, (state, action) => {
        state.selected.status = ReduxStatus.SUCCESS;
        state.selected.data = action.payload;
      })
      .addCase(getDetailRole.rejected, (state) => {
        state.selected.status = ReduxStatus.FAILURE;
      })
      .addCase(getListPermissionTree.fulfilled, (state, action) => {
        state.permissionTree = action.payload || [];
      })
      .addCase(updateRolePermisison.pending, (state) => {
        state.$updateRolePermission.status = ReduxStatus.LOADING;
      })
      .addCase(updateRolePermisison.fulfilled, (state) => {
        state.$updateRolePermission.status = ReduxStatus.SUCCESS;
      })
      .addCase(updateRolePermisison.rejected, (state) => {
        state.$updateRolePermission.status = ReduxStatus.FAILURE;
      });
  }
});

const roleConfigReducer = roleConfigSlice.reducer;

export const {
  setSelectedRoleId,
  clearSelected: clearSelectedRole,
  resetStatus: resetStatusRole
} = roleConfigSlice.actions;

export default roleConfigReducer;
