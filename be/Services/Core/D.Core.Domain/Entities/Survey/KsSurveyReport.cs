using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Survey
{
    [Table(nameof(KsSurveyReport), Schema = DbSchema.Ks)]
    public class KsSurveyReport : EntityBase
    {
        [Column("SurveyId")]
        [Description("Báo cáo của đợt khảo sát nào")]
        public int IdKhaoSat { get; set; }

        [Column("TotalParticipants")]
        [Description("Tổng số lượt tham gia nộp bài")]
        public int TongSoLuotThamGia { get; set; }

        [Column("AverageScore")]
        [Description("Điểm đánh giá trung bình")]
        public double? DiemTrungBinh { get; set; }

        [Column("StatisticsData")]
        [Description("Dữ liệu thống kê chi tiết từng câu (Lưu dạng JSON)")]
        public string DuLieuThongKe { get; set; }

        [Column("GeneratedAt")]
        [Description("Thời gian tạo báo cáo")]
        public DateTime ThoiGianTao { get; set; }

        [ForeignKey(nameof(IdKhaoSat))]
        public virtual KsSurvey Survey { get; set; }
    }
}
