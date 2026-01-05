'use client';
import { Card, Tabs, Empty, Button } from 'antd';
import { ArrowLeftOutlined, EditOutlined, PlusOutlined } from '@ant-design/icons';
import { useParams, useRouter } from 'next/navigation';
import { useEffect, useRef, useState } from 'react';
import { useAppDispatch } from '@redux/hooks';
import {
  getByIdDetailDelegation,
  getByIdGuestGroup,
  getByIdReceptionTime
} from '@redux/feature/delegation/delegationThunk';
import { DelegationIncomingTab, DetailGuestGroupTab, ReceptionTimeTab } from './(tab)';
import { IDetailDelegationIncoming, IReceptionTime, IViewGuestGroup } from '@models/delegation/delegation.model';
import type { FormInstance } from 'antd';
import { toast } from 'react-toastify';
export default function DetailDoanVaoPage() {
  const { id } = useParams();
  const router = useRouter();
  const dispatch = useAppDispatch();
  const [activeKey, setActiveKey] = useState('delegation');
  const [delegation, setDelegation] = useState<IViewGuestGroup | null>(null);
  const [detailDelegation, setDetailDelegation] = useState<IDetailDelegationIncoming | null>(null);
  const [receptionTime, setReceptionTime] = useState<IReceptionTime[]>([]);
  const [isEdit, setIsEdit] = useState(false);
  const delegationFormRef = useRef<FormInstance>(null);
  const staffFormRef = useRef<FormInstance>(null);
  const receptionTimeFormRef = useRef<FormInstance>(null);

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
    } else if (key === 'receptionTime' && receptionTime.length === 0) {
      dispatch(getByIdReceptionTime(Number(id)))
        .unwrap()
        .then((res) => setReceptionTime(res));
    }
  };
  const onClickUpdate = () => {
    setIsEdit(true);
  };
  const onClickCancel = () => {
    setIsEdit(false);
  };

  const onClickSave = async () => {
    try {
      await Promise.all([
        delegationFormRef.current?.submit(),
        staffFormRef.current?.submit(),
        receptionTimeFormRef.current?.submit()
      ]);
      toast.success('Cập nhật thành công');
      setIsEdit(false);
    } catch (err) {
      toast.error(String(err));
    }
  };
  const reloadDelegation = async () => {
    if (!id) return;
    const res = await dispatch(getByIdGuestGroup(Number(id))).unwrap();
    setDelegation(res);
  };
  const reloadReceptionTime = async () => {
    if (!id) return;
    const res = await dispatch(getByIdReceptionTime(Number(id))).unwrap();
    setReceptionTime(res);
  };

  const tabItems = [
    {
      key: 'delegation',
      label: 'Thông tin đoàn vào',
      children: delegation ? (
        <DelegationIncomingTab data={delegation} isEdit={isEdit} ref={delegationFormRef} onUpdated={reloadDelegation} />
      ) : (
        <Empty description="Không có dữ liệu đoàn vào" />
      )
    },
    {
      key: 'staffReception',
      label: 'Thông tin nhân sự tiếp đoàn',
      children: detailDelegation ? (
        <DetailGuestGroupTab data={detailDelegation} isEdit={isEdit} />
      ) : (
        <Empty description="Không có dữ liệu nhân sự tiếp đoàn" />
      )
    },
    {
      key: 'receptionTime',
      label: 'Thông tin thời gian tiếp đoàn',
      children:
        receptionTime.length > 0 ? (
          <ReceptionTimeTab
            ref={receptionTimeFormRef}
            data={receptionTime}
            isEdit={isEdit}
            onUpdated={reloadReceptionTime}
          />
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
      extra={
        !isEdit ? (
          <Button type="primary" icon={<EditOutlined />} onClick={onClickUpdate}>
            Chỉnh sửa
          </Button>
        ) : (
          <div style={{ display: 'flex', gap: 8 }}>
            <Button onClick={onClickCancel}>Huỷ</Button>
            <Button type="primary" onClick={onClickSave}>
              Lưu
            </Button>
          </div>
        )
      }
      bodyStyle={{ maxHeight: '90%', overflow: 'auto' }}
    >
      <Tabs type="card" items={tabItems} activeKey={activeKey} onChange={handleTabChange} />
    </Card>
  );
}
