using D.Core.Domain.Entities.Hrm.DanhMuc;
using Microsoft.EntityFrameworkCore;

namespace D.Core.Domain.Shared.SeedData
{
    public static class HrmDanhMucSeedData
    {
        public static void SeedDataHrm(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedDataDmGioiTinh();
            modelBuilder.SeedDataDmDanToc();
            modelBuilder.SeedDataDmLoaiHopDong();
            modelBuilder.SeedDataDmQuocTich();
            modelBuilder.SeedDataDmTonGiao();
            modelBuilder.SeedDataDmLoaiPhongBan();
            modelBuilder.SeedDataDmQuanHeGiaDinh();
        }

        public static void SeedDataDmGioiTinh(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DmGioiTinh>()
                .HasData(
                    new DmGioiTinh
                    {
                        Id = 1,
                        MaGioiTinh = "Male",
                        TenGioiTinh = "Nam",
                    },
                    new DmGioiTinh
                    {
                        Id = 2,
                        MaGioiTinh = "Female",
                        TenGioiTinh = "Nữ",
                    }
                );
        }

        public static void SeedDataDmDanToc(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DmDanToc>()
                .HasData(
                    new DmDanToc
                    {
                        Id = 1,
                        MaDanToc = "Kinh",
                        TenDanToc = "Kinh",
                    },
                    new DmDanToc
                    {
                        Id = 2,
                        MaDanToc = "Tay",
                        TenDanToc = "Tày",
                    },
                    new DmDanToc
                    {
                        Id = 3,
                        MaDanToc = "Thai",
                        TenDanToc = "Thái",
                    },
                    new DmDanToc
                    {
                        Id = 4,
                        MaDanToc = "Muong",
                        TenDanToc = "Mường",
                    },
                    new DmDanToc
                    {
                        Id = 5,
                        MaDanToc = "Hoa",
                        TenDanToc = "Hoa",
                    },
                    new DmDanToc
                    {
                        Id = 6,
                        MaDanToc = "Khmer",
                        TenDanToc = "Khơ Me",
                    },
                    new DmDanToc
                    {
                        Id = 7,
                        MaDanToc = "Nung",
                        TenDanToc = "Nùng",
                    },
                    new DmDanToc
                    {
                        Id = 8,
                        MaDanToc = "Hmong",
                        TenDanToc = "H'Mông",
                    },
                    new DmDanToc
                    {
                        Id = 9,
                        MaDanToc = "Dao",
                        TenDanToc = "Dao",
                    },
                    new DmDanToc
                    {
                        Id = 10,
                        MaDanToc = "GiaRai",
                        TenDanToc = "Gia Rai",
                    },
                    new DmDanToc
                    {
                        Id = 11,
                        MaDanToc = "Ede",
                        TenDanToc = "Ê Đê",
                    },
                    new DmDanToc
                    {
                        Id = 12,
                        MaDanToc = "BaNa",
                        TenDanToc = "Ba Na",
                    },
                    new DmDanToc
                    {
                        Id = 13,
                        MaDanToc = "SanChay",
                        TenDanToc = "Sán Chay",
                    },
                    new DmDanToc
                    {
                        Id = 14,
                        MaDanToc = "Cham",
                        TenDanToc = "Chăm",
                    },
                    new DmDanToc
                    {
                        Id = 15,
                        MaDanToc = "SanDiu",
                        TenDanToc = "Sán Dìu",
                    },
                    new DmDanToc
                    {
                        Id = 16,
                        MaDanToc = "CoHo",
                        TenDanToc = "Cơ Ho",
                    },
                    new DmDanToc
                    {
                        Id = 17,
                        MaDanToc = "XoDang",
                        TenDanToc = "Xơ Đăng",
                    },
                    new DmDanToc
                    {
                        Id = 18,
                        MaDanToc = "RaGlai",
                        TenDanToc = "Ra Glai",
                    },
                    new DmDanToc
                    {
                        Id = 19,
                        MaDanToc = "Mnong",
                        TenDanToc = "M'Nông",
                    },
                    new DmDanToc
                    {
                        Id = 20,
                        MaDanToc = "CoTu",
                        TenDanToc = "Cơ Tu",
                    },
                    new DmDanToc
                    {
                        Id = 21,
                        MaDanToc = "Xtieng",
                        TenDanToc = "X'tiêng",
                    },
                    new DmDanToc
                    {
                        Id = 22,
                        MaDanToc = "GieTrieng",
                        TenDanToc = "Gié Triêng",
                    },
                    new DmDanToc
                    {
                        Id = 23,
                        MaDanToc = "Co",
                        TenDanToc = "Cơ",
                    },
                    new DmDanToc
                    {
                        Id = 24,
                        MaDanToc = "TaOi",
                        TenDanToc = "Tà Ôi",
                    },
                    new DmDanToc
                    {
                        Id = 25,
                        MaDanToc = "Ma",
                        TenDanToc = "Mạ",
                    },
                    new DmDanToc
                    {
                        Id = 26,
                        MaDanToc = "BruVanKieu",
                        TenDanToc = "Bru - Vân Kiều",
                    },
                    new DmDanToc
                    {
                        Id = 27,
                        MaDanToc = "Khmu",
                        TenDanToc = "Khơ Mú",
                    },
                    new DmDanToc
                    {
                        Id = 28,
                        MaDanToc = "Mang",
                        TenDanToc = "Mảng",
                    },
                    new DmDanToc
                    {
                        Id = 29,
                        MaDanToc = "Khang",
                        TenDanToc = "Kháng",
                    },
                    new DmDanToc
                    {
                        Id = 30,
                        MaDanToc = "LaHa",
                        TenDanToc = "La Ha",
                    },
                    new DmDanToc
                    {
                        Id = 31,
                        MaDanToc = "Cong",
                        TenDanToc = "Cống",
                    },
                    new DmDanToc
                    {
                        Id = 32,
                        MaDanToc = "SiLa",
                        TenDanToc = "Si La",
                    },
                    new DmDanToc
                    {
                        Id = 33,
                        MaDanToc = "HaNhi",
                        TenDanToc = "Hà Nhì",
                    },
                    new DmDanToc
                    {
                        Id = 34,
                        MaDanToc = "LaChi",
                        TenDanToc = "La Chí",
                    },
                    new DmDanToc
                    {
                        Id = 35,
                        MaDanToc = "PhuLa",
                        TenDanToc = "Phù Lá",
                    },
                    new DmDanToc
                    {
                        Id = 36,
                        MaDanToc = "LaHu",
                        TenDanToc = "La Hủ",
                    },
                    new DmDanToc
                    {
                        Id = 37,
                        MaDanToc = "PaThen",
                        TenDanToc = "Pà Thẻn",
                    },
                    new DmDanToc
                    {
                        Id = 38,
                        MaDanToc = "LoLo",
                        TenDanToc = "Lô Lô",
                    },
                    new DmDanToc
                    {
                        Id = 39,
                        MaDanToc = "Churu",
                        TenDanToc = "Chu Ru",
                    },
                    new DmDanToc
                    {
                        Id = 40,
                        MaDanToc = "Lao",
                        TenDanToc = "Lào",
                    },
                    new DmDanToc
                    {
                        Id = 41,
                        MaDanToc = "Lu",
                        TenDanToc = "Lự",
                    },
                    new DmDanToc
                    {
                        Id = 42,
                        MaDanToc = "Ngai",
                        TenDanToc = "Ngái",
                    },
                    new DmDanToc
                    {
                        Id = 43,
                        MaDanToc = "Giay",
                        TenDanToc = "Giáy",
                    },
                    new DmDanToc
                    {
                        Id = 44,
                        MaDanToc = "Chut",
                        TenDanToc = "Chứt",
                    },
                    new DmDanToc
                    {
                        Id = 45,
                        MaDanToc = "Odu",
                        TenDanToc = "Ơ Đu",
                    },
                    new DmDanToc
                    {
                        Id = 46,
                        MaDanToc = "Brau",
                        TenDanToc = "Brâu",
                    },
                    new DmDanToc
                    {
                        Id = 47,
                        MaDanToc = "Romam",
                        TenDanToc = "Rơ Măm",
                    },
                    new DmDanToc
                    {
                        Id = 48,
                        MaDanToc = "BoY",
                        TenDanToc = "Bố Y",
                    },
                    new DmDanToc
                    {
                        Id = 49,
                        MaDanToc = "CoLao",
                        TenDanToc = "Cờ Lao",
                    },
                    new DmDanToc
                    {
                        Id = 50,
                        MaDanToc = "PuPeo",
                        TenDanToc = "Pu Péo",
                    }
                );
        }

