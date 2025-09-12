using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addColumn_NsNhanSu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email2",
                schema: "hrm",
                table: "NsNhanSu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "hrm",
                table: "NsNhanSu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordKey",
                schema: "hrm",
                table: "NsNhanSu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "hrm",
                table: "NsNhanSu",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email2",
                schema: "hrm",
                table: "NsNhanSu");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "hrm",
                table: "NsNhanSu");

            migrationBuilder.DropColumn(
                name: "PasswordKey",
                schema: "hrm",
                table: "NsNhanSu");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "hrm",
                table: "NsNhanSu");
        }
    }
}
