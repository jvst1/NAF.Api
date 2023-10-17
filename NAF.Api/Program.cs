using Microsoft.EntityFrameworkCore;
using NAF.Infra.CrossCutting.DependencyInjection;
using NAF.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(x =>
{
    x.UseMySql(
        builder.Configuration.GetConnectionString("NAF"),
        new MySqlServerVersion(new Version(1, 0, 0))
        );
});

builder.Services.AddDependencyInjection();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
