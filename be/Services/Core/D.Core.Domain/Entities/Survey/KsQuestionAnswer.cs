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
    [Table(nameof(KsQuestionAnswer), Schema = DbSchema.Ks)]
    public class KsQuestionAnswer : EntityBase
    {
        [Column("QuestionId")]
        [Description("Thuộc về câu hỏi")]
        public int IdCauHoi { get; set; }

        [Column("Content")]
        [Description("Nội dung")]
        [MaxLength(2000)]
        public string NoiDung { get; set; }

        [Column("Value")]
        [Description("Giá trị")]
        public int Value { get; set; }

        [Column("SortOrder")]
        [Description("Thứ tự hiển thị (A, B, C...)")]
        public int ThuTu { get; set; }

        [Column("IsCorrect")]
        [Description("kết quả")]
        public bool IsCorrect { get; set; }

        [Column("Description")]
        [Description("Mô tả")]
        [MaxLength(500)]
        public string MoTa { get; set; }

        [ForeignKey(nameof(IdCauHoi))]
        public virtual KsSurveyQuestion Question { get; set; }
    }
}
