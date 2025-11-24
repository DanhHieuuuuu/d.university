'use client';

import { BellIcon } from '@components/custom-icon';
import { Badge, Button, Popover } from 'antd';
import { useAppSelector } from '@redux/hooks';
import { useNavigate } from '@hooks/navigate';

import SmallNotificationItem from './items/small-item';
import '@styles/notice.style.scss';

const NotificationComponent = () => {
  const { navigateTo } = useNavigate();
  const { data, totalUnread } = useAppSelector((state) => state.noticeState.$list);

  const handleNavigate = () => {
    navigateTo('/notification/list');
  };

  var content = (
    <div className="notification-popup-content">
      <div className="notification-header">
        <p>Thông báo</p>
        <span>Bạn đang có {totalUnread} thông báo</span>
      </div>
      <div className="notification-body">
        {data.map((noti, idx) => (
          <SmallNotificationItem key={noti.id} item={noti} />
        ))}
      </div>
      <div className="notification-footer" onClick={handleNavigate}>
        <p>Xem tất cả</p>
      </div>
    </div>
  );

  return (
    <Popover content={content} placement="bottomRight" trigger={['click']}>
      <div className="btn-notification">
        <Badge text="" showZero count={totalUnread} overflowCount={9} offset={[-4, 8]}>
          <Button type="text" shape="circle" size="large" icon={<BellIcon style={{ color: 'white' }} />} />
        </Badge>
      </div>
    </Popover>
  );
};

export default NotificationComponent;
