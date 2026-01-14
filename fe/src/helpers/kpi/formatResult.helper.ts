import { LoaiCongThuc } from "@/constants/kpi/loaiCongThuc.enum";

export const formatKetQua = (
  value: number | undefined,
  loaiCongThuc?: LoaiCongThuc
) => {
  if (value == null) return '-';

  switch (loaiCongThuc) {
    case LoaiCongThuc.YES_NO:
      return value === 1 ? 'Hoàn thành' : 'Không hoàn thành';

    case LoaiCongThuc.PERCENT:
      return `${value}%`;

    default:
      return value;
  }
};
