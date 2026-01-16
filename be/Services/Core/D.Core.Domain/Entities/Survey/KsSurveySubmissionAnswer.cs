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
    [Table(nameof(KsSurveySubmissionAnswer), Schema = DbSchema.Ks)]
    public class KsSurveySubmissionAnswer : EntityBase
    {
        [Column("SubmissionId")]
        [Description("Thuộc phiên làm bài")]
        public int IdPhienLamBai { get; set; }

        [Column("QuestionId")]
        [Description("Trả lời cho câu hỏi")]
        public int IdCauHoi { get; set; }

        [Column("SelectedAnswerId")]
        [Description("Id đáp án đã chọn (Null nếu là câu tự luận)")]
        public int? IdDapAnChon { get; set; }

        [Column("TextResponse")]
        [Description("Nội dung trả lời tự luận")]
        public string? CauTraLoiText { get; set; }

        [ForeignKey(nameof(IdPhienLamBai))]
        public virtual KsSurveySubmission Submission { get; set; }

        [ForeignKey(nameof(IdCauHoi))]
        public virtual KsSurveyQuestion Question { get; set; }

        [ForeignKey(nameof(IdDapAnChon))]
        public virtual KsQuestionAnswer SelectedAnswer { get; set; }
    }
}
