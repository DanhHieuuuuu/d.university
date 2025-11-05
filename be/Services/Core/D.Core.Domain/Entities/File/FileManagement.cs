using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.File
{
    [Table(nameof(FileManagement), Schema = DbSchema.File)]
    public class FileManagement : EntityBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? ApplicationField { get; set; }
    }
}
