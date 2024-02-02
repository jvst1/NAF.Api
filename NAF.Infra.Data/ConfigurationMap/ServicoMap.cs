using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NAF.Domain.Entities;

namespace NAF.Infra.Data.ConfigurationMap
{
    public class ServicoMap : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(s => s.Codigo);

            builder.Property(c => c.Codigo).IsRequired();
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.DtInclusao).IsRequired();
            builder.Property(s => s.Nome).HasMaxLength(255).IsRequired();
            builder.Property(s => s.Descricao).IsRequired(false);
            builder.Property(s => s.DtAlteracao).IsRequired();

            builder.HasOne(s => s.Area)
                   .WithMany()
                   .HasForeignKey(s => s.CodigoArea)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
