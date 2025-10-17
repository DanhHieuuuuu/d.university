using D.DomainBase.Common;
namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon
{
    public class DmToBoMonGetByIdRequestDto : IQuery<DmToBoMonResponseDto>
    {
        public int Id { get; set; }
    }
}
