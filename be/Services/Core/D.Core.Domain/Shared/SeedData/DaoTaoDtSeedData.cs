using D.Core.Domain.Entities.DaoTao;
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
            modelBuilder.SeedDataDtMonTienQuyet();
            modelBuilder.SeedDataDtChuongTrinhKhung();
            modelBuilder.SeedDataDtChuongTrinhKhungMon();
            modelBuilder.SeedDataDtQuyDinhThangDiem();
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
                    TenTiengAnh = "Artificial Intelligence",
                    NganhId = 1,
                },
                new DtChuyenNganh
                {
                    Id = 4,
                    MaChuyenNganh = "MMT",
                    TenChuyenNganh = "Mạng máy tính và Truyền thông",
                    TenTiengAnh = "Computer Networks and Communications",
                    NganhId = 1,
                },
                new DtChuyenNganh
                {
                    Id = 5,
                    MaChuyenNganh = "ATTT",
                    TenChuyenNganh = "An toàn thông tin",
                    TenTiengAnh = "Information Security",
                    NganhId = 1,
                },
                new DtChuyenNganh
                {
                    Id = 6,
                    MaChuyenNganh = "KHMT",
                    TenChuyenNganh = "Khoa học máy tính",
                    TenTiengAnh = "Computer Science",
                    NganhId = 2,
                },
                new DtChuyenNganh
                {
                    Id = 7,
                    MaChuyenNganh = "KHDL",
                    TenChuyenNganh = "Khoa học dữ liệu",
                    TenTiengAnh = "Data Science",
                    NganhId = 2,
                }
            );
        }

        public static void SeedDataDtMon(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtMonHoc>().HasData(
                // Môn cơ sở ngành
                new DtMonHoc { Id = 1, MaMonHoc = "IT1110", TenMonHoc = "Tin học đại cương", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 2, MaMonHoc = "IT3040", TenMonHoc = "Kỹ thuật lập trình", SoTinChi = 3, SoTietLyThuyet = 45, SoTietThucHanh = 15 },
                new DtMonHoc { Id = 3, MaMonHoc = "IT3090", TenMonHoc = "Cơ sở dữ liệu", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 4, MaMonHoc = "MI1111", TenMonHoc = "Giải tích 1", SoTinChi = 4, SoTietLyThuyet = 60, SoTietThucHanh = 0 },
                new DtMonHoc { Id = 5, MaMonHoc = "MI1121", TenMonHoc = "Giải tích 2", SoTinChi = 3, SoTietLyThuyet = 45, SoTietThucHanh = 0 },
                new DtMonHoc { Id = 6, MaMonHoc = "MI1131", TenMonHoc = "Đại số tuyến tính", SoTinChi = 3, SoTietLyThuyet = 45, SoTietThucHanh = 0 },
                new DtMonHoc { Id = 7, MaMonHoc = "MI2020", TenMonHoc = "Xác suất thống kê", SoTinChi = 3, SoTietLyThuyet = 45, SoTietThucHanh = 0 },
                new DtMonHoc { Id = 8, MaMonHoc = "PH1110", TenMonHoc = "Vật lý đại cương", SoTinChi = 3, SoTietLyThuyet = 45, SoTietThucHanh = 0 },
                new DtMonHoc { Id = 9, MaMonHoc = "IT2010", TenMonHoc = "Cấu trúc dữ liệu và giải thuật", SoTinChi = 4, SoTietLyThuyet = 45, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 10, MaMonHoc = "IT2020", TenMonHoc = "Lập trình hướng đối tượng", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 11, MaMonHoc = "IT2030", TenMonHoc = "Kiến trúc máy tính", SoTinChi = 3, SoTietLyThuyet = 45, SoTietThucHanh = 0 },
                new DtMonHoc { Id = 12, MaMonHoc = "IT2040", TenMonHoc = "Hệ điều hành", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 13, MaMonHoc = "IT3010", TenMonHoc = "Mạng máy tính", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 14, MaMonHoc = "IT3020", TenMonHoc = "Nguyên lý ngôn ngữ lập trình", SoTinChi = 3, SoTietLyThuyet = 45, SoTietThucHanh = 0 },

                // Môn chuyên ngành KTPM
                new DtMonHoc { Id = 15, MaMonHoc = "IT4010", TenMonHoc = "Công nghệ phần mềm", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 16, MaMonHoc = "IT4011", TenMonHoc = "Phân tích thiết kế hệ thống", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 17, MaMonHoc = "IT4012", TenMonHoc = "Kiểm thử phần mềm", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 18, MaMonHoc = "IT4013", TenMonHoc = "Quản lý dự án phần mềm", SoTinChi = 3, SoTietLyThuyet = 45, SoTietThucHanh = 0 },
                new DtMonHoc { Id = 19, MaMonHoc = "IT4014", TenMonHoc = "Phát triển ứng dụng Web", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 20, MaMonHoc = "IT4015", TenMonHoc = "Phát triển ứng dụng di động", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },

                // Môn chuyên ngành HTTT
                new DtMonHoc { Id = 21, MaMonHoc = "IT4020", TenMonHoc = "Hệ quản trị cơ sở dữ liệu", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 22, MaMonHoc = "IT4021", TenMonHoc = "Phân tích dữ liệu", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 23, MaMonHoc = "IT4022", TenMonHoc = "Kho dữ liệu và OLAP", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 24, MaMonHoc = "IT4023", TenMonHoc = "Hệ thống thông tin quản lý", SoTinChi = 3, SoTietLyThuyet = 45, SoTietThucHanh = 0 },

                // Môn chuyên ngành AI
                new DtMonHoc { Id = 25, MaMonHoc = "IT4030", TenMonHoc = "Nhập môn Trí tuệ nhân tạo", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 26, MaMonHoc = "IT4031", TenMonHoc = "Học máy", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 27, MaMonHoc = "IT4032", TenMonHoc = "Học sâu", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 28, MaMonHoc = "IT4033", TenMonHoc = "Xử lý ngôn ngữ tự nhiên", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 29, MaMonHoc = "IT4034", TenMonHoc = "Thị giác máy tính", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },

                // Môn chuyên ngành MMT
                new DtMonHoc { Id = 30, MaMonHoc = "IT4040", TenMonHoc = "An ninh mạng", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 31, MaMonHoc = "IT4041", TenMonHoc = "Quản trị mạng", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 32, MaMonHoc = "IT4042", TenMonHoc = "Điện toán đám mây", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 33, MaMonHoc = "IT4043", TenMonHoc = "Internet of Things", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },

                // Môn chuyên ngành ATTT
                new DtMonHoc { Id = 34, MaMonHoc = "IT4050", TenMonHoc = "Mã hóa và bảo mật thông tin", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 35, MaMonHoc = "IT4051", TenMonHoc = "Bảo mật hệ thống", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 36, MaMonHoc = "IT4052", TenMonHoc = "Kiểm thử xâm nhập", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },
                new DtMonHoc { Id = 37, MaMonHoc = "IT4053", TenMonHoc = "Pháp chứng kỹ thuật số", SoTinChi = 3, SoTietLyThuyet = 30, SoTietThucHanh = 30 },

                // Môn đồ án
                new DtMonHoc { Id = 38, MaMonHoc = "IT5010", TenMonHoc = "Đồ án chuyên ngành", SoTinChi = 3, SoTietLyThuyet = 0, SoTietThucHanh = 90 },
                new DtMonHoc { Id = 39, MaMonHoc = "IT5020", TenMonHoc = "Đồ án tốt nghiệp", SoTinChi = 10, SoTietLyThuyet = 0, SoTietThucHanh = 300 }
            );
        }

        public static void SeedDataDtMonTienQuyet(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtMonTienQuyet>().HasData(
                // Giải tích 2 yêu cầu Giải tích 1
                new DtMonTienQuyet { Id = 1, MonHocId = 5, MonTienQuyetId = 4, LoaiDieuKien = 1 },
                // Cấu trúc dữ liệu yêu cầu Kỹ thuật lập trình
                new DtMonTienQuyet { Id = 2, MonHocId = 9, MonTienQuyetId = 2, LoaiDieuKien = 1 },
                // LT hướng đối tượng yêu cầu Kỹ thuật lập trình
                new DtMonTienQuyet { Id = 3, MonHocId = 10, MonTienQuyetId = 2, LoaiDieuKien = 1 },
                // Hệ điều hành yêu cầu Kiến trúc máy tính
                new DtMonTienQuyet { Id = 4, MonHocId = 12, MonTienQuyetId = 11, LoaiDieuKien = 1 },
                // Mạng máy tính yêu cầu Hệ điều hành
                new DtMonTienQuyet { Id = 5, MonHocId = 13, MonTienQuyetId = 12, LoaiDieuKien = 1 },
                // Công nghệ phần mềm yêu cầu LT hướng đối tượng
                new DtMonTienQuyet { Id = 6, MonHocId = 15, MonTienQuyetId = 10, LoaiDieuKien = 1 },
                // Phân tích thiết kế HT yêu cầu Công nghệ phần mềm
                new DtMonTienQuyet { Id = 7, MonHocId = 16, MonTienQuyetId = 15, LoaiDieuKien = 1 },
                // Kiểm thử PM yêu cầu Công nghệ phần mềm
                new DtMonTienQuyet { Id = 8, MonHocId = 17, MonTienQuyetId = 15, LoaiDieuKien = 1 },
                // Phát triển Web yêu cầu LT hướng đối tượng và CSDL
                new DtMonTienQuyet { Id = 9, MonHocId = 19, MonTienQuyetId = 10, LoaiDieuKien = 1 },
                new DtMonTienQuyet { Id = 10, MonHocId = 19, MonTienQuyetId = 3, LoaiDieuKien = 1 },
                // Học máy yêu cầu Xác suất thống kê
                new DtMonTienQuyet { Id = 11, MonHocId = 26, MonTienQuyetId = 7, LoaiDieuKien = 1 },
                // Học sâu yêu cầu Học máy
                new DtMonTienQuyet { Id = 12, MonHocId = 27, MonTienQuyetId = 26, LoaiDieuKien = 1 },
                // An ninh mạng yêu cầu Mạng máy tính
                new DtMonTienQuyet { Id = 13, MonHocId = 30, MonTienQuyetId = 13, LoaiDieuKien = 1 },
                // Mã hóa bảo mật yêu cầu Đại số tuyến tính
                new DtMonTienQuyet { Id = 14, MonHocId = 34, MonTienQuyetId = 6, LoaiDieuKien = 1 },
                // Bảo mật hệ thống yêu cầu An ninh mạng
                new DtMonTienQuyet { Id = 15, MonHocId = 35, MonTienQuyetId = 30, LoaiDieuKien = 1 }
            );
        }

        public static void SeedDataDtChuongTrinhKhung(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtChuongTrinhKhung>().HasData(
                // Chương trình khung cho chuyên ngành Kỹ thuật phần mềm
                new DtChuongTrinhKhung { Id = 1, MaChuongTrinhKhung = "KTPM-K68", TenChuongTrinhKhung = "Chương trình đào tạo Kỹ thuật phần mềm K68", TongSoTinChi = 150, KhoaHocId = 1, NganhId = 1, ChuyenNganhId = 1 },
                // Chương trình khung cho chuyên ngành Hệ thống thông tin
                new DtChuongTrinhKhung { Id = 2, MaChuongTrinhKhung = "HTTT-K68", TenChuongTrinhKhung = "Chương trình đào tạo Hệ thống thông tin K68", TongSoTinChi = 150, KhoaHocId = 1, NganhId = 1, ChuyenNganhId = 2 },
                // Chương trình khung cho chuyên ngành Trí tuệ nhân tạo
                new DtChuongTrinhKhung { Id = 3, MaChuongTrinhKhung = "AI-K68", TenChuongTrinhKhung = "Chương trình đào tạo Trí tuệ nhân tạo K68", TongSoTinChi = 150, KhoaHocId = 1, NganhId = 1, ChuyenNganhId = 3 },
                // Chương trình khung cho chuyên ngành Mạng máy tính
                new DtChuongTrinhKhung { Id = 4, MaChuongTrinhKhung = "MMT-K68", TenChuongTrinhKhung = "Chương trình đào tạo Mạng máy tính K68", TongSoTinChi = 150, KhoaHocId = 1, NganhId = 1, ChuyenNganhId = 4 },
                // Chương trình khung cho chuyên ngành An toàn thông tin
                new DtChuongTrinhKhung { Id = 5, MaChuongTrinhKhung = "ATTT-K68", TenChuongTrinhKhung = "Chương trình đào tạo An toàn thông tin K68", TongSoTinChi = 150, KhoaHocId = 1, NganhId = 1, ChuyenNganhId = 5 }
            );
        }

        public static void SeedDataDtChuongTrinhKhungMon(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtChuongTrinhKhungMon>().HasData(
                // ============ CTK 1: Kỹ thuật phần mềm ============
                // Học kỳ 1
                new DtChuongTrinhKhungMon { Id = 1, ChuongTrinhKhungId = 1, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024" },   // Tin học đại cương
                new DtChuongTrinhKhungMon { Id = 2, ChuongTrinhKhungId = 1, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024" },   // Giải tích 1
                new DtChuongTrinhKhungMon { Id = 3, ChuongTrinhKhungId = 1, MonHocId = 6, HocKy = 1, NamHoc = "2023-2024" },   // Đại số tuyến tính
                new DtChuongTrinhKhungMon { Id = 4, ChuongTrinhKhungId = 1, MonHocId = 8, HocKy = 1, NamHoc = "2023-2024" },   // Vật lý đại cương
                // Học kỳ 2
                new DtChuongTrinhKhungMon { Id = 5, ChuongTrinhKhungId = 1, MonHocId = 2, HocKy = 2, NamHoc = "2023-2024" },   // Kỹ thuật lập trình
                new DtChuongTrinhKhungMon { Id = 6, ChuongTrinhKhungId = 1, MonHocId = 5, HocKy = 2, NamHoc = "2023-2024" },   // Giải tích 2
                new DtChuongTrinhKhungMon { Id = 7, ChuongTrinhKhungId = 1, MonHocId = 7, HocKy = 2, NamHoc = "2023-2024" },   // Xác suất thống kê
                new DtChuongTrinhKhungMon { Id = 8, ChuongTrinhKhungId = 1, MonHocId = 11, HocKy = 2, NamHoc = "2023-2024" },  // Kiến trúc máy tính
                // Học kỳ 3
                new DtChuongTrinhKhungMon { Id = 9, ChuongTrinhKhungId = 1, MonHocId = 9, HocKy = 3, NamHoc = "2024-2025" },   // CTDL & Giải thuật
                new DtChuongTrinhKhungMon { Id = 10, ChuongTrinhKhungId = 1, MonHocId = 10, HocKy = 3, NamHoc = "2024-2025" }, // LT hướng đối tượng
                new DtChuongTrinhKhungMon { Id = 11, ChuongTrinhKhungId = 1, MonHocId = 3, HocKy = 3, NamHoc = "2024-2025" },  // Cơ sở dữ liệu
                new DtChuongTrinhKhungMon { Id = 12, ChuongTrinhKhungId = 1, MonHocId = 12, HocKy = 3, NamHoc = "2024-2025" }, // Hệ điều hành
                // Học kỳ 4
                new DtChuongTrinhKhungMon { Id = 13, ChuongTrinhKhungId = 1, MonHocId = 13, HocKy = 4, NamHoc = "2024-2025" }, // Mạng máy tính
                new DtChuongTrinhKhungMon { Id = 14, ChuongTrinhKhungId = 1, MonHocId = 15, HocKy = 4, NamHoc = "2024-2025" }, // Công nghệ phần mềm
                new DtChuongTrinhKhungMon { Id = 15, ChuongTrinhKhungId = 1, MonHocId = 14, HocKy = 4, NamHoc = "2024-2025" }, // Nguyên lý NNLT
                // Học kỳ 5 - Chuyên ngành KTPM
                new DtChuongTrinhKhungMon { Id = 16, ChuongTrinhKhungId = 1, MonHocId = 16, HocKy = 5, NamHoc = "2025-2026" }, // Phân tích thiết kế HT
                new DtChuongTrinhKhungMon { Id = 17, ChuongTrinhKhungId = 1, MonHocId = 17, HocKy = 5, NamHoc = "2025-2026" }, // Kiểm thử phần mềm
                new DtChuongTrinhKhungMon { Id = 18, ChuongTrinhKhungId = 1, MonHocId = 19, HocKy = 5, NamHoc = "2025-2026" }, // Phát triển Web
                // Học kỳ 6 - Chuyên ngành KTPM
                new DtChuongTrinhKhungMon { Id = 19, ChuongTrinhKhungId = 1, MonHocId = 18, HocKy = 6, NamHoc = "2025-2026" }, // Quản lý dự án PM
                new DtChuongTrinhKhungMon { Id = 20, ChuongTrinhKhungId = 1, MonHocId = 20, HocKy = 6, NamHoc = "2025-2026" }, // Phát triển ứng dụng mobile
                new DtChuongTrinhKhungMon { Id = 21, ChuongTrinhKhungId = 1, MonHocId = 38, HocKy = 6, NamHoc = "2025-2026" }, // Đồ án chuyên ngành
                // Học kỳ 7
                new DtChuongTrinhKhungMon { Id = 22, ChuongTrinhKhungId = 1, MonHocId = 39, HocKy = 7, NamHoc = "2026-2027" }, // Đồ án tốt nghiệp

                // ============ CTK 2: Hệ thống thông tin ============
                // Học kỳ 1-4 giống CTK 1 (môn cơ sở)
                new DtChuongTrinhKhungMon { Id = 23, ChuongTrinhKhungId = 2, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 24, ChuongTrinhKhungId = 2, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 25, ChuongTrinhKhungId = 2, MonHocId = 6, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 26, ChuongTrinhKhungId = 2, MonHocId = 2, HocKy = 2, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 27, ChuongTrinhKhungId = 2, MonHocId = 7, HocKy = 2, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 28, ChuongTrinhKhungId = 2, MonHocId = 9, HocKy = 3, NamHoc = "2024-2025" },
                new DtChuongTrinhKhungMon { Id = 29, ChuongTrinhKhungId = 2, MonHocId = 10, HocKy = 3, NamHoc = "2024-2025" },
                new DtChuongTrinhKhungMon { Id = 30, ChuongTrinhKhungId = 2, MonHocId = 3, HocKy = 3, NamHoc = "2024-2025" },
                // Học kỳ 5-6 - Chuyên ngành HTTT
                new DtChuongTrinhKhungMon { Id = 31, ChuongTrinhKhungId = 2, MonHocId = 21, HocKy = 5, NamHoc = "2025-2026" }, // Hệ QTCSDL
                new DtChuongTrinhKhungMon { Id = 32, ChuongTrinhKhungId = 2, MonHocId = 22, HocKy = 5, NamHoc = "2025-2026" }, // Phân tích dữ liệu
                new DtChuongTrinhKhungMon { Id = 33, ChuongTrinhKhungId = 2, MonHocId = 23, HocKy = 6, NamHoc = "2025-2026" }, // Kho dữ liệu
                new DtChuongTrinhKhungMon { Id = 34, ChuongTrinhKhungId = 2, MonHocId = 24, HocKy = 6, NamHoc = "2025-2026" }, // HTTT quản lý
                new DtChuongTrinhKhungMon { Id = 35, ChuongTrinhKhungId = 2, MonHocId = 38, HocKy = 6, NamHoc = "2025-2026" },
                new DtChuongTrinhKhungMon { Id = 36, ChuongTrinhKhungId = 2, MonHocId = 39, HocKy = 7, NamHoc = "2026-2027" },

                // ============ CTK 3: Trí tuệ nhân tạo ============
                new DtChuongTrinhKhungMon { Id = 37, ChuongTrinhKhungId = 3, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 38, ChuongTrinhKhungId = 3, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 39, ChuongTrinhKhungId = 3, MonHocId = 6, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 40, ChuongTrinhKhungId = 3, MonHocId = 2, HocKy = 2, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 41, ChuongTrinhKhungId = 3, MonHocId = 7, HocKy = 2, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 42, ChuongTrinhKhungId = 3, MonHocId = 9, HocKy = 3, NamHoc = "2024-2025" },
                new DtChuongTrinhKhungMon { Id = 43, ChuongTrinhKhungId = 3, MonHocId = 10, HocKy = 3, NamHoc = "2024-2025" },
                // Chuyên ngành AI
                new DtChuongTrinhKhungMon { Id = 44, ChuongTrinhKhungId = 3, MonHocId = 25, HocKy = 5, NamHoc = "2025-2026" }, // Nhập môn AI
                new DtChuongTrinhKhungMon { Id = 45, ChuongTrinhKhungId = 3, MonHocId = 26, HocKy = 5, NamHoc = "2025-2026" }, // Học máy
                new DtChuongTrinhKhungMon { Id = 46, ChuongTrinhKhungId = 3, MonHocId = 27, HocKy = 6, NamHoc = "2025-2026" }, // Học sâu
                new DtChuongTrinhKhungMon { Id = 47, ChuongTrinhKhungId = 3, MonHocId = 28, HocKy = 6, NamHoc = "2025-2026" }, // NLP
                new DtChuongTrinhKhungMon { Id = 48, ChuongTrinhKhungId = 3, MonHocId = 29, HocKy = 6, NamHoc = "2025-2026" }, // Thị giác máy tính
                new DtChuongTrinhKhungMon { Id = 49, ChuongTrinhKhungId = 3, MonHocId = 38, HocKy = 6, NamHoc = "2025-2026" },
                new DtChuongTrinhKhungMon { Id = 50, ChuongTrinhKhungId = 3, MonHocId = 39, HocKy = 7, NamHoc = "2026-2027" },

                // ============ CTK 4: Mạng máy tính ============
                new DtChuongTrinhKhungMon { Id = 51, ChuongTrinhKhungId = 4, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 52, ChuongTrinhKhungId = 4, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 53, ChuongTrinhKhungId = 4, MonHocId = 2, HocKy = 2, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 54, ChuongTrinhKhungId = 4, MonHocId = 11, HocKy = 2, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 55, ChuongTrinhKhungId = 4, MonHocId = 12, HocKy = 3, NamHoc = "2024-2025" },
                new DtChuongTrinhKhungMon { Id = 56, ChuongTrinhKhungId = 4, MonHocId = 13, HocKy = 4, NamHoc = "2024-2025" },
                // Chuyên ngành MMT
                new DtChuongTrinhKhungMon { Id = 57, ChuongTrinhKhungId = 4, MonHocId = 30, HocKy = 5, NamHoc = "2025-2026" }, // An ninh mạng
                new DtChuongTrinhKhungMon { Id = 58, ChuongTrinhKhungId = 4, MonHocId = 31, HocKy = 5, NamHoc = "2025-2026" }, // Quản trị mạng
                new DtChuongTrinhKhungMon { Id = 59, ChuongTrinhKhungId = 4, MonHocId = 32, HocKy = 6, NamHoc = "2025-2026" }, // Cloud
                new DtChuongTrinhKhungMon { Id = 60, ChuongTrinhKhungId = 4, MonHocId = 33, HocKy = 6, NamHoc = "2025-2026" }, // IoT
                new DtChuongTrinhKhungMon { Id = 61, ChuongTrinhKhungId = 4, MonHocId = 38, HocKy = 6, NamHoc = "2025-2026" },
                new DtChuongTrinhKhungMon { Id = 62, ChuongTrinhKhungId = 4, MonHocId = 39, HocKy = 7, NamHoc = "2026-2027" },

                // ============ CTK 5: An toàn thông tin ============
                new DtChuongTrinhKhungMon { Id = 63, ChuongTrinhKhungId = 5, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 64, ChuongTrinhKhungId = 5, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 65, ChuongTrinhKhungId = 5, MonHocId = 6, HocKy = 1, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 66, ChuongTrinhKhungId = 5, MonHocId = 2, HocKy = 2, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 67, ChuongTrinhKhungId = 5, MonHocId = 11, HocKy = 2, NamHoc = "2023-2024" },
                new DtChuongTrinhKhungMon { Id = 68, ChuongTrinhKhungId = 5, MonHocId = 12, HocKy = 3, NamHoc = "2024-2025" },
                new DtChuongTrinhKhungMon { Id = 69, ChuongTrinhKhungId = 5, MonHocId = 13, HocKy = 4, NamHoc = "2024-2025" },
                // Chuyên ngành ATTT
                new DtChuongTrinhKhungMon { Id = 70, ChuongTrinhKhungId = 5, MonHocId = 30, HocKy = 5, NamHoc = "2025-2026" }, // An ninh mạng
                new DtChuongTrinhKhungMon { Id = 71, ChuongTrinhKhungId = 5, MonHocId = 34, HocKy = 5, NamHoc = "2025-2026" }, // Mã hóa bảo mật
                new DtChuongTrinhKhungMon { Id = 72, ChuongTrinhKhungId = 5, MonHocId = 35, HocKy = 6, NamHoc = "2025-2026" }, // Bảo mật HT
                new DtChuongTrinhKhungMon { Id = 73, ChuongTrinhKhungId = 5, MonHocId = 36, HocKy = 6, NamHoc = "2025-2026" }, // Kiểm thử xâm nhập
                new DtChuongTrinhKhungMon { Id = 74, ChuongTrinhKhungId = 5, MonHocId = 37, HocKy = 6, NamHoc = "2025-2026" }, // Pháp chứng số
                new DtChuongTrinhKhungMon { Id = 75, ChuongTrinhKhungId = 5, MonHocId = 38, HocKy = 6, NamHoc = "2025-2026" },
                new DtChuongTrinhKhungMon { Id = 76, ChuongTrinhKhungId = 5, MonHocId = 39, HocKy = 7, NamHoc = "2026-2027" }
            );
        }

        public static void SeedDataDtQuyDinhThangDiem(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtQuyDinhThangDiem>().HasData(
                new DtQuyDinhThangDiem { Id = 1, DiemChu = "A+", DiemSoMin = 9.0m, DiemSoMax = 10.0m, DiemHe4 = 4.0m, Mota = "Xuất sắc", TrangThai = true },
                new DtQuyDinhThangDiem { Id = 2, DiemChu = "A", DiemSoMin = 8.5m, DiemSoMax = 8.9m, DiemHe4 = 3.7m, Mota = "Giỏi", TrangThai = true },
                new DtQuyDinhThangDiem { Id = 3, DiemChu = "B+", DiemSoMin = 8.0m, DiemSoMax = 8.4m, DiemHe4 = 3.5m, Mota = "Khá giỏi", TrangThai = true },
                new DtQuyDinhThangDiem { Id = 4, DiemChu = "B", DiemSoMin = 7.0m, DiemSoMax = 7.9m, DiemHe4 = 3.0m, Mota = "Khá", TrangThai = true },
                new DtQuyDinhThangDiem { Id = 5, DiemChu = "C+", DiemSoMin = 6.5m, DiemSoMax = 6.9m, DiemHe4 = 2.5m, Mota = "Trung bình khá", TrangThai = true },
                new DtQuyDinhThangDiem { Id = 6, DiemChu = "C", DiemSoMin = 5.5m, DiemSoMax = 6.4m, DiemHe4 = 2.0m, Mota = "Trung bình", TrangThai = true },
                new DtQuyDinhThangDiem { Id = 7, DiemChu = "D+", DiemSoMin = 5.0m, DiemSoMax = 5.4m, DiemHe4 = 1.5m, Mota = "Yếu", TrangThai = true },
                new DtQuyDinhThangDiem { Id = 8, DiemChu = "D", DiemSoMin = 4.0m, DiemSoMax = 4.9m, DiemHe4 = 1.0m, Mota = "Kém", TrangThai = true },
                new DtQuyDinhThangDiem { Id = 9, DiemChu = "F", DiemSoMin = 0.0m, DiemSoMax = 3.9m, DiemHe4 = 0.0m, Mota = "Không đạt", TrangThai = true }
            );
        }
    }
}  
