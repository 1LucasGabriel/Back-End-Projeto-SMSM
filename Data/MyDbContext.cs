using APIProjeto.Data.Map;
using APIProjeto.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Demanda> Demandas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<UnidadeSaude> UnidadesSaude { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vaga> Vagas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendamentoMap());
            modelBuilder.ApplyConfiguration(new DemandaMap());
            modelBuilder.ApplyConfiguration(new EspecialidadeMap());
            modelBuilder.ApplyConfiguration(new PacienteMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new ProcedimentoMap());
            modelBuilder.ApplyConfiguration(new UnidadeMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new VagaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
