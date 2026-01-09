'use client';

import { useEffect } from 'react';
import { message } from 'antd';
import { useAppDispatch } from '@redux/hooks';
import { $fetchNotification } from '@redux/feature/noticeSlice';
import connection from '@services/delegation/notificationHub.service';
import * as signalR from '@microsoft/signalr';

export default function NotificationRealtime() {
     console.log('🔥 NotificationRealtime mounted');
  const dispatch = useAppDispatch();

useEffect(() => {
  async function start() {
    try {
      debugger
      console.log('Current connection state:', connection.state);

      // Luôn đăng ký event, để chắc chắn nhận thông báo
      connection.off('ReceiveNotification'); // clear trước để tránh trùng
      connection.on('ReceiveNotification', (data) => {
        console.log('New notification:', data);
        message.info(data.title || 'Bạn có thông báo mới');
        dispatch($fetchNotification({ PageIndex: 0, PageSize: 10 }));
      });

      if (connection.state !== signalR.HubConnectionState.Connected) {
        await connection.start();
        console.log('SignalR connected');
      }
    } catch (err) {
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
