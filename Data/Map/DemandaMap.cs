using APIProjeto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIProjeto.Data.Map
{
    public class DemandaMap : IEntityTypeConfiguration<Demanda>
    {
        public void Configure(EntityTypeBuilder<Demanda> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdPaciente).IsRequired();
            builder.Property(x => x.IdProcedimento).IsRequired();
            builder.Property(x => x.IdUnidadeSolicitante).IsRequired();
            builder.Property(x => x.IdUsuarioSolicitante).IsRequired();
            builder.Property(x => x.DataSolicitacao).IsRequired();
            builder.Property(x => x.Prioridade).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Justificativa).IsRequired();
        }
    }
}
