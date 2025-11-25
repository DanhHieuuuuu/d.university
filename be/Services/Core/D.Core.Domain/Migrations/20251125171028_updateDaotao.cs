using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateDaotao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KhoaHoc",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.DropColumn(
                name: "MaNganh",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.AlterColumn<string>(
                name: "HocKy",
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChuyenNganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KhoaHocId",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DtKhoa",
                schema: "dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKhoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenTiengAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VietTat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DtKhoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DtNganh",
                schema: "dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenTiengAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true),
                    KhoaId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DtNganh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DtNganh_DtKhoa_KhoaId",
                        column: x => x.KhoaId,
                        principalSchema: "dt",
                        principalTable: "DtKhoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DtChuyenNganh",
                schema: "dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChuyenNganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenChuyenNganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenTiengAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true),
                    NganhId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DtChuyenNganh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DtChuyenNganh_DtNganh_NganhId",
                        column: x => x.NganhId,
                        principalSchema: "dt",
                        principalTable: "DtNganh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DtMonHoc_ToBoMonId",
                schema: "dt",
                table: "DtMonHoc",
                column: "ToBoMonId");

            migrationBuilder.CreateIndex(
                name: "IX_DtChuongTrinhKhung_ChuyenNganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                column: "ChuyenNganhId");

            migrationBuilder.CreateIndex(
                name: "IX_DtChuongTrinhKhung_KhoaHocId",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                column: "KhoaHocId");

            migrationBuilder.CreateIndex(
                name: "IX_DtChuongTrinhKhung_NganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                column: "NganhId");

            migrationBuilder.CreateIndex(
                name: "IX_DtChuyenNganh_NganhId",
                schema: "dt",
                table: "DtChuyenNganh",
                column: "NganhId");

            migrationBuilder.CreateIndex(
                name: "IX_DtNganh_KhoaId",
                schema: "dt",
                table: "DtNganh",
                column: "KhoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DtChuongTrinhKhung_DmKhoaHoc_KhoaHocId",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                column: "KhoaHocId",
                principalSchema: "hrm",
                principalTable: "DmKhoaHoc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DtChuongTrinhKhung_DtChuyenNganh_ChuyenNganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                column: "ChuyenNganhId",
                principalSchema: "dt",
                principalTable: "DtChuyenNganh",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DtChuongTrinhKhung_DtNganh_NganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                column: "NganhId",
                principalSchema: "dt",
                principalTable: "DtNganh",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DtMonHoc_DmToBoMon_ToBoMonId",
                schema: "dt",
                table: "DtMonHoc",
                column: "ToBoMonId",
                principalSchema: "hrm",
                principalTable: "DmToBoMon",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DtChuongTrinhKhung_DmKhoaHoc_KhoaHocId",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.DropForeignKey(
                name: "FK_DtChuongTrinhKhung_DtChuyenNganh_ChuyenNganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.DropForeignKey(
                name: "FK_DtChuongTrinhKhung_DtNganh_NganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.DropForeignKey(
                name: "FK_DtMonHoc_DmToBoMon_ToBoMonId",
                schema: "dt",
                table: "DtMonHoc");

            migrationBuilder.DropTable(
                name: "DtChuyenNganh",
                schema: "dt");

            migrationBuilder.DropTable(
                name: "DtNganh",
                schema: "dt");

            migrationBuilder.DropTable(
                name: "DtKhoa",
                schema: "dt");

            migrationBuilder.DropIndex(
                name: "IX_DtMonHoc_ToBoMonId",
                schema: "dt",
                table: "DtMonHoc");

            migrationBuilder.DropIndex(
                name: "IX_DtChuongTrinhKhung_ChuyenNganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.DropIndex(
                name: "IX_DtChuongTrinhKhung_KhoaHocId",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.DropIndex(
                name: "IX_DtChuongTrinhKhung_NganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.DropColumn(
                name: "ChuyenNganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.DropColumn(
                name: "KhoaHocId",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.DropColumn(
                name: "NganhId",
                schema: "dt",
                table: "DtChuongTrinhKhung");

            migrationBuilder.AlterColumn<int>(
                name: "HocKy",
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KhoaHoc",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaNganh",
                schema: "dt",
                table: "DtChuongTrinhKhung",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
