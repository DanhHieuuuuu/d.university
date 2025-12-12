using d.Shared.Permission;
using D.ControllerBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ResponseAPI> CreateDelegationIncoming([FromBody] CreateRequestDto dto)
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
        public async Task<ResponseAPI> UpdateDoanVao([FromBody] UpdateDelegationIncomingRequestDto dto)
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
        public async Task<ResponseAPI> GetReceptionTimeById(
            [FromQuery] ReceptionTimeRequestDto dto
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

    }
}
