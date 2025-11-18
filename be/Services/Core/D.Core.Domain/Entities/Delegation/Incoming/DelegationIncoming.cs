using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Delegation.Incoming
{
    public class DelegationIncoming : EntityBase
    {
        [Column("Name")]
        [Description("Tên đoàn vào")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("Content")]
        [Description("Nội dung")]
        [MaxLength(255)]
        public string Content { get; set; }

        [Column("IdPhongBan")]
        [Description("Id của phòng ban tiếp nhận")]
        public int IdPhongBan { get; set; }

        [Column("Location")]
        [Description("Vị trí tiếp nhận")]
        [MaxLength(255)]
        public string? Location { get; set; }

        [Column("IdNhanSuReception")]
        [Description("Id của nhân sự tiếp đón")]
        public int IdNhanSuReception { get; set; }

        [Column("TotalPerson")]
        [Description("Tổng số người tham gia")]
        public int TotalPerson { get; set; }

        [Column("PhoneNumber")]
        [Description("Số điện thoại liên hệ")]
        [MaxLength(255)]
        public string? PhoneNumber { get; set; }

        [Column("ShuttleService")]
        [Description("Dịch vụ đưa đón")]
        [MaxLength(255)]
        public string? ShuttleService { get; set; }

        [Column("Status")]
        [Description("Trạng thái")]
        public int Status { get; set; }

        [Column("Gift")]
        [Description("Quà")]
        [MaxLength(255)]
        public string Gift { get; set; }
    }
}
