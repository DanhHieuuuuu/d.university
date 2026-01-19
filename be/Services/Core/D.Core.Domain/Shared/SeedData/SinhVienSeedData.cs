using D.Core.Domain.Entities.SinhVien;
using Microsoft.EntityFrameworkCore;

namespace D.Core.Domain.Shared.SeedData
{
    public static class SinhVienSeedData
    {
        public static void SeedDataSv(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedDataSvSinhVien();
            modelBuilder.SeedDataSvDiemMonHoc();
            modelBuilder.SeedDataSvKetQuaHocKy();
            modelBuilder.SeedDataSvThongTinHocVu();
        }

        public static void SeedDataSvSinhVien(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SvSinhVien>().HasData(
                new SvSinhVien
                {
                    Id = 100,
                    Mssv = "SV2023001",
                    HoDem = "Nguyễn Văn",
                    Ten = "An",
                    NgaySinh = new DateTime(2004, 5, 15),
                    NoiSinh = "Hà Nội",
                    GioiTinh = 1,
                    QuocTich = 1,
                    DanToc = 1,
                    TonGiao = 1,
                    SoCccd = "001204012345",
                    SoDienThoai = "0901234567",
                    Email = "nguyenvanan2004@gmail.com",
                    Email2 = "an.nv@student.duniversity.edu.vn",
                    KhoaHoc = 1,
                    Khoa = 1,
                    Nganh = 1,
                    ChuyenNganh = 1,
                    TrangThaiHoc = 1,
                    NguyenQuan = "Quận Thanh Xuân, Hà Nội",
                    NoiOHienTai = "Số 123, Đường Nguyễn Trãi, Quận Thanh Xuân, Hà Nội"
                },
                new SvSinhVien
                {
                    Id = 101,
                    Mssv = "SV2023002",
                    HoDem = "Trần Thị",
                    Ten = "Bích",
                    NgaySinh = new DateTime(2004, 8, 22),
                    NoiSinh = "Hải Phòng",
                    GioiTinh = 2,
                    QuocTich = 1,
                    DanToc = 1,
                    TonGiao = 2,
                    SoCccd = "001204023456",
                    SoDienThoai = "0912345678",
                    Email = "tranthibich2004@gmail.com",
                    Email2 = "bich.tt@student.duniversity.edu.vn",
                    KhoaHoc = 1,
                    Khoa = 1,
                    Nganh = 1,
                    ChuyenNganh = 2,
                    TrangThaiHoc = 1,
                    NguyenQuan = "Quận Ngô Quyền, Hải Phòng",
                    NoiOHienTai = "Số 45, Đường Lê Lợi, Quận Cầu Giấy, Hà Nội"
                },
                new SvSinhVien
                {
                    Id = 102,
                    Mssv = "SV2023003",
                    HoDem = "Lê Minh",
                    Ten = "Cường",
                    NgaySinh = new DateTime(2004, 2, 10),
                    NoiSinh = "Đà Nẵng",
                    GioiTinh = 1,
                    QuocTich = 1,
                    DanToc = 1,
                    TonGiao = 1,
                    SoCccd = "001204034567",
                    SoDienThoai = "0923456789",
                    Email = "leminhcuong2004@gmail.com",
                    Email2 = "cuong.lm@student.duniversity.edu.vn",
                    KhoaHoc = 1,
                    Khoa = 1,
                    Nganh = 1,
                    ChuyenNganh = 1,
                    TrangThaiHoc = 1,
                    NguyenQuan = "Quận Hải Châu, Đà Nẵng",
                    NoiOHienTai = "Số 78, Đường Trần Duy Hưng, Quận Cầu Giấy, Hà Nội"
                },
                new SvSinhVien
                {
                    Id = 103,
                    Mssv = "SV2023004",
                    HoDem = "Phạm Thu",
                    Ten = "Dung",
                    NgaySinh = new DateTime(2004, 11, 30),
                    NoiSinh = "Bắc Ninh",
                    GioiTinh = 2,
                    QuocTich = 1,
                    DanToc = 1,
                    TonGiao = 3,
                    SoCccd = "001204045678",
                    SoDienThoai = "0934567890",
                    Email = "phamthudung2004@gmail.com",
                    Email2 = "dung.pt@student.duniversity.edu.vn",
                    KhoaHoc = 1,
                    Khoa = 1,
                    Nganh = 1,
                    ChuyenNganh = 1,
                    TrangThaiHoc = 1,
                    NguyenQuan = "TP Bắc Ninh, Bắc Ninh",
                    NoiOHienTai = "Số 22, Đường Xuân Thủy, Quận Cầu Giấy, Hà Nội"
                },
                new SvSinhVien
                {
                    Id = 104,
                    Mssv = "SV2023005",
                    HoDem = "Hoàng Văn",
                    Ten = "Em",
                    NgaySinh = new DateTime(2004, 7, 18),
                    NoiSinh = "Nam Định",
                    GioiTinh = 1,
                    QuocTich = 1,
                    DanToc = 1,
                    TonGiao = 1,
                    SoCccd = "001204056789",
                    SoDienThoai = "0945678901",
                    Email = "hoangvanem2004@gmail.com",
                    Email2 = "em.hv@student.duniversity.edu.vn",
                    KhoaHoc = 1,
                    Khoa = 1,
                    Nganh = 1,
                    ChuyenNganh = 2,
                    TrangThaiHoc = 1,
                    NguyenQuan = "TP Nam Định, Nam Định",
                    NoiOHienTai = "Số 55, Đường Phạm Văn Đồng, Quận Bắc Từ Liêm, Hà Nội"
                }
            );
        }

