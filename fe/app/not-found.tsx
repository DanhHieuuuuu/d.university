import { Button } from 'antd';
import Title from 'antd/es/typography/Title';

export default function NotFound() {
  return (
    <div className="flex h-[100vh] flex-col items-center justify-center gap-8">
      <Title level={1}>404</Title>
      <p>Không tìm thấy tài nguyên</p>
      <Button type="primary" color="primary" href="/home">
        Quay lại Trang chủ
      </Button>
    </div>
  );
}
