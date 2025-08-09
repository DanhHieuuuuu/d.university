using D.Auth.Domain.Dtos.students;
using D.ControllerBases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Auth.API.Controllers
{
    [Route("/api/student")]
    [ApiController]
    public class StudentController : APIControllerBase
    {
        private IMediator _mediator;
        public StudentController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list-student")]
        public async Task<ResponseAPI> GetListStudent(StudentResquestDto dto)
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
