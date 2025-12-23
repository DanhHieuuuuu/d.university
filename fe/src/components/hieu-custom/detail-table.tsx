import React from 'react';

export interface DetailRow {
  label: string;
  value: any;
}

interface DetailTableProps {
  rows: DetailRow[];
  bordered?: boolean;
}

export default function DetailTable({ rows, bordered = false }: DetailTableProps) {
  if (!rows || rows.length === 0) {
    return <div className="mt-3 rounded-md bg-gray-50 p-4 text-center text-gray-500">Không có dữ liệu để hiển thị</div>;
  }

  return (
    <div className="overflow-x-auto">
      <table className="w-full table-fixed border-collapse">
        <tbody>
          {rows.map((item, i) => {
            const isArray = Array.isArray(item.value);

            return (
              <tr key={i} className="align-top">
                {/* Label */}
                <td className="px-1.5 py-1 text-sm font-medium text-gray-600" style={{ width: '40%' ,verticalAlign: 'top' }}>
                  {item.label}
                </td>

                {/* Value */}
                <td
                  className="px-1.5 py-1 pl-8 text-sm text-gray-900"
                  style={{ width: '60%' ,verticalAlign: 'top' }}
                >
                  {isArray ? (
                    <ul className="ml-3 list-disc space-y-0.5">
                      {item.value.map((v: any, idx: number) => (
                        <li key={idx}>{v}</li>
                      ))}
                    </ul>
                  ) : (item.value)}
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
