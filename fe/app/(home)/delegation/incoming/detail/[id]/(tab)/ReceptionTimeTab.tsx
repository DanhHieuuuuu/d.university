'use client';

import React, { useMemo, forwardRef, useImperativeHandle } from 'react';
import dayjs from 'dayjs';
import { Form, Input, InputNumber, DatePicker, TimePicker } from 'antd';
import type { FormInstance } from 'antd';
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table';
import { IReceptionTime } from '@models/delegation/delegation.model';
import { renderField } from '@utils/render-field.helper';
import { toast } from 'react-toastify';
import { updateReceptionTime } from '@redux/feature/delegation/delegationThunk';
import { useAppDispatch } from '@redux/hooks';

type ReceptionTimeTabProps = {
  data: IReceptionTime | null;
  isEdit?: boolean;
  onUpdated?: () => void;
};

const ReceptionTimeTab = forwardRef<FormInstance, ReceptionTimeTabProps>(({ data, isEdit = false, onUpdated }, ref) => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  useImperativeHandle(ref, () => form);

  const onFinish = async (values: any) => {
    try {
      const payload = {
        ...values,
        delegationIncomingId: data?.delegationIncomingId,
        date: values.date ? dayjs(values.date).format('YYYY-MM-DD') : null,
        startDate: values.startDate ? dayjs(values.startDate).format('HH:mm:ss') : null,
        endDate: values.endDate ? dayjs(values.endDate).format('HH:mm:ss') : null
      };

      await dispatch(updateReceptionTime(payload)).unwrap();
      onUpdated?.();
    } catch (err) {
    }
  };

  const rows: DetailRow[] = useMemo(() => {
    if (!data) return [];
    const options = { isEdit };

    return [
      {
        label: 'Mã đoàn',
        value: renderField('delegationCode', data.delegationCode, <Input disabled />, options)
      },
      {
        label: 'Tên đoàn vào',
        value: renderField('delegationName', data.delegationName, <Input disabled />, options)
      },
      {
        label: 'Ngày tiếp đón',
        value: renderField(
          'date',
          data.date ? dayjs(data.date) : undefined,
          <DatePicker style={{ width: '100%' }} />,
           { ...options, displayType: 'date' } 
          
        )
      },
      {
        label: 'Thời gian bắt đầu',
        value: renderField(
          'startDate',
          data.startDate ? dayjs(data.startDate, 'HH:mm:ss') : undefined,
          <TimePicker format="HH:mm" style={{ width: '100%' }} disabled={!isEdit} />,
         { isEdit, displayType: 'time' }
        )
      },
      {
        label: 'Thời gian kết thúc',
        value: renderField(
          'endDate',
          data.endDate ? dayjs(data.endDate, 'HH:mm:ss') : undefined,
          <TimePicker format="HH:mm" style={{ width: '100%' }} disabled={!isEdit} />,
          { isEdit, displayType: 'time' }
        )
      },

      {
        label: 'Địa điểm tiếp đón',
        value: renderField('address', data.address, <Input />, options)
      },
      {
        label: 'Nội dung',
        value: renderField('content', data.content, <Input />, options),
        full: true
      },
      {
        label: 'Tổng số người',
        value: renderField('totalPerson', data.totalPerson, <InputNumber style={{ width: '100%' }} />, options)
      }
    ];
  }, [data, isEdit]);

  return (
    <Form form={form} layout="vertical" onFinish={onFinish}>
      <DetailTable rows={rows} />
    </Form>
  );
});

ReceptionTimeTab.displayName = 'ReceptionTimeTab';

export default ReceptionTimeTab;
