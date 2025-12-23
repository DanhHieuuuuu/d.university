export class KpiRoleConst {
  static HIEU_TRUONG = "HIEU_TRUONG";
  static TRUONG_DON_VI = "TRUONG_DON_VI";
  static NHAN_VIEN = "NHAN_VIEN";
  static GIANG_VIEN = "GIANG_VIEN";

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
