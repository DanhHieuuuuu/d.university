'use client'; 
    
import React, { useMemo } from 'react'; 
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table'; 
import { IDetailDelegationIncoming } from '@models/delegation/delegation.model'; 
 
type DetailGuestGroupTabProps = { 
  data: IDetailDelegationIncoming | null; 
  isEdit?: boolean;
}; 
 
const DetailGuestGroupTab: React.FC<DetailGuestGroupTabProps> = ({ data }) => { 
  // Cột
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
 
  return <DetailTable rows={rows} />; 
}; 
 
export default DetailGuestGroupTab; 
