using APIProjeto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIProjeto.Data.Map
{
    public class VagaMap : IEntityTypeConfiguration<Vaga>
    {
        public void Configure(EntityTypeBuilder<Vaga> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdProcedimento).IsRequired();
            builder.Property(x => x.IdUnidadeOfertante).IsRequired();
            builder.Property(x => x.Quantidade).IsRequired();
            builder.Property(x => x.MesAnoReferencia).IsRequired();
        }
    }
}
