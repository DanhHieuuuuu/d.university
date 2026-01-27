using AutoMapper;
using d.Shared.Permission.Auth;
using D.Core.Domain.Dtos.Survey.Report;
using D.Core.Domain.Dtos.Survey.AI;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Domain.Entities.Survey;
using D.Core.Domain.Entities.Survey.Constants;
using D.Core.Infrastructure.Services.Survey.Report.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Survey.Report.Implement
{
    public class ReportSurveyService : ServiceBase, IReportSurveyService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IHttpContextAccessor _httpContextAccessor;

        public ReportSurveyService(
            ILogger<ReportSurveyService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContext;
        }

        public async Task<bool> GenerateReportAsync(int surveyId)
        {
            var submissions = await _unitOfWork.iKsSurveySubmissionRepository.TableNoTracking
                .Where(s => s.IdKhaoSat == surveyId && s.TrangThai == SubmissionStatus.Submitted)
                .ToListAsync();

            int totalParticipants = submissions.Count;
            double avgScore = totalParticipants > 0 ? submissions.Average(s => s.DiemTong ?? 0) : 0;

            var survey = _unitOfWork.iKsSurveyRepository.FindById(surveyId);
            if (survey == null) throw new Exception("Khảo sát không tồn tại");

            var questions = await _unitOfWork.iKsSurveyRequestRepository
                .GetQuestionsByRequestIdAsync(survey.IdYeuCau);

            var stats = new ReportStatisticsDto();

            // Get all responses exclude deleted
            var submissionIds = submissions.Select(s => s.Id).ToList();
            var allResponses = await _unitOfWork.iKsSurveySubmissionAnswerRepository.TableNoTracking
                .Where(r => submissionIds.Contains(r.IdPhienLamBai) && !r.Deleted)
                .ToListAsync();

            foreach (var q in questions)
            {
                var qStat = new QuestionStatDto
                {
                    QuestionId = q.Id,
                    Content = q.NoiDung,
                    Type = q.LoaiCauHoi,
                    BatBuoc = q.BatBuoc
                };

                var responsesForQ = allResponses.Where(r => r.IdCauHoi == q.Id).ToList();

                if (q.LoaiCauHoi == 1 || q.LoaiCauHoi == 2)
                {
                    var grouped = responsesForQ.GroupBy(r => r.IdDapAnChon).ToList();

                    foreach (var ans in q.Answers)
                    {
                        int count = grouped.FirstOrDefault(g => g.Key == ans.Id)?.Count() ?? 0;
                        qStat.Answers.Add(new AnswerStatDto
                        {
                            Label = ans.NoiDung,
                            Count = count,
                            Percent = totalParticipants > 0
                                      ? Math.Round((double)count * 100 / totalParticipants, 2)
                                      : 0,
                            Value = ans.Value,
                            IsCorrect = ans.IsCorrect
                        });
                    }
                }
                else
                {                    
                    qStat.RecentTextResponses = responsesForQ
                        .Where(r => !string.IsNullOrEmpty(r.CauTraLoiText))
                        .OrderByDescending(r => r.Id)
                        .Take(5)
                        .Select(r => r.CauTraLoiText)
                        .ToList();
                }

                stats.Questions.Add(qStat);
            }

            var reportEntity = await _unitOfWork.iKsSurveyReportRepository.Table
                .FirstOrDefaultAsync(r => r.IdKhaoSat == surveyId);

            string jsonStats = JsonSerializer.Serialize(stats);

            if (reportEntity == null)
            {
                reportEntity = new KsSurveyReport
                {
                    IdKhaoSat = surveyId,
                    TongSoLuotThamGia = totalParticipants,
                    DiemTrungBinh = avgScore,
                    ThoiGianTao = DateTime.Now,
                    DuLieuThongKe = jsonStats
                };

                await LogActionAsync(
                    targetId: survey.MaKhaoSat,
                    actionType: "GenerateReport",
                    description: "Khởi tạo báo cáo thống kê lần đầu",
                    oldValue: "Chưa có",
                    newValue: $"Tổng tham gia: {totalParticipants}");
                
                await _unitOfWork.iKsSurveyReportRepository.AddAsync(reportEntity);
            }
            else
            {
                string oldInfo = $"Tổng tham gia: {reportEntity.TongSoLuotThamGia}, Điểm TB: {reportEntity.DiemTrungBinh}";
                string newInfo = $"Tổng tham gia: {totalParticipants}, Điểm TB: {avgScore}";

                reportEntity.TongSoLuotThamGia = totalParticipants;
                reportEntity.DiemTrungBinh = avgScore;
                reportEntity.ThoiGianTao = DateTime.Now;
                reportEntity.DuLieuThongKe = jsonStats;
                _unitOfWork.iKsSurveyReportRepository.Update(reportEntity);

                await LogActionAsync(
                    targetId: survey.MaKhaoSat,
                    actionType: "UpdateReport",
                    description: "Cập nhật lại số liệu báo cáo",
                    oldValue: oldInfo,
                    newValue: newInfo);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<PageResultDto<SurveyReportResponseDto>> GetReportsPagingAsync(FilterReportSurveyDto dto)
        {
            var query = _unitOfWork.iKsSurveyReportRepository.TableNoTracking
                .Include(r => r.Survey)
                .AsQueryable();

            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                query = query.Where(r => r.Survey.TenKhaoSat.Contains(dto.Keyword));
            }

            query = query.OrderByDescending(r => r.ThoiGianTao);

            var total = await query.CountAsync();
            var items = await query
                .Skip((dto.PageIndex - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .Select(r => new SurveyReportResponseDto
                {
                    ReportId = r.Id,
                    SurveyId = r.IdKhaoSat,
                    TenKhaoSat = r.Survey.TenKhaoSat,
                    TotalParticipants = r.TongSoLuotThamGia,
                    AverageScore = r.DiemTrungBinh ?? 0,
                    GeneratedAt = r.ThoiGianTao
                })
                .ToListAsync();

            return new PageResultDto<SurveyReportResponseDto>
            {
                Items = items,
                TotalItem = total,
            };
        }

        public async Task<SurveyReportDetailDto> GetReportDetailAsync(int reportId)
        {
            var report = await _unitOfWork.iKsSurveyReportRepository.TableNoTracking
                .Include(r => r.Survey)
                .FirstOrDefaultAsync(r => r.Id == reportId);

            if (report == null) throw new Exception("Báo cáo không tồn tại.");

            var respondentsQuery = from s in _unitOfWork.iKsSurveySubmissionRepository.TableNoTracking
                                   join nv in _unitOfWork.iNsNhanSuRepository.TableNoTracking
                                   on s.IdNguoiDung equals nv.Id into uGroup
                                   from nv in uGroup.DefaultIfEmpty()

                                   join sv in _unitOfWork.iSvSinhVienRepository.TableNoTracking
                                   on s.IdNguoiDung equals sv.Id into svGroup
                                   from sv in svGroup.DefaultIfEmpty()

                                   where s.IdKhaoSat == report.IdKhaoSat
                                      && s.TrangThai == SubmissionStatus.Submitted
                                   orderby s.DiemTong descending
                                   select new SurveyRespondentDto
                                   {
                                       SubmissionId = s.Id,
                                       FullName = nv != null
                                          ? $"{nv.HoDem} {nv.Ten}"
                                          : (sv != null ? $"{sv.HoDem} {sv.Ten}" : "Unknown"),
                                       UserCode = nv != null
                                          ? nv.MaNhanSu
                                          : (sv != null ? sv.Mssv : ""),
                                       SubmitTime = s.ThoiGianNop,
                                       TotalScore = s.DiemTong ?? 0
                                   };

            var respondents = await respondentsQuery.ToListAsync();

            var stats = string.IsNullOrEmpty(report.DuLieuThongKe)
                ? new ReportStatisticsDto()
                : JsonSerializer.Deserialize<ReportStatisticsDto>(report.DuLieuThongKe);

            return new SurveyReportDetailDto
            {
                ReportId = report.Id,
                TenKhaoSat = report.Survey.TenKhaoSat,
                TotalParticipants = report.TongSoLuotThamGia,
                AverageScore = report.DiemTrungBinh ?? 0,
                LastGenerated = report.ThoiGianTao,
                Statistics = stats,
                Respondents = respondents
            };
        }

        public async Task<SurveyAIDataDto> GetAIAnalysisDataAsync(int reportId)
        {
            var report = await _unitOfWork.iKsSurveyReportRepository.TableNoTracking
                .Include(r => r.Survey)
                    .ThenInclude(s => s.SurveyRequest)
                        .ThenInclude(sr => sr.Criterias)
                .Include(r => r.Survey)
                    .ThenInclude(s => s.SurveyRequest)
                        .ThenInclude(sr => sr.Targets)
                .FirstOrDefaultAsync(r => r.Id == reportId);

            if (report == null) throw new Exception("Báo cáo không tồn tại.");
            if (report.Survey?.SurveyRequest == null) throw new Exception("Không tìm thấy thông tin yêu cầu khảo sát.");

            var stats = string.IsNullOrEmpty(report.DuLieuThongKe)
                ? new ReportStatisticsDto()
                : JsonSerializer.Deserialize<ReportStatisticsDto>(report.DuLieuThongKe);

            var criteria = report.Survey.SurveyRequest.Criterias
                .Select(c => new RequestSurveyCriteriaDto
                {
                    IdTieuChi = c.Id,
                    TenTieuChi = c.TenTieuChi,
                    Weight = c.Weight,
                    MoTa = c.MoTa
                })
                .ToList();

            var questions = await _unitOfWork.iKsSurveyRequestRepository
                .GetQuestionsByRequestIdAsync(report.Survey.SurveyRequest.Id);

            var targets = report.Survey.SurveyRequest.Targets
                .Select(t => new RequestSurveyTargetDto
                {
                    LoaiDoiTuong = t.LoaiDoiTuong,
                    IdPhongBan = t.IdPhongBan,
                    IdKhoa = t.IdKhoa,
                    IdKhoaHoc = t.IdKhoaHoc
                })
                .ToList();

            return new SurveyAIDataDto
            {
                ReportId = report.Id,
                TenKhaoSat = report.Survey.TenKhaoSat,
                TotalParticipants = report.TongSoLuotThamGia,
                AverageScore = report.DiemTrungBinh ?? 0,
                Statistics = stats,
                Criteria = criteria,
                Targets = targets,
            };
        }

        public async Task<bool> SaveAIResponseAsync(int reportId, List<AIReportDto> responses)
        {
            var report = await _unitOfWork.iKsSurveyReportRepository.TableNoTracking
                .FirstOrDefaultAsync(r => r.Id == reportId);

            if (report == null) throw new Exception("Báo cáo không tồn tại.");

            var existingResponses = await _unitOfWork.iKsAIResponseRepository.Table
                .Where(r => r.IdBaoCao == reportId)
                .ToListAsync();

            if (existingResponses.Any())
            {
                _unitOfWork.iKsAIResponseRepository.DeleteRange(existingResponses);
            }

            var aiResponses = responses.Select(r => new KsAIResponse
            {
                IdBaoCao = reportId,
                IdTieuChi = r.IdTieuChi,
                DiemCamXuc = r.DiemCamXuc,
                NhanCamXuc = r.NhanCamXuc,
                TomTatNoiDung = r.TomTatNoiDung,
                XuHuong = r.XuHuong,
                GoiYCaiThien = r.GoiYCaiThien
            }).ToList();

            await _unitOfWork.iKsAIResponseRepository.AddRangeAsync(aiResponses);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<List<AIReportDetailDto>> GetAIResponsesByReportIdAsync(int reportId)
        {
            var aiResponses = await _unitOfWork.iKsAIResponseRepository.TableNoTracking
                .Include(r => r.Criteria)
                .Where(r => r.IdBaoCao == reportId)
                .Select(r => new AIReportDetailDto
                {
                    Id = r.Id,
                    IdBaoCao = r.IdBaoCao,
                    IdTieuChi = r.IdTieuChi,
                    DiemCamXuc = r.DiemCamXuc,
                    NhanCamXuc = r.NhanCamXuc,
                    TomTatNoiDung = r.TomTatNoiDung,
                    XuHuong = r.XuHuong,
                    GoiYCaiThien = r.GoiYCaiThien,
                    TenTieuChi = r.Criteria.TenTieuChi,
                    Weight = r.Criteria.Weight
                })
                .ToListAsync();

            return aiResponses;
        }

        private async Task LogActionAsync(string targetId, string actionType, string description, string? oldValue = null, string? newValue = null)
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContextAccessor);

            var user = await _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .FirstOrDefaultAsync(u => u.Id == userId);

            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown";

            var log = new KsSurveyLog
            {
                IdNguoiThaoTac = userId,
                TenNguoiThaoTac = userName,
                LoaiHanhDong = actionType,
                MoTa = description,
                TenBang = nameof(KsSurveyReport),
                IdDoiTuong = targetId,
                DuLieuCu = oldValue,
                DuLieuMoi = newValue,
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.iKsSurveyLogRepository.AddAsync(log);
        }

    }
}
