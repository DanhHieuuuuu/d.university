using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Auth.Domain.Migrations.AuthDB
{
    /// <inheritdoc />
    public partial class AddUserType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                schema: "hrm",
                table: "NsNhanSu",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "hrm",
                table: "NsNhanSu");
        }
    }
}
