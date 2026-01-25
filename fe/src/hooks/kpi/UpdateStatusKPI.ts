import { Modal } from 'antd';
import { toast } from 'react-toastify';
import { useAppDispatch } from '@redux/hooks';
import { AsyncThunkAction } from '@reduxjs/toolkit';

type Config = {
  validStatus: number[];
  invalidMsg: string;
  confirmTitle?: string;
  confirmMessage?: string;
  successMsg: string;
  nextStatus: number;
  updateAction: (payload: { ids: number[]; trangThai: number }) => AsyncThunkAction<any, any, any>;
  afterSuccess?: () => void;
};

export function useKpiStatusAction() {
  const dispatch = useAppDispatch();

  const processUpdateStatus = async (selectedIds: number[], list: any[], config: Config) => {
    if (!selectedIds.length) {
      toast.warning('Vui lòng chọn ít nhất một KPI');
      return;
    }

    const validItems = list.filter((kpi) => selectedIds.includes(kpi.id) && config.validStatus.includes(kpi.trangThai));

    if (!validItems.length) {
      toast.error(config.invalidMsg);
      return;
    }

    const ids = validItems.map((x) => x.id);

    Modal.confirm({
      title: config.confirmTitle || 'Xác nhận',
      content: config.confirmMessage || 'Bạn có chắc chắn?',
      okText: 'Đồng ý',
      onOk: async () => {
        try {
          await dispatch(
            config.updateAction({
              ids,
              trangThai: config.nextStatus
            })
          ).unwrap();

          toast.success(config.successMsg);
          config.afterSuccess?.();
        } catch {
          toast.error('Thao tác thất bại');
        }
      }
    });
  };

  return { processUpdateStatus };
}
