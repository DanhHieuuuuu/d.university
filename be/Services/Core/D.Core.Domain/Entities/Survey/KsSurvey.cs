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
    [Table(nameof(KsSurvey), Schema = DbSchema.Ks)]
    public class KsSurvey : EntityBase
    {
        [Column("SurveyRequestId")]
        [Description("Được tạo ra từ yêu cầu")]
        public int IdYeuCau { get; set; }

        [Column("SurveyCode")]
        [Description("Mã khảo sát")]
        [MaxLength(255)]
        public string MaKhaoSat { get; set; }

        [Column("Name")]
        [Description("Tên khảo sát")]
        [MaxLength(255)]
        public string TenKhaoSat { get; set; }

        [Column("Description")]
        [Description("Mô tả")]
        [MaxLength(2000)]
        public string MoTa { get; set; }

        [Column("TimeStart")]
        [Description("Thời gian mở")]
        public DateTime ThoiGianBatDau { get; set; }

        [Column("TimeEnd")]
        [Description("Thời gian đóng")]
        public DateTime ThoiGianKetThuc { get; set; }

        [Column("IdPhongBan")]
        [Description("Id của phòng ban yêu cầu")]
        public int IdPhongBan { get; set; }

        [Column("Status")]
        [Description("Trạng thái (0: Closed, 1: Active/Open, 2: Completed)\")")]
        public int Status { get; set; }

        [ForeignKey(nameof(IdYeuCau))]
        public virtual KsSurveyRequest SurveyRequest { get; set; }
    }
}
