using Microsoft.Extensions.DependencyInjection;
using NAF.Application.Interfaces;
using NAF.Application.Services;
using NAF.Domain.Interface.Services;
using NAF.Domain.Services;

namespace NAF.Infra.CrossCutting.DependencyInjection
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            ConfigureHttpContexts(services);

            ConfigureScoped(services);

            ConfigureTransientBase(services);

            ConfigureApplicationServices(services);

            ConfigureDomainServices(services);

            ConfigureServices(services);

            ConfigureRepositories(services);

            ConfigureMiddlewares(services);
        }
        private static void ConfigureApplicationServices(IServiceCollection services)
        {
            #region Scoped

            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IAuthAppService, AuthAppService>();

            #endregion
        }
        private static void ConfigureMiddlewares(IServiceCollection services)
        {
        }
        private static void ConfigureRepositories(IServiceCollection services)
        {
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            #region Scoped

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            #endregion

            #region Singleton

            services.AddSingleton<IJwtService, JwtService>();

            #endregion
        }
        private static void ConfigureDomainServices(IServiceCollection services)
        {
        }
        private static void ConfigureTransientBase(IServiceCollection services)
        {
        }
        private static void ConfigureScoped(IServiceCollection services)
        {
        }
        private static void ConfigureHttpContexts(IServiceCollection services)
        {
        }
    }
}
