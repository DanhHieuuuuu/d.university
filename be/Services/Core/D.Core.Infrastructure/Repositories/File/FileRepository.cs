using D.Core.Domain.Entities.File;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Repositories.File
{
    public class FileRepository : RepositoryBase<FileManagement>, IFileRepository
    {
        public FileRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public FileManagement? GetById(int id)
        {
            return Table.FirstOrDefault(x => x.Id == id && x.Deleted != true);
        }

        public bool IsNameExist(string name)
        {
            return TableNoTracking.Any(x => x.Name == name && x.Deleted != true);
        }

        public bool IsIdExist(int id)
        {
            return TableNoTracking.Any(x => x.Id == id && x.Deleted != true);
        }
    }

    public interface IFileRepository : IRepositoryBase<FileManagement>
    {
        FileManagement? GetById(int id);
        bool IsNameExist(string name);
        bool IsIdExist(int id);
    }
}
