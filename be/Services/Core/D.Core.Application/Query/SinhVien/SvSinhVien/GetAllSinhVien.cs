using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.SinhVien.SvSinhVien
{
    internal class GetAllSinhVien : IQueryHandler<SvSinhVienGetAllRequestDto, PageResultDto<SvSinhVienGetAllResponseDto>>
    {
        public ISvSinhVienService _svSinhVienService;

        public GetAllSinhVien(ISvSinhVienService svSinhVienService)
        {
            _svSinhVienService = svSinhVienService;
        }

        public async Task<PageResultDto<SvSinhVienGetAllResponseDto>> Handle(
            SvSinhVienGetAllRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _svSinhVienService.GetAllSinhVien(request);
        }
    }
}
