using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NAF.Domain.Entities;
using NAF.Infra.Data.ConfigurationMap;

namespace NAF.Infra.Data.Context
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        protected readonly IConfiguration Configuration;

        #region DbSets

        public DbSet<Area> Area { get; set; }
        public DbSet<ChamadoComentario> ChamadoComentario { get; set; }
        public DbSet<ChamadoDocumento> ChamadoDocumento { get; set; }
        public DbSet<ChamadoHistorico> ChamadoHistorico { get; set; }
        public DbSet<Chamado> Chamado { get; set; }
        public DbSet<PerguntaFrequente> PerguntaFrequente { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<EnvioEmail> EnvioEmail { get; set; }

        #endregion

        public DatabaseContext(IConfiguration configuration) => Configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("NAF");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AreaMap());
            builder.ApplyConfiguration(new ChamadoComentarioMap());
            builder.ApplyConfiguration(new ChamadoDocumentoMap());
            builder.ApplyConfiguration(new ChamadoHistoricoMap());
            builder.ApplyConfiguration(new ChamadoMap());
            builder.ApplyConfiguration(new PerguntaFrequenteMap());
            builder.ApplyConfiguration(new ServicoMap());
            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new EnvioEmailMap());
        }
    }
}