export class surveyTargetConst {
  static ALL = 0;
  static STUDENT = 1;
  static LECTURER = 2;

  static list = [
    {
      value: this.ALL,
      name: 'Tất cả',
    },
    {
      value: this.STUDENT,
      name: 'Sinh viên',
    },
    {
      value: this.LECTURER,
      name: 'Giảng viên',
    },
  ];

  static getName(value?: number) {
    return this.list.find(x => x.value == value)?.name ?? '';
  }
}