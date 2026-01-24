import { LoaiCongThuc } from '@/constants/kpi/loaiCongThuc.enum';
import { Input, Select, Checkbox } from 'antd';

type Props = {
  loaiKetQua?: LoaiCongThuc;
  value: any;
  onChange: (value: any) => void;
  editable?: boolean;
};

export default function KetQuaInput({ loaiKetQua, value, onChange, editable = true }: Props) {
  const renderMap: Record<LoaiCongThuc, JSX.Element> = {
    [LoaiCongThuc.NUMBER]: numberInput(),
    [LoaiCongThuc.PERCENT]: percentInput(),
    [LoaiCongThuc.YES_NO]: (
      <Select
        size="middle"
        value={value}
        style={{ width: '95%' }}
        options={[
          { value: 1, label: 'Hoàn thành' },
          { value: 0, label: 'Không hoàn thành' }
        ]}
        onChange={onChange}
        disabled={!editable}
      />
    )
  };
  if (loaiKetQua == null) return null;
  return renderMap[loaiKetQua] ?? null;

  function numberInput() {
    return (
      <Input
        size="middle"
        type="number"
        style={{ width: '95%' }}
        value={value}
        onChange={(e) => onChange(Number(e.target.value))}
        disabled={!editable}
      />
    );
  }

  function percentInput() {
    return (
      <Input
        size="middle"
        type="number"
        style={{ width: '95%' }}
        min={0}
        max={100}
        suffix="%"
        value={value}
        disabled={!editable}
        onChange={(e) => {
          const raw = e.target.value;
          if (raw === '') {
            onChange(undefined);
            return;
          }
          const v = Number(raw);
          if (Number.isNaN(v)) return;
          if (v < 0 || v > 100) return;
          onChange(v);
        }}
      />
    );
  }
}
