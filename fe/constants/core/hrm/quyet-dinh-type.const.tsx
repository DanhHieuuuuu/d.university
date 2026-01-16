import { ITagInfo } from '@models/common/table.model';

export type NsQuyetDinhTypeTag = ITagInfo & {
  value: number;
};


export class NsQuyetDinhTypeConst {
  public static TIEP_NHAN = 1
  public static DIEU_CHUYEN = 2
  public static BO_NHIEM = 3
  public static BAI_NHIEM = 4
  public static THOI_VIEC = 5

  private static readonly map: Record<number, NsQuyetDinhTypeTag> = {
      [this.TIEP_NHAN]: {
        value: this.TIEP_NHAN,
        label: 'Tiếp nhận',
        className: 'tag-init',
      },
      [this.DIEU_CHUYEN]: {
        value: this.DIEU_CHUYEN,
        label: 'Điều chuyển',
        className: 'tag-init'
      },
      [this.BO_NHIEM]: {
        value: this.BO_NHIEM,
        label: 'Bổ nhiệm',
        className: 'tag-init'
      },
      [this.BAI_NHIEM]: {
        value: this.BAI_NHIEM,
        label: 'Bãi nhiệm',
        className: 'tag-init'
      },
      [this.THOI_VIEC]: {
        value: this.THOI_VIEC,
        label: 'thôi việc',
        className: 'tag-init'
      },
    };
  
    public static getTag(value?: number): NsQuyetDinhTypeTag | null {
      if (value == null) return null;
      return this.map[value] ?? null;
    }
  
    public static getInfo(
      value: number,
      field: keyof NsQuyetDinhTypeTag | null = null
    ): NsQuyetDinhTypeTag | string | number | undefined {
      const info = this.map[value];
      if (!info) return undefined;
  
      return field ? info[field] : info;
    }
}

