'use client';

import React, { useMemo } from 'react';
import dayjs from 'dayjs';
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table';
import { IReceptionTime } from '@models/delegation/delegation.model';

type ReceptionTimeTabProps = {
  data: IReceptionTime | null;
};

const ReceptionTimeTab: React.FC<ReceptionTimeTabProps> = ({ data }) => {
  const rows: DetailRow[] = useMemo(() => {
    if (!data) return [];

    return [
      { label: 'Mã đoàn', value: data.delegationCode || '' },
      { label: 'Tên đoàn vào', value: data.delegationName || '' },
      { label: 'Ngày tiếp đón', value: data.date ? dayjs(data.date).format('DD/MM/YYYY') : '' },
      { label: 'Thời gian bắt đầu', value: data.startDate ? dayjs(data.startDate, 'HH:mm:ss').format('HH:mm') : '' },
      { label: 'Thời gian kết thúc', value: data.endDate ? dayjs(data.endDate, 'HH:mm:ss').format('HH:mm') : '' },
      { label: 'Địa điểm tiếp đón', value: data.address || '' },
      { label: 'Nội dung', value: data.content || '', full: true },
      { label: 'Tổng số người', value: data.totalPerson || 0 }
    ];
  }, [data]);

  return <DetailTable rows={rows} />;
};

export default ReceptionTimeTab;
