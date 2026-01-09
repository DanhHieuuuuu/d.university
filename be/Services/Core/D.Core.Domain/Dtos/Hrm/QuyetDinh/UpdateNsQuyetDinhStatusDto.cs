using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.QuyetDinh
{
    public class UpdateNsQuyetDinhStatusDto : ICommand
    {
        public int? IdQuyetDinh { get; set; }
        public int? Status { get; set; }
    }
}
