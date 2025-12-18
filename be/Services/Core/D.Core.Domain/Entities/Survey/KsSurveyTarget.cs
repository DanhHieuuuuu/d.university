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
    [Table(nameof(KsSurveyTarget), Schema = DbSchema.Ks)]
    public class KsSurveyTarget : EntityBase
    {
        [Column("SurveyRequestId")]
        [Description("Thuộc về yêu cầu khảo sát nào")]
        public int IdYeuCau { get; set; }

        [Column("Type")]
        [Description("Loại đối tượng (0: Tất cả, 1: Sinh viên, 2: Giảng viên)")]
        public int LoaiDoiTuong { get; set; }

        [Column("DepartmentId")]
        [Description("Lọc theo Phòng ban")]
        public int? IdPhongBan { get; set; }

        [Column("FacultyId")]
        [Description("Lọc theo Khoa(sinh viên)")]
        public int? IdKhoa { get; set; }

        [Column("CourseId")]
        [Description("Lọc theo Khóa học")]
        public int? IdKhoaHoc { get; set; }

        //[Column("ClassId")]
        //[Description("Lọc theo Lớp hành chính cụ thể (Dùng cho SV)")]
        //public int? IdLop { get; set; }

        [Column("Description")]
        [Description("Mô tả")]
        [MaxLength(2000)]
        public string MoTa { get; set; }

        [ForeignKey(nameof(IdYeuCau))]
        public virtual KsSurveyRequest SurveyRequest { get; set; }
    }
}
