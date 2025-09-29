using APIProjeto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIProjeto.Data.Map
{
    public class AgendamentoMap : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdDemanda).IsRequired();
            builder.Property(x => x.IdVaga).IsRequired();
            builder.Property(x => x.IdUsuarioRegulador).IsRequired();
            builder.Property(x => x.DataAgendamento).IsRequired();
            builder.Property(x => x.DataRealizacao);
            builder.Property(x => x.StatusComparecimento);
        }
    }
}
