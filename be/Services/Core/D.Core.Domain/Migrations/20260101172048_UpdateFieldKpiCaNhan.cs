using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldKpiCaNhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CapTrenDanhGia",
                schema: "kpi",
                table: "KpiDonVi",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiemKpiCapTren",
                schema: "kpi",
                table: "KpiDonVi",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CapTrenDanhGia",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiemKpiCapTren",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapTrenDanhGia",
                schema: "kpi",
                table: "KpiDonVi");

            migrationBuilder.DropColumn(
                name: "DiemKpiCapTren",
                schema: "kpi",
                table: "KpiDonVi");

            migrationBuilder.DropColumn(
                name: "CapTrenDanhGia",
                schema: "kpi",
                table: "KpiCaNhan");

            migrationBuilder.DropColumn(
                name: "DiemKpiCapTren",
                schema: "kpi",
                table: "KpiCaNhan");
        }
    }
}