        public static void SeedDataDmLoaiHopDong(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DmLoaiHopDong>()
                .HasData(
                    new DmLoaiHopDong
                    {
                        Id = 1,
                        MaLoaiHopDong = "HD00",
                        TenLoaiHopDong = "Hợp đồng thử việc",
                    },
                    new DmLoaiHopDong
                    {
                        Id = 2,
                        MaLoaiHopDong = "HD01",
                        TenLoaiHopDong = "Hợp đồng làm việc có thời hạn",
                    },
                    new DmLoaiHopDong
                    {
                        Id = 3,
                        MaLoaiHopDong = "HD02",
                        TenLoaiHopDong = "Hợp đồng làm việc không thời hạn",
                    },
                    new DmLoaiHopDong
                    {
                        Id = 4,
                        MaLoaiHopDong = "HD03",
                        TenLoaiHopDong = "Hợp đồng lao động có thời hạn",
                    },
                    new DmLoaiHopDong
                    {
                        Id = 5,
                        MaLoaiHopDong = "HD04",
                        TenLoaiHopDong = "Hợp đồng lao động không thời hạn",
                    }
                );
        }

        public static void SeedDataDmLoaiPhongBan(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DmLoaiPhongBan>()
                .HasData(
                    new DmLoaiPhongBan
                    {
                        Id = 1,
                        MaLoaiPhongBan = "KHAC",
                        TenLoaiPhongBan = "Khác",
                    },
                    new DmLoaiPhongBan
                    {
                        Id = 2,
                        MaLoaiPhongBan = "KHOA",
                        TenLoaiPhongBan = "Khoa",
                    },
                    new DmLoaiPhongBan
                    {
                        Id = 3,
                        MaLoaiPhongBan = "BAN",
                        TenLoaiPhongBan = "Phòng - Ban",
                    },
                    new DmLoaiPhongBan
                    {
                        Id = 4,
                        MaLoaiPhongBan = "VIEN",
                        TenLoaiPhongBan = "Viện",
                    },
                    new DmLoaiPhongBan
                    {
                        Id = 5,
                        MaLoaiPhongBan = "TRUNGTAM",
                        TenLoaiPhongBan = "Trung tâm",
                    }
                );
        }

