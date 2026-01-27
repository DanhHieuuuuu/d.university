using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updatesurvey5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                schema: "ks",
                table: "KsSurveySubmission",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "ks",
                table: "KsSurveySubmission");

            
        }
    }
}
