'use client';

import { useAppSelector } from '@redux/hooks';

const Home = () => {
  const { user } = useAppSelector((state) => state.authState);
  return (
    <div>
      Home page
      <div>Xin chào {user?.fullName}</div>
    </div>
  );
};

export default Home;
