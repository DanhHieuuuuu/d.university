using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddSvDiemTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DtQuyDinhThangDiem",
                schema: "dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiemChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiemSoMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiemSoMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiemHe4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_DtQuyDinhThangDiem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SvDiemMonHoc",
                schema: "sv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinhVienId = table.Column<int>(type: "int", nullable: false),
                    MonHocId = table.Column<int>(type: "int", nullable: false),
                    HocKy = table.Column<int>(type: "int", nullable: false),
                    NamHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiemQuaTrinh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiemCuoiKy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiemTongKet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiemHe4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiemChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KetQua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanHoc = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_SvDiemMonHoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SvKetQuaHocKy",
                schema: "sv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinhVienId = table.Column<int>(type: "int", nullable: false),
                    HocKy = table.Column<int>(type: "int", nullable: false),
                    NamHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiemTrungBinhHocKy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiemTrungBinhTinhLuy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GpaTichLy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTinChiDat = table.Column<int>(type: "int", nullable: false),
                    SoTinChiTichLuy = table.Column<int>(type: "int", nullable: false),
                    SoTinChiNo = table.Column<int>(type: "int", nullable: false),
                    XepLoaiHocKy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    XepLoaiRenLuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiemRenLuyen = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SvKetQuaHocKy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SvThongTinHocVu",
                schema: "sv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinhVienId = table.Column<int>(type: "int", nullable: false),
                    GpaHienTai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GpaTBTichLuy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    XepLoaiHocLuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTinChiTichLuy = table.Column<int>(type: "int", nullable: false),
                    SoTinChiNo = table.Column<int>(type: "int", nullable: false),
                    CanhBaoHocVu = table.Column<bool>(type: "bit", nullable: false),
                    MucCanhBao = table.Column<int>(type: "int", nullable: false),
                    LyDoCanhBao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HocKyHienTai = table.Column<int>(type: "int", nullable: false),
                    NamHocHienTai = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_SvThongTinHocVu", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DtQuyDinhThangDiem",
                schema: "dt");

            migrationBuilder.DropTable(
                name: "SvDiemMonHoc",
                schema: "sv");

            migrationBuilder.DropTable(
                name: "SvKetQuaHocKy",
                schema: "sv");

            migrationBuilder.DropTable(
                name: "SvThongTinHocVu",
                schema: "sv");
        }
    }
}
