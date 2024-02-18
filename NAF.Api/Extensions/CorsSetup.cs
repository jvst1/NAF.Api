﻿namespace NAF.Api.Extensions
{
    public static class CorsSetup
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration, string origins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: origins, policy =>
                {
                    policy.WithOrigins(configuration.GetSection("AppSettings:WebUrl").Value)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
    }
}
