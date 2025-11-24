using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updatekhoahoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DmKhoa",
                schema: "hrm");

            migrationBuilder.RenameColumn(
                name: "ChuyenNganh",
                schema: "sv",
                table: "SvSinhVien",
                newName: "TonGiao");

            migrationBuilder.AlterColumn<int>(
                name: "GioiTinh",
                schema: "sv",
                table: "SvSinhVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Nganh",
                schema: "sv",
                table: "SvSinhVien",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguyenQuan",
                schema: "sv",
                table: "SvSinhVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiOHienTai",
                schema: "sv",
                table: "SvSinhVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DmKhoaHoc",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoaHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKhoaHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CachViet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: true),
                    NguoiTao = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmKhoaHoc", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "hrm",
                table: "DmKhoaHoc",
                columns: new[] { "Id", "CachViet", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "IsVisible", "MaKhoaHoc", "ModifiedBy", "ModifiedDate", "Nam", "NgayTao", "NguoiTao", "TenKhoaHoc" },
                values: new object[,]
                {
                    { 1, "2024 - 2028", "root", false, null, null, true, "K69", null, null, "2024", null, null, "Khóa 69 (2024)" },
                    { 2, "2025 - 2029", "root", false, null, null, true, "K70", null, null, "2025", null, null, "Khóa 70 (2025)" },
                    { 3, "2026 - 2030", "root", false, null, null, true, "K71", null, null, "2026", null, null, "Khóa 71 (2026)" }
                });

            migrationBuilder.UpdateData(
                schema: "hrm",
                table: "DmLoaiPhongBan",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MaLoaiPhongBan", "TenLoaiPhongBan" },
                values: new object[] { "KhoaHoc", "KhoaHoc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DmKhoaHoc",
                schema: "hrm");

            migrationBuilder.DropColumn(
                name: "Nganh",
                schema: "sv",
                table: "SvSinhVien");

            migrationBuilder.DropColumn(
                name: "NguyenQuan",
                schema: "sv",
                table: "SvSinhVien");

            migrationBuilder.DropColumn(
                name: "NoiOHienTai",
                schema: "sv",
                table: "SvSinhVien");

            migrationBuilder.RenameColumn(
                name: "TonGiao",
                schema: "sv",
                table: "SvSinhVien",
                newName: "ChuyenNganh");

            migrationBuilder.AlterColumn<bool>(
                name: "GioiTinh",
                schema: "sv",
                table: "SvSinhVien",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DmKhoa",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CachViet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: true),
                    MaKhoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<int>(type: "int", nullable: true),
                    TenKhoa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmKhoa", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "hrm",
                table: "DmKhoa",
                columns: new[] { "Id", "CachViet", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "IsVisible", "MaKhoa", "ModifiedBy", "ModifiedDate", "Nam", "NgayTao", "NguoiTao", "TenKhoa" },
                values: new object[,]
                {
                    { 1, "2024 - 2028", "root", false, null, null, true, "K69", null, null, "2024", null, null, "Khóa 69 (2024)" },
                    { 2, "2025 - 2029", "root", false, null, null, true, "K70", null, null, "2025", null, null, "Khóa 70 (2025)" },
                    { 3, "2026 - 2030", "root", false, null, null, true, "K71", null, null, "2026", null, null, "Khóa 71 (2026)" }
                });

            migrationBuilder.UpdateData(
                schema: "hrm",
                table: "DmLoaiPhongBan",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MaLoaiPhongBan", "TenLoaiPhongBan" },
                values: new object[] { "KHOA", "Khoa" });
        }
    }
}
