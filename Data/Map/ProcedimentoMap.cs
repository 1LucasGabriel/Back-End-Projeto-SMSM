using APIProjeto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIProjeto.Data.Map
{
    public class ProcedimentoMap : IEntityTypeConfiguration<Procedimento>
    {
        public void Configure(EntityTypeBuilder<Procedimento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CodigoProcedimento).IsRequired();
            builder.Property(x => x.NomeProcedimento).IsRequired();
            builder.Property(x => x.Tipo).IsRequired();
            builder.Property(x => x.IdEspecialidade).IsRequired();
        }
    }
}
