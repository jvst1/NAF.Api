namespace NAF.Api.Extensions
{
    public static class CorsSetup
    {
        private static readonly string CorsOrigins = "https://naf-6phyih6xc-jvsts-projects.vercel.app";

        public static void ConfigureCors(this IServiceCollection services, string origins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: origins, policy =>
                {
                    policy.WithOrigins(CorsOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
    }
}
