using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace D.Core.Domain
{
    public class CoreDbContextFactory : IDesignTimeDbContextFactory<CoreDBContext>
    {
        public CoreDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoreDBContext>();
            optionsBuilder.UseSqlServer("Data Source=203.210.148.167,8092;Initial Catalog=DO_AN;User ID=sa;Password=labDev@123;Trust Server Certificate=True;MultipleActiveResultSets=True;");

            return new CoreDBContext(optionsBuilder.Options);
        }
    }
}
