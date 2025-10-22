using D.Core.Domain.Entities.SinhVien;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Repositories.SinhVien
{
    public class SvSinhVienRepository : RepositoryBase<SvSinhVien>, ISvSinhVienRepository
    {
        public SvSinhVienRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public bool IsSoCccdExits(string cccd)
        {
            return TableNoTracking.Any(x => x.SoCccd == cccd);
        }

        public string GenerateMssv(int khoa)
        {
            int currentCount = TableNoTracking.Count(x => x.Khoa == khoa) + 1;

            string mssv = $"{currentCount:D5}{khoa:D2}";

            return mssv;
        }

        public string GenerateEmail(string mssv)
        {
            string email = $"{mssv}@edu.duniversity.vn";

            return email;
        }

        public SvSinhVien? GetByMssv(string mssv)
        {
            return Table.FirstOrDefault(x => x.Mssv == mssv);
        }
    }

    public interface ISvSinhVienRepository : IRepositoryBase<SvSinhVien>
    {
        bool IsSoCccdExits(string cccd);
        string GenerateMssv(int khoa);
        string GenerateEmail(string mssv);
        SvSinhVien? GetByMssv(string mssv);
    }
}
