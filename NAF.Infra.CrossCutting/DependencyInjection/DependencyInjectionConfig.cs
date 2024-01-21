using Microsoft.Extensions.DependencyInjection;
using NAF.Application.Interfaces;
using NAF.Application.Services;
using NAF.Domain.Interface.Repositories;
using NAF.Domain.Interface.Services;
using NAF.Domain.Services;
using NAF.Infra.Data.Repository;

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

            #region Transient

            services.AddTransient<IAreaAppService, AreaAppService>();
            services.AddTransient<IServicoAppService, ServicoAppService>();
            services.AddTransient<IPerguntaFrequenteAppService, PerguntaFrequenteAppService>();

            #endregion
        }
        private static void ConfigureMiddlewares(IServiceCollection services)
        {
        }
        private static void ConfigureRepositories(IServiceCollection services)
        {
            #region Transient

            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IServicoRepository, ServicoRepository>();
            services.AddTransient<IPerguntaFrequenteRepository, PerguntaFrequenteRepository>();

            #endregion

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
            #region Transient

            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<IServicoService, ServicoService>();
            services.AddTransient<IPerguntaFrequenteService, PerguntaFrequenteService>();

            #endregion
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
