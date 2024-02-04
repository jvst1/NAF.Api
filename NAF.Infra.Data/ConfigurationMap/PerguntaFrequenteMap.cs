using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NAF.Domain.Entities;

namespace NAF.Infra.Data.ConfigurationMap
{
    public class PerguntaFrequenteMap : IEntityTypeConfiguration<PerguntaFrequente>
    {
        public void Configure(EntityTypeBuilder<PerguntaFrequente> builder)
        {
            builder.HasKey(pf => pf.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Codigo).IsRequired();
            builder.Property(c => c.DtInclusao).IsRequired();
            builder.Property(pf => pf.Pergunta).IsRequired(false).HasMaxLength(500);
            builder.Property(pf => pf.Resposta).IsRequired(false).HasMaxLength(2000);
            builder.Property(pf => pf.DtAlteracao).IsRequired();

            builder.HasIndex(pf => pf.DtAlteracao);
        }
    }
}
