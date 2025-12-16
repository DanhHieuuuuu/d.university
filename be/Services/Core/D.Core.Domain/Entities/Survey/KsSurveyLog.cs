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
    [Table(nameof(KsSurveyLog), Schema = DbSchema.Ks)]
    public class KsSurveyLog : EntityBase
    {
        [Column("UserId")]
        [Description("Id người thực hiện")]
        public int? IdNguoiThaoTac { get; set; }

        [Column("UserName")]
        [Description("Tên người thực hiện")]
        [MaxLength(255)]
        public string? TenNguoiThaoTac { get; set; }

        [Column("ActionType")]
        [Description("Loại hành động")]
        [MaxLength(50)]
        public string LoaiHanhDong { get; set; }

        [Column("Description")]
        [Description("Mô tả ")]
        [MaxLength(500)]
        public string MoTa { get; set; }

        [Column("TargetTable")]
        [Description("Bảng bị thay đổi")]
        [MaxLength(100)]
        public string TenBang { get; set; }

        [Column("TargetId")]
        [Description("Id dòng dữ liệu bị thay đổi")]
        [MaxLength(50)]
        public string IdDoiTuong { get; set; }

        [Column("OldValue")]
        [Description("Dữ liệu cũ")]
        public string? DuLieuCu { get; set; }

        [Column("NewValue")]
        [Description("Dữ liệu mới")]
        public string? DuLieuMoi { get; set; }
    }
}
