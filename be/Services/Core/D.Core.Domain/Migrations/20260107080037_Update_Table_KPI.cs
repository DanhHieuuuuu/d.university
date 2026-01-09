using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_KPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiCongThuc",
                schema: "kpi",
                table: "KpiDonVi");

            migrationBuilder.RenameColumn(
                name: "LoaiCongThuc",
                schema: "kpi",
                table: "KpiTruong",
                newName: "IdCongThuc");

            migrationBuilder.RenameColumn(
                name: "ThamSoCongThuc",
                schema: "kpi",
                table: "KpiDonVi",
                newName: "LoaiKetQua");

            migrationBuilder.RenameColumn(
                name: "ThamSoCongThuc",
                schema: "kpi",
                table: "KpiCaNhan",
                newName: "LoaiKetQua");

            migrationBuilder.RenameColumn(
                name: "LoaiCongThuc",
                schema: "kpi",
                table: "KpiCaNhan",
                newName: "IdCongThuc");

            migrationBuilder.AddColumn<decimal>(
                name: "CapTrenDanhGia",
                schema: "kpi",
                table: "KpiTruong",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiemKpiCapTren",
                schema: "kpi",
                table: "KpiTruong",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsKeKhai",
                schema: "kpi",
                table: "KpiTruong",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoaiKetQua",
                schema: "kpi",
                table: "KpiTruong",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                schema: "kpi",
                table: "KpiDonVi",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCongThuc",
                schema: "kpi",
                table: "KpiDonVi",
                type: "int",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsKeKhai",
                schema: "kpi",
                table: "KpiDonVi",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChienLuoc",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCaNhanKeKhai",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapTrenDanhGia",
                schema: "kpi",
                table: "KpiTruong");

            migrationBuilder.DropColumn(
                name: "DiemKpiCapTren",
                schema: "kpi",
                table: "KpiTruong");

            migrationBuilder.DropColumn(
                name: "IsKeKhai",
                schema: "kpi",
                table: "KpiTruong");

            migrationBuilder.DropColumn(
                name: "LoaiKetQua",
                schema: "kpi",
                table: "KpiTruong");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                schema: "kpi",
                table: "KpiDonVi");

            migrationBuilder.DropColumn(
                name: "IdCongThuc",
                schema: "kpi",
                table: "KpiDonVi");

            migrationBuilder.DropColumn(
                name: "IsKeKhai",
                schema: "kpi",
                table: "KpiDonVi");

            migrationBuilder.DropColumn(
                name: "ChienLuoc",
                schema: "kpi",
                table: "KpiCaNhan");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                schema: "kpi",
                table: "KpiCaNhan");

            migrationBuilder.DropColumn(
                name: "IsCaNhanKeKhai",
                schema: "kpi",
                table: "KpiCaNhan");

            migrationBuilder.RenameColumn(
                name: "IdCongThuc",
                schema: "kpi",
                table: "KpiTruong",
                newName: "LoaiCongThuc");

            migrationBuilder.RenameColumn(
                name: "LoaiKetQua",
                schema: "kpi",
                table: "KpiDonVi",
                newName: "ThamSoCongThuc");

            migrationBuilder.RenameColumn(
                name: "LoaiKetQua",
                schema: "kpi",
                table: "KpiCaNhan",
                newName: "ThamSoCongThuc");

            migrationBuilder.RenameColumn(
                name: "IdCongThuc",
                schema: "kpi",
                table: "KpiCaNhan",
                newName: "LoaiCongThuc");

            migrationBuilder.AddColumn<int>(
                name: "LoaiCongThuc",
                schema: "kpi",
                table: "KpiDonVi",
                type: "int",
                nullable: true);
        }
    }
}
