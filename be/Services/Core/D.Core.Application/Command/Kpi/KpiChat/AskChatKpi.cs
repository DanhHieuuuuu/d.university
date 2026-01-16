using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiChat;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.Core.Infrastructure.Services.Kpi.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Kpi.KpiChat
{
    public class AskChatKpi : ICommandHandler<AskKpiChatCommand, string>
    {
        private readonly IKpiChatService _service;

        public AskChatKpi(IKpiChatService service)
        {
            _service = service;
        }

        public async Task<string> Handle(AskKpiChatCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
                return "Vui lòng nhập câu hỏi.";
            return await _service.AskKpiQuestion(request.Question);
        }
    }
}
