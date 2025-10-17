using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu
{
    public class DeleteDmChucVuDto : ICommand
    {
        public int Id { get; set; }
    }
}
