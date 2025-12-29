using AutoMapper;
using Azure.Core;
using d.Shared.Permission.Auth;
using D.Core.Domain.Dtos.Survey.Submit;
using D.Core.Domain.Dtos.Survey.Surveys;
using D.Core.Domain.Entities.Survey;
using D.Core.Domain.Entities.Survey.Constants;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Minio.DataModel.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace D.Core.Infrastructure.Services.Survey.Surveys.Implement
{
    public class SurveyService : ServiceBase, ISurveyService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IHttpContextAccessor _httpContextAccessor;

        public SurveyService(
            ILogger<SurveyService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContext;
        }

        public PageResultDto<SurveyResponseDto> Paging(FilterSurveyDto dto)
        {
            _logger.LogInformation($"{nameof(Paging)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var query = from s in _unitOfWork.iKsSurveyRepository.TableNoTracking

                        where (string.IsNullOrEmpty(dto.Keyword) || s.TenKhaoSat.Contains(dto.Keyword) || s.MaKhaoSat.Contains(dto.Keyword))
                           && (!dto.Status.HasValue || s.Status == dto.Status)

                        orderby s.Id
                        select new SurveyResponseDto
                        {
                            Id = s.Id,
                            MaKhaoSat = s.MaKhaoSat,
                            TenKhaoSat = s.TenKhaoSat,
                            ThoiGianBatDau = s.ThoiGianBatDau,
                            ThoiGianKetThuc = s.ThoiGianKetThuc,
                            Status = s.Status,
                        };

            var totalCount = query.Count();
            var items = query.Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

            return new PageResultDto<SurveyResponseDto>
            {
                Items = items,
                TotalItem = totalCount,

            };
        }

        public async Task<SurveyDetailDto> GetByIdSurvey(int id)
        {
            _logger.LogInformation($"{nameof(GetByIdSurvey)} called with id: {id}");

            var entity = await _unitOfWork.iKsSurveyRepository.TableNoTracking
                .Include(s => s.SurveyRequest)
                    .ThenInclude(r => r.Questions)
                        .ThenInclude(q => q.Answers)
                .Include(s => s.SurveyRequest)
                    .ThenInclude(r => r.Targets)
                .Include(s => s.SurveyRequest) 
                    .ThenInclude(r => r.Criterias)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) throw new Exception("Không tìm thấy khảo sát.");

            return _mapper.Map<SurveyDetailDto>(entity);
        }

        public async Task CreateSurveyFromRequestAsync(int requestId)
        {
            _logger.LogInformation($"{nameof(CreateSurveyFromRequestAsync)} method called, dto: {JsonSerializer.Serialize(requestId)}.");

            var request = _unitOfWork.iKsSurveyRequestRepository.FindById(requestId);
            if (request == null) throw new Exception("Không tìm thấy yêu cầu gốc.");

            var exists = await _unitOfWork.iKsSurveyRepository.TableNoTracking
                .AnyAsync(x => x.IdYeuCau == requestId && x.Deleted == false);
            if (exists) return;

            var survey = new KsSurvey
            {
                IdYeuCau = request.Id,
                MaKhaoSat = request.MaYeuCau,
                TenKhaoSat = request.TenKhaoSatYeuCau,
                MoTa = request.MoTa,
                ThoiGianBatDau = request.ThoiGianBatDau,
                ThoiGianKetThuc = request.ThoiGianKetThuc,
                IdPhongBan = request.IdPhongBan,
                Status = SurveyStatus.Close,

                CreatedDate = DateTime.Now,
                CreatedBy = CommonUntil.GetCurrentUserId(_httpContextAccessor).ToString()
            };

            await _unitOfWork.iKsSurveyRepository.AddAsync(survey);
            await _unitOfWork.SaveChangesAsync();

            await LogActionAsync(survey.MaKhaoSat, "Create", "Hệ thống tạo khảo sát từ yêu cầu", null, "Open");
        }

        public async Task OpenSurveyAsync(int id)
        {
            _logger.LogInformation($"{nameof(OpenSurveyAsync)} method called, dto: {JsonSerializer.Serialize(id)}.");

            var entity = _unitOfWork.iKsSurveyRepository.FindById(id);
            if (entity == null) throw new Exception("Không tìm thấy khảo sát.");
            if (entity.Status == SurveyStatus.Open) return;
            
            var oldStatus = entity.Status;
            entity.Status = SurveyStatus.Open;

            _unitOfWork.iKsSurveyRepository.Update(entity);

            await LogActionAsync(
                entity.MaKhaoSat,
                "Open",
                "Mở khảo sát",
                oldStatus.ToString(),
                SurveyStatus.Open.ToString()
            );

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CloseSurveyAsync(int id)
        {
            _logger.LogInformation($"{nameof(CloseSurveyAsync)} method called, dto: {JsonSerializer.Serialize(id)}.");

            var entity = _unitOfWork.iKsSurveyRepository.FindById(id);
            if (entity == null) throw new Exception("Không tìm thấy khảo sát.");
            if (entity.Status == SurveyStatus.Close) return;
            
            var oldStatus = entity.Status;
            entity.Status = SurveyStatus.Close;
            
            _unitOfWork.iKsSurveyRepository.Update(entity);

            await LogActionAsync(
                entity.MaKhaoSat,
                "Close",
                "Đóng khảo sát (Ngưng kích hoạt)",
                oldStatus.ToString(),
                SurveyStatus.Close.ToString()
            );

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task LogActionAsync(string targetId, string actionType, string description, string? oldValue = null, string? newValue = null)
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContextAccessor);

            var user = await _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .FirstOrDefaultAsync(u => u.Id == userId);

            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown";

            var fullDescription = $"{description}. Thực hiện bởi {userName} vào {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            var log = new KsSurveyLog
            {
                IdNguoiThaoTac = userId,
                TenNguoiThaoTac = userName,
                LoaiHanhDong = actionType,
                MoTa = fullDescription,
                TenBang = nameof(KsSurvey),
                IdDoiTuong = targetId,
                DuLieuCu = oldValue,
                DuLieuMoi = newValue,
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.iKsSurveyLogRepository.AddAsync(log);
        }

        //public async Task<PageResultDto<SurveyResponseDto>> GetMySurveysAsync(FilterMySurveyDto dto)
        //{
        //    _logger.LogInformation($"{nameof(GetMySurveysAsync)} method called, dto: {JsonSerializer.Serialize(dto)}.");

        //    var userId = CommonUntil.GetCurrentUserId(_httpContextAccessor);

        //    var user = await _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefaultAsync(u => u.Id == userId);
        //    if (user == null) throw new Exception("Không tìm thấy thông tin người dùng.");

        //    var query = _unitOfWork.iKsSurveyRepository.TableNoTracking
        //        .Include(s => s.SurveyRequest)
        //            .ThenInclude(r => r.Targets)
        //        .Where(s => s.Status == SurveyStatus.Open);

        //    if (!string.IsNullOrEmpty(dto.Keyword))
        //    {
        //        query = query.Where(s => s.TenKhaoSat.Contains(dto.Keyword) || s.MaKhaoSat.Contains(dto.Keyword));
        //    }

        //    var allOpenSurveys = await query.ToListAsync();

        //    var validSurveys = allOpenSurveys.Where(s =>
        //    {
        //        var targets = s.SurveyRequest.Targets;
        //        if (targets == null || !targets.Any()) return true; // Không target = All

        //        foreach (var t in targets)
        //        {
        //            if (t.LoaiDoiTuong == SurveyTarget.All) return true;
        //            if (t.LoaiDoiTuong == SurveyTarget.Lecturer)
        //            {
        //                bool matchPhongBan = (t.IdPhongBan == null) || (t.IdPhongBan == user.HienTaiPhongBan);
        //                if (matchPhongBan) return true;
        //            }
        //            // TODO: Check Student
        //        }
        //        return false;
        //    }).ToList();

        //    var total = validSurveys.Count();
        //    var items = validSurveys.Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();

        //    return new PageResultDto<SurveyResponseDto>
        //    {
        //        Items = _mapper.Map<List<SurveyResponseDto>>(items),
        //        TotalItem = total,
        //    };
        //}

        public async Task<PageResultDto<SurveyResponseDto>> GetMySurveysAsync(FilterMySurveyDto dto)
        {
            _logger.LogInformation($"{nameof(GetMySurveysAsync)} method called.");

            var userId = CommonUntil.GetCurrentUserId(_httpContextAccessor);
            var userType = CommonUntil.GetCurrentUserType(_httpContextAccessor);

            int? uPhongBan = null;
            int? uKhoa = null;
            int? uKhoaHoc = null;

            if (userType == UserTypeConstant.STUDENT)
            {
                var sv = await _unitOfWork.iSvSinhVienRepository.TableNoTracking
                    .FirstOrDefaultAsync(x => x.Id == userId);
                if (sv == null) throw new Exception("Không tìm thấy thông tin sinh viên.");

                uKhoa = sv.Khoa;
                uKhoaHoc = sv.KhoaHoc;
            }
            else
            {
                var ns = await _unitOfWork.iNsNhanSuRepository.TableNoTracking
                    .FirstOrDefaultAsync(x => x.Id == userId);
                if (ns == null) throw new Exception("Không tìm thấy thông tin nhân sự.");

                uPhongBan = ns.HienTaiPhongBan;
            }

            var query = _unitOfWork.iKsSurveyRepository.TableNoTracking
                .Include(s => s.SurveyRequest)
                .Where(s => s.Status == SurveyStatus.Open);

            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                query = query.Where(s => s.TenKhaoSat.Contains(dto.Keyword) || s.MaKhaoSat.Contains(dto.Keyword));
            }

            query = query.Where(s =>
                !s.SurveyRequest.Targets.Any() ||
                s.SurveyRequest.Targets.Any(t =>
                    t.LoaiDoiTuong == SurveyTarget.All ||
                    (
                        userType != UserTypeConstant.STUDENT &&
                        t.LoaiDoiTuong == SurveyTarget.Lecturer &&
                        (t.IdPhongBan == null || t.IdPhongBan == uPhongBan)
                    ) ||
                    (
                        userType == UserTypeConstant.STUDENT &&
                        t.LoaiDoiTuong == SurveyTarget.Student &&
                        (t.IdKhoa == null || t.IdKhoa == uKhoa) &&
                        (t.IdKhoaHoc == null || t.IdKhoaHoc == uKhoaHoc)
                    )
                )
            );

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(s => s.Id)
                .Skip((dto.PageIndex - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            return new PageResultDto<SurveyResponseDto>
            {
                Items = _mapper.Map<List<SurveyResponseDto>>(items),
                TotalItem = total,
            };
        }

        public async Task<StartSurveyResponseDto> StartSurveyAsync(int surveyId)
        {
            _logger.LogInformation($"{nameof(StartSurveyAsync)} method called. Dto: {surveyId}");

            var userId = CommonUntil.GetCurrentUserId(_httpContextAccessor);
            var userType = CommonUntil.GetCurrentUserType(_httpContextAccessor);

            var submission = await _unitOfWork.iKsSurveySubmissionRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.IdKhaoSat == surveyId && x.IdNguoiDung == userId);

            if (submission != null && submission.TrangThai == SubmissionStatus.Submitted)
            {
                throw new Exception("Bạn đã hoàn thành khảo sát này.");
            }

            int? uPhongBan = null;
            int? uKhoa = null;
            int? uKhoaHoc = null;
            int? uNganh = null;

            if (userType == UserTypeConstant.STUDENT)
            {
                var sv = await _unitOfWork.iSvSinhVienRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == userId);
                if (sv == null) throw new Exception("Không tìm thấy thông tin sinh viên.");
                uKhoa = sv.Khoa;
                uKhoaHoc = sv.KhoaHoc;
                uNganh = sv.Nganh;
            }
            else
            {
                var ns = await _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == userId);
                if (ns == null) throw new Exception("Không tìm thấy thông tin nhân sự.");
                uPhongBan = ns.HienTaiPhongBan;
            }

            var surveyEntity = await _unitOfWork.iKsSurveyRepository.TableNoTracking
                .Include(x => x.SurveyRequest).ThenInclude(r => r.Targets) 
                .FirstOrDefaultAsync(x => x.Id == surveyId);
            if (surveyEntity == null) throw new Exception("Khảo sát không tồn tại.");

            if (surveyEntity.ThoiGianKetThuc < DateTime.Now)            
                throw new Exception("Đợt khảo sát đã kết thúc, bạn không thể thực hiện hành động này.");

            var targets = surveyEntity.SurveyRequest.Targets;
            if (targets != null && targets.Any())
            {
                bool isAllowed = targets.Any(t =>
                    t.LoaiDoiTuong == SurveyTarget.All
                    ||
                    (
                        userType != UserTypeConstant.STUDENT &&
                        t.LoaiDoiTuong == SurveyTarget.Lecturer &&
                        (t.IdPhongBan == null || t.IdPhongBan == uPhongBan)
                    )
                    ||
                    (
                        userType == UserTypeConstant.STUDENT &&
                        t.LoaiDoiTuong == SurveyTarget.Student &&
                        (t.IdKhoa == null || t.IdKhoa == uKhoa) &&
                        (t.IdKhoaHoc == null || t.IdKhoaHoc == uKhoaHoc)
                    )
                );

                if (!isAllowed)
                {
                    throw new Exception("Bạn không thuộc đối tượng tham gia khảo sát này.");
                }
            }

            var questionsEntities = await _unitOfWork.iKsSurveyRequestRepository
                .GetQuestionsByRequestIdAsync(surveyEntity.IdYeuCau);

            var questionsDto = _mapper.Map<List<SurveyExamDto>>(questionsEntities);

            if (submission == null)
            {
                submission = new KsSurveySubmission
                {
                    IdKhaoSat = surveyId,
                    IdNguoiDung = userId,
                    ThoiGianBatDau = DateTime.Now,
                    TrangThai = SubmissionStatus.InProgress,
                    DiemTong = 0
                };
                await _unitOfWork.iKsSurveySubmissionRepository.AddAsync(submission);

                await LogSubmissionActivityAsync(submission.Id, "Start", "Bắt đầu làm bài");

                await _unitOfWork.SaveChangesAsync();

                return new StartSurveyResponseDto
                {
                    SubmissionId = submission.Id,
                    SurveyId = surveyId,
                    TenKhaoSat = surveyEntity.TenKhaoSat,
                    ThoiGianBatDau = submission.ThoiGianBatDau,
                    Questions = questionsDto,
                    SavedAnswers = new List<SavedAnswerDto>()
                };
            }
            else
            {
                var savedAnswers = await _unitOfWork.iKsSurveySubmissionAnswerRepository.TableNoTracking
                    .Where(x => x.IdPhienLamBai == submission.Id)
                    .ToListAsync();

                return new StartSurveyResponseDto
                {
                    SubmissionId = submission.Id,
                    SurveyId = surveyId,
                    TenKhaoSat = surveyEntity.TenKhaoSat,
                    ThoiGianBatDau = submission.ThoiGianBatDau,
                    Questions = questionsDto,
                    SavedAnswers = _mapper.Map<List<SavedAnswerDto>>(savedAnswers)
                };
            }
        }

        public async Task SaveDraftAsync(SubmitSurveyRequestDto dto)
        {
            var submission = _unitOfWork.iKsSurveySubmissionRepository.FindById(dto.SubmissionId);

            if (submission == null) return;
            if (submission.TrangThai == SubmissionStatus.Submitted) return; // Đã nộp thì không cho sửa

            await ProcessUpsertAnswers(submission.Id, dto.Answers);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<SurveyResultDto> SubmitSurveyAsync(SubmitSurveyRequestDto dto)
        {
            _logger.LogInformation($"{nameof(SubmitSurveyAsync)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var submission = _unitOfWork.iKsSurveySubmissionRepository.FindById(dto.SubmissionId);
            if (submission == null) throw new Exception("Không tìm thấy bài làm.");
            if (submission.TrangThai == SubmissionStatus.Submitted) throw new Exception("Bài này đã nộp rồi.");

            await ProcessUpsertAnswers(submission.Id, dto.Answers);

            var surveyInfo = _unitOfWork.iKsSurveyRepository.FindById(submission.IdKhaoSat);

            var correctAnswers = await _unitOfWork.iKsSurveyRequestRepository
                .GetCorrectAnswersAsync(surveyInfo.IdYeuCau);

            var userAnswers = await _unitOfWork.iKsSurveySubmissionAnswerRepository.Table
                .Where(x => x.IdPhienLamBai == submission.Id).ToListAsync();

            double totalScore = 0;
            int correctCount = 0;

            foreach (var ua in userAnswers)
            {
                var correct = correctAnswers.FirstOrDefault(c => c.QuestionId == ua.IdCauHoi);
                if (correct != null && ua.IdDapAnChon == correct.AnswerId)
                {
                    totalScore += 1;
                    correctCount++;
                }
            }

            submission.TrangThai = SubmissionStatus.Submitted;
            submission.ThoiGianNop = DateTime.Now;
            submission.DiemTong = totalScore;

            _unitOfWork.iKsSurveySubmissionRepository.Update(submission);

            await LogSubmissionActivityAsync(
                submission.Id,
                "Submit",
                $"Nộp bài. Điểm: {totalScore}. Đúng: {correctCount}/{correctAnswers.Count}"
            );

            await _unitOfWork.SaveChangesAsync();

            scope.Complete();

            return new SurveyResultDto
            {
                SubmissionId = submission.Id,
                TotalScore = totalScore,
                TotalCorrect = correctCount,
                TotalQuestions = correctAnswers.Count,
                SubmitTime = submission.ThoiGianNop.Value
            };
        }

        private async Task ProcessUpsertAnswers(int submissionId, List<SavedAnswerDto> newAnswers)
        {
            if (newAnswers == null || !newAnswers.Any()) return;

            var existingAnswers = await _unitOfWork.iKsSurveySubmissionAnswerRepository.Table
                .Where(x => x.IdPhienLamBai == submissionId).ToListAsync();

            foreach (var item in newAnswers)
            {
                var existing = existingAnswers.FirstOrDefault(x => x.IdCauHoi == item.QuestionId);
                if (existing != null)
                {
                    existing.IdDapAnChon = item.SelectedAnswerId;
                    existing.CauTraLoiText = item.TextResponse;
                    _unitOfWork.iKsSurveySubmissionAnswerRepository.Update(existing);
                }
                else
                {
                    var newAns = new KsSurveySubmissionAnswer
                    {
                        IdPhienLamBai = submissionId,
                        IdCauHoi = item.QuestionId,
                        IdDapAnChon = item.SelectedAnswerId,
                        CauTraLoiText = item.TextResponse
                    };
                    await _unitOfWork.iKsSurveySubmissionAnswerRepository.AddAsync(newAns);
                }
            }
        }

        private async Task LogSubmissionActivityAsync(int submissionId, string action, string note)
        {
            try
            {
                var log = new KsSurveySubmissionLog
                {
                    IdPhienLamBai = submissionId,
                    HanhDong = action,
                    MoTa = note,
                    CreatedDate = DateTime.Now,
                };

                await _unitOfWork.iKsSurveySubmissionLogRepository.AddAsync(log);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi ghi log bài làm");
            }
        }

    }
}
