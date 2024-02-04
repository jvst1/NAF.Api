using NAF.Infra.CrossCutting.DependencyInjection;
using NAF.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

internal class Program
{
    private static void Main(string[] args)
    {
        var allowOrigins = "Origins";

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder("Bearer").RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });

        builder.Services.ConfigureSwagger();

        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                             .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                             .AddEnvironmentVariables();

        builder.Services.ConfigureDatabase(builder.Configuration);
        builder.Services.ConfigureAppSettings(builder.Configuration);
        builder.Services.ConfigureIdentity();
        builder.Services.AddDependencyInjection();
        builder.Services.ConfigureAuthentication(builder.Configuration);
        builder.Services.ConfigureCors(allowOrigins);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(allowOrigins);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}