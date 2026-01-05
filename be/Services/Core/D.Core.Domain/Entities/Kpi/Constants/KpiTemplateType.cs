using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Kpi.Constants
{
    public static class KpiTemplateTypes
    {
        public static readonly TemplateTypeDto PhoHieuTruong = new()
        {
            Value = "1",
            Name = "PHO_HIEU_TRUONG"
        };

        public static readonly TemplateTypeDto ChuyenVienCoSV = new()
        {
            Value = "2",
            Name = "CHUYEN_VIEN_CO_SINH_VIEN_KHOA_CHUYEN_NGANH"
        };

        public static readonly TemplateTypeDto ChuyenVienKhongSV = new()
        {
            Value = "3",
            Name = "CHUYEN_VIEN_KHOA_KHONG_CO_SINH_VIEN_CHUYEN_NGANH"
        };

        public static readonly TemplateTypeDto GiangVien = new()
        {
            Value = "4",
            Name = "GIANG_VIEN"
        };

        public static readonly TemplateTypeDto GiangVienKiemNhiem = new()
        {
            Value = "5",
            Name = "GIANG_VIEN_KIEM_NHIEM"
        };

        public static readonly TemplateTypeDto GiangVienMoi = new()
        {
            Value = "6",
            Name = "GIANG_VIEN_MOI_VAO"
        };

        public static readonly TemplateTypeDto KySu = new()
        {
            Value = "7",
            Name = "KY_SU"
        };

        public static readonly TemplateTypeDto HieuTruong = new()
        {
            Value = "8",
            Name = "HIEU_TRUONG"
        };

        public static IReadOnlyList<TemplateTypeDto> All =>
            new List<TemplateTypeDto>
            {
            PhoHieuTruong,
            ChuyenVienCoSV,
            ChuyenVienKhongSV,
            GiangVien,
            GiangVienKiemNhiem,
            GiangVienMoi,
            KySu,
            HieuTruong
            };
    }

}
