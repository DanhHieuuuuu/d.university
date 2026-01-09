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
    [Table(nameof(KsSurveyQuestion), Schema = DbSchema.Ks)]
    public class KsSurveyQuestion : EntityBase
    {
        [Column("SurveyRequestId")]
        [Description("Thuộc về yêu cầu khảo sát")]
        public int IdYeuCau { get; set; }

        [Column("QuestionCode")]
        [Description("Mã câu hỏi")]
        [MaxLength(255)]
        public string MaCauHoi { get; set; }

        [Column("Content")]
        [Description("Nội dung")]
        [MaxLength(2000)]
        public string NoiDung { get; set; }

        [Column("Type")]
        [Description("Loại câu hỏi (1:Radio, 2:Checkbox, 3:Text)")]
        public int LoaiCauHoi { get; set; }

        [Column("IsRequired")]
        [Description("Có bắt buộc trả lời không")]
        public bool BatBuoc { get; set; }

        [Column("SortOrder")]
        [Description("Thứ tự hiển thị (A, B, C...)")]
        public int ThuTu { get; set; }

        [ForeignKey(nameof(IdYeuCau))]
        public virtual KsSurveyRequest SurveyRequest { get; set; }
        public virtual ICollection<KsQuestionAnswer> Answers { get; set; }
    }
}
