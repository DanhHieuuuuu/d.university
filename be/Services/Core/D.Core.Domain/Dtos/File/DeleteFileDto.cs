using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.File
{
    public class DeleteFileDto : ICommand<bool>
    {
        public int Id { get; set; }
    }
}