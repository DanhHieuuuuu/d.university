export class DelegationStatusConst {
  public static TAO_MOI = 1;
  public static DA_CHINH_SUA = 2;
  public static BI_HUY = 3;


  public static list = [
    {
      value: this.TAO_MOI,
      name: 'Tạo mới',
      class: 'tag-init',
    },
    {
      value: this.BI_HUY,
      name: 'Bị huỷ',
      class: 'tag-cancel',
    },
    {
      value: this.DA_CHINH_SUA,
      name: 'Đã chỉnh sửa',
      class: 'tag-edit',
    },
   
  ];
  public static getInfo(
    value: number,
    field: keyof { value: number; name: string; class: string } | null = null
  ): { value: number; name: string; class: string } | string | number | undefined {
    const status = this.list.find((x) => x.value === value);

    if (!status) return undefined;

    return field ? status[field] : status;
  }
}
