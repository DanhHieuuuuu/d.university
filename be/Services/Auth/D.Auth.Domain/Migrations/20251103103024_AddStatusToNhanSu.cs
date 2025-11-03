using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Auth.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToNhanSu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "hrm",
                table: "NsNhanSu",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "hrm",
                table: "NsNhanSu");
        }
    }
}
