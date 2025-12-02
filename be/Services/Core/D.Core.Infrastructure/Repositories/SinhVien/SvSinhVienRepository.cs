using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Domain.Entities.SinhVien;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

        public async Task<string> GenerateMssv(int khoahoc)
        {
            var khoaHoc = await _dbContext.Set<DmKhoaHoc>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == khoahoc);

            if (khoaHoc == null) throw new Exception("Không tìm thấy thông tin khóa học!");

            string prefixString = Regex.Match(khoaHoc.MaKhoaHoc ?? string.Empty, @"\d+").Value;
            if (string.IsNullOrEmpty(prefixString)) prefixString = "00";

            var lastStudent = await TableNoTracking
                            .Where(x => x.KhoaHoc == khoahoc
                                        && !string.IsNullOrEmpty(x.Mssv)
                                        && x.Mssv.StartsWith(prefixString))
                            .OrderByDescending(x => x.Mssv.Length)
                            .ThenByDescending(x => x.Mssv)
                            .FirstOrDefaultAsync();

            int nextSequence = 1;
            if (lastStudent != null)
            {
                string lastSeqStr = lastStudent.Mssv.Substring(prefixString.Length);

                if (int.TryParse(lastSeqStr, out int lastSeq))
                {
                    nextSequence = lastSeq + 1;
                }
            }

            return $"{prefixString}{nextSequence:D5}";

            //var count = await TableNoTracking.CountAsync(x => x.KhoaHoc == khoahoc);
            //var nextSequence = count + 1;

            //string mssv = $"{khoahoc}{nextSequence:D5}";
            //return mssv;
        }

        public string GenerateEmail(string mssv)
        {
            string email = $"{mssv}@edu.duniversity.vn";

            return email;
        }

        //public SvSinhVien? GetByMssv(string mssv)
        //{
        //    return Table.FirstOrDefault(x => x.Mssv == mssv);
        //}
        public async Task<SvSinhVien?> GetByMssv(string mssv)
        {
            return await Table.FirstOrDefaultAsync(x => x.Mssv == mssv);
        }
    }

    public interface ISvSinhVienRepository : IRepositoryBase<SvSinhVien>
    {
        bool IsSoCccdExits(string cccd);
        Task<string> GenerateMssv(int khoa);
        string GenerateEmail(string mssv);
        Task<SvSinhVien?> GetByMssv(string mssv);
    }
}
