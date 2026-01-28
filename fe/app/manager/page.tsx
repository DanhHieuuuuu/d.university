'use client';

import Image from 'next/image';
import { Avatar } from 'antd';
import { UserOutlined } from '@ant-design/icons';

import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { useAppSelector } from '@redux/hooks';
import { selectorUser } from '@redux/feature/auth/authSelectors';
import { useUserAvatar } from '@hooks/useUserAvatar';

const Index = () => {
  const user = useAppSelector(selectorUser);
  const hoten = user?.hoDem + ' ' + user?.ten;
  const { avatarUrl } = useUserAvatar(user?.imageLink);
  
  return (
    <div className="flex h-full w-full flex-col items-center justify-center rounded-lg border-[1px] border-solid bg-white">
      <div className="user-information mb-8 flex flex-col items-center justify-center text-2xl">
        <Avatar
          size={{ xs: 24, sm: 32, md: 40, lg: 64, xl: 80, xxl: 100 }}
          alt={`Ảnh đại diện của ${hoten}`}
          src={avatarUrl || undefined}
          icon={!avatarUrl && <UserOutlined />}
        />
        <p className="font-bold">Xin chào {hoten} </p>
      </div>
      <div className="box rounded-lg border-[1px] border-solid p-8 text-base">
        <p className="mb-4 text-lg font-semibold">Quyền riêng tư và cá nhân hóa</p>
        <div className="container flex items-center gap-12">
          <div className="left flex flex-col gap-y-2 italic">
            <p>Bạn đã đăng nhập thành công vào hệ thống</p>
            <p>Đăng nhập gần nhất:</p>
            <p>Lần đăng nhập thất bại: 0 lần</p>
            <p>Hết hạn mật khẩu còn: Còn 365 ngày</p>
          </div>
          <div className="right">
            <Image alt="Shield_img" width={128} height={128} src="/images/shield.png" />
          </div>
        </div>
      </div>
    </div>
  );
};

export default withAuthGuard(Index, PermissionCoreConst.UserMenuAdmin);
