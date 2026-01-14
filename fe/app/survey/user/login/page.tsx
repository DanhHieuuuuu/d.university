'use client';

import { Button, Checkbox, Form, FormProps, Input, Spin, message, Tabs } from 'antd';
import { colors } from '@styles/colors';
import { toast } from 'react-toastify';
import { GraduationCap, Users } from 'lucide-react';
import React from 'react';
import { useRouter } from 'next/navigation';

import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { login } from '@redux/feature/auth/authThunk';
import { sinhVienLogin } from '@redux/feature/student/studentThunk';
import { ILogin } from '@models/auth/auth.model';
import { ISinhVienLogin } from '@models/auth/sinhvien.model';
import { processApiMsgError } from '@utils/index';

type LoginFormData = {
  username: string;
  password: string;
  remember: boolean;
};

function SurveyLoginPage() {
  const router = useRouter();
  const dispatch = useAppDispatch();
  const [activeTab, setActiveTab] = React.useState<string>('student');
  const [loginLoading, setLoginLoading] = React.useState(false);
  const [messageApi, contextHolder] = message.useMessage();
  const [staffForm] = Form.useForm();
  const [studentForm] = Form.useForm();

  // Handle staff login
  const onStaffLogin: FormProps<LoginFormData>['onFinish'] = async (values) => {
    const body: ILogin = {
      maNhanSu: values.username!,
      password: values.password!,
      remember: values.remember || false
    };

    setLoginLoading(true);
    try {
      const data: any = await dispatch(login(body)).unwrap();
      console.log('result login', data);

      if (data.status == 1) {
        toast.success('Đăng nhập thành công');
        router.push('/survey/user/mysurvey');
      } else {
        toast.error(data.message || 'Đăng nhập thất bại');
      }
    } catch (err) {
      processApiMsgError(err, 'Đăng nhập thất bại, vui lòng thử lại.');
    } finally {
      setLoginLoading(false);
    }
  };

  // Handle student login
  const onStudentLogin: FormProps<LoginFormData>['onFinish'] = async (values) => {
    const body: ISinhVienLogin = {
      mssv: values.username!,
      password: values.password!,
      remember: values.remember || false
    };

    setLoginLoading(true);
    try {
      const data: any = await dispatch(sinhVienLogin(body)).unwrap();
      console.log('result login', data);

      if (data.status == 1) {
        toast.success('Đăng nhập thành công');
        router.push('/survey/user/mysurvey');
      } else {
        toast.error(data.message || 'Đăng nhập thất bại');
      }
    } catch (err) {
      processApiMsgError(err, 'Đăng nhập thất bại, vui lòng thử lại.');
    } finally {
      setLoginLoading(false);
    }
  };

  const renderLoginForm = (
    userType: 'staff' | 'student',
    form: any,
    onFinish: FormProps<LoginFormData>['onFinish']
  ) => {
    const isStudent = userType === 'student';
    const usernameLabel = isStudent ? 'Mã số sinh viên' : 'Mã nhân sự';
    const usernamePlaceholder = isStudent ? 'Nhập mã số sinh viên' : 'Nhập mã nhân sự';

    return (
      <Form
        form={form}
        name={`${userType}-login-form`}
        layout="vertical"
        initialValues={{ remember: true }}
        onFinish={onFinish}
        autoComplete="off"
        className="flex w-full flex-col"
      >
        <Form.Item<LoginFormData>
          label={<span className="text-sm font-medium text-gray-700">{usernameLabel}</span>}
          name="username"
          rules={[{ required: true, message: `Vui lòng nhập ${usernameLabel.toLowerCase()}!` }]}
        >
          <Input
            className="w-full rounded-lg border px-4 py-2 focus:outline-none focus:ring-2"
            style={{ borderColor: colors.gray, boxShadow: `0 0 0 2px ${colors.primaryLight}33` }}
            placeholder={usernamePlaceholder}
          />
        </Form.Item>

        <Form.Item<LoginFormData>
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

        <div className="mb-4 flex items-center justify-between">
          <Form.Item<LoginFormData> name="remember" valuePropName="checked" label={null} className="mb-0">
            <Checkbox>Ghi nhớ đăng nhập</Checkbox>
          </Form.Item>
        </div>

        <Form.Item label={null}>
          <Button
            type="primary"
            htmlType="submit"
            loading={loginLoading}
            className="w-full rounded-lg py-2 font-semibold text-white transition"
            style={{ backgroundColor: colors.primary, borderColor: colors.primaryNavy }}
          >
            Đăng nhập
          </Button>
        </Form.Item>
      </Form>
    );
  };

  return (
    <>
      {contextHolder}
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
            Đăng nhập Hệ thống Khảo sát
          </h2>
          <p className="mb-6 text-center" style={{ color: colors.gray }}>
            Chào mừng bạn đến với hệ thống khảo sát
          </p>

          {loginLoading ? (
            <div className="p-24">
              <Spin size="large" />
            </div>
          ) : (
            <div className="w-full">
              <Tabs
                activeKey={activeTab}
                onChange={setActiveTab}
                centered
                items={[
                  {
                    key: 'student',
                    label: (
                      <span className="flex items-center gap-2">
                        <GraduationCap size={16} />
                        Sinh viên
                      </span>
                    ),
                    children: renderLoginForm('student', studentForm, onStudentLogin)
                  },
                  {
                    key: 'staff',
                    label: (
                      <span className="flex items-center gap-2">
                        <Users size={16} />
                        Giảng viên
                      </span>
                    ),
                    children: renderLoginForm('staff', staffForm, onStaffLogin)
                  }
                ]}
              />
            </div>
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
    </>
  );
}

export default SurveyLoginPage;
