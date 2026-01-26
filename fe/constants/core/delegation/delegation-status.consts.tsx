import { ITagInfo } from '@models/common/table.model';

export type DelegationStatusTagInfo = ITagInfo & {
  value: number;
};

export class DelegationStatusConst {
  public static TAO_MOI = 1;
  public static DE_XUAT = 2;
  public static PHE_DUYET = 3;
  public static DANG_TIEP_DOAN = 4;
  public static DONE = 5;
  public static BI_HUY = 6;
  public static CAN_BO_SUNG = 7;
  public static DA_CHINH_SUA = 8;
  public static DA_HET_HAN = 9;

  private static readonly map: Record<number, DelegationStatusTagInfo> = {
    [this.TAO_MOI]: {
      value: this.TAO_MOI,
      label: 'Tạo mới',
      className: 'tag-init'
    },
    [this.DE_XUAT]: {
      value: this.DE_XUAT,
      label: 'Đề xuất',
      className: 'tag-propose'
    },
    [this.PHE_DUYET]: {
      value: this.PHE_DUYET,
      label: 'BGH phê duyệt',
      className: 'tag-approve'
    },
    [this.DANG_TIEP_DOAN]: {
      value: this.DANG_TIEP_DOAN,
      label: 'Đang tiếp đoàn',
      className: 'tag-reception'
    },
    [this.DONE]: {
      value: this.DONE,
      label: 'Hoàn thành',
      className: 'tag-done'
    },
    [this.BI_HUY]: {
      value: this.BI_HUY,
      label: 'Bị huỷ',
      className: 'tag-cancel'
    },
    [this.CAN_BO_SUNG]: {
      value: this.CAN_BO_SUNG,
      label: 'Cần bổ sung',
      className: 'tag-edit'
    },
    [this.DA_CHINH_SUA]: {
      value: this.DA_CHINH_SUA,
      label: 'Đã chỉnh sửa',
      className: 'tag-edit'
    },
    [this.DA_HET_HAN]: {
      value: this.DA_HET_HAN,
      label: 'Đã hết hạn',
      className: 'tag-cancel'
    }
  };

  public static getTag(value?: number): DelegationStatusTagInfo | null {
    if (value == null) return null;
    return this.map[value] ?? null;
  }

  public static getInfo(
    value: number,
    field: keyof DelegationStatusTagInfo | null = null
  ): DelegationStatusTagInfo | string | number | undefined {
    const info = this.map[value];
    if (!info) return undefined;

    return field ? info[field] : info;
  }
}
