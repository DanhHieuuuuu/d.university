export type KpiTableRow<T> = T & {
  rowType: 'group' | 'data' | 'total';
  isMetaRow: boolean;
};

export function buildKpiGroupedTable<T extends { loaiKpi: number; trongSo: any; id: any }>(
  list: T[]
): KpiTableRow<T>[] {
  if (!list?.length) return [];

  const map = new Map<number, T[]>();
  list.forEach(item => {
    if (!map.has(item.loaiKpi)) map.set(item.loaiKpi, []);
    map.get(item.loaiKpi)!.push(item);
  });

  const result: KpiTableRow<T>[] = [];

  map.forEach((items, loaiKpi) => {
    result.push({
      rowType: 'group',
      loaiKpi,
      isMetaRow: true,
    } as KpiTableRow<T>);

    items.forEach(item =>
      result.push({ ...item, rowType: 'data', isMetaRow: false })
    );

    result.push({
      rowType: 'total',
      trongSo: items.reduce((s, i) => s + (Number(i.trongSo) || 0), 0),
      isMetaRow: true,
    } as KpiTableRow<T>);
  });

  return result;
}
