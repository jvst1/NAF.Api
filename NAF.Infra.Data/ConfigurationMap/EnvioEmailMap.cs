using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NAF.Domain.Entities;

namespace NAF.Infra.Data.ConfigurationMap
{
    public class EnvioEmailMap : IEntityTypeConfiguration<EnvioEmail>
    {
        public void Configure(EntityTypeBuilder<EnvioEmail> builder)
        {
            builder.HasKey(ee => ee.Codigo);

            builder.Property(ee => ee.Codigo).IsRequired();
            builder.Property(ee => ee.DtInclusao).IsRequired();
            builder.Property(ee => ee.De).IsRequired();
            builder.Property(ee => ee.Para).IsRequired();
            builder.Property(ee => ee.Copia).IsRequired(false);
            builder.Property(ee => ee.CopiaOculta).IsRequired(false);
            builder.Property(ee => ee.Assunto).IsRequired();
            builder.Property(ee => ee.Texto).IsRequired(false);
            builder.Property(ee => ee.Enviado).IsRequired();
            builder.Property(ee => ee.DataEnvio).IsRequired(false);
            builder.Property(ee => ee.Erro).IsRequired(false);
            builder.Property(ee => ee.ReplyTo).IsRequired(false);
            builder.Property(ee => ee.MessageId).IsRequired(false);
            builder.Property(ee => ee.Replace).IsRequired(false);
        }
    }
}
