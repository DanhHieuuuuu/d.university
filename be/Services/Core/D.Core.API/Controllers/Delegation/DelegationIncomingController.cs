using d.Shared.Permission;
using d.Shared.Permission.Permission;
using D.ControllerBase;
using D.Core.Domain.Dtos.Delegation.Incoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace D.Core.API.Controllers.Delegation
{
    [Route("/api/delegation-incoming")]
    [ApiController]
    public class DelegationIncomingController : APIControllerBase
    {
        private IMediator _mediator;

        public DelegationIncomingController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy paging
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("paging")]
        public async Task<ResponseAPI> Paging([FromQuery] FilterDelegationIncomingDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Tạo mới đoàn vào
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreButtonCreateDoanVao)]
        [HttpPost("create")]
        public async Task<ResponseAPI> CreateDelegationIncoming([FromForm] CreateRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy phòng ban 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get-phongban")]
        public async Task<ResponseAPI> GetAllPhongBan([FromQuery] ViewPhongBanRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy Đoàn vào 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get-delegation-incoming")]
        public async Task<ResponseAPI> GetAllDelegationIncoming([FromQuery] ViewDelegationIncomingRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy nhân sự
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get-nhansu")]
        public async Task<ResponseAPI> GetAllNhanSu([FromQuery] ViewNhanSuRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy trạng thái 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get-status")]
        public async Task<ResponseAPI> GetListTrangThai([FromQuery] ViewTrangThaiRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Cập nhật thông tin đoàn vào
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreButtonUpdateDoanVao)]
        [HttpPut("update")]
        public async Task<ResponseAPI> UpdateDoanVao([FromForm] UpdateDelegationIncomingRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Cập nhật thông tin chi tiết đoàn vào
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreButtonUpdateDoanVao)]
        [HttpPut("update-detail-delegation")]
        public async Task<ResponseAPI> UpdateDetailDelegation([FromBody] UpdateDetailDelegationsRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa mềm Đoàn vào
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>    
        [PermissionFilter(PermissionCoreKeys.CoreButtonDeleteDoanVao)]
        [HttpDelete("delete/{id}")]
        public async Task<ResponseAPI> DeleteDoanVao([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDelegationIncomingDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Xóa mềm DepartmentSupport
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>    
        [HttpDelete("delete-department-support/{id}")]
        public async Task<ResponseAPI> DeleteDepartmentSupprot([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDepartmentSupportDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy đoàn vào theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("get-by-id")]
        public async Task<ResponseAPI> GetDelegationIncomingById(
            [FromQuery] DelegationIncomingGetByIdRequestDto dto
        )
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy thông tin thành viên theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("get-staff-by-id")]
        public async Task<ResponseAPI> GetDetailDelegationIncomingById(
            [FromQuery] DetailDelegationIncomingRequestDto dto
        )
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy thông tin thời gian đoàn vào theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("get-reception-time-by-id")]
        public async Task<ResponseAPI> GetReceptionTimeById([FromQuery] ReceptionTimeRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Tạo mới Thời gian tiếp đoàn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreButtonCreateTimeXuLyDoanVao)]
        [HttpPost("create-reception-time")]
        public async Task<ResponseAPI> CreateReceptionTime([FromBody] CreateReceptionTimeListRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// <summary>
        /// Tạo mới chuẩn bị đồ tiếp đoàn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create-prepare")]
        public async Task<ResponseAPI> CreatePrepare([FromBody] CreatePrepareRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// <summary>
        /// Cập nhật thông tin thời gian tiếp đoàn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update-reception-time")]
        public async Task<ResponseAPI> UpdateReceptionTime([FromBody] UpdateReceptionTimesRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Cập nhật đồ chuẩn bị
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update-prepare")]
        public async Task<ResponseAPI> UpdatePrepare([FromBody] UpdatePrepareRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("download-excel")]
        public IActionResult DownloadExcel()
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Template",
                "detail_delegation.xlsx"
            );

            if (!System.IO.File.Exists(filePath))
                return NotFound("File không tồn tại");

            var bytes = System.IO.File.ReadAllBytes(filePath);

            var stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read
            );

            return FileByStream(
                 stream,
                 "Delegation_Template.xlsx");
        }

        /// <summary>
        /// Xóa mềm Thời gian tiếp đoàn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>     
        [HttpDelete("delete-reception-time/{id}")]
        public async Task<ResponseAPI> DeleteReceptionTime([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteReceptionTimeDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Insert Log Đoàn vào
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost("log-status/insert")]
        public async Task<ResponseAPI> InsertLogStatus([FromBody] InsertDelegationIncomingLogDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy log status
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreMenuLog)]
        [HttpGet("get-log-status")]
        public async Task<ResponseAPI> GetLogStatus([FromQuery] FindDelegationIncomingLogDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Insert Log thời gian tiếp đoàn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("log-reception-time/insert")]
        public async Task<ResponseAPI> InsertLogReceptionTime([FromBody] InsertReceptionTimeLogDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy log Thời gian tiếp đoàn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreMenuLog)]
        [HttpGet("get-log-reception-time")]
        public async Task<ResponseAPI> GetLogReceptionTime([FromQuery] FindReceptionTimeLogDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Trạng thái tiếp theo của đoàn vào
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("next-status")]
        public async Task<ResponseAPI> NextStatus([FromBody] UpdateStatusRequestDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy paging nhân viên hỗ trợ 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("paging-supporter")]
        public async Task<ResponseAPI> PagingSupporter([FromQuery] FilterSupporterDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Tạo mới nhân viên hỗ trợ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create-supporter")]
        public async Task<ResponseAPI> CreateSupporter([FromBody] CreateSupporterRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy paging bộ phận hỗ trợ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("paging-department-support")]
        public async Task<ResponseAPI> PagingDepartmentSupport([FromQuery] FilterDepartmentSupportDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Tạo mới bộ phận hỗ trợ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create-department-support")]
        public async Task<ResponseAPI> CreateDepartmentSupport([FromBody] CreateDepartmentSupportRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Cập nhật Phòng ban hỗ trợ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update-department-support")]
        public async Task<ResponseAPI> UpdateDepartmentSupport([FromBody] UpdateDepartmentSupportRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy phòng ban theo id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get-id-department-support")]
        public async Task<ResponseAPI> GetByIdDepartmentSupport([FromQuery] DetailDepartmentSupportRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Xuất báo cáo đoàn vào
        /// </summary>
        [HttpGet("export-report")]
        public async Task<IActionResult> ExportDelegationIncomingReport()
        {
            var fileBytes = await _mediator.Send(new ExportReport());

            var fileName =$"BaoCaoDoanVao_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }

        /// <summary>
        /// Xuất tờ trình đoàn vào
        /// </summary>
        [HttpPost("bao_cao_doan_vao")]
        public async Task<IActionResult> ExportGiayDoanVao([FromBody] ExportGiayDoanVaoDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);

                return FileByStream(
                    result.Stream!,
                    result.FileName!
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Thống kê đoàn vào
        /// </summary>
        [HttpGet("statistical")]
        public async Task<ResponseAPI> GetDelegationIncomingSummary()
        {
            try
            {
                var result = await _mediator.Send(
                    new StatisticalRequestDto()
                );
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Lấy danh sách ngày tạo log thời gian tiếp đoàn
        /// </summary>
        [HttpGet("get-created-dates")]
        public async Task<ResponseAPI> GetAllCreatedDate(
            [FromQuery] GetAllCreatedDateRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }
}
