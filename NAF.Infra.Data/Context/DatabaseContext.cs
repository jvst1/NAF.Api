using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NAF.Infra.Data.Context
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        protected readonly IConfiguration Configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("NAF");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}