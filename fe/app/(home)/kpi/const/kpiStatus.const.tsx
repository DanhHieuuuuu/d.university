import { TagProps } from 'antd';

export class KpiTrangThaiConst {
  static TAO_MOI = 1;
  static DE_XUAT = 2;
  static PHE_DUYET = 3;
  static HOAN_THANH = 4;
  static TU_CHOI = 5;
  static CHINH_SUA = 6;
  static DA_CHINH_SUA = 7;
  static DUOC_GIAO = 8;
  static DANG_DANH_GIA = 9;
  static TU_CHOI_KET_QUA = 10;
  static DA_CHAM = 11;
  static DA_KE_KHAI = 12;
  static DA_GUI_CHAM = 13;

  static list = [
    { value: this.TAO_MOI, text: 'Tạo mới', color: 'default' },
    { value: this.DE_XUAT, text: 'Đề xuất', color: 'processing' },
    { value: this.PHE_DUYET, text: 'Phê duyệt', color: 'blue' },
    { value: this.HOAN_THANH, text: 'Hoàn thành', color: 'success' },
    { value: this.TU_CHOI, text: 'Từ chối', color: 'error' },
    { value: this.CHINH_SUA, text: 'Yêu cầu chỉnh sửa', color: 'warning' },
    { value: this.DA_CHINH_SUA, text: 'Đã chỉnh sửa', color: 'gold' },
    { value: this.DUOC_GIAO, text: 'Được giao', color: 'cyan' },
    { value: this.DANG_DANH_GIA, text: 'Đang đánh giá', color: 'processing' },
    { value: this.TU_CHOI_KET_QUA, text: 'Từ chối kết quả', color: 'error' },
    { value: this.DA_CHAM, text: 'Đã chấm', color: 'success' },
    { value: this.DA_KE_KHAI, text: 'Đã kê khai', color: 'purple' },
    { value: this.DA_GUI_CHAM, text: 'Đã gửi chấm', color: 'geekblue' },
  ] as { value: number; text: string; color: TagProps['color'] }[];

  static get(value?: number) {
    return this.list.find(x => x.value === value);
  }

  static getText(value?: number) {
    return this.get(value)?.text ?? '';
  }

  static getColor(value?: number) {
    return this.get(value)?.color ?? 'default';
  }
}