        public static void SeedDataSvDiemMonHoc(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SvDiemMonHoc>().HasData(
                // SV100 - Nguyễn Văn An - HK1
                new SvDiemMonHoc { Id = 100, SinhVienId = 100, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 7.5m, DiemCuoiKy = 8.0m, DiemTongKet = 7.8m, DiemHe4 = 3.0m, DiemChu = "B", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 101, SinhVienId = 100, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 9.0m, DiemCuoiKy = 8.5m, DiemTongKet = 8.7m, DiemHe4 = 3.7m, DiemChu = "A", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 102, SinhVienId = 100, MonHocId = 2, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 8.5m, DiemCuoiKy = 8.0m, DiemTongKet = 8.2m, DiemHe4 = 3.5m, DiemChu = "B+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                // SV100 - HK2
                new SvDiemMonHoc { Id = 103, SinhVienId = 100, MonHocId = 3, HocKy = 2, NamHoc = "2023-2024", DiemQuaTrinh = 8.5m, DiemCuoiKy = 9.0m, DiemTongKet = 8.8m, DiemHe4 = 3.7m, DiemChu = "A", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },

                // SV101 - Trần Thị Bích - HK1
                new SvDiemMonHoc { Id = 104, SinhVienId = 101, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 9.0m, DiemCuoiKy = 9.5m, DiemTongKet = 9.3m, DiemHe4 = 4.0m, DiemChu = "A+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 105, SinhVienId = 101, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 8.0m, DiemCuoiKy = 8.5m, DiemTongKet = 8.3m, DiemHe4 = 3.5m, DiemChu = "B+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 106, SinhVienId = 101, MonHocId = 2, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 9.5m, DiemCuoiKy = 9.0m, DiemTongKet = 9.2m, DiemHe4 = 4.0m, DiemChu = "A+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                // SV101 - HK2
                new SvDiemMonHoc { Id = 107, SinhVienId = 101, MonHocId = 3, HocKy = 2, NamHoc = "2023-2024", DiemQuaTrinh = 9.5m, DiemCuoiKy = 9.5m, DiemTongKet = 9.5m, DiemHe4 = 4.0m, DiemChu = "A+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },

                // SV102 - Lê Minh Cường - HK1
                new SvDiemMonHoc { Id = 108, SinhVienId = 102, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 6.0m, DiemCuoiKy = 5.5m, DiemTongKet = 5.7m, DiemHe4 = 2.0m, DiemChu = "C", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 109, SinhVienId = 102, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 7.5m, DiemCuoiKy = 8.0m, DiemTongKet = 7.8m, DiemHe4 = 3.0m, DiemChu = "B", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 110, SinhVienId = 102, MonHocId = 2, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 6.5m, DiemCuoiKy = 7.0m, DiemTongKet = 6.8m, DiemHe4 = 2.5m, DiemChu = "C+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                // SV102 - HK2
                new SvDiemMonHoc { Id = 111, SinhVienId = 102, MonHocId = 3, HocKy = 2, NamHoc = "2023-2024", DiemQuaTrinh = 7.0m, DiemCuoiKy = 7.5m, DiemTongKet = 7.3m, DiemHe4 = 2.8m, DiemChu = "B", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },

                // SV103 - Phạm Thu Dung - HK1
                new SvDiemMonHoc { Id = 112, SinhVienId = 103, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 8.5m, DiemCuoiKy = 8.0m, DiemTongKet = 8.2m, DiemHe4 = 3.5m, DiemChu = "B+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 113, SinhVienId = 103, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 9.5m, DiemCuoiKy = 9.0m, DiemTongKet = 9.2m, DiemHe4 = 4.0m, DiemChu = "A+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 114, SinhVienId = 103, MonHocId = 2, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 8.0m, DiemCuoiKy = 8.5m, DiemTongKet = 8.3m, DiemHe4 = 3.5m, DiemChu = "B+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                // SV103 - HK2
                new SvDiemMonHoc { Id = 115, SinhVienId = 103, MonHocId = 3, HocKy = 2, NamHoc = "2023-2024", DiemQuaTrinh = 9.0m, DiemCuoiKy = 9.5m, DiemTongKet = 9.3m, DiemHe4 = 4.0m, DiemChu = "A+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },

                // SV104 - Hoàng Văn Em - HK1
                new SvDiemMonHoc { Id = 116, SinhVienId = 104, MonHocId = 4, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 7.0m, DiemCuoiKy = 7.5m, DiemTongKet = 7.3m, DiemHe4 = 2.8m, DiemChu = "B", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 117, SinhVienId = 104, MonHocId = 1, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 8.0m, DiemCuoiKy = 8.5m, DiemTongKet = 8.3m, DiemHe4 = 3.5m, DiemChu = "B+", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                new SvDiemMonHoc { Id = 118, SinhVienId = 104, MonHocId = 2, HocKy = 1, NamHoc = "2023-2024", DiemQuaTrinh = 7.5m, DiemCuoiKy = 7.0m, DiemTongKet = 7.2m, DiemHe4 = 2.8m, DiemChu = "B", KetQua = "Đạt", LanHoc = 1, GhiChu = "" },
                // SV104 - HK2
                new SvDiemMonHoc { Id = 119, SinhVienId = 104, MonHocId = 3, HocKy = 2, NamHoc = "2023-2024", DiemQuaTrinh = 7.5m, DiemCuoiKy = 8.0m, DiemTongKet = 7.8m, DiemHe4 = 3.0m, DiemChu = "B", KetQua = "Đạt", LanHoc = 1, GhiChu = "" }
            );
        }

        public static void SeedDataSvKetQuaHocKy(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SvKetQuaHocKy>().HasData(
                // SV100 - Nguyễn Văn An
                new SvKetQuaHocKy { Id = 100, SinhVienId = 100, HocKy = 1, NamHoc = "2023-2024", DiemTrungBinhHocKy = 8.23m, DiemTrungBinhTinhLuy = 8.23m, GpaTichLy = 3.4m, SoTinChiDat = 10, SoTinChiTichLuy = 10, SoTinChiNo = 0, XepLoaiHocKy = "Giỏi", XepLoaiRenLuyen = "Tốt", DiemRenLuyen = 85 },
                new SvKetQuaHocKy { Id = 101, SinhVienId = 100, HocKy = 2, NamHoc = "2023-2024", DiemTrungBinhHocKy = 8.8m, DiemTrungBinhTinhLuy = 8.52m, GpaTichLy = 3.55m, SoTinChiDat = 3, SoTinChiTichLuy = 13, SoTinChiNo = 0, XepLoaiHocKy = "Giỏi", XepLoaiRenLuyen = "Tốt", DiemRenLuyen = 88 },

                // SV101 - Trần Thị Bích
                new SvKetQuaHocKy { Id = 102, SinhVienId = 101, HocKy = 1, NamHoc = "2023-2024", DiemTrungBinhHocKy = 8.93m, DiemTrungBinhTinhLuy = 8.93m, GpaTichLy = 3.83m, SoTinChiDat = 10, SoTinChiTichLuy = 10, SoTinChiNo = 0, XepLoaiHocKy = "Giỏi", XepLoaiRenLuyen = "Xuất sắc", DiemRenLuyen = 92 },
                new SvKetQuaHocKy { Id = 103, SinhVienId = 101, HocKy = 2, NamHoc = "2023-2024", DiemTrungBinhHocKy = 9.5m, DiemTrungBinhTinhLuy = 9.07m, GpaTichLy = 3.87m, SoTinChiDat = 3, SoTinChiTichLuy = 13, SoTinChiNo = 0, XepLoaiHocKy = "Xuất sắc", XepLoaiRenLuyen = "Xuất sắc", DiemRenLuyen = 95 },

                // SV102 - Lê Minh Cường
                new SvKetQuaHocKy { Id = 104, SinhVienId = 102, HocKy = 1, NamHoc = "2023-2024", DiemTrungBinhHocKy = 6.77m, DiemTrungBinhTinhLuy = 6.77m, GpaTichLy = 2.5m, SoTinChiDat = 10, SoTinChiTichLuy = 10, SoTinChiNo = 0, XepLoaiHocKy = "Trung bình khá", XepLoaiRenLuyen = "Khá", DiemRenLuyen = 72 },
                new SvKetQuaHocKy { Id = 105, SinhVienId = 102, HocKy = 2, NamHoc = "2023-2024", DiemTrungBinhHocKy = 7.3m, DiemTrungBinhTinhLuy = 6.89m, GpaTichLy = 2.57m, SoTinChiDat = 3, SoTinChiTichLuy = 13, SoTinChiNo = 0, XepLoaiHocKy = "Khá", XepLoaiRenLuyen = "Khá", DiemRenLuyen = 75 },

                // SV103 - Phạm Thu Dung
                new SvKetQuaHocKy { Id = 106, SinhVienId = 103, HocKy = 1, NamHoc = "2023-2024", DiemTrungBinhHocKy = 8.57m, DiemTrungBinhTinhLuy = 8.57m, GpaTichLy = 3.67m, SoTinChiDat = 10, SoTinChiTichLuy = 10, SoTinChiNo = 0, XepLoaiHocKy = "Giỏi", XepLoaiRenLuyen = "Tốt", DiemRenLuyen = 84 },
                new SvKetQuaHocKy { Id = 107, SinhVienId = 103, HocKy = 2, NamHoc = "2023-2024", DiemTrungBinhHocKy = 9.3m, DiemTrungBinhTinhLuy = 8.74m, GpaTichLy = 3.75m, SoTinChiDat = 3, SoTinChiTichLuy = 13, SoTinChiNo = 0, XepLoaiHocKy = "Xuất sắc", XepLoaiRenLuyen = "Tốt", DiemRenLuyen = 86 },

                // SV104 - Hoàng Văn Em
                new SvKetQuaHocKy { Id = 108, SinhVienId = 104, HocKy = 1, NamHoc = "2023-2024", DiemTrungBinhHocKy = 7.6m, DiemTrungBinhTinhLuy = 7.6m, GpaTichLy = 3.03m, SoTinChiDat = 10, SoTinChiTichLuy = 10, SoTinChiNo = 0, XepLoaiHocKy = "Khá", XepLoaiRenLuyen = "Khá", DiemRenLuyen = 76 },
                new SvKetQuaHocKy { Id = 109, SinhVienId = 104, HocKy = 2, NamHoc = "2023-2024", DiemTrungBinhHocKy = 7.8m, DiemTrungBinhTinhLuy = 7.65m, GpaTichLy = 3.05m, SoTinChiDat = 3, SoTinChiTichLuy = 13, SoTinChiNo = 0, XepLoaiHocKy = "Khá", XepLoaiRenLuyen = "Khá", DiemRenLuyen = 78 }
            );
        }

        public static void SeedDataSvThongTinHocVu(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SvThongTinHocVu>().HasData(
                new SvThongTinHocVu { Id = 100, SinhVienId = 100, GpaHienTai = 3.55m, GpaTBTichLuy = 3.55m, XepLoaiHocLuc = "Giỏi", SoTinChiTichLuy = 13, SoTinChiNo = 0, CanhBaoHocVu = false, MucCanhBao = 0, LyDoCanhBao = "", HocKyHienTai = 2, NamHocHienTai = "2023-2024" },
                new SvThongTinHocVu { Id = 101, SinhVienId = 101, GpaHienTai = 3.87m, GpaTBTichLuy = 3.87m, XepLoaiHocLuc = "Giỏi", SoTinChiTichLuy = 13, SoTinChiNo = 0, CanhBaoHocVu = false, MucCanhBao = 0, LyDoCanhBao = "", HocKyHienTai = 2, NamHocHienTai = "2023-2024" },
                new SvThongTinHocVu { Id = 102, SinhVienId = 102, GpaHienTai = 2.57m, GpaTBTichLuy = 2.57m, XepLoaiHocLuc = "Trung bình khá", SoTinChiTichLuy = 13, SoTinChiNo = 0, CanhBaoHocVu = false, MucCanhBao = 0, LyDoCanhBao = "", HocKyHienTai = 2, NamHocHienTai = "2023-2024" },
                new SvThongTinHocVu { Id = 103, SinhVienId = 103, GpaHienTai = 3.75m, GpaTBTichLuy = 3.75m, XepLoaiHocLuc = "Giỏi", SoTinChiTichLuy = 13, SoTinChiNo = 0, CanhBaoHocVu = false, MucCanhBao = 0, LyDoCanhBao = "", HocKyHienTai = 2, NamHocHienTai = "2023-2024" },
                new SvThongTinHocVu { Id = 104, SinhVienId = 104, GpaHienTai = 3.05m, GpaTBTichLuy = 3.05m, XepLoaiHocLuc = "Khá", SoTinChiTichLuy = 13, SoTinChiNo = 0, CanhBaoHocVu = false, MucCanhBao = 0, LyDoCanhBao = "", HocKyHienTai = 2, NamHocHienTai = "2023-2024" }
            );
        }
    }
}
