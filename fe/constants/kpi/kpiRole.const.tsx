export class KpiRoleConst {
  static HIEU_TRUONG = "HIEU_TRUONG";
  static PHO_HIEU_TRUONG = "PHO_HIEU_TRUONG";
  static TRUONG_DON_VI_CAP_2 = "TRUONG_DON_VI_CAP_2";
  static CHUYEN_VIEN = "CHUYEN_VIEN";
  static GIANG_VIEN = "GIANG_VIEN";

  static list = [
    {
      value: this.HIEU_TRUONG,
      name: 'Hiệu trưởng',
    },
    {
      value: this.PHO_HIEU_TRUONG,
      name: 'Phó hiệu trưởng',
    },
    {
      value: this.TRUONG_DON_VI_CAP_2,
      name: 'Trưởng đơn vị',
    },
    {
      value: this.CHUYEN_VIEN,
      name: 'Chuyên viên',
    },
    {
      value: this.GIANG_VIEN,
      name: 'Giảng viên',
    }
  ];

  static getName(value?: string) {
    return this.list.find(x => x.value == value)?.name ?? '';
  }
}
