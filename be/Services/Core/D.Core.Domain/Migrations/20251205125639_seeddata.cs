using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert seed data for DtKhoa
            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtKhoa",
                columns: new[] { "Id", "MaKhoa", "TenKhoa", "TenTiengAnh", "VietTat", "Email", "Sdt", "DiaChi", "TrangThai", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "CNTT", "Công nghệ Thông tin", "Information Technology", "FIT", "fit@huce.edu.vn", null, null, true, "root", false, null, null, null, null },
                    { 2, "KT", "Kinh tế & Quản lý", "Economics and Management", "FEM", "ktql@huce.edu.vn", null, null, true, "root", false, null, null, null, null }
                });

            // Insert seed data for DtNganh
            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtNganh",
                columns: new[] { "Id", "MaNganh", "TenNganh", "TenTiengAnh", "MoTa", "TrangThai", "KhoaId", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "7480201", "Công nghệ thông tin", "Information Technology", null, true, 1, "root", false, null, null, null, null },
                    { 2, "7480101", "Khoa học máy tính", "Computer Science", null, true, 1, "root", false, null, null, null, null },
                    { 3, "7340201", "Tài chính - Ngân hàng", null, null, true, 2, "root", false, null, null, null, null }
                });

            // Insert seed data for DtChuyenNganh
            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtChuyenNganh",
                columns: new[] { "Id", "MaChuyenNganh", "TenChuyenNganh", "TenTiengAnh", "MoTa", "TrangThai", "NganhId", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "KTPM", "Kỹ thuật phần mềm", "Software Engineering", null, true, 1, "root", false, null, null, null, null },
                    { 2, "HTTT", "Hệ thống thông tin", "Information Systems", null, true, 1, "root", false, null, null, null, null },
                    { 3, "AI", "Trí tuệ nhân tạo", null, null, true, 2, "root", false, null, null, null, null }
                });

            // Insert seed data for DtMonHoc
            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtMonHoc",
                columns: new[] { "Id", "MaMonHoc", "TenMonHoc", "SoTinChi", "SoTietLyThuyet", "SoTietThucHanh", "MoTa", "TrangThai", "ToBoMonId", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "IT1110", "Tin học đại cương", 3, 30, 30, null, true, null, "root", false, null, null, null, null },
                    { 2, "IT3040", "Kỹ thuật lập trình", 3, 45, 0, null, true, null, "root", false, null, null, null, null },
                    { 3, "IT3090", "Cơ sở dữ liệu", 3, 45, 15, null, true, null, "root", false, null, null, null, null },
                    { 4, "MI1111", "Giải tích 1", 4, 60, null, null, true, null, "root", false, null, null, null, null }
                });

            // Insert seed data for DtChuongTrinhKhung
            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtChuongTrinhKhung",
                columns: new[] { "Id", "MaChuongTrinhKhung", "TenChuongTrinhKhung", "TongSoTinChi", "KhoaHocId", "NganhId", "ChuyenNganhId", "MoTa", "TrangThai", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "CNTT-K66-KS", "Chương trình chuẩn Kỹ sư CNTT K66", 150, 1, 1, 2, null, true, "root", false, null, null, null, null },
                    { 2, "KTPM-K67-CLC", "Kỹ sư KTPM Chất lượng cao K67", 160, 2, 1, 1, null, true, "root", false, null, null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                columns: new[] { "ChuongTrinhKhungId", "MonHocId", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "HocKy", "Id", "ModifiedBy", "ModifiedDate", "NamHoc", "TrangThai" },
                values: new object[,]
                {
                    { 1, 1, "root", false, null, null, 1, 1, null, null, "2021-2022", true },
                    { 1, 2, "root", false, null, null, 1, 3, null, null, "2021-2022", true },
                    { 1, 3, "root", false, null, null, 1, 4, null, null, "2021-2022", true },
                    { 1, 4, "root", false, null, null, 1, 2, null, null, "2021-2022", true },
                    { 2, 2, "root", false, null, null, 2, 5, null, null, "2021-2022", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete in reverse order of insertion
            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhung",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhung",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtMonHoc",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuyenNganh",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtNganh",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtKhoa",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 });
        }
    }
}
