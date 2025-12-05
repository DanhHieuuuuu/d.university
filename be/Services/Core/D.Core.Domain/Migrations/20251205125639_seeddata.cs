using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                columns: new[] { "ChuongTrinhKhungId", "MonHocId", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "HocKy", "Id", "ModifiedBy", "ModifiedDate", "NamHoc", "TrangThai" },
                values: new object[,]
                {
                    { 1, 1, "root", false, null, null, 1, 1, null, null, "2021-2022", true },
                    { 1, 2, "root", false, null, null, 1, 3, null, null, "2021-2022", true },
                    { 1, 3, "root", false, null, null, 1, 4, null, null, "2021-2022", true },
                    { 1, 4, "root", false, null, null, 1, 2, null, null, "2021-2022", true },
                    { 2, 2, "root", false, null, null, 2, 5, null, null, "2021-2022", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                keyColumns: new[] { "ChuongTrinhKhungId", "MonHocId" },
                keyValues: new object[] { 2, 2 });
        }
    }
}
