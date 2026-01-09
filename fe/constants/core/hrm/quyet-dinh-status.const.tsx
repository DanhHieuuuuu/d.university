import { ITagInfo } from '@models/common/table.model';

export type NsQuyetDinhStatusTag = ITagInfo & {
  value: number;
};


export class NsQuyetDinhStatusConst {
  public static TAO_MOI = 0
  public static PHE_DUYET = 1
  public static TU_CHOI = 2

  private static readonly map: Record<number, NsQuyetDinhStatusTag> = {
      [this.TAO_MOI]: {
        value: this.TAO_MOI,
        label: 'Tạo mới',
        className: 'tag-init',
      },
      [this.PHE_DUYET]: {
        value: this.PHE_DUYET,
        label: 'Phê duyệt',
        className: 'tag-done'
      },
      [this.TU_CHOI]: {
        value: this.TU_CHOI,
        label: 'Từ chối',
        className: 'tag-cancel'
      },
    };
  
    public static getTag(value?: number): NsQuyetDinhStatusTag | null {
      if (value == null) return null;
      return this.map[value] ?? null;
    }
  
    public static getInfo(
      value: number,
      field: keyof NsQuyetDinhStatusTag | null = null
    ): NsQuyetDinhStatusTag | string | number | undefined {
      const info = this.map[value];
      if (!info) return undefined;
  
      return field ? info[field] : info;
    }
}

