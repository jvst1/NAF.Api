using Microsoft.AspNetCore.Identity;
using NAF.Infra.Data.Context;

namespace NAF.Api.Extensions
{
    public static class IdentitySetup
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<DatabaseContext>()
                    .AddDefaultTokenProviders();
        }
    }
}
