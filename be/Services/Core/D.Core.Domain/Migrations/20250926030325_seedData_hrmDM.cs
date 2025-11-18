using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace D.Core.Domain.Migrations
{
    /// <inheritdoc />
    public partial class seedData_hrmDM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsToBoMon",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsQuanHeGiaDinh",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsNhanSu",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsHopDongChiTiet",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsHopDong",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmTonGiao",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmToBoMon",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmQuocTich",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmPhongBan",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmLoaiPhongBan",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmLoaiHopDong",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmGioiTinh",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmDanToc",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmChucVu",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                schema: "hrm",
                table: "DmDanToc",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "MaDanToc", "ModifiedBy", "ModifiedDate", "TenDanToc" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, "Kinh", null, null, "Kinh" },
                    { 2, "root", false, null, null, "Tay", null, null, "Tày" },
                    { 3, "root", false, null, null, "Thai", null, null, "Thái" },
                    { 4, "root", false, null, null, "Muong", null, null, "Mường" },
                    { 5, "root", false, null, null, "Hoa", null, null, "Hoa" },
                    { 6, "root", false, null, null, "Khmer", null, null, "Khơ Me" },
                    { 7, "root", false, null, null, "Nung", null, null, "Nùng" },
                    { 8, "root", false, null, null, "Hmong", null, null, "H'Mông" },
                    { 9, "root", false, null, null, "Dao", null, null, "Dao" },
                    { 10, "root", false, null, null, "GiaRai", null, null, "Gia Rai" },
                    { 11, "root", false, null, null, "Ede", null, null, "Ê Đê" },
                    { 12, "root", false, null, null, "BaNa", null, null, "Ba Na" },
                    { 13, "root", false, null, null, "SanChay", null, null, "Sán Chay" },
                    { 14, "root", false, null, null, "Cham", null, null, "Chăm" },
                    { 15, "root", false, null, null, "SanDiu", null, null, "Sán Dìu" },
                    { 16, "root", false, null, null, "CoHo", null, null, "Cơ Ho" },
                    { 17, "root", false, null, null, "XoDang", null, null, "Xơ Đăng" },
                    { 18, "root", false, null, null, "RaGlai", null, null, "Ra Glai" },
                    { 19, "root", false, null, null, "Mnong", null, null, "M'Nông" },
                    { 20, "root", false, null, null, "CoTu", null, null, "Cơ Tu" },
                    { 21, "root", false, null, null, "Xtieng", null, null, "X'tiêng" },
                    { 22, "root", false, null, null, "GieTrieng", null, null, "Gié Triêng" },
                    { 23, "root", false, null, null, "Co", null, null, "Cơ" },
                    { 24, "root", false, null, null, "TaOi", null, null, "Tà Ôi" },
                    { 25, "root", false, null, null, "Ma", null, null, "Mạ" },
                    { 26, "root", false, null, null, "BruVanKieu", null, null, "Bru - Vân Kiều" },
                    { 27, "root", false, null, null, "Khmu", null, null, "Khơ Mú" },
                    { 28, "root", false, null, null, "Mang", null, null, "Mảng" },
                    { 29, "root", false, null, null, "Khang", null, null, "Kháng" },
                    { 30, "root", false, null, null, "LaHa", null, null, "La Ha" },
                    { 31, "root", false, null, null, "Cong", null, null, "Cống" },
                    { 32, "root", false, null, null, "SiLa", null, null, "Si La" },
                    { 33, "root", false, null, null, "HaNhi", null, null, "Hà Nhì" },
                    { 34, "root", false, null, null, "LaChi", null, null, "La Chí" },
                    { 35, "root", false, null, null, "PhuLa", null, null, "Phù Lá" },
                    { 36, "root", false, null, null, "LaHu", null, null, "La Hủ" },
                    { 37, "root", false, null, null, "PaThen", null, null, "Pà Thẻn" },
                    { 38, "root", false, null, null, "LoLo", null, null, "Lô Lô" },
                    { 39, "root", false, null, null, "Churu", null, null, "Chu Ru" },
                    { 40, "root", false, null, null, "Lao", null, null, "Lào" },
                    { 41, "root", false, null, null, "Lu", null, null, "Lự" },
                    { 42, "root", false, null, null, "Ngai", null, null, "Ngái" },
                    { 43, "root", false, null, null, "Giay", null, null, "Giáy" },
                    { 44, "root", false, null, null, "Chut", null, null, "Chứt" },
                    { 45, "root", false, null, null, "Odu", null, null, "Ơ Đu" },
                    { 46, "root", false, null, null, "Brau", null, null, "Brâu" },
                    { 47, "root", false, null, null, "Romam", null, null, "Rơ Măm" },
                    { 48, "root", false, null, null, "BoY", null, null, "Bố Y" },
                    { 49, "root", false, null, null, "CoLao", null, null, "Cờ Lao" },
                    { 50, "root", false, null, null, "PuPeo", null, null, "Pu Péo" }
                });

            migrationBuilder.InsertData(
                schema: "hrm",
                table: "DmGioiTinh",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "MaGioiTinh", "ModifiedBy", "ModifiedDate", "TenGioiTinh" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, "Male", null, null, "Nam" },
                    { 2, "root", false, null, null, "Female", null, null, "Nữ" }
                });

            migrationBuilder.InsertData(
                schema: "hrm",
                table: "DmLoaiHopDong",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "IdBieuMau", "IsActive", "MaLoaiHopDong", "ModifiedBy", "ModifiedDate", "TenLoaiHopDong" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, null, true, "HD00", null, null, "Hợp đồng thử việc" },
                    { 2, "root", false, null, null, null, true, "HD01", null, null, "Hợp đồng làm việc có thời hạn" },
                    { 3, "root", false, null, null, null, true, "HD02", null, null, "Hợp đồng làm việc không thời hạn" },
                    { 4, "root", false, null, null, null, true, "HD03", null, null, "Hợp đồng lao động có thời hạn" },
                    { 5, "root", false, null, null, null, true, "HD04", null, null, "Hợp đồng lao động không thời hạn" }
                });

            migrationBuilder.InsertData(
                schema: "hrm",
                table: "DmLoaiPhongBan",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "MaLoaiPhongBan", "ModifiedBy", "ModifiedDate", "TenLoaiPhongBan" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, "KHAC", null, null, "Khác" },
                    { 2, "root", false, null, null, "KHOA", null, null, "Khoa" },
                    { 3, "root", false, null, null, "BAN", null, null, "Phòng - Ban" },
                    { 4, "root", false, null, null, "VIEN", null, null, "Viện" },
                    { 5, "root", false, null, null, "TRUNGTAM", null, null, "Trung tâm" }
                });

            migrationBuilder.InsertData(
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "MaQuanHe", "ModifiedBy", "ModifiedDate", "TenQuanHe" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, "VoChong", null, null, "Vợ/Chồng" },
                    { 2, "root", false, null, null, "Con", null, null, "Con" },
                    { 3, "root", false, null, null, "BoDe", null, null, "Bố" },
                    { 4, "root", false, null, null, "MeDe", null, null, "Mẹ" },
                    { 5, "root", false, null, null, "Ace", null, null, "Anh/Chị/Em" },
                    { 6, "root", false, null, null, "BoVC", null, null, "Bố (chồng, vợ)" },
                    { 7, "root", false, null, null, "MeVC", null, null, "Mẹ (chồng, vợ)" },
                    { 8, "root", false, null, null, "AceVC", null, null, "Anh/Chị/Em (chồng, vợ)" }
                });

            migrationBuilder.InsertData(
                schema: "hrm",
                table: "DmQuocTich",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "MaQuocGia", "ModifiedBy", "ModifiedDate", "STT", "TenQuocGia", "TenQuocGia_EN" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, "VN", null, null, null, "Việt Nam", "Vietnam" },
                    { 2, "root", false, null, null, "US", null, null, null, "Hoa Kỳ", "United States" },
                    { 3, "root", false, null, null, "CN", null, null, null, "Trung Quốc", "China" },
                    { 4, "root", false, null, null, "JP", null, null, null, "Nhật Bản", "Japan" },
                    { 5, "root", false, null, null, "KR", null, null, null, "Hàn Quốc", "South Korea" },
                    { 6, "root", false, null, null, "RU", null, null, null, "Nga", "Russia" },
                    { 7, "root", false, null, null, "FR", null, null, null, "Pháp", "France" },
                    { 8, "root", false, null, null, "DE", null, null, null, "Đức", "Germany" },
                    { 9, "root", false, null, null, "GB", null, null, null, "Anh", "United Kingdom" },
                    { 10, "root", false, null, null, "AU", null, null, null, "Úc", "Australia" },
                    { 11, "root", false, null, null, "CA", null, null, null, "Canada", "Canada" },
                    { 12, "root", false, null, null, "LA", null, null, null, "Lào", "Laos" },
                    { 13, "root", false, null, null, "KH", null, null, null, "Campuchia", "Cambodia" },
                    { 14, "root", false, null, null, "TH", null, null, null, "Thái Lan", "Thailand" },
                    { 15, "root", false, null, null, "MY", null, null, null, "Malaysia", "Malaysia" },
                    { 16, "root", false, null, null, "SG", null, null, null, "Singapore", "Singapore" },
                    { 17, "root", false, null, null, "PH", null, null, null, "Philippines", "Philippines" },
                    { 18, "root", false, null, null, "ID", null, null, null, "Indonesia", "Indonesia" },
                    { 19, "root", false, null, null, "IN", null, null, null, "Ấn Độ", "India" },
                    { 20, "root", false, null, null, "IT", null, null, null, "Ý", "Italy" },
                    { 21, "root", false, null, null, "ES", null, null, null, "Tây Ban Nha", "Spain" },
                    { 22, "root", false, null, null, "NL", null, null, null, "Hà Lan", "Netherlands" },
                    { 23, "root", false, null, null, "CH", null, null, null, "Thụy Sĩ", "Switzerland" },
                    { 24, "root", false, null, null, "SE", null, null, null, "Thụy Điển", "Sweden" },
                    { 25, "root", false, null, null, "NO", null, null, null, "Na Uy", "Norway" },
                    { 26, "root", false, null, null, "DK", null, null, null, "Đan Mạch", "Denmark" },
                    { 27, "root", false, null, null, "FI", null, null, null, "Phần Lan", "Finland" },
                    { 28, "root", false, null, null, "BE", null, null, null, "Bỉ", "Belgium" },
                    { 29, "root", false, null, null, "AT", null, null, null, "Áo", "Austria" },
                    { 30, "root", false, null, null, "NZ", null, null, null, "New Zealand", "New Zealand" },
                    { 31, "root", false, null, null, "BR", null, null, null, "Brazil", "Brazil" },
                    { 32, "root", false, null, null, "AR", null, null, null, "Argentina", "Argentina" },
                    { 33, "root", false, null, null, "MX", null, null, null, "Mexico", "Mexico" },
                    { 34, "root", false, null, null, "CL", null, null, null, "Chile", "Chile" },
                    { 35, "root", false, null, null, "ZA", null, null, null, "Nam Phi", "South Africa" },
                    { 36, "root", false, null, null, "AE", null, null, null, "Các Tiểu vương quốc Ả Rập Thống nhất", "United Arab Emirates" },
                    { 37, "root", false, null, null, "SA", null, null, null, "Ả Rập Xê Út", "Saudi Arabia" },
                    { 38, "root", false, null, null, "TR", null, null, null, "Thổ Nhĩ Kỳ", "Turkey" },
                    { 39, "root", false, null, null, "PK", null, null, null, "Pakistan", "Pakistan" },
                    { 40, "root", false, null, null, "BD", null, null, null, "Bangladesh", "Bangladesh" },
                    { 41, "root", false, null, null, "LK", null, null, null, "Sri Lanka", "Sri Lanka" },
                    { 42, "root", false, null, null, "MM", null, null, null, "Myanmar", "Myanmar" },
                    { 43, "root", false, null, null, "NP", null, null, null, "Nepal", "Nepal" },
                    { 44, "root", false, null, null, "IR", null, null, null, "Iran", "Iran" },
                    { 45, "root", false, null, null, "IQ", null, null, null, "Iraq", "Iraq" },
                    { 46, "root", false, null, null, "EG", null, null, null, "Ai Cập", "Egypt" },
                    { 47, "root", false, null, null, "IL", null, null, null, "Israel", "Israel" },
                    { 48, "root", false, null, null, "JO", null, null, null, "Jordan", "Jordan" },
                    { 49, "root", false, null, null, "QA", null, null, null, "Qatar", "Qatar" },
                    { 50, "root", false, null, null, "KW", null, null, null, "Kuwait", "Kuwait" },
                    { 51, "root", false, null, null, "NG", null, null, null, "Nigeria", "Nigeria" },
                    { 52, "root", false, null, null, "KE", null, null, null, "Kenya", "Kenya" }
                });

            migrationBuilder.InsertData(
                schema: "hrm",
                table: "DmTonGiao",
                columns: new[] { "Id", "CreatedBy", "Deleted", "DeletedBy", "DeletedDate", "MaTonGiao", "ModifiedBy", "ModifiedDate", "TenTonGiao" },
                values: new object[,]
                {
                    { 1, "root", false, null, null, "K", null, null, "Không" },
                    { 2, "root", false, null, null, "PG", null, null, "Phật giáo" },
                    { 3, "root", false, null, null, "CG", null, null, "Công giáo" },
                    { 4, "root", false, null, null, "TL", null, null, "Tin lành" },
                    { 5, "root", false, null, null, "CD", null, null, "Cao Đài" },
                    { 6, "root", false, null, null, "HDM", null, null, "Hồi giáo" },
                    { 7, "root", false, null, null, "PGHH", null, null, "Phật giáo Hòa Hảo" },
                    { 8, "root", false, null, null, "BAH", null, null, "Baha'i" },
                    { 9, "root", false, null, null, "TDCSPH", null, null, "Tịnh độ Cư sĩ Phật hội" },
                    { 10, "root", false, null, null, "CDPL", null, null, "Cơ đốc Phục Lâm" },
                    { 11, "root", false, null, null, "PGTÂ", null, null, "Phật giáo Tứ Ân Hiếu nghĩa" },
                    { 12, "root", false, null, null, "MSĐ", null, null, "Minh Sư đạo" },
                    { 13, "root", false, null, null, "MLĐ", null, null, "Minh Lý đạo – Tam Tông Miếu" },
                    { 14, "root", false, null, null, "BLM", null, null, "Bà-la-môn giáo" },
                    { 15, "root", false, null, null, "MM", null, null, "Mặc Môn" },
                    { 16, "root", false, null, null, "PGHTL", null, null, "Phật giáo Hiếu nghĩa Tà Lơn" },
                    { 17, "root", false, null, null, "BSKH", null, null, "Bửu Sơn Kỳ Hương" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmDanToc",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmGioiTinh",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmGioiTinh",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiHopDong",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiHopDong",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiHopDong",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiHopDong",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiHopDong",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiPhongBan",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiPhongBan",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiPhongBan",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiPhongBan",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmLoaiPhongBan",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmQuocTich",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "hrm",
                table: "DmTonGiao",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsToBoMon",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsQuanHeGiaDinh",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsNhanSu",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsHopDongChiTiet",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "NsHopDong",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmTonGiao",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmToBoMon",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmQuocTich",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmQuanHeGiaDinh",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmPhongBan",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmLoaiPhongBan",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmLoaiHopDong",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmGioiTinh",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmDanToc",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "hrm",
                table: "DmChucVu",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
