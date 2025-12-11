using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updatesv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KhoaHoc",
                schema: "sv",
                table: "SvSinhVien",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KhoaHoc",
                schema: "sv",
                table: "SvSinhVien");
        }
    }
}
