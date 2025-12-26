export class KpiLoaiConst {
  static CHUC_NANG = 1;
  static MUC_TIEU = 2;
  static TUAN_THU = 3;

  static list = [
    {
      value: this.CHUC_NANG,
      name: 'Chức năng',
    },
    {
      value: this.MUC_TIEU,
      name: 'Mục tiêu',
    },
    {
      value: this.TUAN_THU,
      name: 'Tuân thủ',
    },
  ];

  static getName(value?: number) {
    return this.list.find(x => x.value === value)?.name ?? '';
  }
}
