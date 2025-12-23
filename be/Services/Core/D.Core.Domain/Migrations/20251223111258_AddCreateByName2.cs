using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateByName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByName",
                schema: "dio",
                table: "ReceptionTime");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                schema: "dio",
                table: "LogReceptionTime",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByName",
                schema: "dio",
                table: "LogReceptionTime");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                schema: "dio",
                table: "ReceptionTime",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
