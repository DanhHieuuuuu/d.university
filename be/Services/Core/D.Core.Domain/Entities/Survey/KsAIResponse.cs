using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Survey
{
    [Table(nameof(KsAIResponse), Schema = DbSchema.Ks)]
    public class KsAIResponse : EntityBase
    {
        [Column("ReportId")]
        [Description("Thuộc báo cáo")]
        public int IdBaoCao { get; set; }

        [Column("CriteriaId")]
        [Description("Phân tích dựa trên tiêu chí")]
        public int IdTieuChi { get; set; }

        [Column("SentimentScore")]
        [Description("Điểm (1.0: Tích cực, -1.0: Tiêu cực, 0: Trung lập)")]
        public double DiemCamXuc { get; set; }

        [Column("SentimentLabel")]
        [Description("Nhãn (Positive/Negative/Neutral)")]
        [MaxLength(50)]
        public string NhanCamXuc { get; set; }

        [Column("Summary")]
        [Description("Tóm tắt nội dung")]
        public string TomTatNoiDung { get; set; }

        [Column("KeyTrends")]
        [Description("Xu hướng, điểm nổi bật")]
        public string XuHuong { get; set; }

        [Column("Recommendation")]
        [Description("Gợi ý cải thiện từ AI")]
        public string GoiYCaiThien { get; set; }

        [ForeignKey(nameof(IdBaoCao))]
        public virtual KsSurveyReport Report { get; set; }

        [ForeignKey(nameof(IdTieuChi))]
        public virtual KsSurveyCriteria Criteria { get; set; }
    }
 }
