export class requestStatusConst {
  static DRAFT = 1;
  static PENDING = 2;
  static APPROVED = 3;
  static REJECTED = 4;
  static CANCELED = 5;

  static list = [
    {
      value: this.DRAFT,
      name: 'Bản nháp'
    },
    {
      value: this.PENDING,
      name: 'Đề xuất '
    },
    {
      value: this.APPROVED,
      name: 'Phê duyệt'
    },
    {
      value: this.REJECTED,
      name: 'Từ chối'
    },
    {
      value: this.CANCELED,
      name: 'Bị hủy/đóng'
    }
  ];

  static getName(value?: number) {
    return this.list.find((x) => x.value == value)?.name ?? '';
  }
}