        public static void SeedDataDmQuocTich(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DmQuocTich>()
                .HasData(
                    new DmQuocTich
                    {
                        Id = 1,
                        MaQuocGia = "VN",
                        TenQuocGia = "Việt Nam",
                        TenQuocGia_EN = "Vietnam",
                    },
                    new DmQuocTich
                    {
                        Id = 2,
                        MaQuocGia = "US",
                        TenQuocGia = "Hoa Kỳ",
                        TenQuocGia_EN = "United States",
                    },
                    new DmQuocTich
                    {
                        Id = 3,
                        MaQuocGia = "CN",
                        TenQuocGia = "Trung Quốc",
                        TenQuocGia_EN = "China",
                    },
                    new DmQuocTich
                    {
                        Id = 4,
                        MaQuocGia = "JP",
                        TenQuocGia = "Nhật Bản",
                        TenQuocGia_EN = "Japan",
                    },
                    new DmQuocTich
                    {
                        Id = 5,
                        MaQuocGia = "KR",
                        TenQuocGia = "Hàn Quốc",
                        TenQuocGia_EN = "South Korea",
                    },
                    new DmQuocTich
                    {
                        Id = 6,
                        MaQuocGia = "RU",
                        TenQuocGia = "Nga",
                        TenQuocGia_EN = "Russia",
                    },
                    new DmQuocTich
                    {
                        Id = 7,
                        MaQuocGia = "FR",
                        TenQuocGia = "Pháp",
                        TenQuocGia_EN = "France",
                    },
                    new DmQuocTich
                    {
                        Id = 8,
                        MaQuocGia = "DE",
                        TenQuocGia = "Đức",
                        TenQuocGia_EN = "Germany",
                    },
                    new DmQuocTich
                    {
                        Id = 9,
                        MaQuocGia = "GB",
                        TenQuocGia = "Anh",
                        TenQuocGia_EN = "United Kingdom",
                    },
                    new DmQuocTich
                    {
                        Id = 10,
                        MaQuocGia = "AU",
                        TenQuocGia = "Úc",
                        TenQuocGia_EN = "Australia",
                    },
                    new DmQuocTich
                    {
                        Id = 11,
                        MaQuocGia = "CA",
                        TenQuocGia = "Canada",
                        TenQuocGia_EN = "Canada",
                    },
                    new DmQuocTich
                    {
                        Id = 12,
                        MaQuocGia = "LA",
                        TenQuocGia = "Lào",
                        TenQuocGia_EN = "Laos",
                    },
                    new DmQuocTich
                    {
                        Id = 13,
                        MaQuocGia = "KH",
                        TenQuocGia = "Campuchia",
                        TenQuocGia_EN = "Cambodia",
                    },
                    new DmQuocTich
                    {
                        Id = 14,
                        MaQuocGia = "TH",
                        TenQuocGia = "Thái Lan",
                        TenQuocGia_EN = "Thailand",
                    },
                    new DmQuocTich
                    {
                        Id = 15,
                        MaQuocGia = "MY",
                        TenQuocGia = "Malaysia",
                        TenQuocGia_EN = "Malaysia",
                    },
                    new DmQuocTich
                    {
                        Id = 16,
                        MaQuocGia = "SG",
                        TenQuocGia = "Singapore",
                        TenQuocGia_EN = "Singapore",
                    },
                    new DmQuocTich
                    {
                        Id = 17,
                        MaQuocGia = "PH",
                        TenQuocGia = "Philippines",
                        TenQuocGia_EN = "Philippines",
                    },
                    new DmQuocTich
                    {
                        Id = 18,
                        MaQuocGia = "ID",
                        TenQuocGia = "Indonesia",
                        TenQuocGia_EN = "Indonesia",
                    },
                    new DmQuocTich
                    {
                        Id = 19,
                        MaQuocGia = "IN",
                        TenQuocGia = "Ấn Độ",
                        TenQuocGia_EN = "India",
                    },
                    new DmQuocTich
                    {
                        Id = 20,
                        MaQuocGia = "IT",
                        TenQuocGia = "Ý",
                        TenQuocGia_EN = "Italy",
                    },
                    new DmQuocTich
                    {
                        Id = 21,
                        MaQuocGia = "ES",
                        TenQuocGia = "Tây Ban Nha",
                        TenQuocGia_EN = "Spain",
                    },
                    new DmQuocTich
                    {
                        Id = 22,
                        MaQuocGia = "NL",
                        TenQuocGia = "Hà Lan",
                        TenQuocGia_EN = "Netherlands",
                    },
                    new DmQuocTich
                    {
                        Id = 23,
                        MaQuocGia = "CH",
                        TenQuocGia = "Thụy Sĩ",
                        TenQuocGia_EN = "Switzerland",
                    },
                    new DmQuocTich
                    {
                        Id = 24,
                        MaQuocGia = "SE",
                        TenQuocGia = "Thụy Điển",
                        TenQuocGia_EN = "Sweden",
                    },
                    new DmQuocTich
                    {
                        Id = 25,
                        MaQuocGia = "NO",
                        TenQuocGia = "Na Uy",
                        TenQuocGia_EN = "Norway",
                    },
                    new DmQuocTich
                    {
                        Id = 26,
                        MaQuocGia = "DK",
                        TenQuocGia = "Đan Mạch",
                        TenQuocGia_EN = "Denmark",
                    },
                    new DmQuocTich
                    {
                        Id = 27,
                        MaQuocGia = "FI",
                        TenQuocGia = "Phần Lan",
                        TenQuocGia_EN = "Finland",
                    },
                    new DmQuocTich
                    {
                        Id = 28,
                        MaQuocGia = "BE",
                        TenQuocGia = "Bỉ",
                        TenQuocGia_EN = "Belgium",
                    },
                    new DmQuocTich
                    {
                        Id = 29,
                        MaQuocGia = "AT",
                        TenQuocGia = "Áo",
                        TenQuocGia_EN = "Austria",
                    },
                    new DmQuocTich
                    {
                        Id = 30,
                        MaQuocGia = "NZ",
                        TenQuocGia = "New Zealand",
                        TenQuocGia_EN = "New Zealand",
                    },
                    new DmQuocTich
                    {
                        Id = 31,
                        MaQuocGia = "BR",
                        TenQuocGia = "Brazil",
                        TenQuocGia_EN = "Brazil",
                    },
                    new DmQuocTich
                    {
                        Id = 32,
                        MaQuocGia = "AR",
                        TenQuocGia = "Argentina",
                        TenQuocGia_EN = "Argentina",
                    },
                    new DmQuocTich
                    {
                        Id = 33,
                        MaQuocGia = "MX",
                        TenQuocGia = "Mexico",
                        TenQuocGia_EN = "Mexico",
                    },
                    new DmQuocTich
                    {
                        Id = 34,
                        MaQuocGia = "CL",
                        TenQuocGia = "Chile",
                        TenQuocGia_EN = "Chile",
                    },
                    new DmQuocTich
                    {
                        Id = 35,
                        MaQuocGia = "ZA",
                        TenQuocGia = "Nam Phi",
                        TenQuocGia_EN = "South Africa",
                    },
                    new DmQuocTich
                    {
                        Id = 36,
                        MaQuocGia = "AE",
                        TenQuocGia = "Các Tiểu vương quốc Ả Rập Thống nhất",
                        TenQuocGia_EN = "United Arab Emirates",
                    },
                    new DmQuocTich
                    {
                        Id = 37,
                        MaQuocGia = "SA",
                        TenQuocGia = "Ả Rập Xê Út",
                        TenQuocGia_EN = "Saudi Arabia",
                    },
                    new DmQuocTich
                    {
                        Id = 38,
                        MaQuocGia = "TR",
                        TenQuocGia = "Thổ Nhĩ Kỳ",
                        TenQuocGia_EN = "Turkey",
                    },
                    new DmQuocTich
                    {
                        Id = 39,
                        MaQuocGia = "PK",
                        TenQuocGia = "Pakistan",
                        TenQuocGia_EN = "Pakistan",
                    },
                    new DmQuocTich
                    {
                        Id = 40,
                        MaQuocGia = "BD",
                        TenQuocGia = "Bangladesh",
                        TenQuocGia_EN = "Bangladesh",
                    },
                    new DmQuocTich
                    {
                        Id = 41,
                        MaQuocGia = "LK",
                        TenQuocGia = "Sri Lanka",
                        TenQuocGia_EN = "Sri Lanka",
                    },
                    new DmQuocTich
                    {
                        Id = 42,
                        MaQuocGia = "MM",
                        TenQuocGia = "Myanmar",
                        TenQuocGia_EN = "Myanmar",
                    },
                    new DmQuocTich
                    {
                        Id = 43,
                        MaQuocGia = "NP",
                        TenQuocGia = "Nepal",
                        TenQuocGia_EN = "Nepal",
                    },
                    new DmQuocTich
                    {
                        Id = 44,
                        MaQuocGia = "IR",
                        TenQuocGia = "Iran",
                        TenQuocGia_EN = "Iran",
                    },
                    new DmQuocTich
                    {
                        Id = 45,
                        MaQuocGia = "IQ",
                        TenQuocGia = "Iraq",
                        TenQuocGia_EN = "Iraq",
                    },
                    new DmQuocTich
                    {
                        Id = 46,
                        MaQuocGia = "EG",
                        TenQuocGia = "Ai Cập",
                        TenQuocGia_EN = "Egypt",
                    },
                    new DmQuocTich
                    {
                        Id = 47,
                        MaQuocGia = "IL",
                        TenQuocGia = "Israel",
                        TenQuocGia_EN = "Israel",
                    },
                    new DmQuocTich
                    {
                        Id = 48,
                        MaQuocGia = "JO",
                        TenQuocGia = "Jordan",
                        TenQuocGia_EN = "Jordan",
                    },
                    new DmQuocTich
                    {
                        Id = 49,
                        MaQuocGia = "QA",
                        TenQuocGia = "Qatar",
                        TenQuocGia_EN = "Qatar",
                    },
                    new DmQuocTich
                    {
                        Id = 50,
                        MaQuocGia = "KW",
                        TenQuocGia = "Kuwait",
                        TenQuocGia_EN = "Kuwait",
                    },
                    new DmQuocTich
                    {
                        Id = 51,
                        MaQuocGia = "NG",
                        TenQuocGia = "Nigeria",
                        TenQuocGia_EN = "Nigeria",
                    },
                    new DmQuocTich
                    {
                        Id = 52,
                        MaQuocGia = "KE",
                        TenQuocGia = "Kenya",
                        TenQuocGia_EN = "Kenya",
                    }
                );
        }

