﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NAF.Domain.Entities;

namespace NAF.Infra.Data.ConfigurationMap
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Codigo);

            builder.Property(c => c.Codigo).IsRequired();
            builder.Property(c => c.DtInclusao).IsRequired();
            builder.Property(u => u.Nome).HasMaxLength(255).IsRequired(false);
            builder.Property(u => u.Identificador).HasMaxLength(100).IsRequired(false);
            builder.Property(u => u.Email).HasMaxLength(255).IsRequired(false);
            builder.Property(u => u.TelefoneCelular).HasMaxLength(20).IsRequired(false);
            builder.Property(u => u.DocumentoFederal).HasMaxLength(20).IsRequired(false);
            builder.Property(u => u.TipoPerfil).IsRequired();
            builder.Property(u => u.Situacao).IsRequired();
            builder.Property(u => u.IdentityUserId).IsRequired();

            builder.HasIndex(u => u.DocumentoFederal).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
