using APIProjeto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIProjeto.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Cpf).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Senha).IsRequired().HasMaxLength(30);
            builder.Property(x => x.IdUnidadeSaude).IsRequired();
            builder.Property(x => x.IdPerfil).IsRequired();
        }
    }
}
