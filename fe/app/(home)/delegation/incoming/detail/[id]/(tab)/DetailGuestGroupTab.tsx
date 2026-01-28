'use client';

import React, { useMemo } from 'react';
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table';
import { Divider, Typography } from 'antd';
import { IDetailDelegationIncoming } from '@models/delegation/delegation.model';

type DetailGuestGroupTabProps = {
  data: IDetailDelegationIncoming | null;
   isEdit?: boolean; 

};

const DetailGuestGroupTab: React.FC<DetailGuestGroupTabProps> = ({ data }) => {
  if (!data) return null;

  return (
    <>
      {/* ===== THÔNG TIN ĐOÀN ===== */}
      <DetailTable
        rows={[
          { label: 'Mã đoàn', value: data.delegationCode },
          { label: 'Tên đoàn', value: data.delegationName },
          { label: 'Tổng số thành viên', value: data.members?.length ?? 0 }

        ]}
      />

      <Divider />

      {/* ===== DANH SÁCH THÀNH VIÊN ===== */}
      <Typography.Title level={5}>Danh sách thành viên</Typography.Title>

      {data.members.map((member) => {
        const rows: DetailRow[] = [
          { label: 'Mã thành viên', value: member.code },
          { label: 'Họ', value: member.firstName },
          { label: 'Tên', value: member.lastName },
          { label: 'Năm sinh', value: member.yearOfBirth },
          { label: 'SĐT', value: member.phoneNumber },
          { label: 'Email', value: member.email },
          { label: 'Trưởng đoàn', value: member.isLeader ? 'Có' : 'Không' }
        ];

        return (
          <div key={member.id} style={{ marginBottom: 24 }}>
            <DetailTable rows={rows} />
            <Divider />
          </div>
        );
      })}

      {/* ===== PHÒNG BAN HỖ TRỢ ===== */}
      <Typography.Title level={5}>Phòng ban hỗ trợ</Typography.Title>

      {data.departmentSupports?.map((dept) => (
        <div key={dept.departmentSupportId} style={{ marginTop: 16 }}>
          <Typography.Text strong>
            {dept.departmentSupportName}
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
