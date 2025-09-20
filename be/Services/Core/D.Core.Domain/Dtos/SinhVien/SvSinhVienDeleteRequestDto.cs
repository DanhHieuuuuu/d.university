using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class SvSinhVienDeleteRequestDto : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
