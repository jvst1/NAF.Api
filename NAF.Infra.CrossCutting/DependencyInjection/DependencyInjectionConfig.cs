using Microsoft.Extensions.DependencyInjection;

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
        }
        private static void ConfigureMiddlewares(IServiceCollection services)
        {
        }
        private static void ConfigureRepositories(IServiceCollection services)
        {
        }
        private static void ConfigureServices(IServiceCollection services)
        {
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
