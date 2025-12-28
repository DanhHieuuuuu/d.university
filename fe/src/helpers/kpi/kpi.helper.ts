import { KpiLoaiConst } from "@/app/(home)/kpi/const/kpiType.const";

export function buildKpiGroupedTable<
  T extends { loaiKpi: number; trongSo: any; id: any }
>(list: T[]) {
  if (!list?.length) return [];

  const map = new Map<number, T[]>();
  list.forEach(item => {
    if (!map.has(item.loaiKpi)) map.set(item.loaiKpi, []);
    map.get(item.loaiKpi)!.push(item);
  });

  const result: any[] = [];

  map.forEach((items, loaiKpi) => {
    result.push({
      rowType: 'group',
      loaiKpi,
      isMetaRow: true,
    });

    items.forEach(item =>
      result.push({
        ...item,
        rowType: 'data',
        isMetaRow: false,
      })
    );

    result.push({
      rowType: 'total',
      trongSo: items.reduce((s, i) => s + (Number(i.trongSo) || 0), 0),
      isMetaRow: true,
    });
  });

  return result;
}