using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class SeedDtQuyDinhThangDiem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "DiemChu", "DiemHe4", "DiemSoMax", "DiemSoMin", "ModifiedBy", "ModifiedDate", "Mota", "TrangThai" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, "A+", 4.0m, 10.0m, 9.0m, null, null, "Xuất sắc", true },
                    { 2, "root", false, null, null, "A", 3.7m, 8.9m, 8.5m, null, null, "Giỏi", true },
                    { 3, "root", false, null, null, "B+", 3.5m, 8.4m, 8.0m, null, null, "Khá giỏi", true },
                    { 4, "root", false, null, null, "B", 3.0m, 7.9m, 7.0m, null, null, "Khá", true },
                    { 5, "root", false, null, null, "C+", 2.5m, 6.9m, 6.5m, null, null, "Trung bình khá", true },
                    { 6, "root", false, null, null, "C", 2.0m, 6.4m, 5.5m, null, null, "Trung bình", true },
                    { 7, "root", false, null, null, "D+", 1.5m, 5.4m, 5.0m, null, null, "Yếu", true },
                    { 8, "root", false, null, null, "D", 1.0m, 4.9m, 4.0m, null, null, "Kém", true },
                    { 9, "root", false, null, null, "F", 0.0m, 3.9m, 0.0m, null, null, "Không đạt", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "dt",
                table: "DtQuyDinhThangDiem",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
