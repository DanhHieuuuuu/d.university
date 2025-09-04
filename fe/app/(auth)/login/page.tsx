'use client';

import { Button, Checkbox, Form, FormProps, Input } from 'antd';
import Image from 'next/image';
import { useRouter } from 'next/navigation';

type FieldType = {
  username?: string;
  password?: string;
  remember?: string;
};

const onFinishFailed: FormProps<FieldType>['onFinishFailed'] = (errorInfo) => {
  console.log('Failed:', errorInfo);
};

function Index() {
  const router = useRouter();

  const onFinish: FormProps<FieldType>['onFinish'] = async (values) => {
    if (values.remember) {
      await localStorage.setItem('accessToken', 'abc');
    }
    router.push('/home');
    console.log('Success:', values);
  };

  return (
    <div className="flex flex-col items-center justify-center h-[100vh]">
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
    </div>
  );
}

export default Index;
