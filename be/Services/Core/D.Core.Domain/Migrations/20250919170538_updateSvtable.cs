using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateSvtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lop",
                schema: "sv",
                table: "SvSinhVien",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThaiHoc",
                schema: "sv",
                table: "SvSinhVien",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lop",
                schema: "sv",
                table: "SvSinhVien");

            migrationBuilder.DropColumn(
                name: "TrangThaiHoc",
                schema: "sv",
                table: "SvSinhVien");
        }
    }
}
