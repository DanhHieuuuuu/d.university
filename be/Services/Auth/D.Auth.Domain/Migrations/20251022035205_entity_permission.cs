using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Auth.Domain.Migrations
{
    /// <inheritdoc />
    public partial class entity_permission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionKey = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PermissionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                schema: "auth",
                table: "RolePermission",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "auth",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                schema: "auth",
                table: "RolePermission",
                column: "PermissionId",
                principalSchema: "auth",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                schema: "auth",
                table: "RolePermission");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "auth");

            migrationBuilder.DropIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "auth",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                schema: "auth",
                table: "RolePermission");


        }
    }
}
