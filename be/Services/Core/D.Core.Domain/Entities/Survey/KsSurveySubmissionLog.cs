using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Survey
{
    [Table(nameof(KsSurveySubmissionLog), Schema = DbSchema.Ks)]
    public class KsSurveySubmissionLog : EntityBase
    {
        [Column("SubmissionId")]
        public int IdPhienLamBai { get; set; }

        [Column("Action")]
        [MaxLength(50)]
        public string HanhDong { get; set; } // "Start", "Submit", "AutoSave"...

        [Column("Description")]
        [MaxLength(500)]
        public string MoTa { get; set; } // "Nộp bài được 8 điểm", "Bắt đầu làm bài"...

    }
}
