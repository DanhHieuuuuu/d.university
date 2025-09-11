using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class update_hrm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLoaiHopDong",
                schema: "hrm",
                table: "NsNhanSu");

            migrationBuilder.CreateTable(
                name: "DmChucVu",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HsChucVu = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
                    HsTrachNhiem = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
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
                    table.PrimaryKey("PK_DmChucVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmLoaiHopDong",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLoaiHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenLoaiHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdBieuMau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_DmLoaiHopDong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmLoaiPhongBan",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLoaiPhongBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenLoaiPhongBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_DmLoaiPhongBan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmPhongBan",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhongBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenPhongBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLoaiPhongBan = table.Column<int>(type: "int", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hotline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    STT = table.Column<int>(type: "int", nullable: true),
                    ChucVuNguoiDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_DmPhongBan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmToBoMon",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBoMon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenBoMon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdPhongBan = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_DmToBoMon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NsHopDong",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNhanSu = table.Column<int>(type: "int", nullable: true),
                    IdLoaiHopDong = table.Column<int>(type: "int", nullable: true),
                    NgayKyKet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KyLan1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KyLan2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KyLan3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayBatDauThuViec = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayKetThucThuViec = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HopDongCoThoiHanTuNgay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HopDongCoThoiHanDenNgay = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_NsHopDong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NsHopDongChiTiet",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHopDong = table.Column<int>(type: "int", nullable: true),
                    IdNhanSu = table.Column<int>(type: "int", nullable: true),
                    MaNhanSu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdChucVu = table.Column<int>(type: "int", nullable: true),
                    IdPhongBan = table.Column<int>(type: "int", nullable: true),
                    IdToBoMon = table.Column<int>(type: "int", nullable: true),
                    LuongCoBan = table.Column<int>(type: "int", nullable: true),
                    HsChucVu = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
                    HsLuong = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
                    HsKhac = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_NsHopDongChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NsToBoMon",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdToBoMon = table.Column<int>(type: "int", nullable: true),
                    IdNhanSu = table.Column<int>(type: "int", nullable: true),
                    IsToTruong = table.Column<bool>(type: "bit", nullable: true),
                    IsToPho = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_NsToBoMon", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DmChucVu",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "DmLoaiHopDong",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "DmLoaiPhongBan",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "DmPhongBan",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "DmToBoMon",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "NsHopDong",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "NsHopDongChiTiet",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "NsToBoMon",
                schema: "hrm");

            migrationBuilder.AddColumn<int>(
                name: "IdLoaiHopDong",
                schema: "hrm",
                table: "NsNhanSu",
                type: "int",
                nullable: true);
        }
    }
}
