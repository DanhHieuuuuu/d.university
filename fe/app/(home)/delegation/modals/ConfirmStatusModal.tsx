import { Modal, Button, Space } from 'antd';
import { toast } from 'react-toastify';
import { IViewGuestGroup } from '@models/delegation/delegation.model';
import { updateStatus } from '@redux/feature/delegation/delegationThunk';
import type { AppDispatch } from '@redux/store';

type ConfirmAction = 'upgrade' | 'cancel' | 'supplement';

interface ConfirmActionConfig {
  action: ConfirmAction;
  label: string;
  type?: 'primary' | 'default' | 'dashed';
  danger?: boolean;
}
interface ConfirmStatusModalProps {
  open: boolean;
  title: string;
  content: string;
  data: IViewGuestGroup;
  dispatch: AppDispatch;
  onClose: () => void;
  onSuccess?: () => void;
  actions: readonly ConfirmActionConfig[];
}

export const ConfirmStatusModal = ({
  open,
  title,
  content,
  data,
  dispatch,
  onClose,
  onSuccess,
  actions
}: ConfirmStatusModalProps) => {
  const handleUpdateStatus = async (action: 'upgrade' | 'cancel' | 'supplement') => {
    try {
      await dispatch(
        updateStatus({
          idDelegation: data.id,
          oldStatus: data.status,
          action
        })
      ).unwrap();

      toast.success(
        action === 'upgrade' ? 'Thành công' : action === 'supplement' ? 'Đã yêu cầu bổ sung' : 'Không đồng ý'
      );

      onSuccess?.();
      onClose();
    } catch (error: any) {
      toast.error(error?.message || 'Thao tác thất bại');
    }
  };

  return (
    <Modal open={open} title={title} onCancel={onClose} footer={null} destroyOnClose>
      <p>{content}</p>

      <Space
        style={{
          marginTop: 24,
          width: '100%',
          justifyContent: 'flex-end'
        }}
      >
        <Button onClick={onClose}>Huỷ</Button>
        {actions.map((btn) => (
          <Button
            key={btn.action}
            type={btn.type ?? 'default'}
            danger={btn.danger}
            onClick={() => handleUpdateStatus(btn.action)}
          >
            {btn.label}
          </Button>
        ))}
      </Space>
    </Modal>
  );
};
