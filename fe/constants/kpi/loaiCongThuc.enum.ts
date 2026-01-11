export enum LoaiCongThuc {
  NUMBER = 'NUMBER',
  PERCENT = 'PERCENT',
  YES_NO = 'YES_NO',
}

export const LOAI_KET_QUA_OPTIONS = [
  { value: LoaiCongThuc.NUMBER, label: 'Số' },
  { value: LoaiCongThuc.PERCENT, label: 'Phần trăm (%)' },
  { value: LoaiCongThuc.YES_NO, label: 'Có / Không' },
];