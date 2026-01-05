'use client';

import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';

const Index = () => {
  return <div>Trang chủ Admin</div>;
};

export default withAuthGuard(Index, PermissionCoreConst.UserMenuAdmin);
