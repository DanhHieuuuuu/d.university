using System.Text;
using System.Text.Json;
using AutoMapper;
using D.Core.Domain.Dtos.Hrm.SemanticSearch;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.Embedding.Jina.Services;
using D.InfrastructureBase.Service;
using D.QdrantClient.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace D.Core.Infrastructure.Services.Hrm.Implements
{
    public class NhanSuQdrantService : ServiceBase, INhanSuQdrantService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IQdrantClientService _qdrant;
        private readonly IJinaEmbeddingService _jina;

        public NhanSuQdrantService(
            ILogger<NhanSuQdrantService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IQdrantClientService qdrant,
            IJinaEmbeddingService jina
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _qdrant = qdrant;
            _jina = jina;
        }

        public async Task SyncAllAsync(FetchNhanSuQdrantDto dto, CancellationToken ct = default)
        {
            // Kiểm tra đã tồn tại collectio hay chưa
            await _qdrant.EnsureCollectionAsync(ct);

            Dictionary<int, string> pbDict =
                _unitOfWork.iDmPhongBanRepository.TableNoTracking.ToDictionary(
                    x => x.Id,
                    x => x.TenPhongBan
                );

            Dictionary<int, string> cvDict =
                _unitOfWork.iDmChucVuRepository.TableNoTracking.ToDictionary(
                    x => x.Id,
                    x => x.TenChucVu
                );

            Dictionary<int, string> dtDict =
                _unitOfWork.iDmDanTocRepository.TableNoTracking.ToDictionary(
                    x => x.Id,
                    x => x.TenDanToc
                );

            Dictionary<int, string> gtDict =
                _unitOfWork.iDmGioiTinhRepository.TableNoTracking.ToDictionary(
                    x => x.Id,
                    x => x.TenGioiTinh
                );

            Dictionary<int, string> qtDict =
                _unitOfWork.iDmQuocTichRepository.TableNoTracking.ToDictionary(
                    x => x.Id,
                    x => x.TenQuocGia
                );

            // Lấy dữ liệu từ database
            var nhanSusRaw = await _unitOfWork
                .iNsNhanSuRepository.TableNoTracking.Where(x =>
                    x.DaVeHuu != true && x.IsThoiViec != true && x.DaChamDutHopDong != true
                )
                .Select(x => new
                {
                    x.Id,
                    x.MaNhanSu,
                    HoTen = x.HoDem + " " + x.Ten,
                    x.GioiTinh,
                    x.NgaySinh,
                    x.NoiSinh,
                    x.NoiOHienTai,
                    x.QuocTich,
                    x.TonGiao,
                    x.NguyenQuan,
                    x.SoDienThoai,
                    x.SoCccd,
                    x.Email,
                    x.HienTaiPhongBan,
                    x.HienTaiChucVu,
                    x.DanToc,
                    x.TenHocVi,
                    x.TenHocHam,
                    x.TenChuyenNganhHocHam,
                    x.TenChuyenNganhHocVi,
                    x.TrinhDoHocVan,
                    x.TrinhDoNgoaiNgu,
                })
                .ToListAsync(ct);

            var nhanSus = nhanSusRaw
                .Select(x => new SearchSemanticResponseDto
                {
                    IdNhanSu = x.Id,
                    MaNhanSu = x.MaNhanSu,
                    HoTen = x.HoTen,
                    GioiTinh = x.GioiTinh,
                    GioiTinhText =
                        x.GioiTinh.HasValue && gtDict.TryGetValue(x.GioiTinh.Value, out var gt)
                            ? gt
                            : null,
                    NgaySinh = x.NgaySinh,
                    NgaySinhText = x.NgaySinh?.ToString("dd-MM-yyyy"),
                    NoiSinh = x.NoiSinh,
                    QuocTich = x.QuocTich,
                    TenQuocTich =
                        x.QuocTich.HasValue && qtDict.TryGetValue(x.QuocTich.Value, out var qt)
                            ? qt
                            : null,
                    DanToc = x.DanToc,
                    TenDanToc =
                        x.DanToc.HasValue && dtDict.TryGetValue(x.DanToc.Value, out var dt)
                            ? dt
                            : null,
                    NguyenQuan = x.NguyenQuan,
                    NoiOHienTai = x.NoiOHienTai,
                    SoCccd = x.SoCccd,
                    SoDienThoai = x.SoDienThoai,
                    Email = x.Email,
                    TenPhongBan =
                        x.HienTaiPhongBan.HasValue
                        && pbDict.TryGetValue(x.HienTaiPhongBan.Value, out var pb)
                            ? pb
                            : null,
                    TenChucVu =
                        x.HienTaiChucVu.HasValue
                        && cvDict.TryGetValue(x.HienTaiChucVu.Value, out var cv)
                            ? cv
                            : null,

                    TenHocVi = x.TenHocVi,
                    TenHocHam = x.TenHocHam,
                    TenChuyenNganhHocHam = x.TenChuyenNganhHocHam,
                    TenChuyenNganhHocVi = x.TenChuyenNganhHocVi,
                    TrinhDoHocVan = x.TrinhDoHocVan,
                    TrinhDoNgoaiNgu = x.TrinhDoNgoaiNgu,
                    TonGiao = x.TonGiao,
                })
                .ToList();

            const int batchSize = 100;
            var batch = new List<(string id, float[] vector, object? payload)>();

            foreach (var ns in nhanSus)
            {
                var searchableText = BuildSearchableText(ns);

                var vector = await _jina.EmbedPassageAsync(searchableText, ct);

                var payload = new SearchSemanticResponseDto
                {
                    IdNhanSu = ns.IdNhanSu,
                    MaNhanSu = ns.MaNhanSu,
                    HoTen = ns.HoTen,
                    NgaySinh = ns.NgaySinh,
                    NgaySinhText = ns.NgaySinhText,
                    NoiSinh = ns.NoiSinh,
                    GioiTinh = ns.GioiTinh,
                    GioiTinhText = ns.GioiTinhText,
                    QuocTich = ns.QuocTich,
                    TenQuocTich = ns.TenQuocTich,
                    DanToc = ns.DanToc,
                    TenDanToc = ns.TenDanToc,
                    TonGiao = ns.TonGiao,
                    NguyenQuan = ns.NguyenQuan,
                    NoiOHienTai = ns.NoiOHienTai,
                    SoCccd = ns.SoCccd,
                    SoDienThoai = ns.SoDienThoai,
                    Email = ns.Email,
                    TenChucVu = ns.TenChucVu,
                    TenPhongBan = ns.TenPhongBan,
                    TenToBoMon = ns.TenToBoMon,
                    TrinhDoHocVan = ns.TrinhDoHocVan,
                    TrinhDoNgoaiNgu = ns.TrinhDoNgoaiNgu,
                    TenChuyenNganhHocHam = ns.TenChuyenNganhHocHam,
                    TenChuyenNganhHocVi = ns.TenChuyenNganhHocVi,
                    TenHocHam = ns.TenHocHam,
                    TenHocVi = ns.TenHocVi,
                };

                using var md5 = System.Security.Cryptography.MD5.Create();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(ns.IdNhanSu.ToString()));
                var stableId = new Guid(hash);

                batch.Add((stableId.ToString(), vector, payload));

                if (batch.Count >= batchSize)
                {
                    await _qdrant.UpsertPointsBatchAsync(batch, ct);
                    batch.Clear();
                }
            }

            // Đẩy phần dư còn lại
            if (batch.Count > 0)
            {
                await _qdrant.UpsertPointsBatchAsync(batch, ct);
            }
        }

        public async Task<List<SearchSemanticResponseDto>> SearchSemanticAsync(
            SearchSemanticRequestDto dto,
            CancellationToken ct = default
        )
        {
            _logger.LogInformation(
                $"{nameof(SearchSemanticAsync)}: dto = {JsonSerializer.Serialize(dto)}"
            );

            if (string.IsNullOrEmpty(dto.Keyword))
            {
                return null;
            }

            // Sinh embedding từ câu truy vấn
            var queryText = $"Tìm nhân sự: {dto.Keyword}";
            var queryVector = await _jina.EmbedQueryAsync(queryText, ct);

            // Gọi API search của Qdrant
            var searchResults = await _qdrant.SearchAsync(
                queryVector,
                dto.PageSize,
                dto.SkipCount(),
                null,
                ct
            );
            _logger.LogInformation($"Qdrant found {searchResults.Count()} vectors.");

            #region log debugging - Log payload tìm được

            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // format đẹp
            };

            foreach (var ns in searchResults)
            {
                var payloadJson = JsonSerializer.Serialize(ns.Payload, options);
                _logger.LogInformation(
                    $"SearchSemanticAsync - Score: {ns.Score}"
                );
            }

            #endregion

            // Map kết quả trả về thành DTO
            var mappedResult = searchResults
                .OrderByDescending(r => r.Score)
                .Where(r => r.Payload != null && r.Score > 0.3f)
                .Select(r => MapPayloadToNhanSu(r.Payload!))
                .ToList();

            // Sắp xếp theo Id tăng dần
            //var result = mappedResult.OrderBy(x => x.IdNhanSu).ToList();
            var result = mappedResult;

            return result;
        }

        /// <summary>
        /// Ghép nội dung text từ các field nhân sự để sinh embedding.
        /// Đây là phần cực kỳ quan trọng để search ngữ nghĩa có chất lượng tốt.
        /// </summary>
        private static string BuildSearchableText(SearchSemanticResponseDto ns)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(ns.HoTen))
                sb.Append($"Nhân sự {ns.HoTen}. ");

            if (!string.IsNullOrWhiteSpace(ns.MaNhanSu))
                sb.Append($"Mã nhân sự {ns.MaNhanSu}. ");

            if (!string.IsNullOrWhiteSpace(ns.GioiTinhText))
                sb.Append($"Giới tính {ns.GioiTinhText}. ");

            if (ns.NgaySinh.HasValue)
                sb.Append($"Sinh ngày {ns.NgaySinh:dd/MM/yyyy}. ");

            if (!string.IsNullOrWhiteSpace(ns.NoiSinh))
                sb.Append($"Nơi sinh {ns.NoiSinh}. ");

            if (!string.IsNullOrWhiteSpace(ns.NguyenQuan))
                sb.Append($"Nguyên quán {ns.NguyenQuan}. ");

            if (!string.IsNullOrWhiteSpace(ns.TenDanToc))
                sb.Append($"Dân tộc {ns.TenDanToc}. ");

            if (!string.IsNullOrWhiteSpace(ns.TenQuocTich))
                sb.Append($"Quốc tịch {ns.TenQuocTich}. ");

            if (!string.IsNullOrWhiteSpace(ns.SoDienThoai))
                sb.Append($"Số điện thoại {ns.SoDienThoai}. ");

            if (!string.IsNullOrWhiteSpace(ns.Email))
                sb.Append($"Email {ns.Email}. ");

            if (!string.IsNullOrWhiteSpace(ns.TenPhongBan))
                sb.Append($"Hiện đang công tác tại phòng ban {ns.TenPhongBan}. ");

            if (!string.IsNullOrWhiteSpace(ns.TenChucVu))
                sb.Append($"Chức vụ {ns.TenChucVu}. ");

            if (!string.IsNullOrWhiteSpace(ns.TenToBoMon))
                sb.Append($"Thuộc tổ bộ môn {ns.TenToBoMon}. ");

            if (!string.IsNullOrWhiteSpace(ns.TrinhDoHocVan))
                sb.Append($"Trình độ học vấn {ns.TrinhDoHocVan}. ");

            if (!string.IsNullOrWhiteSpace(ns.TenHocVi))
                sb.Append($"Học vị {ns.TenHocVi}. ");

            if (!string.IsNullOrWhiteSpace(ns.TenHocHam))
                sb.Append($"Học hàm {ns.TenHocHam}. ");

            if (!string.IsNullOrWhiteSpace(ns.TenChuyenNganhHocVi))
                sb.Append($"Chuyên ngành {ns.TenChuyenNganhHocVi}. ");

            if (!string.IsNullOrWhiteSpace(ns.TenChuyenNganhHocHam))
                sb.Append($"Chuyên ngành học hàm {ns.TenChuyenNganhHocHam}. ");

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Helper để map payload
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        private static SearchSemanticResponseDto MapPayloadToNhanSu(
            Dictionary<string, object> payload
        )
        {
            // Tạo dictionary không phân biệt chữ hoa, chữ thường
            var ciPayload = new Dictionary<string, object>(
                payload,
                StringComparer.OrdinalIgnoreCase
            );

            return new SearchSemanticResponseDto
            {
                IdNhanSu = ConvertToInt(ciPayload, nameof(SearchSemanticResponseDto.IdNhanSu)),
                MaNhanSu = ConvertToString(ciPayload, nameof(SearchSemanticResponseDto.MaNhanSu)),
                HoTen = ConvertToString(ciPayload, nameof(SearchSemanticResponseDto.HoTen)),
                GioiTinh = ConvertToInt(ciPayload, nameof(SearchSemanticResponseDto.GioiTinh)),
                GioiTinhText = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.GioiTinhText)
                ),
                NoiSinh = ConvertToString(ciPayload, nameof(SearchSemanticResponseDto.NoiSinh)),
                QuocTich = ConvertToInt(ciPayload, nameof(SearchSemanticResponseDto.QuocTich)),
                TenQuocTich = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.TenQuocTich)
                ),
                DanToc = ConvertToInt(ciPayload, nameof(SearchSemanticResponseDto.DanToc)),
                TenDanToc = ConvertToString(ciPayload, nameof(SearchSemanticResponseDto.TenDanToc)),
                NguyenQuan = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.NguyenQuan)
                ),
                NoiOHienTai = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.NoiOHienTai)
                ),
                SoCccd = ConvertToString(ciPayload, nameof(SearchSemanticResponseDto.SoCccd)),
                SoDienThoai = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.SoDienThoai)
                ),
                Email = ConvertToString(ciPayload, nameof(SearchSemanticResponseDto.Email)),
                TrinhDoHocVan = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.TrinhDoHocVan)
                ),
                TrinhDoNgoaiNgu = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.TrinhDoNgoaiNgu)
                ),
                NgaySinhText = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.NgaySinhText)
                ),
                TenPhongBan = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.TenPhongBan)
                ),
                TenChucVu = ConvertToString(ciPayload, nameof(SearchSemanticResponseDto.TenChucVu)),
                TenHocVi = ConvertToString(ciPayload, nameof(SearchSemanticResponseDto.TenHocVi)),
                TenHocHam = ConvertToString(ciPayload, nameof(SearchSemanticResponseDto.TenHocHam)),
                TenChuyenNganhHocVi = ConvertToString(
                    ciPayload,
                    nameof(SearchSemanticResponseDto.TenChuyenNganhHocVi)
                ),
            };
        }

        private static string? ConvertToString(Dictionary<string, object> dict, string key)
        {
            if (dict.TryGetValue(key, out var value) && value != null)
                return value.ToString();
            return null;
        }

        private static int ConvertToInt(Dictionary<string, object> dict, string key)
        {
            if (dict.TryGetValue(key, out var value) && value != null)
            {
                if (int.TryParse(value.ToString(), out var intValue))
                    return intValue;
            }
            return 0;
        }
    }
}
