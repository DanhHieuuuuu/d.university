import { LoaiCongThuc } from "@/app/(home)/kpi/const/loaiCongThuc.enum";

export const formatKetQua = (
  value: number | undefined,
  loaiCongThuc?: LoaiCongThuc
) => {
  if (value == null) return '-';

  switch (loaiCongThuc) {
    case LoaiCongThuc.HoanThanh:
      return value === 1 ? 'Hoàn thành' : 'Không hoàn thành';

    case LoaiCongThuc.ChiaTheoMucTieu:
      return `${value}%`;

    case LoaiCongThuc.KyLuatBang:
      return value === 1 ? 'Có' : 'Không';

    default:
      return value;
  }
};
