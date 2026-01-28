'use client';

import React, { useMemo } from 'react';
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table';
import { IDetailDelegationIncoming } from '@models/delegation/delegation.model';
import { Divider, Typography } from 'antd';

type DetailGuestGroupTabProps = {
  data: IDetailDelegationIncoming | null;
  isEdit?: boolean;
};

const DetailGuestGroupTab: React.FC<DetailGuestGroupTabProps> = ({ data }) => {
  const rows: DetailRow[] = useMemo(() => {
    if (!data) return [];

    return [
      { label: 'Mã thành viên', value: data.code },
      { label: 'Họ', value: data.firstName },
      { label: 'Tên', value: data.lastName },
      { label: 'Năm sinh', value: data.yearOfBirth },
      { label: 'SĐT', value: data.phoneNumber },
      { label: 'Email', value: data.email },
      { label: 'Trưởng đoàn', value: data.isLeader ? 'Có' : 'Không' }
    ];
  }, [data]);

  return (
    <>
      <DetailTable rows={rows} />
      {data?.departmentSupports?.map((dept) => (
        <div key={dept.departmentSupportId} style={{ marginTop: 16 }}>
          <Divider />
          <Typography.Text strong>
            Phòng ban hỗ trợ: {dept.departmentSupportName}
          </Typography.Text>

          {dept.supporters?.length ? (
            <ul style={{ marginTop: 8, paddingLeft: 20 }}>
              {dept.supporters.map((sp) => (
                <li key={sp.id}>
                  {sp.supporterName} ({sp.supporterCode})
                </li>
              ))}
            </ul>
          ) : (
            <Typography.Text type="secondary">
              Không có nhân sự hỗ trợ
            </Typography.Text>
          )}
        </div>
      ))}
    </>
  );
};

export default DetailGuestGroupTab;
