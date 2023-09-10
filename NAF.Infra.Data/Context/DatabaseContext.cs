using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NAF.Infra.Data.Context
{
    public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("ConnectionString");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}