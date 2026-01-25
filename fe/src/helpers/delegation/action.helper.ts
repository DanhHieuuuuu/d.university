import { toast } from 'react-toastify';
import { DelegationIncomingService } from '@/src/services/delegation/delegationIncoming.service';
import { IViewGuestGroup } from '@models/delegation/delegation.model';

export const exportBaoCaoDoanVao = async (record: IViewGuestGroup) => {
  try {
    const res = await DelegationIncomingService.baoCaoDoanVao({
      listId: [record.id],
      isExportAll: false
    });

    const blob = new Blob([res.data], { type: 'application/zip' });
    const url = window.URL.createObjectURL(blob);

    const a = document.createElement('a');
    a.href = url;
    a.download = `Bao_cao_doan_vao_${record.name}.zip`;
    document.body.appendChild(a);
    a.click();

    document.body.removeChild(a);
    window.URL.revokeObjectURL(url);

    toast.success('Xuất báo cáo thành công');
  } catch {
    toast.error('Không xuất được báo cáo');
  }
};
