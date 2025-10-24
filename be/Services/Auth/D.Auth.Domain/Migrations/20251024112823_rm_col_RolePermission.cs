using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Auth.Domain.Migrations
{
    /// <inheritdoc />
    public partial class rm_col_RolePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionName",
                schema: "auth",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "PermissonKey",
                schema: "auth",
                table: "RolePermission");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PermissionName",
                schema: "auth",
                table: "RolePermission",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PermissonKey",
                schema: "auth",
                table: "RolePermission",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
