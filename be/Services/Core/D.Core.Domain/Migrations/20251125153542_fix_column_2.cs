using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class fix_column_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "ReceptionDate",
                schema: "dio",
                table: "DelegationIncoming",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "RequestDate",
                schema: "dio",
                table: "DelegationIncoming",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceptionDate",
                schema: "dio",
                table: "DelegationIncoming");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                schema: "dio",
                table: "DelegationIncoming");
        }
    }
}
