using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addkhoatable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KhoaHoc",
                schema: "sv",
                table: "SvSinhVien");

            migrationBuilder.CreateTable(
                name: "DmKhoa",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKhoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DmKhoa",
                schema: "hrm");

            migrationBuilder.AddColumn<int>(
                name: "KhoaHoc",
                schema: "sv",
                table: "SvSinhVien",
                type: "int",
                nullable: true);
        }
    }
}
