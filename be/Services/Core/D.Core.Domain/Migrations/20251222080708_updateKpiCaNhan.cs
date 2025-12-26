using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateKpiCaNhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STT",
                schema: "kpi",
                table: "KpiTruong");

            migrationBuilder.DropColumn(
                name: "STT",
                schema: "kpi",
                table: "KpiDonVi");

            migrationBuilder.AlterColumn<decimal>(
                name: "TiLe",
                schema: "kpi",
                table: "KpiRole",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoaiKPI",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "LinhVuc",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinhVuc",
                schema: "kpi",
                table: "KpiCaNhan");

            migrationBuilder.AddColumn<int>(
                name: "STT",
                schema: "kpi",
                table: "KpiTruong",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TiLe",
                schema: "kpi",
                table: "KpiRole",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STT",
                schema: "kpi",
                table: "KpiDonVi",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoaiKPI",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
