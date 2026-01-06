export class surveyStatusConst {
  static CLOSE = 0;
  static OPEN = 1;
  static COMPLETE = 2;

  static list = [
    {
      value: this.CLOSE,
      name: 'Đóng',
    },
    {
      value: this.OPEN,
      name: 'Mở',
    },
    {
      value: this.COMPLETE,
      name: 'Hoàn thành',
    }
  ];

  static getName(value?: number) {
    return this.list.find(x => x.value == value)?.name ?? '';
  }
}
