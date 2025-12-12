'use client';

import React, { useMemo } from 'react';
import dayjs from 'dayjs';
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table';

type DelegationIncomingTabProps = {
  data: any;
};

const DelegationIncomingTab: React.FC<DelegationIncomingTabProps> = ({ data }) => {
  const rows: DetailRow[] = useMemo(() => {
    if (!data) return [];

    return [
      { label: 'Mã đoàn', value: data.code },
      { label: 'Tên đoàn vào', value: data.name },
      { label: 'Nội dung', value: data.content, full: true },
      { label: 'Phòng ban phụ trách', value: data.phongBan },
      { label: 'Địa điểm', value: data.location },
      { label: 'Nhân sự tiếp đón', value: data.staffReceptionName },
      { label: 'Tổng chi phí', value: data.totalMoney },
      { label: 'Tổng số người', value: data.totalPerson },
      { label: 'SĐT liên hệ', value: data.phoneNumber },
      {
        label: 'Ngày yêu cầu',
        value: data.requestDate ? dayjs(data.requestDate).format('DD/MM/YYYY') : ''
      },
      {
        label: 'Ngày tiếp đón',
        value: data.receptionDate ? dayjs(data.receptionDate).format('DD/MM/YYYY') : ''
      }
    ];
  }, [data]);

  return <DetailTable rows={rows} />;
};

export default DelegationIncomingTab;
