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
    [Table(nameof(KsSurveyRequest), Schema = DbSchema.Ks)]
    public class KsSurveyRequest : EntityBase
    {
        [Column("SurveyRequestCode")]
        [Description("Mã yêu cầu khảo sát")]
        [MaxLength(255)]
        public string MaYeuCau { get; set; }

        [Column("Name")]
        [Description("Tên khảo sát yêu cầu")]
        [MaxLength(255)]
        public string TenKhaoSatYeuCau { get; set; }

        [Column("Description")]
        [Description("Mô tả")]
        [MaxLength(2000)]
        public string MoTa { get; set; }

        [Column("TimeStart")]
        [Description("Thời gian mở dự kiến")]
        public DateTime ThoiGianBatDau { get; set; }

        [Column("TimeEnd")]
        [Description("Thời gian đóng dự kiến")]
        public DateTime ThoiGianKetThuc { get; set; }

        [Column("IdPhongBan")]
        [Description("Id của phòng ban yêu cầu")]
        public int IdPhongBan { get; set; }

        [Column("Status")]
        [Description("Trạng thái (0:Draft, 1:Pending, 2:Approved, 3:Rejected)")]
        public int TrangThai { get; set; }

        [Column("Reason")]
        [Description("Lý do từ chối")]
        [MaxLength(500)]
        public string LyDoTuChoi { get; set; }

        public virtual ICollection<KsSurveyTarget> Targets { get; set; }
        public virtual ICollection<KsSurveyQuestion> Questions { get; set; }
        public virtual ICollection<KsSurveyCriteria> Criterias { get; set; }
    }
}
