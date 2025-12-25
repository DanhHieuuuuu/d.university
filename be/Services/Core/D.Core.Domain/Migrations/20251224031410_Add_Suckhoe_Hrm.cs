using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Add_Suckhoe_Hrm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CanNang",
                schema: "hrm",
                table: "NsNhanSu",
                type: "decimal(4,1)",
                precision: 4,
                scale: 1,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChieuCao",
                schema: "hrm",
                table: "NsNhanSu",
                type: "decimal(4,1)",
                precision: 4,
                scale: 1,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhatSk",
                schema: "hrm",
                table: "NsNhanSu",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhomMau",
                schema: "hrm",
                table: "NsNhanSu",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanNang",
                schema: "hrm",
                table: "NsNhanSu");

            migrationBuilder.DropColumn(
                name: "ChieuCao",
                schema: "hrm",
                table: "NsNhanSu");

            migrationBuilder.DropColumn(
                name: "NgayCapNhatSk",
                schema: "hrm",
                table: "NsNhanSu");

            migrationBuilder.DropColumn(
                name: "NhomMau",
                schema: "hrm",
                table: "NsNhanSu");
        }
    }
}
