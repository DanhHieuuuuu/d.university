export class DelegationStatusConst {
  public static TAO_MOI = 1;
  public static DE_XUAT = 2;
  public static PHE_DUYET = 3;
  public static DANG_TIEP_DOAN = 4;
  public static DONE = 5;
  public static BI_HUY = 6;
  public static CAN_BO_SUNG = 7;
  public static DA_CHINH_SUA = 8;

  public static list = [
    {
      value: this.TAO_MOI,
      name: 'Tạo mới',
      class: 'tag-init'
    },
    {
      value: this.BI_HUY,
      name: 'Bị huỷ',
      class: 'tag-cancel'
    },
    {
      value: this.DA_CHINH_SUA,
      name: 'Đã chỉnh sửa',
      class: 'tag-edit'
    },
    {
      value: this.DE_XUAT,
      name: 'Đề xuất',
      class: 'tag-propose'
    },
    {
      value: this.PHE_DUYET,
      name: 'Phê duyệt',
      class: 'tag-approve'
    },
    {
      value: this.DANG_TIEP_DOAN,
      name: 'Đang tiếp đoàn',
      class: 'tag-reception'
    },
    {
      value: this.DONE,
      name: 'Hoàn thành',
      class: 'tag-done'
    },
    {
      value: this.CAN_BO_SUNG,
      name: 'Cần bổ sung',
      class: 'tag-edit'
    }
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
