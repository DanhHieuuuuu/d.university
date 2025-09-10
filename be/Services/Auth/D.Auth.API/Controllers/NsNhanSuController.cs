using D.Auth.Domain.Dtos;
using D.ControllerBases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Auth.API.Controllers
{
    [Route("/api/nhansu")]
    [ApiController]
    public class NsNhanSuController : APIControllerBase
    {
        private IMediator _mediator;
        public NsNhanSuController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list-nhan-su")]
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
        
    }
}
