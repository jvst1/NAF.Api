using NAF.Infra.Data.External_Dependence;

namespace NAF.Api.Extensions
{
    public static class AppSettingsSetup
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(options => configuration.GetSection("AppSettings").Bind(options));
        }
    }
}
