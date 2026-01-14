export interface ICongThuc {
  id: number;
  congThuc: string;
  loaiKpiApDung: number[];
}
export const LIST_CONG_THUC: ICongThuc[] = [
    { id: 1, congThuc: 'Mỗi lần vi phạm (nộp muộn, không đầy đủ, không đúng yêu cầu): trừ 1% điểm KPIs', loaiKpiApDung: [1, 2], },
    { id: 2, congThuc: 'Mỗi lượt vi phạm > mục tiêu trừ 30% điểm trọng số', loaiKpiApDung: [1, 2], },
    { id: 3, congThuc: 'Mỗi lượt vi phạm trừ 20% điểm trọng số', loaiKpiApDung: [1, 2], },
    { id: 4, congThuc: 'Mỗi lần vi phạm (nộp muộn, không đầy đủ, không đúng yêu cầu): trừ 2% điểm KPIs', loaiKpiApDung: [1, 2], },
    { id: 5, congThuc: 'Mỗi lần vi phạm (nộp muộn, không đầy đủ, không đúng yêu cầu): trừ 2% điểm KPIs', loaiKpiApDung: [1, 2], },
    { id: 6, congThuc: 'Mỗi lần vi phạm (nộp muộn, không đầy đủ, không đúng yêu cầu): trừ 2% điểm KPIs', loaiKpiApDung: [1, 2], },
    { id: 7, congThuc: 'Mỗi lần vi phạm (nộp muộn, không đầy đủ, không đúng yêu cầu): trừ 2% điểm KPIs', loaiKpiApDung: [1, 2], },
    { id: 8, congThuc: 'Nếu kết quả thực tế < Mục tiêu: KPI = 0% Nếu kết quả thực tế >= Mục tiêu: KPI =Trọng số', loaiKpiApDung: [1, 2], },
    { id: 9, congThuc: 'Mỗi lượt vi phạm trừ 10% điểm trọng số', loaiKpiApDung: [1, 2], },
    { id: 10, congThuc: 'Kết quả thực tế/ Mục tiêu * Trọng số', loaiKpiApDung: [1, 2], },
    { id: 11, congThuc: 'Nếu kết quả thực tế > Mục tiêu: KPI = 0% Nếu kết quả thực tế <= Mục tiêu: KPI = Trọng số', loaiKpiApDung: [1, 2], },
    { id: 12, congThuc: 'Mục tiêu/Kết quả thực tế * Trọng số', loaiKpiApDung: [1, 2], },
    { id: 13, congThuc: 'Nếu kết quả thực tế < Mục tiêu: KPI = 0% Nếu kết quả thực tế >= Mục tiêu: KPI = Kết quả thực tế/ Mục tiêu * Trọng số', loaiKpiApDung: [1, 2], },
    { id: 14, congThuc: 'Không hoàn thành: KPI = 0% Hoàn thành: KPI = Trọng số', loaiKpiApDung: [1, 2], },
    { id: 15, congThuc: 'Mỗi lượt vi phạm > mục tiêu trừ 10% điểm trọng số', loaiKpiApDung: [1, 2], },
];
