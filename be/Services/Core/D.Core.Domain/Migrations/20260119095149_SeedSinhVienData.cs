using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class SeedSinhVienData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "sv",
                table: "SvDiemMonHoc",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "DiemChu", "DiemCuoiKy", "DiemHe4", "DiemQuaTrinh", "DiemTongKet", "GhiChu", "HocKy", "KetQua", "LanHoc", "ModifiedBy", "ModifiedDate", "MonHocId", "NamHoc", "SinhVienId" },
                values: new object[,]
                {
                    { 100, "root", false, null, null, "B", 8.0m, 3.0m, 7.5m, 7.8m, "", 1, "Đạt", 1, null, null, 4, "2023-2024", 100 },
                    { 101, "root", false, null, null, "A", 8.5m, 3.7m, 9.0m, 8.7m, "", 1, "Đạt", 1, null, null, 1, "2023-2024", 100 },
                    { 102, "root", false, null, null, "B+", 8.0m, 3.5m, 8.5m, 8.2m, "", 1, "Đạt", 1, null, null, 2, "2023-2024", 100 },
                    { 103, "root", false, null, null, "A", 9.0m, 3.7m, 8.5m, 8.8m, "", 2, "Đạt", 1, null, null, 3, "2023-2024", 100 },
                    { 104, "root", false, null, null, "A+", 9.5m, 4.0m, 9.0m, 9.3m, "", 1, "Đạt", 1, null, null, 4, "2023-2024", 101 },
                    { 105, "root", false, null, null, "B+", 8.5m, 3.5m, 8.0m, 8.3m, "", 1, "Đạt", 1, null, null, 1, "2023-2024", 101 },
                    { 106, "root", false, null, null, "A+", 9.0m, 4.0m, 9.5m, 9.2m, "", 1, "Đạt", 1, null, null, 2, "2023-2024", 101 },
                    { 107, "root", false, null, null, "A+", 9.5m, 4.0m, 9.5m, 9.5m, "", 2, "Đạt", 1, null, null, 3, "2023-2024", 101 },
                    { 108, "root", false, null, null, "C", 5.5m, 2.0m, 6.0m, 5.7m, "", 1, "Đạt", 1, null, null, 4, "2023-2024", 102 },
                    { 109, "root", false, null, null, "B", 8.0m, 3.0m, 7.5m, 7.8m, "", 1, "Đạt", 1, null, null, 1, "2023-2024", 102 },
                    { 110, "root", false, null, null, "C+", 7.0m, 2.5m, 6.5m, 6.8m, "", 1, "Đạt", 1, null, null, 2, "2023-2024", 102 },
                    { 111, "root", false, null, null, "B", 7.5m, 2.8m, 7.0m, 7.3m, "", 2, "Đạt", 1, null, null, 3, "2023-2024", 102 },
                    { 112, "root", false, null, null, "B+", 8.0m, 3.5m, 8.5m, 8.2m, "", 1, "Đạt", 1, null, null, 4, "2023-2024", 103 },
                    { 113, "root", false, null, null, "A+", 9.0m, 4.0m, 9.5m, 9.2m, "", 1, "Đạt", 1, null, null, 1, "2023-2024", 103 },
                    { 114, "root", false, null, null, "B+", 8.5m, 3.5m, 8.0m, 8.3m, "", 1, "Đạt", 1, null, null, 2, "2023-2024", 103 },
                    { 115, "root", false, null, null, "A+", 9.5m, 4.0m, 9.0m, 9.3m, "", 2, "Đạt", 1, null, null, 3, "2023-2024", 103 },
                    { 116, "root", false, null, null, "B", 7.5m, 2.8m, 7.0m, 7.3m, "", 1, "Đạt", 1, null, null, 4, "2023-2024", 104 },
                    { 117, "root", false, null, null, "B+", 8.5m, 3.5m, 8.0m, 8.3m, "", 1, "Đạt", 1, null, null, 1, "2023-2024", 104 },
                    { 118, "root", false, null, null, "B", 7.0m, 2.8m, 7.5m, 7.2m, "", 1, "Đạt", 1, null, null, 2, "2023-2024", 104 },
                    { 119, "root", false, null, null, "B", 8.0m, 3.0m, 7.5m, 7.8m, "", 2, "Đạt", 1, null, null, 3, "2023-2024", 104 }
                });

            migrationBuilder.InsertData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "DiemRenLuyen", "DiemTrungBinhHocKy", "DiemTrungBinhTinhLuy", "GpaTichLy", "HocKy", "ModifiedBy", "ModifiedDate", "NamHoc", "SinhVienId", "SoTinChiDat", "SoTinChiNo", "SoTinChiTichLuy", "XepLoaiHocKy", "XepLoaiRenLuyen" },
                values: new object[,]
                {
                    { 100, "root", false, null, null, 85, 8.23m, 8.23m, 3.4m, 1, null, null, "2023-2024", 100, 10, 0, 10, "Giỏi", "Tốt" },
                    { 101, "root", false, null, null, 88, 8.8m, 8.52m, 3.55m, 2, null, null, "2023-2024", 100, 3, 0, 13, "Giỏi", "Tốt" },
                    { 102, "root", false, null, null, 92, 8.93m, 8.93m, 3.83m, 1, null, null, "2023-2024", 101, 10, 0, 10, "Giỏi", "Xuất sắc" },
                    { 103, "root", false, null, null, 95, 9.5m, 9.07m, 3.87m, 2, null, null, "2023-2024", 101, 3, 0, 13, "Xuất sắc", "Xuất sắc" },
                    { 104, "root", false, null, null, 72, 6.77m, 6.77m, 2.5m, 1, null, null, "2023-2024", 102, 10, 0, 10, "Trung bình khá", "Khá" },
                    { 105, "root", false, null, null, 75, 7.3m, 6.89m, 2.57m, 2, null, null, "2023-2024", 102, 3, 0, 13, "Khá", "Khá" },
                    { 106, "root", false, null, null, 84, 8.57m, 8.57m, 3.67m, 1, null, null, "2023-2024", 103, 10, 0, 10, "Giỏi", "Tốt" },
                    { 107, "root", false, null, null, 86, 9.3m, 8.74m, 3.75m, 2, null, null, "2023-2024", 103, 3, 0, 13, "Xuất sắc", "Tốt" },
                    { 108, "root", false, null, null, 76, 7.6m, 7.6m, 3.03m, 1, null, null, "2023-2024", 104, 10, 0, 10, "Khá", "Khá" },
                    { 109, "root", false, null, null, 78, 7.8m, 7.65m, 3.05m, 2, null, null, "2023-2024", 104, 3, 0, 13, "Khá", "Khá" }
                });

            migrationBuilder.InsertData(
                schema: "sv",
                table: "SvSinhVien",
                columns: new[] { "Id", "ChuyenNganh", "CreatedBy", "DanToc", "Deleted", "DeletedBy", "DeletedDate", "Email", "Email2", "GioiTinh", "HoDem", "Khoa", "KhoaHoc", "LopQL", "ModifiedBy", "ModifiedDate", "Mssv", "Nganh", "NgaySinh", "NguyenQuan", "NoiOHienTai", "NoiSinh", "Password", "PasswordKey", "QuocTich", "SoCccd", "SoDienThoai", "Ten", "TonGiao", "TrangThaiHoc" },
                values: new object[,]
                {
                    { 100, 1, "root", 1, false, null, null, "nguyenvanan2004@gmail.com", "an.nv@student.duniversity.edu.vn", 1, "Nguyễn Văn", 1, 1, null, null, null, "SV2023001", 1, new DateTime(2004, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quận Thanh Xuân, Hà Nội", "Số 123, Đường Nguyễn Trãi, Quận Thanh Xuân, Hà Nội", "Hà Nội", null, null, 1, "001204012345", "0901234567", "An", 1, 1 },
                    { 101, 2, "root", 1, false, null, null, "tranthibich2004@gmail.com", "bich.tt@student.duniversity.edu.vn", 2, "Trần Thị", 1, 1, null, null, null, "SV2023002", 1, new DateTime(2004, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quận Ngô Quyền, Hải Phòng", "Số 45, Đường Lê Lợi, Quận Cầu Giấy, Hà Nội", "Hải Phòng", null, null, 1, "001204023456", "0912345678", "Bích", 2, 1 },
                    { 102, 1, "root", 1, false, null, null, "leminhcuong2004@gmail.com", "cuong.lm@student.duniversity.edu.vn", 1, "Lê Minh", 1, 1, null, null, null, "SV2023003", 1, new DateTime(2004, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quận Hải Châu, Đà Nẵng", "Số 78, Đường Trần Duy Hưng, Quận Cầu Giấy, Hà Nội", "Đà Nẵng", null, null, 1, "001204034567", "0923456789", "Cường", 1, 1 },
                    { 103, 1, "root", 1, false, null, null, "phamthudung2004@gmail.com", "dung.pt@student.duniversity.edu.vn", 2, "Phạm Thu", 1, 1, null, null, null, "SV2023004", 1, new DateTime(2004, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "TP Bắc Ninh, Bắc Ninh", "Số 22, Đường Xuân Thủy, Quận Cầu Giấy, Hà Nội", "Bắc Ninh", null, null, 1, "001204045678", "0934567890", "Dung", 3, 1 },
                    { 104, 2, "root", 1, false, null, null, "hoangvanem2004@gmail.com", "em.hv@student.duniversity.edu.vn", 1, "Hoàng Văn", 1, 1, null, null, null, "SV2023005", 1, new DateTime(2004, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "TP Nam Định, Nam Định", "Số 55, Đường Phạm Văn Đồng, Quận Bắc Từ Liêm, Hà Nội", "Nam Định", null, null, 1, "001204056789", "0945678901", "Em", 1, 1 }
                });

            migrationBuilder.InsertData(
                schema: "sv",
                table: "SvThongTinHocVu",
                columns: new[] { "Id", "CanhBaoHocVu", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "GpaHienTai", "GpaTBTichLuy", "HocKyHienTai", "LyDoCanhBao", "ModifiedBy", "ModifiedDate", "MucCanhBao", "NamHocHienTai", "SinhVienId", "SoTinChiNo", "SoTinChiTichLuy", "XepLoaiHocLuc" },
                values: new object[,]
                {
                    { 100, false, "root", false, null, null, 3.55m, 3.55m, 2, "", null, null, 0, "2023-2024", 100, 0, 13, "Giỏi" },
                    { 101, false, "root", false, null, null, 3.87m, 3.87m, 2, "", null, null, 0, "2023-2024", 101, 0, 13, "Giỏi" },
                    { 102, false, "root", false, null, null, 2.57m, 2.57m, 2, "", null, null, 0, "2023-2024", 102, 0, 13, "Trung bình khá" },
                    { 103, false, "root", false, null, null, 3.75m, 3.75m, 2, "", null, null, 0, "2023-2024", 103, 0, 13, "Giỏi" },
                    { 104, false, "root", false, null, null, 3.05m, 3.05m, 2, "", null, null, 0, "2023-2024", 104, 0, 13, "Khá" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvDiemMonHoc",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvKetQuaHocKy",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvSinhVien",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvSinhVien",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvSinhVien",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvSinhVien",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvSinhVien",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvThongTinHocVu",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvThongTinHocVu",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvThongTinHocVu",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvThongTinHocVu",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "sv",
                table: "SvThongTinHocVu",
                keyColumn: "Id",
                keyValue: 104);
        }
    }
}
