using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace D.Auth.Domain
{
    public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDBContext>
    {
        public AuthDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDBContext>();
            optionsBuilder.UseSqlServer("Data Source=203.210.148.167,8092;Initial Catalog=DO_AN;User ID=sa;Password=labDev@123;Trust Server Certificate=True;MultipleActiveResultSets=True;");

            return new AuthDBContext(optionsBuilder.Options);
        }
    }
}
