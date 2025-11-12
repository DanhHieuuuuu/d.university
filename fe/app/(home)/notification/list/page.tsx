'use client';

import { useEffect, useState } from 'react';
import { Button, Card, Popover } from 'antd';
import { CheckOutlined, EllipsisOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';

import NotificationItem from '@components/common/Notification/items/large-item';
import { $fetchNotification, $markAllAsRead } from '@redux/feature/noticeSlice';
import { colors } from '@styles/colors';

const Page = () => {
  const dispatch = useAppDispatch();
  const { data, totalUnread } = useAppSelector((state) => state.noticeState.$list);
  const [openPopup, setOpenPopup] = useState<boolean>(false);

  useEffect(() => {
    dispatch($fetchNotification({ SkipCount: 0, MaxResultCount: 10 }));
  }, []);

  const handleMarkAllAsRead = async () => {
    if (totalUnread > 0) {
      await dispatch($markAllAsRead());
    }
    setOpenPopup(false);
  };

  return (
    <Card
      title="Thông báo"
      className="h-fit"
      extra={
        <Popover
          open={openPopup}
          trigger={['click']}
          onOpenChange={(val) => setOpenPopup(val)}
          placement="bottom"
          content={
            <div>
              <Button type="text" icon={<CheckOutlined />} onClick={handleMarkAllAsRead}>
                Đánh dấu tất cả đã đọc
              </Button>
            </div>
          }
        >
          <Button type="text" icon={<EllipsisOutlined />}></Button>
        </Popover>
      }
    >
      <div className="notification-page">
        <div className='notification-list'>
          {data.map((noti) => (
            <NotificationItem key={noti.id} item={noti} />
          ))}
        </div>
        <div className='text-center mt-4'>
          <Button type='text' style={{color: colors.primary, fontWeight: 500}}>Xem thêm</Button>
        </div>
      </div>
    </Card>
  );
};

export default Page;
