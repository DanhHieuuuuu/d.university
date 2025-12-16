import React from 'react';

export interface DetailRow {
  label: string;
  value: any;
}

interface DetailTableProps {
  rows: DetailRow[];
  bordered?: boolean;
}

export default function DetailTable({ rows, bordered = true }: DetailTableProps) {
  // Handle empty or undefined rows
  if (!rows || rows.length === 0) {
    return (
      <div className="mt-4 rounded-lg bg-gray-50 p-8 text-center text-gray-500">
        Không có dữ liệu để hiển thị
      </div>
    );
  }

  return (
    <div className="overflow-x-auto rounded-lg shadow-md">
      <table className="w-full border-collapse bg-white">
        <tbody>
          {rows.map((item, i) => {
            const isArray = Array.isArray(item.value);

            return (
              <tr 
                key={i} 
                className={`transition-colors duration-150 ${
                  i % 2 === 0 ? 'bg-slate-50' : 'bg-white'
                } hover:bg-blue-50`}
              >
                {/* Label */}
                <td
                  className={`w-1/3 px-4 py-3 align-top font-semibold text-gray-700 ${
                    bordered ? 'border border-gray-200' : ''
                  }`}
                >
                  {item.label}
                </td>

                {/* Value */}
                <td 
                  className={`break-words px-4 py-3 align-top text-gray-800 ${
                    bordered ? 'border border-gray-200' : ''
                  }`}
                >
                  {isArray ? (
                    <ul className="ml-5 list-disc space-y-1 text-gray-700">
                      {item.value.map((v: any, idx: number) => (
                        <li key={idx}>{v}</li>
                      ))}
                    </ul>
                  ) : (
                    item.value
                  )}
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}