        public static void SeedDataDmTonGiao(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DmTonGiao>()
                .HasData(
                    new DmTonGiao
                    {
                        Id = 1,
                        MaTonGiao = "K",
                        TenTonGiao = "Không",
                    },
                    new DmTonGiao
                    {
                        Id = 2,
                        MaTonGiao = "PG",
                        TenTonGiao = "Phật giáo",
                    },
                    new DmTonGiao
                    {
                        Id = 3,
                        MaTonGiao = "CG",
                        TenTonGiao = "Công giáo",
                    },
                    new DmTonGiao
                    {
                        Id = 4,
                        MaTonGiao = "TL",
                        TenTonGiao = "Tin lành",
                    },
                    new DmTonGiao
                    {
                        Id = 5,
                        MaTonGiao = "CD",
                        TenTonGiao = "Cao Đài",
                    },
                    new DmTonGiao
                    {
                        Id = 6,
                        MaTonGiao = "HDM",
                        TenTonGiao = "Hồi giáo",
                    },
                    new DmTonGiao
                    {
                        Id = 7,
                        MaTonGiao = "PGHH",
                        TenTonGiao = "Phật giáo Hòa Hảo",
                    },
                    new DmTonGiao
                    {
                        Id = 8,
                        MaTonGiao = "BAH",
                        TenTonGiao = "Baha'i",
                    },
                    new DmTonGiao
                    {
                        Id = 9,
                        MaTonGiao = "TDCSPH",
                        TenTonGiao = "Tịnh độ Cư sĩ Phật hội",
                    },
                    new DmTonGiao
                    {
                        Id = 10,
                        MaTonGiao = "CDPL",
                        TenTonGiao = "Cơ đốc Phục Lâm",
                    },
                    new DmTonGiao
                    {
                        Id = 11,
                        MaTonGiao = "PGTÂ",
                        TenTonGiao = "Phật giáo Tứ Ân Hiếu nghĩa",
                    },
                    new DmTonGiao
                    {
                        Id = 12,
                        MaTonGiao = "MSĐ",
                        TenTonGiao = "Minh Sư đạo",
                    },
                    new DmTonGiao
                    {
                        Id = 13,
                        MaTonGiao = "MLĐ",
                        TenTonGiao = "Minh Lý đạo – Tam Tông Miếu",
                    },
                    new DmTonGiao
                    {
                        Id = 14,
                        MaTonGiao = "BLM",
                        TenTonGiao = "Bà-la-môn giáo",
                    },
                    new DmTonGiao
                    {
                        Id = 15,
                        MaTonGiao = "MM",
                        TenTonGiao = "Mặc Môn",
                    },
                    new DmTonGiao
                    {
                        Id = 16,
                        MaTonGiao = "PGHTL",
                        TenTonGiao = "Phật giáo Hiếu nghĩa Tà Lơn",
                    },
                    new DmTonGiao
                    {
                        Id = 17,
                        MaTonGiao = "BSKH",
                        TenTonGiao = "Bửu Sơn Kỳ Hương",
                    }
                );
        }

