'use client';

import { Button, Checkbox, Form, FormProps, Input, Spin } from 'antd';
import { colors } from '@styles/colors';
import { toast } from 'react-toastify';
import { GraduationCap } from 'lucide-react';

import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { login } from '@redux/feature/authSlice';
import { ILogin } from '@models/auth/auth.model';
import { userStatusE } from '@models/common/common';

import { useNavigate } from '@hooks/navigate';
import { processApiMsgError } from '@utils/index';

function Index() {
  const dispatch = useAppDispatch();
  const { navigateTo } = useNavigate();

  const { loading: loginLoading } = useAppSelector((state) => state.authState.$login);

  const user = useAppSelector((state) => state.authState);
  if (Object.keys(user).length > 0) {
    if (user.status === userStatusE.active) {
      navigateTo('/home');
    }
  }

  const onFinish: FormProps<ILogin>['onFinish'] = async (values) => {
    const body: ILogin = {
      maNhanSu: values.maNhanSu!,
      password: values.password!,
      remember: values.remember
    };

    try {
      const data: any = await dispatch(login(body)).unwrap();
      console.log('result login', data);

      if (data.status == 1) {
        toast.success('Đăng nhập thành công');
        navigateTo('/home');
      } else {
        toast.error(data.message);
      }
    } catch (err) {
      processApiMsgError(err, 'Đăng nhập thất bại, vui lòng thử lại.');
    }
  };

  return (
    <div
      className="relative flex min-h-screen flex-col items-center justify-center overflow-hidden"
      style={{ backgroundColor: colors.white }}
    >
      {/* Background image with 10% opacity */}
      <img
        src="/images/school-869061_1280.jpg"
        alt="library background"
        className="absolute inset-0 z-0 h-full w-full object-cover opacity-10"
        style={{ pointerEvents: 'none' }}
      />
      {/* Overlay content */}
      <div
        className="relative z-10 flex w-full max-w-md flex-col items-center rounded-2xl p-8 shadow-lg"
        style={{ backgroundColor: colors.white }}
      >
        <GraduationCap className="mb-6 h-12 w-12" style={{ color: colors.primary }} />
        <h2 className="mb-2 text-2xl font-bold" style={{ color: colors.primaryNavy }}>
          Đăng nhập
        </h2>
        <p className="mb-6 text-center" style={{ color: colors.gray }}>
          Chào mừng bạn đến với hệ thống quản lý trường học
        </p>
        {loginLoading ? (
          <div className="p-24">
            <Spin size="large" />
          </div>
        ) : (
          <Form
            name="login-form"
            layout="vertical"
            initialValues={{ remember: true }}
            onFinish={onFinish}
            autoComplete="off"
            className="flex w-full flex-col"
          >
            <Form.Item<ILogin>
              label={<span className="text-sm font-medium text-gray-700">Mã nhân sự</span>}
              name="maNhanSu"
              rules={[{ required: true, message: 'Vui lòng nhập mã nhân sự!' }]}
            >
              <Input
                className="w-full rounded-lg border px-4 py-2 focus:outline-none focus:ring-2"
                style={{ borderColor: colors.gray, boxShadow: `0 0 0 2px ${colors.primaryLight}33` }}
                placeholder="Nhập mã nhân sự"
              />
            </Form.Item>

            <Form.Item<ILogin>
              label={<span className="text-sm font-medium text-gray-700">Mật khẩu</span>}
              name="password"
              rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}
            >
              <Input.Password
                className="w-full rounded-lg border px-4 py-2 focus:outline-none focus:ring-2"
                style={{ borderColor: colors.gray, boxShadow: `0 0 0 2px ${colors.primaryLight}33` }}
                placeholder="Nhập mật khẩu"
              />
            </Form.Item>

            <Form.Item<ILogin> name="remember" valuePropName="checked" label={null}>
              <Checkbox>Ghi nhớ đăng nhập</Checkbox>
            </Form.Item>

            <Form.Item label={null}>
              <Button
                type="primary"
                htmlType="submit"
                className="w-full rounded-lg py-2 font-semibold text-white transition"
                style={{ backgroundColor: colors.primary, borderColor: colors.primaryNavy }}
              >
                Đăng nhập
              </Button>
            </Form.Item>
          </Form>
        )}
        <div className="mt-6 text-center text-sm" style={{ color: colors.gray }}>
          <span>Bạn cần hỗ trợ? Liên hệ ngay </span>
          <a href="#" style={{ color: colors.primaryLight }} className="hover:underline">
            19001009
          </a>
        </div>
      </div>
      <footer className="relative z-10 mt-8 text-center text-xs" style={{ color: colors.gray }}>
        &copy; {new Date().getFullYear()} D.University. All rights reserved.
      </footer>
    </div>
  );
}

export default Index;
