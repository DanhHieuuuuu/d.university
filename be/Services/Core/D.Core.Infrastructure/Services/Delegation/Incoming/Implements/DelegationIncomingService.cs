using AutoMapper;
using d.Shared.Permission.Error;
using d.Shared.Permission.Role;
using D.Constants.Core.Delegation;
using D.ControllerBase.Exceptions;
using D.Core.Domain;
using D.Core.Domain.Dtos.Delegation;
using D.Core.Domain.Dtos.Delegation.Incoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.Core.Infrastructure.Services.File.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using D.Notification.ApplicationService.Abstracts;
using D.Notification.ApplicationService.Implements;
using D.Notification.Domain.Enums;
using D.Notification.Dtos;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenXMLLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements
{
    public class DelegationIncomingService : ServiceBase, IDelegationIncomingService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IExcelService _excelService;
        private readonly INotificationService _notificationService;
        private readonly IFileService _fileService;
        public DelegationIncomingService(
            ILogger<DelegationIncomingService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IExcelService excelService,
            INotificationService notificationService,
            IFileService fileService

        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _excelService = excelService;
            _notificationService = notificationService;
            _fileService = fileService;
        }

        /// <summary>
        /// Hàm paging
        /// </summary>
        /// <param name="dto">các biến đầu vào paging</param>
        /// <returns>PageResultDto</returns>
        public PageResultDto<PageDelegationIncomingResultDto> Paging(FilterDelegationIncomingDto dto)
        {
            _logger.LogInformation($"{nameof(Paging)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var phongBanTable = _unitOfWork.iDmPhongBanRepository.TableNoTracking;
            var staffTable = _unitOfWork.iNsNhanSuRepository.TableNoTracking;
            var receptionTime = _unitOfWork.iReceptionTimeRepository.TableNoTracking.Include(d => d.Id);
            var query = from d in _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                        join pb in phongBanTable on d.IdPhongBan equals pb.Id into pbJoin
                        from pb in pbJoin.DefaultIfEmpty()
                        join s in staffTable on d.IdStaffReception equals s.Id into sJoin
                        from s in sJoin.DefaultIfEmpty()
                        where (string.IsNullOrEmpty(dto.Keyword) || d.Name.ToLower().Contains(dto.Keyword.ToLower()))
                              && (dto.IdPhongBan == 0 || d.IdPhongBan == dto.IdPhongBan)
                              && (dto.Status == 0 || d.Status == dto.Status)
                        select new PageDelegationIncomingResultDto
                        {
                            Id = d.Id,
                            Code = d.Code,
                            Name = d.Name,
                            Content = d.Content,
                            IdPhongBan = d.IdPhongBan,
                            PhongBan = pb != null ? pb.TenPhongBan : null,
                            IdStaffReception = d.IdStaffReception,
                            StaffReceptionName = s != null ? s.HoDem + " " + s.Ten : null,
                            TotalPerson = d.TotalPerson,
                            PhoneNumber = d.PhoneNumber,
                            Status = d.Status,
                            Location = d.Location,
                            RequestDate = d.RequestDate,
                            ReceptionDate = d.ReceptionDate,
                            TotalMoney = d.TotalMoney,
                            ReceptionTimes = d.ReceptionTimes.Where(s => !s.Deleted).ToList(),
                        };

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            return new PageResultDto<PageDelegationIncomingResultDto>
            {
                Items = items,
                TotalItem = totalCount,
            };
        }


        public async Task<CreateResponseDto> Create(CreateRequestDto dto)
        {
            _logger.LogInformation($"{nameof(Create)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            bool isExist = _unitOfWork.iDelegationIncomingRepository.IsMaDoanVaoExist(dto.Code);
            if (isExist)
            {
                throw new Exception($"Đã tồn tại Đoàn vào có mã {dto.Code}");
            }
            var newDoanVao = _mapper.Map<DelegationIncoming>(dto);

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);


            newDoanVao.Status = DelegationStatus.Create;
            _unitOfWork.iDelegationIncomingRepository.Add(newDoanVao);
            await _unitOfWork.SaveChangesAsync();

            if (dto.DetailDelegation != null)
            {
                var templateRules = new List<ExcelColumnRule>
                {
                    new()
                    {
                        Header = "Họ",
                        Required = true,
                        Validator = v => v.Length >= 1,
                        ErrorMessage = "Họ không hợp lệ"
                    },
                    new()
                    {
                        Header = "Tên",
                        Required = true,
                        Validator = v => v.Length >= 1,
                        ErrorMessage = "Tên không hợp lệ"
                    },
                    new()
                    {
                        Header = "Năm sinh",
                        Required = true,
                        Validator = v =>
                        {
                            if (!int.TryParse(v, out var year)) return false;
                            return year >= 1900 && year <= DateTime.Now.Year;
                        },
                        ErrorMessage = "Năm sinh không hợp lệ"
                    },
                    new()
                    {
                        Header = "Số điện thoại liên lạc",
                        Required = true,
                        Validator = v =>
                            System.Text.RegularExpressions.Regex.IsMatch(v, @"^(0|\+84)[0-9]{9}$"),
                        ErrorMessage = "Số điện thoại không đúng định dạng"
                    },
                    new()
                    {
                        Header = "Email",
                        Required = true,
                        Validator = IsValidEmail,
                        ErrorMessage = "Email sai định dạng"
                    },
                    new()
                    {
                        Header = "Trưởng đoàn (Có/Không)",
                        Required = true,
                        Validator = v =>
                            v.Equals("Có", StringComparison.OrdinalIgnoreCase) ||
                            v.Equals("Không", StringComparison.OrdinalIgnoreCase),
                        ErrorMessage = "Giá trị Trưởng đoàn phải là 'Có' hoặc 'Không'"
                    }
                };

                await _excelService.CheckValidateDetailDelegationAsync(dto.DetailDelegation, templateRules);

                List<DetailDelegationIncoming> detailDelegationIncomings = await _excelService.ParseExcelToListDetailDelegationAsync(dto.DetailDelegation);

                int countDetail = _unitOfWork.iDetailDelegationIncomingRepository.TableNoTracking.Count();

                for(int i = 0; i < detailDelegationIncomings.Count(); i++)
                {
                    int stt = countDetail + i + 1;
                    detailDelegationIncomings[i].Code = $"DDI{stt}";
                    detailDelegationIncomings[i].DelegationIncomingId = newDoanVao.Id;
                }

                _unitOfWork.iDetailDelegationIncomingRepository.AddRange(detailDelegationIncomings);
                await _unitOfWork.SaveChangesAsync();
            }


            #region Log         
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking
            .FirstOrDefault(u => u.Id == userId);
            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown";

            var log = new LogStatus
            {
                DelegationIncomingCode = newDoanVao.Code,
                NewStatus = DelegationStatus.Create,
                OldStatus = DelegationStatus.Create,
                Description = $"Thêm đoàn vào ",
                CreatedDate = DateTime.Now,
                Reason = "Thêm mới đoàn vào",
                CreatedByName = userName,
            };

            _unitOfWork.iLogStatusRepository.Add(log);
            await _unitOfWork.SaveChangesAsync();
            scope.Complete();
            #endregion
            await _notificationService.SendAsync(new NotificationMessage
            {
                Receiver = new Receiver
                {
                    // người tiếp nhận đoàn
                    UserId = newDoanVao.IdStaffReception
                },
                Title = "Đoàn vào mới",
                Content = "Bạn có một đoàn vào mới cần xử lý",
                AltContent = $"Đoàn {newDoanVao.Name} ({newDoanVao.Code}) vừa được tạo cho bạn.",
                Channel = NotificationChannel.Realtime
            });
            return _mapper.Map<CreateResponseDto>(newDoanVao);

        }

        public async Task<UpdateDelegationIncomingResponseDto> UpdateDelegationIncoming(UpdateDelegationIncomingRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateDelegationIncoming)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDelegationIncomingRepository.Table
                .FirstOrDefault(x => x.Id == dto.Id);

            if (exist == null)
                throw new Exception($"Không tìm thấy Đoàn vào này.");

            var existMaDoanVao = _unitOfWork.iDelegationIncomingRepository.Table
                .Any(x => x.Code == dto.Code && x.Id != dto.Id);

            if (existMaDoanVao)
                throw new Exception($"Đã tồn tại mã Đoàn vào \"{dto.Code}\".");

            // Lưu giá trị cũ để log
            var oldValues = new
            {
                exist.Code,
                exist.Name,
                exist.Content,
                exist.IdPhongBan,
                exist.Location,
                exist.IdStaffReception,
                exist.TotalPerson,
                exist.PhoneNumber,
                exist.RequestDate,
                exist.ReceptionDate,
                exist.TotalMoney,
                exist.Status
            };

            // Cập nhật 
            exist.Code = dto.Code;
            exist.Name = dto.Name;
            exist.Content = dto.Content;
            exist.IdPhongBan = dto.IdPhongBan;
            exist.Location = dto.Location;
            exist.IdStaffReception = dto.IdStaffReception;
            exist.TotalPerson = dto.TotalPerson;
            exist.PhoneNumber = dto.PhoneNumber;
            exist.RequestDate = dto.RequestDate;
            exist.ReceptionDate = dto.ReceptionDate;
            exist.TotalMoney = dto.TotalMoney;
            exist.Status = DelegationStatus.Edited;

            _unitOfWork.iDelegationIncomingRepository.Update(exist);
            await _unitOfWork.SaveChangesAsync();

            var staffReceptionName = _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .Where(x => x.Id == exist.IdStaffReception)
                .Select(x => x.HoDem + " " + x.Ten)
                .FirstOrDefault();

            var phongBanName = _unitOfWork.iDmPhongBanRepository.TableNoTracking
                .Where(x => x.Id == exist.IdPhongBan)
                .Select(x => x.TenPhongBan)
                .FirstOrDefault();

            #region Log
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking
            .FirstOrDefault(u => u.Id == userId);
            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown";

            var changes = new List<string>();
            if (oldValues.Code != dto.Code) changes.Add($"Code: '{oldValues.Code}' => '{dto.Code}'");
            if (oldValues.Name != dto.Name) changes.Add($"Name: '{oldValues.Name}' => '{dto.Name}'");
            if (oldValues.Content != dto.Content) changes.Add($"Content: '{oldValues.Content}' => '{dto.Content}'");
            if (oldValues.IdPhongBan != dto.IdPhongBan) changes.Add($"IdPhongBan: '{oldValues.IdPhongBan}' => '{dto.IdPhongBan}'");
            if (oldValues.Location != dto.Location) changes.Add($"Location: '{oldValues.Location}' => '{dto.Location}'");
            if (oldValues.IdStaffReception != dto.IdStaffReception) changes.Add($"IdStaffReception: '{oldValues.IdStaffReception}' => '{dto.IdStaffReception}'");
            if (oldValues.TotalPerson != dto.TotalPerson) changes.Add($"TotalPerson: '{oldValues.TotalPerson}' => '{dto.TotalPerson}'");
            if (oldValues.PhoneNumber != dto.PhoneNumber) changes.Add($"PhoneNumber: '{oldValues.PhoneNumber}' => '{dto.PhoneNumber}'");
            if (oldValues.RequestDate != dto.RequestDate) changes.Add($"RequestDate: '{oldValues.RequestDate}' => '{dto.RequestDate}'");
            if (oldValues.ReceptionDate != dto.ReceptionDate) changes.Add($"ReceptionDate: '{oldValues.ReceptionDate}' => '{dto.ReceptionDate}'");
            if (oldValues.TotalMoney != dto.TotalMoney) changes.Add($"TotalMoney: '{oldValues.TotalMoney}' => '{dto.TotalMoney}'");
            if (oldValues.Status != exist.Status) changes.Add($"Status: '{DelegationStatus.Names[oldValues.Status]} => {DelegationStatus.Names[exist.Status]}'");

            var description = changes.Any()
                ? $"Cập nhật đoàn vào: {string.Join("; ", changes)}."
                : $"Cập nhật đoàn vào nhưng không thay đổi giá trị.";

            var log = new LogStatus
            {
                DelegationIncomingCode = exist.Code,
                OldStatus = oldValues.Status,
                NewStatus = exist.Status,
                Reason = "Cập nhật",
                Description = description,
                CreatedDate = DateTime.Now,
                CreatedByName = userName,
            };

            _unitOfWork.iLogStatusRepository.Add(log);
            await _unitOfWork.SaveChangesAsync();
            #endregion

            var result = _mapper.Map<UpdateDelegationIncomingResponseDto>(exist);
            result.StaffReceptionName = staffReceptionName;
            result.PhongBan = phongBanName;

            return result;
        }


        public void DeleteDoanVao(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDoanVao)} method called. Dto: {id}");

            var exist = _unitOfWork.iDelegationIncomingRepository.FindById(id);

            if (exist != null)
            {
                _unitOfWork.iDelegationIncomingRepository.Delete(exist);
                _unitOfWork.iDelegationIncomingRepository.SaveChange();
            }
            else
            {
                throw new Exception($"Đoàn vào không tồn tại hoặc đã bị xóa");
            }
        }


        public List<ViewPhongBanResponseDto> GetAllPhongBan(ViewPhongBanRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllPhongBan)} called.");

            var list = _unitOfWork.iDmPhongBanRepository.TableNoTracking
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.STT ?? int.MaxValue)
                .Select(x => new ViewPhongBanResponseDto
                {
                    IdPhongBan = x.Id,
                    TenPhongBan = x.TenPhongBan
                })
                .ToList();

            return list;
        }
        public List<ViewNhanSuResponseDto> GetAllNhanSu(ViewNhanSuRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllNhanSu)} called.");

            var list =
                (from ns in _unitOfWork.iNsNhanSuRepository.TableNoTracking
                 where ns.DaChamDutHopDong != true
                    && ns.DaVeHuu != true
                    && ns.IsThoiViec != true

                 join pb in _unitOfWork.iDmPhongBanRepository.TableNoTracking
                    on ns.HienTaiPhongBan equals pb.Id into pbJoin
                 from pb in pbJoin.DefaultIfEmpty()

                 join sp in _unitOfWork.iSupporterRepository.TableNoTracking
                    on ns.Id equals sp.SupporterId into spJoin
                 from sp in spJoin.DefaultIfEmpty()

                 select new ViewNhanSuResponseDto
                 {
                     IdNhanSu = ns.Id,
                     TenNhanSu = (ns.HoDem ?? "") + " " + (ns.Ten ?? ""),
                     SupporterCode = sp != null ? sp.SupporterCode : null,
                     IdPhongBan = ns.HienTaiPhongBan

                 })
                .ToList();

            return list;
        }



        public List<ViewTrangThaiResponseDto> GetListTrangThai(ViewTrangThaiRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetListTrangThai)}");

            var trangThaiExist = _unitOfWork.iDelegationIncomingRepository
                .TableNoTracking
                .Where(x => x.Status != null)
                .Select(x => x.Status)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            return trangThaiExist
                .Select(x => new ViewTrangThaiResponseDto { Status = x })
                .ToList();
        }
        public async Task<PageDelegationIncomingResultDto> GetByIdDelegationIncoming(int id)
        {
            _logger.LogInformation($"{nameof(GetByIdDelegationIncoming)} called with id: {id}");

            var phongBanTable = _unitOfWork.iDmPhongBanRepository.TableNoTracking;
            var staffTable = _unitOfWork.iNsNhanSuRepository.TableNoTracking;

            var query = from d in _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                        where d.Id == id
                        join pb in phongBanTable on d.IdPhongBan equals pb.Id into pbJoin
                        from pb in pbJoin.DefaultIfEmpty()
                        join s in staffTable on d.IdStaffReception equals s.Id into sJoin
                        from s in sJoin.DefaultIfEmpty()
                        select new PageDelegationIncomingResultDto
                        {
                            Id = d.Id,
                            Code = d.Code,
                            Name = d.Name,
                            Content = d.Content,
                            IdPhongBan = d.IdPhongBan,
                            PhongBan = pb != null ? pb.TenPhongBan : null,
                            IdStaffReception = d.IdStaffReception,
                            StaffReceptionName = s != null ? s.HoDem + " " + s.Ten : null,
                            TotalPerson = d.TotalPerson,
                            PhoneNumber = d.PhoneNumber,
                            Status = d.Status,
                            Location = d.Location,
                            RequestDate = d.RequestDate,
                            ReceptionDate = d.ReceptionDate,
                            TotalMoney = d.TotalMoney
                        };

            var result = query.FirstOrDefault();
            return result;
        }
        public async Task<DetailDelegationIncomingResponseDto> GetByIdDetailDelegation(int id)
        {
            _logger.LogInformation($"{nameof(GetByIdDetailDelegation)} called with id: {id}");

            var detail = _unitOfWork.iDetailDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == id);

            if (detail == null)
            {
                return null; 
            }

            var delegation = _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == detail.DelegationIncomingId);
           
            var result = new DetailDelegationIncomingResponseDto
            {
                Id = detail.Id,
                Code = detail.Code,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                YearOfBirth = detail.YearOfBirth,
                PhoneNumber = detail.PhoneNumber,
                Email = detail.Email,
                IsLeader = detail.IsLeader,
                DelegationIncomingId = detail.DelegationIncomingId,
                DelegationName = delegation != null ? delegation.Name : null, 
                DelegationCode = delegation != null ? delegation.Code : null
            };

            return result;
        }
        public async Task<ReceptionTimeResponseDto> GetByIdReceptionTime(int id)
        {
            _logger.LogInformation($"{nameof(GetByIdReceptionTime)} called with id: {id}");

            var receptionTime = _unitOfWork.iReceptionTimeRepository.TableNoTracking
                .FirstOrDefault(r => r.Id == id);

            if (receptionTime == null)
                return null; 

            var delegation = _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == receptionTime.DelegationIncomingId);

            var result = new ReceptionTimeResponseDto
            {
                Id = receptionTime.Id,
                StartDate = receptionTime.StartDate,
                EndDate = receptionTime.EndDate,
                Date = receptionTime.Date,
                Content = receptionTime.Content,
                TotalPerson = receptionTime.TotalPerson,
                Address = receptionTime.Address,
                DelegationIncomingId = receptionTime.DelegationIncomingId,
                DelegationName = delegation != null ? delegation.Name : null,
                DelegationCode = delegation != null ? delegation.Code : null
            };

            return result;
        }

        /// <summary>
        /// Trạng thái tiếp theo của đoàn vào
        /// </summary>
        /// <param name="idDelegation"></param>
        /// <param name="oldStatus"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task NextStatus(UpdateStatusRequestDto dto)
        {
            _logger.LogInformation($"{nameof(NextStatus)} method called, dto: {JsonSerializer.Serialize(dto)}.");
            // Kiểm check delegation
            var delegation = _unitOfWork.iDelegationIncomingRepository.FindById(dto.IdDelegation);

            if(delegation == null)
            {
                throw new UserFriendlyException(ErrorCodeConstant.DelegationNotFound, "Không tìm thấy đoàn vào.");
            }
            if(delegation.Status != dto.OldStatus)
                throw new UserFriendlyException(4002, "Trạng thái của đoàn vào đã được sửa đổi.");

            if(delegation.Status == DelegationStatus.Done)
                throw new UserFriendlyException(4003, "Đoàn này đã hoàn thành tiếp đoàn.");

            //if(delegation.Status == DelegationStatus.ReceptionGroup)
            //    throw new UserFriendlyException(4003, "Đoàn này đang thực hiện tiếp đoàn.");

            if (dto.Action == "upgrade")
            {
                if(dto.OldStatus == DelegationStatus.Create)
                {
                    delegation.Status = DelegationStatus.Propose;
                }
                else if( dto.OldStatus == DelegationStatus.Propose || dto.OldStatus == DelegationStatus.Edited)
                {
                    delegation.Status = DelegationStatus.BGHApprove;
                }
                else if (dto.OldStatus == DelegationStatus.BGHApprove)
                {
                    delegation.Status = DelegationStatus.ReceptionGroup;
                }
                else if (dto.OldStatus == DelegationStatus.ReceptionGroup)
                {
                    delegation.Status = DelegationStatus.Done;
                }
                else if (dto.OldStatus == DelegationStatus.NeedEdit)
                {
                    delegation.Status = DelegationStatus.Edited;
                }
            }
            else if(dto.Action == "supplement")
            {
                delegation.Status = DelegationStatus.NeedEdit;
            }
            else if(dto.Action == "cancel")
            {
                delegation.Status = DelegationStatus.Canceled;
            }
            _unitOfWork.iDelegationIncomingRepository.Update(delegation);
            await _unitOfWork.SaveChangesAsync();

            // Ghi log
            #region Log         
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking
            .FirstOrDefault(u => u.Id == userId);
            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown";

            var log = new LogStatus
            {
                DelegationIncomingCode = delegation.Code,
                NewStatus = dto.OldStatus,
                OldStatus = delegation.Status,
                Description = $"Đã thay đổi trạng thái từ {DelegationStatus.Names[dto.OldStatus]} => {DelegationStatus.Names[delegation.Status]} ",
                CreatedDate = DateTime.Now,
                CreatedByName = userName,
                Reason = "Trạng thái"
            };

            _unitOfWork.iLogStatusRepository.Add(log);
            await _unitOfWork.SaveChangesAsync();
            #endregion
            await _notificationService.SendAsync(new NotificationMessage
            {
                Receiver = new Receiver
                {
                    UserId = delegation.IdStaffReception
                },
                Title = "Cập nhật trạng thái đoàn vào",
                Content = $"Đoàn {delegation.Code} đã được cập nhật trạng thái",
                AltContent = $"Cập nhật trạng thái mới: {DelegationStatus.Names[delegation.Status]}",
                Channel = NotificationChannel.Realtime
            });
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                _ = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<byte[]> ExportDelegationIncomingReport()
        {
            var data = await _unitOfWork.iDelegationIncomingRepository
                .TableNoTracking
                .Where(x => x.Status == DelegationStatus.Done)
                .Select(x => new DelegationIncomingReportDto
                {
                    Code = x.Code,
                    Name = x.Name,
                    TotalPerson = x.TotalPerson,
                    TotalMoney = x.TotalMoney,
                    ReceptionDate = x.ReceptionDate,
                    Status = x.Status,
                })
                .ToListAsync();

            if (!data.Any())
                throw new UserFriendlyException(4004, "Không có đoàn hoàn thành để xuất báo cáo.");

            return await _excelService.ExportAsync(
                data,
                sheetName: "BaoCaoDoanVao",
                title: "BÁO CÁO ĐOÀN VÀO"
            );
        }
        private string FormatDateTimeVN(DateOnly dt)
        {
            string thu = dt.ToString("dddd", new System.Globalization.CultureInfo("vi-VN"));
            thu = char.ToUpper(thu[0]) + thu.Substring(1); // Viết hoa chữ đầu
            return $"{thu}, ngày {dt:dd}, tháng {dt:MM}, năm {dt:yyyy}";
        }
        private string FormatReceptionTimeVN(DateOnly date, TimeOnly start, TimeOnly end)
        {
            var culture = new System.Globalization.CultureInfo("vi-VN");

            // Ghép DateOnly + TimeOnly 
            var startDateTime = date.ToDateTime(start);

            string thu = culture.DateTimeFormat.GetDayName(startDateTime.DayOfWeek);
            thu = char.ToUpper(thu[0]) + thu.Substring(1);

            return $"{start:HH} giờ {start:mm} – {end:HH} giờ {end:mm}, {thu}, ngày {date:dd}, tháng {date:MM}, năm {date:yyyy}";
        }

        private string FormatCurrentDateVN()
        {
            var now = DateTime.Now;
            return $"Hà Nội, ngày {now:dd} tháng {now:MM} năm {now:yyyy}";
        }

        public ExportFileDto ExportGiayDoanVao(ExportGiayDoanVaoDto dto)
        {
            try
            {
                if (dto.ListId.Count > 0)
                {
                    if (dto.IsExportAll)
                    {
                        dto.ListId = _unitOfWork.iDelegationIncomingRepository
                            .TableNoTracking
                            .Where(x => !x.Deleted)
                            .Select(x => x.Id)
                            .ToList();
                    }
                }
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        int stt = 1;
                        string path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "Template",
                            "bao_cao_doan_vao.docx"
                        );
                        foreach (var id in dto.ListId)
                        {
                            var guestGroup = _unitOfWork.iDelegationIncomingRepository.TableNoTracking.FirstOrDefault(s =>
                                s.Id == id
                            );
                            var departmentName = _unitOfWork.iDmPhongBanRepository
                                .TableNoTracking
                                .Where(x => x.Id == guestGroup.IdPhongBan)
                                .Select(x => x.TenPhongBan)
                                .FirstOrDefault();
                            var receptionStaffName = _unitOfWork.iNsNhanSuRepository
                                .TableNoTracking
                                .Where(x => x.Id == guestGroup.IdStaffReception)
                                .Select(x => x.HoDem + " " + x.Ten)
                                .FirstOrDefault();
                            var gifts = (
                                from rt in _unitOfWork.iReceptionTimeRepository.TableNoTracking
                                join p in _unitOfWork.iPrepareRepository.TableNoTracking
                                    on rt.Id equals p.ReceptionTimeId
                                where rt.DelegationIncomingId == guestGroup.Id
                                select new
                                {
                                    p.Name,
                                    p.Money
                                }
                            ).ToList();
                            var giftText = gifts.Any()
                                ? string.Join(
                                    Environment.NewLine,
                                    gifts.Select(g => $"{g.Name} ({g.Money:N0}đ)")
                                  )
                                : "";
                            var receptionTimes = _unitOfWork.iReceptionTimeRepository
                                .TableNoTracking
                                .Where(t => t.DelegationIncomingId == guestGroup.Id)
                                .OrderBy(t => t.Date)
                                .ThenBy(t => t.StartDate)
                                .ToList();


                            // Format thời gian
                            string timeDetails = string.Join(
                                Environment.NewLine,
                                receptionTimes.Select(t =>
                                    FormatReceptionTimeVN(t.Date, t.StartDate, t.EndDate)
                                )
                            );


                            string fileName = $"Tờ trình việc tiếp đón đoàn {guestGroup.Name}.docx";
                            var result = _fileService.FillTextToDocumentTemplate(
                                path,
                                fileName,
                                new List<InputReplaceDto>
                                {
                                    new InputReplaceDto(
                                        "{department}",
                                        departmentName ?? ""
                                    ),
                                    new InputReplaceDto("{content}", guestGroup.Content ?? ""),
                                    new InputReplaceDto("{name}", guestGroup.Name ?? ""),                                                                   
                                    new InputReplaceDto("{totalperson}",guestGroup.TotalPerson.ToString() ?? ""),
                                    new InputReplaceDto("{location}", guestGroup.Location ?? ""),
                                    new InputReplaceDto("{gift}", giftText ?? ""),
                                    new InputReplaceDto("{receptionstaff}", receptionStaffName ?? ""),                                                         
                                    new InputReplaceDto(
                                        "{phonenumber}",
                                        guestGroup.PhoneNumber ?? ""
                                    ),
                                    new InputReplaceDto("{currentdate}", FormatCurrentDateVN()),
                                    new InputReplaceDto("{timeguestgroups}", timeDetails),
                                }
                            );                           

                            var tempFile = archive.CreateEntry(fileName);
                            using (var entryStream = tempFile.Open())
                            {
                                result.Stream!.Seek(0, SeekOrigin.Begin);
                                result.Stream.CopyTo(entryStream);
                            }
                            stt++;
                        }
                    }
                    // copy zip file to result
                    MemoryStream ms = new MemoryStream();
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(ms);
                    ms.Position = 0;

                    return new ExportFileDto
                    {
                        FileName = $"Bao_cao_doan_vao_{DateTime.Now.ToFileTime()}.zip",
                        Stream = ms,
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw;
            }
        }

        public StatisticalResultDto GetStatistical()
        {
            _logger.LogInformation($"{nameof(GetStatistical)} called.");

            // Query các đoàn vào
            var query = _unitOfWork.iDelegationIncomingRepository
                .TableNoTracking
                .Where(x => !x.Deleted);

            // Tổng tất cả
            var totalAll = query.Count();

            // Gom theo trạng thái
            var byStatus = query
                .GroupBy(x => x.Status)
                .Select(g => new StatisticalStatusDto
                {
                    Status = g.Key,
                    Total = g.Count()
                })
                .OrderBy(x => x.Status)
                .ToList();

            return new StatisticalResultDto
            {
                TotalAll = totalAll,
                ByStatus = byStatus
            };
        }




    }
}
