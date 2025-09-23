'use client';

import { Button, Checkbox, Form, FormProps, Input } from 'antd';
import { toast } from 'react-toastify';
import { GraduationCap } from 'lucide-react';

import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { login } from '@redux/feature/authSlice';
import { ILogin } from '@models/auth/auth.model';
import { userStatusE } from '@models/common';

import { useNavigate } from '@hooks/navigate';
import { processApiMsgError } from '@utils/index';
import GlobalLoading from '@components/common/Loading';

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
    <div className="relative flex min-h-screen flex-col items-center justify-center overflow-hidden">
      {/* Background image with 10% opacity */}
      <img
        src="/images/school-869061_1280.jpg"
        alt="library background"
        className="absolute inset-0 z-0 h-full w-full object-cover opacity-10"
        style={{ pointerEvents: 'none' }}
      />
      {/* Overlay content */}
      <div className="relative z-10 flex w-full max-w-md flex-col items-center rounded-2xl bg-white p-8 shadow-lg">
        <GraduationCap className="mb-6 h-12 w-12 text-blue-600" />
        <h2 className="mb-2 text-2xl font-bold text-gray-800">Đăng nhập</h2>
        <p className="mb-6 text-center text-gray-500">Chào mừng bạn đến với hệ thống quản lý trường học</p>
        {loginLoading ? (
          <GlobalLoading />
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
                className="w-full rounded-lg border border-gray-300 px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                placeholder="Nhập mã nhân sự"
              />
            </Form.Item>

            <Form.Item<ILogin>
              label={<span className="text-sm font-medium text-gray-700">Mật khẩu</span>}
              name="password"
              rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}
            >
              <Input.Password
                className="w-full rounded-lg border border-gray-300 px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
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
                className="w-full rounded-lg bg-blue-600 py-2 font-semibold text-white transition hover:bg-blue-700"
              >
                Đăng nhập
              </Button>
            </Form.Item>
          </Form>
        )}
        <div className="mt-6 text-center text-sm text-gray-500">
          <span>Bạn cần hỗ trợ? Liên hệ ngay </span>
          <a href="#" className="text-blue-600 hover:underline">
            19001009
          </a>
        </div>
      </div>
      <footer className="relative z-10 mt-8 text-center text-xs text-gray-400">
        &copy; {new Date().getFullYear()} D.University. All rights reserved.
      </footer>
    </div>
  );
}

export default Index;