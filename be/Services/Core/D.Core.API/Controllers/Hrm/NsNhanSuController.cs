using D.ControllerBase;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using d.Shared.Permission;
using d.Shared.Permission.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using D.Core.Domain.Dtos.Hrm.HopDong;

namespace D.Core.API.Controllers.Hrm
{
    [Route("/api/nhansu")]
    [ApiController]
    public class NsNhanSuController : APIControllerBase
    {
        private IMediator _mediator;

        public NsNhanSuController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách nhân sự
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("find")]
        public async Task<ResponseAPI> GetListNhanSu(NsNhanSuRequestDto dto)
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
        /// Danh sách nhân sự bản format
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get-all")]
        public async Task<ResponseAPI> GetAll(NsNhanSuGetAllRequestDto dto)
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
        /// Danh sách nhân sự by KpiRole
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get-all-by-kpi-role")]
        public async Task<ResponseAPI> GetAllByKpiRole(NsNhanSuByKpiRoleRequestDto dto)
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
        /// Thêm mới nhân sự
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreButtonCreateNhanSu)]
        [HttpPost("create")]
        public async Task<ResponseAPI> CreateNhanSu(CreateNhanSuDto dto)
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
        /// Cập nhật thông tin nhân sự (thông tin cá nhân, thông tin gia đình)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreButtonUpdateNhanSu)]
        [HttpPut("update")]
        public async Task<ResponseAPI> UpdateNhanSu(UpdateNhanSuDto dto)
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
        /// Lấy thông tin nhân sự bằng số điện thoại, mã nhân sự
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreMenuNhanSu)]
        [HttpGet("get")]
        public async Task<ResponseAPI> FindByMaNsSdt(FindByMaNsSdtDto dto)
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
        /// Lấy thông tin nhân sự theo id
        /// </summary>
        /// <param name="idNhanSu"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreMenuNhanSu)]
        [HttpGet("{idNhanSu}")]
        public async Task<ResponseAPI> FindByIdNhanSu(int idNhanSu)
        {
            try
            {
                var dto = new NsNhanSuFindByIdRequestDto { IdNhanSu = idNhanSu };
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy thông tin hiển thị hồ sơ nhân sự theo Id
        /// </summary>
        /// <param name="idNhanSu"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreMenuNhanSu)]
        [HttpGet("ho-so/{idNhanSu}")]
        public async Task<ResponseAPI> GetHoSoNhanSu(int idNhanSu)
        {
            try
            {
                var dto = new NsNhanSuHoSoChiTietRequestDto { IdNhanSu = idNhanSu };
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Tạo hợp đồng
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreButtonCreateNhanSu)]
        [HttpPost("contract/create")]
        public async Task<ResponseAPI> CreateHopDong(CreateHopDongDto dto)
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

        [HttpGet("contract/find")]
        public async Task<ResponseAPI> GetAllContract(NsHopDongRequestDto dto)
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
