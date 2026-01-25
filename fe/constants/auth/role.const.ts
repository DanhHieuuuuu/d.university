import { ITagInfo } from '@models/common/table.model';

export type RoleStatusTagInfo = ITagInfo & {
  value: number;
};

export class RoleStatusConst {
  public static DISABLED = 0;
  public static ACTIVE = 1;

  private static readonly map: Record<number, RoleStatusTagInfo> = {
    [this.ACTIVE]: {
      value: this.ACTIVE,
      label: 'Hoạt động',
      className: 'tag-done'
    },
    [this.DISABLED]: {
      value: this.DISABLED,
      label: 'Khóa',
      className: 'tag-edit'
    }
  };

  public static getTag(value?: number): RoleStatusTagInfo | null {
    if (value == null) return null;
    return this.map[value] ?? null;
  }

  public static getInfo(
    value: number,
    field: keyof RoleStatusTagInfo | null = null
  ): RoleStatusTagInfo | string | number | undefined {
    const info = this.map[value];
    if (!info) return undefined;

    return field ? info[field] : info;
  }
}
