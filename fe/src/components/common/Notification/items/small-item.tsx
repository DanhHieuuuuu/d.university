'use client';

import { formatDate } from '@utils/index';
import { IViewNotification } from '@models/notice/notification.model';
import { useAppDispatch } from '@redux/hooks';
import { $markAsRead } from '@redux/feature/noticeSlice';

interface SmallNotificationItemProps {
  item: IViewNotification;
}

const SmallNotificationItem: React.FC<SmallNotificationItemProps> = (props) => {
  const { item } = props;
  const dispatch = useAppDispatch();

  const formatedDate = formatDate(item.createdAt, 'HH:mm dd/MM/yyyy');

  const handleOnClick = () => {
    if (!item.isRead) {
      dispatch($markAsRead(item.id));
    }
    return;
  };

  return (
    <div
      className={`notification-component-item small ${item.isRead ? 'read' : 'unread'}`}
      onClick={!item.isRead ? handleOnClick : undefined}
    >
      <div className="notification-content">
        <p className="notification-title">{item.title}</p>
        <span className="notification-timestamp">{formatedDate}</span>
      </div>
      {!item.isRead && <div className="dot" />}
    </div>
  );
};

export default SmallNotificationItem;
