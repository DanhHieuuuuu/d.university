using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class GetRequestSurveyDetailDto : IQuery<RequestSurveyDetailDto>
    {
        public int Id { get; set; }
        public GetRequestSurveyDetailDto(int id) => Id = id;
    }
}
