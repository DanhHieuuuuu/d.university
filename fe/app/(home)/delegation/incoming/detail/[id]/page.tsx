'use client';
import { Card, Tabs, Empty } from 'antd';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { useParams, useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import { useAppDispatch } from '@redux/hooks';
import {
  getByIdDetailDelegation,
  getByIdGuestGroup,
  getByIdReceptionTime
} from '@redux/feature/delegation/delegationThunk';
import { DelegationIncomingTab, DetailGuestGroupTab, ReceptionTimeTab } from './(tab)';
import { IDetailDelegationIncoming, IReceptionTime, IViewGuestGroup } from '@models/delegation/delegation.model';

export default function DetailDoanVaoPage() {
  const { id } = useParams();
  const router = useRouter();
  const dispatch = useAppDispatch();
  const [activeKey, setActiveKey] = useState('delegation');
  const [delegation, setDelegation] = useState<IViewGuestGroup | null>(null);
  const [detailDelegation, setDetailDelegation] = useState<IDetailDelegationIncoming | null>(null);
  const [receptionTime, setReceptionTime] = useState<IReceptionTime | null>(null);

  useEffect(() => {
    if (id && activeKey === 'delegation' && !delegation) {
      dispatch(getByIdGuestGroup(Number(id)))
        .unwrap()
        .then((res) => setDelegation(res));
    }
  }, [id, activeKey, delegation, dispatch]);

  const handleTabChange = (key: string) => {
    setActiveKey(key);
    if (key === 'staffReception' && !detailDelegation) {
      dispatch(getByIdDetailDelegation(Number(id)))
        .unwrap()
        .then((res) => setDetailDelegation(res));
    } else if (key === 'receptionTime' && !receptionTime) {
      dispatch(getByIdReceptionTime(Number(id)))
        .unwrap()
        .then((res) => setReceptionTime(res));
    }
  };

  const tabItems = [
    {
      key: 'delegation',
      label: 'Thông tin đoàn vào',
      children: delegation ? (
        <DelegationIncomingTab data={delegation} />
      ) : (
        <Empty description="Không có dữ liệu đoàn vào" />
      )
    },
    {
      key: 'staffReception',
      label: 'Thông tin nhân sự tiếp đoàn',
      children: detailDelegation ? (
        <DetailGuestGroupTab data={detailDelegation} />
      ) : (
        <Empty description="Không có dữ liệu nhân sự tiếp đoàn" />
      )
    },
    {
      key: 'receptionTime',
      label: 'Thông tin thời gian tiếp đoàn',
      children: receptionTime ? (
        <ReceptionTimeTab data={receptionTime} />
      ) : (
        <Empty description="Không có dữ liệu thời gian tiếp đoàn" />
      )
    }
  ];

  return (
    <Card
      bordered={false}
      className="h-full"
      title={
        <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
          <div
            onClick={() => router.back()}
            style={{
              display: 'inline-flex',
              alignItems: 'center',
              justifyContent: 'center',
              width: 34,
              height: 34,
              borderRadius: 6,
              cursor: 'pointer',
              transition: '0.2s'
            }}
            className="hover-box"
          >
            <ArrowLeftOutlined style={{ fontSize: 16 }} />
          </div>
          <span>Chi tiết đoàn vào</span>
        </div>
      }
    >
      <Tabs type="card" items={tabItems} activeKey={activeKey} onChange={handleTabChange} />
    </Card>
  );
}
