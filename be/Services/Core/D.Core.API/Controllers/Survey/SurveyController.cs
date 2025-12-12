using D.ControllerBase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Survey
{
    [Route("/api/survey")]
    [ApiController]
    public class SurveyController : APIControllerBase
    {
        private readonly IMediator _mediator;

        public SurveyController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }


    }
}
