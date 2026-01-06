using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Sysvar
{
    [Table(nameof(SysVar), Schema = DbSchema.Sysvar)]
    public class SysVar : EntityBase
    {

        [Required]
        [MaxLength(128)]
        [Unicode(false)]
        public required string GrName { get; set; }

        [Required]
        [MaxLength(128)]
        [Unicode(false)]
        public required string VarName { get; set; }

        [Required]
        [MaxLength(128)]
        [Unicode(false)]
        public required string VarValue { get; set; }

        [MaxLength(128)]
        [Unicode(true)]
        public string? VarDesc { get; set; }
    }
}
