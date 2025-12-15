using D.Core.Domain.Dtos.Delegation;
using D.Core.Domain.Entities.Delegation.Incoming;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts
{
    public interface IExcelService
    {
        Task CheckValidateDetailDelegationAsync(IFormFile file, List<ExcelColumnRule> rules);
        Task<List<DetailDelegationIncoming>> ParseExcelToListDetailDelegationAsync(IFormFile file);
    }
}
