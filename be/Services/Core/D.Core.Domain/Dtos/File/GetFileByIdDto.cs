using D.DomainBase.Common;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.File
{
    public class GetFileByIdDto : IQuery<FileResponseDto>
    {
        [FromQuery(Name = "id")]
        public int Id { get; set; }
    }
}
