using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan
{
    public class DeleteDmPhongBanDto : ICommand
    {
        public int Id { get; set; }
    }
}
