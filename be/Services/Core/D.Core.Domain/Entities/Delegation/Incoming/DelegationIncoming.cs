using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Delegation.Incoming
{
    [Table(nameof(DelegationIncoming), Schema = DbSchema.Delegation)]
    public class DelegationIncoming : EntityBase
    {
        [Column("Code")]
        [Description("Mã đoàn vào")]
        [MaxLength(255)]
        public string Code { get; set; }

        [Column("Name")]
        [Description("Tên đoàn vào")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("Content")]
        [Description("Nội dung")]
        [MaxLength(255)]
        public string? Content { get; set; }

        [Column("IdPhongBan")]
        [Description("Id của phòng ban tiếp nhận")]
        public int IdPhongBan { get; set; }

        [Column("Location")]
        [Description("Vị trí tiếp nhận")]
        [MaxLength(255)]
        public string? Location { get; set; }

        [Column("IdStaffReception")]
        [Description("Id của nhân sự tiếp đón")]
        public int IdStaffReception { get; set; }

        [Column("TotalPerson")]
        [Description("Tổng số người tham gia")]
        public int TotalPerson { get; set; }

        [Column("PhoneNumber")]
        [Description("Số điện thoại liên hệ")]
        [MaxLength(255)]
        public string? PhoneNumber { get; set; }

        [Column("Status")]
        [Description("Trạng thái")]
        public int Status { get; set; }

        [Column("RequestDate")]
        [Description("Ngày yêu cầu")]
        public DateOnly RequestDate { get; set; }

        [Column("ReceptionDate")]
        [Description("Ngày tiếp nhận")]
        public DateOnly ReceptionDate { get; set; }

        [Column("TotalMoney")]
        [Description("Tổng chi phí")]
        public decimal TotalMoney { get; set; }
        [Column("IsExpiryNotified")]
        [Description("Gửi thông báo sắp hết hạn")]
        public bool IsExpiryNotified { get; set; } = false;

        public virtual ICollection<DetailDelegationIncoming>? DelegationDetails { get; set; }
        public virtual ICollection<ReceptionTime>? ReceptionTimes { get; set; }
        public virtual ICollection<DepartmentSupport>? DepartmentSupports { get; set; }

    }
}
