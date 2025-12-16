export class KpiRoleConst {
  static HIEU_TRUONG = "Hiệu trưởng";
  static TRUONG_DON_VI = "Trưởng đơn vị";
  static NHAN_VIEN = "Nhân viên";
  static GIANG_VIEN = "Giảng viên";

  static list = [
    {
      value: this.HIEU_TRUONG,
      name: 'Hiệu trưởng',
    },
    {
      value: this.TRUONG_DON_VI,
      name: 'Trưởng đơn vị',
    },
    {
      value: this.NHAN_VIEN,
      name: 'Nhân viên',
    },
    {
      value: this.GIANG_VIEN,
      name: 'Giảng viên',
    },
  ];

  static getName(value?: string) {
    return this.list.find(x => x.value == value)?.name ?? '';
  }
}
