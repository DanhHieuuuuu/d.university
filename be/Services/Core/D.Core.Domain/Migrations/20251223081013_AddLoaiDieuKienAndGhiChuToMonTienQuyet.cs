using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddLoaiDieuKienAndGhiChuToMonTienQuyet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                schema: "dt",
                table: "DtMonTienQuyet",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoaiDieuKien",
                schema: "dt",
                table: "DtMonTienQuyet",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                schema: "dt",
                table: "DtMonTienQuyet");

            migrationBuilder.DropColumn(
                name: "LoaiDieuKien",
                schema: "dt",
                table: "DtMonTienQuyet");
        }
    }
}
