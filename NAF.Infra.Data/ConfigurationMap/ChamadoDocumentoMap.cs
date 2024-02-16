using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NAF.Domain.Entities;

namespace NAF.Infra.Data.ConfigurationMap
{
    public class ChamadoDocumentoMap : IEntityTypeConfiguration<ChamadoDocumento>
    {
        public void Configure(EntityTypeBuilder<ChamadoDocumento> builder)
        {
            builder.HasKey(cd => cd.Codigo);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Codigo).IsRequired();
            builder.Property(c => c.DtInclusao).IsRequired();
            builder.Property(cd => cd.NomeArquivo).HasMaxLength(255).IsRequired(false);
            builder.Property(cd => cd.Arquivo).IsRequired(false);

            builder.Property(cd => cd.CodigoUsuario).IsRequired();
            builder.Property(cd => cd.CodigoChamado).IsRequired();

            builder.HasOne(c => c.Usuario)
                   .WithMany()
                   .HasForeignKey(c => c.CodigoUsuario)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Chamado)
                   .WithMany()
                   .HasForeignKey(c => c.CodigoChamado)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
