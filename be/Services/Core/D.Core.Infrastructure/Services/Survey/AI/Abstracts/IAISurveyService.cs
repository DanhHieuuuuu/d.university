using D.Core.Domain.Dtos.Survey.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Survey.AI.Abstracts
{
    public interface IAISurveyService
    {
        Task<bool> AnalyzeSurveyWithAIAsync(int reportId);
    }
}
