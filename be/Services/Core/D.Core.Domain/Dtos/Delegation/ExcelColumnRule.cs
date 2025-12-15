using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation
{
    public class ExcelColumnRule
    {
        public string Header { get; set; }
        public bool Required { get; set; }
        public Func<string, bool>? Validator { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
