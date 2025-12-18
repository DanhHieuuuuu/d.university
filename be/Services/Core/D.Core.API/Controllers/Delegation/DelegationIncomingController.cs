using d.Shared.Permission;
using D.ControllerBase;
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
        /// Xóa mềm Đoàn vào
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>     
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
        [HttpPost("create-reception-time")]
        public async Task<ResponseAPI> CreateReceptionTime([FromBody] CreateReceptionTimeRequestDto dto)
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
        public async Task<ResponseAPI> UpdateReceptionTime([FromBody] UpdateReceptionTimeRequestDto dto)
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
        /// <returns></returns>
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
    }
}
