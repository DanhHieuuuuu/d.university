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
            migrationBuilder.DropTable(
                name: "DmChuongTrinhKhungMon",
                schema: "dt");

            migrationBuilder.DropTable(
                name: "DmMonTienQuyet",
                schema: "dt");

            migrationBuilder.DropTable(
                name: "DmChuongTrinhKhung",
                schema: "dt");

            migrationBuilder.DropTable(
                name: "DmMonHoc",
                schema: "dt");

            migrationBuilder.CreateTable(
                name: "DtChuongTrinhKhung",
                schema: "dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChuongTrinhKhung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenChuongTrinhKhung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhoaHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongSoTinChi = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_DtChuongTrinhKhung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DtMonHoc",
                schema: "dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaMonHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenMonHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
                    SoTietLyThuyet = table.Column<int>(type: "int", nullable: true),
                    SoTietThucHanh = table.Column<int>(type: "int", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    ToBoMonId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_DtMonHoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DtChuongTrinhKhungMon",
                schema: "dt",
                columns: table => new
                {
                    ChuongTrinhKhungId = table.Column<int>(type: "int", nullable: false),
                    MonHocId = table.Column<int>(type: "int", nullable: false),
                    HocKy = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_DtChuongTrinhKhungMon", x => new { x.ChuongTrinhKhungId, x.MonHocId });
                    table.ForeignKey(
                        name: "FK_DtChuongTrinhKhungMon_DtChuongTrinhKhung_ChuongTrinhKhungId",
                        column: x => x.ChuongTrinhKhungId,
                        principalSchema: "dt",
                        principalTable: "DtChuongTrinhKhung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DtChuongTrinhKhungMon_DtMonHoc_MonHocId",
                        column: x => x.MonHocId,
                        principalSchema: "dt",
                        principalTable: "DtMonHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DtMonTienQuyet",
                schema: "dt",
                columns: table => new
                {
                    MonHocId = table.Column<int>(type: "int", nullable: false),
                    MonTienQuyetId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_DtMonTienQuyet", x => new { x.MonHocId, x.MonTienQuyetId });
                    table.ForeignKey(
                        name: "FK_DtMonTienQuyet_DtMonHoc_MonHocId",
                        column: x => x.MonHocId,
                        principalSchema: "dt",
                        principalTable: "DtMonHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DtMonTienQuyet_DtMonHoc_MonTienQuyetId",
                        column: x => x.MonTienQuyetId,
                        principalSchema: "dt",
                        principalTable: "DtMonHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DtChuongTrinhKhungMon_MonHocId",
                schema: "dt",
                table: "DtChuongTrinhKhungMon",
                column: "MonHocId");

            migrationBuilder.CreateIndex(
                name: "IX_DtMonTienQuyet_MonTienQuyetId",
                schema: "dt",
                table: "DtMonTienQuyet",
                column: "MonTienQuyetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DtChuongTrinhKhungMon",
                schema: "dt");

            migrationBuilder.DropTable(
                name: "DtMonTienQuyet",
                schema: "dt");

            migrationBuilder.DropTable(
                name: "DtChuongTrinhKhung",
                schema: "dt");

            migrationBuilder.DropTable(
                name: "DtMonHoc",
                schema: "dt");

            migrationBuilder.CreateTable(
                name: "DmChuongTrinhKhung",
                schema: "dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KhoaHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaChuongTrinhKhung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenChuongTrinhKhung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongSoTinChi = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmChuongTrinhKhung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmMonHoc",
                schema: "dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaMonHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoTietLyThuyet = table.Column<int>(type: "int", nullable: true),
                    SoTietThucHanh = table.Column<int>(type: "int", nullable: true),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
                    TenMonHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToBoMonId = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmMonHoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmChuongTrinhKhungMon",
                schema: "dt",
                columns: table => new
                {
                    ChuongTrinhKhungId = table.Column<int>(type: "int", nullable: false),
                    MonHocId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HocKy = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmChuongTrinhKhungMon", x => new { x.ChuongTrinhKhungId, x.MonHocId });
                    table.ForeignKey(
                        name: "FK_DmChuongTrinhKhungMon_DmChuongTrinhKhung_ChuongTrinhKhungId",
                        column: x => x.ChuongTrinhKhungId,
                        principalSchema: "dt",
                        principalTable: "DmChuongTrinhKhung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DmChuongTrinhKhungMon_DmMonHoc_MonHocId",
                        column: x => x.MonHocId,
                        principalSchema: "dt",
                        principalTable: "DmMonHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DmMonTienQuyet",
                schema: "dt",
                columns: table => new
                {
                    MonHocId = table.Column<int>(type: "int", nullable: false),
                    MonTienQuyetId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmMonTienQuyet", x => new { x.MonHocId, x.MonTienQuyetId });
                    table.ForeignKey(
                        name: "FK_DmMonTienQuyet_DmMonHoc_MonHocId",
                        column: x => x.MonHocId,
                        principalSchema: "dt",
                        principalTable: "DmMonHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DmMonTienQuyet_DmMonHoc_MonTienQuyetId",
                        column: x => x.MonTienQuyetId,
                        principalSchema: "dt",
                        principalTable: "DmMonHoc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DmChuongTrinhKhungMon_MonHocId",
                schema: "dt",
                table: "DmChuongTrinhKhungMon",
                column: "MonHocId");

            migrationBuilder.CreateIndex(
                name: "IX_DmMonTienQuyet_MonTienQuyetId",
                schema: "dt",
                table: "DmMonTienQuyet",
                column: "MonTienQuyetId");
        }
    }
}
