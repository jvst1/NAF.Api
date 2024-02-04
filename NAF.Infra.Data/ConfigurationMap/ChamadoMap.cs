using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NAF.Domain.Entities;

namespace NAF.Infra.Data.ConfigurationMap
{
    public class ChamadoMap : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Codigo).IsRequired();
            builder.Property(c => c.DtInclusao).IsRequired();
            builder.Property(c => c.CodigoOperador).IsRequired(false);
            builder.Property(c => c.Titulo).HasMaxLength(255).IsRequired(false);
            builder.Property(c => c.Descricao).IsRequired(false);
            builder.Property(c => c.Situacao).IsRequired();
            builder.Property(c => c.DtAlteracao).IsRequired();
            builder.Property(c => c.CodigoUsuario).IsRequired();
            builder.Property(c => c.CodigoServico).IsRequired();
        }
    }
}
