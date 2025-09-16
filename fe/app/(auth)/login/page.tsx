'use client';

import { useRouter } from 'next/navigation';
import { Button, Checkbox, Form, FormProps, Input } from 'antd';
import Loading from '@components/common/Loading';

import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { login } from '@redux/feature/authSlice';

import { userStatusE } from '@models/common';
import { ILogin } from '@models/auth/auth.model';
import { processApiMsgError } from '@utils/index';

type FieldType = {
  username?: string;
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
      username: values.username!,
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
    <div className="flex h-[100vh] flex-col items-center justify-center">
      {loginLoading ? (
        <Loading />
      ) : (
        <Form
          name="basic"
          labelCol={{ span: 8 }}
          wrapperCol={{ span: 16 }}
          style={{ maxWidth: 600, minWidth: 400 }}
          initialValues={{ remember: true }}
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          autoComplete="off"
        >
          <Form.Item<FieldType>
            label="Username"
            name="username"
            rules={[{ required: true, message: 'Please input your username!' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item<FieldType>
            label="Password"
            name="password"
            rules={[{ required: true, message: 'Please input your password!' }]}
          >
            <Input.Password />
          </Form.Item>

          <Form.Item<FieldType> name="remember" valuePropName="checked" label={null}>
            <Checkbox>Remember me</Checkbox>
          </Form.Item>

          <Form.Item label={null}>
            <Button type="primary" htmlType="submit">
              Submit
            </Button>
          </Form.Item>
        </Form>
      )}
    </div>
  );
}

export default Index;
