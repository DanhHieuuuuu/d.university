using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiChat
{
    public class AskKpiChatCommand : ICommand<string>
    {
        public string Question { get; set; }
    }
}
