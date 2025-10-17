using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon
{
    public class DeleteDmToBoMonDto : ICommand
    {
        public int Id { get; set; }
    }
}
