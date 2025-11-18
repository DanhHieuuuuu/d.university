using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class DeleteSinhVienDto : ICommand<bool>
    {
        public string? Mssv { get; set; }
    }
}
