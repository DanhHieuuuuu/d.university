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
    [Table(nameof(KsSurveyCriteria), Schema = DbSchema.Ks)]
    public class KsSurveyCriteria : EntityBase
    {
        [Column("SurveyRequestId")]
        [Description("Thuộc về yêu cầu khảo sát")]
        public int IdYeuCau { get; set; }

        [Column("CriteriaName")]
        [Description("Tên tiêu chí")]
        public string TenTieuChi { get; set; }

        [Column("Weight")]
        [Description("Trọng số")]
        public double Weight { get; set; }

        [Column("Description")]
        [Description("Mô tả")]
        [MaxLength(2000)]
        public string MoTa { get; set; }

        [Column("Keyword")]
        [Description("Từ khóa")]
        [MaxLength(2000)]
        public string Keyword { get; set; }
    }
}
