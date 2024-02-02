using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NAF.Domain.Entities;

namespace NAF.Infra.Data.ConfigurationMap
{
    public class AreaMap : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(a => a.Codigo);

            builder.Property(c => c.Codigo).IsRequired();
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.DtInclusao).IsRequired();
            builder.Property(a => a.Nome).IsRequired().HasMaxLength(255);
            builder.Property(a => a.DtAlteracao).IsRequired();
        }
    }

}
