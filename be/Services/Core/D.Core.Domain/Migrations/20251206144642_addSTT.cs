using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addSTT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "STT",
                schema: "kpi",
                table: "KpiTruong",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STT",
                schema: "kpi",
                table: "KpiDonVi",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STT",
                schema: "kpi",
                table: "KpiTruong");

            migrationBuilder.DropColumn(
                name: "STT",
                schema: "kpi",
                table: "KpiDonVi");
        }
    }
}
