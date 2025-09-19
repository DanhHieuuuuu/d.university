import { Button } from 'antd';
import Title from 'antd/es/typography/Title';

export default function NotFound() {
  return (
    <div className="flex h-[100vh] flex-col items-center justify-center gap-8">
      <Title level={1}>Not Found</Title>
      <p>Could not find requested resource</p>
      <Button type="primary" color="primary" href="/home">
        Return Home
      </Button>
    </div>
  );
}
