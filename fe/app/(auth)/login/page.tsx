'use client';

import { useRouter } from 'next/navigation';
import { Button, Checkbox, Form, FormProps, Input } from 'antd';
import Loading from '@components/common/Loading';

import { GraduationCap } from "lucide-react";
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { login } from '@redux/feature/authSlice';

import { userStatusE } from '@models/common';
import { ILogin } from '@models/auth/auth.model';
import { processApiMsgError } from '@utils/index';

type FieldType = {
  maNhanSu?: string;
  password?: string;
  remember?: string;
};

const onFinishFailed: FormProps<FieldType>['onFinishFailed'] = (errorInfo) => {
  console.log('Failed:', errorInfo);
};

function Index() {
  const dispatch = useAppDispatch();
  const router = useRouter();

  const { loading: loginLoading } = useAppSelector((state) => state.authState.$login);

  const user = useAppSelector((state) => state.authState);
  if (Object.keys(user).length > 0) {
    if (user.status === userStatusE.active) {
      router.push('/projects');
    }
  }

  const onFinish: FormProps<FieldType>['onFinish'] = async (values) => {
    const body: ILogin = {
      maNhanSu: values.maNhanSu!,
      password: values.password!
    };

    try {
      const data: any = await dispatch(login(body)).unwrap();

      if (values.remember) {
        localStorage.setItem('refreshToken', data.refresh_token);
      }
      localStorage.setItem('accessToken', data.access_token);

      router.push('/home');
    } catch (err) {
      processApiMsgError(err, 'Đăng nhập thất bại, vui lòng thử lại.');
    }
  };

  return (
    <div className="min-h-screen flex flex-col items-center justify-center relative overflow-hidden">
      {/* Background image with 10% opacity */}
      <img
        src="/images/school-869061_1280.jpg"
        alt="library background"
        className="absolute inset-0 w-full h-full object-cover opacity-10 z-0"
        style={{ pointerEvents: 'none' }}
      />
      {/* Overlay content */}
      <div className="relative z-10 w-full max-w-md bg-white rounded-2xl shadow-lg p-8 flex flex-col items-center">
        <GraduationCap className="h-12 w-12 text-blue-600 mb-6" />
        <h2 className="text-2xl font-bold text-gray-800 mb-2">Đăng nhập</h2>
        <p className="text-gray-500 mb-6 text-center">Chào mừng bạn đến với hệ thống quản lý trường học</p>
        {loginLoading ? (
          <Loading />
        ) : (
          <Form
            name="basic"
            layout="vertical"
            initialValues={{ remember: true }}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete="off"
            className="w-full flex flex-col"
          >
            <Form.Item<FieldType>
              label={<span className="text-sm font-medium text-gray-700">Mã Nhân Sự</span>}
              name="maNhanSu"
              rules={[{ required: true, message: 'Vui lòng nhập Mã nhân sự của bạn!' }]}
            >
              <Input className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500" placeholder="Nhập email" />
            </Form.Item>

            <Form.Item<FieldType>
              label={<span className="text-sm font-medium text-gray-700">Mật khẩu</span>}
              name="password"
              rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}
            >
              <Input.Password className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500" placeholder="Nhập mật khẩu" />
            </Form.Item>

            <Form.Item<FieldType> name="remember" valuePropName="checked" label={null}>
              <Checkbox>Ghi nhớ đăng nhập</Checkbox>
            </Form.Item>

            <Form.Item label={null}>
              <Button type="primary" htmlType="submit" className="w-full py-2 bg-blue-600 text-white font-semibold rounded-lg hover:bg-blue-700 transition">
                Đăng nhập
              </Button>
            </Form.Item>
          </Form>
        )}
        <div className="mt-6 text-sm text-gray-500 text-center">
          <span>Bạn cần hỗ trợ? Liên hệ ngay </span>
          <a href="#" className="text-blue-600 hover:underline">19001009</a>
        </div>
      </div>
      <footer className="relative z-10 mt-8 text-xs text-gray-400 text-center">
        &copy; {new Date().getFullYear()} D.University. All rights reserved.
      </footer>
    </div>
  );
}

export default Index;
