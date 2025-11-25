using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class seeddatadt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtKhoa",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "DiaChi", "Email", "MaKhoa", "ModifiedBy", "ModifiedDate", "Sdt", "TenKhoa", "TenTiengAnh", "TrangThai", "VietTat" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, null, "fit@huce.edu.vn", "CNTT", null, null, null, "Công nghệ Thông tin", "Information Technology", true, "FIT" },
                    { 2, "root", false, null, null, null, "ktql@huce.edu.vn", "KT", null, null, null, "Kinh tế & Quản lý", "Economics and Management", true, "FEM" }
                });

            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtMonHoc",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "MaMonHoc", "MoTa", "ModifiedBy", "ModifiedDate", "SoTietLyThuyet", "SoTietThucHanh", "SoTinChi", "TenMonHoc", "ToBoMonId", "TrangThai" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, "IT1110", null, null, null, 30, 30, 3, "Tin học đại cương", null, true },
                    { 2, "root", false, null, null, "IT3040", null, null, null, 45, 0, 3, "Kỹ thuật lập trình", null, true },
                    { 3, "root", false, null, null, "IT3090", null, null, null, 45, 15, 3, "Cơ sở dữ liệu", null, true },
                    { 4, "root", false, null, null, "MI1111", null, null, null, 60, null, 4, "Giải tích 1", null, true }
                });

            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtNganh",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "KhoaId", "MaNganh", "MoTa", "ModifiedBy", "ModifiedDate", "TenNganh", "TenTiengAnh", "TrangThai" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, 1, "7480201", null, null, null, "Công nghệ thông tin", "Information Technology", true },
                    { 2, "root", false, null, null, 1, "7480101", null, null, null, "Khoa học máy tính", "Computer Science", true },
                    { 3, "root", false, null, null, 2, "7340201", null, null, null, "Tài chính - Ngân hàng", null, true }
                });

            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtChuyenNganh",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "MaChuyenNganh", "MoTa", "ModifiedBy", "ModifiedDate", "NganhId", "TenChuyenNganh", "TenTiengAnh", "TrangThai" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, "KTPM", null, null, null, 1, "Kỹ thuật phần mềm", "Software Engineering", true },
                    { 2, "root", false, null, null, "HTTT", null, null, null, 1, "Hệ thống thông tin", "Information Systems", true },
                    { 3, "root", false, null, null, "AI", null, null, null, 2, "Trí tuệ nhân tạo", null, true }
                });

            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtChuongTrinhKhung",
                columns: new[] { "Id", "ChuyenNganhId", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "KhoaHocId", "MaChuongTrinhKhung", "MoTa", "ModifiedBy", "ModifiedDate", "NganhId", "TenChuongTrinhKhung", "TongSoTinChi", "TrangThai" },
                values: new object[,]
                {
                    { 1, 2, "root", false, null, null, 1, "CNTT-K66-KS", null, null, null, 1, "Chương trình chuẩn Kỹ sư CNTT K66", 150, true },
                    { 2, 1, "root", false, null, null, 2, "KTPM-K67-CLC", null, null, null, 1, "Kỹ sư KTPM Chất lượng cao K67", 160, true }
                });

            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                columns: new[] { "ChuongTrinhKhungId", "MonHocId", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "HocKy", "Id", "ModifiedBy", "ModifiedDate", "TrangThai" },
                values: new object[,]
                {
                    { 1, 1, "root", false, null, null, "2021-2022", 1, null, null, true },
                    { 1, 2, "root", false, null, null, "2021-2022", 3, null, null, true },
                    { 1, 3, "root", false, null, null, "2021-2022", 4, null, null, true },
                    { 1, 4, "root", false, null, null, "2021-2022", 2, null, null, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhung",
                keyColumn: "Id",
                keyValue: 2);

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
                table: "DtChuyenNganh",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtNganh",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhung",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuyenNganh",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtKhoa",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtMonHoc",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtMonHoc",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtMonHoc",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtMonHoc",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtNganh",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuyenNganh",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtNganh",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtKhoa",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
