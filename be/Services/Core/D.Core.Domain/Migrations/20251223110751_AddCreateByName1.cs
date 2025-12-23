using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateByName1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateByName",
                schema: "dio",
                table: "LogStatus",
                newName: "CreatedByName");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                schema: "dio",
                table: "ReceptionTime",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByName",
                schema: "dio",
                table: "ReceptionTime");

            migrationBuilder.RenameColumn(
                name: "CreatedByName",
                schema: "dio",
                table: "LogStatus",
                newName: "CreateByName");
        }
    }
}
