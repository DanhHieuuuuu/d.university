export class surveyStatusConst {
  static CLOSE = 1;
  static OPEN = 2;
  static COMPLETE = 3;

  static list = [
    {
      value: this.CLOSE,
      name: 'Đóng'
    },
    {
      value: this.OPEN,
      name: 'Mở'
    },
    {
      value: this.COMPLETE,
      name: 'Hoàn thành'
    }
  ];

  static getName(value?: number) {
    return this.list.find((x) => x.value == value)?.name ?? '';
  }
}