        public static void SeedDataDmQuanHeGiaDinh(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DmQuanHeGiaDinh>()
                .HasData(
                    new DmQuanHeGiaDinh
                    {
                        Id = 1,
                        MaQuanHe = "VoChong",
                        TenQuanHe = "Vợ/Chồng",
                    },
                    new DmQuanHeGiaDinh
                    {
                        Id = 2,
                        MaQuanHe = "Con",
                        TenQuanHe = "Con",
                    },
                    new DmQuanHeGiaDinh
                    {
                        Id = 3,
                        MaQuanHe = "BoDe",
                        TenQuanHe = "Bố",
                    },
                    new DmQuanHeGiaDinh
                    {
                        Id = 4,
                        MaQuanHe = "MeDe",
                        TenQuanHe = "Mẹ",
                    },
                    new DmQuanHeGiaDinh
                    {
                        Id = 5,
                        MaQuanHe = "Ace",
                        TenQuanHe = "Anh/Chị/Em",
                    },
                    new DmQuanHeGiaDinh
                    {
                        Id = 6,
                        MaQuanHe = "BoVC",
                        TenQuanHe = "Bố (chồng, vợ)",
                    },
                    new DmQuanHeGiaDinh
                    {
                        Id = 7,
                        MaQuanHe = "MeVC",
                        TenQuanHe = "Mẹ (chồng, vợ)",
                    },
                    new DmQuanHeGiaDinh
                    {
                        Id = 8,
                        MaQuanHe = "AceVC",
                        TenQuanHe = "Anh/Chị/Em (chồng, vợ)",
                    }
                );
        }
    }
}
