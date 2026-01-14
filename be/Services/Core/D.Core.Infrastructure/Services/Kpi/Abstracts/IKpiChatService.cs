using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiChatService
    {
        Task<string> GetKpiContextForChat(int userId);
        Task<string> AskKpiQuestion(string userQuery);
    }
}
