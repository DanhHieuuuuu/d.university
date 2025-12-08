using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityKpiModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "kpi");

            migrationBuilder.CreateTable(
                name: "KpiCaNhan",
                schema: "kpi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<int>(type: "int", nullable: false),
                    KPI = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MucTieu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrongSo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LoaiKPI = table.Column<int>(type: "int", nullable: false),
                    IdNhanSu = table.Column<int>(type: "int", nullable: false),
                    IdKpiDonVi = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    KetQuaThucTe = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CongThucTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiCongThuc = table.Column<int>(type: "int", nullable: true),
                    ThamSoCongThuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiemKpi = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.PrimaryKey("PK_KpiCaNhan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiDonVi",
                schema: "kpi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kpi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MucTieu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrongSo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IdDonVi = table.Column<int>(type: "int", nullable: true),
                    LoaiKpi = table.Column<int>(type: "int", nullable: true),
                    NamHoc = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    IdKpiTruong = table.Column<int>(type: "int", nullable: true),
                    KetQuaThucTe = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CongThucTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiCongThuc = table.Column<int>(type: "int", nullable: true),
                    ThamSoCongThuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiemKpi = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.PrimaryKey("PK_KpiDonVi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiLogStatus",
                schema: "kpi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KpiId = table.Column<int>(type: "int", nullable: true),
                    OldStatus = table.Column<int>(type: "int", nullable: true),
                    NewStatus = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_KpiLogStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiRole",
                schema: "kpi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNhanSu = table.Column<int>(type: "int", nullable: false),
                    IdDonVi = table.Column<int>(type: "int", nullable: true),
                    TiLe = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true),
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
                    table.PrimaryKey("PK_KpiRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiTemplate",
                schema: "kpi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KPI = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    STT = table.Column<int>(type: "int", nullable: false),
                    MucTieu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrongSo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoaiKPI = table.Column<int>(type: "int", nullable: false),
                    LoaiTemplate = table.Column<int>(type: "int", nullable: false),
                    NamHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_KpiTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiTruong",
                schema: "kpi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinhVuc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ChienLuoc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Kpi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MucTieu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrongSo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoaiKpi = table.Column<int>(type: "int", nullable: true),
                    NamHoc = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    KetQuaThucTe = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CongThucTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiCongThuc = table.Column<int>(type: "int", nullable: true),
                    ThamSoCongThuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiemKpi = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.PrimaryKey("PK_KpiTruong", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KpiCaNhan",
                schema: "kpi");

            migrationBuilder.DropTable(
                name: "KpiDonVi",
                schema: "kpi");

            migrationBuilder.DropTable(
                name: "KpiLogStatus",
                schema: "kpi");

            migrationBuilder.DropTable(
                name: "KpiRole",
                schema: "kpi");

            migrationBuilder.DropTable(
                name: "KpiTemplate",
                schema: "kpi");

            migrationBuilder.DropTable(
                name: "KpiTruong",
                schema: "kpi");
        }
    }
}
