using D.ControllerBase;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using d.Shared.Permission;
using d.Shared.Permission.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using D.Core.Domain.Dtos.Hrm.HopDong;
using D.Core.Domain.Dtos.Hrm.SemanticSearch;

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
        [PermissionFilter(PermissionCoreKeys.CoreMenuHrmDanhSach)]
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
        [PermissionFilter(PermissionCoreKeys.CoreMenuHrmDanhSach)]
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
        [PermissionFilter(PermissionCoreKeys.CoreMenuHrmDanhSach)]
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
        [PermissionFilter(PermissionCoreKeys.CoreButtonViewNhanSu)]
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
        [PermissionFilter(PermissionCoreKeys.CoreButtonCreateHrmContract)]
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

        /// <summary>
        /// Danh sách hợp đồng
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreTableHrmContract)]
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

        /// <summary>
        /// Thống kê nhân sự theo phòng ban
        /// </summary>
        /// <returns></returns>
        [HttpGet("thongke-theo-phongban")]
        public async Task<ResponseAPI> ThongKeNhanSuTheoPhongBanApi()
        {
            try
            {
                var dto = new ThongKeNhanSuTheoPhongBanRequestDto { };
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Tìm kiếm ngữ nghĩa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<ResponseAPI> SearchSemanticNhanSuApi(SearchSemanticRequestDto dto)
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
        /// Đồng bộ nhân sự vào Qdrant
        /// </summary>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreButtonSyncNhanSu)]
        [HttpPost("sync")]
        public async Task<ResponseAPI> FetchNhanSuQdrantApi()
        {
            try
            {
                var dto = new FetchNhanSuQdrantDto { };
                await _mediator.Send(dto);
                return new("Đã đồng bộ data với Qdrant");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
