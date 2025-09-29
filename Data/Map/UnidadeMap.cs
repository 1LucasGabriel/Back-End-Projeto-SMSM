using APIProjeto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIProjeto.Data.Map
{
    public class UnidadeMap : IEntityTypeConfiguration<UnidadeSaude>
    {
        public void Configure(EntityTypeBuilder<UnidadeSaude> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Tipo).IsRequired();
            builder.Property(x => x.Regiao).IsRequired();
        }
    }
}
