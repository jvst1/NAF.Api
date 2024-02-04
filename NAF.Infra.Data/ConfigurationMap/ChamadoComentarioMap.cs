using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NAF.Domain.Entities;

namespace NAF.Infra.Data.ConfigurationMap
{
    public class ChamadoComentarioMap : IEntityTypeConfiguration<ChamadoComentario>
    {
        public void Configure(EntityTypeBuilder<ChamadoComentario> builder)
        {
            builder.HasKey(cc => cc.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Codigo).IsRequired();
            builder.Property(c => c.DtInclusao).IsRequired();
            builder.Property(cc => cc.Mensagem).IsRequired(false);
            builder.Property(cc => cc.DtAlteracao).IsRequired();
            builder.Property(cc => cc.CodigoUsuario).IsRequired();
            builder.Property(cc => cc.CodigoChamado).IsRequired();
        }
    }

}
