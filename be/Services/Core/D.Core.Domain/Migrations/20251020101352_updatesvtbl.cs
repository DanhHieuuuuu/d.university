using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updatesvtbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lop",
                schema: "sv",
                table: "SvSinhVien",
                newName: "LopQL");

            migrationBuilder.AlterColumn<int>(
                name: "TrangThaiHoc",
                schema: "sv",
                table: "SvSinhVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

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

            migrationBuilder.RenameColumn(
                name: "LopQL",
                schema: "sv",
                table: "SvSinhVien",
                newName: "Lop");

            migrationBuilder.AlterColumn<bool>(
                name: "TrangThaiHoc",
                schema: "sv",
                table: "SvSinhVien",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
