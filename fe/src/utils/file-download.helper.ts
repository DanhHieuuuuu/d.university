import { DelegationIncomingService } from '@services/delegation/delegationIncoming.service';
import { processApiMsgError } from '@utils/index';

/**
 * Helper function để download file từ blob response
 */
export const downloadBlobFile = (blob: Blob, fileName: string) => {
  const url = window.URL.createObjectURL(blob);

  const a = document.createElement('a');
  a.href = url;
  a.download = fileName;
  document.body.appendChild(a);
  a.click();

  a.remove();
  window.URL.revokeObjectURL(url);
};

/**
 * Download file Excel template đoàn vào
 */
export const downloadDelegationTemplateExcel = async () => {
  try {
    const res = await DelegationIncomingService.downloadTemplateExcel();

    let fileName = 'Delegation_Template.xlsx';
    const disposition = res.headers?.['content-disposition'];

    if (disposition) {
      // support filename & filename*
      const match = disposition.match(/filename\*?=(?:UTF-8'')?"?([^";]+)"?/);

      if (match?.[1]) {
        fileName = decodeURIComponent(match[1]);
      }
    }

    downloadBlobFile(res.data, fileName);
  } catch (err) {
    processApiMsgError(err, 'Không tải được file Excel mẫu');
    throw err;
  }
};
