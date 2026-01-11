using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKpiLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapKpi",
                schema: "kpi",
                table: "KpiLogStatus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                schema: "kpi",
                table: "KpiLogStatus",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapKpi",
                schema: "kpi",
                table: "KpiLogStatus");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                schema: "kpi",
                table: "KpiLogStatus");
        }
    }
}
