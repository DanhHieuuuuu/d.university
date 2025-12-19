import { Modal } from 'antd';
import { toast } from 'react-toastify';
import { IViewGuestGroup } from '@models/delegation/delegation.model';
import { updateStatus } from '@redux/feature/delegation/delegationThunk';
import type { AppDispatch } from '@redux/store';

interface IConfirmStatusModal {
  title: string;
  content: string;
  okText?: string;
  cancelText?: string;
  okAction: 'upgrade' | 'cancel';
  cancelAction?: 'cancel';
  data: IViewGuestGroup;
  dispatch: AppDispatch;
  onSuccess?: () => void;
}

export const openConfirmStatusModal = ({
  title,
  content,
  okText = 'Đồng ý',
  cancelText = 'Hủy',
  okAction,
  cancelAction,
  data,
  dispatch,
  onSuccess
}: IConfirmStatusModal) => {
  Modal.confirm({
    title,
    content,
    okText,
    cancelText,
    okButtonProps: { type: 'primary' },
    onOk: async () => {
      try {
        await dispatch(
          updateStatus({
            idDelegation: data.id,
            oldStatus: data.status,
            action: okAction
          })
        ).unwrap();

        toast.success('Thành công');
        onSuccess?.();
      } catch (error: any) {
        toast.error(error?.message || 'Thất bại');
      }
    },

    onCancel: cancelAction
      ? async () => {
          try {
            await dispatch(
              updateStatus({
                idDelegation: data.id,
                oldStatus: data.status,
                action: cancelAction
              })
            ).unwrap();

            toast.success('Đã từ chối');
            onSuccess?.();
          } catch (error: any) {
            toast.error(error?.message || 'Thao tác thất bại');
          }
        }
      : undefined
  });
};
