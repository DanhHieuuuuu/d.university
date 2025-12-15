using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class InsertReceptionTimeLogDto : ICommand
    {
        public int ReceptionTimeId { get; set; }
        public string Type { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? Reason { get; set; }
    }
}
