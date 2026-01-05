using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class add_QuaTrinhCongTac_hrm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NsHopDongChiTiet",
                schema: "hrm");

            migrationBuilder.DropColumn(
                name: "KyLan1",
                schema: "hrm",
                table: "NsHopDong");

            migrationBuilder.DropColumn(
                name: "KyLan2",
                schema: "hrm",
                table: "NsHopDong");

            migrationBuilder.DropColumn(
                name: "KyLan3",
                schema: "hrm",
                table: "NsHopDong");

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                schema: "hrm",
                table: "NsHopDong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LuongCoBan",
                schema: "hrm",
                table: "NsHopDong",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NsQuaTrinhCongTac",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNhanSu = table.Column<int>(type: "int", nullable: true),
                    MaNhanSu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdChucVu = table.Column<int>(type: "int", nullable: true),
                    IdPhongBan = table.Column<int>(type: "int", nullable: true),
                    IdToBoMon = table.Column<int>(type: "int", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdQuyetDinh = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_NsQuaTrinhCongTac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NsQuyetDinh",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNhanSu = table.Column<int>(type: "int", nullable: true),
                    MaNhanSu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiQuyetDinh = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    NoiDungTomTat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_NsQuyetDinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NsQuyetDinhLog",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuyetDinh = table.Column<int>(type: "int", nullable: false),
                    OldStatus = table.Column<int>(type: "int", nullable: true),
                    NewStatus = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_NsQuyetDinhLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NsQuaTrinhCongTac",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "NsQuyetDinh",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "NsQuyetDinhLog",
                schema: "hrm");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                schema: "hrm",
                table: "NsHopDong");

            migrationBuilder.DropColumn(
                name: "LuongCoBan",
                schema: "hrm",
                table: "NsHopDong");

            migrationBuilder.AddColumn<DateTime>(
                name: "KyLan1",
                schema: "hrm",
                table: "NsHopDong",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "KyLan2",
                schema: "hrm",
                table: "NsHopDong",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "KyLan3",
                schema: "hrm",
                table: "NsHopDong",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NsHopDongChiTiet",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HsChucVu = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
                    HsKhac = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
                    HsLuong = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
                    IdChucVu = table.Column<int>(type: "int", nullable: true),
                    IdHopDong = table.Column<int>(type: "int", nullable: true),
                    IdNhanSu = table.Column<int>(type: "int", nullable: true),
                    IdPhongBan = table.Column<int>(type: "int", nullable: true),
                    IdToBoMon = table.Column<int>(type: "int", nullable: true),
                    LuongCoBan = table.Column<int>(type: "int", nullable: true),
                    MaNhanSu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NsHopDongChiTiet", x => x.Id);
                });
        }
    }
}
