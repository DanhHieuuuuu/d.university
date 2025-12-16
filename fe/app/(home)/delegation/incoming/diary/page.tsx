'use client';

import { useEffect } from 'react';
import { Card } from 'antd';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getLogStatus } from '@redux/feature/delegation/delegationThunk';
import { ILogStatus } from '@models/delegation/delegation.model';
import { formatDateTimeView, formatDateView } from '@utils/index';
import { withAuthGuard } from '@src/hoc/withAuthGuard';
import { PermissionCoreConst } from '@/constants/permissionWeb/PermissionCore';
import { DelegationStatusConst } from '../../consts/delegation-status.consts';
import { getStatusClass, getStatusName } from '@utils/status.helper';

const Page = () => {
  const dispatch = useAppDispatch();
  const { listLogStatus, status } = useAppSelector((state) => state.delegationState);

  useEffect(() => {
    dispatch(getLogStatus());
  }, [dispatch]);

  return (
    <Card title="Nhật ký đoàn vào" className="h-full">
      {status === 'loading' && <p>Đang tải dữ liệu...</p>}

      {listLogStatus && listLogStatus.length === 0 && <p>Không có dữ liệu.</p>}

      <div className="log-list">
        {listLogStatus &&
          listLogStatus.map((log: ILogStatus, index: number) => (
            <div key={index} className="log-item">
              <div className='log-head'>
                <p style={{ fontSize: '18px', fontWeight: 'bold' }}>
                   {formatDateTimeView(log.createdDate)}
                </p>
                <p>                 
                  <span className={getStatusClass(log.newStatus)}>{getStatusName(log.newStatus)}</span>
                </p>
              </div>

              <p>
                <strong>Mô tả:</strong> {log.description || '----'}
              </p>
              <p>
                <strong>Lý do:</strong> {log.reason || '---- '}
              </p>
              <p>
                <strong>Id người tạo:</strong> {log.createdBy}
              </p>
            </div>
          ))}
      </div>

      <style jsx>{`
        .log-list {
          display: flex;
          flex-direction: column;
          gap: 12px;
        }
        .log-item {
          padding: 12px;
          border: 1px solid #e8e8e8;
          border-radius: 6px;
          background-color: #fafafa;
        }
        .log-item p {
          margin: 4px 0;
        }
        .log-head{
          display:flex;
          justify-content: space-between;
         
        }
          
      `}</style>
    </Card>
  );
};

export default withAuthGuard(Page, PermissionCoreConst.CoreMenuDelegation);
