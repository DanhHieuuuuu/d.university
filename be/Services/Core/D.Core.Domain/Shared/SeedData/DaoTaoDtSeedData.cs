using D.Core.Domain.Entities.DaoTao;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Shared.SeedData
{
    public static class DaoTaoDtSeedData
    {
        public static void SeedDataDt(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedDataDtKhoa();
            modelBuilder.SeedDataDtNganh();
            modelBuilder.SeedDataDtChuyenNganh();
            modelBuilder.SeedDataDtMon();
            modelBuilder.SeedDataDtChuongTrinhKhung();
            modelBuilder.SeedDataDtChuongTrinhKhungMon();

        }

        public static void SeedDataDtKhoa(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtKhoa>().HasData(
                new DtKhoa
                {
                    Id = 1,
                    MaKhoa = "CNTT",
                    TenKhoa = "Công nghệ Thông tin",
                    TenTiengAnh = "Information Technology",
                    VietTat = "FIT",
                    Email = "fit@huce.edu.vn",
                },
                new DtKhoa
                {
                    Id = 2,
                    MaKhoa = "KT",
                    TenKhoa = "Kinh tế & Quản lý",
                    TenTiengAnh = "Economics and Management",
                    VietTat = "FEM",
                    Email = "ktql@huce.edu.vn",
                }
            );
        }

        public static void SeedDataDtNganh(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtNganh>().HasData(
                new DtNganh
                {
                    Id = 1,
                    MaNganh = "7480201",
                    TenNganh = "Công nghệ thông tin",
                    TenTiengAnh = "Information Technology",
                    KhoaId = 1,
                },
                new DtNganh
                {
                    Id = 2,
                    MaNganh = "7480101",
                    TenNganh = "Khoa học máy tính",
                    TenTiengAnh = "Computer Science",
                    KhoaId = 1,
                },
                new DtNganh
                {
                    Id = 3,
                    MaNganh = "7340201",
                    TenNganh = "Tài chính - Ngân hàng",
                    KhoaId = 2,
                }
            );
        }

        public static void SeedDataDtChuyenNganh(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtChuyenNganh>().HasData(
                new DtChuyenNganh
                {
                    Id = 1,
                    MaChuyenNganh = "KTPM",
                    TenChuyenNganh = "Kỹ thuật phần mềm",
                    TenTiengAnh = "Software Engineering",
                    NganhId = 1,
                },
                new DtChuyenNganh
                {
                    Id = 2,
                    MaChuyenNganh = "HTTT",
                    TenChuyenNganh = "Hệ thống thông tin",
                    TenTiengAnh = "Information Systems",
                    NganhId = 1,
                },
                new DtChuyenNganh
                {
                    Id = 3,
                    MaChuyenNganh = "AI",
                    TenChuyenNganh = "Trí tuệ nhân tạo",
                    NganhId = 2,
                }
            );
        }

        public static void SeedDataDtMon(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtMonHoc>().HasData(
                new DtMonHoc
                {
                    Id = 1,
                    MaMonHoc = "IT1110",
                    TenMonHoc = "Tin học đại cương",
                    SoTinChi = 3,
                    SoTietLyThuyet = 30,
                    SoTietThucHanh = 30,
                },
                new DtMonHoc
                {
                    Id = 2,
                    MaMonHoc = "IT3040",
                    TenMonHoc = "Kỹ thuật lập trình",
                    SoTinChi = 3,
                    SoTietLyThuyet = 45,
                    SoTietThucHanh = 0,
                },
                new DtMonHoc
                {
                    Id = 3,
                    MaMonHoc = "IT3090",
                    TenMonHoc = "Cơ sở dữ liệu",
                    SoTinChi = 3,
                    SoTietLyThuyet = 45,
                    SoTietThucHanh = 15,
                },
                new DtMonHoc
                {
                    Id = 4,
                    MaMonHoc = "MI1111",
                    TenMonHoc = "Giải tích 1",
                    SoTinChi = 4,
                    SoTietLyThuyet = 60,
                }
            );
        }

        public static void SeedDataDtChuongTrinhKhung(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtChuongTrinhKhung>().HasData(
                new DtChuongTrinhKhung
                {
                    Id = 1,
                    MaChuongTrinhKhung = "CNTT-K66-KS",
                    TenChuongTrinhKhung = "Chương trình chuẩn Kỹ sư CNTT K66",
                    TongSoTinChi = 150,
                    KhoaHocId = 1,
                    NganhId = 1,
                    ChuyenNganhId = 2,
                },
                new DtChuongTrinhKhung
                {
                    Id = 2,
                    MaChuongTrinhKhung = "KTPM-K67-CLC",
                    TenChuongTrinhKhung = "Kỹ sư KTPM Chất lượng cao K67",
                    TongSoTinChi = 160,
                    KhoaHocId = 2,
                    NganhId = 1,
                    ChuyenNganhId = 1,
                }
            );
        }

        public static void SeedDataDtChuongTrinhKhungMon(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtChuongTrinhKhungMon>().HasData(
                new DtChuongTrinhKhungMon { Id = 1, ChuongTrinhKhungId = 1, MonHocId = 1, HocKy = "2021-2022"},
                new DtChuongTrinhKhungMon { Id = 2, ChuongTrinhKhungId = 1, MonHocId = 4, HocKy = "2021-2022"},
                new DtChuongTrinhKhungMon { Id = 3, ChuongTrinhKhungId = 1, MonHocId = 2, HocKy = "2021-2022"},
                new DtChuongTrinhKhungMon { Id = 4, ChuongTrinhKhungId = 1, MonHocId = 3, HocKy = "2021-2022"}
            );
        }
    }
}  
