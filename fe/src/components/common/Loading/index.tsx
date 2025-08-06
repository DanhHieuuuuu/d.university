import { Spin } from 'antd';

function Loading() {
  return (
    <div className="flex justify-center pt-20">
      <Spin size="large" />
    </div>
  );
}

export default Loading;
