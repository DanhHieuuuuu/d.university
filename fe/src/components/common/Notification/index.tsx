'use client';

import { BellIcon } from '@components/custom-icon';
import { Badge, Button, ConfigProvider, Popover } from 'antd';
import { useAppSelector } from '@redux/hooks';

import SmallNotificationItem from './items/small-item';
import '@styles/notice.style.scss';

const NotificationComponent = () => {
  const { data, totalUnread } = useAppSelector((state) => state.noticeState.$list);
  var content = (
    <div className='notification-popup-content'>
      <div className="notification-header">
        <p>Thông báo</p>
        <span>Bạn đang có {totalUnread} thông báo</span>
      </div>
      <div className="notification-body">
        {data.map((noti, idx) => (
          <SmallNotificationItem key={noti.id} item={noti} />
        ))}
      </div>
      <div className='notification-footer'>
        <p>Xem tất cả</p>
      </div>
    </div>
  );

  return (
    
      <Popover content={content} placement="bottomRight" trigger={['click']}>
        <div className="btn-notification">
          <Badge text="" showZero count={totalUnread} overflowCount={9} offset={[-4, 8]}>
            <Button type="text" shape="circle" size="large" icon={<BellIcon />} />
          </Badge>
        </div>
      </Popover>
  );
};

export default NotificationComponent;
