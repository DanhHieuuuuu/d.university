import { Input, Select, Checkbox } from 'antd';
import { LoaiCongThuc } from '@/app/(home)/kpi/const/loaiCongThuc.enum';

type Props = {
    loaiCongThuc?: LoaiCongThuc;
    value: any;
    onChange: (value: any) => void;
    editable?: boolean;
};

export default function KetQuaInput({
    loaiCongThuc,
    value,
    onChange,
    editable = true,
}: Props) {
    const renderMap: Record<LoaiCongThuc, JSX.Element> = {

        [LoaiCongThuc.TruMoiLan]: numberInput(),
        [LoaiCongThuc.VuotMucTru]: numberInput(),
        [LoaiCongThuc.KyLuatVuotMuc]: numberInput(),
        [LoaiCongThuc.ChiaTheoMucTieu]: percentInput(),

        [LoaiCongThuc.HoanThanh]: (
            <Select
                size="small"
                value={value}
                style={{ width: '70%' }}
                options={[
                    { value: 1, label: 'Hoàn thành' },
                    { value: 0, label: 'Không hoàn thành' },
                ]}
                onChange={onChange}
                disabled={!editable}
            />
        ),

        [LoaiCongThuc.KyLuatBang]: (
            <Checkbox
                checked={value === 1}
                style={{ width: '70%' }}
                onChange={(e) => onChange(e.target.checked ? 1 : 0)}
                disabled={!editable}
                
            />
        ),
    };
    if (loaiCongThuc == null) return null;
    return renderMap[loaiCongThuc] ?? null;

    function numberInput() {
        return (
            <Input
                size="small"
                type="number"
                style={{ width: '70%' }}
                value={value}
                onChange={(e) => onChange(Number(e.target.value))}
                disabled={!editable}
            />
        );
    }

    function percentInput() {
        return (
            <Input
                size="small"
                type="number"
                style={{ width: '70%' }}
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
