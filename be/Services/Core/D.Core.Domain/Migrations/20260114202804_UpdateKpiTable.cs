using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKpiTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsKeKhai",
                schema: "kpi",
                table: "KpiTruong");

            migrationBuilder.DropColumn(
                name: "IsKeKhai",
                schema: "kpi",
                table: "KpiDonVi");

            migrationBuilder.DropColumn(
                name: "IsCaNhanKeKhai",
                schema: "kpi",
                table: "KpiCaNhan");

            migrationBuilder.DropColumn(
                name: "STT",
                schema: "kpi",
                table: "KpiCaNhan");

            migrationBuilder.AlterColumn<string>(
                name: "Kpi",
                schema: "kpi",
                table: "KpiTruong",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Kpi",
                schema: "kpi",
                table: "KpiDonVi",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KPI",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdKpiDonVi",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Kpi",
                schema: "kpi",
                table: "KpiTruong",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<bool>(
                name: "IsKeKhai",
                schema: "kpi",
                table: "KpiTruong",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Kpi",
                schema: "kpi",
                table: "KpiDonVi",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<bool>(
                name: "IsKeKhai",
                schema: "kpi",
                table: "KpiDonVi",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KPI",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "IdKpiDonVi",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCaNhanKeKhai",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STT",
                schema: "kpi",
                table: "KpiCaNhan",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
