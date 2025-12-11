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
    [Table(nameof(KsSurveySubmission), Schema = DbSchema.Ks)]
    public class KsSurveySubmission : EntityBase
    {
        [Column("SurveyId")]
        [Description("Thuộc đợt khảo sát")]
        public int IdKhaoSat { get; set; }

        [Column("UserId")]
        [Description("Người thực hiện")]
        public int? IdNguoiDung { get; set; }

        [Column("StartTime")]
        [Description("Thời gian bắt đầu làm bài")]
        public DateTime ThoiGianBatDau { get; set; }

        [Column("SubmitTime")]
        [Description("Thời gian nộp bài")]
        public DateTime? ThoiGianNop { get; set; }

        [Column("Status")]
        [Description("Trạng thái (0: Đang làm, 1: Đã nộp)")]
        public int TrangThai { get; set; }

        [Column("TotalScore")]
        [Description("Tổng điểm")]
        public double? DiemTong { get; set; }

        [ForeignKey(nameof(IdKhaoSat))]
        public virtual KsSurvey Survey { get; set; }

        // [ForeignKey(nameof(IdNguoiDung))] 
        // public virtual User User { get; set; }

        public virtual ICollection<KsSurveySubmissionAnswer> Responses { get; set; }
    }
}
