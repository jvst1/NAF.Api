﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NAF.Domain.Entities;

namespace NAF.Infra.Data.ConfigurationMap
{
    public class ChamadoHistoricoMap : IEntityTypeConfiguration<ChamadoHistorico>
    {
        public void Configure(EntityTypeBuilder<ChamadoHistorico> builder)
        {
            builder.HasKey(h => h.Codigo);

            builder.Property(c => c.Codigo).IsRequired();
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.DtInclusao).IsRequired();
            builder.Property(h => h.TipoAlteracao).IsRequired();
            builder.Property(h => h.CampoAlterado).HasMaxLength(255).IsRequired(false);
            builder.Property(h => h.ValorAntigo).IsRequired(false);
            builder.Property(h => h.ValorNovo).IsRequired(false);

            builder.HasOne(h => h.Usuario)
                   .WithMany()
                   .HasForeignKey(h => h.CodigoUsuario)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.Chamado)
                   .WithMany()
                   .HasForeignKey(h => h.CodigoChamado)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
