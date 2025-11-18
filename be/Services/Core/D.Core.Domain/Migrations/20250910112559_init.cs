using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "hrm");

            migrationBuilder.CreateTable(
                name: "NsNhanSu",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanSu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoDem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoiSinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    QuocTich = table.Column<int>(type: "int", nullable: true),
                    DanToc = table.Column<int>(type: "int", nullable: true),
                    TonGiao = table.Column<int>(type: "int", nullable: true),
                    NguyenQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiOHienTai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoCccd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapCccd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoiCapCccd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KhanCapSoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KhanCapNguoiLienHe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSoThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNganHang1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Atm1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNganHang2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Atm2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HienTaiChucVu = table.Column<int>(type: "int", nullable: true),
                    HienTaiPhongBan = table.Column<int>(type: "int", nullable: true),
                    IdLoaiHopDong = table.Column<int>(type: "int", nullable: true),
                    IdHopDong = table.Column<int>(type: "int", nullable: true),
                    DaChamDutHopDong = table.Column<bool>(type: "bit", nullable: true),
                    DaVeHuu = table.Column<bool>(type: "bit", nullable: true),
                    IsThoiViec = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_NsNhanSu", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NsNhanSu",
                schema: "hrm");
        }
    }
}
