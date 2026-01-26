'use client';

import { useEffect, useRef } from 'react';
import { message } from 'antd';
import { useAppDispatch } from '@redux/hooks';
import { $fetchNotification } from '@redux/feature/noticeSlice';
import connection from '@services/delegation/notificationHub.service';
import { HubConnectionState } from '@microsoft/signalr';

export default function NotificationRealtime() {
  const dispatch = useAppDispatch();
  const startedRef = useRef(false);

  useEffect(() => {
    async function start() {
      try {
        console.log('Current connection state:', connection.state);

        // Đăng ký event ReceiveNotification
        // Trước hết xóa hết các listener cũ để tránh bị double event
        connection.off('ReceiveNotification');
        connection.on('ReceiveNotification', (data) => {
          console.log('New notification:', data);
          message.info(data.title || 'Bạn có thông báo mới');
          // Gọi redux action để fetch lại danh sách notification
          dispatch($fetchNotification({ PageIndex: 0, PageSize: 10 }));
        });

        // Chỉ start khi Disconnected
        if (connection.state === HubConnectionState.Disconnected && !startedRef.current) {
          startedRef.current = true;

          await connection.start();
          console.log('SignalR connected');
        }
      } catch (err) {
        startedRef.current = false;
        console.error('SignalR error:', err);
      }
    }

    start();

    return () => {
      connection.off('ReceiveNotification');
    };
  }, [dispatch]);

  return null;
}
