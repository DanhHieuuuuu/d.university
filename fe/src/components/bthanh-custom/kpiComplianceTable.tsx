import React from 'react';

const KpiReferenceTable = () => {
  return (
    <div className="overflow-x-auto">
      <table className="min-w-full border-collapse border border-gray-300 text-sm">
        <thead>
          <tr className="bg-gray-100">
            <th rowSpan={2} className="border border-gray-300 p-2 text-center font-bold">
              STT
            </th>
            <th rowSpan={2} className="min-w-[150px] border border-gray-300 p-2 text-center font-bold">
              TIÊU CHÍ
            </th>
            <th colSpan={5} className="border border-gray-300 p-2 text-center font-bold text-red-600">
              MỨC ĐỘ (Điểm trừ % KPI)
            </th>
          </tr>
          <tr className="bg-gray-50">
            <th className="border border-gray-300 p-2 text-center font-semibold text-red-600">30%</th>
            <th className="border border-gray-300 p-2 text-center font-semibold text-red-600">10%</th>
            <th className="border border-gray-300 p-2 text-center font-semibold text-red-600">5%</th>
            <th className="border border-gray-300 p-2 text-center font-semibold text-red-600">2%</th>
            <th className="border border-gray-300 p-2 text-center font-semibold text-red-600">1%</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td className="border border-gray-300 p-2 text-center">1</td>
            <td className="border border-gray-300 p-2">Vi phạm về thời gian làm việc</td>
            <td className="border border-gray-300 p-2 text-center">Trên 07 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">06 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">05 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">04 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">03 lần/năm</td>
          </tr>
          <tr>
            <td className="border border-gray-300 p-2 text-center">2</td>
            <td className="border border-gray-300 p-2">Vi phạm về quy định chấm công</td>
            <td className="border border-gray-300 p-2 text-center">Trên 08 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">07 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">06 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">05 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">04 lần/năm</td>
          </tr>
          <tr>
            <td className="border border-gray-300 p-2 text-center">3</td>
            <td className="border border-gray-300 p-2">Vi phạm tuân thủ về trật tự và tác phong làm việc</td>
            <td className="border border-gray-300 p-2 text-center">Trên 03 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">03 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">02 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">01 lần/năm</td>
            <td className="border border-gray-300 bg-gray-50 p-2 text-center">x</td>
          </tr>
          <tr>
            <td className="border border-gray-300 p-2 text-center">4</td>
            <td className="border border-gray-300 p-2">Vi phạm quy tắc ứng xử (với sinh viên/ đồng nghiệp/ khách)</td>
            <td className="border border-gray-300 p-2 text-center">Trên 03 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">03 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">02 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">01 lần/năm</td>
            <td className="border border-gray-300 bg-gray-50 p-2 text-center">x</td>
          </tr>
          <tr>
            <td className="border border-gray-300 p-2 text-center">5</td>
            <td className="border border-gray-300 p-2">Vi phạm quy định sử dụng đồng phục, thẻ nhân viên</td>
            <td className="border border-gray-300 p-2 text-center">Trên 03 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">03 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">02 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">01 lần/năm</td>
            <td className="border border-gray-300 bg-gray-50 p-2 text-center">x</td>
          </tr>
          <tr>
            <td className="border border-gray-300 p-2 text-center">6</td>
            <td className="border border-gray-300 p-2">Vi phạm quy trình nghiệp vụ</td>
            <td className="border border-gray-300 p-2 text-center">Kỷ luật bằng văn bản từ 02 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">Kỷ luật bằng văn bản 01 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">Kỷ luật bằng lời nói 02 lần/năm</td>
            <td className="border border-gray-300 p-2 text-center">Kỷ luật bằng lời nói 01 lần/năm</td>
            <td className="border border-gray-300 bg-gray-50 p-2 text-center">x</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default KpiReferenceTable;
