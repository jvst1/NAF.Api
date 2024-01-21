using Microsoft.EntityFrameworkCore;
using NAF.Infra.Data.Context;

namespace NAF.Api.Extensions
{
    public static class DatabaseSetup
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(x =>
            {
                x.UseMySql(
                    configuration.GetConnectionString("NAF"),
                    new MySqlServerVersion(new Version(1, 0, 0))
                    );
            });
        }
    }
}
